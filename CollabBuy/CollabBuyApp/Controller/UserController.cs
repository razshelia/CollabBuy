using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller yang bertindak sebagai Mandor alur manajemen pengguna.
    /// Menangani Login, Registrasi, dan aksi Admin terhadap User.
    /// </summary>
    public class UserController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly UserRepository _userRepo;
        private readonly ActivityLogRepository _logRepo;

        // === KONSTRUKTOR ===
        public UserController()
        {
            this._userRepo = new UserRepository();
            this._logRepo = new ActivityLogRepository();
        }

        // =======================================================
        // FITUR AUTENTIKASI (LOGIN)
        // =======================================================
        public (User user, string pesan) Login(string username, string password)
        {
            (User user, string pesan) hasil;

            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    hasil = (null, "Username dan Password tidak boleh kosong!");
                }
                else
                {
                    List<User> semuaUser = this._userRepo.GetAll();
                    string hashPasswordInput = this.HashSha256(password);

                    User userDitemukan = null;
                    string pesanLogin = "";

                    foreach (User u in semuaUser)
                    {
                        if (u.GetUsername() == username && u.GetPassword() == hashPasswordInput)
                        {
                            if (u.IsDiblokir())
                            {
                                userDitemukan = null;
                                pesanLogin = "Akun Anda telah diblokir oleh Admin!";
                                break;
                            }
                            else
                            {
                                // Catat log aktivitas login
                                ActivityLog log = new ActivityLog(u.GetIdUser(), "Berhasil login ke sistem");
                                this._logRepo.Insert(log);

                                userDitemukan = u;
                                pesanLogin = "Login berhasil! Selamat datang, " + u.GetNama();
                                break;
                            }
                        }
                        else
                        {
                            bool lanjutCari = true; // Penugasan nyata menghindari else kosong
                        }
                    }

                    // Evaluasi hasil pencarian dari loop
                    if (userDitemukan != null)
                    {
                        hasil = (userDitemukan, pesanLogin);
                    }
                    else if (pesanLogin != "") // Tertangkap kasus akun diblokir
                    {
                        hasil = (null, pesanLogin);
                    }
                    else
                    {
                        hasil = (null, "Username atau Password salah!");
                    }
                }
            }
            catch (Exception ex)
            {
                hasil = (null, "Terjadi error sistem saat login: " + ex.Message);
            }

            return hasil;
        }

        // =======================================================
        // FITUR REGISTRASI
        // =======================================================
        public (bool sukses, string pesan) RegistrasiPembeli(string nama, string email, string noTelepon, string username, string password)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                string hashPassword = this.HashSha256(password);
                Pembeli pembeliBaru = new Pembeli(nama, username, hashPassword);

                pembeliBaru.SetEmail(email);
                pembeliBaru.SetNomorTelepon(noTelepon);

                pembeliBaru.Validate();
                this._userRepo.Insert(pembeliBaru);

                hasil = (true, "Yey! Akun kamu berhasil dibuat. Langsung login aja bestie!");
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("username"))
                {
                    hasil = (false, "Yah, Username itu udah dipakai orang lain. Cari yang lain yuk!");
                }
                else if (ex.Message.Contains("email"))
                {
                    hasil = (false, "Email ini udah pernah didaftarin. Lupa password kah?");
                }
                else
                {
                    hasil = (false, "Waduh error sistem nih: " + ex.Message);
                }
            }

            return hasil;
        }

        public (bool sukses, string pesan) RegistrasiPenjual(string nama, string username, string password, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                string hashPassword = this.HashSha256(password);
                Penjual penjualBaru = new Penjual(nama, username, hashPassword);

                penjualBaru.SetNim(nim);
                penjualBaru.SetNamaToko(namaToko);
                penjualBaru.SetTahunMasuk(tahunMasuk);
                penjualBaru.SetBuktiKtm(buktiKtm);

                penjualBaru.Validate();
                this._userRepo.Insert(penjualBaru);

                hasil = (true, "Registrasi penjual berhasil! Menunggu verifikasi Admin.");
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("username") || ex.Message.Contains("nim"))
                {
                    hasil = (false, "Username atau NIM sudah terdaftar!");
                }
                else
                {
                    hasil = (false, "Error sistem: " + ex.Message);
                }
            }

            return hasil;
        }

        // =======================================================
        // FITUR ADMIN & MANAJEMEN PROFIL
        // =======================================================
        public (bool sukses, string pesan) TindakPenjualNakal(int idAduan, int idPenjual, string balasanAdmin)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                if (string.IsNullOrWhiteSpace(balasanAdmin))
                {
                    hasil = (false, "Balasan/alasan penindakan wajib diisi!");
                }
                else
                {
                    this._userRepo.TindakPenjualNakal(idAduan, idPenjual, balasanAdmin.Trim());
                    hasil = (true, "Penjual berhasil diblokir dan aduan telah diselesaikan.");
                }
            }
            catch (Exception ex)
            {
                hasil = (false, "Gagal menindak penjual: " + ex.Message);
            }

            return hasil;
        }

        public (bool sukses, string pesan) ValidasiPenjual(int idPenjual)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                User user = this._userRepo.GetById(idPenjual);

                if (user == null)
                {
                    hasil = (false, "User tidak ditemukan!");
                }
                else
                {
                    Penjual penjual = user as Penjual;

                    if (penjual == null)
                    {
                        hasil = (false, "User ini bukan penjual!");
                    }
                    else
                    {
                        penjual.Approve();
                        this._userRepo.Update(penjual);
                        hasil = (true, "Penjual berhasil diverifikasi!");
                    }
                }
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                hasil = (false, "Terjadi kesalahan sistem: " + ex.Message);
            }

            return hasil;
        }

        public DataTable GetAntreanLapak()
        {
            DataTable tabelAntrean;
            try
            {
                tabelAntrean = this._userRepo.GetPendingVerifikasi();
            }
            catch
            {
                tabelAntrean = new DataTable();
            }
            return tabelAntrean;
        }

        public (bool sukses, string pesan) UpdateProfil(User user, string rawPasswordBaru)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                if (!string.IsNullOrEmpty(rawPasswordBaru))
                {
                    user.SetPassword(this.HashSha256(rawPasswordBaru));
                }
                else
                {
                    bool lewatiUbahPassword = true; // Penugasan nyata menghindari else kosong
                }

                user.Validate();
                this._userRepo.Update(user);

                hasil = (true, "Yey! Profil kamu berhasil diperbarui.");
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                hasil = (false, "Yah gagal update profil: " + ex.Message);
            }

            return hasil;
        }

        public bool CekPendingVerifikasi(int idUser)
        {
            bool statusPending = this._userRepo.CheckPendingVerification(idUser);
            return statusPending;
        }

        public (bool sukses, string pesan) AjukanVerifikasiToko(int idUser, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                if (buktiKtm == null || buktiKtm.Length == 0)
                {
                    hasil = (false, "Foto KTM wajib di-upload ya bestie buat bukti!");
                }
                else
                {
                    this._userRepo.AjukanLapakBaru(idUser, nim, namaToko, tahunMasuk, buktiKtm);
                    hasil = (true, "Pengajuan lapak berhasil dikirim! Silakan tunggu konfirmasi Admin.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("nim"))
                {
                    hasil = (false, "NIM ini udah dipakai untuk lapak lain!");
                }
                else
                {
                    hasil = (false, "Gagal mengajukan toko: " + ex.Message);
                }
            }

            return hasil;
        }

        // =======================================================
        // METHOD BANTUAN PRIVATE (HELPER)
        // =======================================================
        private string HashSha256(string input)
        {
            string hasilHash;

            if (string.IsNullOrEmpty(input))
            {
                hasilHash = "";
            }
            else
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder builder = new StringBuilder();

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    hasilHash = builder.ToString();
                }
            }

            return hasilHash;
        }
    }
}
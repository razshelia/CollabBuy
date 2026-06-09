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
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return (null, "Username dan Password tidak boleh kosong!");

                string hashPasswordInput = this.HashSha256(password);
                User userDitemukan = this._userRepo.GetByUsername(username);

                if (userDitemukan == null)
                    return (null, "Username atau Password salah!");

                if (userDitemukan.Password != hashPasswordInput)
                    return (null, "Username atau Password salah!");

                if (userDitemukan.IsDiblokir)
                    return (null, "Akun Anda telah diblokir oleh Admin!");

                // Upgrade peran jika Penjual sudah terverifikasi
                if (userDitemukan is Penjual penjualCek && penjualCek.GetStatusPersetujuan())
                {
                    try { userDitemukan.Peran = "Penjual"; } catch { }
                }

                ActivityLog log = new ActivityLog(userDitemukan.IdUser, "Berhasil login ke sistem");
                this._logRepo.Insert(log);

                return (userDitemukan, "Login berhasil! Selamat datang, " + userDitemukan.Nama);
            }
            catch (Exception ex)
            {
                return (null, "Terjadi error sistem saat login: " + ex.Message);
            }
        }

        // =======================================================
        // FITUR REGISTRASI
        // =======================================================
        public (bool sukses, string pesan) RegistrasiPembeli(string nama, string email, string noTelepon, string username, string password)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                // Validasi panjang password RAW dulu sebelum di-hash
                if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                {
                    return (false, "Password minimal 8 karakter ya!");
                }

                string hashPassword = this.HashSha256(password);
                Pembeli pembeliBaru = new Pembeli(nama, username, hashPassword);

                pembeliBaru.Email = email;
                pembeliBaru.NomorTelepon = noTelepon;

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
                    hasil = (false, "Yah, Username itu udah dipakai orang lain. Cari yang lain yuk!");
                else if (ex.Message.Contains("email"))
                    hasil = (false, "Email ini udah pernah didaftarin. Lupa password kah?");
                else
                    hasil = (false, "Waduh error sistem nih: " + ex.Message);
            }

            return hasil;
        }

        public (bool sukses, string pesan) RegistrasiPenjual(string nama, string username, string password, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            (bool sukses, string pesan) hasil;

            try
            {
                // Validasi panjang password RAW dulu sebelum di-hash
                if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                {
                    return (false, "Password minimal 8 karakter ya!");
                }

                string hashPassword = this.HashSha256(password);
                Penjual penjualBaru = new Penjual(nama, username, hashPassword);

                penjualBaru.Nim = nim;
                penjualBaru.NamaToko = namaToko;
                penjualBaru.TahunMasuk = tahunMasuk;
                penjualBaru.BuktiKtm = buktiKtm;

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
                    hasil = (false, "Username atau NIM sudah terdaftar!");
                else
                    hasil = (false, "Error sistem: " + ex.Message);
            }

            return hasil;
        }

        // =======================================================
        // FITUR ADMIN & MANAJEMEN PROFIL
        // =======================================================
        public (bool sukses, string pesan) TindakPenjualNakal(int idAduan, int idPenjual, string balasanAdmin)
        {
            if (string.IsNullOrWhiteSpace(balasanAdmin))
                return (false, "Balasan/alasan penindakan wajib diisi!");

            try
            {
                this._userRepo.TindakPenjualNakal(idAduan, idPenjual, balasanAdmin.Trim());
                return (true, "Penjual berhasil diblokir dan aduan telah diselesaikan.");
            }
            catch (Exception ex)
            {
                return (false, "Gagal menindak penjual: " + ex.Message);
            }
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
                        ActivityLog log = new ActivityLog(idPenjual, "Akun penjual berhasil diverifikasi oleh Admin");
                        this._logRepo.Insert(log);

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

        public (bool sukses, string pesan) UpdateProfil(User user, string rawPasswordBaru, string rawPasswordLama = null)
        {
            (bool sukses, string pesan) hasil;
            try
            {
                if (!string.IsNullOrWhiteSpace(rawPasswordBaru) && rawPasswordLama != null)
                {
                    // Validasi panjang password RAW sebelum di-hash
                    if (rawPasswordBaru.Length < 8)
                    {
                        return (false, "Password baru minimal 8 karakter ya!");
                    }

                    string hashLama = this.HashSha256(rawPasswordLama);
                    if (user.Password != hashLama)
                    {
                        return (false, "Password lama salah! Coba lagi ya bestie.");
                    }

                    user.Password = this.HashSha256(rawPasswordBaru);
                }

                this._userRepo.Update(user);

                ActivityLog log = new ActivityLog(user.IdUser, "Update profil akun.");
                this._logRepo.Insert(log);

                hasil = (true, "Profil berhasil disimpan! Lo makin kece bestie ✨");
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("username"))
                    hasil = (false, "Username sudah dipakai orang lain, coba username lain!");
                else
                    hasil = (false, "Error sistem: " + ex.Message);
            }
            return hasil;
        }
        public bool IsUsernameAvailable(int idUserSaatIni, string username)
        {
            return this._userRepo.IsUsernameAvailable(idUserSaatIni, username);
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
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());  // tampil pesan ramah, tidak crash
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("nim"))
                    hasil = (false, "NIM ini udah dipakai untuk lapak lain!");
                else
                    hasil = (false, "Gagal mengajukan toko: " + ex.Message);
            }

            return hasil;
        }

        // =======================================================
        // METHOD BANTUAN PRIVATE (HELPER)
        // =======================================================
        private string HashSha256(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        public int? VerifikasiIdentitasUser(string username, string email, string nomorTelepon)
        {
            try { return this._userRepo.VerifikasiIdentitasUser(username, email, nomorTelepon); }
            catch { return null; }
        }

        public bool ResetPasswordUser(int idUser, string passwordBaru)
        {
            // Konsisten dengan syarat registrasi: minimal 8 karakter
            if (string.IsNullOrWhiteSpace(passwordBaru) || passwordBaru.Length < 8) return false;
            try
            {
                string hash = this.HashSha256(passwordBaru);
                return this._userRepo.ResetPasswordUser(idUser, hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserController.ResetPasswordUser] Error: " + ex.Message);
                return false;
            }
        }
    }
}
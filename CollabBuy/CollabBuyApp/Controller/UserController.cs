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
        // =======================================================
        // TIMPA METHOD RegistrasiPembeli
        // =======================================================
        public (bool sukses, string pesan) RegistrasiPembeli(string nama, string email, string noTelepon, string username, string password)
        {
            try
            {
                // Lempar exception langsung untuk plaintext password sebelum di-hash
                if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                    throw new InvalidOrderException("Password minimal 8 karakter ya!", "password", "USER_PASS_PENDEK");

                string hashPassword = this.HashSha256(password);
                Pembeli pembeliBaru = new Pembeli(nama, username, hashPassword);

                pembeliBaru.Email = email;
                pembeliBaru.NomorTelepon = noTelepon;

                pembeliBaru.Validate();
                this._userRepo.Insert(pembeliBaru);

                return (true, "Yey! Akun kamu berhasil dibuat. Langsung login aja bestie!");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("username")) return (false, "Yah, Username itu udah dipakai orang lain. Cari yang lain yuk!");
                if (ex.Message.Contains("email")) return (false, "Email ini udah pernah didaftarin. Lupa password kah?");
                return (false, "Waduh error sistem nih: " + ex.Message);
            }
        }

        // =======================================================
        // TIMPA METHOD RegistrasiPenjual
        // =======================================================
        public (bool sukses, string pesan) RegistrasiPenjual(string nama, string username, string password, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            try
            {
                // Lempar exception langsung
                if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                    throw new InvalidOrderException("Password minimal 8 karakter ya!", "password", "USER_PASS_PENDEK");

                string hashPassword = this.HashSha256(password);
                Penjual penjualBaru = new Penjual(nama, username, hashPassword);

                penjualBaru.Nim = nim;
                penjualBaru.NamaToko = namaToko;
                penjualBaru.TahunMasuk = tahunMasuk;
                penjualBaru.BuktiKtm = buktiKtm;

                penjualBaru.Validate();
                this._userRepo.Insert(penjualBaru);

                return (true, "Registrasi penjual berhasil! Menunggu verifikasi Admin.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("username") || ex.Message.Contains("nim")) return (false, "Username atau NIM sudah terdaftar!");
                return (false, "Error sistem: " + ex.Message);
            }
        }

        // =======================================================
        // TIMPA METHOD UpdateProfil
        // =======================================================
        public (bool sukses, string pesan) UpdateProfil(User user, string rawPasswordBaru, string rawPasswordLama = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(rawPasswordBaru) && rawPasswordLama != null)
                {
                    // Lempar exception
                    if (rawPasswordBaru.Length < 8)
                        throw new InvalidOrderException("Password baru minimal 8 karakter ya!", "password", "USER_PASS_PENDEK");

                    string hashLama = this.HashSha256(rawPasswordLama);
                    string hashBaru = this.HashSha256(rawPasswordBaru);
                    user.UbahPassword(hashLama, hashBaru);
                }

                this._userRepo.Update(user);

                ActivityLog log = new ActivityLog(user.IdUser, "Update profil akun.");
                this._logRepo.Insert(log);

                return (true, "Profil berhasil disimpan! Lo makin kece bestie ✨");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToLower();
                if (errorMsg.Contains("username")) return (false, "Username sudah dipakai orang lain, coba username lain!");
                if (errorMsg.Contains("email")) return (false, "Email ini sudah dipakai oleh akun lain. Gunakan email lain ya!");
                return (false, "Error sistem: " + ex.Message);
            }
        }

        // =======================================================
        // FITUR ADMIN & MANAJEMEN PROFIL
        // =======================================================
        public (bool sukses, string pesan) TindakPenjualNakal(int idAduan, string namaToko, string balasanAdmin)
        {
            if (string.IsNullOrWhiteSpace(balasanAdmin))
                return (false, "Balasan/alasan penindakan wajib diisi!");

            if (string.IsNullOrWhiteSpace(namaToko))
                return (false, "Nama toko wajib diisi!");

            try
            {
                int? idPenjual = this._userRepo.GetIdPenjualByNamaToko(namaToko);

                if (idPenjual == null)
                    return (false, $"Toko \"{namaToko}\" tidak ditemukan atau belum terverifikasi.");

                this._userRepo.TindakPenjualNakal(idAduan, idPenjual.Value, balasanAdmin.Trim());
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
                // PERBAIKAN: validasi input lewat model Verification (guard clauses)
                // sebelum dikirim ke repository, supaya NIM/NamaToko/Tahun/KTM
                // tidak bisa lolos ke database tanpa validasi seperti sebelumnya.
                Verification pengajuan = new Verification(idUser, nim, namaToko, buktiKtm, tahunMasuk);
                pengajuan.Validate();

                this._userRepo.AjukanLapakBaru(idUser, pengajuan.Nim, pengajuan.NamaToko, pengajuan.TahunMasuk, pengajuan.BuktiKtm);
                hasil = (true, "Pengajuan lapak berhasil dikirim! Silakan tunggu konfirmasi Admin.");
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
        public (bool sukses, string pesan) TolakPenjual(int idPenjual, string alasan)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(alasan))
                    return (false, "Alasan penolakan wajib diisi!");

                User user = this._userRepo.GetById(idPenjual);
                if (user == null)
                    return (false, "User tidak ditemukan!");

                Penjual penjual = user as Penjual;
                if (penjual == null)
                    return (false, "User ini bukan penjual!");

                penjual.Reject(alasan); // method Reject sudah ada di model Penjual

                // Hapus data verifikasi dari DB agar bisa daftar ulang
                this._userRepo.TolakVerifikasiPenjual(idPenjual, alasan);

                ActivityLog log = new ActivityLog(idPenjual, $"Pengajuan lapak ditolak Admin. Alasan: {alasan}");
                this._logRepo.Insert(log);

                return (true, "Pengajuan lapak berhasil ditolak.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Error sistem: " + ex.Message);
            }
        }
    }
}
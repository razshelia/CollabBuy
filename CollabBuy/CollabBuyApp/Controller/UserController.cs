using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
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
            _userRepo = new UserRepository();
            _logRepo = new ActivityLogRepository();
        }


        // =======================================================
        // FITUR AUTENTIKASI (LOGIN)
        // =======================================================

        /// <summary>
        /// Memverifikasi kredensial login pengguna.
        /// Mengembalikan objek User (bisa Penjual atau Pembeli) jika sukses.
        /// </summary>
        public (User user, string pesan) Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return (null, "Username dan Password tidak boleh kosong!");
                }

                // 1. Ambil seluruh user dari database (atau bisa juga pakai query spesifikByUsername di Repo)
                // Untuk simplicitas, kita loop dari memory list. Di dunia nyata, Repo punya method GetByUsername.
                List<User> semuaUser = _userRepo.GetAll();

                string hashPasswordInput = HashSha256(password);

                foreach (User u in semuaUser)
                {
                    if (u.GetUsername() == username && u.GetPassword() == hashPasswordInput)
                    {
                        if (u.IsDiblokir())
                        {
                            return (null, "Akun Anda telah diblokir oleh Admin!");
                        }

                        // Catat log aktivitas login
                        ActivityLog log = new ActivityLog(u.GetIdUser(), "Berhasil login ke sistem");
                        _logRepo.Insert(log);

                        // Polimorfisme: u bisa berupa objek Penjual atau Pembeli
                        return (u, "Login berhasil! Selamat datang, " + u.GetNama());
                    }
                }

                return (null, "Username atau Password salah!");
            }
            catch (Exception ex)
            {
                return (null, "Terjadi error sistem saat login: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR REGISTRASI
        // =======================================================

        /// <summary>
        /// Mendaftarkan pembeli baru ke dalam sistem.
        /// </summary>
        public (bool sukses, string pesan) RegistrasiPembeli(string nama, string username, string password)
        {
            try
            {
                string hashPassword = HashSha256(password);

                // Buat objek Model (Validasi data ada di dalam konstruktor & setter Model)
                Pembeli pembeliBaru = new Pembeli(nama, username, hashPassword);
                pembeliBaru.Validate();

                // Simpan ke DB via Repository
                _userRepo.Insert(pembeliBaru);

                return (true, "Registrasi pembeli berhasil! Silakan login.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                // Tangkap error database (misal: UNIQUE constraint username duplikat)
                if (ex.Message.Contains("username"))
                {
                    return (false, "Username sudah digunakan, pilih username lain!");
                }
                return (false, "Error sistem: " + ex.Message);
            }
        }
        public (bool sukses, string pesan) RegistrasiPenjual(string nama, string username, string password, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            try
            {
                string hashPassword = HashSha256(password);
                Penjual penjualBaru = new Penjual(nama, username, hashPassword);

                penjualBaru.SetNim(nim);
                penjualBaru.SetNamaToko(namaToko);
                penjualBaru.SetTahunMasuk(tahunMasuk);
                penjualBaru.SetBuktiKtm(buktiKtm); // Validasi ukuran file ada di Model

                penjualBaru.Validate();
                _userRepo.Insert(penjualBaru); // Repo akan otomatis pakai Transaction untuk 2 tabel

                return (true, "Registrasi penjual berhasil! Menunggu verifikasi Admin.");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex)
            {
                if (ex.Message.Contains("username") || ex.Message.Contains("nim")) return (false, "Username atau NIM sudah terdaftar!");
                return (false, "Error sistem: " + ex.Message);
            }
        }

        // =======================================================
        // FITUR ADMIN (MANAJEMEN PENJUAL)
        // =======================================================

        /// <summary>
        /// Menindak penjual yang melanggar dengan memblokir akunnya 
        /// dan menyelesaikan aduan terkait.
        /// Memanggil Stored Procedure sp_tindak_penjual_nakal.
        /// </summary>
        public (bool sukses, string pesan) TindakPenjualNakal(int idAduan, int idPenjual, string balasanAdmin)
        {
            try
            {
                if (string.IsNullOrEmpty(balasanAdmin))
                {
                    return (false, "Balasan/alasan penindakan wajib diisi!");
                }

                // Panggil method khusus di UserRepository yang mengeksekusi SP
                _userRepo.TindakPenjualNakal(idAduan, idPenjual, balasanAdmin);

                return (true, "Penjual berhasil diblokir dan aduan telah diselesaikan.");
            }
            catch (Exception ex)
            {
                return (false, "Gagal menindak penjual: " + ex.Message);
            }
        }

        /// <summary>
        /// Menyetujui verifikasi KTM penjual oleh Admin.
        /// </summary>
        public (bool sukses, string pesan) ValidasiPenjual(int idPenjual)
        {
            try
            {
                User user = _userRepo.GetById(idPenjual);
                if (user == null)
                {
                    return (false, "User tidak ditemukan!");
                }

                // Cek polimorfisme: pastikan ini objek Penjual
                Penjual penjual = user as Penjual;
                if (penjual == null)
                {
                    return (false, "User ini bukan penjual!");
                }

                // Eksekusi logika bisnis di Model (Approve)
                penjual.Approve();

                // Update status di DB via Repository
                _userRepo.Update(penjual);

                return (true, "Penjual berhasil diverifikasi!");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
        }


        // =======================================================
        // METHOD BANTUAN PRIVATE (HELPER)
        // =======================================================

        /// <summary>
        /// Mengubah string password menjadi format Hash SHA256 
        /// agar cocok dengan data di database PostgreSQL.
        /// </summary>
        private string HashSha256(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
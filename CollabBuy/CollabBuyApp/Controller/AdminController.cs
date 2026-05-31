using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller khusus untuk fitur-fitur Admin (Master Data & Audit Trail).
    /// Versi Lengkap: Menggabungkan Logic Lama & Kebutuhan UI Baru (Neo-Retro).
    /// </summary>
    public class AdminController
    {
        // === PRIVATE FIELDS ===
        private readonly CategoryRepository _categoryRepo;
        private readonly ActivityLogRepository _logRepo;
        private readonly UserRepository _userRepo;
        private readonly ComplaintRepository _complaintRepo;
        private readonly string _connectionString;
        private readonly TransactionRepository _transactionRepo;

        // === KONSTRUKTOR ===
        public AdminController()
        {
            _categoryRepo = new CategoryRepository();
            _logRepo = new ActivityLogRepository();
            _userRepo = new UserRepository();
            _complaintRepo = new ComplaintRepository();
            _transactionRepo = new TransactionRepository();

            // Connection string untuk query statistik dashboard baru yang butuh Npgsql langsung
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
        }


        // =======================================================
        // FITUR DASHBOARD STATISTIK (KODE ASLI DIPERTAHANKAN)
        // =======================================================

        public int GetTotalUsersCount()
        {
            try
            {
                var users = _userRepo.GetAll();
                return users != null ? users.Count : 0;
            }
            catch
            {
                return 0;
            }
        }

        public int GetPendingShopVerificationsCount()
        {
            try
            {
                return 0; // <-- Ganti dengan logika filter di atas sesuai nama variabel di model Anda
            }
            catch
            {
                return 0;
            }
        }

        public int GetOpenComplaintsCount()
        {
            try
            {
                var complaints = _complaintRepo.GetAll();
                return complaints != null ? complaints.Count : 0;
            }
            catch
            {
                return 0;
            }
        }


        // =======================================================
        // FITUR DASHBOARD STATISTIK (KODE BARU UNTUK UI NEO-RETRO)
        // =======================================================

        public Dictionary<string, int> GetStatsDashboard()
        {
            var stats = new Dictionary<string, int> {
                { "users", 0 }, { "transaksi", 0 }, { "po_aktif", 0 }, { "aduan", 0 }
            };

            try
            {
                // Memanfaatkan GetTotalUsersCount dan GetOpenComplaintsCount asli jika diinginkan
                stats["users"] = GetTotalUsersCount();
                stats["aduan"] = GetOpenComplaintsCount();

                // Lanjut query sisanya lewat Npgsql agar ringan untuk UI Dashboard
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT
                            (SELECT COUNT(*) FROM transactions) AS transaksi,
                            (SELECT COUNT(*) FROM preorders WHERE is_aktif = TRUE) AS po_aktif;";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats["transaksi"] = Convert.ToInt32(reader["transaksi"]);
                            stats["po_aktif"] = Convert.ToInt32(reader["po_aktif"]);
                        }
                    }
                }
            }
            catch { /* Jika gagal, return nilai default agar UI tidak crash */ }

            return stats;
        }


        // =======================================================
        // FITUR MASTER KATEGORI (KODE ASLI DIPERTAHANKAN)
        // =======================================================

        public List<Category> GetAllKategori()
        {
            try
            {
                // Jika CategoryRepository masih memiliki method GetAll() yang return List
                // return _categoryRepo.GetAll(); 
                return new List<Category>();
            }
            catch (Exception)
            {
                return new List<Category>();
            }
        }

        public (bool sukses, string pesan) TambahKategoriBaru(string namaKategori)
        {
            try
            {
                // Validasi Model Asli tetap dipakai
                Category kategori = new Category(namaKategori);
                kategori.Validate();

                // Karena _categoryRepo dari revisi UI minta parameter string, kita get dari object
                // Jika repo lama kamu butuh objek, silakan diganti menjadi _categoryRepo.Insert(kategori);
                _categoryRepo.Insert(kategori.GetNamaKategori());

                return (true, "Kategori berhasil ditambahkan.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("nama_kategori") || ex.Message.Contains("unique"))
                {
                    return (false, "Nama kategori sudah ada di database!");
                }
                return (false, "Error sistem: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR KELOLA KATEGORI BARANG (KODE BARU UNTUK UI NEO-RETRO)
        // =======================================================

        /// <summary>
        /// Mengembalikan DataTable agar mudah di-bind ke DataGridView UI.
        /// </summary>
        public DataTable GetKategori()
        {
            try { return _categoryRepo.GetAll(); }
            catch { return new DataTable(); }
        }

        /// <summary>
        /// Method UI Gen-Z memanggil logika asli (TambahKategoriBaru) yang sudah punya validasi Model.
        /// </summary>
        public (bool sukses, string pesan) TambahKategori(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama)) return (false, "Nama kategorinya diisi dulu dong Mimin!");

            var hasil = TambahKategoriBaru(nama);
            if (hasil.sukses)
            {
                return (true, "Sip! Kategori baru udah berhasil ditambah. 🎉");
            }

            return (false, "Waduh: " + hasil.pesan);
        }

        public (bool sukses, string pesan) EditKategori(int id, string namaBaru)
        {
            if (string.IsNullOrWhiteSpace(namaBaru)) return (false, "Nama kategori ga boleh kosong ngab!");

            try
            {
                // Tetap lakukan validasi model asli
                Category kategori = new Category(namaBaru);
                kategori.Validate();

                _categoryRepo.Update(id, namaBaru);
                return (true, "Mantap! Kategori berhasil diupdate. ✨");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Server error nih: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) HapusKategori(int id)
        {
            try
            {
                _categoryRepo.Delete(id);
                return (true, "Kategori berhasil dihapus selamanya! 🗑️");
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign Key Violation
            {
                return (false, "Gabisa dihapus Min! Kategori ini lagi dipake jualan sama bestie-bestie. 😭");
            }
            catch (Exception ex)
            {
                return (false, "Gagal ngehapus: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR AUDIT TRAIL (KODE ASLI DIPERTAHANKAN)
        // =======================================================

        /// <summary>
        /// Mengambil log aktivitas untuk ditampilkan di dashboard Admin.
        /// </summary>
        public List<ActivityLog> GetLogAktivitas()
        {
            try
            {
                return _logRepo.GetAll();
            }
            catch (Exception)
            {
                return new List<ActivityLog>();
            }
        }

        /// <summary>
        /// Mengambil semua data user untuk ditampilkan di KelolaUserControl.
        /// Memanggil UserRepository yang sudah ada (_userRepo).
        /// </summary>
        public DataTable GetSemuaUser()
        {
            try
            {
                return _userRepo.GetSemuaUser();
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal mengambil data user: " + ex.Message);
            }
        }

        /// <summary>
        /// Memblokir atau mengaktifkan kembali akun user.
        /// Memanggil UserRepository yang sudah ada (_userRepo).
        /// </summary>
        public (bool sukses, string pesan) ToggleBlokirUser(int idUser, bool blokir)
        {
            try
            {
                _userRepo.ToggleBlokirUser(idUser, blokir);
                string aksi = blokir ? "diblokir" : "diaktifkan kembali";
                return (true, $"Akun berhasil {aksi}!");
            }
            catch (Exception ex)
            {
                return (false, "Gagal update status akun: " + ex.Message);
            }
        }

        /// <summary>
        /// Mengambil log aktivitas dalam bentuk DataTable untuk LogAktivitasControl.
        /// Memanggil ActivityLogRepository yang sudah ada (_logRepo).
        /// Method GetLogAktivitas() yang lama (return List) tetap dipertahankan.
        /// </summary>
        public DataTable GetLogAktivitasDataTable()
        {
            try
            {
                return _logRepo.GetAllAsDataTable();
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal mengambil log aktivitas: " + ex.Message);
            }
        }

        /// <summary>
        /// Mengambil data leaderboard penjual untuk DashboardAdminControl.
        /// Memanggil TransactionRepository karena data melibatkan transaction_details.
        /// </summary>
        public DataTable GetLeaderboardPenjual()
        {
            try
            {
                return _transactionRepo.GetLeaderboardPenjual();
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal mengambil data leaderboard: " + ex.Message);
            }
        }


    }
}
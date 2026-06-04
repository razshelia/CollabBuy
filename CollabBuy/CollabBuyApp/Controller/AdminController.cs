using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Configuration;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Controllers
{
    public class AdminController
    {
        private readonly CategoryRepository _categoryRepo;
        private readonly ActivityLogRepository _logRepo;
        private readonly UserRepository _userRepo;
        private readonly ComplaintRepository _complaintRepo;
        private readonly TransactionRepository _transactionRepo;

        public AdminController()
        {
            _categoryRepo = new CategoryRepository();
            _logRepo = new ActivityLogRepository();
            _userRepo = new UserRepository();
            _complaintRepo = new ComplaintRepository();
            _transactionRepo = new TransactionRepository();
        }


        // =======================================================
        // DASHBOARD
        // =======================================================

        /// <summary>
        /// Mengambil 4 statistik sekaligus dalam satu query agar efisien.
        /// </summary>
        public Dictionary<string, int> GetStatsDashboard()
        {
            var stats = new Dictionary<string, int> {
                { "users", 0 }, { "transaksi", 0 }, { "po_aktif", 0 }, { "aduan", 0 }
            };

            string connectionString = ConfigurationManager
                .ConnectionStrings["CollabBuyDb"]?.ConnectionString;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT
                            (SELECT COUNT(*) FROM users)                              AS users,
                            (SELECT COUNT(*) FROM transactions)                       AS transaksi,
                            (SELECT COUNT(*) FROM preorders    WHERE is_aktif = TRUE) AS po_aktif,
                            (SELECT COUNT(*) FROM complaints)                         AS aduan;";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats["users"] = Convert.ToInt32(reader["users"]);
                            stats["transaksi"] = Convert.ToInt32(reader["transaksi"]);
                            stats["po_aktif"] = Convert.ToInt32(reader["po_aktif"]);
                            stats["aduan"] = Convert.ToInt32(reader["aduan"]);
                        }
                    }
                }
            }
            catch { /* return nilai default agar UI tidak crash */ }

            return stats;
        }


        // =======================================================
        // KELOLA KATEGORI
        // =======================================================

        public DataTable GetKategori()
        {
            try { return _categoryRepo.GetAll(); }
            catch { return new DataTable(); }
        }

        public (bool sukses, string pesan) TambahKategori(string nama)
        {
            try
            {
                Category kategori = new Category(nama); // konstruktor sudah validasi
                _categoryRepo.Insert(kategori.NamaKategori);
                return (true, "Kategori baru berhasil ditambahkan!");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("nama_kategori") || ex.Message.Contains("unique"))
                    return (false, "Nama kategori sudah ada di database!");
                return (false, "Error sistem: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) EditKategori(int id, string namaBaru)
        {
            try
            {
                Category kategori = new Category(namaBaru); // konstruktor sudah validasi
                _categoryRepo.Update(id, kategori.NamaKategori);
                return (true, "Kategori berhasil diupdate.");
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

        public (bool sukses, string pesan) HapusKategori(int id)
        {
            try
            {
                _categoryRepo.Delete(id);
                return (true, "Kategori berhasil dihapus.");
            }
            catch (PostgresException ex) when (ex.SqlState == "23503")
            {
                return (false, "Kategori tidak bisa dihapus karena masih dipakai produk.");
            }
            catch (Exception ex)
            {
                return (false, "Gagal menghapus: " + ex.Message);
            }
        }


        // =======================================================
        // KELOLA USER
        // =======================================================

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

        public (bool sukses, string pesan) ToggleBlokirUser(int idUser, bool blokir)
        {
            try
            {
                _userRepo.ToggleBlokirUser(idUser, blokir);
                string aksi = blokir ? "diblokir" : "diaktifkan kembali";
                return (true, $"Akun berhasil {aksi}.");
            }
            catch (Exception ex)
            {
                return (false, "Gagal update status akun: " + ex.Message);
            }
        }


        // =======================================================
        // LOG AKTIVITAS
        // =======================================================

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

        public List<ActivityLog> GetAllActivityLogs()
        {
            try
            {
                return _logRepo.GetAll(); // pakai _logRepo yang sudah ada, bukan new lagi
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ambil log: " + ex.Message);
                return new List<ActivityLog>();
            }
        }


        // =======================================================
        // LAPORAN / LEADERBOARD
        // =======================================================

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
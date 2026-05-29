using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller khusus untuk fitur-fitur Admin (Master Data & Audit Trail).
    /// </summary>
    public class AdminController
    {
        // === PRIVATE FIELDS ===
        private readonly CategoryRepository _categoryRepo;
        private readonly ActivityLogRepository _logRepo;

        // Tambahan Repository untuk Kebutuhan Dashboard
        private readonly UserRepository _userRepo;
        private readonly ComplaintRepository _complaintRepo;

        // === KONSTRUKTOR ===
        public AdminController()
        {
            _categoryRepo = new CategoryRepository();
            _logRepo = new ActivityLogRepository();
            _userRepo = new UserRepository();
            _complaintRepo = new ComplaintRepository();
        }

        // =======================================================
        // FITUR DASHBOARD STATISTIK (TAMBAHAN BARU)
        // =======================================================

        public int GetTotalUsersCount()
        {
            try
            {
                // Menghitung total semua user yang terdaftar
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
                // Asumsi: Menghitung user dengan Role Penjual yang belum diverifikasi
                // (Sesuaikan properti 'Role' dan 'IsVerified' dengan struktur Model User Anda)
                // Contoh LINQ: return _userRepo.GetAll().Count(u => u.Role == "Penjual" && !u.IsVerified);
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
                // Asumsi: Menghitung aduan (complaint) yang statusnya belum Selesai/Resolved
                // Contoh LINQ: return _complaintRepo.GetAll().Count(c => c.Status == "Pending");
                var complaints = _complaintRepo.GetAll();
                return complaints != null ? complaints.Count : 0; // Sementara menghitung semua aduan
            }
            catch
            {
                return 0;
            }
        }

        // =======================================================
        // FITUR MASTER KATEGORI (KODE ASLI)
        // =======================================================

        public List<Category> GetAllKategori()
        {
            try
            {
                return _categoryRepo.GetAll();
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
                Category kategori = new Category(namaKategori);
                kategori.Validate();

                _categoryRepo.Insert(kategori);

                return (true, "Kategori berhasil ditambahkan.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("nama_kategori"))
                {
                    return (false, "Nama kategori sudah ada di database!");
                }
                return (false, "Error sistem: " + ex.Message);
            }
        }

        // =======================================================
        // FITUR AUDIT TRAIL (LOG AKTIVITAS)
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
    }
}
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;

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

        // === KONSTRUKTOR ===
        public AdminController()
        {
            _categoryRepo = new CategoryRepository();
            _logRepo = new ActivityLogRepository();
        }


        // =======================================================
        // FITUR MASTER KATEGORI
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
                // Tangkap error Unique Constraint dari DB
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
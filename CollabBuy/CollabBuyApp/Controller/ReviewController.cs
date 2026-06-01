using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data; // Wajib ditambahkan untuk DataTable UI

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller untuk mengelola alur Ulasan/Review Produk.
    /// Sudah disesuaikan untuk UI Gen-Z tanpa membuang logic asli.
    /// </summary>
    public class ReviewController
    {
        // === PRIVATE FIELDS ===
        private readonly ReviewRepository _reviewRepo;
        private readonly ActivityLogRepository _logRepo;

        // === KONSTRUKTOR ===
        public ReviewController()
        {
            _reviewRepo = new ReviewRepository();
            _logRepo = new ActivityLogRepository();
        }

        // =======================================================
        // FITUR PEMBELI (NEO-RETRO UI)
        // =======================================================

        /// <summary>
        /// Mengambil daftar produk yang statusnya selesai dan bisa di-review.
        /// </summary>
        public DataTable GetListProdukBuatDiulas(int idUser)
        {
            try { return _reviewRepo.GetProdukBisaDiulas(idUser); }
            catch (Exception) { return new DataTable(); }
        }

        /// <summary>
        /// Mengirim ulasan produk baru dari pembeli (Versi Upgrade dari KirimUlasan).
        /// Mempertahankan validasi model.
        /// </summary>
        public (bool sukses, string pesan) GasNgasihRating(int idProduk, int idUser, int rating, string komentar)
        {
            if (idProduk <= 0)
                return (false, "Pilih dulu barang yang mau di-review dong.");

            try
            {
                // 1. Validasi Model Asli
                Review review = new Review(idProduk, idUser, rating, komentar);
                review.Validate();

                // 2. Simpan ke DB
                _reviewRepo.Insert(review);

                return (true, "Makasih banyak review-nya bestie! ⭐");
            }
            catch (InvalidOrderException ex)
            {
                return (false, "Waduh: " + ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Error sistem: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR PENJUAL (NEO-RETRO UI)
        // =======================================================

        /// <summary>
        /// Mengambil list review khusus untuk lapak penjual tertentu.
        /// </summary>
        public DataTable GetReviewLapak(int idPenjual)
        {
            try { return _reviewRepo.GetReviewsByPenjual(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }

        /// <summary>
        /// Penjual membalas ulasan dari pembeli (Versi Upgrade dari BalasUlasan).
        /// </summary>
        public (bool sukses, string pesan) BalasUlasanLapak(int idUlasan, string balasanPenjual, int idPenjual)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(balasanPenjual))
                    return (false, "Balasan ke customer ga boleh kosong ngab!");

                Review review = _reviewRepo.GetById(idUlasan);
                if (review == null)
                    return (false, "Ulasannya ga ketemu nih.");

                // Method IResolvable
                review.BeriTanggapan(balasanPenjual);
                _reviewRepo.Update(review);

                // Tetap menggunakan Activity Log
                ActivityLog log = new ActivityLog(idPenjual, "Membalas ulasan ID: " + idUlasan);
                _logRepo.Insert(log);

                return (true, "Sip, balasan udah terkirim ke customer! 🚀");
            }
            catch (InvalidOrderException ex)
            {
                return (false, "Waduh: " + ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Error sistem: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR ADMIN (KODE ASLI DIPERTAHANKAN)
        // =======================================================

        /// <summary>
        /// Mengambil semua ulasan (biasanya difilter by idProduk di UI DataGridView untuk Admin).
        /// </summary>
        public List<Review> GetAllUlasan()
        {
            try
            {
                return _reviewRepo.GetAll();
            }
            catch (Exception)
            {
                return new List<Review>();
            }
        }
    }
}
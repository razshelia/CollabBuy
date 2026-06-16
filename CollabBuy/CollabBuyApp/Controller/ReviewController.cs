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
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewController.GetListProdukBuatDiulas] Error: {ex.Message}");
                return new DataTable();
            }

        }

        /// <summary>
        /// Mengirim ulasan produk baru dari pembeli (Versi Upgrade dari KirimUlasan).
        /// Mempertahankan validasi model.
        /// </summary>
        // =======================================================
        // TIMPA METHOD GasNgasihRating
        // =======================================================
        public (bool sukses, string pesan) GasNgasihRating(int idProduk, int idUser, int rating, string komentar)
        {
            try
            {
                DataTable produkBisaDiulas = _reviewRepo.GetProdukBisaDiulas(idUser);
                bool pernahBeli = false;

                if (produkBisaDiulas != null)
                {
                    foreach (DataRow row in produkBisaDiulas.Rows)
                    {
                        if (Convert.ToInt32(row["id_produk"]) == idProduk)
                        {
                            pernahBeli = true;
                            break;
                        }
                    }
                }

                if (!pernahBeli)
                    throw new InvalidOrderException("Kamu belum pernah beli produk ini atau pesanan belum selesai. Hanya pembeli yang sudah selesai transaksi yang bisa review.", "id_produk", "REVIEW_DITOLAK");

                // Instansiasi Model: Validasi rating & komentar bekerja otomatis
                Review review = new Review(idProduk, idUser, rating, komentar);
                review.Validate();

                _reviewRepo.Insert(review);
                return (true, "Makasih banyak review-nya bestie! ⭐");
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

        // =======================================================
        // TIMPA METHOD BalasUlasanLapak
        // =======================================================
        public (bool sukses, string pesan) BalasUlasanLapak(int idUlasan, string balasanPenjual, int idPenjual)
        {
            try
            {
                Review review = _reviewRepo.GetById(idUlasan);
                if (review == null)
                    throw new InvalidOrderException("Ulasannya ga ketemu nih.", "id_ulasan", "REVIEW_NOT_FOUND");

                // Model memvalidasi panjang balasan
                review.BeriTanggapan(balasanPenjual);
                _reviewRepo.Update(review);

                ActivityLog log = new ActivityLog(idPenjual, "Membalas ulasan ID: " + idUlasan);
                _logRepo.Insert(log);

                return (true, "Sip, balasan udah terkirim ke customer! 🚀");
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


        // =======================================================
        // FITUR PENJUAL (NEO-RETRO UI)
        // =======================================================

        /// <summary>
        /// Mengambil list review khusus untuk lapak penjual tertentu.
        /// </summary>
        public DataTable GetReviewLapak(int idPenjual)
        {
            try { return _reviewRepo.GetReviewsByPenjual(idPenjual); }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewController.GetReviewLapak] Error: {ex.Message}");
                return new DataTable();
            }

        }
    }
}
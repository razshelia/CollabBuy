using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller untuk mengelola alur Ulasan/Review Produk.
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
        // FITUR PEMBELI
        // =======================================================

        /// <summary>
        /// Mengirim ulasan produk baru dari pembeli.
        /// </summary>
        public (bool sukses, string pesan) KirimUlasan(int idProduk, int idUser, int rating, string komentar)
        {
            try
            {
                Review review = new Review(idProduk, idUser, rating, komentar);
                review.Validate();

                _reviewRepo.Insert(review);

                return (true, "Ulasan berhasil dikirim!");
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
        // FITUR PENJUAL
        // =======================================================

        /// <summary>
        /// Penjual membalas ulasan dari pembeli.
        /// Memanfaatkan method BeriTanggapan() dari Interface IResolvable.
        /// </summary>
        public (bool sukses, string pesan) BalasUlasan(int idUlasan, string balasanPenjual, int idPenjual)
        {
            try
            {
                if (string.IsNullOrEmpty(balasanPenjual))
                {
                    return (false, "Balasan penjual tidak boleh kosong!");
                }

                Review review = _reviewRepo.GetById(idUlasan);
                if (review == null)
                {
                    return (false, "Ulasan tidak ditemukan!");
                }

                // Method IResolvable
                review.BeriTanggapan(balasanPenjual);

                _reviewRepo.Update(review);

                ActivityLog log = new ActivityLog(idPenjual, "Membalas ulasan ID: " + idUlasan);
                _logRepo.Insert(log);

                return (true, "Balasan berhasil dikirim.");
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
        // FITUR LIHAT ULASAN
        // =======================================================

        /// <summary>
        /// Mengambil semua ulasan (biasanya difilter by idProduk di UI DataGridView).
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
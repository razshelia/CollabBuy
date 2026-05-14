using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewService()
        {
            _reviewRepo = new ReviewRepository();
        }

        // ── 1. Tambah ulasan (oleh pembeli) ──
        public bool TambahUlasan(int idProduk, int idUser, int rating, string komentar)
        {
            // Validasi rating
            if (rating < 1 || rating > 5)
            {
                UXHelper.TampilkanError("Rating harus antara 1 sampai 5.");
                return false;
            }

            Review ulasan = new Review();
            ulasan.IdProduk = idProduk;
            ulasan.IdUser = idUser;
            ulasan.Rating = rating;
            ulasan.Komentar = komentar; // boleh null

            bool sukses = _reviewRepo.TambahUlasan(ulasan);
            if (sukses)
                UXHelper.TampilkanSukses("Ulasan berhasil ditambahkan. Terima kasih!");
            return sukses;
        }

        // ── 2. Ambil ulasan berdasarkan produk (untuk detail produk) ──
        public List<Review> AmbilUlasanProduk(int idProduk)
        {
            return _reviewRepo.AmbilUlasanByProduk(idProduk);
        }

        // ── 3. Ambil ulasan untuk penjual (semua produk miliknya) ──
        public List<Review> AmbilUlasanPenjual(int idPenjual)
        {
            return _reviewRepo.AmbilUlasanByPenjual(idPenjual);
        }

        // ── 4. Balas ulasan (oleh penjual) ──
        public bool BalasUlasan(int idUlasan, string balasan)
        {
            if (string.IsNullOrWhiteSpace(balasan))
            {
                UXHelper.TampilkanError("Balasan tidak boleh kosong.");
                return false;
            }

            bool sukses = _reviewRepo.BalasUlasan(idUlasan, balasan);
            if (sukses)
                UXHelper.TampilkanSukses("Balasan berhasil dikirim.");
            return sukses;
        }
    }
}
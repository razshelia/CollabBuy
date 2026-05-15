using System;
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

        public bool TambahUlasan(int idProduk, int idUser, int rating, string komentar)
        {
            if (rating < 1 || rating > 5)
            {
                UXHelper.TampilkanError("Rating harus antara 1 sampai 5.");
                return false;
            }

            Review ulasan = new Review();
            ulasan.IdProduk = idProduk;
            ulasan.IdUser = idUser;
            ulasan.Rating = rating;
            ulasan.Komentar = komentar;

            try
            {
                bool sukses = _reviewRepo.TambahUlasan(ulasan);
                if (sukses) UXHelper.TampilkanSukses("Ulasan berhasil ditambahkan. Terima kasih!");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<Review> AmbilUlasanProduk(int idProduk)
        {
            try { return _reviewRepo.AmbilUlasanByProduk(idProduk); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Review>(); }
        }

        public List<Review> AmbilUlasanPenjual(int idPenjual)
        {
            try { return _reviewRepo.AmbilUlasanByPenjual(idPenjual); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Review>(); }
        }

        public bool BalasUlasan(int idUlasan, string balasan)
        {
            if (string.IsNullOrWhiteSpace(balasan))
            {
                UXHelper.TampilkanError("Balasan tidak boleh kosong.");
                return false;
            }

            try
            {
                bool sukses = _reviewRepo.BalasUlasan(idUlasan, balasan);
                if (sukses) UXHelper.TampilkanSukses("Balasan berhasil dikirim.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }
    }
}
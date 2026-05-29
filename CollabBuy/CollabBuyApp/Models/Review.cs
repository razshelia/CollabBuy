using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Ulasan Produk.
    /// Mengimplementasikan IValidatable dan IResolvable.
    /// 
    /// Pemetaan Database:
    /// - Tabel: reviews
    /// - Kolom: rating (CHECK >=1 AND <=5), balasan_penjual
    /// </summary>
    public class Review : IValidatable, IResolvable
    {
        // === PRIVATE FIELDS ===
        private int _idUlasan;
        private int _idProduk;
        private int _idUser;
        private int _rating;
        private string _komentar;
        private DateTime _tanggalUlasan;
        private string _balasanPenjual;

        // === KONSTRUKTOR ===
        public Review(int idProduk, int idUser, int rating, string komentar)
        {
            _idProduk = idProduk;
            _idUser = idUser;
            SetRating(rating);
            SetKomentar(komentar);
            _tanggalUlasan = DateTime.Now;
            _balasanPenjual = "";
        }

        // === GETTER & SETTER ===
        public int GetIdUlasan() { return _idUlasan; }
        public void SetIdUlasan(int id) { _idUlasan = id; }

        public int GetIdProduk() { return _idProduk; }
        public int GetIdUser() { return _idUser; }

        public int GetRating() { return _rating; }
        public void SetRating(int rating)
        {
            // Pemetaan CHECK Constraint Database: rating >= 1 AND rating <= 5
            if (rating < 1 || rating > 5)
            {
                throw new InvalidOrderException("Rating harus di antara 1 sampai 5!", "rating", "RATING_INVALID");
            }
            _rating = rating;
        }

        public string GetKomentar() { return _komentar; }
        public void SetKomentar(string komentar)
        {
            // Boleh kosong (NULL di DB), tapi jika diisi harus lebih dari 0 karakter
            if (komentar != null && komentar.Length == 0)
            {
                throw new InvalidOrderException("Komentar tidak boleh string kosong!", "komentar", "KOMENTAR_INVALID");
            }
            _komentar = komentar;
        }

        public DateTime GetTanggalUlasan() { return _tanggalUlasan; }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (_rating < 1 || _rating > 5)
            {
                throw new InvalidOrderException("Review tidak valid: Rating di luar jangkauan.", "rating", "REVIEW_INVALID");
            }
        }

        // === IMPLEMENTASI IResolvable ===
        /// <summary>
        /// Penjual memberikan balasan terhadap review pembeli.
        /// Pemetaan DB: reviews.balasan_penjual
        /// </summary>
        public void BeriTanggapan(string tanggapan)
        {
            if (string.IsNullOrEmpty(tanggapan))
            {
                throw new InvalidOrderException("Balasan penjual tidak boleh kosong!", "balasan_penjual", "REVIEW_BALAS_KOSONG");
            }
            _balasanPenjual = tanggapan;
        }

        public bool IsSelesai()
        {
            // Review dianggap "selesai/resolusi" jika sudah dibalas penjual
            return !string.IsNullOrEmpty(_balasanPenjual);
        }

        public string GetTanggapan()
        {
            return _balasanPenjual;
        }
    }
}
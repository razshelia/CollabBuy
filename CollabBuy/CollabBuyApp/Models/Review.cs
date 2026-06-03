using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Ulasan Produk.
    /// Mengimplementasikan IValidatable dan IResolvable.
    /// Dilengkapi dengan formatting UI otomatis untuk Bintang dan Balasan.
    /// </summary>
    public class Review : IValidatable, IResolvable
    {
        // === PRIVATE FIELDS (Strict Encapsulation) ===
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
            this._idProduk = idProduk;
            this._idUser = idUser;

            this.SetRating(rating);
            this.SetKomentar(komentar);

            this._tanggalUlasan = DateTime.Now;
            this._balasanPenjual = "";
        }

        // === GETTER & SETTER DENGAN ENKAPSULASI KETAT (IF-ELSE) ===
        public int GetIdUlasan()
        {
            return this._idUlasan;
        }

        public void SetIdUlasan(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Ulasan tidak valid!", "id_ulasan", "ULASAN_ID_INVALID");
            }
            else
            {
                this._idUlasan = id;
            }
        }

        public int GetIdProduk()
        {
            return this._idProduk;
        }

        public int GetIdUser()
        {
            return this._idUser;
        }

        public int GetRating()
        {
            return this._rating;
        }

        public void SetRating(int rating)
        {
            // Pemetaan CHECK Constraint Database: rating >= 1 AND rating <= 5
            if (rating < 1 || rating > 5)
            {
                throw new InvalidOrderException("Rating harus di antara 1 sampai 5!", "rating", "RATING_INVALID");
            }
            else
            {
                this._rating = rating;
            }
        }

        public string GetKomentar()
        {
            return this._komentar;
        }

        public void SetKomentar(string komentar)
        {
            if (string.IsNullOrWhiteSpace(komentar))
            {
                this._komentar = "Tidak ada komentar tertulis.";
            }
            else if (komentar.Trim().Length < 10)
            {
                throw new InvalidOrderException("Komentar ulasan minimal 10 karakter kalau mau diisi ya!", "komentar", "REVIEW_KOMENTAR_PENDEK");
            }
            else if (komentar.Trim().Length > 500)
            {
                throw new InvalidOrderException("Komentar ulasan maksimal 500 karakter!", "komentar", "REVIEW_KOMENTAR_PANJANG");
            }
            else
            {
                this._komentar = komentar.Trim();
            }
        }

        public DateTime GetTanggalUlasan()
        {
            return this._tanggalUlasan;
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        /// <summary>
        /// Mengubah angka rating (misal: 4) menjadi string visual (⭐⭐⭐⭐).
        /// </summary>
        public string DapatkanBintangUI()
        {
            string bintangVisual;

            if (this._rating == 5)
            {
                bintangVisual = "⭐⭐⭐⭐⭐";
            }
            else if (this._rating == 4)
            {
                bintangVisual = "⭐⭐⭐⭐";
            }
            else if (this._rating == 3)
            {
                bintangVisual = "⭐⭐⭐";
            }
            else if (this._rating == 2)
            {
                bintangVisual = "⭐⭐";
            }
            else if (this._rating == 1)
            {
                bintangVisual = "⭐";
            }
            else
            {
                bintangVisual = "Belum Ada Rating";
            }

            return bintangVisual;
        }

        /// <summary>
        /// Memotong komentar ulasan agar rapi saat ditampilkan di Card/Grid UI.
        /// </summary>
        public string DapatkanPreviewKomentar(int batasKarakter)
        {
            string preview;

            if (this._komentar == "Tidak ada komentar tertulis.")
            {
                preview = this._komentar;
            }
            else if (this._komentar.Length <= batasKarakter)
            {
                preview = this._komentar;
            }
            else
            {
                preview = this._komentar.Substring(0, batasKarakter) + "...";
            }

            return preview;
        }

        public string DapatkanStatusBalasan()
        {
            string statusUi;

            if (this.IsSelesai())
            {
                statusUi = "✅ Telah Dibalas Penjual";
            }
            else
            {
                statusUi = "⏳ Menunggu Balasan";
            }

            return statusUi;
        }

        public string DapatkanWaktuFormatUI()
        {
            string waktuFormat;

            if (this._tanggalUlasan != DateTime.MinValue)
            {
                waktuFormat = this._tanggalUlasan.ToString("dd MMM yyyy, HH:mm");
            }
            else
            {
                waktuFormat = "Tanggal tidak diketahui";
            }

            return waktuFormat;
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            bool validasiUlasanSelesai;

            if (this._rating < 1 || this._rating > 5)
            {
                throw new InvalidOrderException("Review tidak valid: Rating di luar jangkauan.", "rating", "REVIEW_INVALID");
            }
            else
            {
                validasiUlasanSelesai = true; // Penugasan nyata agar else tidak kosong
            }
        }

        // === IMPLEMENTASI IResolvable ===

        public void BeriTanggapan(string tanggapan)
        {
            if (string.IsNullOrWhiteSpace(tanggapan))
            {
                throw new InvalidOrderException("Balasan penjual tidak boleh kosong!", "balasan_penjual", "REVIEW_BALAS_KOSONG");
            }
            else if (tanggapan.Trim().Length < 5)
            {
                throw new InvalidOrderException("Balasan penjual minimal 5 karakter!", "balasan_penjual", "REVIEW_BALAS_PENDEK");
            }
            else if (tanggapan.Trim().Length > 500)
            {
                throw new InvalidOrderException("Balasan penjual maksimal 500 karakter!", "balasan_penjual", "REVIEW_BALAS_PANJANG");
            }
            else
            {
                this._balasanPenjual = tanggapan.Trim();
            }
        }

        public bool IsSelesai()
        {
            bool sudahDibalas;

            // Review dianggap "selesai/resolusi" jika sudah dibalas penjual
            if (string.IsNullOrWhiteSpace(this._balasanPenjual))
            {
                sudahDibalas = false;
            }
            else
            {
                sudahDibalas = true;
            }

            return sudahDibalas;
        }

        public string GetTanggapan()
        {
            return this._balasanPenjual;
        }
    }
}
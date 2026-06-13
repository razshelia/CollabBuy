using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Ulasan Produk.
    /// Mengimplementasikan IValidatable dan IResolvable.
    /// Dilengkapi dengan formatting UI otomatis untuk Bintang dan Balasan.
    /// </summary>
    public class Review : IValidatable, IResolvable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idProduk;
        private int _idUser;
        private DateTime _tanggalUlasan;
        private int _idUlasan;
        private int _rating;
        private string _komentar;
        private string _balasanPenjual;

        // === PROPERTIES ===

        // Auto-Properties (Read-only dari luar)
        public int IdProduk
        {
            get { return this._idProduk; }
            private set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Produk pada ulasan tidak valid!", "id_produk", "ULASAN_IDPRODUK_INVALID");
                this._idProduk = value;
            }
        }

        public int IdUser
        {
            get { return this._idUser; }
            private set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID User pada ulasan tidak valid!", "id_user", "ULASAN_IDUSER_INVALID");
                this._idUser = value;
            }
        }
        public DateTime TanggalUlasan
        {
            get { return this._tanggalUlasan; }
            set
            {
                // Guard clause 1: Mencegah penempatan tanggal default/kosong
                if (value == DateTime.MinValue)
                    throw new InvalidOrderException("Tanggal ulasan tidak boleh kosong!", "tanggal_ulasan", "ULASAN_DATE_EMPTY");

                // Guard clause 2: Mencegah manipulasi waktu ulasan dari masa depan
                if (value > DateTime.Now)
                    throw new InvalidOrderException("Tanggal ulasan tidak boleh mendahului waktu saat ini!", "tanggal_ulasan", "ULASAN_DATE_FUTURE");

                this._tanggalUlasan = value;
            }
        }

        public int IdUlasan
        {
            get { return this._idUlasan; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Ulasan tidak valid!", "id_ulasan", "ULASAN_ID_INVALID");
                this._idUlasan = value;
            }
        }

        public int Rating
        {
            get { return this._rating; }
            set
            {
                // Pemetaan CHECK Constraint Database: rating >= 1 AND rating <= 5
                if (value < 1 || value > 5)
                    throw new InvalidOrderException("Rating harus di antara 1 sampai 5!", "rating", "RATING_INVALID");
                this._rating = value;
            }
        }

        public string Komentar
        {
            get { return this._komentar; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._komentar = "Tidak ada komentar tertulis.";
                    return; // Early return pengganti else
                }

                if (value.Trim().Length < 10)
                    throw new InvalidOrderException("Komentar ulasan minimal 10 karakter kalau mau diisi ya!", "komentar", "REVIEW_KOMENTAR_PENDEK");

                if (value.Trim().Length > 500)
                    throw new InvalidOrderException("Komentar ulasan maksimal 500 karakter!", "komentar", "REVIEW_KOMENTAR_PANJANG");

                this._komentar = value.Trim();
            }
        }

        // === KONSTRUKTOR ===
        public Review(int idProduk, int idUser, int rating, string komentar)
        {
            this.IdProduk = idProduk;
            this.IdUser = idUser;
            this.Rating = rating;
            this.Komentar = komentar;
            this.TanggalUlasan = DateTime.Now;
            this._balasanPenjual = "";
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        /// <summary>
        /// Mengubah angka rating (misal: 4) menjadi string visual (⭐⭐⭐⭐).
        /// </summary>
        public string DapatkanBintangUI()
        {
            // Trik C# untuk mencetak karakter berulang tanpa looping atau if-else panjang!
            if (this.Rating >= 1 && this.Rating <= 5)
            {
                return new string('⭐', this.Rating);
            }

            return "Belum Ada Rating";
        }

        /// <summary>
        /// Memotong komentar ulasan agar rapi saat ditampilkan di Card/Grid UI.
        /// </summary>
        public string DapatkanPreviewKomentar(int batasKarakter)
        {
            if (this.Komentar == "Tidak ada komentar tertulis." || this.Komentar.Length <= batasKarakter)
            {
                return this.Komentar;
            }

            return this.Komentar.Substring(0, batasKarakter) + "...";
        }

        public string DapatkanStatusBalasan()
        {
            // Menggunakan Ternary Operator (? :)
            return this.IsSelesai() ? "✅ Telah Dibalas Penjual" : "⏳ Menunggu Balasan";
        }

        public string DapatkanWaktuFormatUI()
        {
            return this.TanggalUlasan != DateTime.MinValue
                ? this.TanggalUlasan.ToString("dd MMM yyyy, HH:mm")
                : "Tanggal tidak diketahui";
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (this._rating < 1 || this._rating > 5)
                throw new InvalidOrderException("Review tidak valid: Rating di luar jangkauan.", "rating", "REVIEW_INVALID");

            // Variabel dummy validasiUlasanSelesai dihapus karena tidak berguna
        }

        // === IMPLEMENTASI IResolvable ===
        public void BeriTanggapan(string tanggapan)
        {
            if (string.IsNullOrWhiteSpace(tanggapan))
                throw new InvalidOrderException("Balasan penjual tidak boleh kosong!", "balasan_penjual", "REVIEW_BALAS_KOSONG");

            if (tanggapan.Trim().Length < 5)
                throw new InvalidOrderException("Balasan penjual minimal 5 karakter!", "balasan_penjual", "REVIEW_BALAS_PENDEK");

            if (tanggapan.Trim().Length > 500)
                throw new InvalidOrderException("Balasan penjual maksimal 500 karakter!", "balasan_penjual", "REVIEW_BALAS_PANJANG");

            this._balasanPenjual = tanggapan.Trim();
        }

        public bool IsSelesai()
        {
            // Cukup 1 baris: Mengembalikan True jika balasan_penjual TIDAK kosong
            return !string.IsNullOrWhiteSpace(this._balasanPenjual);
        }

        // Tetap menggunakan method karena merupakan kontrak dari interface IResolvable
        public string GetTanggapan()
        {
            return this._balasanPenjual;
        }
    }
}
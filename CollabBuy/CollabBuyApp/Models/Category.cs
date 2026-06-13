using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;
using System.Globalization;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Kategori Produk.
    /// Mengimplementasikan IValidatable.
    /// </summary>
    public class Category : IValidatable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idKategori;
        private string _namaKategori;

        // === PROPERTIES (Get & Set dalam satu blok) ===
        public int IdKategori
        {
            get { return this._idKategori; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOrderException("ID Kategori tidak valid!", "id_kategori", "KAT_ID_INVALID");
                }
                this._idKategori = value;
            }
        }

        public string NamaKategori
        {
            get { return this._namaKategori; }
            set
            {
                // Guard Clauses murni tanpa else if / else
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Nama kategori tidak boleh kosong!", "nama_kategori", "KATEGORI_KOSONG");
                }

                if (value.Trim().Length < 4)
                {
                    throw new InvalidOrderException("Nama kategori minimal 4 karakter!", "nama_kategori", "KATEGORI_TERLALU_PENDEK");
                }

                if (value.Trim().Length > 50)
                {
                    throw new InvalidOrderException("Nama kategori maksimal 50 karakter!", "nama_kategori", "KATEGORI_TERLALU_PANJANG");
                }

                this._namaKategori = value.Trim();
            }
        }

        // === KONSTRUKTOR ===
        public Category(string namaKategori)
        {
            // Memanggil setter dari Properti
            this.NamaKategori = namaKategori;

            // Otomatis merapikan nama saat objek dibuat
            this.RapikanNamaKategori();
        }

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR (OOP BEST PRACTICE)
        // =========================================================

        // Method 1: Validasi
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this._namaKategori))
            {
                throw new InvalidOrderException("Validasi gagal: Kategori tidak punya nama.", "nama_kategori", "KATEGORI_INVALID");
            }
        }

        // Method 2: Pembersihan Data (Data Cleansing)
        /// <summary>
        /// Menghapus spasi berlebih dan membuat format teks menjadi Title Case (Huruf depan besar).
        /// Contoh: "   makanan   ringan " -> "Makanan Ringan"
        /// </summary>
        public void RapikanNamaKategori()
        {
            // Early return jika kosong, langsung keluar dari method
            if (string.IsNullOrWhiteSpace(this._namaKategori)) return;

            // Hapus spasi depan belakang dan ubah ke Title Case
            string teksBersih = this._namaKategori.Trim();
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            this._namaKategori = textInfo.ToTitleCase(teksBersih.ToLower());
        }

        // Method 3: Formatting UI
        /// <summary>
        /// Mengembalikan teks yang siap dimasukkan ke ComboBox/Dropdown UI.
        /// </summary>
        public string DapatkanFormatDropdown()
        {
            return $"[{this._idKategori}] - {this._namaKategori}";
        }

        // Method 4: Logika Pencarian
        /// <summary>
        /// Mengecek apakah kategori ini cocok dengan kata kunci pencarian dari user.
        /// Tidak sensitif terhadap huruf besar/kecil.
        /// </summary>
        public bool PencarianCocok(string keyword)
        {
            // Early return untuk optimasi
            if (string.IsNullOrWhiteSpace(keyword)) return true;

            return this._namaKategori.ToLower().Contains(keyword.ToLower());
        }
    }
}
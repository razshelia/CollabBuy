using CollabBuy.CollabBuyApp.Models;
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
        // === PRIVATE FIELDS ===
        private int _idKategori;
        private string _namaKategori;

        // === KONSTRUKTOR ===
        public Category(string namaKategori)
        {
            this.SetNamaKategori(namaKategori);

            // Otomatis merapikan nama saat objek dibuat
            this.RapikanNamaKategori();
        }

        // === GETTER & SETTER DENGAN GUARD CLAUSES ===
        public int GetIdKategori()
        {
            return this._idKategori;
        }

        public void SetIdKategori(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Kategori tidak valid!", "id_kategori", "KAT_ID_INVALID");
            }
            this._idKategori = id;
        }

        public string GetNamaKategori()
        {
            return this._namaKategori;
        }

        public void SetNamaKategori(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                throw new InvalidOrderException("Nama kategori tidak boleh kosong!", "nama_kategori", "KATEGORI_KOSONG");
            }
            this._namaKategori = nama;
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
            if (string.IsNullOrWhiteSpace(keyword)) return true; // Kalau kolom search kosong, anggap cocok

            return this._namaKategori.ToLower().Contains(keyword.ToLower());
        }
    }
}
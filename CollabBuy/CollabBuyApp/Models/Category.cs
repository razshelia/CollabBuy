using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Kategori Produk.
    /// Mengimplementasikan IValidatable.
    /// 
    /// Pemetaan Database:
    /// - Tabel: categories
    /// - Relasi: Akan digunakan sebagai referensi di Product
    /// </summary>
    public class Category : IValidatable
    {
        // === PRIVATE FIELDS ===
        private int _idKategori;
        private string _namaKategori;

        // === KONSTRUKTOR ===
        public Category(string namaKategori)
        {
            SetNamaKategori(namaKategori);
        }

        // === GETTER & SETTER DENGAN VALIDASI ===
        public int GetIdKategori() { return _idKategori; }
        public void SetIdKategori(int id) { _idKategori = id; }

        public string GetNamaKategori() { return _namaKategori; }
        public void SetNamaKategori(string nama)
        {
            if (string.IsNullOrEmpty(nama))
            {
                throw new InvalidOrderException("Nama kategori tidak boleh kosong!", "nama_kategori", "KATEGORI_KOSONG");
            }
            _namaKategori = nama;
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (string.IsNullOrEmpty(_namaKategori))
            {
                throw new InvalidOrderException("Validasi gagal: Kategori tidak punya nama.", "nama_kategori", "KATEGORI_INVALID");
            }
        }
    }
}
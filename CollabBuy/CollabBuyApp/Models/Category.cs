using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Category
    {
        private int _idKategori;
        private string _namaKategori;
        private string _deskripsi;

        public int IdKategori
        {
            get => _idKategori;
            set { if (value < 0) _idKategori = 0; else _idKategori = value; }
        }

        public string NamaKategori
        {
            get => _namaKategori;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nama kategori wajib diisi."); _namaKategori = value.Trim(); }
        }

        public string Deskripsi
        {
            get => _deskripsi;
            set => _deskripsi = string.IsNullOrWhiteSpace(value) ? "Tidak ada deskripsi untuk kategori ini." : value.Trim();
        }
    }
}
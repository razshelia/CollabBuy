using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Review
    {
        private int _idUlasan;
        private int _idProduk;
        private int _idUser;
        private int _rating;
        private string _komentar;
        private DateTime _tanggalUlasan;
        private string _balasanPenjual;

        public Review() { _tanggalUlasan = DateTime.Now; }

        public int IdUlasan
        {
            get => _idUlasan;
            set { if (value <= 0) throw new ArgumentException("ID Ulasan tidak valid."); _idUlasan = value; }
        }

        public int IdProduk
        {
            get => _idProduk;
            set { if (value <= 0) throw new ArgumentException("ID Produk tidak valid."); _idProduk = value; }
        }

        public int IdUser
        {
            get => _idUser;
            set { if (value <= 0) throw new ArgumentException("ID User tidak valid."); _idUser = value; }
        }

        public int Rating
        {
            get => _rating;
            set { if (value < 1 || value > 5) throw new ArgumentException("Rating harus 1-5."); _rating = value; }
        }

        public string Komentar
        {
            get => _komentar;
            set => _komentar = string.IsNullOrWhiteSpace(value) ? "User tidak memberikan komentar." : value.Trim();
        }

        public DateTime TanggalUlasan
        {
            get => _tanggalUlasan;
            set { if (value.Year < 2026) throw new ArgumentException("Tanggal tidak valid."); _tanggalUlasan = value; }
        }

        public string BalasanPenjual
        {
            get => _balasanPenjual;
            set => _balasanPenjual = string.IsNullOrWhiteSpace(value) ? "Belum dibalas oleh penjual." : value.Trim();
        }
    }
}
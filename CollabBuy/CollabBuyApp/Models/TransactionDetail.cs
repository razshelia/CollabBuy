using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class TransactionDetail
    {
        private int _idDetail;
        private int _idTransaksi;
        private int _idProduk;
        private string _namaPenitip;
        private int _jumlahPesanan;
        private string _catatan;
        private int _selisihRefund;

        public TransactionDetail() { _selisihRefund = 0; }

        public int IdDetail
        {
            get => _idDetail;
            set { if (value <= 0) throw new ArgumentException("ID Detail tidak valid."); _idDetail = value; }
        }

        public int IdTransaksi
        {
            get => _idTransaksi;
            set { if (value <= 0) throw new ArgumentException("ID Transaksi tidak valid."); _idTransaksi = value; }
        }

        public int IdProduk
        {
            get => _idProduk;
            set { if (value <= 0) throw new ArgumentException("ID Produk tidak valid."); _idProduk = value; }
        }

        public string NamaPenitip
        {
            get => _namaPenitip;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nama penitip wajib diisi."); _namaPenitip = value.Trim(); }
        }

        public int JumlahPesanan
        {
            get => _jumlahPesanan;
            set { if (value < 1) throw new ArgumentException("Jumlah minimal 1."); _jumlahPesanan = value; }
        }

        public string Catatan
        {
            get => _catatan;
            set => _catatan = string.IsNullOrWhiteSpace(value) ? "Tidak ada catatan." : value.Trim();
        }

        public int SelisihRefund
        {
            get => _selisihRefund;
            set { if (value < 0) _selisihRefund = 0; else _selisihRefund = value; }
        }
    }
}
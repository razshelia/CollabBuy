using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Transaction
    {
        private int _idTransaksi;
        private int _idKoordinator;
        private DateTime _tanggalTransaksi;
        private int _totalBayarGrup;
        private string _statusPesanan;
        private string _buktiBayar;
        private bool _isValid;

        public Transaction()
        {
            _tanggalTransaksi = DateTime.Now;
            _statusPesanan = "Menunggu";
            _isValid = false;
        }

        public int IdTransaksi
        {
            get => _idTransaksi;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID Transaksi tidak valid.");
                _idTransaksi = value;
            }
        }

        public int IdKoordinator
        {
            get => _idKoordinator;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID Koordinator tidak valid.");
                _idKoordinator = value;
            }
        }

        public DateTime TanggalTransaksi
        {
            get => _tanggalTransaksi;
            set => _tanggalTransaksi = value;
        }

        public int TotalBayarGrup
        {
            get => _totalBayarGrup;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Total bayar tidak boleh negatif.");
                _totalBayarGrup = value;
            }
        }

        public string StatusPesanan
        {
            get => _statusPesanan;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Status pesanan tidak boleh kosong.");
                _statusPesanan = value.Trim();
            }
        }

        public string BuktiBayar
        {
            get => _buktiBayar;
            set => _buktiBayar = value; // bisa null
        }

        public bool IsValid
        {
            get => _isValid;
            set => _isValid = value;
        }
    }
}
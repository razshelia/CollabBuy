using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Catalog
    {
        private int _idProduk;
        private string _judulPo;
        private string _namaKategori;
        private string _namaProduk;
        private int _hargaDasar;
        private int? _hargaDiskon;
        private DateTime _batasWaktu;
        private string _infoRekening;

        public Catalog() { }

        public Catalog(int idProduk, string judulPo, string namaKategori, string namaProduk,
                       int hargaDasar, int? hargaDiskon, DateTime batasWaktu, string infoRekening)
        {
            IdProduk = idProduk;
            JudulPo = judulPo;
            NamaKategori = namaKategori;
            NamaProduk = namaProduk;
            HargaDasar = hargaDasar;
            HargaDiskon = hargaDiskon;
            BatasWaktu = batasWaktu;
            InfoRekening = infoRekening;
        }

        public int IdProduk
        {
            get => _idProduk;
            set { if (value <= 0) throw new ArgumentException("ID Produk tidak valid."); _idProduk = value; }
        }

        public string JudulPo
        {
            get => _judulPo;
            set { _judulPo = string.IsNullOrWhiteSpace(value) ? "PO Tanpa Judul" : value.Trim(); }
        }

        public string NamaKategori
        {
            get => _namaKategori;
            set { _namaKategori = string.IsNullOrWhiteSpace(value) ? "Umum" : value.Trim(); }
        }

        public string NamaProduk
        {
            get => _namaProduk;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nama produk kosong."); _namaProduk = value.Trim(); }
        }

        public int HargaDasar
        {
            get => _hargaDasar;
            set { if (value < 0) throw new ArgumentException("Harga tidak boleh negatif."); _hargaDasar = value; }
        }

        public int? HargaDiskon
        {
            get => _hargaDiskon;
            set { if (value.HasValue && value.Value < 0) throw new ArgumentException("Diskon tidak valid."); _hargaDiskon = value; }
        }

        public DateTime BatasWaktu
        {
            get => _batasWaktu;
            set { if (value.Year < 2026) _batasWaktu = DateTime.Now; else _batasWaktu = value; }
        }

        public string InfoRekening
        {
            get => _infoRekening;
            set { _infoRekening = string.IsNullOrWhiteSpace(value) ? "Hubungi Penjual" : value.Trim(); }
        }

        public string Tampilkan() => $"{NamaProduk} - Rp{HargaDasar}";

        public string Tampilkan(bool denganKategori) => denganKategori ? $"[{NamaKategori}] {Tampilkan()}" : Tampilkan();
    }
}
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

        // Overloading konstruktor #1: default
        public Catalog() { }

        // Overloading konstruktor #2: parameter lengkap
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
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID Produk tidak valid.");
                _idProduk = value;
            }
        }

        public string JudulPo
        {
            get => _judulPo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Judul PO tidak boleh kosong.");
                _judulPo = value.Trim();
            }
        }

        public string NamaKategori
        {
            get => _namaKategori;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama kategori tidak boleh kosong.");
                _namaKategori = value.Trim();
            }
        }

        public string NamaProduk
        {
            get => _namaProduk;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama produk tidak boleh kosong.");
                _namaProduk = value.Trim();
            }
        }

        public int HargaDasar
        {
            get => _hargaDasar;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga dasar tidak boleh negatif.");
                _hargaDasar = value;
            }
        }

        public int? HargaDiskon
        {
            get => _hargaDiskon;
            set
            {
                if (value.HasValue && value.Value < 0)
                    throw new ArgumentException("Harga diskon tidak boleh negatif.");
                _hargaDiskon = value;
            }
        }

        public DateTime BatasWaktu
        {
            get => _batasWaktu;
            set
            {
                // Tidak divalidasi masa lalu karena bisa berasal dari database
                _batasWaktu = value;
            }
        }

        public string InfoRekening
        {
            get => _infoRekening;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Info rekening tidak boleh kosong.");
                _infoRekening = value.Trim();
            }
        }

        // Overloading method Tampilkan()
        public string Tampilkan()
        {
            return $"{NamaProduk} - Rp{HargaDasar}";
        }

        public string Tampilkan(bool denganKategori)
        {
            return denganKategori
                ? $"[{NamaKategori}] {Tampilkan()}"
                : Tampilkan();
        }

        public string Tampilkan(bool denganKategori, bool denganBatasWaktu)
        {
            string hasil = Tampilkan(denganKategori);
            if (denganBatasWaktu)
                hasil += $" (sampai {BatasWaktu:dd MMM yyyy})";
            return hasil;
        }
    }
}
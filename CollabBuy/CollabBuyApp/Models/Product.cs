using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Product
    {
        private int _idProduk;
        private int _idPo;
        private int? _idKategori;
        private string _namaProduk;
        private int _hargaDasar;
        private int? _hargaDiskon;
        private int? _targetKuota;
        private int _minOrder;
        private string _fotoProduk;
        private string _deskripsi;

        public Product()
        {
            _minOrder = 1;
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

        public int IdPo
        {
            get => _idPo;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID PO tidak valid.");
                _idPo = value;
            }
        }

        public int? IdKategori
        {
            get => _idKategori;
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentException("ID Kategori tidak valid.");
                _idKategori = value;
            }
        }

        public string NamaProduk
        {
            get => _namaProduk;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama produk wajib diisi.");
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

        public int? TargetKuota
        {
            get => _targetKuota;
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentException("Target kuota harus lebih dari 0.");
                _targetKuota = value;
            }
        }

        public int MinOrder
        {
            get => _minOrder;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Minimal pemesanan harus ≥ 1.");
                _minOrder = value;
            }
        }

        public string FotoProduk
        {
            get => _fotoProduk;
            set => _fotoProduk = value;
        }

        // Deskripsi produk — boleh kosong/null
        public string Deskripsi
        {
            get => _deskripsi;
            set => _deskripsi = value;
        }
    }
}
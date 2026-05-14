using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Product
    {
        // ── properti lama ──
        private string namaProduk;
        private int stokProduk;
        private string fotoProduk;
        private bool isAktif;
        private Kategori kategoriProduk;

        // ── properti baru (dengan validasi) ──
        private int idProduk;
        private decimal hargaSatuan;
        private int idSeller;

        public Product()
        {
            this.isAktif = true;
        }

        // IdProduk
        public int IdProduk
        {
            get { return this.idProduk; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID produk tidak valid.");
                this.idProduk = value;
            }
        }

        // HargaSatuan (harga per unit produk)
        public decimal HargaSatuan
        {
            get { return this.hargaSatuan; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga satuan tidak boleh negatif.");
                this.hargaSatuan = value;
            }
        }

        // IdSeller (foreign key ke user penjual)
        public int IdSeller
        {
            get { return this.idSeller; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID seller tidak valid.");
                this.idSeller = value;
            }
        }

        // ── properti lama tetap ada ──
        public string NamaProduk
        {
            get { return this.namaProduk; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama produk wajib diisi.");
                this.namaProduk = value;
            }
        }

        public int StokProduk
        {
            get { return this.stokProduk; }
            set { this.stokProduk = value < 0 ? 0 : value; }
        }

        public string FotoProduk
        {
            get { return this.fotoProduk; }
            set { this.fotoProduk = string.IsNullOrWhiteSpace(value) ? "Images/Products/default.png" : value; }
        }

        public bool IsAktif
        {
            get { return this.isAktif; }
            set { this.isAktif = value; }
        }

        public Kategori KategoriProduk
        {
            get { return this.kategoriProduk; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Kategori tidak valid.");
                this.kategoriProduk = value;
            }
        }
    }
}
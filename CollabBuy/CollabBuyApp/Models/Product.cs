using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Product
    {
        private string namaProduk;
        private int stokProduk;
        private string fotoProduk;
        private bool isAktif;

        // COMPOSITION/AGGREGATION: Menghubungkan produk dengan Kategori
        private Kategori kategoriProduk;

        public Product()
        {
            this.isAktif = true;
        }

        public string NamaProduk
        {
            get { return this.namaProduk; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nama produk wajib diisi.");
                }
                else
                {
                    this.namaProduk = value;
                }
            }
        }

        public int StokProduk
        {
            get { return this.stokProduk; }
            set
            {
                if (value < 0)
                {
                    this.stokProduk = 0; // Mencegah stok minus
                }
                else
                {
                    this.stokProduk = value;
                }
            }
        }

        public string FotoProduk
        {
            get { return this.fotoProduk; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    // Fallback ke gambar default jika path kosong
                    this.fotoProduk = "Images/Products/default.png";
                }
                else
                {
                    this.fotoProduk = value;
                }
            }
        }

        public bool IsAktif
        {
            get { return this.isAktif; }
            set
            {
                if (value == true)
                {
                    this.isAktif = true;
                }
                else
                {
                    this.isAktif = false;
                }
            }
        }

        public Kategori KategoriProduk
        {
            get { return this.kategoriProduk; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Kategori tidak valid.");
                }
                else
                {
                    this.kategoriProduk = value;
                }
            }
        }
    }
}
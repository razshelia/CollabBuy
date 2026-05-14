using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public abstract class PreOrder
    {
        private string namaBarang;

        // ENCAPSULATION PROTECTED: Hanya bisa diakses oleh kelas PreOrder dan turunannya.
        // UI tidak akan bisa melihat atau mengubah `hargaDasar` secara langsung.
        protected decimal hargaDasar;

        public string NamaBarang
        {
            get { return this.namaBarang; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nama barang PO tidak valid.");
                }
                else
                {
                    this.namaBarang = value;
                }
            }
        }

        // Method untuk mengisi harga dari database (diakses oleh child class)
        protected void SetHargaDasar(decimal harga)
        {
            if (harga < 0)
            {
                this.hargaDasar = 0;
            }
            else
            {
                this.hargaDasar = harga;
            }
        }

        // Method abstrak untuk menghitung harga akhir sesuai jenis PO
        public abstract decimal HitungHargaFinal(int kuotaTerkumpul);
    }
}
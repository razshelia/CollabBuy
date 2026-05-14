using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class DetailCheckout
    {
        private string namaPenitip;
        private int jumlahPesanan;

        // Constructor internal agar hanya bisa dibuat oleh kelas Checkout (Composition yang sangat ketat)
        internal DetailCheckout(string nama, int jumlah)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                this.namaPenitip = "Anonim";
            }
            else
            {
                this.namaPenitip = nama;
            }

            if (jumlah <= 0)
            {
                this.jumlahPesanan = 1;
            }
            else
            {
                this.jumlahPesanan = jumlah;
            }
        }

        public string NamaPenitip
        {
            get { return this.namaPenitip; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nama penitip tidak boleh kosong.");
                }
                else
                {
                    this.namaPenitip = value;
                }
            }
        }

        public int JumlahPesanan
        {
            get { return this.jumlahPesanan; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Jumlah pesanan minimal 1.");
                }
                else
                {
                    this.jumlahPesanan = value;
                }
            }
        }
    }
}
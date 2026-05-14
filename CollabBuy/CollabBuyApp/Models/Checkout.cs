using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Checkout
    {
        private string metodePembayaran;
        private decimal totalBayarAwal;
        private decimal kembalian;
        private string statusPesanan;

        // COMPOSITION: Induk menguasai penuh siklus hidup List Detail
        private List<DetailCheckout> daftarDetail;

        public Checkout(string metode, decimal totalAwal)
        {
            if (string.IsNullOrWhiteSpace(metode))
            {
                this.metodePembayaran = "Transfer";
            }
            else
            {
                this.metodePembayaran = metode;
            }

            if (totalAwal < 0)
            {
                this.totalBayarAwal = 0;
            }
            else
            {
                this.totalBayarAwal = totalAwal;
            }

            this.kembalian = 0;
            this.statusPesanan = "Menunggu";

            // Instansiasi List langsung dilakukan di dalam konstruktor
            this.daftarDetail = new List<DetailCheckout>();
        }

        // METHOD COMPOSITION: Objek detail hanya boleh diciptakan lewat fungsi ini
        public void TambahPesananTitipan(string nama, int jumlah)
        {
            if (string.IsNullOrWhiteSpace(nama) || jumlah <= 0)
            {
                // UX Friendly: Abaikan tanpa crash
            }
            else
            {
                DetailCheckout detailBaru = new DetailCheckout(nama, jumlah);
                this.daftarDetail.Add(detailBaru);
            }
        }

        public decimal Kembalian
        {
            get { return this.kembalian; }
            set
            {
                if (value < 0)
                {
                    this.kembalian = 0;
                }
                else
                {
                    this.kembalian = value;
                }
            }
        }

        public string StatusPesanan
        {
            get { return this.statusPesanan; }
            set
            {
                if (value == "Menunggu" || value == "Diproses" || value == "Tersedia" || value == "Selesai" || value == "Dibatalkan")
                {
                    this.statusPesanan = value;
                }
                else
                {
                    throw new ArgumentException("Status pesanan tidak dikenali sistem.");
                }
            }
        }
    }
}
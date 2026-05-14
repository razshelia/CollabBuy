namespace CollabBuy.CollabBuyApp.Models
{
    public class POGotongRoyong : PreOrder
    {
        private int targetDiskon;
        private decimal hargaDiskon;

        public POGotongRoyong(string nama, decimal hargaAwal, decimal diskon, int target)
        {
            this.NamaBarang = nama;
            this.SetHargaDasar(hargaAwal); // Mengakses protected method dari induk

            if (target <= 0)
            {
                this.targetDiskon = 10; // Default
            }
            else
            {
                this.targetDiskon = target;
            }

            if (diskon >= hargaAwal)
            {
                this.hargaDiskon = hargaAwal - 1000; // Mencegah diskon lebih besar dari harga
            }
            else
            {
                this.hargaDiskon = diskon;
            }
        }

        public override decimal HitungHargaFinal(int kuotaTerkumpul)
        {
            // Mengakses protected field `this.hargaDasar` dari kelas induk
            if (kuotaTerkumpul >= this.targetDiskon)
            {
                return this.hargaDiskon; // Target tercapai, dapat diskon!
            }
            else
            {
                return this.hargaDasar; // Target belum tercapai, harga normal
            }
        }
    }
}
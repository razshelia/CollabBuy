namespace CollabBuy.CollabBuyApp.Models
{
    public class POBiasa : PreOrder
    {
        public POBiasa(string nama, decimal harga)
        {
            this.NamaBarang = nama;
            // Mengakses method protected dari kelas induk (PreOrder)
            this.SetHargaDasar(harga);
        }

        // OVERRIDE: Harga final PO Biasa selalu sama dengan harga dasar, berapapun kuotanya
        public override decimal HitungHargaFinal(int kuotaTerkumpul)
        {
            if (kuotaTerkumpul < 0)
            {
                return this.hargaDasar;
            }
            else
            {
                return this.hargaDasar;
            }
        }
    }
}
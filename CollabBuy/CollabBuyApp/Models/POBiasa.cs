namespace CollabBuy.CollabBuyApp.Models
{
    public class PreorderBiasa : Preorder
    {
        public override string JenisPo => "Biasa";

        public override decimal HitungHarga(int jumlah, decimal hargaDasar)
        {
            return jumlah * hargaDasar;
        }
    }
}
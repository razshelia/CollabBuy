namespace CollabBuy.CollabBuyApp.Models
{
    public class POBiasa : Preorder
    {
        // Mengisi JenisPo secara otomatis
        public override string JenisPo => "Biasa";

        // Implementasi Polimorfisme: Harga normal tanpa diskon
        public override int HitungHarga(int jumlah, int hargaDasar)
        {
            return jumlah * hargaDasar;
        }
    }
}
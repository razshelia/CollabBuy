namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IPembeli
    {
        // Kontrak bahwa setiap pembeli wajib bisa melakukan checkout
        bool LakukanCheckout(int idPo, int jumlah);
    }
}
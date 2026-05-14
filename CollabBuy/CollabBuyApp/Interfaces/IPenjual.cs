namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IPenjual
    {
        // Kontrak bahwa setiap penjual wajib bisa mengelola produknya
        bool KelolaProduk(string namaProduk, int stok);
    }
}
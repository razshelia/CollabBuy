using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IProductRepository
    {
        bool TambahProduk(Product produk);
        List<Product> AmbilProdukByPo(int idPo);
        Product AmbilProdukById(int idProduk);
        bool UpdateProduk(Product produk);
        bool HapusProduk(int idProduk);
        int HitungHargaAktual(int idProduk);
        int AmbilJumlahProduk();

        // Method ini WAJIB dipertahankan untuk mengisi ComboBox di Form Buat PO
        List<Product> AmbilProdukByPenjual(int idPenjual);
    }
}
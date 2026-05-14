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
    }
}
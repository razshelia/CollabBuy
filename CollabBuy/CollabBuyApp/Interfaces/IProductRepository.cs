using CollabBuy.CollabBuyApp.Models;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IProductRepository
    {
        // Fungsi Manajemen Produk oleh Penjual
        bool TambahProduk(Product produkBaru, int idSeller);
        bool EditProduk(Product produkLama);
        bool HapusProduk(int idProduk);

        // Fungsi Penarikan Data (Katalog)
        List<Product> AmbilSemuaProduk();

        // Pencarian (Polymorphism Overloading dari Controller ke Repository)
        List<Product> CariProduk(string keywordNama);
        List<Product> CariProduk(int idKategori);
    }
}
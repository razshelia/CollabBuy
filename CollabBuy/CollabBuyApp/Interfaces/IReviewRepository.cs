using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IReviewRepository
    {
        bool TambahUlasan(Review ulasan);
        List<Review> AmbilUlasanByProduk(int idProduk);
        List<Review> AmbilUlasanByPenjual(int idPenjual);
        bool BalasUlasan(int idUlasan, string balasan);
    }
}
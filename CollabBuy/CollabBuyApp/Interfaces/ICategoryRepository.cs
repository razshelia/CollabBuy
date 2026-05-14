using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> AmbilSemua();
        Category AmbilById(int id);
        bool Tambah(Category kategori);
        bool Update(Category kategori);
        bool Hapus(int id);
    }
}
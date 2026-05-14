using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CategoryService
    {
        private CategoryRepository categoryRepo;

        public CategoryService()
        {
            this.categoryRepo = new CategoryRepository();
        }

        public List<Kategori> MuatSemuaKategori()
        {
            List<Kategori> daftar = this.categoryRepo.AmbilSemuaKategori();

            if (daftar.Count == 0)
            {
                // Tidak perlu error, mungkin memang belum ada data dari admin
                return new List<Kategori>();
            }
            else
            {
                return daftar;
            }
        }

        // Catatan: Tambahkan method TambahKategori(), EditKategori() 
        // dengan pola if-else dan UXHelper yang sama untuk Admin.
    }
}
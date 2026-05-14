using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CatalogService
    {
        private readonly ICatalogRepository _catalogRepo;

        public CatalogService()
        {
            _catalogRepo = new CatalogRepository();
        }

        /// <summary>
        /// Mengambil semua produk dari PO yang masih aktif dan belum lewat batas waktu.
        /// </summary>
        public List<Catalog> AmbilKatalogAktif()
        {
            return _catalogRepo.AmbilKatalogAktif();
        }
    }
}
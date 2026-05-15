using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
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
            try
            {
                return _catalogRepo.AmbilKatalogAktif();
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return new List<Catalog>();
            }
        }
    }
}
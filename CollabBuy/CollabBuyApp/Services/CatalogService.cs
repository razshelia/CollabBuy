using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CatalogService
    {
        private readonly ICatalogRepository _catalogRepo;
        public CatalogService(ICatalogRepository catalogRepo)
        {
            _catalogRepo = catalogRepo;
        }

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
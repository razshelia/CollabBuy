using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface ICatalogRepository
    {
        List<Catalog> AmbilKatalogAktif();
    }
}
using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CatalogRepository : BaseRepository, ICatalogRepository
    {
        public List<Catalog> AmbilKatalogAktif()
        {
            List<Catalog> list = new List<Catalog>();
            string sql = "SELECT id_produk, judul_po, nama_kategori, nama_produk, harga_dasar, harga_diskon, batas_waktu, info_rekening FROM vw_katalog_aktif";

            ExecuteQuery(sql, null, reader =>
            {
                list.Add(new Catalog(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    reader.GetDateTime(6),
                    reader.GetString(7)
                ));
            });

            return list;
        }
    }
}
using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly DatabaseHelper _db;

        public CatalogRepository()
        {
            _db = new DatabaseHelper();
        }

        public List<Catalog> AmbilKatalogAktif()
        {
            List<Catalog> list = new List<Catalog>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                // Menggunakan view vw_katalog_aktif
                string sql = "SELECT id_produk, judul_po, nama_kategori, nama_produk, harga_dasar, harga_diskon, batas_waktu, info_rekening FROM vw_katalog_aktif";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal mengambil data katalog aktif dari database.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return list;
        }
    }
}
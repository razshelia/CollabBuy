using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CategoryRepository
    {
        private DatabaseHelper dbHelper;

        public CategoryRepository()
        {
            this.dbHelper = new DatabaseHelper();
        }

        public List<Kategori> AmbilSemuaKategori()
        {
            List<Kategori> daftarKategori = new List<Kategori>();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

            if (koneksi == null)
            {
                return daftarKategori;
            }

            try
            {
                koneksi.Open();

                // ✅ FIX: Tambahkan id_kategori ke query agar ValueMember bisa dipakai
                string sql = "SELECT id_kategori, nama_kategori, deskripsi FROM categories ORDER BY nama_kategori";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Kategori kat = new Kategori();
                            kat.IdKategori = reader.GetInt32(0);          // ✅ FIX: Set IdKategori
                            kat.NamaKategori = reader.GetString(1);
                            kat.Deskripsi = reader.IsDBNull(2) ? "Tidak ada deskripsi" : reader.GetString(2);

                            daftarKategori.Add(kat);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Fail gracefully
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }

            return daftarKategori;
        }
    }
}
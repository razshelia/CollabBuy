using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public List<Category> AmbilSemua()
        {
            List<Category> list = new List<Category>();
            string sql = "SELECT id_kategori, nama_kategori FROM categories ORDER BY nama_kategori";
            ExecuteQuery(sql, null, reader =>
            {
                Category kat = new Category();
                kat.IdKategori = reader.GetInt32(0);
                kat.NamaKategori = reader.GetString(1);
                list.Add(kat);
            });

            return list;
        }

        public Category AmbilById(int id)
        {
            Category kat = null;
            string sql = "SELECT id_kategori, nama_kategori FROM categories WHERE id_kategori = @id";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", id), reader =>
            {
                kat = new Category();
                kat.IdKategori = reader.GetInt32(0);
                kat.NamaKategori = reader.GetString(1);
            });

            return kat;
        }

        public bool Tambah(Category kategori)
        {
            string sql = "INSERT INTO categories (nama_kategori) VALUES (@nama)";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("nama", kategori.NamaKategori);
            });

            return row > 0;
        }

        public bool Update(Category kategori)
        {
            string sql = "UPDATE categories SET nama_kategori = @nama WHERE id_kategori = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("nama", kategori.NamaKategori);
                cmd.Parameters.AddWithValue("id", kategori.IdKategori);
            });

            return row > 0;
        }

        public bool Hapus(int id)
        {
            string sql = "DELETE FROM categories WHERE id_kategori = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("id", id);
            });

            return row > 0;
        }
    }
}
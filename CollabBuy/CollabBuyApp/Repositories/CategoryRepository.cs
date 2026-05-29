using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data Kategori Produk.
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// </summary>
    public class CategoryRepository : IQueryRepository<Category>, ICommandRepository<Category>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public CategoryRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<Category>
        // =======================================================

        public Category GetById(int idKategori)
        {
            Category kategori = null;

            string query = "SELECT id_kategori, nama_kategori FROM categories WHERE id_kategori = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKategori);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string namaKategori = reader.GetString(reader.GetOrdinal("nama_kategori"));

                            kategori = new Category(namaKategori);
                            kategori.SetIdKategori(reader.GetInt32(reader.GetOrdinal("id_kategori")));
                        }
                    }
                }
            }
            return kategori;
        }

        public List<Category> GetAll()
        {
            List<Category> listKategori = new List<Category>();
            string query = "SELECT id_kategori, nama_kategori FROM categories ORDER BY nama_kategori;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string namaKategori = reader.GetString(reader.GetOrdinal("nama_kategori"));

                            Category kategori = new Category(namaKategori);
                            kategori.SetIdKategori(reader.GetInt32(reader.GetOrdinal("id_kategori")));

                            listKategori.Add(kategori);
                        }
                    }
                }
            }
            return listKategori;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<Category>
        // =======================================================

        public void Insert(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity kategori tidak boleh null.");

            string query = "INSERT INTO categories (nama_kategori) VALUES (@nama);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", entity.GetNamaKategori());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal menyimpan kategori baru ke database.", "", "DB_INSERT_CATEGORY_FAILED");
                    }
                }
            }
        }

        public void Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity kategori tidak boleh null.");

            string query = "UPDATE categories SET nama_kategori = @nama WHERE id_kategori = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdKategori());
                    cmd.Parameters.AddWithValue("@nama", entity.GetNamaKategori());

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
using System;
using System.Data;
using Npgsql;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan!");
        }

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_kategori, nama_kategori FROM categories WHERE is_deleted = FALSE ORDER BY id_kategori ASC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        public void Insert(string namaKategori)
        {
            string query = "INSERT INTO categories (nama_kategori) VALUES (@nama);";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", namaKategori);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id, string namaKategori)
        {
            string query = "UPDATE categories SET nama_kategori = @nama WHERE id_kategori = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", namaKategori);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            string query = "UPDATE categories SET is_deleted = TRUE WHERE id_kategori = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

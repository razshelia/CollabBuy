using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CategoryRepository : BaseRepository, ISoftDeletable
    {
        public CategoryRepository() : base() { }

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
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                string queryCheck = "SELECT COUNT(*) FROM categories WHERE LOWER(nama_kategori) = LOWER(@nama) AND is_deleted = FALSE;";
                using (var cmdCheck = new NpgsqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@nama", namaKategori);
                    long count = (long)cmdCheck.ExecuteScalar();
                    if (count > 0)
                        throw new Exception("Nama kategori sudah ada!");
                }

                string query = "INSERT INTO categories (nama_kategori) VALUES (@nama);";
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
        public bool IsKategoriDigunakanProduk(int idKategori)
        {
            string query = "SELECT COUNT(*) FROM products WHERE id_kategori = @id AND is_deleted = FALSE;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKategori);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public void SoftDelete(int id)
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

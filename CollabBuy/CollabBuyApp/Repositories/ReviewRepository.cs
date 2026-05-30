using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ReviewRepository : IQueryRepository<Review>, ICommandRepository<Review>
    {
        private readonly string _connectionString;

        public ReviewRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan!");
        }

        public Review GetById(int idUlasan)
        {
            Review review = null;
            string query = "SELECT id_ulasan, id_produk, id_user, rating, komentar, balasan_penjual FROM reviews WHERE id_ulasan = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUlasan);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            review = new Review(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.IsDBNull(4) ? "" : reader.GetString(4));
                            review.SetIdUlasan(reader.GetInt32(0));
                            if (!reader.IsDBNull(5)) review.BeriTanggapan(reader.GetString(5));
                        }
                    }
                }
            }
            return review;
        }

        public List<Review> GetAll()
        {
            return new List<Review>();
        }

        // --- METHOD TAMBAHAN UNTUK UI REVIEW ---
        public DataTable GetReviewsByPenjual(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT r.id_ulasan, p.nama_produk, u.nama AS nama_pembeli, r.rating, r.komentar, r.tanggal_ulasan, r.balasan_penjual
                FROM reviews r
                JOIN products p ON r.id_produk = p.id_produk
                JOIN users u ON r.id_user = u.id_user
                WHERE p.id_penjual = @id
                ORDER BY r.tanggal_ulasan DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPenjual);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetProdukBisaDiulas(int idUser)
        {
            DataTable dt = new DataTable();
            // User hanya bisa review barang yang pernah dia beli dan status transaksinya selesai
            string query = @"
                SELECT DISTINCT p.id_produk, p.nama_produk
                FROM transaction_details td
                JOIN transactions t ON td.id_transaksi = t.id_transaksi
                JOIN products p ON td.id_produk = p.id_produk
                WHERE t.id_koordinator = @id AND t.status_pesanan = 'Selesai';";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public void Insert(Review entity)
        {
            string query = "INSERT INTO reviews (id_produk, id_user, rating, komentar) VALUES (@idProduk, @idUser, @rating, @komentar);";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduk", entity.GetIdProduk());
                    cmd.Parameters.AddWithValue("@idUser", entity.GetIdUser());
                    cmd.Parameters.AddWithValue("@rating", entity.GetRating());
                    cmd.Parameters.AddWithValue("@komentar", string.IsNullOrEmpty(entity.GetKomentar()) ? (object)DBNull.Value : entity.GetKomentar());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Review entity)
        {
            string query = "UPDATE reviews SET balasan_penjual = @balasan WHERE id_ulasan = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdUlasan());
                    cmd.Parameters.AddWithValue("@balasan", string.IsNullOrEmpty(entity.GetTanggapan()) ? (object)DBNull.Value : entity.GetTanggapan());
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
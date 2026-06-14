using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ReviewRepository : BaseRepository, IQueryRepository<Review>, ICommandRepository<Review>
    {
        public ReviewRepository() : base() { }

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
                            review.IdUlasan = reader.GetInt32(0);
                            if (!reader.IsDBNull(5)) review.BeriTanggapan(reader.GetString(5));
                        }
                    }
                }
            }
            return review;
        }

        public DataTable GetReviewsByPenjual(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT id_ulasan, nama_produk, nama_pembeli,
                   rating, komentar, tanggal_ulasan, balasan_penjual
            FROM vw_ulasan_penjual
            WHERE id_penjual = @id;";

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
            string query = "SELECT id_produk, nama_produk FROM fn_produk_bisa_diulas(@id);";

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
                    cmd.Parameters.AddWithValue("@idProduk", entity.IdProduk);
                    cmd.Parameters.AddWithValue("@idUser", entity.IdUser);
                    cmd.Parameters.AddWithValue("@rating", entity.Rating);
                    cmd.Parameters.AddWithValue("@komentar", string.IsNullOrEmpty(entity.Komentar) ? (object)DBNull.Value : entity.Komentar);
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
                    cmd.Parameters.AddWithValue("@id", entity.IdUlasan);
                    cmd.Parameters.AddWithValue("@balasan", string.IsNullOrEmpty(entity.GetTanggapan()) ? (object)DBNull.Value : entity.GetTanggapan());
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

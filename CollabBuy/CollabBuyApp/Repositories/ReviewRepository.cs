using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data Ulasan (Review).
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// </summary>
    public class ReviewRepository : IQueryRepository<Review>, ICommandRepository<Review>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public ReviewRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<Review>
        // =======================================================

        public Review GetById(int idUlasan)
        {
            Review review = null;

            string query = "SELECT id_ulasan, id_produk, id_user, rating, komentar, balasan_penjual FROM reviews WHERE id_ulasan = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUlasan);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idProduk = reader.GetInt32(reader.GetOrdinal("id_produk"));
                            int idUser = reader.GetInt32(reader.GetOrdinal("id_user"));
                            int rating = reader.GetInt32(reader.GetOrdinal("rating"));
                            string komentar = reader.IsDBNull(reader.GetOrdinal("komentar")) ? "" : reader.GetString(reader.GetOrdinal("komentar"));

                            review = new Review(idProduk, idUser, rating, komentar);
                            review.SetIdUlasan(reader.GetInt32(reader.GetOrdinal("id_ulasan")));

                            // Pemetaan Interface IResolvable dari DB ke RAM
                            if (!reader.IsDBNull(reader.GetOrdinal("balasan_penjual")))
                            {
                                string balasanDb = reader.GetString(reader.GetOrdinal("balasan_penjual"));
                                review.BeriTanggapan(balasanDb);
                            }
                        }
                    }
                }
            }
            return review;
        }

        public List<Review> GetAll()
        {
            List<Review> listReview = new List<Review>();
            string query = "SELECT id_ulasan, id_produk, id_user, rating, komentar, balasan_penjual FROM reviews ORDER BY id_ulasan DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idProduk = reader.GetInt32(reader.GetOrdinal("id_produk"));
                            int idUser = reader.GetInt32(reader.GetOrdinal("id_user"));
                            int rating = reader.GetInt32(reader.GetOrdinal("rating"));
                            string komentar = reader.IsDBNull(reader.GetOrdinal("komentar")) ? "" : reader.GetString(reader.GetOrdinal("komentar"));

                            Review review = new Review(idProduk, idUser, rating, komentar);
                            review.SetIdUlasan(reader.GetInt32(reader.GetOrdinal("id_ulasan")));

                            if (!reader.IsDBNull(reader.GetOrdinal("balasan_penjual")))
                            {
                                review.BeriTanggapan(reader.GetString(reader.GetOrdinal("balasan_penjual")));
                            }

                            listReview.Add(review);
                        }
                    }
                }
            }
            return listReview;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<Review>
        // =======================================================

        public void Insert(Review entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity review tidak boleh null.");

            string query = "INSERT INTO reviews (id_produk, id_user, rating, komentar) VALUES (@idProduk, @idUser, @rating, @komentar);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduk", entity.GetIdProduk());
                    cmd.Parameters.AddWithValue("@idUser", entity.GetIdUser());
                    cmd.Parameters.AddWithValue("@rating", entity.GetRating());
                    cmd.Parameters.AddWithValue("@komentar", string.IsNullOrEmpty(entity.GetKomentar()) ? (object)DBNull.Value : entity.GetKomentar());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal menyimpan review ke database.", "", "DB_INSERT_REVIEW_FAILED");
                    }
                }
            }
        }

        public void Update(Review entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity review tidak boleh null.");

            // Update ini biasanya dipanggil saat Penjual membalas review (BeriTanggapan)
            string query = "UPDATE reviews SET balasan_penjual = @balasan WHERE id_ulasan = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdUlasan());
                    cmd.Parameters.AddWithValue("@balasan", string.IsNullOrEmpty(entity.GetTanggapan()) ? (object)DBNull.Value : entity.GetTanggapan());

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
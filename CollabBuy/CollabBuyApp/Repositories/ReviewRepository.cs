using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseHelper _db;

        public ReviewRepository()
        {
            _db = new DatabaseHelper();
        }

        public bool TambahUlasan(Review ulasan)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"INSERT INTO reviews (id_produk, id_user, rating, komentar)
                               VALUES (@idProduk, @idUser, @rating, @komentar)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idProduk", ulasan.IdProduk);
                    cmd.Parameters.AddWithValue("idUser", ulasan.IdUser);
                    cmd.Parameters.AddWithValue("rating", ulasan.Rating);
                    cmd.Parameters.AddWithValue("komentar", (object)ulasan.Komentar ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Gagal menyimpan ulasan ke database.", ex); }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }

        public List<Review> AmbilUlasanByProduk(int idProduk)
        {
            List<Review> list = new List<Review>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"SELECT r.id_ulasan, r.id_produk, r.id_user, r.rating, r.komentar, 
                                      r.tanggal_ulasan, r.balasan_penjual, u.username
                               FROM reviews r JOIN users u ON r.id_user = u.id_user
                               WHERE r.id_produk = @idProduk
                               ORDER BY r.tanggal_ulasan DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idProduk", idProduk);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review r = new Review();
                            r.IdUlasan = reader.GetInt32(0);
                            r.IdProduk = reader.GetInt32(1);
                            r.IdUser = reader.GetInt32(2);
                            r.Rating = reader.GetInt32(3);
                            r.Komentar = reader.IsDBNull(4) ? null : reader.GetString(4);
                            r.TanggalUlasan = reader.GetDateTime(5);
                            r.BalasanPenjual = reader.IsDBNull(6) ? null : reader.GetString(6);
                            list.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal mengambil daftar ulasan produk.", ex); }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
            return list;
        }

        public List<Review> AmbilUlasanByPenjual(int idPenjual)
        {
            List<Review> list = new List<Review>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"SELECT r.id_ulasan, r.id_produk, r.id_user, r.rating, r.komentar, 
                                      r.tanggal_ulasan, r.balasan_penjual
                               FROM reviews r
                               JOIN products p ON r.id_produk = p.id_produk
                               JOIN preorders po ON p.id_po = po.id_po
                               WHERE po.id_penjual = @idPenjual
                               ORDER BY r.tanggal_ulasan DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idPenjual", idPenjual);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review r = new Review();
                            r.IdUlasan = reader.GetInt32(0);
                            r.IdProduk = reader.GetInt32(1);
                            r.IdUser = reader.GetInt32(2);
                            r.Rating = reader.GetInt32(3);
                            r.Komentar = reader.IsDBNull(4) ? null : reader.GetString(4);
                            r.TanggalUlasan = reader.GetDateTime(5);
                            r.BalasanPenjual = reader.IsDBNull(6) ? null : reader.GetString(6);
                            list.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal mengambil daftar ulasan untuk penjual.", ex); }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
            return list;
        }

        public bool BalasUlasan(int idUlasan, string balasan)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "UPDATE reviews SET balasan_penjual = @balasan WHERE id_ulasan = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("balasan", balasan);
                    cmd.Parameters.AddWithValue("id", idUlasan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Gagal menyimpan balasan ulasan ke database.", ex); }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }
    }
}
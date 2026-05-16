using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public bool TambahUlasan(Review ulasan)
        {
            string sql = @"INSERT INTO reviews (id_produk, id_user, rating, komentar)
                           VALUES (@idProduk, @idUser, @rating, @komentar)";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("idProduk", ulasan.IdProduk);
                cmd.Parameters.AddWithValue("idUser", ulasan.IdUser);
                cmd.Parameters.AddWithValue("rating", ulasan.Rating);
                cmd.Parameters.AddWithValue("komentar", (object)ulasan.Komentar ?? DBNull.Value);
            });

            return row > 0;
        }

        public List<Review> AmbilUlasanByProduk(int idProduk)
        {
            List<Review> list = new List<Review>();
            string sql = @"SELECT r.id_ulasan, r.id_produk, r.id_user, r.rating, r.komentar, 
                                  r.tanggal_ulasan, r.balasan_penjual, u.username
                           FROM reviews r JOIN users u ON r.id_user = u.id_user
                           WHERE r.id_produk = @idProduk
                           ORDER BY r.tanggal_ulasan DESC";
            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idProduk", idProduk), reader =>
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
            });

            return list;
        }

        public List<Review> AmbilUlasanByPenjual(int idPenjual)
        {
            List<Review> list = new List<Review>();
            string sql = @"SELECT r.id_ulasan, r.id_produk, r.id_user, r.rating, r.komentar, 
                                  r.tanggal_ulasan, r.balasan_penjual
                           FROM reviews r
                           JOIN products p ON r.id_produk = p.id_produk
                           JOIN preorders po ON p.id_po = po.id_po
                           WHERE po.id_penjual = @idPenjual
                           ORDER BY r.tanggal_ulasan DESC";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idPenjual", idPenjual), reader =>
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
            });

            return list;
        }

        public bool BalasUlasan(int idUlasan, string balasan)
        {
            string sql = "UPDATE reviews SET balasan_penjual = @balasan WHERE id_ulasan = @id";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("balasan", balasan);
                cmd.Parameters.AddWithValue("id", idUlasan);
            });

            return row > 0;
        }
    }
}
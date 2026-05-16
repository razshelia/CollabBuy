using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Interfaces;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public bool TambahProduk(Product produk)
        {
            string sql = @"INSERT INTO products 
                           (id_penjual, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk)
                           VALUES (@penjual, @kategori, @nama, @deskripsi, @harga, @diskon, @target, @min, @foto)";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("penjual", produk.IdPenjual);
                cmd.Parameters.AddWithValue("kategori", (object)produk.IdKategori ?? DBNull.Value);
                cmd.Parameters.AddWithValue("nama", produk.NamaProduk);
                cmd.Parameters.AddWithValue("deskripsi", (object)produk.Deskripsi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("harga", produk.HargaDasar);
                cmd.Parameters.AddWithValue("diskon", (object)produk.HargaDiskon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("target", (object)produk.TargetKuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("min", produk.MinOrder);
                cmd.Parameters.AddWithValue("foto", (object)produk.FotoProduk ?? DBNull.Value);
            });

            return row > 0;
        }

        public List<Product> AmbilProdukByPo(int idPo)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM products WHERE id_po = @id";
            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idPo), reader =>
            {
                list.Add(MapEntity(reader));
            });

            return list;
        }

        public Product AmbilProdukById(int idProduk)
        {
            Product produk = null;
            string sql = "SELECT * FROM products WHERE id_produk = @id";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idProduk), reader =>
            {
                produk = MapEntity(reader);
            });

            return produk;
        }

        public bool UpdateProduk(Product produk)
        {
            string sql = @"UPDATE products 
                           SET id_kategori = @kategori, nama_produk = @nama, deskripsi = @deskripsi,
                               harga_dasar = @harga, harga_diskon = @diskon, target_kuota = @target, 
                               min_order = @min, foto_produk = @foto
                           WHERE id_produk = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("id", produk.IdProduk);
                cmd.Parameters.AddWithValue("kategori", (object)produk.IdKategori ?? DBNull.Value);
                cmd.Parameters.AddWithValue("nama", produk.NamaProduk);
                cmd.Parameters.AddWithValue("deskripsi", (object)produk.Deskripsi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("harga", produk.HargaDasar);
                cmd.Parameters.AddWithValue("diskon", (object)produk.HargaDiskon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("target", (object)produk.TargetKuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("min", produk.MinOrder);
                cmd.Parameters.AddWithValue("foto", (object)produk.FotoProduk ?? DBNull.Value);
            });

            return row > 0;
        }

        public bool HapusProduk(int idProduk)
        {
            string sql = "DELETE FROM products WHERE id_produk = @id";

            int row = ExecuteNonQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idProduk));

            return row > 0;
        }

        public int HitungHargaAktual(int idProduk)
        {
            string sql = "SELECT cek_harga_saat_ini(@id)";
            var result = ExecuteScalar(sql, cmd => cmd.Parameters.AddWithValue("id", idProduk));

            if (result != DBNull.Value && result != null)
                return Convert.ToInt32(result);

            return 0;
        }

        public int AmbilJumlahProduk()
        {
            string sql = "SELECT COUNT(*) FROM products";

            var result = ExecuteScalar(sql, null);

            if (result != DBNull.Value && result != null)
                return Convert.ToInt32(result);

            return 0;
        }

        public List<Product> AmbilProdukByPenjual(int idPenjual)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM products WHERE id_penjual = @id ORDER BY id_produk DESC";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idPenjual), reader =>
            {
                list.Add(MapEntity(reader));
            });

            return list;
        }
        private Product MapEntity(NpgsqlDataReader reader)
        {
            return new Product
            {
                IdProduk = Convert.ToInt32(reader["id_produk"]),
                IdPenjual = Convert.ToInt32(reader["id_penjual"]),
                IdPo = reader["id_po"] != DBNull.Value ? Convert.ToInt32(reader["id_po"]) : (int?)null,
                IdKategori = reader["id_kategori"] != DBNull.Value ? Convert.ToInt32(reader["id_kategori"]) : (int?)null,
                NamaProduk = reader["nama_produk"].ToString(),
                Deskripsi = reader["deskripsi"]?.ToString(),
                HargaDasar = Convert.ToInt32(reader["harga_dasar"]),
                HargaDiskon = reader["harga_diskon"] != DBNull.Value ? Convert.ToInt32(reader["harga_diskon"]) : (int?)null,
                TargetKuota = reader["target_kuota"] != DBNull.Value ? Convert.ToInt32(reader["target_kuota"]) : (int?)null,
                MinOrder = Convert.ToInt32(reader["min_order"]),
                FotoProduk = reader["foto_produk"]?.ToString()
            };
        }
    }
}
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DatabaseHelper _db = new DatabaseHelper();

        public bool TambahProduk(Product produk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"INSERT INTO products 
                               (id_penjual, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk)
                               VALUES (@penjual, @kategori, @nama, @deskripsi, @harga, @diskon, @target, @min, @foto)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
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

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Terjadi kesalahan saat menyimpan produk ke database.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public List<Product> AmbilProdukByPo(int idPo)
        {
            List<Product> list = new List<Product>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "SELECT * FROM products WHERE id_po = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idPo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) list.Add(MapEntity(reader));
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal memuat produk dari PO ini.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return list;
        }

        public Product AmbilProdukById(int idProduk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "SELECT * FROM products WHERE id_produk = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idProduk);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapEntity(reader);
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal mengambil detail produk.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return null;
        }

        public bool UpdateProduk(Product produk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"UPDATE products 
                               SET id_kategori = @kategori, nama_produk = @nama, deskripsi = @deskripsi,
                                   harga_dasar = @harga, harga_diskon = @diskon, target_kuota = @target, 
                                   min_order = @min, foto_produk = @foto
                               WHERE id_produk = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
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

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Gagal memperbarui data produk.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public bool HapusProduk(int idProduk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "DELETE FROM products WHERE id_produk = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idProduk);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Gagal menghapus produk karena data sedang digunakan.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public int HitungHargaAktual(int idProduk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return 0;

            try
            {
                conn.Open();
                string sql = "SELECT cek_harga_saat_ini(@id)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idProduk);
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null) return Convert.ToInt32(result);
                }
            }
            catch (Exception ex) { throw new Exception("Gagal menghitung harga aktual.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return 0;
        }

        public int AmbilJumlahProduk()
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return 0;

            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM products";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { throw new Exception("Gagal mengambil jumlah produk.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public List<Product> AmbilProdukByPenjual(int idPenjual)
        {
            List<Product> list = new List<Product>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "SELECT * FROM products WHERE id_penjual = @id ORDER BY id_produk DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idPenjual);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) list.Add(MapEntity(reader));
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal memuat katalog produk penjual.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
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
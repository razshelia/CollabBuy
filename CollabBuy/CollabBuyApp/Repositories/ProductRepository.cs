using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseHelper _db;

        public ProductRepository()
        {
            _db = new DatabaseHelper();
        }

        public bool TambahProduk(Product produk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = @"INSERT INTO products
                               (id_po, id_kategori, nama_produk, harga_dasar, harga_diskon,
                                target_kuota, min_order, foto_produk, deskripsi)
                               VALUES (@po, @kat, @nama, @harga, @diskon, @target, @min, @foto, @desk)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("po", produk.IdPo);
                    cmd.Parameters.AddWithValue("kat", (object)produk.IdKategori ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("nama", produk.NamaProduk);
                    cmd.Parameters.AddWithValue("harga", produk.HargaDasar);
                    cmd.Parameters.AddWithValue("diskon", (object)produk.HargaDiskon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("target", (object)produk.TargetKuota ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("min", produk.MinOrder);
                    cmd.Parameters.AddWithValue("foto", (object)produk.FotoProduk ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("desk", (object)produk.Deskripsi ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal tambah produk: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public List<Product> AmbilProdukByPo(int idPo)
        {
            List<Product> list = new List<Product>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = @"SELECT id_produk, id_po, id_kategori, nama_produk, harga_dasar,
                                      harga_diskon, target_kuota, min_order, foto_produk, deskripsi
                               FROM products WHERE id_po = @po";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("po", idPo);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        while (reader.Read())
                            list.Add(BuatProdukDariReader(reader));
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil produk: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return list;
        }

        public Product AmbilProdukById(int idProduk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return null;

            try
            {
                conn.Open();
                string sql = @"SELECT id_produk, id_po, id_kategori, nama_produk, harga_dasar,
                                      harga_diskon, target_kuota, min_order, foto_produk, deskripsi
                               FROM products WHERE id_produk = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idProduk);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        if (reader.Read())
                            return BuatProdukDariReader(reader);
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil produk: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return null;
        }

        public bool UpdateProduk(Product produk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = @"UPDATE products
                               SET nama_produk  = @nama,
                                   harga_dasar  = @harga,
                                   harga_diskon = @diskon,
                                   target_kuota = @target,
                                   min_order    = @min,
                                   foto_produk  = @foto,
                                   id_kategori  = @kat,
                                   deskripsi    = @desk
                               WHERE id_produk = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nama", produk.NamaProduk);
                    cmd.Parameters.AddWithValue("harga", produk.HargaDasar);
                    cmd.Parameters.AddWithValue("diskon", (object)produk.HargaDiskon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("target", (object)produk.TargetKuota ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("min", produk.MinOrder);
                    cmd.Parameters.AddWithValue("foto", (object)produk.FotoProduk ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("kat", (object)produk.IdKategori ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("desk", (object)produk.Deskripsi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("id", produk.IdProduk);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal update produk: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public bool HapusProduk(int idProduk)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

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
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal hapus produk: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private Product BuatProdukDariReader(NpgsqlDataReader reader)
        {
            return new Product
            {
                IdProduk = reader.GetInt32(0),
                IdPo = reader.GetInt32(1),
                IdKategori = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                NamaProduk = reader.GetString(3),
                HargaDasar = reader.GetInt32(4),
                HargaDiskon = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                TargetKuota = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                MinOrder = reader.GetInt32(7),
                FotoProduk = reader.IsDBNull(8) ? null : reader.GetString(8),
                Deskripsi = reader.IsDBNull(9) ? null : reader.GetString(9),
            };
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
                    object hasil = cmd.ExecuteScalar();
                    return hasil != DBNull.Value ? Convert.ToInt32(hasil) : 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal menghitung harga: " + ex.Message);
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
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
                    return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }
    }
}
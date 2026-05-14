using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DatabaseHelper dbHelper;

        public ProductRepository()
        {
            this.dbHelper = new DatabaseHelper();
        }

        public bool TambahProduk(Product produkBaru, int idSeller)
        {
            if (produkBaru == null)
            {
                return false;
            }
            else
            {
                NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

                if (koneksi == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        koneksi.Open();
                        string sql = "INSERT INTO products (id_seller, id_kategori, nama_produk, stok_produk, foto_produk, is_aktif) VALUES (@seller, @kategori, @nama, @stok, @foto, @aktif)";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                        {
                            cmd.Parameters.AddWithValue("seller", idSeller);
                            // Mengambil ID Kategori dari objek hasil Composition/Aggregation
                            cmd.Parameters.AddWithValue("kategori", 1); // Disederhanakan untuk contoh, idealnya produkBaru.KategoriProduk.Id
                            cmd.Parameters.AddWithValue("nama", produkBaru.NamaProduk);
                            cmd.Parameters.AddWithValue("stok", produkBaru.StokProduk);
                            cmd.Parameters.AddWithValue("foto", produkBaru.FotoProduk);
                            cmd.Parameters.AddWithValue("aktif", produkBaru.IsAktif);

                            cmd.ExecuteNonQuery();
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        if (koneksi.State == ConnectionState.Open)
                        {
                            koneksi.Close();
                        }
                        else
                        {
                            // Koneksi sudah tertutup
                        }
                    }
                }
            }
        }

        public bool EditProduk(Product produkLama)
        {
            // Implementasi UPDATE
            return false;
        }

        public bool HapusProduk(int idProduk)
        {
            if (idProduk <= 0)
            {
                return false;
            }
            else
            {
                NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

                if (koneksi == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        koneksi.Open();
                        // Kita gunakan Soft Delete (is_aktif = FALSE) agar data transaksi historis tidak rusak
                        string sql = "UPDATE products SET is_aktif = FALSE WHERE id_produk = @id";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                        {
                            cmd.Parameters.AddWithValue("id", idProduk);
                            cmd.ExecuteNonQuery();
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        if (koneksi.State == ConnectionState.Open)
                        {
                            koneksi.Close();
                        }
                        else
                        {
                            // Abaikan
                        }
                    }
                }
            }
        }

        public List<Product> AmbilSemuaProduk()
        {
            List<Product> daftarProduk = new List<Product>();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

            if (koneksi == null)
            {
                return daftarProduk;
            }
            else
            {
                try
                {
                    koneksi.Open();
                    string sql = "SELECT nama_produk, stok_produk, foto_produk FROM products WHERE is_aktif = TRUE";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product p = new Product();
                                p.NamaProduk = reader.GetString(0);
                                p.StokProduk = reader.GetInt32(1);
                                p.FotoProduk = reader.GetString(2);
                                daftarProduk.Add(p);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Kembalikan list kosong jika error
                }
                finally
                {
                    if (koneksi.State == ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                    else
                    {
                        // Abaikan
                    }
                }
                return daftarProduk;
            }
        }

        // --- POLYMORPHISM OVERLOADING ---

        // 1. Mencari menggunakan teks (Nama Produk)
        public List<Product> CariProduk(string keywordNama)
        {
            List<Product> hasilPencarian = new List<Product>();

            if (string.IsNullOrWhiteSpace(keywordNama))
            {
                return hasilPencarian;
            }
            else
            {
                // Implementasi pencarian query dengan ILIKE (PostgreSQL case-insensitive)
                return hasilPencarian;
            }
        }

        // 2. Mencari menggunakan angka (ID Kategori)
        public List<Product> CariProduk(int idKategori)
        {
            List<Product> hasilPencarian = new List<Product>();

            if (idKategori <= 0)
            {
                return hasilPencarian;
            }
            else
            {
                // Implementasi filter berdasarkan kategori
                return hasilPencarian;
            }
        }
    }
}
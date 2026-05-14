using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseHelper _db;

        public CategoryRepository()
        {
            _db = new DatabaseHelper();
        }

        public List<Category> AmbilSemua()
        {
            List<Category> list = new List<Category>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = "SELECT id_kategori, nama_kategori FROM categories ORDER BY nama_kategori";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category kat = new Category();
                        kat.IdKategori = reader.GetInt32(0);
                        kat.NamaKategori = reader.GetString(1);
                        list.Add(kat);
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal mengambil kategori: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public Category AmbilById(int id)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return null;

            try
            {
                conn.Open();
                string sql = "SELECT id_kategori, nama_kategori FROM categories WHERE id_kategori = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category kat = new Category();
                            kat.IdKategori = reader.GetInt32(0);
                            kat.NamaKategori = reader.GetString(1);
                            return kat;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal mengambil kategori: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }

        public bool Tambah(Category kategori)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "INSERT INTO categories (nama_kategori) VALUES (@nama)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nama", kategori.NamaKategori);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal menambah kategori: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool Update(Category kategori)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE categories SET nama_kategori = @nama WHERE id_kategori = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nama", kategori.NamaKategori);
                    cmd.Parameters.AddWithValue("id", kategori.IdKategori);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal update kategori: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool Hapus(int id)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "DELETE FROM categories WHERE id_kategori = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal menghapus kategori: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
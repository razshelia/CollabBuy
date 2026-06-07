using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ProductRepository : IQueryRepository<Product>, ICommandRepository<Product>
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
        }

        // =======================================================
        // IMPLEMENTASI IQueryRepository<Product>
        // =======================================================

        public Product GetById(int id)
        {
            Product p = null;
            string query = @"
                SELECT id_produk, id_penjual, id_po, id_kategori, nama_produk, 
                       deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk 
                FROM products 
                WHERE id_produk = @id AND is_deleted = FALSE;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = new Product(reader.GetInt32(reader.GetOrdinal("id_penjual")), reader.GetInt32(reader.GetOrdinal("id_kategori")),
                                            reader.GetString(reader.GetOrdinal("nama_produk")), reader.GetInt32(reader.GetOrdinal("harga_dasar")));
                            p.IdProduk = reader.GetInt32(reader.GetOrdinal("id_produk"));

                            if (!reader.IsDBNull(reader.GetOrdinal("id_po")))
                            {
                                p.IdPo = reader.GetInt32(reader.GetOrdinal("id_po"));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("deskripsi")))
                            {
                                p.Deskripsi = reader.GetString(reader.GetOrdinal("deskripsi"));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("harga_diskon")))
                            {
                                p.HargaDiskon = reader.GetInt32(reader.GetOrdinal("harga_diskon"));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("target_kuota")))
                            {
                                p.TargetKuota = reader.GetInt32(reader.GetOrdinal("target_kuota"));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("min_order")))
                            {
                                p.MinOrder = reader.GetInt32(reader.GetOrdinal("min_order"));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("foto_produk")))
                            {
                                p.FotoProduk = (byte[])reader["foto_produk"];
                            }
                        }
                    }
                }
            }
            return p;
        }
        /// <summary>
        /// Mengambil nama toko penjual berdasarkan id_produk.
        /// Dipakai di DetailProdukControl tanpa mengubah model Product.
        /// </summary>
        public string GetNamaTokoByIdProduk(int idProduk)
        {
            // Sebelumnya: query JOIN 3 tabel inline
            // Sekarang: fn_nama_toko_by_produk — satu baris
            string query = "SELECT fn_nama_toko_by_produk(@id);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idProduk);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value
                        ? result.ToString()
                        : "Penjual";
                }
            }
        }
        /// <summary>
        /// Mengembalikan List<Product> milik penjual tertentu dari DB.
        /// Dipakai untuk mengisi _katalogLapak di model Penjual.
        /// </summary>
        public List<Product> GetByPenjualAsList(int idPenjual)
        {
            var list = new List<Product>();
            // Sebelumnya: query JOIN inline
            // Sekarang: vw_produk_per_penjual — semua kolom yang dibutuhkan MappingReaderToProduct sudah ada
            string query = @"
                SELECT id_produk, id_penjual, id_po, id_kategori, nama_produk,
                       deskripsi, harga_dasar, harga_diskon, target_kuota, min_order,
                       foto_produk, jenis_po
                FROM vw_produk_per_penjual
                WHERE id_penjual = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPenjual);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MappingReaderToProduct(reader));
                    }
                }
            }
            return list;
        }

        // =======================================================
        // METHOD UNTUK UI (DATA TABLE)
        // =======================================================

        public DataTable GetKatalogUtama()
        {
            DataTable dt = new DataTable();
            // Sebelumnya: query 20+ baris dengan correlated subquery
            // Sekarang: pakai vw_katalog_produk yang sudah dibuat di batch sebelumnya
            string query = @"
                SELECT id_produk, nama_produk, nama_kategori, judul_po, harga_dasar,
                       harga_diskon, batas_waktu, foto_produk, nama_toko, jenis_po,
                       target_kuota, in_sesi_po, terpesan
                FROM vw_katalog_produk
                WHERE id_po IS NULL
                   OR (in_sesi_po = TRUE AND batas_waktu >= CURRENT_TIMESTAMP)
                ORDER BY batas_waktu ASC NULLS LAST;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetProdukLapak(int idPenjual)
        {
            DataTable dt = new DataTable();
            // Sebelumnya: query JOIN 3 tabel inline
            // Sekarang: vw_produk_per_penjual — kolom lengkap, filter id_penjual
            string query = @"
                SELECT id_produk, id_po, nama_produk, nama_kategori, judul_po,
                       harga_dasar, target_kuota, foto_produk, deskripsi, min_order, id_kategori
                FROM vw_produk_per_penjual
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

        public List<Product> GetAll()
        {
            var listProduk = new List<Product>();
            string query = "SELECT * FROM vw_produk_per_penjual;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        listProduk.Add(MappingReaderToProduct(reader));
                }
            }
            return listProduk;
        }

        public DataTable GetKatalogAktif(int limit = 100)
        {
            DataTable dtKatalog = new DataTable();
            string query = @"
                SELECT id_produk, nama_produk, nama_kategori, judul_po, harga_dasar,
                       harga_diskon, batas_waktu, foto_produk, nama_toko, jenis_po,
                       target_kuota, in_sesi_po, terpesan
                FROM vw_katalog_produk
                WHERE in_sesi_po = TRUE
                  AND batas_waktu >= CURRENT_TIMESTAMP
                ORDER BY batas_waktu ASC NULLS LAST
                LIMIT @limit;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dtKatalog);
                }
            }
            return dtKatalog;
        }

        public DataTable GetProdukByPenjualDataTable(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT id_produk, nama_produk, nama_kategori, harga_dasar, target_kuota, judul_po, foto_produk
                FROM vw_produk_per_penjual
                WHERE id_penjual = @idPenjual;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        // =======================================================
        // IMPLEMENTASI ICommandRepository<Product>
        // =======================================================

        public void Insert(Product entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity produk tidak boleh null.");

            string query = @"
                INSERT INTO products (id_penjual, id_po, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk, is_deleted) 
                VALUES (@penjual, @po, @kategori, @nama, @deskripsi, @hargaDasar, @hargaDiskon, @targetKuota, @minOrder, @foto, FALSE);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    MappingProductToParameters(cmd, entity);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOrderException("Gagal menyimpan produk baru ke database.", "", "DB_INSERT_PRODUCT_FAILED");
                }
            }
        }

        public void Update(Product entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity produk tidak boleh null.");

            string query = @"
                UPDATE products SET id_po = @po, id_kategori = @kategori, nama_produk = @nama, 
                deskripsi = @deskripsi, harga_dasar = @hargaDasar, harga_diskon = @hargaDiskon, 
                target_kuota = @targetKuota, min_order = @minOrder, foto_produk = @foto
                WHERE id_produk = @id AND is_deleted = FALSE;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.IdProduk);
                    MappingProductToParameters(cmd, entity);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOrderException("Gagal mengupdate produk, ID tidak ditemukan.", "id_produk", "DB_UPDATE_PRODUCT_FAILED");
                }
            }
        }

        /// <summary>
        /// Soft delete: set is_deleted = TRUE, data tidak hilang dari DB.
        /// </summary>
        public void SoftDelete(int idProduk)
        {
            string query = "UPDATE products SET is_deleted = TRUE WHERE id_produk = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idProduk);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOrderException("Gagal menghapus produk, ID tidak ditemukan.", "id_produk", "DB_DELETE_PRODUCT_FAILED");
                }
            }
        }

        // =======================================================
        // HELPER METHODS (DRY)
        // =======================================================

        private Product MappingReaderToProduct(NpgsqlDataReader reader)
        {
            var produk = new Product(reader.GetInt32(reader.GetOrdinal("id_penjual")), reader.GetInt32(reader.GetOrdinal("id_kategori")),
                                     reader.GetString(reader.GetOrdinal("nama_produk")), reader.GetInt32(reader.GetOrdinal("harga_dasar")));
            produk.IdProduk = reader.GetInt32(reader.GetOrdinal("id_produk"));

            if (!reader.IsDBNull(reader.GetOrdinal("id_po")))
            {
                produk.IdPo = reader.GetInt32(reader.GetOrdinal("id_po"));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("deskripsi")))
            {
                produk.Deskripsi = reader.GetString(reader.GetOrdinal("deskripsi"));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("harga_diskon")))
            {
                produk.HargaDiskon = reader.GetInt32(reader.GetOrdinal("harga_diskon"));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("target_kuota")))
            {
                produk.TargetKuota = reader.GetInt32(reader.GetOrdinal("target_kuota"));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("min_order")))
            {
                produk.MinOrder = reader.GetInt32(reader.GetOrdinal("min_order"));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("foto_produk")))
            {
                produk.FotoProduk = (byte[])reader["foto_produk"];
            }
            produk.JenisPo = !reader.IsDBNull(reader.GetOrdinal("jenis_po"))
                ? reader.GetString(reader.GetOrdinal("jenis_po"))
                : "Biasa";

            return produk;
        }

        private void MappingProductToParameters(NpgsqlCommand cmd, Product entity)
        {
            cmd.Parameters.AddWithValue("@penjual", entity.IdPenjual);
            cmd.Parameters.AddWithValue("@kategori", entity.IdKategori);
            cmd.Parameters.AddWithValue("@nama", entity.NamaProduk);
            cmd.Parameters.AddWithValue("@hargaDasar", entity.HargaDasar);
            cmd.Parameters.AddWithValue("@po", entity.IdPo.HasValue ? (object)entity.IdPo.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@deskripsi", string.IsNullOrEmpty(entity.Deskripsi) ? (object)DBNull.Value : entity.Deskripsi);
            cmd.Parameters.AddWithValue("@hargaDiskon", entity.HargaDiskon.HasValue ? (object)entity.HargaDiskon.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@targetKuota", entity.TargetKuota > 0 ? (object)entity.TargetKuota : DBNull.Value);
            cmd.Parameters.AddWithValue("@minOrder", entity.MinOrder);
            cmd.Parameters.AddWithValue("@foto", (object)entity.FotoProduk ?? DBNull.Value);
        }
        public DataTable GetProdukDalamPO(int idPo)
        {
            DataTable dt = new DataTable();
            // Sebelumnya: query panjang dengan correlated subquery terpesan
            // Sekarang: vw_katalog_produk (sudah include terpesan + in_sesi_po),
            // difilter id_po dan dipastikan PO aktif
            string query = @"
                SELECT id_produk, nama_produk, nama_kategori, judul_po,
                       harga_dasar, harga_diskon, batas_waktu, foto_produk,
                       nama_toko, jenis_po, target_kuota, terpesan, in_sesi_po
                FROM vw_katalog_produk
                WHERE id_po      = @idPo
                  AND in_sesi_po = TRUE
                  AND batas_waktu >= CURRENT_TIMESTAMP
                ORDER BY nama_produk ASC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPo", idPo);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetPOHampirPenuh()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_produk, nama_produk, judul_po, harga_dasar, target_kuota, terisi, foto_produk FROM vw_produk_hampir_penuh;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }
    }
}
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
        /// Mengembalikan List<Product> milik penjual tertentu dari DB.
        /// Dipakai untuk mengisi _katalogLapak di model Penjual.
        /// </summary>
        public List<Product> GetByPenjualAsList(int idPenjual)
        {
            var list = new List<Product>();
            string query = @"
        SELECT p.id_produk, p.id_penjual, p.id_po, p.id_kategori,
               p.nama_produk, p.deskripsi, p.harga_dasar, p.harga_diskon,
               p.target_kuota, p.min_order, p.foto_produk,
               COALESCE(po.jenis_po, 'Biasa') AS jenis_po
        FROM products p
        LEFT JOIN preorders po ON p.id_po = po.id_po
        WHERE p.id_penjual = @id AND p.is_deleted = FALSE
        ORDER BY p.id_produk DESC;";

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
            string query = @"
                SELECT p.id_produk, p.nama_produk, kat.nama_kategori, po.judul_po,
                       p.harga_dasar, p.harga_diskon, po.batas_waktu, p.foto_produk,
                       COALESCE(v.nama_toko, u.nama) AS nama_toko, po.jenis_po,
                       p.target_kuota,
                       CASE WHEN p.id_po IS NULL THEN FALSE ELSE TRUE END AS in_sesi_po,
                       COALESCE((SELECT SUM(td.jumlah_pesanan)
                                 FROM transaction_details td
                                 JOIN transactions t ON td.id_transaksi = t.id_transaksi
                                 WHERE td.id_produk = p.id_produk
                                   AND t.status_pesanan NOT IN ('Batal', 'Gagal')), 0) AS terpesan
                FROM products p
                LEFT JOIN preorders   po  ON p.id_po       = po.id_po
                LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
                LEFT JOIN users       u   ON p.id_penjual  = u.id_user
                LEFT JOIN verifications v ON p.id_penjual  = v.id_user
                WHERE p.is_deleted = FALSE
                  AND (p.id_po IS NULL
                   OR (po.is_aktif = TRUE AND po.batas_waktu >= CURRENT_TIMESTAMP))
                ORDER BY po.batas_waktu ASC NULLS LAST;";

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
            string query = @"
            SELECT p.id_produk, p.id_po, p.nama_produk, k.nama_kategori, po.judul_po, p.harga_dasar, p.target_kuota, p.foto_produk, p.deskripsi, p.min_order, p.id_kategori
            FROM products p
            JOIN categories k ON p.id_kategori = k.id_kategori
            LEFT JOIN preorders po ON p.id_po = po.id_po
            WHERE p.id_penjual = @id AND p.is_deleted = FALSE
            ORDER BY p.id_produk DESC;";

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
            string query = @"
                SELECT p.id_produk, p.id_penjual, p.id_po, p.id_kategori, 
                       p.nama_produk, p.deskripsi, p.harga_dasar, p.harga_diskon, 
                       p.target_kuota, p.min_order, p.foto_produk, po.jenis_po
                FROM products p
                LEFT JOIN preorders po ON p.id_po = po.id_po
                WHERE p.is_deleted = FALSE
                ORDER BY p.nama_produk;";

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
                SELECT p.id_produk, p.nama_produk, c.nama_kategori, p.harga_dasar, p.target_kuota, po.judul_po, p.foto_produk
                FROM products p
                LEFT JOIN categories c ON p.id_kategori = c.id_kategori
                LEFT JOIN preorders po ON p.id_po = po.id_po
                WHERE p.id_penjual = @idPenjual AND p.is_deleted = FALSE
                ORDER BY p.nama_produk;";

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
            string query = @"
            SELECT p.id_produk, p.nama_produk, kat.nama_kategori, po.judul_po,
                   p.harga_dasar, p.harga_diskon, po.batas_waktu, p.foto_produk,
                   COALESCE(v.nama_toko, u.nama) AS nama_toko, po.jenis_po,
                   p.target_kuota,
                   COALESCE((
                       SELECT SUM(td.jumlah_pesanan)
                       FROM transaction_details td
                       JOIN transactions t ON td.id_transaksi = t.id_transaksi
                       WHERE td.id_produk = p.id_produk
                         AND t.status_pesanan NOT IN ('Batal', 'Gagal')
                   ), 0) AS terpesan,
                   TRUE AS in_sesi_po
            FROM products p
            JOIN preorders po ON p.id_po = po.id_po
            LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
            LEFT JOIN users       u   ON p.id_penjual  = u.id_user
            LEFT JOIN verifications v ON p.id_penjual  = v.id_user
            WHERE p.id_po = @idPo
              AND p.is_deleted = FALSE
              AND po.is_aktif  = TRUE
              AND po.is_deleted = FALSE
            ORDER BY p.nama_produk ASC;";

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
            string query = @"
        SELECT p.id_produk, p.nama_produk, po.judul_po, p.harga_dasar, p.target_kuota, 
               COALESCE(SUM(td.jumlah_pesanan), 0) AS terisi, p.foto_produk
        FROM products p
        JOIN preorders po ON p.id_po = po.id_po
        LEFT JOIN transaction_details td ON p.id_produk = td.id_produk
        WHERE po.is_aktif = TRUE
          AND po.is_deleted = FALSE
          AND po.batas_waktu >= CURRENT_TIMESTAMP
          AND p.target_kuota IS NOT NULL
          AND p.is_deleted = FALSE
        GROUP BY p.id_produk, p.nama_produk, po.judul_po, p.harga_dasar, p.target_kuota, p.foto_produk
        HAVING (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) <= 10
           AND (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) > 0
        ORDER BY (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) ASC;";

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
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data Produk.
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// 
    /// Fungsi utama: Menarik data Produk dari DB ke objek Model di RAM 
    /// agar bisa dihitung logika bisnisnya (Harga Gotong Royong, Kuota).
    /// </summary>
    public class ProductRepository : IQueryRepository<Product>, ICommandRepository<Product>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public ProductRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<Product>
        // =======================================================

        public Product GetById(int id)
        {
            Product p = null;
            string query = @"
                SELECT id_produk, id_penjual, id_po, id_kategori, nama_produk, 
                       deskripsi, harga_dasar, harga_diskon, target_kuota, min_order 
                FROM products 
                WHERE id_produk = @id;";

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
                            // 1. Ambil data wajib untuk constructor Product
                            int idPenjual = reader.GetInt32(reader.GetOrdinal("id_penjual"));
                            int idKategori = reader.GetInt32(reader.GetOrdinal("id_kategori"));
                            string nama = reader.GetString(reader.GetOrdinal("nama_produk"));
                            int hargaDasar = reader.GetInt32(reader.GetOrdinal("harga_dasar"));

                            p = new Product(idPenjual, idKategori, nama, hargaDasar);
                            p.SetIdProduk(reader.GetInt32(reader.GetOrdinal("id_produk")));

                            // 2. Ambil data opsional (Boleh Null di DB)
                            if (!reader.IsDBNull(reader.GetOrdinal("id_po")))
                                p.SetIdPo(reader.GetInt32(reader.GetOrdinal("id_po")));

                            if (!reader.IsDBNull(reader.GetOrdinal("deskripsi")))
                                p.SetDeskripsi(reader.GetString(reader.GetOrdinal("deskripsi")));

                            if (!reader.IsDBNull(reader.GetOrdinal("harga_diskon")))
                                p.SetHargaDiskon(reader.GetInt32(reader.GetOrdinal("harga_diskon")));

                            if (!reader.IsDBNull(reader.GetOrdinal("target_kuota")))
                                p.SetTargetKuota(reader.GetInt32(reader.GetOrdinal("target_kuota")));

                            if (!reader.IsDBNull(reader.GetOrdinal("min_order")))
                                p.SetMinOrder(reader.GetInt32(reader.GetOrdinal("min_order")));
                        }
                    }
                }
            }
            return p;
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
                       u.nama AS nama_penjual, po.jenis_po,
                       p.target_kuota,
                       COALESCE((SELECT SUM(td.jumlah_pesanan)
                                 FROM transaction_details td
                                 JOIN transactions t ON td.id_transaksi = t.id_transaksi
                                 WHERE td.id_produk = p.id_produk
                                   AND t.status_pesanan NOT IN ('Batal', 'Gagal')), 0) AS terpesan
                FROM products p
                LEFT JOIN preorders   po  ON p.id_po       = po.id_po
                LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
                LEFT JOIN users       u   ON p.id_penjual  = u.id_user
                WHERE p.id_po IS NULL
                   OR (po.is_aktif = TRUE AND po.batas_waktu >= CURRENT_TIMESTAMP)
                ORDER BY po.batas_waktu ASC NULLS LAST;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetProdukLapak(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT p.id_produk, p.nama_produk, k.nama_kategori, po.judul_po, p.harga_dasar, p.target_kuota, p.foto_produk 
                FROM products p
                JOIN categories k ON p.id_kategori = k.id_kategori
                LEFT JOIN preorders po ON p.id_po = po.id_po
                WHERE p.id_penjual = @id
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
            List<Product> listProduk = new List<Product>();

            // REVISI: Penambahan p.foto_produk
            string query = @"
                SELECT p.id_produk, p.id_penjual, p.id_po, p.id_kategori, 
                       p.nama_produk, p.deskripsi, p.harga_dasar, p.harga_diskon, 
                       p.target_kuota, p.min_order, p.foto_produk, po.jenis_po
                FROM products p
                LEFT JOIN preorders po ON p.id_po = po.id_po
                ORDER BY p.nama_produk;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listProduk.Add(MappingReaderToProduct(reader));
                        }
                    }
                }
            }

            return listProduk;
        }
        public DataTable GetKatalogAktif(int limit = 100)
        {
            DataTable dtKatalog = new DataTable();
            string query = @"
                SELECT p.id_produk, p.nama_produk, kat.nama_kategori, po.judul_po,
                       p.harga_dasar, p.harga_diskon, po.batas_waktu, p.foto_produk,
                       u.nama AS nama_penjual, po.jenis_po,
                       p.target_kuota,
                       COALESCE((SELECT SUM(td.jumlah_pesanan)
                                 FROM transaction_details td
                                 JOIN transactions t ON td.id_transaksi = t.id_transaksi
                                 WHERE td.id_produk = p.id_produk
                                   AND t.status_pesanan NOT IN ('Batal', 'Gagal')), 0) AS terpesan
                FROM products p
                LEFT JOIN preorders   po  ON p.id_po       = po.id_po
                LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
                LEFT JOIN users       u   ON p.id_penjual  = u.id_user
                WHERE p.id_po IS NULL
                   OR (po.is_aktif = TRUE AND po.batas_waktu >= CURRENT_TIMESTAMP)
                ORDER BY po.batas_waktu ASC NULLS LAST
                LIMIT @limit;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtKatalog);
                    }
                }
            }
            return dtKatalog;
        }

        public DataTable GetProdukByPenjualDataTable(int idPenjual)
        {
            DataTable dt = new DataTable();
            // Tambahin p.foto_produk di sini juga
            string query = @"
                SELECT p.id_produk, p.nama_produk, c.nama_kategori, p.harga_dasar, p.target_kuota, po.judul_po, p.foto_produk
                FROM products p
                LEFT JOIN categories c ON p.id_kategori = c.id_kategori
                LEFT JOIN preorders po ON p.id_po = po.id_po
                WHERE p.id_penjual = @idPenjual
                ORDER BY p.nama_produk;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // =======================================================
        // IMPLEMENTASI ICommandRepository<Product>
        // =======================================================

        public void Insert(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity produk tidak boleh null.");
            }

            // REVISI: Penambahan kolom foto_produk
            string query = @"
                INSERT INTO products (id_penjual, id_po, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk) 
                VALUES (@penjual, @po, @kategori, @nama, @deskripsi, @hargaDasar, @hargaDiskon, @targetKuota, @minOrder, @foto);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    MappingProductToParameters(cmd, entity);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal menyimpan produk baru ke database.", "", "DB_INSERT_PRODUCT_FAILED");
                    }
                }
            }
        }

        public void Update(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity produk tidak boleh null.");
            }

            // REVISI: Penambahan kolom foto_produk
            string query = @"
                UPDATE products SET id_po = @po, id_kategori = @kategori, nama_produk = @nama, 
                deskripsi = @deskripsi, harga_dasar = @hargaDasar, harga_diskon = @hargaDiskon, 
                target_kuota = @targetKuota, min_order = @minOrder, foto_produk = @foto
                WHERE id_produk = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdProduk());
                    MappingProductToParameters(cmd, entity);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal mengupdate produk, ID tidak ditemukan.", "id_produk", "DB_UPDATE_PRODUCT_FAILED");
                    }
                }
            }
        }


        // =======================================================
        // HELPER METHOD UNTUK MENGHINDARI REDUNDANSI KODE (DRY)
        // =======================================================

        /// <summary>
        /// Method bantuan untuk memetakan data dari NpgsqlDataReader ke objek Product.
        /// Menghindari penulisan kode yang sama di GetById dan GetAll.
        /// </summary>
        private Product MappingReaderToProduct(NpgsqlDataReader reader)
        {
            int idPenjual = reader.GetInt32(reader.GetOrdinal("id_penjual"));
            int idKategori = reader.GetInt32(reader.GetOrdinal("id_kategori"));
            string namaProduk = reader.GetString(reader.GetOrdinal("nama_produk"));
            int hargaDasar = reader.GetInt32(reader.GetOrdinal("harga_dasar"));

            Product produk = new Product(idPenjual, idKategori, namaProduk, hargaDasar);
            produk.SetIdProduk(reader.GetInt32(reader.GetOrdinal("id_produk")));

            if (!reader.IsDBNull(reader.GetOrdinal("id_po")))
            {
                produk.SetIdPo(reader.GetInt32(reader.GetOrdinal("id_po")));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("deskripsi")))
            {
                produk.SetDeskripsi(reader.GetString(reader.GetOrdinal("deskripsi")));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("harga_diskon")))
            {
                produk.SetHargaDiskon(reader.GetInt32(reader.GetOrdinal("harga_diskon")));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("target_kuota")))
            {
                produk.SetTargetKuota(reader.GetInt32(reader.GetOrdinal("target_kuota")));
            }
            if (!reader.IsDBNull(reader.GetOrdinal("min_order")))
            {
                produk.SetMinOrder(reader.GetInt32(reader.GetOrdinal("min_order")));
            }

            // REVISI: Pembacaan BYTEA foto_produk dari Database
            if (!reader.IsDBNull(reader.GetOrdinal("foto_produk")))
            {
                byte[] fotoBytes = (byte[])reader["foto_produk"];
                produk.SetFotoProduk(fotoBytes);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("jenis_po")))
            {
                produk.SetJenisPo(reader.GetString(reader.GetOrdinal("jenis_po")));
            }
            else
            {
                produk.SetJenisPo("Biasa");
            }

            return produk;
        }

        /// <summary>
        /// Method bantuan untuk memetakan objek Product ke parameter NpgsqlCommand.
        /// Menghindari penulisan kode yang sama di Insert dan Update.
        /// </summary>
        private void MappingProductToParameters(NpgsqlCommand cmd, Product entity)
        {
            cmd.Parameters.AddWithValue("@penjual", entity.GetIdPenjual());
            cmd.Parameters.AddWithValue("@kategori", entity.GetIdKategori());
            cmd.Parameters.AddWithValue("@nama", entity.GetNamaProduk());
            cmd.Parameters.AddWithValue("@hargaDasar", entity.GetHargaDasar());

            // Handle nullable DB parameters
            cmd.Parameters.AddWithValue("@po", entity.GetIdPo().HasValue ? (object)entity.GetIdPo().Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@deskripsi", string.IsNullOrEmpty(entity.GetDeskripsi()) ? (object)DBNull.Value : entity.GetDeskripsi());
            cmd.Parameters.AddWithValue("@hargaDiskon", entity.GetHargaDiskon().HasValue ? (object)entity.GetHargaDiskon().Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@targetKuota", entity.GetTargetKuota() > 0 ? (object)entity.GetTargetKuota() : DBNull.Value);
            cmd.Parameters.AddWithValue("@minOrder", entity.GetMinOrder());

            // REVISI: Penyimpanan BYTEA foto_produk ke Database
            cmd.Parameters.AddWithValue("@foto", (object)entity.GetFotoProduk() ?? DBNull.Value);
        }


        // =======================================================
        // METHOD PRIVATE BANTUAN RAM (SUB-BAB 3.1 LAPORAN)
        // =======================================================

        /// <summary>
        /// Method khusus untuk mengisi jumlah pesanan yang sudah ada di DB 
        /// ke dalam properti In-Memory objek Product di RAM.
        /// Tanpa ini, perhitungan Kuota Gotong Royong di Model akan salah.
        /// </summary>
        private void IsiJumlahTerpesanDiRam(Product produk)
        {
            string query = "SELECT COALESCE(SUM(jumlah_pesanan), 0) FROM transaction_details WHERE id_produk = @idProduk;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduk", produk.GetIdProduk());
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int totalTerpesan = Convert.ToInt32(result);
                        // Sinkronisasi data DB ke RAM
                        produk.TambahPesanan(totalTerpesan);
                    }
                }
            }
        }
    }
}
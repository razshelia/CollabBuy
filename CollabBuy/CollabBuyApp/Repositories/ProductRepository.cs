using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;

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

        public Product GetById(int idProduk)
        {
            Product produk = null;

            // REVISI: Penambahan p.foto_produk
            string query = @"
                SELECT p.id_produk, p.id_penjual, p.id_po, p.id_kategori, 
                       p.nama_produk, p.deskripsi, p.harga_dasar, p.harga_diskon, 
                       p.target_kuota, p.min_order, p.foto_produk, po.jenis_po
                FROM products p
                LEFT JOIN preorders po ON p.id_po = po.id_po
                WHERE p.id_produk = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idProduk);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            produk = MappingReaderToProduct(reader);
                        }
                    }
                }
            }

            // === PENTING UNTUK RAM (Sub-bab 3.1 Laporan) ===
            if (produk != null && produk.GetTargetKuota() > 0)
            {
                IsiJumlahTerpesanDiRam(produk);
            }

            return produk;
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
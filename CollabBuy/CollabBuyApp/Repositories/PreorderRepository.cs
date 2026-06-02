using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class PreOrderRepository
    {
        private readonly string _connectionString;

        public PreOrderRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan!");
        }
        public DataTable GetById(int idPo)
        {
            DataTable dt = new DataTable();

            // Perhatikan alias info_rekening AS rekening agar sesuai dengan tarikan Controller
            string query = "SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening AS rekening, batas_waktu, is_aktif FROM preorders WHERE id_po = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        // Mengambil daftar sesi PO yang sedang aktif untuk User
        public DataTable GetSesiPOAktif(string keyword)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    po.id_po, 
                    po.judul_po AS nama_sesi, 
                    v.nama_toko, 
                    p.target_kuota AS kuota, 
                    COALESCE(SUM(td.jumlah_pesanan), 0) AS terisi, 
                    p.harga_dasar AS harga, 
                    po.batas_waktu AS deadline, 
                    po.is_aktif 
                FROM preorders po
                JOIN verifications v ON po.id_penjual = v.id_user
                JOIN products p ON po.id_po = p.id_po
                LEFT JOIN transaction_details td ON p.id_produk = td.id_produk
                WHERE po.is_aktif = TRUE
                  AND (po.judul_po ILIKE @keyword OR v.nama_toko ILIKE @keyword)
                GROUP BY po.id_po, po.judul_po, v.nama_toko, p.target_kuota, p.harga_dasar, po.batas_waktu, po.is_aktif
                ORDER BY po.batas_waktu ASC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        // Mengambil produk milik penjual yang belum dimasukkan ke sesi PO manapun
        public DataTable GetProdukTanpaPO(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_produk, nama_produk FROM products WHERE id_penjual = @id AND id_po IS NULL;";

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

        // Menyimpan sesi PO baru DAN mengupdate produk menggunakan Transaction agar aman (ACID)
        public bool InsertPOAndUpdateProduct(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert ke tabel preorders dan ambil id_po yang baru dibuat
                        string insertQuery = @"
                            INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif) 
                            VALUES (@penjual, @judul, @jenis, @rekening, @batas, TRUE) RETURNING id_po;";

                        int newIdPo = 0;
                        using (var cmdInsert = new NpgsqlCommand(insertQuery, conn, dbTx))
                        {
                            cmdInsert.Parameters.AddWithValue("@penjual", idPenjual);
                            cmdInsert.Parameters.AddWithValue("@judul", judul);
                            cmdInsert.Parameters.AddWithValue("@jenis", jenis);
                            cmdInsert.Parameters.AddWithValue("@rekening", rekening);
                            cmdInsert.Parameters.AddWithValue("@batas", batasWaktu);
                            newIdPo = (int)cmdInsert.ExecuteScalar();
                        }

                        // 2. Update id_po dan target_kuota di tabel products
                        string updateQuery = "UPDATE products SET id_po = @idPo, target_kuota = @kuota WHERE id_produk = @idProduk;";
                        using (var cmdUpdate = new NpgsqlCommand(updateQuery, conn, dbTx))
                        {
                            cmdUpdate.Parameters.AddWithValue("@idPo", newIdPo);
                            cmdUpdate.Parameters.AddWithValue("@kuota", targetKuota);
                            cmdUpdate.Parameters.AddWithValue("@idProduk", idProduk);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        dbTx.Commit();
                        return true;
                    }
                    catch
                    {
                        dbTx.Rollback();
                        throw; // Lempar ke controller agar bisa ditangkap catch blok
                    }
                }
            }
        }
    }
}
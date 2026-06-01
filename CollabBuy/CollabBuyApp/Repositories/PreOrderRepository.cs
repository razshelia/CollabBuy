using System;
using System.Configuration;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class PreOrderRepository
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public PreOrderRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan!");
            }
            else
            {
                this._connectionString = connStr;
            }
        }

        /// <summary>
        /// Mengambil data satu sesi PO spesifik berdasarkan ID untuk di-map ke objek Model.
        /// </summary>
        public Models.PreOrder GetById(int idPo)
        {
            Models.PreOrder po = null;
            string query = "SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif FROM preorders WHERE id_po = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idPenjual = reader.GetInt32(1);
                            string judul = reader.GetString(2);
                            string jenis = reader.GetString(3);
                            string rekening = reader.GetString(4);
                            DateTime batas = reader.GetDateTime(5);
                            bool isAktif = reader.GetBoolean(6);

                            po = new Models.PreOrder(idPenjual, judul, jenis, rekening, batas);
                            po.SetIdPo(reader.GetInt32(0));

                            if (isAktif == false)
                            {
                                po.UbahStatus("Tutup");
                            }
                            else
                            {
                                bool tetapAktif = true; // Penugasan nyata menghindari else kosong
                            }
                        }
                        else
                        {
                            po = null;
                        }
                    }
                }
            }
            return po;
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

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Mengambil produk milik penjual yang belum dimasukkan ke sesi PO manapun
        public DataTable GetProdukTanpaPO(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_produk, nama_produk FROM products WHERE id_penjual = @id AND id_po IS NULL;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPenjual);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Menyimpan sesi PO baru DAN mengupdate produk menggunakan Transaction agar aman (ACID)
        public bool InsertPOAndUpdateProduct(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            bool hasilEksekusi;
            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = @"
                            INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif) 
                            VALUES (@penjual, @judul, @jenis, @rekening, @batas, TRUE) RETURNING id_po;";

                        int newIdPo = 0;
                        using (NpgsqlCommand cmdInsert = new NpgsqlCommand(insertQuery, conn, dbTx))
                        {
                            cmdInsert.Parameters.AddWithValue("@penjual", idPenjual);
                            cmdInsert.Parameters.AddWithValue("@judul", judul);
                            cmdInsert.Parameters.AddWithValue("@jenis", jenis);
                            cmdInsert.Parameters.AddWithValue("@rekening", rekening);
                            cmdInsert.Parameters.AddWithValue("@batas", batasWaktu);
                            newIdPo = (int)cmdInsert.ExecuteScalar();
                        }

                        string updateQuery = "UPDATE products SET id_po = @idPo, target_kuota = @kuota WHERE id_produk = @idProduk;";
                        using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(updateQuery, conn, dbTx))
                        {
                            cmdUpdate.Parameters.AddWithValue("@idPo", newIdPo);
                            cmdUpdate.Parameters.AddWithValue("@kuota", targetKuota);
                            cmdUpdate.Parameters.AddWithValue("@idProduk", idProduk);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        dbTx.Commit();
                        hasilEksekusi = true;
                    }
                    catch
                    {
                        dbTx.Rollback();
                        throw;
                    }
                }
            }
            return hasilEksekusi;
        }
    }
}
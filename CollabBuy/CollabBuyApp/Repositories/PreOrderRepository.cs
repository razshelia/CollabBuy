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
            string query = "SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening AS rekening, batas_waktu, is_aktif FROM preorders WHERE id_po = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetSesiPOAktif(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM fn_sesi_po_aktif(@keyword);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", keyword ?? "");
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetSemuaProdukAktif(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT id_produk, nama_produk
                FROM products
                WHERE id_penjual = @id
                  AND is_deleted = FALSE
                ORDER BY nama_produk ASC;";

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
        public int InsertPOSaja(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string insertQuery = @"
                    INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif, is_deleted)
                    VALUES (@penjual, @judul, @jenis, @rekening, @batas, TRUE, FALSE)
                    RETURNING id_po;";
                using (var cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@penjual", idPenjual);
                    cmd.Parameters.AddWithValue("@judul", judul);
                    cmd.Parameters.AddWithValue("@jenis", jenis);
                    cmd.Parameters.AddWithValue("@rekening", rekening);
                    cmd.Parameters.AddWithValue("@batas", batasWaktu);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
        public bool InsertPOAndUpdateProduct(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = @"
                            INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif, is_deleted) 
                            VALUES (@penjual, @judul, @jenis, @rekening, @batas, TRUE, FALSE) RETURNING id_po;";

                        int newIdPo;
                        using (var cmdInsert = new NpgsqlCommand(insertQuery, conn, dbTx))
                        {
                            cmdInsert.Parameters.AddWithValue("@penjual", idPenjual);
                            cmdInsert.Parameters.AddWithValue("@judul", judul);
                            cmdInsert.Parameters.AddWithValue("@jenis", jenis);
                            cmdInsert.Parameters.AddWithValue("@rekening", rekening);
                            cmdInsert.Parameters.AddWithValue("@batas", batasWaktu);
                            newIdPo = (int)cmdInsert.ExecuteScalar();
                        }

                        
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
                        throw; 
                    }
                }
            }
        }
        public bool SoftDeletePO(int idPo)
        {
            string query = "UPDATE preorders SET is_aktif = FALSE, is_deleted = TRUE WHERE id_po = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool UpdatePO(int idPo, string judulBaru, string jenisBaru, string rekeningBaru, DateTime batasWaktuBaru)
        {
            string query = @"
        UPDATE preorders 
        SET judul_po = @judul, jenis_po = @jenis, info_rekening = @rekening, batas_waktu = @batas
        WHERE id_po = @id AND is_deleted = FALSE;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    cmd.Parameters.AddWithValue("@judul", judulBaru);
                    cmd.Parameters.AddWithValue("@jenis", jenisBaru);
                    cmd.Parameters.AddWithValue("@rekening", rekeningBaru);
                    cmd.Parameters.AddWithValue("@batas", batasWaktuBaru);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public DataTable GetPOByPenjual(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM fn_po_by_penjual(@id, FALSE);";

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

        /// <summary>
        /// Khusus untuk dropdown form tambah produk — hanya PO yang masih aktif dan belum lewat batas waktu.
        /// </summary>
        public DataTable GetPOAktifByPenjual(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM fn_po_by_penjual(@id, TRUE);";

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
    }
}

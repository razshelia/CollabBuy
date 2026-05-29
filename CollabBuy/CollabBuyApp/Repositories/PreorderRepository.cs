using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data PreOrder / Sesi PO.
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// </summary>
    public class PreOrderRepository : IQueryRepository<PreOrder>, ICommandRepository<PreOrder>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public PreOrderRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<PreOrder>
        // =======================================================

        public PreOrder GetById(int idPo)
        {
            PreOrder po = null;

            string query = "SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif FROM preorders WHERE id_po = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idPenjual = reader.GetInt32(reader.GetOrdinal("id_penjual"));
                            string judulPo = reader.GetString(reader.GetOrdinal("judul_po"));
                            string jenisPo = reader.GetString(reader.GetOrdinal("jenis_po"));
                            string infoRekening = reader.GetString(reader.GetOrdinal("info_rekening"));
                            DateTime batasWaktu = reader.GetDateTime(reader.GetOrdinal("batas_waktu"));

                            po = new PreOrder(idPenjual, judulPo, jenisPo, infoRekening, batasWaktu);
                            po.SetIdPo(reader.GetInt32(reader.GetOrdinal("id_po")));

                            // Cek status aktif
                            if (reader.GetBoolean(reader.GetOrdinal("is_aktif")) == false)
                            {
                                po.UbahStatus("Tutup");
                            }
                        }
                    }
                }
            }
            return po;
        }

        public List<PreOrder> GetAll()
        {
            List<PreOrder> listPo = new List<PreOrder>();
            string query = "SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif FROM preorders ORDER BY batas_waktu DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPenjual = reader.GetInt32(reader.GetOrdinal("id_penjual"));
                            string judulPo = reader.GetString(reader.GetOrdinal("judul_po"));
                            string jenisPo = reader.GetString(reader.GetOrdinal("jenis_po"));
                            string infoRekening = reader.GetString(reader.GetOrdinal("info_rekening"));
                            DateTime batasWaktu = reader.GetDateTime(reader.GetOrdinal("batas_waktu"));

                            PreOrder po = new PreOrder(idPenjual, judulPo, jenisPo, infoRekening, batasWaktu);
                            po.SetIdPo(reader.GetInt32(reader.GetOrdinal("id_po")));

                            if (reader.GetBoolean(reader.GetOrdinal("is_aktif")) == false)
                            {
                                po.UbahStatus("Tutup");
                            }
                            listPo.Add(po);
                        }
                    }
                }
            }
            return listPo;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<PreOrder>
        // =======================================================

        public void Insert(PreOrder entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity PO tidak boleh null.");

            string query = "INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif) VALUES (@penjual, @judul, @jenis, @rekening, @batas, @aktif);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@penjual", entity.GetIdPenjual());
                    cmd.Parameters.AddWithValue("@judul", entity.GetJudulPo());
                    cmd.Parameters.AddWithValue("@jenis", entity.GetJenisPo());
                    cmd.Parameters.AddWithValue("@rekening", entity.GetInfoRekening());
                    cmd.Parameters.AddWithValue("@batas", entity.GetBatasWaktu());
                    cmd.Parameters.AddWithValue("@aktif", entity.GetStatus() == "Aktif");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal menyimpan PO baru ke database.", "", "DB_INSERT_PO_FAILED");
                    }
                }
            }
        }

        public void Update(PreOrder entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity PO tidak boleh null.");

            string query = "UPDATE preorders SET judul_po = @judul, is_aktif = @aktif WHERE id_po = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdPo());
                    cmd.Parameters.AddWithValue("@judul", entity.GetJudulPo());
                    cmd.Parameters.AddWithValue("@aktif", entity.GetStatus() == "Aktif");

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // =======================================================
        // METHOD KHUSUS (STORED PROCEDURE DB)
        // =======================================================

        /// <summary>
        /// Memanggil Stored Procedure sp_update_status_massal_po.
        /// Mengubah status pesanan secara massal berdasarkan ID PO.
        /// </summary>
        public void UpdateStatusMassal(int idPo, string statusBaru)
        {
            string query = "CALL sp_update_status_massal_po(@idPo, @statusBaru);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPo", idPo);
                    cmd.Parameters.AddWithValue("@statusBaru", statusBaru);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
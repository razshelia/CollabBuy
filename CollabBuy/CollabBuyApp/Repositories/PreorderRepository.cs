using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class PreOrderRepository : IQueryRepository<PreOrder>, ICommandRepository<PreOrder>
    {
        private readonly string _connectionString;

        public PreOrderRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan!");
        }

        // =======================================================
        // IQueryRepository (READ)
        // =======================================================

        public PreOrder GetById(int idPo)
        {
            PreOrder po = null;
            string query = "SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif FROM preorders WHERE id_po = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            po = new PreOrder(reader.GetInt32(reader.GetOrdinal("id_penjual")),
                                              reader.GetString(reader.GetOrdinal("judul_po")),
                                              reader.GetString(reader.GetOrdinal("jenis_po")),
                                              reader.GetString(reader.GetOrdinal("info_rekening")),
                                              reader.GetDateTime(reader.GetOrdinal("batas_waktu")));
                            po.SetIdPo(reader.GetInt32(reader.GetOrdinal("id_po")));
                            if (!reader.GetBoolean(reader.GetOrdinal("is_aktif"))) po.UbahStatus("Tutup");
                        }
                    }
                }
            }
            return po;
        }

        public List<PreOrder> GetAll()
        {
            // Bisa digunakan untuk keperluan internal admin jika perlu
            return new List<PreOrder>();
        }

        public DataTable GetPoByPenjual(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_po, judul_po, jenis_po, batas_waktu, is_aktif, info_rekening FROM preorders WHERE id_penjual = @id ORDER BY batas_waktu DESC;";

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

        // =======================================================
        // ICommandRepository & COMMANDS (WRITE)
        // =======================================================

        /// <summary>
        /// Menggunakan Insert sesuai kontrak ICommandRepository.
        /// </summary>
        public void Insert(PreOrder entity)
        {
            string query = "INSERT INTO preorders (id_penjual, judul_po, jenis_po, batas_waktu, is_aktif, info_rekening) VALUES (@penjual, @judul, @jenis, @batas, TRUE, @rekening);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@penjual", entity.GetIdPenjual());
                    cmd.Parameters.AddWithValue("@judul", entity.GetJudulPo());
                    cmd.Parameters.AddWithValue("@jenis", entity.GetJenisPo());
                    cmd.Parameters.AddWithValue("@batas", entity.GetBatasWaktu());
                    cmd.Parameters.AddWithValue("@rekening", entity.GetInfoRekening());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(PreOrder entity)
        {
            string query = "UPDATE preorders SET judul_po = @judul, is_aktif = @aktif WHERE id_po = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdPo());
                    cmd.Parameters.AddWithValue("@judul", entity.GetJudulPo());
                    cmd.Parameters.AddWithValue("@aktif", entity.GetStatus() == "Aktif");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Stored Procedure Massal (Tidak tumpang tindih dengan Update biasa).
        /// </summary>
        public void UpdateStatusMassal(int idPo, string statusBaru)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("CALL sp_update_status_massal_po(@idPo, @status);", conn))
                {
                    cmd.Parameters.AddWithValue("@idPo", idPo);
                    cmd.Parameters.AddWithValue("@status", statusBaru);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void TutupSesiPo(int idPo)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE preorders SET is_aktif = FALSE WHERE id_po = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPo);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
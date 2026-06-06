using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ComplaintRepository : IQueryRepository<Complaint>, ICommandRepository<Complaint>
    {
        private readonly string _connectionString;

        public ComplaintRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan!");
        }

        public Complaint GetById(int idAduan)
        {
            Complaint aduan = null;
            string query = "SELECT id_aduan, id_user, subjek, deskripsi, tanggal, is_selesai, balasan FROM complaints WHERE id_aduan = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idAduan);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            aduan = new Complaint(reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
                            aduan.IdAduan = reader.GetInt32(0);

                            if (!reader.IsDBNull(4))
                            {
                                aduan.TanggalAduan = reader.GetDateTime(4);
                            }
                            if (reader.GetBoolean(5))
                            {
                                aduan.Status = "Selesai";
                            }
                            if (!reader.IsDBNull(6))
                            {
                                aduan.TanggapanAdmin = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            return aduan;
        }

        public List<Complaint> GetAll()
        {
            return new List<Complaint>(); // Bisa diimplementasikan jika Admin butuh list object
        }

        public DataTable GetRiwayatByUser(int idUser)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM fn_riwayat_aduan_user(@id);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public void Insert(Complaint entity)
        {
            string query = "INSERT INTO complaints (id_user, subjek, deskripsi) VALUES (@idUser, @subjek, @deskripsi);";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", entity.IdUser);
                    cmd.Parameters.AddWithValue("@subjek", entity.JenisAduan);
                    cmd.Parameters.AddWithValue("@deskripsi", entity.Deskripsi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Complaint entity)
        {
            string query = "UPDATE complaints SET is_selesai = @isSelesai, balasan = @balasan WHERE id_aduan = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.IdAduan);
                    cmd.Parameters.AddWithValue("@isSelesai", entity.Status?.ToLower() == "selesai");
                    cmd.Parameters.AddWithValue("@balasan", string.IsNullOrEmpty(entity.TanggapanAdmin) ? (object)DBNull.Value : entity.TanggapanAdmin);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetPendingAduan()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM vw_aduan_pending;";

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

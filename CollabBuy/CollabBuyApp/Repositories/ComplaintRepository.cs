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
                            aduan.SetIdAduan(reader.GetInt32(0));
                            if (!reader.IsDBNull(6)) aduan.BeriTanggapan(reader.GetString(6));
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

        // --- METHOD TAMBAHAN UNTUK UI KENDALA ---
        public DataTable GetRiwayatByUser(int idUser)
        {
            DataTable dt = new DataTable();
            string query = "SELECT subjek, deskripsi, tanggal, is_selesai, balasan FROM complaints WHERE id_user = @id ORDER BY tanggal DESC;";
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
                    cmd.Parameters.AddWithValue("@idUser", entity.GetIdUser());
                    cmd.Parameters.AddWithValue("@subjek", entity.GetSubjek());
                    cmd.Parameters.AddWithValue("@deskripsi", entity.GetDeskripsi());
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
                    cmd.Parameters.AddWithValue("@id", entity.GetIdAduan());
                    cmd.Parameters.AddWithValue("@isSelesai", entity.IsSelesai());
                    cmd.Parameters.AddWithValue("@balasan", entity.GetTanggapan() ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable GetPendingAduan()
        {
            DataTable dt = new DataTable();
            // Ambil aduan yang is_selesai = false, gabungkan dengan nama pelapor
            string query = @"
        SELECT c.id_aduan, c.id_user, u.nama AS nama_pelapor, c.subjek, c.deskripsi, c.tanggal 
        FROM complaints c 
        JOIN users u ON c.id_user = u.id_user 
        WHERE c.is_selesai = FALSE 
        ORDER BY c.tanggal ASC;";

            using (var conn = new Npgsql.NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    using (var da = new Npgsql.NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data Aduan (Complaint).
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// </summary>
    public class ComplaintRepository : IQueryRepository<Complaint>, ICommandRepository<Complaint>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public ComplaintRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<Complaint>
        // =======================================================

        public Complaint GetById(int idAduan)
        {
            Complaint aduan = null;

            string query = "SELECT id_aduan, id_user, subjek, deskripsi, tanggal, is_selesai, balasan FROM complaints WHERE id_aduan = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idAduan);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idUser = reader.GetInt32(reader.GetOrdinal("id_user"));
                            string subjek = reader.GetString(reader.GetOrdinal("subjek"));
                            string deskripsi = reader.GetString(reader.GetOrdinal("deskripsi"));

                            aduan = new Complaint(idUser, subjek, deskripsi);
                            aduan.SetIdAduan(reader.GetInt32(reader.GetOrdinal("id_aduan")));

                            // Pemetaan Interface IResolvable dari DB ke RAM
                            if (!reader.IsDBNull(reader.GetOrdinal("balasan")))
                            {
                                string balasanDb = reader.GetString(reader.GetOrdinal("balasan"));
                                aduan.BeriTanggapan(balasanDb); // Ini akan otomatis set is_selesai = true di Model
                            }
                        }
                    }
                }
            }
            return aduan;
        }

        public List<Complaint> GetAll()
        {
            List<Complaint> listAduan = new List<Complaint>();
            string query = "SELECT id_aduan, id_user, subjek, deskripsi, tanggal, is_selesai, balasan FROM complaints ORDER BY tanggal DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idUser = reader.GetInt32(reader.GetOrdinal("id_user"));
                            string subjek = reader.GetString(reader.GetOrdinal("subjek"));
                            string deskripsi = reader.GetString(reader.GetOrdinal("deskripsi"));

                            Complaint aduan = new Complaint(idUser, subjek, deskripsi);
                            aduan.SetIdAduan(reader.GetInt32(reader.GetOrdinal("id_aduan")));

                            if (!reader.IsDBNull(reader.GetOrdinal("balasan")))
                            {
                                aduan.BeriTanggapan(reader.GetString(reader.GetOrdinal("balasan")));
                            }

                            listAduan.Add(aduan);
                        }
                    }
                }
            }
            return listAduan;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<Complaint>
        // =======================================================

        public void Insert(Complaint entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity aduan tidak boleh null.");

            string query = "INSERT INTO complaints (id_user, subjek, deskripsi) VALUES (@idUser, @subjek, @deskripsi);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", entity.GetIdUser());
                    cmd.Parameters.AddWithValue("@subjek", entity.GetSubjek());
                    cmd.Parameters.AddWithValue("@deskripsi", entity.GetDeskripsi());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal menyimpan aduan ke database.", "", "DB_INSERT_COMPLAINT_FAILED");
                    }
                }
            }
        }

        public void Update(Complaint entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity aduan tidak boleh null.");

            // Update ini biasanya dipanggil saat Admin memberikan tanggapan (BeriTanggapan)
            string query = "UPDATE complaints SET is_selesai = @isSelesai, balasan = @balasan WHERE id_aduan = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.GetIdAduan());
                    cmd.Parameters.AddWithValue("@isSelesai", entity.IsSelesai());
                    cmd.Parameters.AddWithValue("@balasan", string.IsNullOrEmpty(entity.GetTanggapan()) ? (object)DBNull.Value : entity.GetTanggapan());

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
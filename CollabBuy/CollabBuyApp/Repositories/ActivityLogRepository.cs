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
    /// Repository untuk mengakses data Log Aktivitas (Audit Trail).
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// 
    /// Catatan Penting (Business Rule):
    /// Log aktivitas bersifat IMMUTABLE (hanya bisa ditambah, tidak boleh diubah/dihapus).
    /// Method Update() akan melempar exception untuk menjaga integritas audit.
    /// </summary>
    public class ActivityLogRepository : IQueryRepository<ActivityLog>, ICommandRepository<ActivityLog>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public ActivityLogRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<ActivityLog>
        // =======================================================

        public ActivityLog GetById(int idLog)
        {
            ActivityLog log = null;

            string query = "SELECT id_log, id_user, aktivitas, waktu_akses FROM activity_logs WHERE id_log = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idLog);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idUser = reader.GetInt32(reader.GetOrdinal("id_user"));
                            string aktivitas = reader.GetString(reader.GetOrdinal("aktivitas"));

                            log = new ActivityLog(idUser, aktivitas);
                            log.SetIdLog(reader.GetInt32(reader.GetOrdinal("id_log")));
                        }
                    }
                }
            }
            return log;
        }

        public List<ActivityLog> GetAll()
        {
            List<ActivityLog> listLog = new List<ActivityLog>();
            // Urutkan dari yang paling baru (wajib untuk tampilan Audit Trail)
            string query = "SELECT * FROM vw_log_aktivitas ORDER BY waktu_akses DESC LIMIT 50;";

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
                            string aktivitas = reader.GetString(reader.GetOrdinal("aktivitas"));

                            ActivityLog log = new ActivityLog(idUser, aktivitas);
                            log.SetIdLog(reader.GetInt32(reader.GetOrdinal("id_log")));

                            listLog.Add(log);
                        }
                    }
                }
            }
            return listLog;
        }

        public DataTable GetAllAsDataTable()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            al.id_log,
            u.nama     AS pelaku,
            u.peran,
            al.aktivitas,
            al.waktu_akses
        FROM activity_logs al
        JOIN users u ON al.id_user = u.id_user
        ORDER BY al.waktu_akses DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        // =======================================================
        // IMPLEMENTASI ICommandRepository<ActivityLog>
        // =======================================================

        public void Insert(ActivityLog entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity log tidak boleh null.");

            string query = "INSERT INTO activity_logs (id_user, aktivitas) VALUES (@idUser, @aktivitas);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", entity.GetIdUser());
                    cmd.Parameters.AddWithValue("@aktivitas", entity.GetAktivitas());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal menyimpan log aktivitas ke database.", "", "DB_INSERT_LOG_FAILED");
                    }
                }
            }
        }

        public void Update(ActivityLog entity)
        {
            // BUKTI BUSINESS RULE (Sub-bab 3.2 Laporan)
            // Log aktivitas adalah catatan sejarah sistem (Audit Trail). 
            // Dilarang keras mengubah isi log yang sudah masuk demi keamanan data.
            throw new NotSupportedException("Log aktivitas tidak boleh diubah demi integritas Audit Trail!");
        }
    }
}
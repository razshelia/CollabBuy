using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ActivityLogRepository : IQueryRepository<ActivityLog>, ICommandRepository<ActivityLog>
    {
        private readonly string _connectionString;

        public ActivityLogRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
        }

        public ActivityLog GetById(int idLog)
        {
            ActivityLog log = null;
            string query = "SELECT id_log, id_user, aktivitas, waktu_akses FROM activity_logs WHERE id_log = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idLog);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            log = new ActivityLog(reader.GetInt32(reader.GetOrdinal("id_user")), reader.GetString(reader.GetOrdinal("aktivitas")));
                            log.IdLog = reader.GetInt32(reader.GetOrdinal("id_log"));
                            if (!reader.IsDBNull(reader.GetOrdinal("waktu_akses")))
                            {
                                log.WaktuAkses = reader.GetDateTime(reader.GetOrdinal("waktu_akses"));
                            }
                        }
                    }
                }
            }
            return log;
        }

        public List<ActivityLog> GetAll()
        {
            var listLog = new List<ActivityLog>();
            string query = "SELECT id_log, id_user, aktivitas, waktu_akses FROM activity_logs ORDER BY waktu_akses DESC LIMIT 100;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var log = new ActivityLog(reader.GetInt32(reader.GetOrdinal("id_user")), reader.GetString(reader.GetOrdinal("aktivitas")));
                        log.IdLog = reader.GetInt32(reader.GetOrdinal("id_log"));
                        if (!reader.IsDBNull(reader.GetOrdinal("waktu_akses")))
                        {
                            log.WaktuAkses = reader.GetDateTime(reader.GetOrdinal("waktu_akses"));
                        }
                        listLog.Add(log);
                    }
                }
            }
            return listLog;
        }

        public DataTable GetAllAsDataTable()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM vw_activity_log;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Filter log aktivitas berdasarkan id_user tertentu.
        /// Menggunakan vw_log_aktivitas yang memiliki kolom id_user.
        /// </summary>
        public DataTable GetByUser(int idUser)
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT id_log, pelaku, peran, aktivitas, waktu_akses
        FROM   vw_log_aktivitas
        WHERE  id_user = @idUser
        ORDER  BY waktu_akses DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", idUser);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Mengambil N aktivitas terbaru dari seluruh sistem.
        /// Implementasi SQL Section 8D: Advanced Logging System.
        /// </summary>
        public DataTable GetLogTerkini(int limit = 10)
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT pelaku, aktivitas, waktu_akses
        FROM   vw_log_aktivitas
        ORDER  BY waktu_akses DESC
        LIMIT  @limit;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public void Insert(ActivityLog entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity log tidak boleh null.");

            string query = "INSERT INTO activity_logs (id_user, aktivitas) VALUES (@idUser, @aktivitas);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", entity.IdUser);
                    cmd.Parameters.AddWithValue("@aktivitas", entity.Aktivitas);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOrderException("Gagal menyimpan log aktivitas.", "", "DB_INSERT_LOG_FAILED");
                }
            }
        }

        public void Update(ActivityLog entity)
        {
            throw new NotSupportedException("Log aktivitas tidak boleh diubah demi integritas Audit Trail!");
        }
    }
}

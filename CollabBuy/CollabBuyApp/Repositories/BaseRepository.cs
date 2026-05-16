using System;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DatabaseHelper _db;

        protected BaseRepository()
        {
            _db = new DatabaseHelper();
        }

        protected void ExecuteQuery(string sql, Action<NpgsqlCommand> setParams, Action<NpgsqlDataReader> processReader)
        {
            using (var conn = _db.AmbilKoneksi())
            {
                if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        setParams?.Invoke(cmd);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) processReader(reader);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception($"Gagal eksekusi database: {ex.Message}", ex); }
            }
        }

        protected int ExecuteNonQuery(string sql, Action<NpgsqlCommand> setParams)
        {
            using (var conn = _db.AmbilKoneksi())
            {
                if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        setParams?.Invoke(cmd);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) { throw new Exception($"Gagal menyimpan data: {ex.Message}", ex); }
            }
        }

        protected object ExecuteScalar(string sql, Action<NpgsqlCommand> setParams)
        {
            using (var conn = _db.AmbilKoneksi())
            {
                if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        setParams?.Invoke(cmd);
                        return cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex) { throw new Exception($"Gagal mengambil data tunggal: {ex.Message}", ex); }
            }
        }
    }
}
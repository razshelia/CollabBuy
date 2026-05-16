using System;
using System.Configuration;
using Npgsql;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            try
            {
                // Catatan: Pastikan penulisan "CollabBuyDB" sama persis besar-kecilnya dengan yang di App.config
                var connStringSetting = ConfigurationManager.ConnectionStrings["CollabBuyDB"];
                if (connStringSetting == null || string.IsNullOrWhiteSpace(connStringSetting.ConnectionString))
                    throw new Exception("Connection string 'CollabBuyDB' tidak ditemukan di App.config.");

                _connectionString = connStringSetting.ConnectionString;
            }
            catch (Exception ex)
            {
                _connectionString = null;
                System.Diagnostics.Debug.WriteLine("Gagal membaca connection string: " + ex.Message);
            }
        }

        public NpgsqlConnection AmbilKoneksi()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                // Lempar error, jangan pakai UXHelper di sini
                throw new Exception("Konfigurasi database tidak ditemukan. Hubungi administrator.");
            }

            try
            {
                return new NpgsqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                // Lempar error ke atas
                throw new Exception("Gagal membuat koneksi ke database: " + ex.Message);
            }
        }
    }
}
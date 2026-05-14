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
                // Baca dari App.config
                var connStringSetting = ConfigurationManager.ConnectionStrings["CollabBuyDB"];
                if (connStringSetting == null || string.IsNullOrWhiteSpace(connStringSetting.ConnectionString))
                    throw new InvalidOperationException("Connection string 'CollabBuyDB' tidak ditemukan di App.config.");

                _connectionString = connStringSetting.ConnectionString;
            }
            catch (Exception ex)
            {
                // Jika terjadi error fatal, bisa log atau lempar exception
                // UXHelper akan digunakan untuk menampilkan pesan, namun karena ini constructor,
                // kita simpan error dan biarkan pemanggil mengecek.
                _connectionString = null;
                System.Diagnostics.Debug.WriteLine("Gagal membaca connection string: " + ex.Message);
            }
        }

        public NpgsqlConnection AmbilKoneksi()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                UXHelper.TampilkanError("Konfigurasi database tidak ditemukan. Hubungi administrator.");
                return null;
            }

            try
            {
                var koneksi = new NpgsqlConnection(_connectionString);
                return koneksi;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal membuat koneksi ke database: " + ex.Message);
                return null;
            }
        }
    }
}
using Npgsql;
using System;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public class DatabaseHelper
    {
        private string stringKoneksi;

        public DatabaseHelper()
        {
            try
            {
                this.stringKoneksi = ConfigurationManager.ConnectionStrings["CollabBuyDBConfig"]?.ConnectionString;

                if (string.IsNullOrWhiteSpace(this.stringKoneksi))
                {
                    // FALLBACK: gunakan connection string hardcoded jika App.config tidak ditemukan
                    this.stringKoneksi = "Host=localhost;Port=5432;Database=collabbuy_db;Username=postgres;Password=admin123";
                }
            }
            catch (Exception)
            {
                // FALLBACK saat ConfigurationManager gagal total
                this.stringKoneksi = "Host=localhost;Port=5432;Database=collabbuy_db;Username=postgres;Password=admin123";
            }
        }

        public NpgsqlConnection AmbilKoneksi()
        {
            try
            {
                NpgsqlConnection koneksi = new NpgsqlConnection(this.stringKoneksi);
                return koneksi;
            }
            catch (Exception ex)
            {
                // ERROR HANDLING: kembalikan null, UI yang menangani
                System.Diagnostics.Debug.WriteLine("Gagal membuat koneksi: " + ex.Message);
                return null;
            }
        }
    }
}
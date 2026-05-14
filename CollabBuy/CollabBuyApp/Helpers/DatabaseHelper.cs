using Npgsql;
using System;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public class DatabaseHelper
    {
        // ENKAPSULASI: Konfigurasi koneksi tertutup rapat
        private string stringKoneksi;

        public DatabaseHelper()
        {
            // ENKAPSULASI & SECURITY: Mengambil koneksi dengan aman dari App.config
            this.stringKoneksi = ConfigurationManager.ConnectionStrings["CollabBuyDBConfig"].ConnectionString;
        }

        public NpgsqlConnection AmbilKoneksi()
        {
            try
            {
                NpgsqlConnection koneksi = new NpgsqlConnection(this.stringKoneksi);
                return koneksi;
            }
            catch (Exception)
            {
                // ERROR HANDLING: Mengembalikan null jika gagal.
                // Nantinya di layer UI tinggal diatur: if (conn == null) { tampilkan_pesan_ramah_server_down }
                return null;
            }
        }
    }
}
using System;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ReportRepository
    {
        private DatabaseHelper dbHelper;

        public ReportRepository()
        {
            this.dbHelper = new DatabaseHelper();
        }

        // 1. CUBE: Analisis Kombinasi Penjual & Produk
        public DataTable AmbilLaporanOmzetCube()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return dt;

            try
            {
                koneksi.Open();
                string sql = @"
                    SELECT 
                        COALESCE(v.nama_toko, 'Semua Toko') AS nama_toko,
                        COALESCE(p.nama_produk, 'Semua Produk') AS nama_produk,
                        SUM(c.total_bayar_awal) AS omzet
                    FROM checkouts c
                    JOIN preorders po ON c.id_po = po.id_po
                    JOIN products p ON po.id_produk = p.id_produk
                    LEFT JOIN verifications v ON p.id_seller = v.id_user
                    GROUP BY CUBE(v.nama_toko, p.nama_produk)
                    ORDER BY nama_toko, nama_produk";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            catch (Exception)
            {
                // Gagal senyap
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }

            return dt;
        }

        // 2. ROLLUP: Penjualan per Fakultas ke Prodi
        public DataTable AmbilLaporanFakultasRollup()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return dt;

            try
            {
                koneksi.Open();
                string sql = @"
                    SELECT 
                        COALESCE(u.fakultas, 'Semua Fakultas') AS fakultas,
                        COALESCE(u.prodi, 'Semua Prodi') AS prodi,
                        SUM(c.total_bayar_awal) AS belanja
                    FROM checkouts c
                    JOIN users u ON c.id_user_coordinator = u.id_user
                    GROUP BY ROLLUP(u.fakultas, u.prodi)
                    ORDER BY fakultas, prodi";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            catch (Exception)
            {
                // Gagal senyap
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }

            return dt;
        }

        // 3. GROUPING SETS: Perbandingan Omset Toko dan Total Belanja Fakultas
        public DataTable AmbilLaporanPerbandinganGroupingSets()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return dt;

            try
            {
                koneksi.Open();
                string sql = @"
                    SELECT 
                        COALESCE(v.nama_toko, '') AS nama_toko,
                        COALESCE(u.fakultas, '') AS fakultas,
                        SUM(c.total_bayar_awal) AS total
                    FROM checkouts c
                    JOIN users u ON c.id_user_coordinator = u.id_user
                    JOIN preorders po ON c.id_po = po.id_po
                    JOIN products p ON po.id_produk = p.id_produk
                    LEFT JOIN verifications v ON p.id_seller = v.id_user
                    GROUP BY GROUPING SETS ((v.nama_toko), (u.fakultas))
                    ORDER BY nama_toko, fakultas";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            catch (Exception)
            {
                // Gagal senyap
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }

            return dt;
        }
    }
}
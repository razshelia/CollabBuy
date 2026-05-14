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

            if (koneksi == null)
            {
                return dt;
            }
            else
            {
                try
                {
                    koneksi.Open();
                    string sql = @"
                        SELECT v.nama_toko, p.nama_produk, SUM(c.total_bayar_awal) as omzet
                        FROM checkouts c
                        JOIN preorders po ON c.id_po = po.id_po
                        JOIN products p ON p.id_produk = po.id_produk
                        JOIN verifications v ON v.id_user = p.id_seller
                        GROUP BY CUBE(v.nama_toko, p.nama_produk);";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {
                    // Gagal senyap agar aplikasi tidak crash
                }
                finally
                {
                    if (koneksi.State == ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                    else
                    {
                        // Koneksi sudah tertutup
                    }
                }
                return dt;
            }
        }

        // 2. ROLLUP: Penjualan per Fakultas ke Prodi
        public DataTable AmbilLaporanFakultasRollup()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

            if (koneksi == null)
            {
                return dt;
            }
            else
            {
                try
                {
                    koneksi.Open();
                    string sql = @"
                        SELECT u.fakultas, u.prodi, SUM(c.total_bayar_awal) as belanja
                        FROM checkouts c
                        JOIN users u ON c.id_user_coordinator = u.id_user
                        GROUP BY ROLLUP(u.fakultas, u.prodi);";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {
                    // Gagal senyap
                }
                finally
                {
                    if (koneksi.State == ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                    else
                    {
                        // Biarkan
                    }
                }
                return dt;
            }
        }

        // 3. GROUPING SETS: Perbandingan Omset Toko dan Total Belanja Fakultas
        public DataTable AmbilLaporanPerbandinganGroupingSets()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

            if (koneksi == null)
            {
                return dt;
            }
            else
            {
                try
                {
                    koneksi.Open();
                    string sql = @"
                        SELECT v.nama_toko, u.fakultas, SUM(c.total_bayar_awal) as total
                        FROM checkouts c
                        JOIN users u ON c.id_user_coordinator = u.id_user
                        JOIN preorders po ON c.id_po = po.id_po
                        JOIN products p ON p.id_produk = po.id_produk
                        JOIN verifications v ON v.id_user = p.id_seller
                        GROUP BY GROUPING SETS ((v.nama_toko), (u.fakultas));";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {
                    // Gagal senyap
                }
                finally
                {
                    if (koneksi.State == ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                    else
                    {
                        // Biarkan
                    }
                }
                return dt;
            }
        }
    }
}
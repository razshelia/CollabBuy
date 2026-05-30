using System;
using System.Data;
using System.Configuration;
using Npgsql;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository khusus untuk mengakses View, Function, dan Kueri Analitik (Teori Himpunan).
    /// Mengembalikan DataTable karena hasil query tidak memetakan langsung ke satu Model Domain.
    /// </summary>
    public class LaporanRepository
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public LaporanRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // 0. METHOD KHUSUS UNTUK ANALITIK PENJUALAN (SELLER UI)
        // =======================================================

        /// <summary>
        /// Mengambil total pendapatan dan jumlah pesanan yang sudah 'Selesai'.
        /// </summary>
        public (long totalPendapatan, int totalPesanan) GetRingkasanPenjualan(int idPenjual)
        {
            long pendapatan = 0;
            int pesanan = 0;

            string query = @"
                SELECT COUNT(id_transaksi) as total_pesanan, COALESCE(SUM(total_harga), 0) as total_pendapatan 
                FROM transactions 
                WHERE id_koordinator = @id AND status_pesanan = 'Selesai';";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPenjual);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pesanan = reader.GetInt32(reader.GetOrdinal("total_pesanan"));
                            pendapatan = reader.GetInt64(reader.GetOrdinal("total_pendapatan"));
                        }
                    }
                }
            }
            return (pendapatan, pesanan);
        }

        /// <summary>
        /// Mengambil daftar riwayat transaksi 'Selesai' untuk laporan.
        /// </summary>
        public DataTable GetRiwayatCuanDataTable(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT u.nama AS nama_pembeli, t.tanggal_pesanan, t.total_harga
                FROM transactions t
                JOIN users u ON t.id_pembeli = u.id_user
                WHERE t.id_koordinator = @id AND t.status_pesanan = 'Selesai'
                ORDER BY t.tanggal_pesanan DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPenjual);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }


        // =======================================================
        // 1. IMPLEMENTASI VIEW DATABASE
        // =======================================================

        public DataTable GetTransaksiLengkap()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM vw_transaksi_lengkap ORDER BY tanggal_transaksi DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }


        // =======================================================
        // 2. IMPLEMENTASI PURE FUNCTION DATABASE
        // =======================================================

        /// <summary>
        /// Memanggil Function fn_statistik_dashboard_penjual.
        /// </summary>
        public DataTable GetStatistikDashboardPenjual(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM fn_statistik_dashboard_penjual(@idPenjual);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// BUKTI PEMANGGILAN FUNCTION: Memanggil langsung cek_harga_saat_ini() di DB.
        /// </summary>
        public int CekHargaSaatIniViaDatabase(int idProduk)
        {
            string query = "SELECT cek_harga_saat_ini(@idProduk);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduk", idProduk);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
            }
        }


        // =======================================================
        // 3. IMPLEMENTASI TEORI HIMPUAN (SET OPERATIONS)
        // =======================================================

        public DataTable GetTransaksiAktifUnion()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
                UNION
                SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai';";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetSultanMemberIntersect()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT id_user, nama FROM users
                WHERE id_user IN (SELECT id_user FROM verifications WHERE is_verifikasi = TRUE)
                INTERSECT
                SELECT u.id_user, u.nama FROM users u
                JOIN transactions t ON u.id_user = t.id_koordinator;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetPenggunaPasifExcept()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT id_user, nama FROM users
                EXCEPT
                SELECT u.id_user, u.nama FROM users u
                JOIN transactions t ON u.id_user = t.id_koordinator;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }


        // =======================================================
        // 4. IMPLEMENTASI GROUP BY & CASE (KLASIFIKASI)
        // =======================================================

        public DataTable GetStatusKetersediaanKuota()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT
                    p.nama_produk,
                    p.target_kuota,
                    COALESCE(SUM(td.jumlah_pesanan), 0) AS barang_terpesan,
                    CASE
                        WHEN (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) <= 0
                            THEN 'Target Terpenuhi / Habis'
                        WHEN (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) <= 10
                            THEN 'Sisa Kuota Kritis (Peringatan!)'
                        ELSE 'Kuota Masih Aman'
                    END AS status_ketersediaan
                FROM products p
                LEFT JOIN transaction_details td ON p.id_produk = td.id_produk
                WHERE p.target_kuota IS NOT NULL
                GROUP BY p.id_produk, p.nama_produk, p.target_kuota;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// STATEMENT 2: Klasifikasi Performa Penjual (Tier Penjual).
        /// DIPERBAIKI: Menggunakan hitungan Omzet Bersih sesuai SQL.
        /// </summary>
        public DataTable GetKlasifikasiPerformaPenjual()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT
                    u.nama AS nama_penjual,
                    SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) AS total_omzet_bersih,
                    CASE
                        WHEN SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) >= 500000
                            THEN 'Seller Sultan (Top Tier)'
                        WHEN SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) >= 100000
                            THEN 'Seller Menengah (Mid Tier)'
                        ELSE 'Seller Pemula (Newbie)'
                    END AS tier_penjual
                FROM transaction_details td
                JOIN products p ON td.id_produk = p.id_produk
                JOIN users u    ON p.id_penjual = u.id_user
                GROUP BY u.nama
                ORDER BY total_omzet_bersih DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetTotalBarangTerjual()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT p.nama_produk, SUM(td.jumlah_pesanan) AS total_terjual
                FROM transaction_details td
                JOIN products p ON td.id_produk = p.id_produk
                GROUP BY p.nama_produk
                ORDER BY total_terjual DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }


        // =======================================================
        // 5. IMPLEMENTASI CUBE, ROLLUP, GROUPING SETS, SUBQUERY
        // =======================================================

        public DataTable GetAnalisisPasarCube()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT
                    COALESCE(kat.nama_kategori, 'Semua Kategori')     AS kategori,
                    COALESCE(po.jenis_po, 'Tanpa PO / Semua Jenis')  AS jenis_po,
                    SUM(td.jumlah_pesanan)                            AS total_barang_terjual
                FROM transaction_details td
                JOIN      products    p   ON td.id_produk   = p.id_produk
                LEFT JOIN preorders   po  ON p.id_po        = po.id_po
                LEFT JOIN categories  kat ON p.id_kategori  = kat.id_kategori
                GROUP BY CUBE (kat.nama_kategori, po.jenis_po);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// ROLLUP: Hierarki Waktu → Total Tahun → Total Bulan.
        /// DIPERBAIKI: Mengambil kolom Refund dan Omzet Bersih sesuai SQL.
        /// </summary>
        public DataTable GetLaporanKeuanganRollup()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT
                    EXTRACT(YEAR  FROM t.tanggal_transaksi) AS tahun,
                    EXTRACT(MONTH FROM t.tanggal_transaksi) AS bulan,
                    SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli)                                    AS omzet_kotor,
                    SUM(COALESCE(td.selisih_refund, 0))                                                   AS total_refund,
                    SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) AS omzet_bersih
                FROM transactions t
                JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
                WHERE t.status_pesanan = 'Selesai'
                GROUP BY ROLLUP (
                    EXTRACT(YEAR  FROM t.tanggal_transaksi),
                    EXTRACT(MONTH FROM t.tanggal_transaksi)
                );";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetRingkasanGlobalGroupingSets()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT
                    u.nama            AS nama_penjual,
                    kat.nama_kategori AS nama_kategori,
                    SUM(td.jumlah_pesanan) AS unit_terjual
                FROM transaction_details td
                JOIN transactions  t   ON td.id_transaksi = t.id_transaksi
                JOIN products      p   ON td.id_produk    = p.id_produk
                JOIN categories    kat ON p.id_kategori   = kat.id_kategori
                JOIN users         u   ON p.id_penjual    = u.id_user
                GROUP BY GROUPING SETS ((u.nama), (kat.nama_kategori));";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetProdukSisaKuotaKritis()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT nama_produk, target_kuota
                FROM products p
                WHERE p.target_kuota IS NOT NULL
                  AND (
                        p.target_kuota - (
                            SELECT COALESCE(SUM(jumlah_pesanan), 0)
                            FROM transaction_details td
                            WHERE td.id_produk = p.id_produk
                        )
                      ) <= 5;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public DataTable GetLpjDanusPerPo(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT v.* FROM vw_lpj_danus_per_po v
                JOIN preorders po ON v.id_po = po.id_po
                WHERE po.id_penjual = @idPenjual;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
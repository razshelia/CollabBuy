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
        private readonly string _connectionString;

        public LaporanRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
        }

        // Helper untuk mengisi DataTable dengan query + parameter opsional
        private DataTable FillDataTable(string query, Action<NpgsqlCommand> addParams = null)
        {
            DataTable dt = new DataTable();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    addParams?.Invoke(cmd);
                    using (var da = new NpgsqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }


        // =======================================================
        // 0. METHOD KHUSUS UNTUK ANALITIK PENJUALAN (SELLER UI)
        // =======================================================

        public (long totalPendapatan, int totalPesanan) GetRingkasanPenjualan(int idPenjual)
        {
            string query = @"
                SELECT
                    COUNT(DISTINCT t.id_transaksi) AS total_pesanan,
                    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_pendapatan
                FROM transactions t
                JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
                JOIN products p ON td.id_produk = p.id_produk
                WHERE p.id_penjual = @id AND t.status_pesanan = 'Selesai';";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPenjual);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long pendapatan = reader.IsDBNull(reader.GetOrdinal("total_pendapatan"))
                                             ? 0L
                                             : Convert.ToInt64(reader["total_pendapatan"]);
                            int pesanan = reader.IsDBNull(reader.GetOrdinal("total_pesanan"))
                                         ? 0
                                         : Convert.ToInt32(reader["total_pesanan"]);
                            return (pendapatan, pesanan);
                        }
                    }
                }
            }
            return (0, 0);
        }

        public DataTable GetRiwayatCuanDataTable(int idPenjual)
        {
            string query = @"
                SELECT
                    u.nama AS nama_pembeli,
                    t.tanggal_transaksi AS tanggal_pesanan,
                    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_harga
                FROM transactions t
                JOIN users u ON t.id_koordinator = u.id_user
                JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
                JOIN products p ON td.id_produk = p.id_produk
                WHERE p.id_penjual = @id AND t.status_pesanan = 'Selesai'
                GROUP BY t.id_transaksi, u.nama, t.tanggal_transaksi
                ORDER BY t.tanggal_transaksi DESC;";
            return FillDataTable(query, cmd => cmd.Parameters.AddWithValue("@id", idPenjual));
        }


        // =======================================================
        // 1. IMPLEMENTASI VIEW DATABASE
        // =======================================================

        public DataTable GetTransaksiLengkap()
        {
            return FillDataTable("SELECT * FROM vw_transaksi_lengkap ORDER BY tanggal_transaksi DESC;");
        }


        // =======================================================
        // 2. IMPLEMENTASI PURE FUNCTION DATABASE
        // =======================================================

        public DataTable GetStatistikDashboardPenjual(int idPenjual)
        {
            return FillDataTable("SELECT * FROM fn_statistik_dashboard_penjual(@idPenjual);",
                cmd => cmd.Parameters.AddWithValue("@idPenjual", idPenjual));
        }

        public int CekHargaSaatIniViaDatabase(int idProduk)
        {
            string query = "SELECT cek_harga_saat_ini(@idProduk);";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduk", idProduk);
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }
            }
        }


        // =======================================================
        // 3. IMPLEMENTASI TEORI HIMPUNAN (SET OPERATIONS)
        // =======================================================

        public DataTable GetTransaksiAktifUnion()
        {
            string query = @"
                SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
                UNION
                SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai';";
            return FillDataTable(query);
        }

        public DataTable GetSultanMemberIntersect()
        {
            string query = @"
                SELECT id_user, nama FROM users
                WHERE id_user IN (SELECT id_user FROM verifications WHERE is_verifikasi = TRUE)
                INTERSECT
                SELECT u.id_user, u.nama FROM users u
                JOIN transactions t ON u.id_user = t.id_koordinator;";
            return FillDataTable(query);
        }

        public DataTable GetPenggunaPasifExcept()
        {
            string query = @"
                SELECT id_user, nama FROM users
                EXCEPT
                SELECT u.id_user, u.nama FROM users u
                JOIN transactions t ON u.id_user = t.id_koordinator;";
            return FillDataTable(query);
        }


        // =======================================================
        // 4. IMPLEMENTASI GROUP BY & CASE (KLASIFIKASI)
        // =======================================================

        public DataTable GetStatusKetersediaanKuota()
        {
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
            return FillDataTable(query);
        }

        public DataTable GetKlasifikasiPerformaPenjual()
        {
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
            return FillDataTable(query);
        }

        public DataTable GetTotalBarangTerjual()
        {
            string query = @"
                SELECT p.nama_produk, SUM(td.jumlah_pesanan) AS total_terjual
                FROM transaction_details td
                JOIN products p ON td.id_produk = p.id_produk
                GROUP BY p.nama_produk
                ORDER BY total_terjual DESC;";
            return FillDataTable(query);
        }


        // =======================================================
        // 5. IMPLEMENTASI CUBE, ROLLUP, GROUPING SETS, SUBQUERY
        // =======================================================

        public DataTable GetAnalisisPasarCube()
        {
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
            return FillDataTable(query);
        }

        public DataTable GetLaporanKeuanganRollup()
        {
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
            return FillDataTable(query);
        }

        public DataTable GetRingkasanGlobalGroupingSets()
        {
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
            return FillDataTable(query);
        }

        public DataTable GetProdukSisaKuotaKritis()
        {
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
            return FillDataTable(query);
        }

        public DataTable GetLpjDanusPerPo(int idPenjual)
        {
            // FIX: Gunakan subquery yang sudah difilter status 'Selesai' terlebih dahulu,
            // lalu LEFT JOIN ke subquery itu. Dengan cara ini tidak ada konflik dua WHERE,
            // dan hanya transaksi Selesai yang masuk perhitungan — konsisten dengan UI.
            string query = @"
                SELECT
                    po.id_po,
                    po.judul_po,
                    po.jenis_po,
                    po.batas_waktu,
                    p.nama_produk,
                    p.harga_dasar,
                    p.harga_diskon,
                    COALESCE(agg.total_barang_terjual, 0)   AS total_barang_terjual,
                    COALESCE(agg.omzet_kotor, 0)            AS omzet_kotor,
                    COALESCE(agg.total_refund_dicairkan, 0) AS total_refund_dicairkan,
                    COALESCE(agg.omzet_bersih_lpj, 0)       AS omzet_bersih_lpj,
                    CASE
                        WHEN po.is_aktif = TRUE AND po.batas_waktu >= NOW() THEN 'Sedang Berjalan'
                        WHEN po.is_aktif = TRUE AND po.batas_waktu  < NOW() THEN 'Batas Waktu Habis'
                        ELSE 'Ditutup'
                    END AS status_po
                FROM preorders po
                JOIN products p ON po.id_po = p.id_po
                LEFT JOIN (
                    -- Subquery: hanya transaction_details dari transaksi yang sudah Selesai
                    SELECT
                        td.id_produk,
                        SUM(td.jumlah_pesanan)                                                          AS total_barang_terjual,
                        SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli)                              AS omzet_kotor,
                        SUM(COALESCE(td.selisih_refund, 0))                                             AS total_refund_dicairkan,
                        SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli)
                            - COALESCE(td.selisih_refund, 0))                                           AS omzet_bersih_lpj
                    FROM transaction_details td
                    JOIN transactions t ON td.id_transaksi = t.id_transaksi
                    WHERE t.status_pesanan = 'Selesai'   -- filter di sini, bukan di JOIN luar
                    GROUP BY td.id_produk
                ) agg ON p.id_produk = agg.id_produk
                WHERE po.id_penjual = @idPenjual
                  AND po.is_deleted = FALSE
                ORDER BY po.batas_waktu DESC, p.nama_produk ASC;";

            return FillDataTable(query, cmd => cmd.Parameters.AddWithValue("@idPenjual", idPenjual));
        }
    }
}
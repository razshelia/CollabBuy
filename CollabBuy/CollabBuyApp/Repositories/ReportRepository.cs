using System;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DatabaseHelper _db;

        public ReportRepository()
        {
            _db = new DatabaseHelper();
        }

        private DataTable EksekusiQuery(string sql)
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return dt;

            try
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal menjalankan laporan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return dt;
        }

        public DataTable BarangTerjualPerProduk()
        {
            string sql = @"
                SELECT p.nama_produk, SUM(td.jumlah_pesanan) AS total_terjual
                FROM transaction_details td
                JOIN products p ON td.id_produk = p.id_produk
                GROUP BY p.nama_produk ORDER BY total_terjual DESC";
            return EksekusiQuery(sql);
        }

        public DataTable CubeKategoriJenisPO()
        {
            string sql = @"
                SELECT kat.nama_kategori, po.jenis_po, SUM(td.jumlah_pesanan) AS total_barang_terjual
                FROM transaction_details td
                JOIN products p ON td.id_produk = p.id_produk
                JOIN preorders po ON p.id_po = po.id_po
                JOIN categories kat ON p.id_kategori = kat.id_kategori
                GROUP BY CUBE (kat.nama_kategori, po.jenis_po)";
            return EksekusiQuery(sql);
        }

        public DataTable RollupOmzetPerWaktu()
        {
            string sql = @"
                SELECT EXTRACT(YEAR FROM t.tanggal_transaksi) AS tahun,
                       EXTRACT(MONTH FROM t.tanggal_transaksi) AS bulan,
                       SUM(t.total_bayar_grup) AS omzet_kotor
                FROM transactions t WHERE t.status_pesanan = 'Selesai'
                GROUP BY ROLLUP (EXTRACT(YEAR FROM t.tanggal_transaksi), EXTRACT(MONTH FROM t.tanggal_transaksi))";
            return EksekusiQuery(sql);
        }

        public DataTable GroupingSetsPenjualKategori()
        {
            string sql = @"
                SELECT u.nama AS nama_penjual, kat.nama_kategori, SUM(td.jumlah_pesanan) AS unit_terjual
                FROM transaction_details td
                JOIN transactions t ON td.id_transaksi = t.id_transaksi
                JOIN products p ON td.id_produk = p.id_produk
                JOIN categories kat ON p.id_kategori = kat.id_kategori
                JOIN preorders po ON p.id_po = po.id_po
                JOIN users u ON po.id_penjual = u.id_user
                GROUP BY GROUPING SETS ((u.nama), (kat.nama_kategori))";
            return EksekusiQuery(sql);
        }

        public DataTable SubqueryProdukKuotaMenipis()
        {
            string sql = @"
                SELECT nama_produk, target_kuota FROM products p
                WHERE p.target_kuota IS NOT NULL AND (
                    p.target_kuota - (SELECT COALESCE(SUM(jumlah_pesanan), 0) FROM transaction_details td WHERE td.id_produk = p.id_produk)
                ) <= 5";
            return EksekusiQuery(sql);
        }

        public DataTable UnionTransaksiBerjalanSelesai()
        {
            string sql = @"
                SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
                UNION
                SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai'";
            return EksekusiQuery(sql);
        }

        public DataTable IntersectPenjualJugaPembeli()
        {
            string sql = @"
                SELECT id_user FROM verifications WHERE is_verifikasi = TRUE
                INTERSECT
                SELECT id_koordinator FROM transactions";
            return EksekusiQuery(sql);
        }

        public DataTable ExceptUserBelumTransaksi()
        {
            string sql = @"
                SELECT id_user, nama FROM users
                EXCEPT
                SELECT u.id_user, u.nama FROM users u JOIN transactions t ON u.id_user = t.id_koordinator";
            return EksekusiQuery(sql);
        }
    }
}
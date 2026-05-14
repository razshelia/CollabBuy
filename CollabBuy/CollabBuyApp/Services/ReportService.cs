using System.Data;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ReportService
    {
        private readonly IReportRepository _reportRepo;

        public ReportService()
        {
            _reportRepo = new ReportRepository();
        }

        // ── 1. Barang terlaris ──
        public DataTable BarangTerjualPerProduk()
        {
            return _reportRepo.BarangTerjualPerProduk();
        }

        // ── 2. CUBE: Kategori × Jenis PO ──
        public DataTable CubeKategoriJenisPO()
        {
            return _reportRepo.CubeKategoriJenisPO();
        }

        // ── 3. ROLLUP: Omzet per tahun/bulan ──
        public DataTable RollupOmzetPerWaktu()
        {
            return _reportRepo.RollupOmzetPerWaktu();
        }

        // ── 4. GROUPING SETS: Penjual & Kategori ──
        public DataTable GroupingSetsPenjualKategori()
        {
            return _reportRepo.GroupingSetsPenjualKategori();
        }

        // ── 5. Subquery: Produk yang kuotanya menipis ──
        public DataTable SubqueryProdukKuotaMenipis()
        {
            return _reportRepo.SubqueryProdukKuotaMenipis();
        }

        // ── 6. UNION: Transaksi berjalan & selesai ──
        public DataTable UnionTransaksiBerjalanSelesai()
        {
            return _reportRepo.UnionTransaksiBerjalanSelesai();
        }

        // ── 7. INTERSECT: Penjual yang juga pembeli ──
        public DataTable IntersectPenjualJugaPembeli()
        {
            return _reportRepo.IntersectPenjualJugaPembeli();
        }

        // ── 8. EXCEPT: User yang belum pernah transaksi ──
        public DataTable ExceptUserBelumTransaksi()
        {
            return _reportRepo.ExceptUserBelumTransaksi();
        }
    }
}
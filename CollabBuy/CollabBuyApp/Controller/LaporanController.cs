using System;
using System.Data;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Controllers
{
    public class LaporanController
    {
        private readonly LaporanRepository _laporanRepo;

        public LaporanController()
        {
            _laporanRepo = new LaporanRepository();
        }

        // Helper agar tidak copy-paste log di setiap catch
        private void LogError(string namaMethod, Exception ex)
        {
            Console.WriteLine($"[LaporanController.{namaMethod}] Error: {ex.Message}");
        }


        // =======================================================
        // 0. ANALITIK PENJUALAN (SELLER)
        // =======================================================

        public (long totalPendapatan, int totalPesanan) GetRingkasanLapak(int idPenjual)
        {
            try { return _laporanRepo.GetRingkasanPenjualan(idPenjual); }
            catch (Exception ex) { LogError(nameof(GetRingkasanLapak), ex); return (0, 0); }
        }

        public DataTable GetDetailRiwayatCuan(int idPenjual)
        {
            try { return _laporanRepo.GetRiwayatCuanDataTable(idPenjual); }
            catch (Exception ex) { LogError(nameof(GetDetailRiwayatCuan), ex); return new DataTable(); }
        }


        // =======================================================
        // 1. VIEW DATABASE (ADMIN)
        // =======================================================

        public DataTable GetTransaksiLengkap()
        {
            try { return _laporanRepo.GetTransaksiLengkap(); }
            catch (Exception ex) { LogError(nameof(GetTransaksiLengkap), ex); return new DataTable(); }
        }


        // =======================================================
        // 2. PURE FUNCTION DATABASE
        // =======================================================

        public DataTable GetStatistikDashboardPenjual(int idPenjual)
        {
            try { return _laporanRepo.GetStatistikDashboardPenjual(idPenjual); }
            catch (Exception ex) { LogError(nameof(GetStatistikDashboardPenjual), ex); return new DataTable(); }
        }

        public int CekHargaSaatIniViaDatabase(int idProduk)
        {
            try { return _laporanRepo.CekHargaSaatIniViaDatabase(idProduk); }
            catch (Exception ex) { LogError(nameof(CekHargaSaatIniViaDatabase), ex); return 0; }
        }


        // =======================================================
        // 3. SET OPERATIONS
        // =======================================================

        public DataTable GetTransaksiAktifUnion()
        {
            try { return _laporanRepo.GetTransaksiAktifUnion(); }
            catch (Exception ex) { LogError(nameof(GetTransaksiAktifUnion), ex); return new DataTable(); }
        }

        public DataTable GetSultanMemberIntersect()
        {
            try { return _laporanRepo.GetSultanMemberIntersect(); }
            catch (Exception ex) { LogError(nameof(GetSultanMemberIntersect), ex); return new DataTable(); }
        }

        public DataTable GetPenggunaPasifExcept()
        {
            try { return _laporanRepo.GetPenggunaPasifExcept(); }
            catch (Exception ex) { LogError(nameof(GetPenggunaPasifExcept), ex); return new DataTable(); }
        }


        // =======================================================
        // 4. GROUP BY & CASE
        // =======================================================

        public DataTable GetStatusKetersediaanKuota()
        {
            try { return _laporanRepo.GetStatusKetersediaanKuota(); }
            catch (Exception ex) { LogError(nameof(GetStatusKetersediaanKuota), ex); return new DataTable(); }
        }

        public DataTable GetKlasifikasiPerformaPenjual()
        {
            try { return _laporanRepo.GetKlasifikasiPerformaPenjual(); }
            catch (Exception ex) { LogError(nameof(GetKlasifikasiPerformaPenjual), ex); return new DataTable(); }
        }

        public DataTable GetTotalBarangTerjual()
        {
            try { return _laporanRepo.GetTotalBarangTerjual(); }
            catch (Exception ex) { LogError(nameof(GetTotalBarangTerjual), ex); return new DataTable(); }
        }


        // =======================================================
        // 5. CUBE, ROLLUP, GROUPING SETS, SUBQUERY
        // =======================================================

        public DataTable GetAnalisisPasarCube()
        {
            try { return _laporanRepo.GetAnalisisPasarCube(); }
            catch (Exception ex) { LogError(nameof(GetAnalisisPasarCube), ex); return new DataTable(); }
        }

        public DataTable GetLaporanKeuanganRollup()
        {
            try { return _laporanRepo.GetLaporanKeuanganRollup(); }
            catch (Exception ex) { LogError(nameof(GetLaporanKeuanganRollup), ex); return new DataTable(); }
        }

        public DataTable GetRingkasanGlobalGroupingSets()
        {
            try { return _laporanRepo.GetRingkasanGlobalGroupingSets(); }
            catch (Exception ex) { LogError(nameof(GetRingkasanGlobalGroupingSets), ex); return new DataTable(); }
        }

        public DataTable GetProdukSisaKuotaKritis()
        {
            try { return _laporanRepo.GetProdukSisaKuotaKritis(); }
            catch (Exception ex) { LogError(nameof(GetProdukSisaKuotaKritis), ex); return new DataTable(); }
        }

        public DataTable GetLpjDanusPerPo(int idPenjual)
        {
            try { return _laporanRepo.GetLpjDanusPerPo(idPenjual); }
            catch (Exception ex) { LogError(nameof(GetLpjDanusPerPo), ex); return new DataTable(); }
        }
    }
}
using System.Data;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ReportService
    {
        private ReportRepository reportRepo;

        public ReportService()
        {
            this.reportRepo = new ReportRepository();
        }

        public DataTable MuatLaporanOmzetCube()
        {
            DataTable data = this.reportRepo.AmbilLaporanOmzetCube();

            if (data == null || data.Rows.Count == 0)
            {
                UXHelper.TampilkanError("Belum ada data transaksi yang bisa dianalisis (CUBE).");
                return new DataTable();
            }
            else
            {
                return data;
            }
        }

        public DataTable MuatLaporanFakultasRollup()
        {
            DataTable data = this.reportRepo.AmbilLaporanFakultasRollup();

            if (data == null || data.Rows.Count == 0)
            {
                UXHelper.TampilkanError("Belum ada data belanja tingkat Fakultas (ROLLUP).");
                return new DataTable();
            }
            else
            {
                return data;
            }
        }
    }
}
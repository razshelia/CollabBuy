using System;
using System.Data;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ReportService
    {
        private readonly IReportRepository _reportRepo;
        public ReportService(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public DataTable BarangTerjualPerProduk()
        {
            try { return _reportRepo.BarangTerjualPerProduk(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable CubeKategoriJenisPO()
        {
            try { return _reportRepo.CubeKategoriJenisPO(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable RollupOmzetPerWaktu()
        {
            try { return _reportRepo.RollupOmzetPerWaktu(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable GroupingSetsPenjualKategori()
        {
            try { return _reportRepo.GroupingSetsPenjualKategori(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable SubqueryProdukKuotaMenipis()
        {
            try { return _reportRepo.SubqueryProdukKuotaMenipis(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable UnionTransaksiBerjalanSelesai()
        {
            try { return _reportRepo.UnionTransaksiBerjalanSelesai(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable IntersectPenjualJugaPembeli()
        {
            try { return _reportRepo.IntersectPenjualJugaPembeli(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }

        public DataTable ExceptUserBelumTransaksi()
        {
            try { return _reportRepo.ExceptUserBelumTransaksi(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new DataTable(); }
        }
    }
}
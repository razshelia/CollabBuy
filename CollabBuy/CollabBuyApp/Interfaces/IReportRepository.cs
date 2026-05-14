using System.Data;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IReportRepository
    {
        DataTable BarangTerjualPerProduk();
        DataTable CubeKategoriJenisPO();
        DataTable RollupOmzetPerWaktu();
        DataTable GroupingSetsPenjualKategori();
        DataTable SubqueryProdukKuotaMenipis();
        DataTable UnionTransaksiBerjalanSelesai();
        DataTable IntersectPenjualJugaPembeli();
        DataTable ExceptUserBelumTransaksi();
    }
}
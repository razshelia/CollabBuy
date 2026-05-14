using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface ICheckoutRepository
    {
        bool BuatTransaksi(int idUser, int idPo, int jumlahPesanan, string pathBukti);
        List<dynamic> AmbilRiwayatPesanan(int idUser);
        List<dynamic> AmbilPesananMasuk(int idSeller);
        bool ValidasiPembayaran(int idCheckout);
        bool UbahStatusSelesai(int idCheckout);
        bool BatalkanPesanan(int idCheckout);
    }
}
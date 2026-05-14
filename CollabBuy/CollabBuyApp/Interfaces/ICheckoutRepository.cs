using CollabBuy.CollabBuyApp.Models;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface ICheckoutRepository
    {
        // Memanggil Stored Procedure: sp_buat_transaksi
        bool BuatTransaksi(int idUser, int idPo, int jumlahPesanan, string pathBukti);

        // Fitur Riwayat untuk Pembeli
        List<Checkout> AmbilRiwayatPesanan(int idUser);

        // Fitur Validasi untuk Penjual
        List<Checkout> AmbilPesananMasuk(int idSeller);
        bool ValidasiPembayaran(int idCheckout);

        // Penyelesaian Pesanan
        bool UbahStatusSelesai(int idCheckout);
        bool BatalkanPesanan(int idCheckout);
    }
}
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface ITransactionRepository
    {
        int BuatTransaksi(Transaction transaksi, List<TransactionDetail> details);
        bool ValidasiPembayaran(int idTransaksi, string buktiBayar);
        bool UbahStatusPesanan(int idTransaksi, string statusBaru);
        List<Transaction> AmbilRiwayatKoordinator(int idKoordinator);
        List<Transaction> AmbilPesananMasukPenjual(int idPenjual);
        List<TransactionDetail> AmbilDetailTransaksi(int idTransaksi);
        Transaction AmbilTransaksiById(int idTransaksi);
    }
}
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CheckoutService
    {
        private ICheckoutRepository repository;

        public CheckoutService()
        {
            this.repository = new CheckoutRepository();
        }

        public bool BuatTransaksi(int idUser, int idPo, int jumlahPesanan, string pathBukti)
        {
            if (jumlahPesanan <= 0)
            {
                UXHelper.TampilkanError("Jumlah pesanan harus lebih dari 0, Bestie!");
                return false;
            }

            bool sukses = repository.BuatTransaksi(idUser, idPo, jumlahPesanan, pathBukti);
            if (sukses)
                UXHelper.TampilkanSukses("Yeay! Pesanan berhasil dibuat. Tunggu konfirmasi ya! ✨");
            else
                UXHelper.TampilkanError("Gagal membuat pesanan. Coba lagi nanti.");
            return sukses;
        }

        // Digunakan oleh RiwayatControl
        public List<dynamic> AmbilRiwayatUser(int idUser)
        {
            return repository.AmbilRiwayatPesanan(idUser);
        }

        // Method lain untuk seller / admin
        public List<dynamic> AmbilPesananMasuk(int idSeller)
        {
            return repository.AmbilPesananMasuk(idSeller);
        }

        public bool ValidasiPembayaran(int idCheckout)
        {
            bool sukses = repository.ValidasiPembayaran(idCheckout);
            if (sukses)
                UXHelper.TampilkanSukses("Pembayaran berhasil divalidasi!");
            else
                UXHelper.TampilkanError("Gagal validasi pembayaran.");
            return sukses;
        }

        public bool UbahStatusSelesai(int idCheckout)
        {
            bool sukses = repository.UbahStatusSelesai(idCheckout);
            if (sukses)
                UXHelper.TampilkanSukses("Pesanan diselesaikan.");
            return sukses;
        }

        public bool BatalkanPesanan(int idCheckout)
        {
            if (!UXHelper.TampilkanKonfirmasi("Yakin ingin membatalkan pesanan ini?"))
                return false;
            bool sukses = repository.BatalkanPesanan(idCheckout);
            if (sukses)
                UXHelper.TampilkanSukses("Pesanan dibatalkan.");
            return sukses;
        }
    }
}
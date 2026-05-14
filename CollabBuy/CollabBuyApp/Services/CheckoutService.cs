using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CheckoutService
    {
        private CheckoutRepository checkoutRepo;

        public CheckoutService()
        {
            this.checkoutRepo = new CheckoutRepository();
        }

        public bool LakukanPembayaran(int idUser, int idPo, int jumlahPesanan, string lokasiBuktiAsli)
        {
            if (jumlahPesanan <= 0)
            {
                UXHelper.TampilkanError("Jumlah pesanan minimal adalah 1.");
                return false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(lokasiBuktiAsli))
                {
                    UXHelper.TampilkanError("Anda wajib melampirkan bukti transfer!");
                    return false;
                }
                else
                {
                    // Konfirmasi dari UX Helper sebelum memanggil Stored Procedure
                    bool yakin = UXHelper.TampilkanKonfirmasi("Apakah Anda yakin ingin menyelesaikan pembayaran ini?");

                    if (yakin)
                    {
                        // FileHelper di layer Form seharusnya sudah mengamankan file ke folder aplikasi
                        bool sukses = this.checkoutRepo.BuatTransaksi(idUser, idPo, jumlahPesanan, lokasiBuktiAsli);

                        if (sukses)
                        {
                            UXHelper.TampilkanSukses("Pembayaran berhasil dikirim dan menunggu validasi Penjual!");
                            return true;
                        }
                        else
                        {
                            UXHelper.TampilkanError("Transaksi ditolak. Kuota mungkin sudah penuh atau stok habis.");
                            return false;
                        }
                    }
                    else
                    {
                        return false; // User membatalkan aksi
                    }
                }
            }
        }
    }
}
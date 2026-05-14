using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class InteractionService
    {

        public InteractionService()
        {

        }

        public bool KirimAduan(Aduan aduanBaru)
        {
            if (aduanBaru == null)
            {
                UXHelper.TampilkanError("Data aduan kosong!");
                return false;
            }
            else
            {
                // Logika: Pesan tidak boleh terlalu pendek
                if (aduanBaru.Pesan.Length < 10)
                {
                    UXHelper.TampilkanError("Pesan aduan terlalu singkat, mohon jelaskan lebih detail.");
                    return false;
                }
                else
                {
                    // Panggil repository untuk simpan aduan (Simulasi)
                    UXHelper.TampilkanSukses("Aduan berhasil dikirim ke Admin!");
                    return true;
                }
            }
        }

        public bool KirimUlasan(Ulasan ulasanBaru)
        {
            if (ulasanBaru == null)
            {
                UXHelper.TampilkanError("Data ulasan kosong!");
                return false;
            }
            else
            {
                if (ulasanBaru.Rating < 1 || ulasanBaru.Rating > 5)
                {
                    UXHelper.TampilkanError("Rating hanya boleh dari 1 hingga 5 bintang.");
                    return false;
                }
                else
                {
                    // Panggil repository untuk simpan ulasan (Simulasi)
                    UXHelper.TampilkanSukses("Terima kasih atas ulasan Anda!");
                    return true;
                }
            }
        }
    }
}
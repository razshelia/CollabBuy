using System;
using System.Data;
using CollabBuy.CollabBuyApp.Models; // Wajib ada untuk akses class PreOrder
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Controllers
{
    public class PreorderController
    {
        private readonly PreOrderRepository _poRepo;

        public PreorderController()
        {
            _poRepo = new PreOrderRepository();
        }

        public DataTable GetDaftarPoLapak(int idPenjual)
        {
            try { return _poRepo.GetPoByPenjual(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }

        public (bool sukses, string pesan) TambahSesiPo(int idPenjual, string judul, string jenis, DateTime batasWaktu, string rekening)
        {
            // Validasi Input
            if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(rekening) || string.IsNullOrWhiteSpace(jenis))
            {
                return (false, "Judul, Jenis PO, dan Rekening wajib diisi ya bestie!");
            }

            if (batasWaktu <= DateTime.Now)
            {
                return (false, "Batas waktu nggak boleh di masa lalu dong!");
            }

            try
            {
                // 1. Buat Objek Model PreOrder (sesuai kontrak Repository Insert)
                PreOrder poBaru = new PreOrder(idPenjual, judul, jenis, rekening, batasWaktu);
                poBaru.UbahStatus("Aktif"); // Karena baru dibuat, statusnya pasti Aktif

                // 2. Panggil Repository menggunakan method Insert
                _poRepo.Insert(poBaru);

                return (true, "Yey! Sesi PO baru berhasil dibuka! 🎉");
            }
            catch (Exception ex)
            {
                return (false, "Gagal bikin PO: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) ProsesMassalPo(int idPo, string statusBaru)
        {
            try
            {
                // Manggil Stored Procedure via Repository
                _poRepo.UpdateStatusMassal(idPo, statusBaru);
                return (true, $"Semua pesanan di PO ini berhasil diubah jadi '{statusBaru}'! 🚀");
            }
            catch (Exception ex)
            {
                return (false, "Gagal update massal: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) TutupPo(int idPo)
        {
            try
            {
                // Panggil method TutupSesiPo yang ada di Repository
                _poRepo.TutupSesiPo(idPo);
                return (true, "PO berhasil ditutup. Orang-orang udah ga bisa order lagi di sesi ini.");
            }
            catch (Exception ex)
            {
                return (false, "Gagal tutup PO: " + ex.Message);
            }
        }
    }
}
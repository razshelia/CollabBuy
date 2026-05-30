using System;
using System.Data;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Controllers
{
    public class PreOrderController
    {
        private readonly PreOrderRepository _poRepo;

        public PreOrderController()
        {
            _poRepo = new PreOrderRepository();
        }

        public DataTable GetActiveSesiPO(string keyword)
        {
            try { return _poRepo.GetSesiPOAktif(keyword); }
            catch (Exception) { return new DataTable(); }
        }

        public DataTable GetProdukTersedia(int idPenjual)
        {
            try { return _poRepo.GetProdukTanpaPO(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }

        public (bool sukses, string pesan) GasLuncurkanPO(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            // Validasi Input Gen-Z style
            if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(rekening) || string.IsNullOrWhiteSpace(jenis))
            {
                return (false, "Spill judul, jenis PO, sama rekeningnya dong bestie, ga boleh kosong!");
            }

            if (batasWaktu <= DateTime.Now)
            {
                return (false, "Waktu tenggatnya masa di masa lalu? Move on dong, set ke masa depan!");
            }

            if (idProduk <= 0)
            {
                return (false, "Pilih dulu produknya ngab, masa buka jualan tapi ga ada barangnya?");
            }

            try
            {
                bool result = _poRepo.InsertPOAndUpdateProduct(idPenjual, judul, jenis, rekening, batasWaktu, idProduk, targetKuota);
                if (result) return (true, "Yey! Sesi PO kamu berhasil dilaunching! 🎉 Semoga cuan deres!");

                return (false, "Hmm, gagal nyimpen ke database nih.");
            }
            catch (Exception ex)
            {
                return (false, "Waduh error server: " + ex.Message);
            }
        }
    }
}
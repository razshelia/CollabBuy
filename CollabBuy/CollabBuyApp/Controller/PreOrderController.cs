using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Data;

namespace CollabBuy.CollabBuyApp.Controllers
{
    public class PreOrderController
    {
        private readonly PreOrderRepository _poRepo;

        public PreOrderController()
        {
            this._poRepo = new PreOrderRepository();
        }

        /// <summary>
        /// Mengambil objek Model PreOrder utuh berdasarkan ID-nya.
        /// </summary>
        public Models.PreOrder GetPreOrder(int idPo)
        {
            try
            {
                DataTable dt = this._poRepo.GetById(idPo);

                if (dt == null || dt.Rows.Count == 0) return null;

                DataRow row = dt.Rows[0];

                int idPenjual = row["id_penjual"] != DBNull.Value ? Convert.ToInt32(row["id_penjual"]) : 0;
                string judulPo = row["judul_po"].ToString();
                string jenisPo = row["jenis_po"].ToString();
                string rekening = row["rekening"].ToString();
                DateTime batasWaktu = row["batas_waktu"] != DBNull.Value
                    ? Convert.ToDateTime(row["batas_waktu"])
                    : DateTime.Now.AddDays(1); // fallback aman — bukan masa lalu

                var po = new Models.PreOrder(idPenjual, judulPo, jenisPo, rekening, batasWaktu);
                po.TutupOtomatisJikaBasi(); // tandai expired tanpa throw
                return po;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PreOrderController.GetPreOrder] Error ID {idPo}: {ex.Message}");
                return null;
            }
        }

        public int GetJumlahPoAktif()
        {
            try
            {
                DataTable dt = this._poRepo.GetSesiPOAktif("");
                return dt?.Rows.Count ?? 0;
            }
            catch (Exception) { return 0; }
        }

        public DataTable GetActiveSesiPO(string keyword)
        {
            try { return this._poRepo.GetSesiPOAktif(keyword); }
            catch (Exception) { return new DataTable(); }
        }

        /// <summary>
        /// Mengembalikan semua produk aktif milik penjual (is_deleted = FALSE),
        /// termasuk yang sudah di dalam PO lain — agar satu produk bisa dimasukkan PO berkali-kali.
        /// </summary>
        public DataTable GetProdukTersedia(int idPenjual)
        {
            try { return this._poRepo.GetSemuaProdukAktif(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }

        public (bool sukses, string pesan, int idPO) BukaSesiPOBaru(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu)
        {
            if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(rekening) || string.IsNullOrWhiteSpace(jenis))
                return (false, "Judul, jenis PO, dan rekening tidak boleh kosong!", 0);

            try
            {
                Models.PreOrder poBaru = new Models.PreOrder(idPenjual, judul, jenis, rekening, DateTime.Now);
                poBaru.BukaSesiBaru(batasWaktu);
                int idPO = this._poRepo.InsertPOSaja(idPenjual, judul, jenis, rekening, batasWaktu);
                return (true, $"Sesi PO '{judul}' berhasil dibuka! Sekarang tambahkan produk ke sesi ini lewat Manajemen Produk. 🎉", idPO);
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap(), 0); }
            catch (Exception ex) { return (false, "Error: " + ex.Message, 0); }
        }

        public (bool sukses, string pesan) GasLuncurkanPO(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            // Guard clauses — validasi dulu, keluar cepat jika gagal
            if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(rekening) || string.IsNullOrWhiteSpace(jenis))
                return (false, "Spill judul, jenis PO, sama rekeningnya dong bestie, ga boleh kosong!");

            if (batasWaktu <= DateTime.Now)
                return (false, "Waktu tenggatnya masa di masa lalu? Move on dong, set ke masa depan!");

            if (idProduk <= 0)
                return (false, "Pilih dulu produknya ngab, masa buka jualan tapi ga ada barangnya?");

            try
            {
                bool result = this._poRepo.InsertPOAndUpdateProduct(
                    idPenjual, judul, jenis, rekening, batasWaktu, idProduk, targetKuota);

                return result
                    ? (true, "Yey! Sesi PO kamu berhasil dilaunching! 🎉 Semoga cuan deres!")
                    : (false, "Hmm, gagal nyimpen ke database nih.");
            }
            catch (Exception ex)
            {
                return (false, "Waduh error server: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) EditSesiPO(int idPo, string judulBaru, string jenisBaru, string rekeningBaru, DateTime batasWaktuBaru)
        {
            // Validasi input langsung di sini, tanpa buat objek dummy
            if (string.IsNullOrWhiteSpace(judulBaru) || judulBaru.Trim().Length < 5)
                return (false, "Judul PO minimal 5 karakter!");

            if (judulBaru.Trim().Length > 100)
                return (false, "Judul PO maksimal 100 karakter!");

            if (jenisBaru != "Biasa" && jenisBaru != "Gotong Royong")
                return (false, "Jenis PO hanya boleh 'Biasa' atau 'Gotong Royong'!");

            if (string.IsNullOrWhiteSpace(rekeningBaru) || rekeningBaru.Trim().Length < 10)
                return (false, "Info rekening minimal 10 karakter! Contoh: 'BCA 1234567890 a/n Nama'");

            if (batasWaktuBaru <= DateTime.Now)
                return (false, "Waktu tutup harus di masa depan!");

            try
            {
                bool berhasil = this._poRepo.UpdatePO(idPo, judulBaru.Trim(), jenisBaru, rekeningBaru.Trim(), batasWaktuBaru);
                return berhasil
                    ? (true, "Sesi PO berhasil diupdate!")
                    : (false, "PO tidak ditemukan atau sudah dihapus.");
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) TutupSesiPO(int idPo)
        {
            try
            {
                this._poRepo.SoftDelete(idPo);
                return (true, "Sesi PO berhasil ditutup dan dihapus (soft delete).");
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        public DataTable GetPOByPenjual(int idPenjual)
        {
            try { return this._poRepo.GetPOByPenjual(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }

        public DataTable GetPOAktifByPenjual(int idPenjual)
        {
            try { return this._poRepo.GetPOAktifByPenjual(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }
    }
}
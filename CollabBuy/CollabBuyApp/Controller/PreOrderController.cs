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
            Models.PreOrder poObj;
            try
            {
                DataTable dt = this._poRepo.GetById(idPo);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        int idPenjual;
                        if (row["id_penjual"] != DBNull.Value)
                        {
                            idPenjual = Convert.ToInt32(row["id_penjual"]);
                        }
                        else
                        {
                            idPenjual = 0;
                        }

                        string judulPo = row["judul_po"].ToString();
                        string jenisPo = row["jenis_po"].ToString();
                        string rekening = row["rekening"].ToString();

                        DateTime batasWaktu;
                        if (row["batas_waktu"] != DBNull.Value)
                        {
                            batasWaktu = Convert.ToDateTime(row["batas_waktu"]);
                        }
                        else
                        {
                            batasWaktu = DateTime.Now;
                        }

                        // =======================================================
                        // OOP BEST PRACTICE: Instansiasi Model dengan Constructor
                        // Sesuaikan urutan parameter ini dengan constructor di class PreOrder.cs milikmu!
                        // =======================================================
                        poObj = new Models.PreOrder(idPenjual, judulPo, jenisPo, rekening, batasWaktu);
                    }
                    else
                    {
                        poObj = null; 
                    }
                }
                else
                {
                    poObj = null; 
                }
            }
            catch (Exception)
            {
                poObj = null;
            }

            return poObj;
        }

        public int GetJumlahPoAktif()
        {
            int jumlah;
            try
            {
                DataTable dt = this._poRepo.GetSesiPOAktif("");

                if (dt != null)
                {
                    jumlah = dt.Rows.Count;
                }
                else
                {
                    jumlah = 0;
                }
            }
            catch (Exception)
            {
                jumlah = 0;
            }

            return jumlah;
        }

        public DataTable GetActiveSesiPO(string keyword)
        {
            DataTable dt;
            try
            {
                dt = this._poRepo.GetSesiPOAktif(keyword);
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        /// <summary>
        /// Mengembalikan semua produk aktif milik penjual (is_deleted = FALSE),
        /// termasuk yang sudah di dalam PO lain — agar satu produk bisa dimasukkan PO berkali-kali.
        /// </summary>
        public DataTable GetProdukTersedia(int idPenjual)
        {
            return this._poRepo.GetSemuaProdukAktif(idPenjual);
        }
        public (bool sukses, string pesan, int idPO) BukaSesiPOBaru(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu)
        {
            if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(rekening) || string.IsNullOrWhiteSpace(jenis))
                return (false, "Judul, jenis PO, dan rekening tidak boleh kosong!", 0);

            if (batasWaktu <= DateTime.Now)
                return (false, "Waktu tutup harus di masa depan!", 0);

            try
            {
                int idPO = this._poRepo.InsertPOSaja(idPenjual, judul, jenis, rekening, batasWaktu);
                return (true, $"Sesi PO '{judul}' berhasil dibuka! Sekarang tambahkan produk ke sesi ini lewat Manajemen Produk. 🎉", idPO);
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message, 0);
            }
        }
        public (bool sukses, string pesan) GasLuncurkanPO(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            (bool sukses, string pesan) hasil;

            if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(rekening) || string.IsNullOrWhiteSpace(jenis))
            {
                hasil = (false, "Spill judul, jenis PO, sama rekeningnya dong bestie, ga boleh kosong!");
            }
            else
            {
                if (batasWaktu <= DateTime.Now)
                {
                    hasil = (false, "Waktu tenggatnya masa di masa lalu? Move on dong, set ke masa depan!");
                }
                else
                {
                    if (idProduk <= 0)
                    {
                        hasil = (false, "Pilih dulu produknya ngab, masa buka jualan tapi ga ada barangnya?");
                    }
                    else
                    {
                        try
                        {
                            bool result = this._poRepo.InsertPOAndUpdateProduct(idPenjual, judul, jenis, rekening, batasWaktu, idProduk, targetKuota);

                            if (result)
                            {
                                hasil = (true, "Yey! Sesi PO kamu berhasil dilaunching! 🎉 Semoga cuan deres!");
                            }
                            else
                            {
                                hasil = (false, "Hmm, gagal nyimpen ke database nih.");
                            }
                        }
                        catch (Exception ex)
                        {
                            hasil = (false, "Waduh error server: " + ex.Message);
                        }
                    }
                }
            }

            return hasil;
        }
        public (bool sukses, string pesan) EditSesiPO(int idPo, string judulBaru, string jenisBaru, string rekeningBaru, DateTime batasWaktuBaru)
        {
            try
            {
                // Validasi lewat Model
                PreOrder po = new PreOrder(0, judulBaru, jenisBaru, rekeningBaru, batasWaktuBaru);
                bool berhasil = this._poRepo.UpdatePO(idPo, judulBaru, jenisBaru, rekeningBaru, batasWaktuBaru);
                if (berhasil)
                {
                    return (true, "Sesi PO berhasil diupdate!");
                }
                else
                {
                    return (false, "PO tidak ditemukan atau sudah dihapus.");
                }
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
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
                bool berhasil = this._poRepo.SoftDeletePO(idPo);
                if (berhasil)
                {
                    return (true, "Sesi PO berhasil ditutup dan dihapus (soft delete).");
                }
                else
                {
                    return (false, "PO tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        public DataTable GetPOByPenjual(int idPenjual)
        {
            try
            {
                return this._poRepo.GetPOByPenjual(idPenjual);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal memuat sesi PO dari database: " + ex.Message, ex);
            }
        }
    }
}
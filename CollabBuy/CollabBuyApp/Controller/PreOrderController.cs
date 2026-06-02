using System;
using System.Data;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Models;

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
                // PERBAIKAN: Tangkap sebagai DataTable dulu, lalu mapping ke Model!
                DataTable dt = this._poRepo.GetById(idPo);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        // Ekstraksi data dengan proteksi DBNull
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
                        poObj = null; // Data tidak ditemukan
                    }
                }
                else
                {
                    poObj = null; // Tabel kosong/null
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

        public DataTable GetProdukTersedia(int idPenjual)
        {
            DataTable dt;
            try
            {
                dt = this._poRepo.GetProdukTanpaPO(idPenjual);
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
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
    }
}
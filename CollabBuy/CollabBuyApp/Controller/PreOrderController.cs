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
        private readonly UserRepository _userRepo;

        public PreOrderController()
        {
            this._poRepo = new PreOrderRepository();
            this._userRepo = new UserRepository();
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
                    : DateTime.Now.AddDays(1);

                var po = new Models.PreOrder(idPenjual, judulPo, jenisPo, rekening, batasWaktu);
                po.TutupOtomatisJikaBasi();

                // Attach produk berdasarkan idPo (bukan idPenjual) menggunakan kolom yang
                // benar-benar tersedia dari vw_katalog_produk via GetProdukDalamPO()
                ProductController pc = new ProductController();
                DataTable dtProduk = pc.GetProdukDalamPO(idPo);

                if (dtProduk != null)
                {
                    foreach (DataRow p in dtProduk.Rows)
                    {
                        try
                        {
                            // Kolom tersedia: id_produk, nama_produk, harga_dasar,
                            // harga_diskon, target_kuota, terpesan, jenis_po
                            // id_kategori & id_penjual tidak ada di view ini,
                            // gunakan nilai default yang aman
                            var produk = new Models.Product(
                                idPenjual,                              // id_penjual dari PO (bukan dari view)
                                0,                                      // id_kategori tidak tersedia, default 0
                                p["nama_produk"].ToString(),
                                p["harga_dasar"] != DBNull.Value
                                    ? Convert.ToInt32(p["harga_dasar"]) : 0
                            );

                            produk.IdProduk = Convert.ToInt32(p["id_produk"]);

                            if (p["harga_diskon"] != DBNull.Value)
                                produk.HargaDiskon = Convert.ToInt32(p["harga_diskon"]);

                            if (p["target_kuota"] != DBNull.Value)
                                produk.TargetKuota = Convert.ToInt32(p["target_kuota"]);

                            if (p["terpesan"] != DBNull.Value)
                            {
                                int jumlahTerpesan = Convert.ToInt32(p["terpesan"]);
                                if (jumlahTerpesan > 0)
                                    produk.TambahPesanan(jumlahTerpesan);
                            }

                            produk.JenisPo = p["jenis_po"] != DBNull.Value
                                ? p["jenis_po"].ToString() : jenisPo;

                            po.TambahProduk(produk);
                        }
                        catch (Exception exProduk)
                        {
                            // Skip produk bermasalah, jangan batalkan seluruh PO
                            Console.WriteLine($"[GetPreOrder] Skip produk: {exProduk.Message}");
                        }
                    }
                }

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

        // =======================================================
        // TIMPA METHOD BukaSesiPOBaru
        // =======================================================
        public (bool sukses, string pesan, int idPO) BukaSesiPOBaru(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu)
        {
            try
            {
                // 1. Instansiasi Model untuk memicu Guard Clause (Enkapsulasi)
                Models.PreOrder poValidasi = new Models.PreOrder(idPenjual, judul, jenis, rekening, batasWaktu);
                poValidasi.BukaSesiBaru(batasWaktu);

                // 2. Cek verifikasi penjual
                Models.Penjual penjual = this._userRepo.GetById(idPenjual) as Models.Penjual;
                if (penjual == null)
                    throw new InvalidOrderException("Akun penjual tidak ditemukan.", "id_penjual", "PO_PENJUAL_NOT_FOUND");
                if (!penjual.ApakahBisaBukaLapak())
                    throw new InvalidOrderException("Akun penjual belum diverifikasi atau sedang diblokir. Hubungi admin.", "status_akun", "PO_PENJUAL_BANNED");

                int idPO = this._poRepo.InsertPOSaja(idPenjual, judul, jenis, rekening, batasWaktu);
                return (true, $"Sesi PO '{judul}' berhasil dibuka! 🎉", idPO);
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap(), 0);
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message, 0);
            }
        }

        // =======================================================
        // TIMPA METHOD GasLuncurkanPO
        // =======================================================
        public (bool sukses, string pesan) GasLuncurkanPO(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu, int idProduk, int targetKuota)
        {
            try
            {
                if (idProduk <= 0)
                    throw new InvalidOrderException("Pilih dulu produknya ngab, masa buka jualan tapi ga ada barangnya?", "id_produk", "PO_PRODUK_KOSONG");

                // 1. Paksa pembuatan Objek Model agar Guard Clause beraksi
                Models.PreOrder poValidasi = new Models.PreOrder(idPenjual, judul, jenis, rekening, batasWaktu);
                poValidasi.BukaSesiBaru(batasWaktu);

                // 2. Cek izin lapak
                Models.Penjual penjual = this._userRepo.GetById(idPenjual) as Models.Penjual;
                if (penjual == null || !penjual.ApakahBisaBukaLapak())
                    throw new InvalidOrderException("Akun penjual belum diverifikasi atau sedang diblokir.", "status_akun", "PO_PENJUAL_BANNED");

                bool result = this._poRepo.InsertPOAndUpdateProduct(idPenjual, judul, jenis, rekening, batasWaktu, idProduk, targetKuota);

                return result
                    ? (true, "Yey! Sesi PO kamu berhasil dilaunching! 🎉 Semoga cuan deres!")
                    : (false, "Hmm, gagal nyimpen ke database nih.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Waduh error server: " + ex.Message);
            }
        }

        // =======================================================
        // TIMPA METHOD EditSesiPO
        // =======================================================
        public (bool sukses, string pesan) EditSesiPO(int idPo, string judulBaru, string jenisBaru, string rekeningBaru, DateTime batasWaktuBaru)
        {
            try
            {
                // Membuat objek menggunakan idPenjual = 1 sebagai dummy agar tidak error di konstruktor.
                // Tujuannya MURNI agar setter C# melakukan validasi rule bisnis.
                Models.PreOrder poValidasi = new Models.PreOrder(1, judulBaru, jenisBaru, rekeningBaru, batasWaktuBaru);
                poValidasi.BukaSesiBaru(batasWaktuBaru);

                bool berhasil = this._poRepo.UpdatePO(idPo, judulBaru.Trim(), jenisBaru, rekeningBaru.Trim(), batasWaktuBaru);
                return berhasil
                    ? (true, "Sesi PO berhasil diupdate!")
                    : (false, "PO tidak ditemukan atau sudah dihapus.");
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
        public bool CekPoBerjalan(int idPo)
        {
            try
            {
                Models.PreOrder po = this.GetPreOrder(idPo);
                return po != null && po.ApakahPoBerjalan();
            }
            catch { return false; }
        }
    }
}
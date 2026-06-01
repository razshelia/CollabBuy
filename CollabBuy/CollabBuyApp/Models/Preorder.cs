using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Sesi Pre-Order.
    /// Mengimplementasikan IValidatable, IStatusTrackable, dan IQuotaTrackable (Agregat).
    /// </summary>
    public class PreOrder : IValidatable, IStatusTrackable, IQuotaTrackable
    {
        // === PRIVATE FIELDS ===
        private int _idPo;
        private int _idPenjual;
        private string _judulPo;
        private string _jenisPo;
        private string _infoRekening;
        private DateTime _batasWaktu;
        private bool _isAktif;

        // Struktur Data In-Memory (Agregasi)
        private List<Product> _produkDiPo;

        // === KONSTRUKTOR ===
        public PreOrder(int idPenjual, string judulPo, string jenisPo, string infoRekening, DateTime batasWaktu)
        {
            this.SetIdPenjual(idPenjual);
            this.SetJudulPo(judulPo);
            this.SetJenisPo(jenisPo);
            this.SetInfoRekening(infoRekening);
            this.SetBatasWaktu(batasWaktu);

            this._isAktif = true;
            this._produkDiPo = new List<Product>();
        }

        // === GETTER & SETTER (STRICT ENCAPSULATION IF-ELSE) ===
        public int GetIdPo()
        {
            return this._idPo;
        }

        public void SetIdPo(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID PO tidak valid!", "id_po", "PO_ID_INVALID");
            }
            else
            {
                this._idPo = id;
            }
        }

        public int GetIdPenjual()
        {
            return this._idPenjual;
        }

        public void SetIdPenjual(int idPenjual)
        {
            if (idPenjual <= 0)
            {
                throw new InvalidOrderException("ID Penjual tidak valid!", "id_penjual", "PO_PENJUAL_INVALID");
            }
            else
            {
                this._idPenjual = idPenjual;
            }
        }

        public string GetJudulPo()
        {
            return this._judulPo;
        }

        public void SetJudulPo(string judul)
        {
            if (string.IsNullOrWhiteSpace(judul))
            {
                throw new InvalidOrderException("Judul PO wajib diisi!", "judul_po", "PO_JUDUL_KOSONG");
            }
            else
            {
                this._judulPo = judul.Trim();
            }
        }

        public string GetJenisPo()
        {
            return this._jenisPo;
        }

        public void SetJenisPo(string jenis)
        {
            if (string.IsNullOrWhiteSpace(jenis))
            {
                throw new InvalidOrderException("Jenis PO tidak boleh kosong!", "jenis_po", "PO_JENIS_KOSONG");
            }
            else if (jenis != "Biasa" && jenis != "Gotong Royong")
            {
                throw new InvalidOrderException("Jenis PO hanya boleh 'Biasa' atau 'Gotong Royong'!", "jenis_po", "PO_JENIS_INVALID");
            }
            else
            {
                this._jenisPo = jenis.Trim();
            }
        }

        public string GetInfoRekening()
        {
            return this._infoRekening;
        }

        public void SetInfoRekening(string rekening)
        {
            if (string.IsNullOrWhiteSpace(rekening))
            {
                throw new InvalidOrderException("Info rekening wajib diisi!", "info_rekening", "PO_REK_KOSONG");
            }
            else
            {
                this._infoRekening = rekening.Trim();
            }
        }

        public DateTime GetBatasWaktu()
        {
            return this._batasWaktu;
        }

        public void SetBatasWaktu(DateTime batas)
        {
            if (batas < DateTime.Now)
            {
                throw new InvalidOrderException("Batas waktu PO tidak boleh di masa lalu!", "batas_waktu", "PO_WAKTU_INVALID");
            }
            else
            {
                this._batasWaktu = batas;
            }
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & BEHAVIOR
        // =========================================================

        public void TambahProduk(Product produk)
        {
            if (produk == null)
            {
                throw new InvalidOrderException("Produk yang ditambahkan tidak boleh null!", "produk", "PO_PRODUK_NULL");
            }
            else if (produk.GetIdPenjual() != this._idPenjual)
            {
                throw new InvalidOrderException("Produk ini bukan milik penjual PO!", "id_penjual", "PO_KEPEMILIKAN_INVALID");
            }
            else
            {
                this._produkDiPo.Add(produk);
            }
        }

        public List<Product> GetSemuaProduk()
        {
            return this._produkDiPo;
        }

        /// <summary>
        /// Mengecek secara real-time apakah PO masih bisa menerima pesanan.
        /// </summary>
        public bool ApakahPoBerjalan()
        {
            bool isBerjalan;

            if (this._isAktif && this._batasWaktu > DateTime.Now)
            {
                isBerjalan = true;
            }
            else
            {
                isBerjalan = false;
            }

            return isBerjalan;
        }

        /// <summary>
        /// Menghasilkan string sisa waktu atau status penutupan. 
        /// Sangat efisien untuk dipanggil langsung oleh UI (Label).
        /// </summary>
        public string DapatkanSisaWaktu()
        {
            string sisaTeks;
            TimeSpan selisih = this._batasWaktu - DateTime.Now;

            if (!this._isAktif)
            {
                sisaTeks = "🔒 PO Sudah Ditutup Manual";
            }
            else if (selisih.TotalSeconds <= 0)
            {
                sisaTeks = "⏳ Waktu Habis!";
            }
            else if (selisih.TotalDays >= 1)
            {
                sisaTeks = $"Sisa {selisih.Days} Hari {selisih.Hours} Jam";
            }
            else
            {
                sisaTeks = $"Sisa {selisih.Hours} Jam {selisih.Minutes} Menit";
            }

            return sisaTeks;
        }

        /// <summary>
        /// Method maintenance. Dipanggil saat meload data agar PO basi otomatis nonaktif di memori.
        /// </summary>
        public void TutupOtomatisJikaBasi()
        {
            if (this._batasWaktu <= DateTime.Now && this._isAktif)
            {
                this._isAktif = false;
            }
            else
            {
                // Tetap biarkan sesuai status aslinya
                bool statusTetap = this._isAktif;
            }
        }

        /// <summary>
        /// Menghitung potensi omzet penjual dari seluruh barang yang ada di PO ini.
        /// </summary>
        public long EstimasiTotalPendapatan()
        {
            long total = 0;
            if (this._produkDiPo != null)
            {
                foreach (Product p in this._produkDiPo)
                {
                    total = total + (p.GetHargaDasar() * p.GetTargetKuota());
                }
            }
            else
            {
                total = 0;
            }
            return total;
        }

        public string DapatkanInfoCardPO()
        {
            string info;
            string tipe = this._jenisPo == "Gotong Royong" ? "🤝" : "📦";

            if (string.IsNullOrWhiteSpace(this._judulPo))
            {
                info = "Data PO Tidak Lengkap";
            }
            else
            {
                info = $"{tipe} {this._judulPo} | {this.DapatkanSisaWaktu()}";
            }

            return info;
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            bool validasiJudul;
            bool validasiJenis;

            if (string.IsNullOrWhiteSpace(this._judulPo))
            {
                throw new InvalidOrderException("Validasi gagal: Judul PO kosong.", "judul_po", "PO_INVALID");
            }
            else
            {
                validasiJudul = true;
            }

            if (this._jenisPo != "Biasa" && this._jenisPo != "Gotong Royong")
            {
                throw new InvalidOrderException("Validasi gagal: Jenis PO tidak sesuai.", "jenis_po", "PO_INVALID");
            }
            else
            {
                validasiJenis = true;
            }
        }

        // === IMPLEMENTASI IStatusTrackable ===
        public string GetStatus()
        {
            string status;
            if (this._isAktif)
            {
                status = "Aktif";
            }
            else
            {
                status = "Tutup";
            }
            return status;
        }

        public bool BisaDiubahKe(string statusBaru)
        {
            bool bisaUbah;
            string statusSaatIni = this.GetStatus();

            if (statusSaatIni == "Aktif" && statusBaru == "Tutup")
            {
                bisaUbah = true;
            }
            else
            {
                bisaUbah = false;
            }

            return bisaUbah;
        }

        public void UbahStatus(string statusBaru)
        {
            if (!this.BisaDiubahKe(statusBaru))
            {
                throw new InvalidOrderException("Status PO tidak bisa diubah ke '" + statusBaru + "'!", "is_aktif", "PO_STATUS_INVALID");
            }
            else
            {
                this._isAktif = false;
            }
        }

        // === IMPLEMENTASI IQuotaTrackable (Agregat) ===
        public int GetTargetKuota()
        {
            int total = 0;
            if (this._produkDiPo != null)
            {
                foreach (Product p in this._produkDiPo)
                {
                    total = total + p.GetTargetKuota();
                }
            }
            else
            {
                total = 0;
            }
            return total;
        }

        public int GetTerpesan()
        {
            int total = 0;
            if (this._produkDiPo != null)
            {
                foreach (Product p in this._produkDiPo)
                {
                    total = total + p.GetTerpesan();
                }
            }
            else
            {
                total = 0;
            }
            return total;
        }

        public int GetSisaKuota()
        {
            int sisa = this.GetTargetKuota() - this.GetTerpesan();
            return sisa;
        }

        public bool IsKuotaTerpenuhi()
        {
            bool semuaTerpenuhi = true;

            if (this._produkDiPo == null || this._produkDiPo.Count == 0)
            {
                semuaTerpenuhi = false;
            }
            else
            {
                foreach (Product p in this._produkDiPo)
                {
                    if (!p.IsKuotaTerpenuhi())
                    {
                        semuaTerpenuhi = false;
                        break;
                    }
                    else
                    {
                        // Lanjut cek produk berikutnya
                        bool lanjutCek = true;
                    }
                }
            }

            return semuaTerpenuhi;
        }
    }
}
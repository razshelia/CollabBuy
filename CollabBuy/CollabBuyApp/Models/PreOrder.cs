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
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idPo;
        private int _idPenjual;
        private string _judulPo;
        private string _jenisPo;
        private string _infoRekening;
        private DateTime _batasWaktu;
        private bool _isAktif;
        private List<Product> _produkDiPo; // Struktur Data In-Memory (Agregasi)

        // === PROPERTIES ===
        public int IdPo
        {
            get { return this._idPo; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID PO tidak valid!", "id_po", "PO_ID_INVALID");
                this._idPo = value;
            }
        }

        public int IdPenjual
        {
            get { return this._idPenjual; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Penjual tidak valid!", "id_penjual", "PO_PENJUAL_INVALID");
                this._idPenjual = value;
            }
        }

        public string JudulPo
        {
            get { return this._judulPo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Judul PO wajib diisi!", "judul_po", "PO_JUDUL_KOSONG");

                if (value.Trim().Length < 5)
                    throw new InvalidOrderException("Judul PO minimal 5 karakter!", "judul_po", "PO_JUDUL_PENDEK");

                if (value.Trim().Length > 100)
                    throw new InvalidOrderException("Judul PO maksimal 100 karakter!", "judul_po", "PO_JUDUL_PANJANG");

                this._judulPo = value.Trim();
            }
        }

        public string JenisPo
        {
            get { return this._jenisPo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Jenis PO tidak boleh kosong!", "jenis_po", "PO_JENIS_KOSONG");

                if (value != "Biasa" && value != "Gotong Royong")
                    throw new InvalidOrderException("Jenis PO hanya boleh 'Biasa' atau 'Gotong Royong'!", "jenis_po", "PO_JENIS_INVALID");

                this._jenisPo = value.Trim();
            }
        }

        public string InfoRekening
        {
            get { return this._infoRekening; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Info rekening wajib diisi!", "info_rekening", "PO_REK_KOSONG");

                if (value.Trim().Length < 10)
                    throw new InvalidOrderException("Info rekening minimal 10 karakter! Contoh: 'BCA 1234567890 a/n Nama'", "info_rekening", "PO_REK_PENDEK");

                if (value.Trim().Length > 200)
                    throw new InvalidOrderException("Info rekening maksimal 200 karakter!", "info_rekening", "PO_REK_PANJANG");

                this._infoRekening = value.Trim();
            }
        }

        public DateTime BatasWaktu
        {
            get { return this._batasWaktu; }
            set
            {
                if (value < DateTime.Now)
                    throw new InvalidOrderException("Batas waktu PO tidak boleh di masa lalu!", "batas_waktu", "PO_WAKTU_INVALID");

                this._batasWaktu = value;
            }
        }

        // List hanya menggunakan get agar aman
        public List<Product> ProdukDiPo
        {
            get { return this._produkDiPo; }
        }

        // === KONSTRUKTOR ===
        public PreOrder(int idPenjual, string judulPo, string jenisPo, string infoRekening, DateTime batasWaktu)
        {
            this.IdPenjual = idPenjual;
            this.JudulPo = judulPo;
            this.JenisPo = jenisPo;
            this.InfoRekening = infoRekening;
            this.BatasWaktu = batasWaktu;

            this._isAktif = true;
            this._produkDiPo = new List<Product>();
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & BEHAVIOR
        // =========================================================

        public void TambahProduk(Product produk)
        {
            if (produk == null)
                throw new InvalidOrderException("Produk yang ditambahkan tidak boleh null!", "produk", "PO_PRODUK_NULL");

            if (produk.IdPenjual != this._idPenjual) // Sesuaikan nama method jika sudah diubah jadi Property di kelas Product
                throw new InvalidOrderException("Produk ini bukan milik penjual PO!", "id_penjual", "PO_KEPEMILIKAN_INVALID");

            this._produkDiPo.Add(produk);
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
            return this._isAktif && this._batasWaktu > DateTime.Now;
        }

        /// <summary>
        /// Menghasilkan string sisa waktu atau status penutupan. 
        /// Sangat efisien untuk dipanggil langsung oleh UI (Label).
        /// </summary>
        public string DapatkanSisaWaktu()
        {
            if (!this._isAktif) return "🔒 PO Sudah Ditutup Manual";

            TimeSpan selisih = this._batasWaktu - DateTime.Now;

            if (selisih.TotalSeconds <= 0) return "⏳ Waktu Habis!";
            if (selisih.TotalDays >= 1) return $"Sisa {selisih.Days} Hari {selisih.Hours} Jam";

            return $"Sisa {selisih.Hours} Jam {selisih.Minutes} Menit";
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
        }

        /// <summary>
        /// Menghitung potensi omzet penjual dari seluruh barang yang ada di PO ini.
        /// </summary>
        public long EstimasiTotalPendapatan()
        {
            if (this._produkDiPo == null) return 0;

            long total = 0;
            foreach (Product p in this._produkDiPo)
            {
                // Asumsi GetHargaDasar() dan GetTargetKuota() masih method. Ubah ke property (p.HargaDasar) jika sudah di-refactor.
                total += (p.HargaDasar * p.GetTargetKuota());
            }
            return total;
        }

        public string DapatkanInfoCardPO()
        {
            if (string.IsNullOrWhiteSpace(this._judulPo)) return "Data PO Tidak Lengkap";

            string tipe = this._jenisPo == "Gotong Royong" ? "🤝" : "📦";
            return $"{tipe} {this._judulPo} | {this.DapatkanSisaWaktu()}";
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this._judulPo))
                throw new InvalidOrderException("Validasi gagal: Judul PO kosong.", "judul_po", "PO_INVALID");

            if (this._jenisPo != "Biasa" && this._jenisPo != "Gotong Royong")
                throw new InvalidOrderException("Validasi gagal: Jenis PO tidak sesuai.", "jenis_po", "PO_INVALID");
        }

        // === IMPLEMENTASI IStatusTrackable ===
        public string GetStatus()
        {
            return this._isAktif ? "Aktif" : "Tutup";
        }

        public bool BisaDiubahKe(string statusBaru)
        {
            return this.GetStatus() == "Aktif" && statusBaru == "Tutup";
        }

        public void UbahStatus(string statusBaru)
        {
            if (!this.BisaDiubahKe(statusBaru))
            {
                throw new InvalidOrderException($"Status PO tidak bisa diubah ke '{statusBaru}'!", "is_aktif", "PO_STATUS_INVALID");
            }
            this._isAktif = false;
        }

        // === IMPLEMENTASI IQuotaTrackable (Agregat) ===
        public int GetTargetKuota()
        {
            if (this._produkDiPo == null) return 0;

            int total = 0;
            foreach (Product p in this._produkDiPo)
            {
                total += p.GetTargetKuota();
            }
            return total;
        }

        public int GetTerpesan()
        {
            if (this._produkDiPo == null) return 0;

            int total = 0;
            foreach (Product p in this._produkDiPo)
            {
                total += p.GetTerpesan();
            }
            return total;
        }

        public int GetSisaKuota()
        {
            return this.GetTargetKuota() - this.GetTerpesan();
        }

        public bool IsKuotaTerpenuhi()
        {
            if (this._produkDiPo == null || this._produkDiPo.Count == 0) return false;

            // Jika ada satu saja produk yang kuotanya belum terpenuhi, maka PO belum komplit
            foreach (Product p in this._produkDiPo)
            {
                if (!p.IsKuotaTerpenuhi()) return false;
            }

            return true;
        }
    }
}
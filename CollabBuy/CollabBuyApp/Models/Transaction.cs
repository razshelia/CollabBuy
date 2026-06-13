using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Transaksi utama (Keranjang/Kuitansi).
    /// Menerapkan IValidatable, ICalculatable, IStatusTrackable, IApprovable.
    /// Dilengkapi dengan Rich Domain Model Behaviors.
    /// </summary>
    public class Transaction : IValidatable, ICalculatable, IStatusTrackable, IApprovable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idTransaksi;
        private int _idPembeli;
        private DateTime _tanggalTransaksi;
        private string _statusPesanan;
        private bool _isValid;
        private string _alasanPenolakan;
        private byte[] _buktiBayar;
        private List<TransactionDetail> _details; // Struktur Data In-Memory (Agregasi / Komposisi)

        // === PROPERTIES (Get & Set dalam satu blok dengan Guard Clauses) ===
        public int IdTransaksi
        {
            get { return this._idTransaksi; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Transaksi tidak valid!", "id_transaksi", "TRX_ID_INVALID");
                this._idTransaksi = value;
            }
        }

        public int IdPembeli
        {
            get { return this._idPembeli; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Pembeli tidak valid!", "id_pembeli", "TRX_PEMBELI_INVALID");
                this._idPembeli = value;
            }
        }

        public DateTime TanggalTransaksi
        {
            get { return this._tanggalTransaksi; }
            set
            {
                if (value == DateTime.MinValue)
                    throw new InvalidOrderException("Tanggal transaksi tidak valid!", "tanggal", "TRX_TGL_INVALID");
                this._tanggalTransaksi = value;
            }
        }

        // GANTI DENGAN INI:
        public byte[] BuktiBayar
        {
            get { return this._buktiBayar; }
            set
            {
                // Validasi ukuran saja; null/kosong diizinkan (transaksi belum upload bukti bayar)
                if (value != null && value.Length > 5242880) // 5MB
                    throw new InvalidOrderException("Ukuran file bukti bayar maksimal 5MB!", "bukti_bayar", "BUKTI_OVERSIZE");

                this._buktiBayar = value;
            }
        }

        public string AlasanPenolakan
        {
            get { return this._alasanPenolakan; }
        }

        // List hanya menggunakan get agar aman dari timpaan data kosong pihak luar
        public List<TransactionDetail> Details
        {
            get { return this._details; }
        }

        // === KONSTRUKTOR ===
        public Transaction(int idPembeli)
        {
            this.IdPembeli = idPembeli; // Otomatis memicu validasi setter properti

            this._tanggalTransaksi = DateTime.Now;
            this._statusPesanan = "Menunggu";
            this._isValid = false;
            this._alasanPenolakan = "";
            this._buktiBayar = null;
            this._details = new List<TransactionDetail>();
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        public void TambahDetail(TransactionDetail detail)
        {
            if (detail == null)
                throw new InvalidOrderException("Detail transaksi tidak boleh null!", "details", "DETAIL_NULL");

            detail.Validate();
            this._details.Add(detail);
        }

        public List<TransactionDetail> GetSemuaDetail()
        {
            return this._details;
        }

        public void KosongkanKeranjang()
        {
            if (this._details == null)
            {
                this._details = new List<TransactionDetail>();
                return;
            }
            this._details.Clear();
        }

        /// <summary>
        /// Menghitung total seluruh kuantitas (pcs) dari semua barang di keranjang.
        /// </summary>
        public int DapatkanTotalItem()
        {
            if (this._details == null || this._details.Count == 0) return 0;

            int totalQty = 0;
            foreach (TransactionDetail detail in this._details)
            {
                totalQty += detail.JumlahPesanan;
            }
            return totalQty;
        }

        public string DapatkanFormatTagihanUI()
        {
            long totalTagihan = this.HitungTotal();
            return totalTagihan == 0 ? "Rp 0 (Gratis / Kosong)" : $"Rp {totalTagihan:N0}";
        }

        public string DapatkanFormatTagihanUI(long totalOverride)
        {
            // Overload untuk saat total sudah dihitung di luar (dari DB atau controller)
            return totalOverride == 0 ? "Rp 0 (Gratis / Kosong)" : $"Rp {totalOverride:N0}";
        }

        public bool ApakahSudahDibayar()
        {
            // Cukup evaluasi kondisi array byte langsung
            return this._buktiBayar != null && this._buktiBayar.Length > 0;
        }

        public string DapatkanStatusPembayaranUI()
        {
            if (this._isValid) return "✅ Pembayaran Diterima";

            if (this.ApakahSudahDibayar() && !this._isValid && string.IsNullOrWhiteSpace(this._alasanPenolakan))
            {
                return "⏳ Menunggu Verifikasi Admin";
            }

            if (!string.IsNullOrWhiteSpace(this._alasanPenolakan))
            {
                return "❌ Pembayaran Ditolak: " + this._alasanPenolakan;
            }

            return "⚠️ Belum Bayar";
        }

        public string DapatkanInfoResi()
        {
            if (this._idTransaksi <= 0) return "Transaksi Belum Disimpan (Keranjang Aktif)";

            return $"INV-{this._idTransaksi:D6} | {this._tanggalTransaksi.ToString("dd/MM/yyyy")} | Status: {this._statusPesanan}";
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            // Meratakan nested-if menjadi Guard Clauses independen yang sangat rapi
            if (this._idPembeli <= 0)
                throw new InvalidOrderException("ID Pembeli tidak valid!", "id_pembeli", "PEMBELI_INVALID");

            if (this._details == null || this._details.Count == 0)
                throw new InvalidOrderException("Transaksi harus memiliki minimal 1 item pesanan!", "details", "DETAIL_KOSONG");

            foreach (TransactionDetail detail in this._details)
            {
                detail.Validate();
            }
        }

        // === IMPLEMENTASI ICalculatable ===
        public long HitungTotal()
        {
            if (this._details == null) return 0;

            long total = 0;
            foreach (TransactionDetail detail in this._details)
            {
                total += detail.HitungTotal();
            }
            return total;
        }

        public long HitungDiskon()
        {
            if (this._details == null) return 0;

            long totalDiskon = 0;
            foreach (TransactionDetail detail in this._details)
            {
                totalDiskon += detail.HitungDiskon();
            }
            return totalDiskon;
        }

        // === IMPLEMENTASI IStatusTrackable ===
        public string GetStatus()
        {
            return this._statusPesanan;
        }

        public bool BisaDiubahKe(string statusBaru)
        {
            // Merapikan mesin status (State Machine) menggunakan multi-return datar
            if (this._statusPesanan == "Menunggu" && statusBaru == "Diproses") return true;
            if (this._statusPesanan == "Diproses" && statusBaru == "Selesai") return true;
            if (this._statusPesanan == "Menunggu" && statusBaru == "Dibatalkan") return true;

            return false;
        }

        public void UbahStatus(string statusBaru)
        {
            if (!this.BisaDiubahKe(statusBaru))
            {
                throw new InvalidOrderException($"Transaksi tidak bisa diubah dari '{this._statusPesanan}' ke '{statusBaru}'!", "status_pesanan", "STATUS_TRANSITION_INVALID");
            }

            this._statusPesanan = statusBaru.Trim();
        }

        // === IMPLEMENTASI IApprovable ===
        public void Approve()
        {
            this._isValid = true;
            this._alasanPenolakan = "";
        }

        public void Reject(string alasan)
        {
            if (string.IsNullOrWhiteSpace(alasan))
                throw new InvalidOrderException("Alasan penolakan bukti bayar wajib diisi!", "alasan", "REJECT_EMPTY");

            this._isValid = false;
            this._alasanPenolakan = alasan.Trim();
        }

        public bool GetStatusPersetujuan()
        {
            return this._isValid;
        }
    }
}
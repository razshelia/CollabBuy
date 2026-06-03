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
        // === PRIVATE FIELDS ===
        private int _idTransaksi;
        private int _idPembeli;
        private DateTime _tanggalTransaksi;
        private string _statusPesanan;
        private bool _isValid;
        private string _alasanPenolakan;
        private byte[] _buktiBayar;

        // Struktur Data In-Memory (Agregasi / Komposisi)
        private List<TransactionDetail> _details;

        // === KONSTRUKTOR ===
        public Transaction(int idPembeli)
        {
            this.SetIdPembeli(idPembeli);

            this._tanggalTransaksi = DateTime.Now;
            this._statusPesanan = "Menunggu";
            this._isValid = false;
            this._alasanPenolakan = "";
            this._buktiBayar = null;
            this._details = new List<TransactionDetail>();
        }

        // === GETTER & SETTER (STRICT ENCAPSULATION IF-ELSE) ===
        public int GetIdTransaksi()
        {
            return this._idTransaksi;
        }

        public void SetIdTransaksi(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Transaksi tidak valid!", "id_transaksi", "TRX_ID_INVALID");
            }
            else
            {
                this._idTransaksi = id;
            }
        }

        public int GetIdPembeli()
        {
            return this._idPembeli;
        }

        public void SetIdPembeli(int idPembeli)
        {
            if (idPembeli <= 0)
            {
                throw new InvalidOrderException("ID Pembeli tidak valid!", "id_pembeli", "TRX_PEMBELI_INVALID");
            }
            else
            {
                this._idPembeli = idPembeli;
            }
        }

        public DateTime GetTanggalTransaksi()
        {
            return this._tanggalTransaksi;
        }

        public void SetTanggalTransaksi(DateTime tanggal)
        {
            if (tanggal == DateTime.MinValue)
            {
                throw new InvalidOrderException("Tanggal transaksi tidak valid!", "tanggal", "TRX_TGL_INVALID");
            }
            else
            {
                this._tanggalTransaksi = tanggal;
            }
        }

        public byte[] GetBuktiBayar()
        {
            return this._buktiBayar;
        }

        public void SetBuktiBayar(byte[] bukti)
        {
            if (bukti == null || bukti.Length == 0)
            {
                throw new InvalidOrderException("Bukti bayar tidak boleh kosong!", "bukti_bayar", "BUKTI_KOSONG");
            }
            else if (bukti.Length > 5242880) // 5MB
            {
                throw new InvalidOrderException("Ukuran file bukti bayar maksimal 5MB!", "bukti_bayar", "BUKTI_OVERSIZE");
            }
            else
            {
                this._buktiBayar = bukti;
            }
        }

        public string GetAlasanPenolakan()
        {
            return this._alasanPenolakan;
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        public void TambahDetail(TransactionDetail detail)
        {
            if (detail == null)
            {
                throw new InvalidOrderException("Detail transaksi tidak boleh null!", "details", "DETAIL_NULL");
            }
            else
            {
                detail.Validate();
                this._details.Add(detail);
            }
        }

        public List<TransactionDetail> GetSemuaDetail()
        {
            return this._details;
        }

        public void KosongkanKeranjang()
        {
            if (this._details != null)
            {
                this._details.Clear();
            }
            else
            {
                this._details = new List<TransactionDetail>();
            }
        }

        /// <summary>
        /// Menghitung total seluruh kuantitas (pcs) dari semua barang di keranjang.
        /// </summary>
        public int DapatkanTotalItem()
        {
            int totalQty = 0;

            if (this._details != null && this._details.Count > 0)
            {
                foreach (TransactionDetail detail in this._details)
                {
                    // PERBAIKAN: Memanggil method yang benar dari TransactionDetail
                    totalQty = totalQty + detail.GetJumlahPesanan();
                }
            }
            else
            {
                totalQty = 0;
            }

            return totalQty;
        }

        public string DapatkanFormatTagihanUI()
        {
            long totalTagihan = this.HitungTotal();
            string tagihanUi;

            if (totalTagihan == 0)
            {
                tagihanUi = "Rp 0 (Gratis / Kosong)";
            }
            else
            {
                tagihanUi = $"Rp {totalTagihan:N0}";
            }

            return tagihanUi;
        }

        public bool ApakahSudahDibayar()
        {
            bool sudahBayar;

            if (this._buktiBayar != null && this._buktiBayar.Length > 0)
            {
                sudahBayar = true;
            }
            else
            {
                sudahBayar = false;
            }

            return sudahBayar;
        }

        public string DapatkanStatusPembayaranUI()
        {
            string statusPembayaran;

            if (this._isValid)
            {
                statusPembayaran = "✅ Pembayaran Diterima";
            }
            else if (this.ApakahSudahDibayar() && !this._isValid && string.IsNullOrWhiteSpace(this._alasanPenolakan))
            {
                statusPembayaran = "⏳ Menunggu Verifikasi Admin";
            }
            else if (!string.IsNullOrWhiteSpace(this._alasanPenolakan))
            {
                statusPembayaran = "❌ Pembayaran Ditolak: " + this._alasanPenolakan;
            }
            else
            {
                statusPembayaran = "⚠️ Belum Bayar";
            }

            return statusPembayaran;
        }

        public string DapatkanInfoResi()
        {
            string resi;

            if (this._idTransaksi > 0)
            {
                resi = $"INV-{this._idTransaksi:D6} | {this._tanggalTransaksi.ToString("dd/MM/yyyy")} | Status: {this._statusPesanan}";
            }
            else
            {
                resi = "Transaksi Belum Disimpan (Keranjang Aktif)";
            }

            return resi;
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            bool validasiHeader;
            bool validasiDetail;

            if (this._idPembeli <= 0)
            {
                throw new InvalidOrderException("ID Pembeli tidak valid!", "id_pembeli", "PEMBELI_INVALID");
            }
            else
            {
                validasiHeader = true;
            }

            if (this._details == null || this._details.Count == 0)
            {
                throw new InvalidOrderException("Transaksi harus memiliki minimal 1 item pesanan!", "details", "DETAIL_KOSONG");
            }
            else
            {
                validasiDetail = true;
            }

            if (validasiHeader && validasiDetail)
            {
                foreach (TransactionDetail detail in this._details)
                {
                    detail.Validate();
                    bool itemValid = true; // Assignment mencegah body loop kosong jika validate sukses
                }
            }
            else
            {
                // Tidak mungkin masuk sini karena throw exception di atas
                bool skip = true;
            }
        }

        // === IMPLEMENTASI ICalculatable ===
        public long HitungTotal()
        {
            long total = 0;

            if (this._details != null)
            {
                foreach (TransactionDetail detail in this._details)
                {
                    total = total + detail.HitungTotal();
                }
            }
            else
            {
                total = 0;
            }

            return total;
        }

        public long HitungDiskon()
        {
            long totalDiskon = 0;

            if (this._details != null)
            {
                foreach (TransactionDetail detail in this._details)
                {
                    totalDiskon = totalDiskon + detail.HitungDiskon();
                }
            }
            else
            {
                totalDiskon = 0;
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
            bool bisaUbah;

            // PENJUAL: Menunggu → Diproses (WAJIB lewat Diproses dulu, tidak bisa langsung Selesai)
            if (this._statusPesanan == "Menunggu" && statusBaru == "Diproses")
            {
                bisaUbah = true;
            }
            // PENJUAL: Diproses → Selesai (setelah diproses baru bisa diselesaikan)
            else if (this._statusPesanan == "Diproses" && statusBaru == "Selesai")
            {
                bisaUbah = true;
            }
            // PEMBELI: Hanya boleh batal saat masih Menunggu (belum diproses)
            // Saat Diproses atau Selesai, pembeli TIDAK bisa batal (uang sudah dalam proses)
            else if (this._statusPesanan == "Menunggu" && statusBaru == "Dibatalkan")
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
                throw new InvalidOrderException($"Transaksi tidak bisa diubah dari '{this._statusPesanan}' ke '{statusBaru}'!", "status_pesanan", "STATUS_TRANSITION_INVALID");
            }
            else
            {
                this._statusPesanan = statusBaru.Trim();
            }
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
            {
                throw new InvalidOrderException("Alasan penolakan bukti bayar wajib diisi!", "alasan", "REJECT_EMPTY");
            }
            else
            {
                this._isValid = false;
                this._alasanPenolakan = alasan.Trim();
            }
        }

        public bool GetStatusPersetujuan()
        {
            return this._isValid;
        }
    }
}
using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models.Interfaces;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Transaksi utama (Keranjang/Kuitansi).
    /// Menerapkan IValidatable, ICalculatable, IStatusTrackable, IApprovable.
    /// 
    /// Pemetaan Database:
    /// - Tabel: transactions
    /// - Relasi Komposisi: Memiliki List<TransactionDetail> (In-Memory di RAM)
    /// - Kolom bukti_bayar: BYTEA (Menyimpan file gambar di database)
    /// - View: vw_transaksi_lengkap -> Diimplementasikan di HitungTotal() & HitungDiskon()
    /// </summary>
    public class Transaction : IValidatable, ICalculatable, IStatusTrackable, IApprovable
    {
        // === PRIVATE FIELDS ===
        private int _idTransaksi;
        private int _idPembeli; // Dulunya id_koordinator, diubah sesuai permintaan
        private DateTime _tanggalTransaksi;
        private string _statusPesanan;
        private bool _isValid;
        private string _alasanPenolakan;
        private byte[] _buktiBayar; // Tambahan untuk BYTEA di Database

        // Struktur Data In-Memory (Sub-bab 3.1 Laporan)
        // Menggunakan List untuk menampung detail transaksi di RAM sebelum di-flush ke DB
        private List<TransactionDetail> _details;


        // === KONSTRUKTOR ===
        public Transaction(int idPembeli)
        {
            _idPembeli = idPembeli;
            _tanggalTransaksi = DateTime.Now;
            _statusPesanan = "Menunggu";
            _isValid = false;
            _buktiBayar = null;
            _details = new List<TransactionDetail>();
        }


        // === PROPERTI (GETTER & SETTER) ===
        public int GetIdTransaksi() { return _idTransaksi; }
        public void SetIdTransaksi(int id) { _idTransaksi = id; }

        public int GetIdPembeli() { return _idPembeli; }

        public DateTime GetTanggalTransaksi() { return _tanggalTransaksi; }

        /// <summary>
        /// Mengambil data bukti bayar dalam format byte[] (BYTEA).
        /// </summary>
        public byte[] GetBuktiBayar()
        {
            return _buktiBayar;
        }

        /// <summary>
        /// Menetapkan bukti bayar dengan validasi ukuran file.
        /// Ditangani di Model (Sub-bab 3.2 Laporan: Business Rule Validation).
        /// </summary>
        public void SetBuktiBayar(byte[] bukti)
        {
            // Validasi: Maksimal ukuran file 5MB (5 * 1024 * 1024 = 5242880 byte)
            if (bukti != null && bukti.Length > 5242880)
            {
                throw new InvalidOrderException("Ukuran file bukti bayar maksimal 5MB!", "bukti_bayar", "BUKTI_OVERSIZE");
            }
            _buktiBayar = bukti;
        }


        // === METHOD BISNIS LOGIC ===

        /// <summary>
        /// Menambahkan item ke dalam keranjang transaksi di memori (RAM).
        /// Ini membuktikan "Data Structure In-Memory" sesuai Sub-bab 3.1 laporan.
        /// </summary>
        public void TambahDetail(TransactionDetail detail)
        {
            if (detail == null)
            {
                throw new InvalidOrderException("Detail transaksi tidak boleh null!", "details", "DETAIL_NULL");
            }

            detail.Validate();
            _details.Add(detail);
        }

        /// <summary>
        /// Mengambil seluruh list detail transaksi (untuk di-loop oleh Repository saat save).
        /// </summary>
        public List<TransactionDetail> GetSemuaDetail()
        {
            return _details;
        }


        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (_idPembeli <= 0)
            {
                throw new InvalidOrderException("ID Pembeli tidak valid!", "id_pembeli", "PEMBELI_INVALID");
            }
            if (_details.Count == 0)
            {
                throw new InvalidOrderException("Transaksi harus memiliki minimal 1 item pesanan!", "details", "DETAIL_KOSONG");
            }

            // Validasi setiap item di dalam list
            foreach (TransactionDetail detail in _details)
            {
                detail.Validate();
            }
        }


        // === IMPLEMENTASI ICalculatable ===
        /// <summary>
        /// Total Tagihan Keseluruhan.
        /// Pemetaan DB: Kolom total_tagihan dari vw_transaksi_lengkap.
        /// </summary>
        public long HitungTotal()
        {
            long total = 0;
            foreach (TransactionDetail detail in _details)
            {
                total = total + detail.HitungTotal();
            }
            return total;
        }

        /// <summary>
        /// Total Cashback/Refund Keseluruhan.
        /// Pemetaan DB: Kolom total_cashback dari vw_transaksi_lengkap.
        /// </summary>
        public long HitungDiskon()
        {
            long totalDiskon = 0;
            foreach (TransactionDetail detail in _details)
            {
                totalDiskon = totalDiskon + detail.HitungDiskon();
            }
            return totalDiskon;
        }


        // === IMPLEMENTASI IStatusTrackable ===
        public string GetStatus()
        {
            return _statusPesanan;
        }

        public bool BisaDiubahKe(string statusBaru)
        {
            // State Machine Transisi Status
            if (_statusPesanan == "Menunggu" && (statusBaru == "Diproses" || statusBaru == "Dibatalkan"))
            {
                return true;
            }
            if (_statusPesanan == "Diproses" && (statusBaru == "Selesai" || statusBaru == "Dibatalkan"))
            {
                return true;
            }
            return false;
        }

        public void UbahStatus(string statusBaru)
        {
            if (BisaDiubahKe(statusBaru) == false)
            {
                throw new InvalidOrderException(
                    "Transaksi tidak bisa diubah dari '" + _statusPesanan + "' ke '" + statusBaru + "'!",
                    "status_pesanan",
                    "STATUS_TRANSITION_INVALID"
                );
            }
            _statusPesanan = statusBaru;
        }


        // === IMPLEMENTASI IApprovable ===
        public void Approve()
        {
            _isValid = true;
            _alasanPenolakan = "";
        }

        public void Reject(string alasan)
        {
            if (string.IsNullOrEmpty(alasan))
            {
                throw new InvalidOrderException("Alasan penolakan wajib diisi!", "alasan", "REJECT_EMPTY");
            }
            _isValid = false;
            _alasanPenolakan = alasan;
        }

        public bool GetStatusPersetujuan()
        {
            return _isValid;
        }
    }
}
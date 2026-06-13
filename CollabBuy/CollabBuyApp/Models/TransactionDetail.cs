using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk detail item dalam transaksi.
    /// Menerapkan ICalculatable (kalkulasi subtotal & refund).
    /// </summary>
    public class TransactionDetail : IValidatable, ICalculatable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idDetail;
        private int _idTransaksi;
        private string _namaPenitip;
        private int _jumlahPesanan;
        private string _catatan;
        private string _namaProdukSnapshot;
        private int _idProduk;
        private long _hargaSatuanSaatBeli;
        private long? _hargaDiskonSaatBeli;
        private long _selisihRefund;

        // Referensi In-Memory ke objek Product (Aggregation)
        private Product _produkYangDipesan;

        // === PROPERTIES (Enkapsulasi Ketat) ===

        // Auto-Properties (Read-only dari luar)
        public int IdProduk
        {
            get { return this._idProduk; }
            private set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Produk pada detail transaksi tidak valid!", "id_produk", "DETAIL_IDPRODUK_INVALID");
                this._idProduk = value;
            }
        }

        public long HargaSatuanSaatBeli
        {
            get { return this._hargaSatuanSaatBeli; }
            private set
            {
                if (value < 0)
                    throw new InvalidOrderException("Harga satuan tidak boleh negatif!", "harga_satuan", "HARGA_SATUAN_INVALID");
                this._hargaSatuanSaatBeli = value;
            }
        }

        public long? HargaDiskonSaatBeli
        {
            get { return this._hargaDiskonSaatBeli; }
            private set
            {
                if (value.HasValue && value.Value < 0)
                    throw new InvalidOrderException("Harga diskon tidak boleh negatif!", "harga_diskon", "HARGA_DISKON_INVALID");
                this._hargaDiskonSaatBeli = value;
            }
        }

        public long SelisihRefund
        {
            get { return this._selisihRefund; }
            private set
            {
                this._selisihRefund = value < 0 ? 0 : value;
            }
        }
        public int IdDetail
        {
            get { return this._idDetail; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Detail tidak valid!", "id_detail", "DETAIL_ID_INVALID");
                this._idDetail = value;
            }
        }

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

        public string NamaPenitip
        {
            get { return this._namaPenitip; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Nama penitip wajib diisi!", "nama_penitip", "PENITIP_KOSONG");

                if (value.Trim().Length < 3)
                    throw new InvalidOrderException("Nama penitip minimal 3 karakter!", "nama_penitip", "PENITIP_PENDEK");

                if (value.Trim().Length > 100)
                    throw new InvalidOrderException("Nama penitip maksimal 100 karakter!", "nama_penitip", "PENITIP_PANJANG");

                this._namaPenitip = value.Trim();
            }
        }

        public int JumlahPesanan
        {
            get { return this._jumlahPesanan; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("Jumlah pesanan harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
                this._jumlahPesanan = value;
            }
        }

        public string Catatan
        {
            get { return this._catatan; }
            set { this._catatan = string.IsNullOrWhiteSpace(value) ? "-" : value.Trim(); }
        }

        public string NamaProdukSnapshot
        {
            get { return this._namaProdukSnapshot; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Snapshot nama produk tidak boleh kosong!", "nama_snapshot", "SNAPSHOT_KOSONG");
                this._namaProdukSnapshot = value.Trim();
            }
        }

        public Product ProdukYangDipesan
        {
            get { return this._produkYangDipesan; }
            set
            {
                if (value == null)
                    throw new InvalidOrderException("Produk referensi tidak boleh null!", "produk", "REF_PRODUK_NULL");
                this._produkYangDipesan = value;
            }
        }

        // === KONSTRUKTOR ===
        public TransactionDetail(int idProduk, string namaPenitip, int jumlahPesanan)
        {
            this.IdProduk = idProduk;
            this.NamaPenitip = namaPenitip;
            this.JumlahPesanan = jumlahPesanan;

            this.SelisihRefund = 0;
            this._idDetail = 0;
            this._idTransaksi = 0;
            this.Catatan = "";
            this._namaProdukSnapshot = "Menunggu Checkout";
            this._produkYangDipesan = null;
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        public void FinalisasiHargaSaatCheckout(long hargaSatuan, long? hargaDiskon)
        {
            if (hargaSatuan <= 0)
                throw new InvalidOrderException("Harga satuan saat checkout tidak valid!", "harga_satuan", "HARGA_CHECKOUT_INVALID");

            this.HargaSatuanSaatBeli = hargaSatuan;
            this.HargaDiskonSaatBeli = hargaDiskon;

            this.NamaProdukSnapshot = this._produkYangDipesan != null
                ? this._produkYangDipesan.NamaProduk
                : "Produk Tidak Ditemukan";
        }

        /// <summary>
        /// Khusus untuk membaca data dari database — tidak throw meski harga 0 
        /// (data lama yang di-seed tanpa trigger).
        /// </summary>
        public void IsiHargaDariDatabase(long hargaSatuan, long? hargaDiskon, string namaSnapshot)
        {
            this.HargaSatuanSaatBeli = hargaSatuan >= 0 ? hargaSatuan : 0;
            this.HargaDiskonSaatBeli = hargaDiskon;
            if (!string.IsNullOrWhiteSpace(namaSnapshot))
                this.NamaProdukSnapshot = namaSnapshot;
        }

        /// <summary>
        /// Restore nilai selisih_refund langsung dari database saat hydration objek.
        /// Dipanggil setelah IsiHargaDariDatabase() — tidak kalkulasi ulang,
        /// cukup set nilai yang sudah tersimpan di DB agar HitungDiskon() akurat.
        /// </summary>
        public void SetSelisihRefundDariDatabase(long selisihRefund)
        {
            this.SelisihRefund = selisihRefund >= 0 ? selisihRefund : 0;
        }

        public void HitungRefundGotongRoyong()
        {
            // Reset refund ke 0 terlebih dahulu
            this.SelisihRefund = 0;

            // Pengecekan syarat berlapis bisa digabung dengan AND (&&)
            if (this._produkYangDipesan != null &&
                this.HargaDiskonSaatBeli.HasValue &&
                this._produkYangDipesan.IsKuotaTerpenuhi() &&
                this.HargaSatuanSaatBeli > this.HargaDiskonSaatBeli.Value)
            {
                this.SelisihRefund = (this.HargaSatuanSaatBeli - this.HargaDiskonSaatBeli.Value) * this.JumlahPesanan;
            }
        }

        public string DapatkanInfoItemUI()
        {
            return this.JumlahPesanan > 1
                ? $"[{this.NamaPenitip}] {this.NamaProdukSnapshot} (x{this.JumlahPesanan})"
                : $"[{this.NamaPenitip}] {this.NamaProdukSnapshot}";
        }

        public string DapatkanSubtotalUI()
        {
            long subtotal = this.HitungTotal();
            return subtotal > 0 ? $"Rp {subtotal:N0}" : "Gratis";
        }

        public string DapatkanInfoRefundUI()
        {
            return this.SelisihRefund > 0
                ? $"✨ Cashback Gotong Royong: Rp {this.SelisihRefund:N0}"
                : "Tidak ada cashback.";
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (this.IdProduk <= 0)
                throw new InvalidOrderException("ID Produk tidak valid pada detail transaksi!", "id_produk", "DETAIL_INVALID");

            if (string.IsNullOrWhiteSpace(this.NamaPenitip))
                throw new InvalidOrderException("Nama penitip tidak boleh kosong!", "nama_penitip", "PENITIP_KOSONG");

            if (this.JumlahPesanan <= 0)
                throw new InvalidOrderException("Jumlah pesanan item harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
        }

        // === IMPLEMENTASI ICalculatable ===
        public long HitungTotal()
        {
            return (this.JumlahPesanan > 0 && this.HargaSatuanSaatBeli > 0)
                ? this.JumlahPesanan * this.HargaSatuanSaatBeli
                : 0;
        }

        public long HitungDiskon()
        {
            // HitungDiskon hanyalah alias untuk mengembalikan nilai Refund jika ada
            return this.SelisihRefund > 0 ? this.SelisihRefund : 0;
        }
    }
}
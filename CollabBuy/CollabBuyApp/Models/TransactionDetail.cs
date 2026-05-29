using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk detail item dalam transaksi.
    /// Menerapkan ICalculatable (kalkulasi subtotal & refund).
    /// 
    /// Pemetaan Database:
    /// - Tabel: transaction_details
    /// - Trigger: trg_hitung_refund_gotong_royong -> Diimplementasikan di HitungDiskon()
    /// - Trigger: t_before_insert_detail -> Diimplementasikan di FinalisasiHargaSaatCheckout()
    /// </summary>
    public class TransactionDetail : IValidatable, ICalculatable
    {
        // === PRIVATE FIELDS ===
        private int _idDetail;
        private int _idTransaksi;
        private int _idProduk;
        private string _namaProdukSnapshot;
        private string _namaPenitip;
        private int _jumlahPesanan;
        private string _catatan;
        private long _hargaSatuanSaatBeli;
        private long? _hargaDiskonSaatBeli;
        private long _selisihRefund;

        // Referensi In-Memory ke objek Product (Aggregation)
        private Product _produkYangDipesan;

        // === KONSTRUKTOR ===
        public TransactionDetail(int idProduk, string namaPenitip, int jumlahPesanan)
        {
            _idProduk = idProduk;
            SetNamaPenitip(namaPenitip);
            SetJumlahPesanan(jumlahPesanan);
            _selisihRefund = 0;
        }

        // === GETTER & SETTER DENGAN VALIDASI ===
        public int GetIdProduk() { return _idProduk; }

        public string GetNamaPenitip() { return _namaPenitip; }
        public void SetNamaPenitip(string nama)
        {
            if (string.IsNullOrEmpty(nama))
            {
                throw new InvalidOrderException("Nama penitip wajib diisi!", "nama_penitip", "PENITIP_KOSONG");
            }
            _namaPenitip = nama;
        }

        public int GetJumlahPesanan() { return _jumlahPesanan; }
        public void SetJumlahPesanan(int jumlah)
        {
            if (jumlah <= 0)
            {
                throw new InvalidOrderException("Jumlah pesanan harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
            }
            _jumlahPesanan = jumlah;
        }

        public string GetCatatan() { return _catatan; }
        public void SetCatatan(string catatan) { _catatan = catatan; }

        public long GetSelisihRefund() { return _selisihRefund; }

        // Relasi In-Memory
        public Product GetProduk() { return _produkYangDipesan; }
        public void SetProduk(Product produk) { _produkYangDipesan = produk; }


        // === METHOD BISNIS LOGIC ===

        /// <summary>
        /// Menetapkan snapshot harga dan nama saat checkout.
        /// Pemetaan DB: Menggantikan Trigger t_before_insert_detail (trg_set_harga_otomatis).
        /// </summary>
        public void FinalisasiHargaSaatCheckout(long hargaSatuan, long? hargaDiskon)
        {
            _hargaSatuanSaatBeli = hargaSatuan;
            _hargaDiskonSaatBeli = hargaDiskon;

            // PERBAIKAN: Safety check agar tidak NullReferenceException 
            // jika ternyata objek Product belum di-set oleh CartManager
            if (_produkYangDipesan != null)
            {
                _namaProdukSnapshot = _produkYangDipesan.GetNamaProduk();
            }
            else
            {
                _namaProdukSnapshot = "Produk Tidak Ditemukan";
            }
        }

        /// <summary>
        /// Menghitung refund Gotong Royong jika kuota terpenuhi.
        /// Pemetaan DB: Menggantikan Trigger trg_hitung_refund_gotong_royong.
        /// Dipanggil oleh CartManager saat membangun objek Transaksi.
        /// </summary>
        public void HitungRefundGotongRoyong()
        {
            // PERBAIKAN: Cek hargaDiskonSaatBeli harus di cek terlebih dahulu sebelum .Value
            if (_produkYangDipesan != null && _hargaDiskonSaatBeli.HasValue)
            {
                if (_produkYangDipesan.IsKuotaTerpenuhi() && _hargaSatuanSaatBeli > _hargaDiskonSaatBeli.Value)
                {
                    _selisihRefund = (_hargaSatuanSaatBeli - _hargaDiskonSaatBeli.Value) * _jumlahPesanan;
                }
            }
        }


        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (_idProduk <= 0)
            {
                throw new InvalidOrderException("ID Produk tidak valid pada detail transaksi!", "id_produk", "DETAIL_INVALID");
            }
            if (string.IsNullOrEmpty(_namaPenitip))
            {
                throw new InvalidOrderException("Nama penitip tidak boleh kosong!", "nama_penitip", "PENITIP_KOSONG");
            }
            if (_jumlahPesanan <= 0)
            {
                throw new InvalidOrderException("Jumlah pesanan item harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
            }
        }


        // === IMPLEMENTASI ICalculatable ===

        /// <summary>
        /// Total harga subtotal item (jumlah × harga satuan saat beli).
        /// </summary>
        public long HitungTotal()
        {
            return _jumlahPesanan * _hargaSatuanSaatBeli;
        }

        /// <summary>
        /// Total diskon/refund item.
        /// </summary>
        public long HitungDiskon()
        {
            return _selisihRefund;
        }


        // === TAMBAHAN GETTER UNTUK REPOSITORY ===

        /// <summary>
        /// Mengambil nama produk yang di-snapshot saat checkout.
        /// </summary>
        public string GetNamaProdukSnapshot()
        {
            return _namaProdukSnapshot;
        }

        /// <summary>
        /// Menetapkan snapshot nama produk secara langsung — digunakan saat hydration
        /// objek dari database (setelah FinalisasiHargaSaatCheckout dipanggil).
        /// </summary>
        public void SetNamaProdukSnapshot(string nama)
        {
            if (!string.IsNullOrEmpty(nama))
            {
                _namaProdukSnapshot = nama;
            }
        }

        /// <summary>
        /// Mengambil harga satuan saat proses checkout terjadi.
        /// </summary>
        public long GetHargaSatuanSaatBeli()
        {
            return _hargaSatuanSaatBeli;
        }

        /// <summary>
        /// Mengambil harga diskon saat proses checkout terjadi.
        /// </summary>
        public long? GetHargaDiskonSaatBeli()
        {
            return _hargaDiskonSaatBeli;
        }
    }
}
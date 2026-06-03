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
            this._idProduk = idProduk;
            this.SetNamaPenitip(namaPenitip);
            this.SetJumlahPesanan(jumlahPesanan);

            this._selisihRefund = 0;
            this._idDetail = 0;
            this._idTransaksi = 0;
            this._catatan = "";
            this._namaProdukSnapshot = "Menunggu Checkout";
            this._produkYangDipesan = null;
        }

        // === GETTER & SETTER DENGAN ENKAPSULASI KETAT ===
        public int GetIdDetail()
        {
            return this._idDetail;
        }

        public void SetIdDetail(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Detail tidak valid!", "id_detail", "DETAIL_ID_INVALID");
            }
            else
            {
                this._idDetail = id;
            }
        }

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

        public int GetIdProduk()
        {
            return this._idProduk;
        }

        public string GetNamaPenitip()
        {
            return this._namaPenitip;
        }

        public void SetNamaPenitip(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                throw new InvalidOrderException("Nama penitip wajib diisi!", "nama_penitip", "PENITIP_KOSONG");
            }
            else if (nama.Trim().Length < 3)
            {
                throw new InvalidOrderException("Nama penitip minimal 3 karakter!", "nama_penitip", "PENITIP_PENDEK");
            }
            else if (nama.Trim().Length > 100)
            {
                throw new InvalidOrderException("Nama penitip maksimal 100 karakter!", "nama_penitip", "PENITIP_PANJANG");
            }
            else
            {
                this._namaPenitip = nama.Trim();
            }
        }

        public int GetJumlahPesanan()
        {
            return this._jumlahPesanan;
        }

        public void SetJumlahPesanan(int jumlah)
        {
            if (jumlah <= 0)
            {
                throw new InvalidOrderException("Jumlah pesanan harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
            }
            else
            {
                this._jumlahPesanan = jumlah;
            }
        }

        public string GetCatatan()
        {
            return this._catatan;
        }

        public void SetCatatan(string catatan)
        {
            if (string.IsNullOrWhiteSpace(catatan))
            {
                this._catatan = "-";
            }
            else
            {
                this._catatan = catatan.Trim();
            }
        }

        public long GetSelisihRefund()
        {
            return this._selisihRefund;
        }

        // Relasi In-Memory
        public Product GetProduk()
        {
            return this._produkYangDipesan;
        }

        public void SetProduk(Product produk)
        {
            if (produk == null)
            {
                throw new InvalidOrderException("Produk referensi tidak boleh null!", "produk", "REF_PRODUK_NULL");
            }
            else
            {
                this._produkYangDipesan = produk;
            }
        }

        public string GetNamaProdukSnapshot()
        {
            return this._namaProdukSnapshot;
        }

        public void SetNamaProdukSnapshot(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                throw new InvalidOrderException("Snapshot nama produk tidak boleh kosong!", "nama_snapshot", "SNAPSHOT_KOSONG");
            }
            else
            {
                this._namaProdukSnapshot = nama.Trim();
            }
        }

        public long GetHargaSatuanSaatBeli()
        {
            return this._hargaSatuanSaatBeli;
        }

        public long? GetHargaDiskonSaatBeli()
        {
            return this._hargaDiskonSaatBeli;
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        public void FinalisasiHargaSaatCheckout(long hargaSatuan, long? hargaDiskon)
        {
            if (hargaSatuan <= 0)
            {
                throw new InvalidOrderException("Harga satuan saat checkout tidak valid!", "harga_satuan", "HARGA_CHECKOUT_INVALID");
            }
            else
            {
                this._hargaSatuanSaatBeli = hargaSatuan;
                this._hargaDiskonSaatBeli = hargaDiskon;

                if (this._produkYangDipesan != null)
                {
                    this._namaProdukSnapshot = this._produkYangDipesan.GetNamaProduk();
                }
                else
                {
                    this._namaProdukSnapshot = "Produk Tidak Ditemukan";
                }
            }
        }

        public void HitungRefundGotongRoyong()
        {
            if (this._produkYangDipesan != null && this._hargaDiskonSaatBeli.HasValue)
            {
                if (this._produkYangDipesan.IsKuotaTerpenuhi() && this._hargaSatuanSaatBeli > this._hargaDiskonSaatBeli.Value)
                {
                    this._selisihRefund = (this._hargaSatuanSaatBeli - this._hargaDiskonSaatBeli.Value) * this._jumlahPesanan;
                }
                else
                {
                    this._selisihRefund = 0; // Penugasan nyata agar else tidak kosong
                }
            }
            else
            {
                this._selisihRefund = 0; // Penugasan nyata agar else tidak kosong
            }
        }

        public string DapatkanInfoItemUI()
        {
            string info;
            if (this._jumlahPesanan > 1)
            {
                info = $"[{this._namaPenitip}] {this._namaProdukSnapshot} (x{this._jumlahPesanan})";
            }
            else
            {
                info = $"[{this._namaPenitip}] {this._namaProdukSnapshot}";
            }
            return info;
        }

        public string DapatkanSubtotalUI()
        {
            long subtotal = this.HitungTotal();
            string subtotalUi;

            if (subtotal > 0)
            {
                subtotalUi = $"Rp {subtotal:N0}";
            }
            else
            {
                subtotalUi = "Gratis";
            }

            return subtotalUi;
        }

        public bool ApakahDapatRefund()
        {
            bool dapatRefund;

            // Pastikan method hitung dijalankan dulu untuk mengevaluasi kondisi terbaru
            this.HitungRefundGotongRoyong();

            if (this._selisihRefund > 0)
            {
                dapatRefund = true;
            }
            else
            {
                dapatRefund = false;
            }

            return dapatRefund;
        }

        public string DapatkanInfoRefundUI()
        {
            string infoRefund;

            if (this.ApakahDapatRefund())
            {
                infoRefund = $"✨ Cashback Gotong Royong: Rp {this._selisihRefund:N0}";
            }
            else
            {
                infoRefund = "Tidak ada cashback.";
            }

            return infoRefund;
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            bool cekId;
            bool cekPesanan;

            if (this._idProduk <= 0)
            {
                throw new InvalidOrderException("ID Produk tidak valid pada detail transaksi!", "id_produk", "DETAIL_INVALID");
            }
            else
            {
                cekId = true;
            }

            if (string.IsNullOrWhiteSpace(this._namaPenitip))
            {
                throw new InvalidOrderException("Nama penitip tidak boleh kosong!", "nama_penitip", "PENITIP_KOSONG");
            }
            else if (this._jumlahPesanan <= 0)
            {
                throw new InvalidOrderException("Jumlah pesanan item harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
            }
            else
            {
                cekPesanan = cekId; // Chain penugasan nyata
            }
        }

        // === IMPLEMENTASI ICalculatable ===
        public long HitungTotal()
        {
            long totalHarga;

            if (this._jumlahPesanan > 0 && this._hargaSatuanSaatBeli > 0)
            {
                totalHarga = this._jumlahPesanan * this._hargaSatuanSaatBeli;
            }
            else
            {
                totalHarga = 0;
            }

            return totalHarga;
        }

        public long HitungDiskon()
        {
            long totalDiskon;

            if (this._selisihRefund > 0)
            {
                totalDiskon = this._selisihRefund;
            }
            else
            {
                totalDiskon = 0;
            }

            return totalDiskon;
        }
    }
}
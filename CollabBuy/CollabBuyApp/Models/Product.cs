using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk entitas Produk.
    /// Menerapkan IValidatable, ICalculatable, dan IQuotaTrackable.
    /// </summary>
    public class Product : IValidatable, ICalculatable, IQuotaTrackable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idProduk;
        private int? _idPo;
        private string _namaProduk;
        private string _deskripsi;
        private int _hargaDasar;
        private int? _hargaDiskon;
        private int? _targetKuota;
        private int _minOrder;
        private byte[] _fotoProduk;
        private string _jenisPo;
        private int _idPenjual;
        private int _idKategori;
        private int _terpesan;

        // === PROPERTIES ===

        public int IdPenjual
        {
            get { return this._idPenjual; }
            private set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Penjual tidak valid!", "id_penjual", "PRODUK_PENJUAL_INVALID");
                this._idPenjual = value;
            }
        }

        public int IdKategori
        {
            get { return this._idKategori; }
            private set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Kategori tidak valid!", "id_kategori", "PRODUK_KATEGORI_INVALID");
                this._idKategori = value;
            }
        }

        public int Terpesan
        {
            get { return this._terpesan; }
            private set
            {
                if (value < 0)
                    this._terpesan = 0;
                else
                    this._terpesan = value;
            }
        }


        public int IdProduk
        {
            get { return this._idProduk; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Produk tidak valid!", "id_produk", "PRODUK_ID_INVALID");
                this._idProduk = value;
            }
        }

        public int? IdPo
        {
            get { return this._idPo; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new InvalidOrderException("ID PO tidak valid!", "id_po", "PO_ID_INVALID");
                this._idPo = value;
            }
        }

        public string NamaProduk
        {
            get { return this._namaProduk; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Nama produk tidak boleh kosong!", "nama_produk", "PRODUK_INVALID");

                if (value.Trim().Length < 3)
                    throw new InvalidOrderException("Nama produk minimal 3 karakter!", "nama_produk", "PRODUK_NAMA_PENDEK");

                if (value.Trim().Length > 150)
                    throw new InvalidOrderException("Nama produk maksimal 150 karakter!", "nama_produk", "PRODUK_NAMA_PANJANG");

                this._namaProduk = value.Trim();
            }
        }

        public string Deskripsi
        {
            get { return this._deskripsi; }
            set
            {
                this._deskripsi = string.IsNullOrWhiteSpace(value) ? "Tidak ada deskripsi." : value.Trim();
            }
        }

        public int MinOrder
        {
            get { return this._minOrder; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("Minimal order harus lebih dari 0!", "min_order", "MIN_ORDER_INVALID");
                this._minOrder = value;
            }
        }

        public string JenisPo
        {
            get { return this._jenisPo; }
            set
            {
                this._jenisPo = string.IsNullOrWhiteSpace(value) ? "Biasa" : value.Trim();
            }
        }

        public int HargaDasar
        {
            get { return this._hargaDasar; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("Harga dasar harus lebih dari 0!", "harga_dasar", "HARGA_INVALID");
                this._hargaDasar = value;
            }
        }

        public int? HargaDiskon
        {
            get { return this._hargaDiskon; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value <= 0)
                        throw new InvalidOrderException("Harga diskon harus lebih dari 0!", "harga_diskon", "DISKON_INVALID");

                    // PERBAIKAN: hanya validasi terhadap harga dasar jika harga dasar sudah di-set (> 0)
                    if (this._hargaDasar > 0 && value.Value >= this._hargaDasar)
                        throw new InvalidOrderException(
                            "Harga diskon harus lebih kecil dari harga dasar!", "harga_diskon", "DISKON_INVALID");
                }
                this._hargaDiskon = value;
            }
        }

        public int? TargetKuota
        {
            get { return this._targetKuota; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new InvalidOrderException("Target kuota harus lebih dari 0!", "target_kuota", "KUOTA_INVALID");
                this._targetKuota = value;
            }
        }

        public byte[] FotoProduk
        {
            get { return this._fotoProduk; }
            set
            {
                if (value != null && value.Length > 2 * 1024 * 1024) // 2MB
                    throw new InvalidOrderException("Ukuran foto produk maksimal 2MB!", "foto_produk", "FOTO_OVERSIZE");
                this._fotoProduk = value;
            }
        }

        // === KONSTRUKTOR ===
        public Product(int idPenjual, int idKategori, string namaProduk, int hargaDasar)
        {
            this.IdPenjual = idPenjual;
            this.IdKategori = idKategori;
            this.NamaProduk = namaProduk;
            this.HargaDasar = hargaDasar;

            this.MinOrder = 1;
            this.Terpesan = 0;
            this.JenisPo = "Biasa";
            this.Deskripsi = "";
        }

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR (OOP BEST PRACTICE)
        // =========================================================

        // SESUDAH (TIDAK CRASH SAAT TAMBAH, VALIDASI DIPINDAH KE CHECKOUT):
        public void TambahPesanan(int jumlah)
        {
            // Validasi hanya jumlah tidak boleh negatif/nol; min order dicek saat checkout
            if (jumlah <= 0)
            {
                throw new InvalidOrderException("Jumlah pesanan harus lebih dari 0!", "jumlah_pesanan", "QTY_INVALID");
            }
            this.Terpesan += jumlah;
        }
        public void KurangiPesanan(int jumlah)
        {
            if (jumlah <= 0) return;
            this.Terpesan -= jumlah;
        }

        /// <summary>
        /// Validasi total pesanan (seluruh titipan) terhadap MinOrder.
        /// Dipanggil saat checkout, bukan saat TambahItem.
        /// </summary>
        public void ValidasiTotalPesanan(int totalJumlah)
        {
            if (totalJumlah < this.MinOrder)
            {
                throw new InvalidOrderException(
                    $"Total pesanan untuk produk '{this.NamaProduk}' adalah {totalJumlah} pcs, " +
                    $"kurang dari minimal order {this.MinOrder} pcs!",
                    "jumlah_pesanan",
                    "QTY_MIN_INVALID"
                );
            }
        }

        /// <summary>
        /// Mengembalikan teks label badge khusus untuk UI Katalog.
        /// </summary>
        public string DapatkanLabelPromo()
        {
            if (this.JenisPo == "Gotong Royong" && this.HargaDiskon.HasValue) return "🔥 Gotong Royong: Potongan Harga!";
            if (this.TargetKuota.HasValue) return "📦 Pre-Order Reguler";

            return "🛍️ Ready Stock";
        }

        /// <summary>
        /// Format harga menjadi string rapi siap tampil di UI.
        /// Jika ada diskon dan kuota terpenuhi, otomatis dicoret harga aslinya!
        /// </summary>
        public string DapatkanFormatHargaUI()
        {
            long hargaAkhir = this.HitungTotal();

            if (hargaAkhir < this.HargaDasar)
            {
                return $"Rp {hargaAkhir:N0} (Turun dari Rp {this.HargaDasar:N0})";
            }

            return $"Rp {this.HargaDasar:N0}";
        }

        /// <summary>
        /// Mengembalikan status sisa slot dalam bentuk string untuk UI.
        /// </summary>
        public string DapatkanInfoSlot()
        {
            if (!this.TargetKuota.HasValue) return "✅ Ready / Tanpa Batas";

            int sisa = this.GetSisaKuota();

            // Hanya GR yang punya target_kuota — dan kuota penuh bukan berarti tutup
            if (sisa <= 0)
                return $"🎯 Target cashback tercapai! ({this.Terpesan}/{this.TargetKuota.Value})";

            return $"Target cashback: sisa {sisa} slot ({this.Terpesan}/{this.TargetKuota.Value})";
        }

        // === IMPLEMENTASI IValidatable ===
        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            // Guard clauses independen tanpa saling terkait (tanpa else if)
            if (string.IsNullOrWhiteSpace(this._namaProduk))
                throw new InvalidOrderException("Validasi gagal: Nama produk kosong.", "nama_produk", "PRODUK_INVALID");

            if (this._hargaDasar <= 0)
                throw new InvalidOrderException("Validasi gagal: Harga dasar tidak valid.", "harga_dasar", "HARGA_INVALID");

            if (this._hargaDiskon.HasValue && this._hargaDiskon.Value >= this._hargaDasar)
                throw new InvalidOrderException("Harga diskon harus lebih kecil dari harga dasar!", "harga_diskon", "DISKON_INVALID");
        }

        // === IMPLEMENTASI ICalculatable ===
        public long HitungTotal()
        {
            // Cek apakah syarat harga diskon PO Gotong Royong terpenuhi
            if (this.IdPo.HasValue && this.JenisPo == "Gotong Royong" && this.TargetKuota.HasValue && this.HargaDiskon.HasValue && this.IsKuotaTerpenuhi())
            {
                return this.HargaDiskon.Value;
            }

            return this.HargaDasar;
        }

        public long HitungDiskon()
        {
            if (this.IdPo.HasValue && this.JenisPo == "Gotong Royong" && this.TargetKuota.HasValue && this.HargaDiskon.HasValue && this.IsKuotaTerpenuhi())
            {
                return this.HargaDasar - this.HargaDiskon.Value;
            }

            return 0;
        }

        // === IMPLEMENTASI IQuotaTrackable ===
        public int GetTargetKuota()
        {
            // Menggunakan Null-Coalescing Operator (??)
            // Jika TargetKuota null, otomatis kembalikan 0.
            return this.TargetKuota ?? 0;
        }

        public int GetTerpesan()
        {
            return this.Terpesan;
        }

        public int GetSisaKuota()
        {
            // Jika tidak ada limit kuota, kembalikan nilai maksimal integer
            if (!this.TargetKuota.HasValue) return int.MaxValue;

            return this.TargetKuota.Value - this.Terpesan;
        }

        public bool IsKuotaTerpenuhi()
        {
            if (!this.TargetKuota.HasValue) return false;

            // Evaluasi boolean secara langsung
            return this.Terpesan >= this.TargetKuota.Value;
        }
    }
}
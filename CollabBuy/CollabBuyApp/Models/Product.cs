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
        // === PRIVATE FIELDS (Enkapsulasi Ketat) ===
        private int _idProduk;
        private int _idPenjual;
        private int? _idPo;
        private int _idKategori;
        private string _namaProduk;
        private string _deskripsi;
        private int _hargaDasar;
        private int? _hargaDiskon;
        private int? _targetKuota;
        private int _minOrder;
        private int _terpesan;
        private byte[] _fotoProduk;
        private string _jenisPo;

        // === KONSTRUKTOR ===
        public Product(int idPenjual, int idKategori, string namaProduk, int hargaDasar)
        {
            this._idPenjual = idPenjual;
            this._idKategori = idKategori;
            this.SetNamaProduk(namaProduk);
            this.SetHargaDasar(hargaDasar);

            this._minOrder = 1;
            this._terpesan = 0;
            this._jenisPo = "Biasa";
            this._deskripsi = "";
        }

        // === GETTER & SETTER DENGAN ENKAPSULASI STRICT (IF-ELSE) ===
        public int GetIdProduk()
        {
            return this._idProduk;
        }

        public void SetIdProduk(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Produk tidak valid!", "id_produk", "PRODUK_ID_INVALID");
            }
            else
            {
                this._idProduk = id;
            }
        }

        public int GetIdPenjual()
        {
            return this._idPenjual;
        }

        public int? GetIdPo()
        {
            return this._idPo;
        }

        public void SetIdPo(int? idPo)
        {
            if (idPo.HasValue && idPo.Value <= 0)
            {
                throw new InvalidOrderException("ID PO tidak valid!", "id_po", "PO_ID_INVALID");
            }
            else
            {
                this._idPo = idPo;
            }
        }

        public int GetIdKategori()
        {
            return this._idKategori;
        }

        public string GetNamaProduk()
        {
            return this._namaProduk;
        }

        public void SetNamaProduk(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                throw new InvalidOrderException("Nama produk tidak boleh kosong!", "nama_produk", "PRODUK_INVALID");
            }
            else
            {
                this._namaProduk = nama.Trim();
            }
        }

        public string GetDeskripsi()
        {
            return this._deskripsi;
        }

        public void SetDeskripsi(string deskripsi)
        {
            if (string.IsNullOrWhiteSpace(deskripsi))
            {
                this._deskripsi = "Tidak ada deskripsi.";
            }
            else
            {
                this._deskripsi = deskripsi.Trim();
            }
        }

        public int GetMinOrder()
        {
            return this._minOrder;
        }

        public void SetMinOrder(int min)
        {
            if (min <= 0)
            {
                throw new InvalidOrderException("Minimal order harus lebih dari 0!", "min_order", "MIN_ORDER_INVALID");
            }
            else
            {
                this._minOrder = min;
            }
        }

        public void SetJenisPo(string jenis)
        {
            if (string.IsNullOrWhiteSpace(jenis))
            {
                this._jenisPo = "Biasa";
            }
            else
            {
                this._jenisPo = jenis.Trim();
            }
        }

        public void SetHargaDasar(int harga)
        {
            if (harga <= 0)
            {
                throw new InvalidOrderException("Harga dasar harus lebih dari 0!", "harga_dasar", "HARGA_INVALID");
            }
            else
            {
                this._hargaDasar = harga;
            }
        }

        public void SetHargaDiskon(int? harga)
        {
            if (harga.HasValue && harga.Value >= this._hargaDasar)
            {
                throw new InvalidOrderException("Harga diskon harus lebih kecil dari harga dasar!", "harga_diskon", "DISKON_INVALID");
            }
            else
            {
                this._hargaDiskon = harga;
            }
        }

        public void SetTargetKuota(int? kuota)
        {
            if (kuota.HasValue && kuota.Value <= 0)
            {
                throw new InvalidOrderException("Target kuota harus lebih dari 0!", "target_kuota", "KUOTA_INVALID");
            }
            else
            {
                this._targetKuota = kuota;
            }
        }

        public void SetFotoProduk(byte[] foto)
        {
            if (foto != null && foto.Length > 2097152)
            {
                throw new InvalidOrderException("Ukuran foto produk maksimal 2MB!", "foto_produk", "FOTO_OVERSIZE");
            }
            else
            {
                this._fotoProduk = foto;
            }
        }

        public byte[] GetFotoProduk()
        {
            return this._fotoProduk;
        }

        public int GetHargaDasar()
        {
            return this._hargaDasar;
        }

        public int? GetHargaDiskon()
        {
            return this._hargaDiskon;
        }

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR (OOP BEST PRACTICE)
        // =========================================================

        public void TambahPesanan(int jumlah)
        {
            if (jumlah < this._minOrder)
            {
                throw new InvalidOrderException("Jumlah pesanan kurang dari minimal order (" + this._minOrder + ")!", "jumlah_pesanan", "QTY_MIN_INVALID");
            }
            else
            {
                this._terpesan = this._terpesan + jumlah;
            }
        }

        /// <summary>
        /// Mengembalikan teks label badge khusus untuk UI Katalog.
        /// </summary>
        public string DapatkanLabelPromo()
        {
            string label;
            if (this._jenisPo == "Gotong Royong" && this._hargaDiskon.HasValue)
            {
                label = "🔥 Gotong Royong: Potongan Harga!";
            }
            else if (this._targetKuota.HasValue)
            {
                label = "📦 Pre-Order Reguler";
            }
            else
            {
                label = "🛍️ Ready Stock";
            }
            return label;
        }

        /// <summary>
        /// Format harga menjadi string rapi siap tampil di UI.
        /// Jika ada diskon dan kuota terpenuhi, otomatis dicoret harga aslinya!
        /// </summary>
        public string DapatkanFormatHargaUI()
        {
            string hargaUI;
            long hargaAkhir = this.HitungTotal();

            if (hargaAkhir < this._hargaDasar)
            {
                // Diskon Aktif!
                hargaUI = $"Rp {hargaAkhir:N0} (Turun dari Rp {this._hargaDasar:N0})";
            }
            else
            {
                hargaUI = $"Rp {this._hargaDasar:N0}";
            }

            return hargaUI;
        }

        /// <summary>
        /// Mengembalikan status sisa slot dalam bentuk string untuk UI.
        /// </summary>
        public string DapatkanInfoSlot()
        {
            string info;
            if (!this._targetKuota.HasValue)
            {
                info = "✅ Ready / Tanpa Batas";
            }
            else
            {
                int sisa = this.GetSisaKuota();
                if (sisa <= 0)
                {
                    info = "⛔ Ludes / Penuh!";
                }
                else
                {
                    info = $"Sisa {sisa} Slot (Terkumpul {this._terpesan}/{this._targetKuota.Value})";
                }
            }
            return info;
        }


        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            bool validasiProdukSelesai;

            if (string.IsNullOrWhiteSpace(this._namaProduk))
            {
                throw new InvalidOrderException("Validasi gagal: Nama produk kosong.", "nama_produk", "PRODUK_INVALID");
            }
            else if (this._hargaDasar <= 0)
            {
                throw new InvalidOrderException("Validasi gagal: Harga dasar tidak valid.", "harga_dasar", "HARGA_INVALID");
            }
            else if (this._hargaDiskon.HasValue && this._hargaDiskon.Value >= this._hargaDasar)
            {
                throw new InvalidOrderException("Validasi gagal: Harga diskon >= Harga dasar.", "harga_diskon", "DISKON_INVALID");
            }
            else
            {
                validasiProdukSelesai = true;
            }
        }

        // === IMPLEMENTASI ICalculatable ===
        public long HitungTotal()
        {
            long hargaFinal;

            if (this._idPo.HasValue && this._jenisPo == "Gotong Royong" && this._targetKuota.HasValue && this._hargaDiskon.HasValue && this.IsKuotaTerpenuhi())
            {
                hargaFinal = this._hargaDiskon.Value;
            }
            else
            {
                hargaFinal = this._hargaDasar;
            }

            return hargaFinal;
        }

        public long HitungDiskon()
        {
            long diskon;

            if (this._idPo.HasValue && this._jenisPo == "Gotong Royong" && this._targetKuota.HasValue && this._hargaDiskon.HasValue && this.IsKuotaTerpenuhi())
            {
                diskon = this._hargaDasar - this._hargaDiskon.Value;
            }
            else
            {
                diskon = 0;
            }

            return diskon;
        }

        // === IMPLEMENTASI IQuotaTrackable ===
        public int GetTargetKuota()
        {
            int kuota;
            if (this._targetKuota.HasValue)
            {
                kuota = this._targetKuota.Value;
            }
            else
            {
                kuota = 0;
            }
            return kuota;
        }

        public int GetTerpesan()
        {
            return this._terpesan;
        }

        public int GetSisaKuota()
        {
            int sisa;
            if (!this._targetKuota.HasValue)
            {
                sisa = int.MaxValue;
            }
            else
            {
                sisa = this._targetKuota.Value - this._terpesan;
            }
            return sisa;
        }

        public bool IsKuotaTerpenuhi()
        {
            bool terpenuhi;
            if (!this._targetKuota.HasValue)
            {
                terpenuhi = false;
            }
            else if (this._terpesan >= this._targetKuota.Value)
            {
                terpenuhi = true;
            }
            else
            {
                terpenuhi = false;
            }
            return terpenuhi;
        }
    }
}
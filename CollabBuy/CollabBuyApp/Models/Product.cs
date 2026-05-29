using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk entitas Produk.
    /// Menerapkan IValidatable (validasi bisnis), ICalculatable (kalkulasi harga), 
    /// dan IQuotaTrackable (pemantauan kuota preorder).
    /// 
    /// Pemetaan Database:
    /// - Tabel: products
    /// - Kolom foto_produk: BYTEA (Menyimpan file gambar di database)
    /// - Function: cek_harga_saat_ini() -> Diimplementasikan di HitungTotal()
    /// - Trigger: trg_cek_kepemilikan_po -> Diimplementasikan di Validate()
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
        private int _terpesan; // In-Memory: Menyimpan jumlah yang sudah dipesan di RAM
        private byte[] _fotoProduk; // Tambahan untuk BYTEA di Database

        // Properti bantu untuk kalkulasi (di-set oleh repository saat load dari DB)
        private string _jenisPo = "Biasa";


        // === KONSTRUKTOR ===
        public Product(int idPenjual, int idKategori, string namaProduk, int hargaDasar)
        {
            _idPenjual = idPenjual;
            _idKategori = idKategori;
            SetNamaProduk(namaProduk);
            SetHargaDasar(hargaDasar);

            _minOrder = 1;
            _terpesan = 0;
        }


        // === PROPERTI (Getter saja, Setter dipisah sebagai method dengan validasi) ===
        public int GetIdProduk() { return _idProduk; }
        public void SetIdProduk(int id) { _idProduk = id; }

        public int GetIdPenjual() { return _idPenjual; }

        public int? GetIdPo() { return _idPo; }
        public void SetIdPo(int? idPo) { _idPo = idPo; }

        public int GetIdKategori() { return _idKategori; }

        public string GetNamaProduk() { return _namaProduk; }
        public void SetNamaProduk(string nama)
        {
            if (string.IsNullOrEmpty(nama))
            {
                throw new InvalidOrderException("Nama produk tidak boleh kosong!", "nama_produk", "PRODUK_INVALID");
            }
            _namaProduk = nama;
        }

        public string GetDeskripsi() { return _deskripsi; }
        public void SetDeskripsi(string deskripsi) { _deskripsi = deskripsi; }

        public int GetMinOrder() { return _minOrder; }
        public void SetMinOrder(int min)
        {
            if (min <= 0)
            {
                throw new InvalidOrderException("Minimal order harus lebih dari 0!", "min_order", "MIN_ORDER_INVALID");
            }
            _minOrder = min;
        }

        public void SetJenisPo(string jenis)
        {
            _jenisPo = jenis;
        }


        // === METHOD BISNIS LOGIC (Rich Domain Model) ===

        /// <summary>
        /// Menetapkan harga dasar dengan validasi.
        /// </summary>
        public void SetHargaDasar(int harga)
        {
            if (harga <= 0)
            {
                throw new InvalidOrderException("Harga dasar harus lebih dari 0!", "harga_dasar", "HARGA_INVALID");
            }
            _hargaDasar = harga;
        }

        /// <summary>
        /// Menetapkan harga diskon dengan validasi bisnis.
        /// Aturan: Harga diskon tidak boleh sama atau lebih mahal dari harga dasar.
        /// </summary>
        public void SetHargaDiskon(int? harga)
        {
            if (harga.HasValue && harga.Value >= _hargaDasar)
            {
                throw new InvalidOrderException("Harga diskon harus lebih kecil dari harga dasar!", "harga_diskon", "DISKON_INVALID");
            }
            _hargaDiskon = harga;
        }

        public void SetTargetKuota(int? kuota)
        {
            if (kuota.HasValue && kuota.Value <= 0)
            {
                throw new InvalidOrderException("Target kuota harus lebih dari 0!", "target_kuota", "KUOTA_INVALID");
            }
            _targetKuota = kuota;
        }

        /// <summary>
        /// Menambah jumlah pesanan di memori (RAM).
        /// Dipanggil oleh CartManager saat menambah detail pesanan.
        /// </summary>
        public void TambahPesanan(int jumlah)
        {
            if (jumlah < _minOrder)
            {
                throw new InvalidOrderException("Jumlah pesanan kurang dari minimal order (" + _minOrder + ")!", "jumlah_pesanan", "QTY_MIN_INVALID");
            }
            _terpesan = _terpesan + jumlah;
        }

        /// <summary>
        /// Menetapkan foto produk dengan validasi ukuran.
        /// Ditangani di Model (Sub-bab 3.2 Laporan: Business Rule Validation).
        /// </summary>
        public void SetFotoProduk(byte[] foto)
        {
            // Validasi: Maksimal ukuran file 2MB (2 * 1024 * 1024 = 2097152 byte)
            if (foto != null && foto.Length > 2097152)
            {
                throw new InvalidOrderException("Ukuran foto produk maksimal 2MB!", "foto_produk", "FOTO_OVERSIZE");
            }
            _fotoProduk = foto;
        }

        public byte[] GetFotoProduk()
        {
            return _fotoProduk;
        }


        // === IMPLEMENTASI IValidatable ===

        /// <summary>
        /// Memvalidasi seluruh aturan bisnis produk sebelum disimpan.
        /// Pemetaan DB: Menggantikan sebagian logika Trigger trg_cek_kepemilikan_po
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(_namaProduk))
            {
                throw new InvalidOrderException("Validasi gagal: Nama produk kosong.", "nama_produk", "PRODUK_INVALID");
            }
            if (_hargaDasar <= 0)
            {
                throw new InvalidOrderException("Validasi gagal: Harga dasar tidak valid.", "harga_dasar", "HARGA_INVALID");
            }
            if (_hargaDiskon.HasValue && _hargaDiskon.Value >= _hargaDasar)
            {
                throw new InvalidOrderException("Validasi gagal: Harga diskon >= Harga dasar.", "harga_diskon", "DISKON_INVALID");
            }
        }


        // === IMPLEMENTASI ICalculatable ===

        /// <summary>
        /// Menghitung harga saat ini.
        /// Pemetaan DB: Ini adalah logika dari Function PostgreSQL cek_harga_saat_ini().
        /// PERBAIKAN: Hanya menerapkan harga diskon JIKA jenis_po == "Gotong Royong".
        /// </summary>
        public long HitungTotal()
        {
            // Cek apakah PO Gotong Royong, punya target kuota, ada harga diskon, dan kuota sudah terpenuhi
            if (_idPo.HasValue && _jenisPo == "Gotong Royong" && _targetKuota.HasValue && _hargaDiskon.HasValue)
            {
                if (IsKuotaTerpenuhi())
                {
                    return _hargaDiskon.Value;
                }
            }
            return _hargaDasar;
        }

        /// <summary>
        /// Menghitung selisih potongan harga.
        /// PERBAIKAN: Hanya menghitung diskon JIKA jenis_po == "Gotong Royong".
        /// </summary>
        public long HitungDiskon()
        {
            if (_idPo.HasValue && _jenisPo == "Gotong Royong" && _targetKuota.HasValue && _hargaDiskon.HasValue && IsKuotaTerpenuhi())
            {
                return _hargaDasar - _hargaDiskon.Value;
            }
            return 0;
        }


        // === IMPLEMENTASI IQuotaTrackable ===

        public int GetTargetKuota()
        {
            if (_targetKuota.HasValue) { return _targetKuota.Value; }
            return 0;
        }

        public int GetTerpesan()
        {
            return _terpesan;
        }

        public int GetSisaKuota()
        {
            if (!_targetKuota.HasValue) { return int.MaxValue; }
            return _targetKuota.Value - _terpesan;
        }

        public bool IsKuotaTerpenuhi()
        {
            if (!_targetKuota.HasValue) { return false; }
            return _terpesan >= _targetKuota.Value;
        }


        // === TAMBAHAN GETTER UNTUK REPOSITORY ===

        /// <summary>
        /// Mengambil nilai harga dasar murni (tanpa logika gotong royong).
        /// Dipakai Repository untuk INSERT/UPDATE ke database.
        /// </summary>
        public int GetHargaDasar()
        {
            return _hargaDasar;
        }

        /// <summary>
        /// Mengambil nilai harga diskon murni.
        /// Dipakai Repository untuk INSERT/UPDATE ke database.
        /// </summary>
        public int? GetHargaDiskon()
        {
            return _hargaDiskon;
        }
    }
}
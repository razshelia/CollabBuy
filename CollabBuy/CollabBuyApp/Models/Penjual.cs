using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas turunan dari User yang bertindak sebagai penjual/danus.
    /// Mengimplementasikan IApprovable terkait verifikasi KTM dan toko.
    /// </summary>
    public class Penjual : User, IApprovable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private string _nim;
        private string _namaToko;
        private int _tahunMasuk;
        private byte[] _buktiKtm;
        private bool _isVerifikasi;
        private string _alasanPenolakan;
        private List<Product> _katalogLapak; // Relasi Object

        // === PROPERTIES ===
        public string Nim
        {
            get { return this._nim; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("NIM penjual wajib diisi!", "nim", "PENJUAL_NIM_KOSONG");

                if (value.Trim().Length < 8)
                    throw new InvalidOrderException("NIM minimal 8 karakter!", "nim", "PENJUAL_NIM_PENDEK");

                if (value.Trim().Length > 20)
                    throw new InvalidOrderException("NIM maksimal 20 karakter!", "nim", "PENJUAL_NIM_PANJANG");

                this._nim = value.Trim();
            }
        }

        public string NamaToko
        {
            get { return this._namaToko; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Nama toko wajib diisi!", "nama_toko", "PENJUAL_TOKO_KOSONG");

                if (value.Trim().Length < 3)
                    throw new InvalidOrderException("Nama toko minimal 3 karakter!", "nama_toko", "PENJUAL_TOKO_PENDEK");

                if (value.Trim().Length > 60)
                    throw new InvalidOrderException("Nama toko maksimal 60 karakter!", "nama_toko", "PENJUAL_TOKO_PANJANG");

                this._namaToko = value.Trim();
            }
        }

        public int TahunMasuk
        {
            get { return this._tahunMasuk; }
            set
            {
                if (value < 2000 || value > DateTime.Now.Year)
                    throw new InvalidOrderException("Tahun masuk tidak valid!", "tahun_masuk", "TAHUN_INVALID");

                this._tahunMasuk = value;
            }
        }

        public byte[] BuktiKtm
        {
            get { return this._buktiKtm; }
            set
            {
                if (value == null || value.Length == 0)
                    throw new InvalidOrderException("File bukti KTM tidak boleh kosong!", "bukti_ktm", "KTM_KOSONG");

                if (value.Length > 2097152) // 2MB
                    throw new InvalidOrderException("Ukuran file KTM maksimal 2MB!", "bukti_ktm", "KTM_OVERSIZE");

                this._buktiKtm = value;
            }
        }

        // List hanya menggunakan get agar aman dari overwrite
        public List<Product> KatalogLapak
        {
            get { return this._katalogLapak; }
        }

        // === KONSTRUKTOR ===
        public Penjual(string nama, string username, string password)
            : base(nama, username, password, "Penjual")
        {
            // Isi backing field langsung agar tidak memicu error dari guard clause Property
            this._isVerifikasi = false;
            this._alasanPenolakan = "";
            this._nim = "";
            this._namaToko = "";
            this._tahunMasuk = DateTime.Now.Year;
            this._buktiKtm = null;
            this._katalogLapak = new List<Product>();
        }

        // === OVERRIDE METHOD ABSTRAK (POLIMORFISME) ===
        public override string GetTipeUser()
        {
            return this._isVerifikasi ? "Penjual Terverifikasi" : "Penjual Belum Verifikasi";
        }

        // === IMPLEMENTASI IApprovable ===
        public void Approve()
        {
            this._isVerifikasi = true;
            this._alasanPenolakan = "";
        }

        public void Reject(string alasan)
        {
            if (string.IsNullOrWhiteSpace(alasan))
            {
                throw new InvalidOrderException("Alasan penolakan KTM wajib diisi oleh Admin!", "alasan_tolak", "REJECT_INVALID");
            }

            this._isVerifikasi = false;
            this._alasanPenolakan = alasan.Trim();
        }

        // Tetap menggunakan method karena merupakan kontrak dari interface IApprovable
        public bool GetStatusPersetujuan()
        {
            return this._isVerifikasi;
        }

        public string GetAlasanPenolakan()
        {
            return this._alasanPenolakan;
        }

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR KHUSUS PENJUAL
        // =========================================================

        public void TambahProdukKeLapak(Product produkBaru)
        {
            if (produkBaru == null)
            {
                throw new InvalidOrderException("Data produk tidak boleh kosong!", "produk", "PRODUK_NULL");
            }

            this._katalogLapak.Add(produkBaru);
        }

        public int DapatkanTotalProdukAktif()
        {
            return this._katalogLapak == null ? 0 : this._katalogLapak.Count;
        }

        public string DapatkanInfoDanus()
        {
            if (string.IsNullOrWhiteSpace(this._namaToko) || string.IsNullOrWhiteSpace(this._nim))
            {
                return "Data Lapak Belum Lengkap";
            }

            return $"🏪 {this._namaToko} | 🎓 NIM: {this._nim} (Angkatan {this._tahunMasuk})";
        }

        public bool ApakahBisaBukaLapak()
        {
            // Langsung evaluasi kondisi boolean
            return this._isVerifikasi && !this.IsDiblokir();
        }

        /// <summary>
        /// Mengecek apakah mahasiswa masih dalam masa studi wajar (Maksimal 7 tahun).
        /// Dihitung dari tahun ini (2026).
        /// </summary>
        public bool ApakahMahasiswaAktif()
        {
            // Cukup 1 baris operasi matematika dan logika
            return (DateTime.Now.Year - this._tahunMasuk) <= 7;
        }

        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(this._nim) || string.IsNullOrWhiteSpace(this._namaToko))
            {
                throw new InvalidOrderException("Data penjual belum lengkap (NIM atau Nama Toko kosong)!", "nim_toko", "PENJUAL_INVALID");
            }
        }
    }
}
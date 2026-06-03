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
        // === PRIVATE FIELDS ===
        private string _nim;
        private string _namaToko;
        private int _tahunMasuk;
        private byte[] _buktiKtm;
        private bool _isVerifikasi;
        private string _alasanPenolakan;

        // Relasi Object: Penjual memiliki banyak Produk
        private List<Product> _katalogLapak;

        // === KONSTRUKTOR ===
        public Penjual(string nama, string username, string password)
            : base(nama, username, password, "Penjual")
        {
            this._isVerifikasi = false;
            this._alasanPenolakan = "";
            this._nim = "";
            this._namaToko = "";
            this._tahunMasuk = DateTime.Now.Year;
            this._buktiKtm = null;
            this._katalogLapak = new List<Product>();
        }

        // === GETTER & SETTER STRICT ENCAPSULATION ===
        public string GetNim()
        {
            return this._nim;
        }

        public void SetNim(string nim)
        {
            if (string.IsNullOrWhiteSpace(nim))
            {
                throw new InvalidOrderException("NIM penjual wajib diisi!", "nim", "PENJUAL_NIM_KOSONG");
            }
            else if (nim.Trim().Length < 8)
            {
                throw new InvalidOrderException("NIM minimal 8 karakter!", "nim", "PENJUAL_NIM_PENDEK");
            }
            else if (nim.Trim().Length > 20)
            {
                throw new InvalidOrderException("NIM maksimal 20 karakter!", "nim", "PENJUAL_NIM_PANJANG");
            }
            else
            {
                this._nim = nim.Trim();
            }
        }

        //public string Nim
        //{
        //    get { return this._nim; }
        //    set { this._nim = value; }
        //}

        public string GetNamaToko()
        {
            return this._namaToko;
        }

        public void SetNamaToko(string namaToko)
        {
            if (string.IsNullOrWhiteSpace(namaToko))
            {
                throw new InvalidOrderException("Nama toko wajib diisi!", "nama_toko", "PENJUAL_TOKO_KOSONG");
            }
            else if (namaToko.Trim().Length < 3)
            {
                throw new InvalidOrderException("Nama toko minimal 3 karakter!", "nama_toko", "PENJUAL_TOKO_PENDEK");
            }
            else if (namaToko.Trim().Length > 60)
            {
                throw new InvalidOrderException("Nama toko maksimal 60 karakter!", "nama_toko", "PENJUAL_TOKO_PANJANG");
            }
            else
            {
                this._namaToko = namaToko.Trim();
            }
        }

        public int GetTahunMasuk()
        {
            return this._tahunMasuk;
        }

        public void SetTahunMasuk(int tahun)
        {
            if (tahun < 2000 || tahun > DateTime.Now.Year)
            {
                throw new InvalidOrderException("Tahun masuk tidak valid!", "tahun_masuk", "TAHUN_INVALID");
            }
            else
            {
                this._tahunMasuk = tahun;
            }
        }

        public byte[] GetBuktiKtm()
        {
            return this._buktiKtm;
        }

        public void SetBuktiKtm(byte[] ktm)
        {
            if (ktm == null || ktm.Length == 0)
            {
                throw new InvalidOrderException("File bukti KTM tidak boleh kosong!", "bukti_ktm", "KTM_KOSONG");
            }
            else if (ktm.Length > 2097152) // 2MB
            {
                throw new InvalidOrderException("Ukuran file KTM maksimal 2MB!", "bukti_ktm", "KTM_OVERSIZE");
            }
            else
            {
                this._buktiKtm = ktm;
            }
        }

        // === OVERRIDE METHOD ABSTRAK (POLIMORFISME) ===
        public override string GetTipeUser()
        {
            string tipe;
            if (this._isVerifikasi)
            {
                tipe = "Penjual Terverifikasi";
            }
            else
            {
                tipe = "Penjual Belum Verifikasi";
            }
            return tipe;
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
            else
            {
                this._isVerifikasi = false;
                this._alasanPenolakan = alasan.Trim();
            }
        }

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
            else
            {
                this._katalogLapak.Add(produkBaru);
            }
        }

        public int DapatkanTotalProdukAktif()
        {
            int total;
            if (this._katalogLapak == null)
            {
                total = 0;
            }
            else
            {
                total = this._katalogLapak.Count;
            }
            return total;
        }

        public string DapatkanInfoDanus()
        {
            string infoLengkap;

            if (string.IsNullOrWhiteSpace(this._namaToko) || string.IsNullOrWhiteSpace(this._nim))
            {
                infoLengkap = "Data Lapak Belum Lengkap";
            }
            else
            {
                infoLengkap = $"🏪 {this._namaToko} | 🎓 NIM: {this._nim} (Angkatan {this._tahunMasuk})";
            }

            return infoLengkap;
        }

        public bool ApakahBisaBukaLapak()
        {
            bool bisaBuka;

            if (this._isVerifikasi && !this.IsDiblokir())
            {
                bisaBuka = true;
            }
            else
            {
                bisaBuka = false;
            }

            return bisaBuka;
        }

        /// <summary>
        /// Mengecek apakah mahasiswa masih dalam masa studi wajar (Maksimal 7 tahun).
        /// Dihitung dari tahun ini (2026).
        /// </summary>
        public bool ApakahMahasiswaAktif()
        {
            bool isAktif;
            int lamaStudi = DateTime.Now.Year - this._tahunMasuk;

            if (lamaStudi <= 7)
            {
                isAktif = true;
            }
            else
            {
                isAktif = false;
            }

            return isAktif;
        }

        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            bool validasiPenjualSelesai;

            base.Validate();

            if (string.IsNullOrWhiteSpace(this._nim) || string.IsNullOrWhiteSpace(this._namaToko))
            {
                throw new InvalidOrderException("Data penjual belum lengkap (NIM atau Nama Toko kosong)!", "nim_toko", "PENJUAL_INVALID");
            }
            else
            {
                validasiPenjualSelesai = true;
            }
        }
    }
}
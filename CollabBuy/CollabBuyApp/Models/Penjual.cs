using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas turunan dari User yang bertindak sebagai penjual/danus.
    /// Mengimplementasikan IApprovable terkait verifikasi KTM dan toko.
    /// 
    /// Pemetaan Database:
    /// - Tabel: users (peran = 'Penjual') & verifications
    /// </summary>
    public class Penjual : User, IApprovable
    {
        // === PRIVATE FIELDS (Spesifik Penjual) ===
        private string _nim;
        private string _namaToko;
        private int _tahunMasuk;
        private byte[] _buktiKtm;
        private bool _isVerifikasi;

        // === KONSTRUKTOR ===
        public Penjual(string nama, string username, string password)
            : base(nama, username, password, "Penjual")
        {
            _isVerifikasi = false;
        }

        // === GETTER & SETTER ===
        public string GetNim() { return _nim; }
        public void SetNim(string nim)
        {
            if (string.IsNullOrEmpty(nim))
            {
                throw new InvalidOrderException("NIM penjual wajib diisi!", "nim", "PENJUAL_NIM_KOSONG");
            }
            _nim = nim;
        }

        public string GetNamaToko() { return _namaToko; }
        public void SetNamaToko(string namaToko)
        {
            if (string.IsNullOrEmpty(namaToko))
            {
                throw new InvalidOrderException("Nama toko wajib diisi!", "nama_toko", "PENJUAL_TOKO_KOSONG");
            }
            _namaToko = namaToko;
        }

        public int GetTahunMasuk() { return _tahunMasuk; }
        public void SetTahunMasuk(int tahun)
        {
            if (tahun < 2000 || tahun > DateTime.Now.Year)
            {
                throw new InvalidOrderException("Tahun masuk tidak valid!", "tahun_masuk", "TAHUN_INVALID");
            }
            _tahunMasuk = tahun;
        }

        // === TAMBAHAN FIELD BUKTI KTM (BYTEA) ===
        

        /// <summary>
        /// Mengambil data bukti KTM dalam format byte[].
        /// </summary>
        public byte[] GetBuktiKtm()
        {
            return _buktiKtm;
        }

        /// <summary>
        /// Menetapkan file bukti KTM dengan validasi ukuran.
        /// </summary>
        public void SetBuktiKtm(byte[] ktm)
        {
            // Validasi: Maksimal ukuran file 2MB (2 * 1024 * 1024 = 2097152 byte)
            if (ktm != null && ktm.Length > 2097152)
            {
                throw new InvalidOrderException("Ukuran file KTM maksimal 2MB!", "bukti_ktm", "KTM_OVERSIZE");
            }
            _buktiKtm = ktm;
        }


        // === OVERRIDE METHOD ABSTRAK (POLIMORFISME) ===
        public override string GetTipeUser()
        {
            if (_isVerifikasi)
            {
                return "Penjual Terverifikasi";
            }
            return "Penjual Belum Verifikasi";
        }


        // === IMPLEMENTASI IApprovable ===
        /// <summary>
        /// Menyetujui verifikasi penjual oleh Admin.
        /// Pemetaan DB: verifications.is_verifikasi = TRUE
        /// </summary>
        public void Approve()
        {
            _isVerifikasi = true;
        }

        public void Reject(string alasan)
        {
            _isVerifikasi = false;
            // Bisa ditambahkan logika penyimpanan alasan reject jika dibutuhkan
        }

        public bool GetStatusPersetujuan()
        {
            return _isVerifikasi;
        }


        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            base.Validate(); // Panggil validasi dasar dari User
            if (string.IsNullOrEmpty(_nim))
            {
                throw new InvalidOrderException("Data penjual belum lengkap (NIM kosong)!", "nim", "PENJUAL_INVALID");
            }
            if (string.IsNullOrEmpty(_namaToko))
            {
                throw new InvalidOrderException("Data penjual belum lengkap (Nama Toko kosong)!", "nama_toko", "PENJUAL_INVALID");
            }
        }
    }
}
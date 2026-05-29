using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk permohonan verifikasi menjadi Penjual.
    /// Mengimplementasikan IValidatable dan IApprovable.
    /// 
    /// Pemetaan Database:
    /// - Tabel: verifications
    /// - Kolom: is_verifikasi, bukti_ktm
    /// </summary>
    public class Verification : IValidatable, IApprovable
    {
        // === PRIVATE FIELDS ===
        private int _idVerifikasi;
        private int _idUser;
        private string _nim;
        private string _namaToko;
        private byte[] _buktiKtm; // Menggunakan byte[] untuk tipe BYTEA di PostgreSQL
        private int _tahunMasuk;
        private bool _isVerifikasi;

        // === KONSTRUKTOR ===
        public Verification(int idUser, string nim, string namaToko, byte[] buktiKtm, int tahunMasuk)
        {
            _idUser = idUser;
            SetNim(nim);
            SetNamaToko(namaToko);
            SetBuktiKtm(buktiKtm);
            SetTahunMasuk(tahunMasuk);
            _isVerifikasi = false;
        }

        // === GETTER & SETTER ===
        public int GetIdVerifikasi() { return _idVerifikasi; }
        public void SetIdVerifikasi(int id) { _idVerifikasi = id; }

        public int GetIdUser() { return _idUser; }

        public string GetNim() { return _nim; }
        public void SetNim(string nim)
        {
            if (string.IsNullOrEmpty(nim))
            {
                throw new InvalidOrderException("NIM wajib diisi untuk verifikasi!", "nim", "NIM_KOSONG");
            }
            _nim = nim;
        }

        public string GetNamaToko() { return _namaToko; }
        public void SetNamaToko(string namaToko)
        {
            if (string.IsNullOrEmpty(namaToko))
            {
                throw new InvalidOrderException("Nama toko wajib diisi!", "nama_toko", "TOKO_KOSONG");
            }
            _namaToko = namaToko;
        }

        public byte[] GetBuktiKtm() { return _buktiKtm; }
        public void SetBuktiKtm(byte[] bukti)
        {
            if (bukti == null || bukti.Length == 0)
            {
                throw new InvalidOrderException("Bukti KTM wajib di-upload!", "bukti_ktm", "KTM_KOSONG");
            }
            _buktiKtm = bukti;
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

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (string.IsNullOrEmpty(_nim)) { throw new InvalidOrderException("Verifikasi gagal: NIM kosong.", "nim", "VERIF_INVALID"); }
            if (_buktiKtm == null || _buktiKtm.Length == 0) { throw new InvalidOrderException("Verifikasi gagal: KTM belum di-upload.", "bukti_ktm", "VERIF_INVALID"); }
        }

        // === IMPLEMENTASI IApprovable ===
        public void Approve()
        {
            _isVerifikasi = true;
        }

        public void Reject(string alasan)
        {
            _isVerifikasi = false;
            // Alasan penolakan verifikasi bisa dicatat di log atau tabel terpisah jika perlu
        }

        public bool GetStatusPersetujuan()
        {
            return _isVerifikasi;
        }
    }
}
using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas turunan dari User yang bertindak sebagai Administrator sistem.
    /// Bertugas memverifikasi toko, membalas aduan, dan mengawasi sistem.
    /// </summary>
    public class Admin : User
    {
        // === PRIVATE FIELDS ===
        private string _kodeAksesAdmin;

        // === KONSTRUKTOR ===
        public Admin(string nama, string username, string password, string kodeAkses)
            : base(nama, username, password, "Admin")
        {
            this.SetKodeAksesAdmin(kodeAkses);
        }

        // === ENKAPSULASI STRICT ===
        public string GetKodeAksesAdmin()
        {
            return this._kodeAksesAdmin;
        }

        public void SetKodeAksesAdmin(string kodeAkses)
        {
            if (string.IsNullOrWhiteSpace(kodeAkses))
            {
                throw new InvalidOrderException("Kode akses rahasia Admin tidak boleh kosong!", "kode_akses", "ADMIN_KODE_KOSONG");
            }
            else if (kodeAkses.Trim().Length < 6)
            {
                throw new InvalidOrderException("Kode akses Admin minimal 6 karakter!", "kode_akses", "ADMIN_KODE_PENDEK");
            }
            else
            {
                this._kodeAksesAdmin = kodeAkses.Trim();
            }
        }

        // === OVERRIDE METHOD ABSTRAK (POLIMORFISME) ===
        public override string GetTipeUser()
        {
            return "Administrator Sistem";
        }

        // === BEHAVIOR KHUSUS ADMIN ===

        /// <summary>
        /// Validasi tambahan khusus untuk login Admin (Double Security).
        /// </summary>
        public bool ApakahKodeAksesValid(string inputKode)
        {
            bool isValid;
            if (this._kodeAksesAdmin == inputKode)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Mengubah nama UI admin menjadi format resmi.
        /// </summary>
        public string DapatkanNamaResmiMimin()
        {
            string namaResmi;
            if (string.IsNullOrWhiteSpace(this.GetNama()))
            {
                namaResmi = "CS CollabBuy";
            }
            else
            {
                namaResmi = "[ADMIN] " + this.GetNama();
            }
            return namaResmi;
        }

        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            bool validasiAdminSelesai;

            base.Validate(); // Validasi nama, email, username dari class induk

            if (string.IsNullOrWhiteSpace(this._kodeAksesAdmin))
            {
                throw new InvalidOrderException("Validasi Admin Gagal: Kode Akses tidak ada.", "kode_akses", "ADMIN_INVALID");
            }
            else
            {
                validasiAdminSelesai = true; // Assignment nyata
            }
        }
    }
}
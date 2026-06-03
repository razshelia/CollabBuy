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
        // === PRIVATE FIELDS (Backing Field) ===
        private string _kodeAksesAdmin;

        // === PROPERTIES (Get & Set dalam satu blok) ===
        public string KodeAksesAdmin
        {
            get { return this._kodeAksesAdmin; }
            set
            {
                // Guard clause 1
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Kode akses rahasia Admin tidak boleh kosong!", "kode_akses", "ADMIN_KODE_KOSONG");
                }

                // Guard clause 2 (Tidak perlu else)
                if (value.Trim().Length < 6)
                {
                    throw new InvalidOrderException("Kode akses Admin minimal 6 karakter!", "kode_akses", "ADMIN_KODE_PENDEK");
                }

                this._kodeAksesAdmin = value.Trim();
            }
        }

        // === KONSTRUKTOR ===
        public Admin(string nama, string username, string password, string kodeAkses)
            : base(nama, username, password, "Admin")
        {
            // Memanggil setter dari Properti
            this.KodeAksesAdmin = kodeAkses;
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
            // Best Practice: Langsung return hasil evaluasi boolean, tidak perlu if-else
            return this._kodeAksesAdmin == inputKode;
        }

        /// <summary>
        /// Mengubah nama UI admin menjadi format resmi.
        /// </summary>
        public string DapatkanNamaResmiMimin()
        {
            // Best Practice: Early return, hapus else
            if (string.IsNullOrWhiteSpace(this.GetNama()))
            {
                return "CS CollabBuy";
            }

            return "[ADMIN] " + this.GetNama();
        }

        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            base.Validate(); // Validasi nama, email, username dari class induk

            if (string.IsNullOrWhiteSpace(this._kodeAksesAdmin))
            {
                throw new InvalidOrderException("Validasi Admin Gagal: Kode Akses tidak ada.", "kode_akses", "ADMIN_INVALID");
            }

            // Variabel validasiAdminSelesai dan blok else dihapus karena tidak memberikan efek apa-apa di metode void
        }
    }
}
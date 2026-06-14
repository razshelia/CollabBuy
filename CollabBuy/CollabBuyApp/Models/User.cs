using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Abstract class induk untuk semua jenis pengguna.
    /// Mengimplementasikan IValidatable untuk data dasar.
    /// 
    /// PERBAIKAN:
    /// 1. Menghapus seluruh method getter/setter gaya lama (GetNama, SetNama, dll) agar tidak redundan.
    /// 2. Menggunakan C# Properties murni dengan Strict OOP (if-else berlapis).
    /// </summary>
    public abstract class User : IValidatable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idUser;
        private string _nama;
        private string _nomorTelepon;
        private string _email;
        private string _username;
        private string _password;
        private string _peran;
        private bool _isDiblokir;
        private string _alasanBlokir;

        // =========================================================
        // C# PROPERTIES (PENGGANTI METHOD GET/SET)
        // =========================================================

        public int IdUser
        {
            get { return this._idUser; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID User tidak valid!", "id_user", "USER_ID_INVALID");
                this._idUser = value;
            }
        }

        public string Nama
        {
            get { return this._nama; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Nama pengguna tidak boleh kosong!", "nama", "USER_NAMA_KOSONG");
                if (value.Trim().Length < 3)
                    throw new InvalidOrderException("Nama pengguna minimal 3 karakter!", "nama", "USER_NAMA_TERLALU_PENDEK");
                if (value.Trim().Length > 100)
                    throw new InvalidOrderException("Nama pengguna maksimal 100 karakter!", "nama", "USER_NAMA_TERLALU_PANJANG");
                this._nama = value.Trim();
            }
        }

        public string Username
        {
            get { return this._username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                    throw new InvalidOrderException("Username minimal 4 karakter!", "username", "USER_UNAME_INVALID");
                this._username = value.Trim().ToLower();
            }
        }

        public string Password
        {
            get { return this._password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Password tidak boleh kosong!", "password", "USER_PASS_KOSONG");
                if (value.Length < 8)
                    throw new InvalidOrderException("Password minimal 8 karakter!", "password", "USER_PASS_TERLALU_PENDEK");
                this._password = value;
            }
        }

        public string NomorTelepon
        {
            get { return this._nomorTelepon; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) { this._nomorTelepon = ""; return; }
                if (value.Length < 9 || value.Length > 15)
                    throw new InvalidOrderException("Format Nomor WhatsApp tidak valid (harus 9-15 karakter)!", "nomorTelepon", "USER_TELP_INVALID");
                this._nomorTelepon = value.Trim();
            }
        }

        public string Email
        {
            get { return this._email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) { this._email = ""; return; }
                if (value.Trim().Length < 6)
                    throw new InvalidOrderException("Format email tidak valid! Terlalu pendek.", "email", "USER_EMAIL_INVALID");
                if (!value.Contains("@") || !value.Contains("."))
                    throw new InvalidOrderException("Format email tidak valid! (Harus mengandung @ dan .)", "email", "USER_EMAIL_INVALID");
                if (value.IndexOf("@") < 3)
                    throw new InvalidOrderException("Format email tidak valid! Bagian sebelum @ tidak boleh kosong.", "email", "USER_EMAIL_INVALID");
                if (value.LastIndexOf(".") < value.IndexOf("@") + 2)
                    throw new InvalidOrderException("Format email tidak valid! Domain tidak lengkap.", "email", "USER_EMAIL_INVALID");
                this._email = value.Trim().ToLower();
            }
        }

        public string Peran
        {
            get { return this._peran; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Peran pengguna tidak boleh kosong!", "peran", "USER_PERAN_KOSONG");
                if (value != "Admin" && value != "Penjual" && value != "User")
                    throw new InvalidOrderException("Peran tidak valid! Harus Admin, Penjual, atau User.", "peran", "USER_PERAN_INVALID");
                this._peran = value.Trim();
            }
        }

        public bool IsDiblokir
        {
            get { return this._isDiblokir; }
            set
            {
                this._isDiblokir = value;
                if (!value) this._alasanBlokir = "";
            }
        }

        public string AlasanBlokir
        {
            get{ return this._alasanBlokir; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._alasanBlokir = "";
                }
                else
                {
                    this._alasanBlokir = value.Trim();
                }
            }
        }

        protected User(string nama, string username, string password, string peran)
        {
            this.Nama = nama;
            this.Username = username;
            this._password = password;
            this.Peran = peran;
            this._isDiblokir = false;
            this._alasanBlokir = "";
            this._nomorTelepon = "";
            this._email = "";
        }

        // === METHOD ABSTRAK ===
        public abstract string GetTipeUser();

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR
        // =========================================================

        public void Blokir(string alasan)
        {
            if (string.IsNullOrWhiteSpace(alasan))
                throw new InvalidOrderException("Alasan pemblokiran wajib diisi!", "alasan_blokir", "BLOKIR_INVALID");
            this._isDiblokir = true;
            this._alasanBlokir = alasan.Trim();
        }

        public void BukaBlokir()
        {
            this._isDiblokir = false;
            this._alasanBlokir = "";
        }

        public string DapatkanStatusAkun()
            => this._isDiblokir ? $"🚫 Terblokir: {this._alasanBlokir}" : "✅ Aktif & Aman";

        public string DapatkanInfoKontak()
        {
            string infoTelp = string.IsNullOrWhiteSpace(this._nomorTelepon) ? "No HP Belum Diisi" : this._nomorTelepon;
            string infoEmail = string.IsNullOrWhiteSpace(this._email) ? "Email Belum Diisi" : this._email;
            return $"📞 {infoTelp} | ✉️ {infoEmail}";
        }

        public bool UbahPassword(string passLama, string passBaru)
        {
            if (this._password != passLama)
                throw new InvalidOrderException("Gagal: Password lama tidak cocok!", "password", "UBAH_PASS_GAGAL");

            this.Password = passBaru;
            return true;
        }

        // Overload tanpa parameter (default 1 karakter)
        public string DapatkanInisialProfil()
        {
            return DapatkanInisialProfil(1);
        }

        public string DapatkanInisialProfil(int jumlahKarakter)
        {
            if (string.IsNullOrWhiteSpace(this._nama)) return "U";
            int ambil = Math.Min(jumlahKarakter, this._nama.Length);
            return this._nama.Substring(0, ambil).ToUpper();
        }

        public string DapatkanLinkWhatsApp()
        {
            return User.DapatkanLinkWhatsApp(this._nomorTelepon);
        }

        public static string DapatkanLinkWhatsApp(string nomorTelepon)
        {
            if (string.IsNullOrWhiteSpace(nomorTelepon)) return "";
            return nomorTelepon.StartsWith("0")
                ? "https://wa.me/62" + nomorTelepon.Substring(1)
                : "https://wa.me/" + nomorTelepon;
        }

        // === IMPLEMENTASI IValidatable ===
        public virtual void Validate()
        {
            if (string.IsNullOrWhiteSpace(this._nama) || string.IsNullOrWhiteSpace(this._username))
                throw new InvalidOrderException("Validasi gagal: Nama/Username tidak boleh kosong.", "nama_username", "USER_INVALID");
        }
    }
}
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Abstract class induk untuk semua jenis pengguna.
    /// Mengimplementasikan IValidatable untuk data dasar.
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

        // === PROPERTIES ===

        // Auto-Properties untuk status blokir agar aman (Read-Only dari luar)
        public bool IsDiblokir { get; private set; }
        public string AlasanBlokir { get; private set; }

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
                this._username = value.Trim();
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Nomor WhatsApp tidak boleh kosong!", "nomorTelepon", "USER_TELP_KOSONG");

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
                // Boleh kosong (nullable di DB), validasi berlapis hanya jika diisi
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._email = "";
                    return;
                }

                if (value.Trim().Length < 6)
                    throw new InvalidOrderException("Format email tidak valid! Terlalu pendek.", "email", "USER_EMAIL_INVALID");

                if (!value.Contains("@") || !value.Contains("."))
                    throw new InvalidOrderException("Format email tidak valid! (Harus mengandung @ dan .)", "email", "USER_EMAIL_INVALID");

                if (value.IndexOf("@") < 1)
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

        // === KONSTRUKTOR ===
        protected User(string nama, string username, string password, string peran)
        {
            this.Nama = nama;
            this.Username = username;
            this.Password = password;
            this.Peran = peran;

            this.IsDiblokir = false;
            this.AlasanBlokir = "";
            this._nomorTelepon = ""; // Direct assignment menghindari Guard Clause
            this._email = "";        // Direct assignment menghindari Guard Clause
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

            this.IsDiblokir = true;
            this.AlasanBlokir = alasan.Trim();
        }

        public void BukaBlokir()
        {
            this.IsDiblokir = false;
            this.AlasanBlokir = "";
        }

        public string DapatkanStatusAkun()
        {
            return this.IsDiblokir ? $"🚫 Terblokir: {this.AlasanBlokir}" : "✅ Aktif & Aman";
        }

        public string DapatkanInfoKontak()
        {
            string infoTelp = string.IsNullOrWhiteSpace(this.NomorTelepon) ? "No HP Belum Diisi" : this.NomorTelepon;
            string infoEmail = string.IsNullOrWhiteSpace(this.Email) ? "Email Belum Diisi" : this.Email;

            return $"{this.Nama} | 📞 {infoTelp} | ✉️ {infoEmail}";
        }

        public bool ApakahAkunAman()
        {
            // Tinggal kembalikan negasi dari IsDiblokir
            return !this.IsDiblokir;
        }

        // === METHOD TAMBAHAN MAKSIMAL (REAL-WORLD SCENARIO) ===

        public bool UbahPassword(string passLama, string passBaru)
        {
            if (this.Password != passLama)
                throw new InvalidOrderException("Gagal: Password lama tidak cocok!", "password", "UBAH_PASS_GAGAL");

            this.Password = passBaru;
            return true; // Langsung kembalikan true jika sukses melewati Guard Clause
        }

        public string DapatkanInisialProfil()
        {
            return string.IsNullOrWhiteSpace(this.Nama) ? "U" : this.Nama.Substring(0, 1).ToUpper();
        }

        public string DapatkanLinkWhatsApp()
        {
            if (string.IsNullOrWhiteSpace(this.NomorTelepon)) return "";

            if (this.NomorTelepon.StartsWith("0"))
                return "https://wa.me/62" + this.NomorTelepon.Substring(1);

            return "https://wa.me/" + this.NomorTelepon;
        }

        // === IMPLEMENTASI IValidatable ===
        public virtual void Validate()
        {
            // Tidak perlu dummy variable, jika error langsung mental ke Exception!
            if (string.IsNullOrWhiteSpace(this.Nama) || string.IsNullOrWhiteSpace(this.Username))
                throw new InvalidOrderException("Validasi gagal: Nama/Username tidak boleh kosong.", "nama_username", "USER_INVALID");

            if (string.IsNullOrWhiteSpace(this.Email) || string.IsNullOrWhiteSpace(this.NomorTelepon))
                throw new InvalidOrderException("Validasi gagal: Kontak Email/Telepon belum lengkap.", "kontak", "USER_INVALID");
        }
    }
}
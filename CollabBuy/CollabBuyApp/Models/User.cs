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
        // === PRIVATE FIELDS ===
        private int _idUser;
        private string _nama;
        private string _nomorTelepon;
        private string _email;
        private string _username;
        private string _password;
        private string _peran;
        private bool _isDiblokir;
        private string _alasanBlokir;

        // === KONSTRUKTOR ===
        protected User(string nama, string username, string password, string peran)
        {
            this.SetNama(nama);
            this.SetUsername(username);
            this.SetPassword(password);
            this.SetPeran(peran);

            this._isDiblokir = false;
            this._alasanBlokir = "";
            this._nomorTelepon = "";
            this._email = "";
        }

        // === GETTER & SETTER (ENKAPSULASI STRICT IF-ELSE) ===
        public int GetIdUser()
        {
            return this._idUser;
        }

        public void SetIdUser(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID User tidak valid!", "id_user", "USER_ID_INVALID");
            }
            else
            {
                this._idUser = id;
            }
        }

        public string GetNama()
        {
            return this._nama;
        }

        public void SetNama(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                throw new InvalidOrderException("Nama pengguna tidak boleh kosong!", "nama", "USER_NAMA_KOSONG");
            }
            else if (nama.Trim().Length < 3)
            {
                throw new InvalidOrderException("Nama pengguna minimal 3 karakter!", "nama", "USER_NAMA_TERLALU_PENDEK");
            }
            else if (nama.Trim().Length > 100)
            {
                throw new InvalidOrderException("Nama pengguna maksimal 100 karakter!", "nama", "USER_NAMA_TERLALU_PANJANG");
            }
            else
            {
                this._nama = nama.Trim();
            }
        }

        public string GetUsername()
        {
            return this._username;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
            {
                throw new InvalidOrderException("Username minimal 4 karakter!", "username", "USER_UNAME_INVALID");
            }
            else
            {
                this._username = username.Trim();
            }
        }

        public string GetPassword()
        {
            return this._password;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOrderException("Password tidak boleh kosong!", "password", "USER_PASS_KOSONG");
            }
            else if (password.Length < 8)
            {
                throw new InvalidOrderException("Password minimal 8 karakter!", "password", "USER_PASS_TERLALU_PENDEK");
            }
            else
            {
                this._password = password;
            }
        }

        public string GetNomorTelepon()
        {
            return this._nomorTelepon;
        }

        public void SetNomorTelepon(string telp)
        {
            if (string.IsNullOrWhiteSpace(telp))
            {
                throw new InvalidOrderException("Nomor WhatsApp tidak boleh kosong!", "nomorTelepon", "USER_TELP_KOSONG");
            }
            else if (telp.Length < 9 || telp.Length > 15)
            {
                throw new InvalidOrderException("Format Nomor WhatsApp tidak valid (harus 9-15 karakter)!", "nomorTelepon", "USER_TELP_INVALID");
            }
            else
            {
                this._nomorTelepon = telp.Trim();
            }
        }

        public string GetEmail()
        {
            return this._email;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOrderException("Email tidak boleh kosong!", "email", "USER_EMAIL_KOSONG");
            }
            else if (email.Trim().Length < 6)
            {
                throw new InvalidOrderException("Format email tidak valid! Terlalu pendek.", "email", "USER_EMAIL_INVALID");
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                throw new InvalidOrderException("Format email tidak valid! (Harus mengandung @ dan .)", "email", "USER_EMAIL_INVALID");
            }
            else if (email.IndexOf("@") < 1)
            {
                throw new InvalidOrderException("Format email tidak valid! Bagian sebelum @ tidak boleh kosong.", "email", "USER_EMAIL_INVALID");
            }
            else if (email.LastIndexOf(".") < email.IndexOf("@") + 2)
            {
                throw new InvalidOrderException("Format email tidak valid! Domain tidak lengkap.", "email", "USER_EMAIL_INVALID");
            }
            else
            {
                this._email = email.Trim().ToLower();
            }
        }

        public string GetPeran()
        {
            return this._peran;
        }

        public void SetPeran(string peran)
        {
            if (string.IsNullOrWhiteSpace(peran))
            {
                throw new InvalidOrderException("Peran pengguna tidak boleh kosong!", "peran", "USER_PERAN_KOSONG");
            }
            else if (peran != "Admin" && peran != "Penjual" && peran != "User")
            {
                throw new InvalidOrderException("Peran tidak valid! Harus Admin, Penjual, atau User.", "peran", "USER_PERAN_INVALID");
            }
            else
            {
                this._peran = peran.Trim();
            }
        }

        public bool IsDiblokir()
        {
            return this._isDiblokir;
        }

        public string GetAlasanBlokir()
        {
            return this._alasanBlokir;
        }

        // === METHOD ABSTRAK ===
        public abstract string GetTipeUser();

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR 
        // =========================================================

        public void Blokir(string alasan)
        {
            if (string.IsNullOrWhiteSpace(alasan))
            {
                throw new InvalidOrderException("Alasan pemblokiran wajib diisi!", "alasan_blokir", "BLOKIR_INVALID");
            }
            else
            {
                this._isDiblokir = true;
                this._alasanBlokir = alasan.Trim();
            }
        }

        public void BukaBlokir()
        {
            this._isDiblokir = false;
            this._alasanBlokir = "";
        }

        public string DapatkanStatusAkun()
        {
            string statusUi;
            if (this._isDiblokir)
            {
                statusUi = "🚫 Terblokir: " + this._alasanBlokir;
            }
            else
            {
                statusUi = "✅ Aktif & Aman";
            }
            return statusUi;
        }

        public string DapatkanInfoKontak()
        {
            string infoTelp;
            string infoEmail;

            if (string.IsNullOrWhiteSpace(this._nomorTelepon))
            {
                infoTelp = "No HP Belum Diisi";
            }
            else
            {
                infoTelp = this._nomorTelepon;
            }

            if (string.IsNullOrWhiteSpace(this._email))
            {
                infoEmail = "Email Belum Diisi";
            }
            else
            {
                infoEmail = this._email;
            }

            return $"{this._nama} | 📞 {infoTelp} | ✉️ {infoEmail}";
        }

        public bool ApakahAkunAman()
        {
            bool statusAman;
            if (this._isDiblokir)
            {
                statusAman = false;
            }
            else
            {
                statusAman = true;
            }
            return statusAman;
        }

        // === METHOD TAMBAHAN MAKSIMAL (REAL-WORLD SCENARIO) ===

        public bool UbahPassword(string passLama, string passBaru)
        {
            bool isBerhasil;
            if (this._password != passLama)
            {
                throw new InvalidOrderException("Gagal: Password lama tidak cocok!", "password", "UBAH_PASS_GAGAL");
            }
            else
            {
                this.SetPassword(passBaru);
                isBerhasil = true;
            }
            return isBerhasil;
        }

        public string DapatkanInisialProfil()
        {
            string inisial;
            if (string.IsNullOrWhiteSpace(this._nama))
            {
                inisial = "U";
            }
            else
            {
                inisial = this._nama.Substring(0, 1).ToUpper();
            }
            return inisial;
        }

        public string DapatkanLinkWhatsApp()
        {
            string linkWa;
            if (string.IsNullOrWhiteSpace(this._nomorTelepon))
            {
                linkWa = "";
            }
            else if (this._nomorTelepon.StartsWith("0"))
            {
                linkWa = "https://wa.me/62" + this._nomorTelepon.Substring(1);
            }
            else
            {
                linkWa = "https://wa.me/" + this._nomorTelepon;
            }
            return linkWa;
        }

        // === IMPLEMENTASI IValidatable (TANPA ELSE KOSONG) ===
        public virtual void Validate()
        {
            bool lolosTahapSatu;
            bool lolosTahapDua;

            if (string.IsNullOrWhiteSpace(this._nama) || string.IsNullOrWhiteSpace(this._username))
            {
                throw new InvalidOrderException("Validasi gagal: Nama/Username tidak boleh kosong.", "nama_username", "USER_INVALID");
            }
            else
            {
                lolosTahapSatu = true; // Assignment nyata
            }

            if (string.IsNullOrWhiteSpace(this._email) || string.IsNullOrWhiteSpace(this._nomorTelepon))
            {
                throw new InvalidOrderException("Validasi gagal: Kontak Email/Telepon belum lengkap.", "kontak", "USER_INVALID");
            }
            else
            {
                lolosTahapDua = lolosTahapSatu; // Assignment nyata berantai
            }
        }
    }
}
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
                {
                    throw new InvalidOrderException("ID User tidak valid!", "id_user", "USER_ID_INVALID");
                }
                else
                {
                    this._idUser = value;
                }
            }
        }

        public string Nama
        {
            get { return this._nama; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Nama pengguna tidak boleh kosong!", "nama", "USER_NAMA_KOSONG");
                }
                else
                {
                    if (value.Trim().Length < 3)
                    {
                        throw new InvalidOrderException("Nama pengguna minimal 3 karakter!", "nama", "USER_NAMA_TERLALU_PENDEK");
                    }
                    else
                    {
                        if (value.Trim().Length > 100)
                        {
                            throw new InvalidOrderException("Nama pengguna maksimal 100 karakter!", "nama", "USER_NAMA_TERLALU_PANJANG");
                        }
                        else
                        {
                            this._nama = value.Trim();
                        }
                    }
                }
            }
        }

        public string Username
        {
            get { return this._username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new InvalidOrderException("Username minimal 4 karakter!", "username", "USER_UNAME_INVALID");
                }
                else
                {
                    this._username = value.Trim().ToLower();
                }
            }
        }

        public string Password
        {
            get { return this._password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Password tidak boleh kosong!", "password", "USER_PASS_KOSONG");
                }
                else
                {
                    if (value.Length < 8)
                    {
                        throw new InvalidOrderException("Password minimal 8 karakter!", "password", "USER_PASS_TERLALU_PENDEK");
                    }
                    else
                    {
                        this._password = value;
                    }
                }
            }
        }

        public string NomorTelepon
        {
            get { return this._nomorTelepon; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._nomorTelepon = "";
                }
                else
                {
                    if (value.Length < 9 || value.Length > 15)
                    {
                        throw new InvalidOrderException("Format Nomor WhatsApp tidak valid (harus 9-15 karakter)!", "nomorTelepon", "USER_TELP_INVALID");
                    }
                    else
                    {
                        this._nomorTelepon = value.Trim();
                    }
                }
            }
        }

        public string Email
        {
            get { return this._email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._email = "";
                }
                else
                {
                    if (value.Trim().Length < 6)
                    {
                        throw new InvalidOrderException("Format email tidak valid! Terlalu pendek.", "email", "USER_EMAIL_INVALID");
                    }
                    else
                    {
                        if (!value.Contains("@") || !value.Contains("."))
                        {
                            throw new InvalidOrderException("Format email tidak valid! (Harus mengandung @ dan .)", "email", "USER_EMAIL_INVALID");
                        }
                        else
                        {
                            if (value.IndexOf("@") < 1)
                            {
                                throw new InvalidOrderException("Format email tidak valid! Bagian sebelum @ tidak boleh kosong.", "email", "USER_EMAIL_INVALID");
                            }
                            else
                            {
                                if (value.LastIndexOf(".") < value.IndexOf("@") + 2)
                                {
                                    throw new InvalidOrderException("Format email tidak valid! Domain tidak lengkap.", "email", "USER_EMAIL_INVALID");
                                }
                                else
                                {
                                    this._email = value.Trim().ToLower();
                                }
                            }
                        }
                    }
                }
            }
        }

        public string Peran
        {
            get { return this._peran; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Peran pengguna tidak boleh kosong!", "peran", "USER_PERAN_KOSONG");
                }
                else
                {
                    if (value != "Admin" && value != "Penjual" && value != "Pembeli")
                    {
                        throw new InvalidOrderException("Peran tidak valid! Harus Admin, Penjual, atau Pembeli.", "peran", "USER_PERAN_INVALID");
                    }
                    else
                    {
                        this._peran = value.Trim();
                    }
                }
            }
        }

        public bool IsDiblokir
        {
            get
            {
                return this._isDiblokir;
            }
            set
            {
                this._isDiblokir = value;

                // =======================================================
                // SANITY CHECK: Cross-Validation
                // Jika data di-set menjadi tidak diblokir (false), 
                // pastikan alasan blokirnya langsung dibersihkan secara otomatis!
                // =======================================================
                if (value == false)
                {
                    this._alasanBlokir = "";
                }
                else
                {
                    bool statusTerkunci = true;
                }
            }
        }

        public string AlasanBlokir
        {
            get
            {
                return this._alasanBlokir;
            }
            set
            {
                // =======================================================
                // SANITY CHECK: Null & Whitespace Handling
                // Memastikan tidak ada alasan blokir berupa spasi kosong 
                // atau null yang masuk dari database.
                // =======================================================
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

        // === KONSTRUKTOR ===
        protected User(string nama, string username, string password, string peran)
        {
            this.Nama = nama;
            this.Username = username;

            // Bypass validasi panjang password untuk placeholder internal (hashed data dari DB)
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
            string status;
            if (this._isDiblokir)
            {
                status = $"🚫 Terblokir: {this._alasanBlokir}";
            }
            else
            {
                status = "✅ Aktif & Aman";
            }
            return status;
        }

        public string DapatkanInfoKontak()
        {
            string infoTelp;
            if (string.IsNullOrWhiteSpace(this._nomorTelepon))
            {
                infoTelp = "No HP Belum Diisi";
            }
            else
            {
                infoTelp = this._nomorTelepon;
            }

            string infoEmail;
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

        public bool UbahPassword(string passLama, string passBaru)
        {
            bool sukses;
            if (this._password != passLama)
            {
                throw new InvalidOrderException("Gagal: Password lama tidak cocok!", "password", "UBAH_PASS_GAGAL");
            }
            else
            {
                this.Password = passBaru;
                sukses = true;
            }
            return sukses;
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
            string link;
            if (string.IsNullOrWhiteSpace(this._nomorTelepon))
            {
                link = "";
            }
            else
            {
                if (this._nomorTelepon.StartsWith("0"))
                {
                    link = "https://wa.me/62" + this._nomorTelepon.Substring(1);
                }
                else
                {
                    link = "https://wa.me/" + this._nomorTelepon;
                }
            }
            return link;
        }

        // === IMPLEMENTASI IValidatable ===
        public virtual void Validate()
        {
            if (string.IsNullOrWhiteSpace(this._nama) || string.IsNullOrWhiteSpace(this._username))
            {
                throw new InvalidOrderException("Validasi gagal: Nama/Username tidak boleh kosong.", "nama_username", "USER_INVALID");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this._email) || string.IsNullOrWhiteSpace(this._nomorTelepon))
                {
                    throw new InvalidOrderException("Validasi gagal: Kontak Email/Telepon belum lengkap.", "kontak", "USER_INVALID");
                }
                else
                {
                    bool validasiLolos = true;
                }
            }
        }
    }
}
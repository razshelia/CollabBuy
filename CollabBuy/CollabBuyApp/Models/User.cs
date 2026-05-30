using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Abstract class induk untuk semua jenis pengguna.
    /// Mengimplementasikan IValidatable untuk data dasar.
    /// Memindahkan logika IBlockable langsung ke sini karena kemampuan 
    /// diblokir melekat pada level user (bisa Penjual maupun Pembeli).
    /// 
    /// Pemetaan Database:
    /// - Tabel: users
    /// - Kolom: is_diblokir, peran
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
            SetNama(nama);
            SetUsername(username);
            SetPassword(password);
            SetPeran(peran);
            _isDiblokir = false;
            _alasanBlokir = "";
        }

        // === GETTER & SETTER DENGAN VALIDASI ===
        public int GetIdUser() { return _idUser; }
        public void SetIdUser(int id) { _idUser = id; }

        public string GetNama() { return _nama; }
        public void SetNama(string nama)
        {
            if (string.IsNullOrEmpty(nama))
            {
                throw new InvalidOrderException("Nama pengguna tidak boleh kosong!", "nama", "USER_NAMA_KOSONG");
            }
            _nama = nama;
        }

        public string GetUsername() { return _username; }
        public void SetUsername(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length < 4)
            {
                throw new InvalidOrderException("Username minimal 4 karakter!", "username", "USER_UNAME_INVALID");
            }
            _username = username;
        }

        public string GetPassword() { return _password; }
        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOrderException("Password tidak boleh kosong!", "password", "USER_PASS_KOSONG");
            }
            _password = password;
        }

        public string GetNomorTelepon() { return _nomorTelepon; }
        public void SetNomorTelepon(string telp)
        {
            if (string.IsNullOrWhiteSpace(telp))
            {
                throw new InvalidOrderException("Nomor WhatsApp tidak boleh kosong!", "nomorTelepon", "USER_TELP_KOSONG");
            }

            // Validasi panjang standar nomor telepon (9 - 15 digit)
            if (telp.Length < 9 || telp.Length > 15)
            {
                throw new InvalidOrderException("Format Nomor WhatsApp tidak valid (harus 9-15 karakter)!", "nomorTelepon", "USER_TELP_INVALID");
            }

            _nomorTelepon = telp;
        }

        public string GetEmail() { return _email; }
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOrderException("Email tidak boleh kosong!", "email", "USER_EMAIL_KOSONG");
            }

            // Validasi email sederhana harus ada '@' dan '.'
            if (!email.Contains("@") || !email.Contains("."))
            {
                throw new InvalidOrderException("Format email tidak valid! (Harus mengandung @ dan .)", "email", "USER_EMAIL_INVALID");
            }

            _email = email;
        }
        public string GetPeran() { return _peran; }
        public void SetPeran(string peran)
        {
            if (string.IsNullOrWhiteSpace(peran))
            {
                throw new InvalidOrderException("Peran (Role) pengguna tidak boleh kosong!", "peran", "USER_PERAN_KOSONG");
            }

            // Validasi ketat sesuai dengan yang ada di Database
            if (peran != "Admin" && peran != "Penjual" && peran != "User")
            {
                throw new InvalidOrderException("Peran tidak valid! Harus Admin, Penjual, atau User.", "peran", "USER_PERAN_INVALID");
            }

            _peran = peran;
        }


        // === METHOD ABSTRAK ===
        /// <summary>
        /// Setiap turunan User wajib mengimplementasi cara menampilkan peran spesifiknya.
        /// Ini membuktikan Polimorfisme.
        /// </summary>
        public abstract string GetTipeUser();


        // === METHOD KONKRET (LOGIKANYA DIMILIKI OLEH INDUK) ===

        /// <summary>
        /// Memblokir user karena pelanggaran.
        /// Pemetaan DB: Diakses oleh Procedure sp_tindak_penjual_nakal
        /// </summary>
        public void Blokir(string alasan)
        {
            if (string.IsNullOrEmpty(alasan))
            {
                throw new InvalidOrderException("Alasan pemblokiran wajib diisi!", "alasan_blokir", "BLOKIR_INVALID");
            }
            _isDiblokir = true;
            _alasanBlokir = alasan;
        }

        public void BukaBlokir()
        {
            _isDiblokir = false;
            _alasanBlokir = "";
        }

        public bool IsDiblokir()
        {
            return _isDiblokir;
        }

        public string GetAlasanBlokir()
        {
            return _alasanBlokir;
        }


        // === IMPLEMENTASI IValidatable ===
        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(_nama))
            {
                throw new InvalidOrderException("Validasi gagal: Nama user kosong.", "nama", "USER_INVALID");
            }
            if (string.IsNullOrEmpty(_username))
            {
                throw new InvalidOrderException("Validasi gagal: Username kosong.", "username", "USER_INVALID");
            }
            if (string.IsNullOrEmpty(_email))
            {
                throw new InvalidOrderException("Validasi gagal: Email kosong.", "email", "USER_INVALID");
            }
            if (string.IsNullOrEmpty(_nomorTelepon))
            {
                throw new InvalidOrderException("Validasi gagal: Nomor telepon kosong.", "nomor_telepon", "USER_INVALID");
            }
        }
    }
}
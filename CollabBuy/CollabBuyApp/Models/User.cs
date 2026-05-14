using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public abstract class User
    {
        private int _idUser;
        private string _nama;
        private string _nomorTelepon;
        private string _email;
        private string _username;
        private string _password; // hash
        private string _peran;
        private bool _isDiblokir;
        private Verification _verification; // relasi Composition (nullable)

        public User()
        {
            _isDiblokir = false;
            _verification = null;
        }

        // ── Common properties ──
        public int IdUser
        {
            get => _idUser;
            set
            {
                if (value <= 0) throw new ArgumentException("ID User tidak valid.");
                _idUser = value;
            }
        }

        public string Nama
        {
            get => _nama;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama wajib diisi.");
                _nama = value.Trim();
            }
        }

        public string NomorTelepon
        {
            get => _nomorTelepon;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (!long.TryParse(value, out _))
                        throw new ArgumentException("Nomor telepon hanya boleh berisi angka.");
                    if (value.Length < 10 || value.Length > 15)
                        throw new ArgumentException("Nomor telepon harus 10-15 digit.");
                }
                _nomorTelepon = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email wajib diisi.");
                if (!value.Contains("@"))
                    throw new ArgumentException("Format email tidak valid.");
                _email = value.Trim();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException("Username minimal 5 karakter.");
                _username = value.Trim();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password wajib diisi.");
                _password = value;
            }
        }

        public string Peran
        {
            get => _peran;
            protected set  // hanya bisa diset oleh subclass
            {
                if (value != "Admin" && value != "User")
                    throw new ArgumentException("Peran hanya boleh Admin atau User.");
                _peran = value;
            }
        }

        public bool IsDiblokir
        {
            get => _isDiblokir;
            set => _isDiblokir = value;
        }

        // Composition: User memiliki Verification (jika sudah diverifikasi sebagai penjual)
        public Verification Verification
        {
            get => _verification;
            set => _verification = value; // null jika belum diverifikasi
        }

        // Method abstrak untuk menampilkan dashboard (polimorfisme)
        public abstract string TampilkanDashboard();
    }
}
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
        private string _password;
        private string _peran;
        private bool _isDiblokir;
        private Verification _verification;

        public User()
        {
            _isDiblokir = false;
            _verification = null;
        }

        public int IdUser
        {
            get => _idUser;
            set { if (value <= 0) throw new ArgumentException("ID User harus positif."); _idUser = value; }
        }

        public string Nama
        {
            get => _nama;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nama wajib diisi."); _nama = value.Trim(); }
        }

        public string NomorTelepon
        {
            get => _nomorTelepon;
            set
            {
                // 1. Cek apakah kosong
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nomor telepon wajib diisi untuk keperluan koordinasi Pre-Order.");
                }

                // 2. Logika Pembersihan (Sanitization)
                // Membuang spasi atau tanda strip (-) jika user iseng mengetik "0812-3456-789"
                string cleaned = value.Replace(" ", "").Replace("-", "").Replace("+", "");

                // 3. Validasi Karakter
                if (!long.TryParse(cleaned, out _))
                {
                    throw new ArgumentException("Nomor telepon hanya boleh berisi angka.");
                }

                // 4. Validasi Panjang (Standar Indonesia 10-15 digit)
                if (cleaned.Length < 10 || cleaned.Length > 15)
                {
                    throw new ArgumentException("Panjang nomor telepon harus antara 10 sampai 15 digit.");
                }

                _nomorTelepon = cleaned;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("Format email tidak valid.");
                _email = value.Trim().ToLower();
            }
        }

        public string Username
        {
            get => _username;
            set { if (string.IsNullOrWhiteSpace(value) || value.Length < 5) throw new ArgumentException("Username minimal 5 karakter."); _username = value.Trim(); }
        }

        public string Password
        {
            get => _password;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Password tidak boleh kosong."); _password = value; }
        }

        public string Peran
        {
            get => _peran;
            protected set
            {
                if (value != "Admin" && value != "User") throw new ArgumentException("Peran tidak valid.");
                _peran = value;
            }
        }

        public bool IsDiblokir
        {
            get => _isDiblokir;
            set { if (_isDiblokir != value) _isDiblokir = value; }
        }

        public Verification Verification
        {
            get => _verification;
            set { if (_verification != value) _verification = value; }
        }

        public abstract string TampilkanDashboard();
    }
}
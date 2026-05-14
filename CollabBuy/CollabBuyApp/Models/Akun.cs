using System;
using System.Linq;

namespace CollabBuy.CollabBuyApp.Models
{
    public abstract class Akun
    {
        private string username;
        private string password;
        private string email;
        private string nomorTelepon;
        private int idUser;

        public string Username
        {
            get { return this.username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username tidak boleh kosong.");
                }
                else
                {
                    this.username = value;
                }
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password rahasia nggak boleh kosong!");
                }
                else
                {
                    this.password = value;
                }
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email nggak boleh kosong, bestie!");
                }
                else
                {
                    this.email = value;
                }
            }
        }

        public string NomorTelepon
        {
            get { return this.nomorTelepon; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nomor telepon wajib diisi.");
                }
                else
                {
                    if (value.Length < 10)
                    {
                        throw new ArgumentException("Nomor telepon minimal 10 digit.");
                    }
                    else
                    {
                        if (!value.All(char.IsDigit))
                        {
                            throw new ArgumentException("Nomor telepon hanya boleh berisi angka.");
                        }
                        else
                        {
                            this.nomorTelepon = value;
                        }
                    }
                }
            }
        }

        public int IdUser
        {
            get { return this.idUser; }
            set
            {
                if (value <= 0)
                {
                    // Fallback
                    this.idUser = 0;
                }
                else
                {
                    this.idUser = value;
                }
            }
        }

        // POLYMORPHISM: Method abstrak yang wajib di-override oleh kelas anak
        public abstract string TampilkanDashboard();
    }
}
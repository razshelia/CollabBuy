using System;
using System.Security.Cryptography;
using System.Text;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public class PasswordHelper
    {
        // ENKAPSULASI: Variabel disembunyikan (private) dan tidak menggunakan get; set; kosong.
        private string saltRahasia;

        public PasswordHelper()
        {
            // Menambahkan salt agar hash SHA256 jauh lebih aman dari serangan Rainbow Table
            this.saltRahasia = "DanusFasilkomUnej2026";
        }

        public string HashPassword(string passwordAwal)
        {
            if (string.IsNullOrEmpty(passwordAwal))
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        // Menggabungkan password input dengan salt rahasia
                        string passwordDenganSalt = passwordAwal + this.saltRahasia;
                        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordDenganSalt));

                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            builder.Append(bytes[i].ToString("x2"));
                        }

                        return builder.ToString();
                    }
                }
                catch (Exception)
                {
                    // ERROR HANDLING: UX Friendly. Tidak crash, hanya mengembalikan string kosong.
                    // Nantinya layer UI/Service akan mengecek jika string kosong berarti ada masalah sistem.
                    return string.Empty;
                }
            }
        }

        public bool VerifikasiPassword(string passwordInput, string hashTersimpan)
        {
            if (string.IsNullOrEmpty(passwordInput) || string.IsNullOrEmpty(hashTersimpan))
            {
                return false;
            }
            else
            {
                string hashInput = this.HashPassword(passwordInput);

                if (hashInput == hashTersimpan)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    // INHERITANCE: Seller adalah turunan dari User
    public class Seller : User
    {
        private string nim;
        private string linkFotoKtm;
        private int tahunMasuk;
        private string namaToko;

        public Seller()
        {
            // Konstruktor kosong. 
            // Kita sudah HAPUS this.Peran = "User" karena di OOP murni, 
            // perannya ditentukan dari tipe kelas (Inheritance), bukan dari teks string.
        }

        public string Nim
        {
            get { return this.nim; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 10)
                {
                    throw new ArgumentException("NIM harus valid dan tidak boleh kosong.");
                }
                else
                {
                    this.nim = value;
                }
            }
        }

        public string LinkFotoKtm
        {
            get { return this.linkFotoKtm; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Link foto KTM tidak boleh kosong.");
                }
                else
                {
                    this.linkFotoKtm = value;
                }
            }
        }

        public int TahunMasuk
        {
            get { return this.tahunMasuk; }
            set
            {
                int tahunSekarang = DateTime.Now.Year;
                if (value < 2000 || value > tahunSekarang)
                {
                    throw new ArgumentException("Tahun masuk tidak logis.");
                }
                else
                {
                    this.tahunMasuk = value;
                }
            }
        }

        public string NamaToko
        {
            get { return this.namaToko; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nama toko tidak boleh kosong.");
                }
                else
                {
                    this.namaToko = value;
                }
            }
        }

        // LOGIKA BISNIS: Cek masa studi mahasiswa
        public bool IsMasihMahasiswa()
        {
            int tahunSekarang = DateTime.Now.Year;

            if ((tahunSekarang - this.tahunMasuk) <= 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // POLYMORPHISM: Override tampilan dashboard
        public override string TampilkanDashboard()
        {
            // PENGGUNAAN this.NamaLengkap (Bukan this.Nama)
            // Karena properti di kelas induk (User.cs) namanya NamaLengkap
            if (!this.IsMasihMahasiswa())
            {
                return $"Maaf {this.NamaLengkap}, akses penjual dicabut karena masa studi Anda telah berakhir.";
            }
            else
            {
                return $"Dashboard Toko: {this.namaToko}. Semangat jualan {this.NamaLengkap}!";
            }
        }
    }
}
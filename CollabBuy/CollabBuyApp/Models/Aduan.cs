using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Aduan
    {
        private string subjek;
        private string pesan;
        private string statusAduan;

        public Aduan()
        {
            this.statusAduan = "Terbuka";
        }

        public string Subjek
        {
            get { return this.subjek; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Subjek aduan harus diisi.");
                }
                else
                {
                    this.subjek = value;
                }
            }
        }

        public string Pesan
        {
            get { return this.pesan; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Isi pesan aduan tidak boleh kosong.");
                }
                else
                {
                    this.pesan = value;
                }
            }
        }

        public string StatusAduan
        {
            get { return this.statusAduan; }
            set
            {
                if (value == "Terbuka" || value == "Diproses" || value == "Selesai")
                {
                    this.statusAduan = value;
                }
                else
                {
                    throw new ArgumentException("Status aduan tidak valid.");
                }
            }
        }
    }
}
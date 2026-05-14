using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Kategori
    {
        private int idKategori;
        private string namaKategori;
        private string deskripsi;

        // ✅ FIX: Tambahkan IdKategori yang dibutuhkan oleh ComboBox ValueMember
        public int IdKategori
        {
            get { return this.idKategori; }
            set
            {
                if (value <= 0)
                {
                    this.idKategori = 0;
                }
                else
                {
                    this.idKategori = value;
                }
            }
        }

        public string NamaKategori
        {
            get { return this.namaKategori; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nama kategori wajib diisi.");
                }
                else
                {
                    this.namaKategori = value;
                }
            }
        }

        public string Deskripsi
        {
            get { return this.deskripsi; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.deskripsi = "Tidak ada deskripsi.";
                }
                else
                {
                    this.deskripsi = value;
                }
            }
        }
    }
}
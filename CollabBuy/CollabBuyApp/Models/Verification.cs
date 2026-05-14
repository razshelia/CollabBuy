using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Verification
    {
        private int _idVerifikasi;
        private int _idUser;
        private string _nim;
        private string _namaToko;
        private string _buktiKtm;    // path relatif (Uploads/KTM/...)
        private int _tahunMasuk;
        private bool _isVerifikasi;

        public int IdVerifikasi
        {
            get => _idVerifikasi;
            set { if (value <= 0) throw new ArgumentException("ID Verifikasi tidak valid."); _idVerifikasi = value; }
        }

        public int IdUser
        {
            get => _idUser;
            set { if (value <= 0) throw new ArgumentException("ID User tidak valid."); _idUser = value; }
        }

        public string Nim
        {
            get => _nim;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("NIM wajib diisi.");
                if (!long.TryParse(value, out _)) throw new ArgumentException("NIM hanya boleh berisi angka.");
                _nim = value.Trim();
            }
        }

        public string NamaToko
        {
            get => _namaToko;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nama toko wajib diisi.");
                _namaToko = value.Trim();
            }
        }

        public string BuktiKtm
        {
            get => _buktiKtm;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Bukti KTM wajib diunggah.");
                _buktiKtm = value;
            }
        }

        public int TahunMasuk
        {
            get => _tahunMasuk;
            set
            {
                int now = DateTime.Now.Year;
                if (value < now - 7 || value > now)
                    throw new ArgumentException($"Tahun masuk harus antara {now - 7} dan {now}.");
                _tahunMasuk = value;
            }
        }

        public bool IsVerifikasi
        {
            get => _isVerifikasi;
            set => _isVerifikasi = value;
        }
    }
}
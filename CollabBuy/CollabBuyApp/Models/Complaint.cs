using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Complaint
    {
        private int _idAduan;
        private int _idUser;
        private string _subjek;
        private string _deskripsi;
        private DateTime _tanggal;
        private bool _isSelesai;
        private string _balasan;

        public Complaint()
        {
            _tanggal = DateTime.Now;
            _isSelesai = false;
        }

        public int IdAduan
        {
            get => _idAduan;
            set { if (value <= 0) throw new ArgumentException("ID Aduan tidak valid."); _idAduan = value; }
        }

        public int IdUser
        {
            get => _idUser;
            set { if (value <= 0) throw new ArgumentException("ID User tidak valid."); _idUser = value; }
        }

        public string Subjek
        {
            get => _subjek;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Subjek wajib diisi."); _subjek = value.Trim(); }
        }

        public string Deskripsi
        {
            get => _deskripsi;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Deskripsi wajib diisi."); _deskripsi = value.Trim(); }
        }

        public DateTime Tanggal
        {
            get => _tanggal;
            set { if (value.Year < 2026) throw new ArgumentException("Tanggal tidak valid."); _tanggal = value; }
        }

        public bool IsSelesai
        {
            get => _isSelesai;
            set { if (_isSelesai != value) _isSelesai = value; }
        }

        public string Balasan
        {
            get => _balasan;
            set => _balasan = string.IsNullOrWhiteSpace(value) ? "Belum ada balasan dari admin." : value.Trim();
        }
    }
}
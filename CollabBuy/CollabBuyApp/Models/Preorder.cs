using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public abstract class Preorder
    {
        private int _idPo;
        private int _idPenjual;
        private string _judulPo;
        private string _infoRekening;
        private DateTime _batasWaktu;
        private bool _isAktif;

        public Preorder()
        {
            _isAktif = true;
        }

        public int IdPo
        {
            get => _idPo;
            set { if (value <= 0) throw new ArgumentException("ID PO tidak valid."); _idPo = value; }
        }
        public int IdPenjual
        {
            get => _idPenjual;
            set { if (value <= 0) throw new ArgumentException("ID Penjual tidak valid."); _idPenjual = value; }
        }
        public string JudulPo
        {
            get => _judulPo;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Judul PO wajib diisi."); _judulPo = value.Trim(); }
        }
        public abstract string JenisPo { get; }

        public string InfoRekening
        {
            get => _infoRekening;
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Info rekening wajib diisi."); _infoRekening = value.Trim(); }
        }
        public DateTime BatasWaktu
        {
            get => _batasWaktu;
            set { if (value <= DateTime.Now) throw new ArgumentException("Batas waktu PO tidak boleh di masa lalu."); _batasWaktu = value; }
        }
        public bool IsAktif
        {
            get => _isAktif;
            set => _isAktif = value;
        }

        public abstract decimal HitungHarga(int jumlah, decimal hargaDasar);
    }
}
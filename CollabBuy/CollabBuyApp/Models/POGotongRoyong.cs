using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class PreorderGotongRoyong : Preorder
    {
        private int _targetKuota;

        public override string JenisPo => "Gotong Royong";

        public int TargetKuota
        {
            get => _targetKuota;
            set { if (value <= 0) throw new ArgumentException("Target kuota harus > 0."); _targetKuota = value; }
        }

        public override decimal HitungHarga(int jumlah, decimal hargaDasar)
        {
            return jumlah * hargaDasar;
        }

        // Overloading: harga setelah diskon jika kuota tercapai
        public decimal HitungHarga(int jumlah, decimal hargaDasar, int totalPesanan)
        {
            if (totalPesanan >= _targetKuota)
                return jumlah * (hargaDasar * 0.9m); // 10% diskon
            return jumlah * hargaDasar;
        }
    }
}
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class POGotongRoyong : Preorder
    {
        private int _targetKuota;

        public override string JenisPo => "Gotong Royong";

        // Properti khusus anak kelas
        public int TargetKuota
        {
            get => _targetKuota;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Target kuota harus lebih dari 0.");
                _targetKuota = value;
            }
        }

        // Implementasi Polimorfisme: (Nanti logika diskon ditarik dari Function DB, 
        // tapi method ini tetap wajib diisi karena ini turunan abstract)
        public override int HitungHarga(int jumlah, int hargaDasar)
        {
            return jumlah * hargaDasar;
        }
    }
}
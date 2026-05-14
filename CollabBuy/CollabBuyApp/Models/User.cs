using CollabBuy.CollabBuyApp.Interfaces;

namespace CollabBuy.CollabBuyApp.Models
{
    public class User : Akun, IPembeli
    {
        private string namaLengkap;

        public string NamaLengkap
        {
            get { return this.namaLengkap; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new System.ArgumentException("Nama tidak boleh kosong.");
                else
                    this.namaLengkap = value;
            }
        }

        // Property untuk menandai apakah akun sudah diverifikasi sebagai penjual
        public bool IsVerifikasi { get; set; } = false;

        public override string TampilkanDashboard()
        {
            return $"Halo {this.namaLengkap}, mari cari barang Gotong Royong hari ini!";
        }

        public bool LakukanCheckout(int idPo, int jumlah)
        {
            if (jumlah <= 0) return false;
            else return true;
        }

        public string IkutPO(PreOrder barangPO)
        {
            if (barangPO == null) return "Barang tidak ditemukan.";
            else return $"Berhasil mendaftar untuk PO: {barangPO.NamaBarang}";
        }
    }
}

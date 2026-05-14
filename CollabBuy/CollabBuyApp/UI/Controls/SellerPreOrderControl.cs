using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerPreOrderControl : UserControl
    {
        private ProductService productService;
        private int sellerId;

        public SellerPreOrderControl(int p_sellerId)
        {
            this.InitializeComponent();
            this.productService = new ProductService();
            this.sellerId = p_sellerId;

            // Validasi tanggal minimal hari ini + 1
            this.dtpBatasWaktu.MinDate = DateTime.Now.AddDays(1);
        }

        private void btnBukaPo_Click(object sender, EventArgs e)
        {
            if (this.cmbProduk.SelectedItem == null)
            {
                UXHelper.TampilkanError("Pilih dulu dong produknya yang mau di-PO-in!");
                return; // Ganti else kosong
            }

            if (this.cmbJenisPo.Text == "GotongRoyong" && this.numTarget.Value < 2)
            {
                UXHelper.TampilkanError("Namanya juga Gotong Royong, target kuotanya minimal 2 orang dong bestie! 🤝");
                return; // Ganti else kosong
            }

            if (UXHelper.TampilkanKonfirmasi("Yakin settingan PO-nya udah pas?"))
            {
                // Panggil Service untuk INSERT ke tabel preorders
                // bool sukses = this.productService.BuatPOBaru(...);

                UXHelper.TampilkanSukses("Mantap! PO kamu udah resmi dibuka! Siap-siap kebanjiran orderan ya 💸");
            }
        }
    }
}
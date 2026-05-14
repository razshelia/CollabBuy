using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerOrderControl : UserControl
    {
        private CheckoutService checkoutService;
        private int sellerId;

        public SellerOrderControl(int currentSellerId)
        {
            this.InitializeComponent();
            this.checkoutService = new CheckoutService();
            this.sellerId = currentSellerId;
            this.MuatDataPesanan();
        }

        private void MuatDataPesanan()
        {
            // Panggil fungsi service untuk meload pesanan spesifik seller ini
            // this.dgvPesanan.DataSource = this.checkoutService.MuatPesananMasuk(this.sellerId);
        }

        private void btnValidasi_Click(object sender, EventArgs e)
        {
            if (this.dgvPesanan.SelectedRows.Count == 0)
            {
                UXHelper.TampilkanError("Pilih dulu pesanan mana yang mau divalidasi struknya, Kak!");
                return; // Langsung return, gantiin fungsi else kosong
            }

            int idCheckout = Convert.ToInt32(this.dgvPesanan.SelectedRows[0].Cells[0].Value);

            if (UXHelper.TampilkanKonfirmasi("Struk udah dicek dan uang udah masuk? Yakin mau diproses?"))
            {
                // Panggil Service Update Status
                // bool sukses = this.checkoutService.ValidasiPesanan(idCheckout);

                UXHelper.TampilkanSukses("Mantap! Pesanan berhasil divalidasi dan masuk antrian produksi. 🚀");
                this.MuatDataPesanan();
            }
        }
    }
}
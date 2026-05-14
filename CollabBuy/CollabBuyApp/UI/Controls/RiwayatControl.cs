using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class RiwayatControl : UserControl
    {
        private CheckoutService checkoutService;
        private int idUser;

        public RiwayatControl(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.checkoutService = new CheckoutService();
            this.MuatRiwayat();
        }

        private void MuatRiwayat()
        {
            var data = this.checkoutService.AmbilRiwayatUser(this.idUser);
            this.dgvRiwayat.DataSource = data;
            if (data == null || data.Count == 0)
                UXHelper.TampilkanSukses("Belum ada riwayat pesanan nih, Bestie! Yuk jajan dulu. ✨");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.MuatRiwayat();
        }
    }
}
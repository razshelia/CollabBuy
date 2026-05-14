using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class CheckoutControl : UserControl
    {
        private CheckoutService checkoutService;
        private int idUserAktif;
        private int idPoAktif;
        private decimal hargaSatuan;
        private string pathFileBukti = "";

        public CheckoutControl(int idUser, int idPo, string namaProduk, decimal harga)
        {
            this.InitializeComponent();
            this.checkoutService = new CheckoutService();

            this.idUserAktif = idUser;
            this.idPoAktif = idPo;
            this.hargaSatuan = harga;

            this.lblNamaProduk.Text = "Produk: " + namaProduk;
            this.HitungTotal();
        }

        private void HitungTotal()
        {
            decimal total = this.hargaSatuan * this.numJumlah.Value;
            this.lblTotal.Text = "Total Kasbon: Rp " + total.ToString("N0");
        }

        private void numJumlah_ValueChanged(object sender, EventArgs e)
        {
            this.HitungTotal();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.pathFileBukti = ofd.FileName;
                this.lblPathBukti.Text = ofd.SafeFileName;
                UXHelper.TampilkanSukses("Sipp, file aman! ✨");
            }
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.pathFileBukti))
            {
                UXHelper.TampilkanError("Upload dulu dong struk transfernya, no pic hoax! 📸");
                return;
            }

            int jumlahBeli = (int)this.numJumlah.Value;

            // Logika service udah include konfirmasi UXHelper dan validasi
            bool sukses = this.checkoutService.LakukanPembayaran(this.idUserAktif, this.idPoAktif, jumlahBeli, this.pathFileBukti);

            if (sukses)
            {
                // Reset form setelah sukses
                this.numJumlah.Value = 1;
                this.pathFileBukti = "";
                this.lblPathBukti.Text = "Belum ada file terpilih.";
            }
        }
    }
}
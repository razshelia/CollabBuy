using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminDashboardControl : UserControl
    {
        public AdminDashboardControl()
        {
            InitializeComponent();
            LoadStats();
        }

        private void LoadStats()
        {
            try
            {
                var userService = new UserService();
                var productService = new ProductService();
                var transService = new TransactionService();
                var complaintService = new ComplaintService();

                int totalUser = userService.AmbilSemuaUser().Count;
                int totalProduk = productService.AmbilJumlahProduk();
                int totalTransaksi = transService.AmbilJumlahTransaksi();
                int totalAduan = complaintService.AmbilSemuaAduan().Count;

                // Update label di card
                lblTotalUser.Text = totalUser.ToString();
                lblTotalProduk.Text = totalProduk.ToString();
                lblTotalTransaksi.Text = totalTransaksi.ToString();
                lblTotalAduan.Text = totalAduan.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dashboard: " + ex.Message);
            }
        }
    }
}
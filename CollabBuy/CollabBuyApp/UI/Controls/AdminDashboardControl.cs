using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminDashboardControl : UserControl
    {
        public AdminDashboardControl()
        {
            InitializeComponent();
            SetupCards(); // Membangun kartu UI dengan gaya Neo-Retro
            LoadStats();
            LoadReportControl(); // Menampilkan Report Admin di Dashboard
        }

        private void SetupCards()
        {
            pnlCards.Controls.Clear();

            // Warna-warna Gen-Z Pastel
            Color unguPastel = Color.FromArgb(200, 182, 255);
            Color kuningPastel = Color.FromArgb(253, 255, 182);
            Color merahPastel = Color.FromArgb(255, 138, 138);

            pnlCards.Controls.Add(BuatCardStat("TOTAL USER 👤", out lblTotalUser, unguPastel));
            pnlCards.Controls.Add(BuatCardStat("TOTAL PRODUK 📦", out lblTotalProduk, kuningPastel));
            pnlCards.Controls.Add(BuatCardStat("TOTAL TRANSAKSI 🛒", out lblTotalTransaksi, unguPastel));
            pnlCards.Controls.Add(BuatCardStat("TOTAL ADUAN ⚠️", out lblTotalAduan, merahPastel));
        }

        private Panel BuatCardStat(string judul, out Label labelRef, Color bgColor)
        {
            Panel card = new Panel()
            {
                Size = new Size(230, 110),
                BackColor = bgColor,
                Margin = new Padding(0, 0, 15, 15),
                BorderStyle = BorderStyle.FixedSingle // Gaya retro kotak datar
            };

            Label lblJudul = new Label()
            {
                Text = judul,
                Font = new Font("Segoe UI Black", 10F),
                ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                Size = new Size(200, 25),
                Location = new Point(15, 15)
            };

            labelRef = new Label()
            {
                Text = "0",
                Font = new Font("Segoe UI Black", 28F),
                ForeColor = Color.FromArgb(36, 0, 70),
                Size = new Size(200, 50),
                Location = new Point(10, 45)
            };

            card.Controls.Add(lblJudul);
            card.Controls.Add(labelRef);
            return card;
        }

        private void LoadReportControl()
        {
            // Menanamkan Report Control ke dalam panel dashboard
            AdminReportControl reportCtrl = new AdminReportControl();
            reportCtrl.Dock = DockStyle.Fill;
            pnlReportContainer.Controls.Clear();
            pnlReportContainer.Controls.Add(reportCtrl);
        }

        private void LoadStats()
        {
            try
            {
                // TAHAP 4: INJEKSI DEPENDENSI MANUAL DI UI
                var userService = new UserService(new UserRepository());
                var productService = new ProductService(new ProductRepository());
                var transService = new TransactionService(new TransactionRepository());
                var complaintService = new ComplaintService(new ComplaintRepository());

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
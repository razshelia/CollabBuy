using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DashboardUserControl : UserControl
    {
        private readonly User _currentUser;

        // Panggil Kokinya (Controllers) ke sini
        private readonly TransactionController _transactionController;
        private readonly ProductController _productController;

        public DashboardUserControl(User user)
        {
            InitializeComponent();
            _currentUser = user;

            // Inisialisasi Controllers
            _transactionController = new TransactionController();
            _productController = new ProductController();

            if (_currentUser != null)
            {
                lblWelcome.Text = $"Halo, {_currentUser.GetNama()}! 👋";
            }
        }

        private void DashboardUserControl_Load(object sender, EventArgs e)
        {
            SetupGridColumns();
            LoadUserDataSummary();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserDataSummary();
        }

        private void SetupGridColumns()
        {
            dgvActivePO.AutoGenerateColumns = false;
            dgvActivePO.Columns.Clear();

            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Produk", HeaderText = "Nama Produk", DataPropertyName = "Produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn { Name = "PO", HeaderText = "Sesi PO", DataPropertyName = "PO", Width = 250 });
            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga Dasar", DataPropertyName = "Harga", Width = 150 });
            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn { Name = "BatasWaktu", HeaderText = "Batas Order", DataPropertyName = "BatasWaktu", Width = 180 });
        }

        private void LoadUserDataSummary()
        {
            try
            {
                if (_currentUser == null) return;

                // 1. Cek Status Akun (UI Logic)
                if (_currentUser.GetPeran() == "Penjual")
                {
                    lblValueShopStatus.Text = "🏪 Lapak Aktif!";
                    lblValueShopStatus.ForeColor = Color.ForestGreen;
                }
                else
                {
                    lblValueShopStatus.Text = "🔒 Terkunci (Buyer)";
                    lblValueShopStatus.ForeColor = Color.FromArgb(36, 0, 70);
                }

                // 2. Ambil jumlah transaksi aktif LEWAT CONTROLLER
                int totalAktif = _transactionController.GetTotalPesananAktif(_currentUser.GetIdUser());
                lblValueActiveOrders.Text = totalAktif.ToString();

                // 3. Ambil data katalog LEWAT CONTROLLER
                DataTable dtRaw = _productController.GetKatalogAktifDashboard(15);

                // 4. Bikin tabel khusus buat percantik tampilan di UI (Formatting)
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("Produk", typeof(string));
                dtUI.Columns.Add("PO", typeof(string));
                dtUI.Columns.Add("Harga", typeof(string));
                dtUI.Columns.Add("BatasWaktu", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string judulPo = row.IsNull("judul_po") ? "Reguler / Non-PO" : row["judul_po"].ToString();
                    string batasWaktu = row.IsNull("batas_waktu") ? "Selalu Buka" : Convert.ToDateTime(row["batas_waktu"]).ToString("dd MMM yyyy, HH:mm");
                    string harga = "Rp " + Convert.ToInt32(row["harga_dasar"]).ToString("N0");

                    dtUI.Rows.Add(row["nama_produk"].ToString(), judulPo, harga, batasWaktu);
                }

                dgvActivePO.DataSource = dtUI;
                dgvActivePO.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duh, gagal narik data dari server nih bestie: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
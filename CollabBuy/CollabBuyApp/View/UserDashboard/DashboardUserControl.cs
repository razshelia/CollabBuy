using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DashboardUserControl : UserControl
    {
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;
        private readonly PreOrderController _preOrderController;

        public DashboardUserControl(User user)
        {
            InitializeComponent();
            _currentUser = user;

            // PERBAIKAN: TransactionController sekarang menerima dua bentuk konstruktor.
            // Untuk View dashboard (tidak perlu keranjang), gunakan konstruktor default.
            _transactionController = new TransactionController();
            _preOrderController = new PreOrderController();

            if (_currentUser != null)
            {
                // PERBAIKAN: Model User menggunakan metode getter (GetNama()),
                // bukan property auto (.Nama). Ini konsisten dengan pola enkapsulasi OOP
                // yang diterapkan di seluruh kelas model proyek ini.
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

            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaSesi",
                HeaderText = "Nama Sesi Pre-Order / Danus",
                DataPropertyName = "NamaSesi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Penjual",
                HeaderText = "Organisasi / Penjual",
                DataPropertyName = "Penjual",
                Width = 200
            });

            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BatasWaktu",
                HeaderText = "Batas Waktu Order",
                DataPropertyName = "BatasWaktu",
                Width = 180
            });

            dgvActivePO.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status Sesi",
                DataPropertyName = "Status",
                Width = 120
            });
        }

        private void LoadUserDataSummary()
        {
            try
            {
                if (_currentUser == null) return;

                // PERBAIKAN: Gunakan GetPeran() (metode getter) bukan properti .Role
                if (_currentUser.GetPeran() == "Penjual")
                {
                    lblValueShopStatus.Text = "🏪 Lapak Aktif";
                    lblValueShopStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblValueShopStatus.Text = "🔒 Terkunci (Buyer)";
                    lblValueShopStatus.ForeColor = Color.FromArgb(36, 0, 70);
                }

                lblValueActiveOrders.Text = "3";
                lblValueJoinedPO.Text = "1";

                DataTable dtMockPO = new DataTable();
                dtMockPO.Columns.Add("NamaSesi", typeof(string));
                dtMockPO.Columns.Add("Penjual", typeof(string));
                dtMockPO.Columns.Add("BatasWaktu", typeof(string));
                dtMockPO.Columns.Add("Status", typeof(string));

                dtMockPO.Rows.Add("Danus Makaroni Pedas HMTI", "HMTI Mandiri", "Besok, 17:00", "Buka");
                dtMockPO.Rows.Add("PO Kemeja Angkatan 2024", "BEM Fasilkom", "20 Juni 2026", "Buka");
                dtMockPO.Rows.Add("Titip Stand Bazaar Kue Sus", "Ahmad Jaelani", "Hari Ini, 23:59", "Sisa Kuota Sedikit");

                dgvActivePO.DataSource = dtMockPO;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data ringkasan dashboard: " + ex.Message,
                                "CollabBuy - Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
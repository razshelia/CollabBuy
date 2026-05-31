using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class DashboardAdminControl : UserControl
    {
        private readonly User _admin;
        private readonly AdminController _controller;

        public DashboardAdminControl(User admin)
        {
            InitializeComponent();
            _admin = admin;
            _controller = new AdminController();
            this.Resize += (s, e) => AdjustLayout();
        }

        private void DashboardAdminControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            lblSapaan.Text = $"Hola Mimin {_admin.GetUsername()}! 🚀";
            LoadStatistik();
            LoadLogTerbaru();
            LoadLeaderboard();
        }

        private void LoadStatistik()
        {
            Dictionary<string, int> stats = _controller.GetStatsDashboard();
            lblTotalUser.Text = stats["users"].ToString();
            lblTotalTrx.Text = stats["transaksi"].ToString();
            lblTotalPO.Text = stats["po_aktif"].ToString();
            lblAduan.Text = stats["aduan"].ToString();
        }

        private void LoadLogTerbaru()
        {
            try
            {
                DataTable dt = _controller.GetLogAktivitasDataTable();

                // Ambil 5 terbaru saja untuk dashboard
                DataTable dtTop = dt.Clone();
                int batas = Math.Min(5, dt.Rows.Count);
                for (int i = 0; i < batas; i++)
                    dtTop.ImportRow(dt.Rows[i]);

                dgvLog.AutoGenerateColumns = false;
                dgvLog.Columns.Clear();
                dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pelaku", HeaderText = "User", DataPropertyName = "pelaku", Width = 110 });
                dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Aktivitas", HeaderText = "Aktivitas", DataPropertyName = "aktivitas", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Waktu", HeaderText = "Waktu", DataPropertyName = "waktu_format", Width = 130 });

                // Format waktu
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("pelaku", typeof(string));
                dtUI.Columns.Add("aktivitas", typeof(string));
                dtUI.Columns.Add("waktu_format", typeof(string));

                foreach (DataRow row in dtTop.Rows)
                {
                    string waktu = Convert.ToDateTime(row["waktu_akses"]).ToString("dd MMM, HH:mm");
                    dtUI.Rows.Add(row["pelaku"], row["aktivitas"], waktu);
                }

                dgvLog.DataSource = dtUI;
                dgvLog.ClearSelection();
            }
            catch { /* Jika gagal, biarkan kosong */ }
        }

        private void LoadLeaderboard()
        {
            try
            {
                DataTable dt = _controller.GetLeaderboardPenjual();

                dgvLeaderboard.AutoGenerateColumns = false;
                dgvLeaderboard.Columns.Clear();
                dgvLeaderboard.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Penjual", DataPropertyName = "nama_penjual", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvLeaderboard.Columns.Add(new DataGridViewTextBoxColumn { Name = "Omzet", HeaderText = "Total Omzet", DataPropertyName = "omzet_format", Width = 110 });
                dgvLeaderboard.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tier", HeaderText = "Tier", DataPropertyName = "tier_penjual", Width = 150 });

                // Format omzet
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("nama_penjual", typeof(string));
                dtUI.Columns.Add("omzet_format", typeof(string));
                dtUI.Columns.Add("tier_penjual", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string omzet = "Rp " + Convert.ToInt64(row["total_omzet_bersih"]).ToString("N0");
                    dtUI.Rows.Add(row["nama_penjual"], omzet, row["tier_penjual"]);
                }

                dgvLeaderboard.DataSource = dtUI;
                dgvLeaderboard.ClearSelection();
            }
            catch { /* Jika gagal, biarkan kosong */ }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatistik();
            LoadLogTerbaru();
            LoadLeaderboard();
        }

        private void AdjustLayout()
        {
            int margin = 35;
            int w = this.Width - (margin * 2);
            int cardW = (w / 4) - 15;

            // Card statistik — tidak berubah dari semula
            pnlUser.Width = cardW;
            pnlTrx.Width = cardW;
            pnlTrx.Left = margin + cardW + 20;
            pnlPO.Width = cardW;
            pnlPO.Left = margin + (cardW + 20) * 2;
            pnlAduan.Width = cardW;
            pnlAduan.Left = margin + (cardW + 20) * 3;

            // Tombol refresh — tidak berubah dari semula
            btnRefresh.Left = this.Width - margin - btnRefresh.Width;
            btnRefresh.Top = 25;

            // Panel bawah: log kiri, leaderboard kanan
            int bottomTop = pnlUser.Top + pnlUser.Height + 20;
            int bottomHeight = this.Height - bottomTop - margin;
            int halfW = (w / 2) - 10;

            lblLogTitle.Top = bottomTop;
            lblLogTitle.Left = margin;
            pnlLog.Top = bottomTop + 30;
            pnlLog.Left = margin;
            pnlLog.Width = halfW;
            pnlLog.Height = bottomHeight - 30;
            dgvLog.Width = pnlLog.Width - 4;
            dgvLog.Height = pnlLog.Height - 4;

            lblLeaderboardTitle.Top = bottomTop;
            lblLeaderboardTitle.Left = margin + halfW + 20;
            pnlLeaderboard.Top = bottomTop + 30;
            pnlLeaderboard.Left = margin + halfW + 20;
            pnlLeaderboard.Width = halfW;
            pnlLeaderboard.Height = bottomHeight - 30;
            dgvLeaderboard.Width = pnlLeaderboard.Width - 4;
            dgvLeaderboard.Height = pnlLeaderboard.Height - 4;
        }
    }
}
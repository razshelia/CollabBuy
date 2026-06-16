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
        private readonly Models.User _admin;
        private readonly AdminController _controller;

        public DashboardAdminControl(Models.User admin)
        {
            this.InitializeComponent();
            this._admin = admin;
            this._controller = new AdminController();
            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void DashboardAdminControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();

            // OOP BEST PRACTICE: Polimorfisme (Gunakan behavior spesifik jika objeknya Admin)
            Models.Admin adminMimin = this._admin as Models.Admin;
            if (adminMimin != null)
            {
                this.lblSapaan.Text = $"Hola {adminMimin.DapatkanNamaResmiMimin()}! 🚀";
            }
            else
            {
                this.lblSapaan.Text = $"Hola Mimin {this._admin.Username}! 🚀";
            }

            this.LoadStatistik();
            this.LoadLogTerbaru();
            this.LoadLeaderboard();
        }

        private void LoadStatistik()
        {
            Dictionary<string, int> stats = this._controller.GetStatsDashboard();

            if (stats != null)
            {
                if (stats.ContainsKey("users"))
                {
                    this.lblTotalUser.Text = stats["users"].ToString();
                }
                else
                {
                    this.lblTotalUser.Text = "0";
                }

                if (stats.ContainsKey("transaksi"))
                {
                    this.lblTotalTrx.Text = stats["transaksi"].ToString();
                }
                else
                {
                    this.lblTotalTrx.Text = "0";
                }

                if (stats.ContainsKey("po_aktif"))
                {
                    this.lblTotalPO.Text = stats["po_aktif"].ToString();
                }
                else
                {
                    this.lblTotalPO.Text = "0";
                }

                if (stats.ContainsKey("aduan"))
                {
                    this.lblAduan.Text = stats["aduan"].ToString();
                }
                else
                {
                    this.lblAduan.Text = "0";
                }
            }
            else
            {
                this.lblTotalUser.Text = "0";
                this.lblTotalTrx.Text = "0";
                this.lblTotalPO.Text = "0";
                this.lblAduan.Text = "0";
            }
        }

        private void LoadLogTerbaru()
        {
            try
            {
                DataTable dt = this._controller.GetLogAktivitasDataTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable dtTop = dt.Clone();
                    int batas = Math.Min(5, dt.Rows.Count);

                    for (int i = 0; i < batas; i++)
                    {
                        dtTop.ImportRow(dt.Rows[i]);
                    }

                    this.dgvLog.AutoGenerateColumns = false;
                    this.dgvLog.Columns.Clear();
                    this.dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pelaku", HeaderText = "User", DataPropertyName = "pelaku", Width = 110 });
                    this.dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Aktivitas", HeaderText = "Aktivitas", DataPropertyName = "aktivitas", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                    this.dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Waktu", HeaderText = "Waktu", DataPropertyName = "waktu_format", Width = 130 });

                    DataTable dtUI = new DataTable();
                    dtUI.Columns.Add("pelaku", typeof(string));
                    dtUI.Columns.Add("aktivitas", typeof(string));
                    dtUI.Columns.Add("waktu_format", typeof(string));

                    foreach (DataRow row in dtTop.Rows)
                    {
                        string waktuFormat;
                        if (row["waktu_akses"] != DBNull.Value)
                        {
                            waktuFormat = Convert.ToDateTime(row["waktu_akses"]).ToString("dd MMM, HH:mm");
                        }
                        else
                        {
                            waktuFormat = "-";
                        }
                        dtUI.Rows.Add(row["pelaku"], row["aktivitas"], waktuFormat);
                    }

                    this.dgvLog.DataSource = dtUI;
                    this.dgvLog.ClearSelection();
                }
            }
            catch
            {
                bool errorTertangkap = true;
            }
        }

        private void LoadLeaderboard()
        {
            try
            {
                DataTable dt = this._controller.GetLeaderboardPenjual();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dgvLeaderboard.AutoGenerateColumns = false;
                    this.dgvLeaderboard.Columns.Clear();
                    this.dgvLeaderboard.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Penjual", DataPropertyName = "nama_penjual", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                    this.dgvLeaderboard.Columns.Add(new DataGridViewTextBoxColumn { Name = "Omzet", HeaderText = "Total Omzet", DataPropertyName = "omzet_format", Width = 110 });
                    this.dgvLeaderboard.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tier", HeaderText = "Tier", DataPropertyName = "tier_penjual", Width = 150 });

                    DataTable dtUI = new DataTable();
                    dtUI.Columns.Add("nama_penjual", typeof(string));
                    dtUI.Columns.Add("omzet_format", typeof(string));
                    dtUI.Columns.Add("tier_penjual", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string omzetFormat;
                        if (row["total_omzet_bersih"] != DBNull.Value)
                        {
                            omzetFormat = "Rp " + Convert.ToInt64(row["total_omzet_bersih"]).ToString("N0");
                        }
                        else
                        {
                            omzetFormat = "Rp 0";
                        }
                        dtUI.Rows.Add(row["nama_penjual"], omzetFormat, row["tier_penjual"]);
                    }

                    this.dgvLeaderboard.DataSource = dtUI;
                    this.dgvLeaderboard.ClearSelection();
                }
            }
            catch
            {
                bool errorTertangkap = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadStatistik();
            this.LoadLogTerbaru();
            this.LoadLeaderboard();
        }

        private void AdjustLayout()
        {
            int margin = 35;
            int w = this.Width - (margin * 2);
            int cardW = (w / 4) - 15;

            // Card statistik
            this.pnlUser.Width = cardW;
            this.pnlTrx.Width = cardW;
            this.pnlTrx.Left = margin + cardW + 20;
            this.pnlPO.Width = cardW;
            this.pnlPO.Left = margin + (cardW + 20) * 2;
            this.pnlAduan.Width = cardW;
            this.pnlAduan.Left = margin + (cardW + 20) * 3;

            // Tombol refresh
            this.btnRefresh.Left = this.Width - margin - this.btnRefresh.Width;
            this.btnRefresh.Top = 25;

            // Panel bawah: log kiri, leaderboard kanan
            int bottomTop = this.pnlUser.Top + this.pnlUser.Height + 20;
            int bottomHeight = this.Height - bottomTop - margin;
            int halfW = (w / 2) - 10;

            this.lblLogTitle.Top = bottomTop;
            this.lblLogTitle.Left = margin;
            this.pnlLog.Top = bottomTop + 30;
            this.pnlLog.Left = margin;
            this.pnlLog.Width = halfW;
            this.pnlLog.Height = bottomHeight - 30;
            this.dgvLog.Width = this.pnlLog.Width - 4;
            this.dgvLog.Height = this.pnlLog.Height - 4;

            this.lblLeaderboardTitle.Top = bottomTop;
            this.lblLeaderboardTitle.Left = margin + halfW + 20;
            this.pnlLeaderboard.Top = bottomTop + 30;
            this.pnlLeaderboard.Left = margin + halfW + 20;
            this.pnlLeaderboard.Width = halfW;
            this.pnlLeaderboard.Height = bottomHeight - 30;
            this.dgvLeaderboard.Width = this.pnlLeaderboard.Width - 4;
            this.dgvLeaderboard.Height = this.pnlLeaderboard.Height - 4;
        }
    }
}
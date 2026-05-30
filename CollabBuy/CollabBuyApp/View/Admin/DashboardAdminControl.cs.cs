using System;
using System.Collections.Generic;
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
        }

        private void LoadStatistik()
        {
            Dictionary<string, int> stats = _controller.GetStatsDashboard();

            lblTotalUser.Text = stats["users"].ToString();
            lblTotalTrx.Text = stats["transaksi"].ToString();
            lblTotalPO.Text = stats["po_aktif"].ToString();
            lblAduan.Text = stats["aduan"].ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatistik();
        }

        private void AdjustLayout()
        {
            int margin = 35;
            int w = this.Width - (margin * 2);
            int cardW = (w / 4) - 15;

            pnlUser.Width = cardW;
            pnlTrx.Width = cardW;
            pnlTrx.Left = margin + cardW + 20;
            pnlPO.Width = cardW;
            pnlPO.Left = margin + (cardW + 20) * 2;
            pnlAduan.Width = cardW;
            pnlAduan.Left = margin + (cardW + 20) * 3;
        }
    }
}
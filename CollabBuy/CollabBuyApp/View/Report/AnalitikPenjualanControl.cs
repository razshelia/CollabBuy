using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Report
{
    public partial class AnalitikPenjualanControl : UserControl
    {
        private readonly LaporanController _laporanController;
        private readonly User _currentUser;

        public AnalitikPenjualanControl(User seller)
        {
            InitializeComponent();
            _currentUser = seller;
            _laporanController = new LaporanController();
        }

        private void AnalitikPenjualanControl_Load(object sender, EventArgs e)
        {
            LoadAnalitik();
        }

        private void LoadAnalitik()
        {
            try
            {
                // Mengambil ringkasan dari LaporanController
                // var data = _laporanController.GetAnalitikPenjualan(_currentUser.IdUser);

                // Mock Data untuk tampilan
                lblValPendapatan.Text = "Rp 2.500.000";

                // Binding ke DataGridView
                // dgvLaporan.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat laporan: " + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Export PDF akan segera hadir!");
        }
    }
}

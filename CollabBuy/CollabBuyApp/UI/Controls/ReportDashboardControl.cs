using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ReportDashboardControl : UserControl
    {
        private ReportService reportService;

        public ReportDashboardControl()
        {
            this.InitializeComponent();
            this.reportService = new ReportService();
            this.RefreshDataLaporan();
        }

        public void RefreshDataLaporan()
        {
            var cube = this.reportService.MuatLaporanOmzetCube();
            if (cube != null)
            {
                this.dgvCube.DataSource = cube;
            }
            else
            {
                // Kosong
            }

            var rollup = this.reportService.MuatLaporanFakultasRollup();
            if (rollup != null)
            {
                this.dgvRollup.DataSource = rollup;
            }
            else
            {
                // Kosong
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshDataLaporan();
            UXHelper.TampilkanSukses("Data Laporan Berhasil Di-update! 🔥");
        }
    }
}
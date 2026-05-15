using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminReportControl : UserControl
    {
        private ReportService _reportService;

        public AdminReportControl()
        {
            InitializeComponent();
            _reportService = new ReportService();
            ShowDefaultReport();
        }

        private void ShowDefaultReport()
        {
            dgvReport.DataSource = _reportService.BarangTerjualPerProduk();
        }

        private void btnBarangTerjual_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.BarangTerjualPerProduk();
        }

        private void btnCube_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.CubeKategoriJenisPO();
        }

        private void btnRollup_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.RollupOmzetPerWaktu();
        }

        private void btnGroupingSets_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.GroupingSetsPenjualKategori();
        }

        private void btnSubquery_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.SubqueryProdukKuotaMenipis();
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.UnionTransaksiBerjalanSelesai();
        }

        private void btnIntersect_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.IntersectPenjualJugaPembeli();
        }

        private void btnExcept_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.ExceptUserBelumTransaksi();
        }
    }
}
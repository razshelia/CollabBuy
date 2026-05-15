using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerReportControl : UserControl
    {
        private int _idPenjual;
        private ReportService _reportService;

        public SellerReportControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _reportService = new ReportService();
            LoadRingkasan();
            LoadBarangTerlaris();
        }

        private void LoadRingkasan()
        {
            try
            {
                // Ringkasan sederhana: kita bisa ambil dari service yang ada
                var productService = new ProductService();
                var poService = new PreorderService();
                var transactionService = new TransactionService();

                int totalProduk = poService.AmbilPOAktifByPenjual(_idPenjual).Count; // sederhana
                int totalPOAktif = poService.AmbilPOAktifByPenjual(_idPenjual).Count;
                decimal totalOmzet = 0; // bisa dihitung dari transaksi

                lblTotalProduk.Text = totalProduk.ToString();
                lblTotalPO.Text = totalPOAktif.ToString();
                lblTotalOmzet.Text = $"Rp {totalOmzet:N0}";
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat ringkasan: " + ex.Message);
            }
        }

        private void LoadBarangTerlaris()
        {
            DataTable dt = _reportService.BarangTerjualPerProduk();
            dgvLaporan.DataSource = dt;
            dgvLaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBarangTerlaris_Click(object sender, EventArgs e)
        {
            dgvLaporan.DataSource = _reportService.BarangTerjualPerProduk();
        }

        private void btnKuotaMenipis_Click(object sender, EventArgs e)
        {
            dgvLaporan.DataSource = _reportService.SubqueryProdukKuotaMenipis();
        }

        private void btnOmzetBulanan_Click(object sender, EventArgs e)
        {
            dgvLaporan.DataSource = _reportService.RollupOmzetPerWaktu();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRingkasan();
            btnBarangTerlaris.PerformClick();
        }
    }
}
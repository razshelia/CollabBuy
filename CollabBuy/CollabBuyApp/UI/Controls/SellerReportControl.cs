using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerReportControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly ReportService _reportService;

        public SellerReportControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _reportService = new ReportService(new ReportRepository());

            FormatTabelGenZ();
            SetupSummaryCards();
            LoadRingkasan();
            LoadBarangTerlaris();
        }

        private void FormatTabelGenZ()
        {
            dgvLaporan.BorderStyle = BorderStyle.None;
            dgvLaporan.BackgroundColor = Color.White;
            dgvLaporan.GridColor = Color.FromArgb(200, 182, 255);
            dgvLaporan.RowHeadersVisible = false;
            dgvLaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLaporan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLaporan.EnableHeadersVisualStyles = false;
            dgvLaporan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255);
            dgvLaporan.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvLaporan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLaporan.ColumnHeadersHeight = 38;
            dgvLaporan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 255, 182);
            dgvLaporan.DefaultCellStyle.SelectionForeColor = Color.FromArgb(36, 0, 70);
            dgvLaporan.RowTemplate.Height = 32;
        }

        private void SetupSummaryCards()
        {
            pnlRingkasan.Controls.Clear();
            pnlRingkasan.Controls.Add(BuatCardStat("TOTAL PRODUK MASTER 📦", out lblTotalProduk, Color.FromArgb(200, 182, 255), 20));
            pnlRingkasan.Controls.Add(BuatCardStat("SESI PO AKTIF 🏷️", out lblTotalPO, Color.FromArgb(253, 255, 182), 260));
            pnlRingkasan.Controls.Add(BuatCardStat("TOTAL OMZET LAPAK 💰", out lblTotalOmzet, Color.FromArgb(255, 138, 138), 500));
        }

        private Panel BuatCardStat(string judul, out Label labelRef, Color bgColor, int xPos)
        {
            Panel card = new Panel { Size = new Size(220, 75), BackColor = bgColor, Location = new Point(xPos, 12), BorderStyle = BorderStyle.FixedSingle };
            Label lblJudul = new Label { Text = judul, ForeColor = Color.FromArgb(36, 0, 70), Font = new Font("Segoe UI Black", 8.5F, FontStyle.Bold), Size = new Size(200, 20), Location = new Point(12, 10) };
            labelRef = new Label { Text = "0", ForeColor = Color.FromArgb(36, 0, 70), Font = new Font("Segoe UI Black", 18F, FontStyle.Bold), Size = new Size(200, 35), Location = new Point(10, 30) };
            card.Controls.Add(lblJudul);
            card.Controls.Add(labelRef);
            return card;
        }

        private void AktifkanTombol(Button btnAktif)
        {
            foreach (Control ctrl in pnlNavigasi.Controls)
            {
                if (ctrl is Button btn && btn != btnRefresh)
                {
                    btn.BackColor = Color.FromArgb(36, 0, 70);
                    btn.ForeColor = Color.White;
                }
            }
            btnAktif.BackColor = Color.FromArgb(253, 255, 182);
            btnAktif.ForeColor = Color.FromArgb(36, 0, 70);
        }

        private void LoadRingkasan()
        {
            try
            {
                var productService = new ProductService(new ProductRepository());
                var poService = new PreorderService(new PreorderRepository());
                var transactionService = new TransactionService(new TransactionRepository());

                lblTotalProduk.Text = productService.AmbilProdukByPenjual(_idPenjual).Count.ToString();
                lblTotalPO.Text = poService.AmbilPOAktifByPenjual(_idPenjual).Count.ToString();

                decimal totalOmzet = 0;
                var pesanan = transactionService.AmbilPesananMasukPenjual(_idPenjual);
                foreach (var t in pesanan) if (t.IsValid) totalOmzet += t.TotalBayarGrup;
                lblTotalOmzet.Text = $"Rp {totalOmzet:N0}";
            }
            catch { }
        }

        private void LoadBarangTerlaris()
        {
            dgvLaporan.DataSource = _reportService.BarangTerjualPerProduk();
            AktifkanTombol(btnBarangTerlaris);
        }

        private void btnBarangTerlaris_Click(object sender, EventArgs e) => LoadBarangTerlaris();

        private void btnCube_Click(object sender, EventArgs e)
        {
            // Menghubungkan tombol Kombinasi Kategori ke fungsi CUBE
            dgvLaporan.DataSource = _reportService.BarangTerjualPerProduk(); // Sesuaikan nama method CUBE aslimu jika berbeda
            AktifkanTombol((Button)sender);
        }

        private void btnOmzetBulanan_Click(object sender, EventArgs e)
        {
            dgvLaporan.DataSource = _reportService.RollupOmzetPerWaktu();
            AktifkanTombol((Button)sender);
        }

        private void btnGroupingSets_Click(object sender, EventArgs e)
        {
            // Menghubungkan tombol Ringkasan Grup ke fungsi Grouping Sets
            dgvLaporan.DataSource = _reportService.RollupOmzetPerWaktu(); // Sesuaikan nama method Grouping Sets aslimu jika berbeda
            AktifkanTombol((Button)sender);
        }

        private void btnKuotaMenipis_Click(object sender, EventArgs e)
        {
            dgvLaporan.DataSource = _reportService.SubqueryProdukKuotaMenipis();
            AktifkanTombol((Button)sender);
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
            // Menghubungkan tombol Semua Transaksi ke fungsi UNION
            dgvLaporan.DataSource = _reportService.BarangTerjualPerProduk(); // Sesuaikan nama method UNION aslimu jika berbeda
            AktifkanTombol((Button)sender);
        }

        private void btnIntersect_Click(object sender, EventArgs e)
        {
            // Menghubungkan tombol Produk Populer ke fungsi INTERSECT
            dgvLaporan.DataSource = _reportService.BarangTerjualPerProduk(); // Sesuaikan nama method INTERSECT aslimu jika berbeda
            AktifkanTombol((Button)sender);
        }

        private void btnExcept_Click(object sender, EventArgs e)
        {
            // Menghubungkan tombol Akun Pasif ke fungsi EXCEPT
            dgvLaporan.DataSource = _reportService.BarangTerjualPerProduk(); // Sesuaikan nama method EXCEPT aslimu jika berbeda
            AktifkanTombol((Button)sender);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRingkasan();
            LoadBarangTerlaris();
        }
    }
}
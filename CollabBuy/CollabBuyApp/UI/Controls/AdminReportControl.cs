using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminReportControl : UserControl
    {
        private readonly ReportService _reportService;

        public AdminReportControl()
        {
            InitializeComponent();

            // TAHAP 4: INJEKSI MANUAL DI UI
            _reportService = new ReportService(new ReportRepository());

            FormatTabelGenZ(); // Memanggil fungsi desain tabel retro pastel
            ShowDefaultReport();
        }

        private void FormatTabelGenZ()
        {
            // Menyulap DataGridView bawaan Windows menjadi tabel Neo-Retro
            dgvReport.BorderStyle = BorderStyle.None;
            dgvReport.BackgroundColor = Color.White;
            dgvReport.GridColor = Color.FromArgb(200, 182, 255); // Garis tabel ungu pastel
            dgvReport.RowHeadersVisible = false; // Sembunyikan panah kiri yang jadul
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Full screen lebar
            dgvReport.AllowUserToResizeRows = false;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Desain Header (Kolom Atas)
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255); // Pastel Purple
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70); // Dark Purple
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvReport.ColumnHeadersHeight = 40;

            // Desain Baris Isi
            dgvReport.DefaultCellStyle.BackColor = Color.White;
            dgvReport.DefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 255, 182); // Pastel Yellow saat diklik
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(36, 0, 70); // Teks tetap gelap saat diklik
            dgvReport.RowTemplate.Height = 35;
        }

        private void AktifkanTombol(Button btnAktif)
        {
            // Reset semua tombol ke ungu gelap
            foreach (Control ctrl in pnlNavigasi.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(36, 0, 70);
                    btn.ForeColor = Color.White;
                }
            }
            // Ubah tombol yang diklik menjadi ungu pastel agar terlihat "aktif"
            btnAktif.BackColor = Color.FromArgb(200, 182, 255);
            btnAktif.ForeColor = Color.FromArgb(36, 0, 70);
        }

        private void ShowDefaultReport()
        {
            dgvReport.DataSource = _reportService.BarangTerjualPerProduk();
            AktifkanTombol(btnBarangTerjual);
        }

        private void btnBarangTerjual_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.BarangTerjualPerProduk();
            AktifkanTombol((Button)sender);
        }

        private void btnCube_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.CubeKategoriJenisPO();
            AktifkanTombol((Button)sender);
        }

        private void btnRollup_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.RollupOmzetPerWaktu();
            AktifkanTombol((Button)sender);
        }

        private void btnGroupingSets_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.GroupingSetsPenjualKategori();
            AktifkanTombol((Button)sender);
        }

        private void btnSubquery_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.SubqueryProdukKuotaMenipis();
            AktifkanTombol((Button)sender);
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.UnionTransaksiBerjalanSelesai();
            AktifkanTombol((Button)sender);
        }

        private void btnIntersect_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.IntersectPenjualJugaPembeli();
            AktifkanTombol((Button)sender);
        }

        private void btnExcept_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = _reportService.ExceptUserBelumTransaksi();
            AktifkanTombol((Button)sender);
        }
    }
}
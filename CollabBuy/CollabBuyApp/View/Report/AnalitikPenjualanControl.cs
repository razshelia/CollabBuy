using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Report
{
    public partial class AnalitikPenjualanControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly LaporanController _laporanController;
        private readonly AdminController _adminController;
        private DataTable _dtRaw;

        public AnalitikPenjualanControl(Models.User currentUser)
        {
            this.InitializeComponent();
            this._currentUser = currentUser;
            this._laporanController = new LaporanController();
            this._adminController = new AdminController();
            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void AnalitikPenjualanControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();

            bool isAdmin = this._currentUser.Peran == "Admin";

            if (isAdmin)
                this.LoadModeAdmin();
            else
                this.LoadModePenjual();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bool isAdmin = this._currentUser.Peran == "Admin";
            if (isAdmin)
                this.LoadModeAdmin();
            else
                this.LoadModePenjual();
        }

        // =======================================================
        //  MODE ADMIN — Data Bisnis Seluruh Aplikasi
        // =======================================================

        private void LoadModeAdmin()
        {
            this.lblTitle.Text = "📊 Laporan Sistem Bisnis";
            this.lblSubtitle.Text = "Ringkasan data operasional CollabBuy untuk keputusan bisnis";

            this.SetupDataGridViewAdmin();
            this.LoadAdminStats();
            this.LoadAdminTable();
            this.LoadAdminChart();
        }

        private void SetupDataGridViewAdmin()
        {
            this.dgvLaporan.AutoGenerateColumns = false;
            this.dgvLaporan.Columns.Clear();

            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Penjual",
                HeaderText = "Nama Penjual",
                DataPropertyName = "nama_penjual",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Omzet",
                HeaderText = "Total Omzet Bersih (Rp)",
                DataPropertyName = "omzet_format",
                Width = 180
            });
            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tier",
                HeaderText = "Tier Penjual",
                DataPropertyName = "tier_penjual",
                Width = 160
            });
        }

        private void LoadAdminStats()
        {
            try
            {
                // Kartu 1: Total User
                var stats = this._adminController.GetStatsDashboard();
                this.lblTotalCuan.Text = stats["users"].ToString() + " User";
                this.lblCuanTitle.Text = "TOTAL PENGGUNA AKTIF 👥";
                this.pnlCuan.BackColor = Color.FromArgb(200, 230, 255);

                // Kartu 2: Total Transaksi
                this.lblTotalOrder.Text = stats["transaksi"].ToString() + " Transaksi";
                this.lblOrderTitle.Text = "TOTAL TRANSAKSI 🛒";
            }
            catch (Exception ex)
            {
                this.lblTotalCuan.Text = "Error";
                this.lblTotalOrder.Text = "Error";
                MessageBox.Show("Gagal memuat statistik: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAdminTable()
        {
            try
            {
                // Tabel: Klasifikasi performa semua penjual
                DataTable dtRaw = this._laporanController.GetKlasifikasiPerformaPenjual();
                this._dtRaw = dtRaw;

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("nama_penjual", typeof(string));
                dtUI.Columns.Add("omzet_format", typeof(string));
                dtUI.Columns.Add("tier_penjual", typeof(string));

                if (dtRaw != null)
                {
                    foreach (DataRow row in dtRaw.Rows)
                    {
                        string nama = row["nama_penjual"]?.ToString() ?? "-";
                        long omzet = row["total_omzet_bersih"] != DBNull.Value
                                       ? Convert.ToInt64(row["total_omzet_bersih"]) : 0L;
                        string tier = row["tier_penjual"]?.ToString() ?? "-";

                        dtUI.Rows.Add(nama, "Rp " + omzet.ToString("N0"), tier);
                    }
                }

                this.dgvLaporan.DataSource = dtUI;
                this.dgvLaporan.ClearSelection();

                // Label judul tabel
                this.lblGridTitle.Text = "📋 Performa & Klasifikasi Tier Penjual";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat tabel penjual: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAdminChart()
        {
            this.chartPenjualan.Series.Clear();

            Series series = new Series("Omzet Penjual");
            series.ChartType = SeriesChartType.Bar;
            series.Color = Color.FromArgb(90, 24, 154);
            series.BorderColor = Color.FromArgb(36, 0, 70);
            series.BorderWidth = 1;

            if (this._dtRaw != null && this._dtRaw.Rows.Count > 0)
            {
                // Tampilkan top 10 penjual berdasarkan omzet
                var query = this._dtRaw.AsEnumerable()
                    .Where(row => row["total_omzet_bersih"] != DBNull.Value)
                    .OrderByDescending(row => Convert.ToInt64(row["total_omzet_bersih"]))
                    .Take(10);

                foreach (var row in query)
                {
                    string nama = row["nama_penjual"]?.ToString() ?? "-";
                    long omzet = Convert.ToInt64(row["total_omzet_bersih"]);
                    series.Points.AddXY(nama, omzet);
                }
            }

            this.chartPenjualan.Series.Add(series);
            this.chartPenjualan.Titles.Clear();
            this.chartPenjualan.Titles.Add("Top Omzet Penjual (Rp)");
            this.chartPenjualan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chartPenjualan.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        // =======================================================
        //  MODE PENJUAL — Data Lapak Sendiri (Kode Asli)
        // =======================================================

        private void LoadModePenjual()
        {
            this.lblTitle.Text = "Analitik Cuan Kamu 💸";
            this.lblSubtitle.Text = "Pantau terus pemasukan lapak kamu, biar makin semangat jualan!";
            this.pnlCuan.BackColor = Color.FromArgb(200, 255, 200);

            this.SetupDataGridViewPenjual();

            try
            {
                // Kartu ringkasan
                var (totalPendapatan, totalPesanan) =
                    this._laporanController.GetRingkasanLapak(this._currentUser.IdUser);

                this.lblTotalCuan.Text = "Rp " + totalPendapatan.ToString("N0");
                this.lblCuanTitle.Text = "TOTAL CUAN MASUK 🤑";
                this.lblTotalOrder.Text = totalPesanan + " Pesanan";
                this.lblOrderTitle.Text = "ORDERAN KELAR ✅";

                // Tabel riwayat cuan
                this._dtRaw = this._laporanController.GetDetailRiwayatCuan(this._currentUser.IdUser);

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("nama_pembeli", typeof(string));
                dtUI.Columns.Add("tanggal_format", typeof(string));
                dtUI.Columns.Add("total_format", typeof(string));

                if (this._dtRaw != null)
                {
                    foreach (DataRow row in this._dtRaw.Rows)
                    {
                        string pembeli = row["nama_pembeli"] != DBNull.Value
                            ? row["nama_pembeli"].ToString() : "Anonim";
                        string tanggal = row["tanggal_pesanan"] != DBNull.Value
                            ? Convert.ToDateTime(row["tanggal_pesanan"]).ToString("dd MMM yyyy") : "-";
                        string total = row["total_harga"] != DBNull.Value
                            ? "Rp " + Convert.ToInt64(row["total_harga"]).ToString("N0") : "Rp 0";

                        dtUI.Rows.Add(pembeli, tanggal, total);
                    }
                }

                this.dgvLaporan.DataSource = dtUI;
                this.dgvLaporan.ClearSelection();
                this.lblGridTitle.Text = "📋 Riwayat Transaksi Selesai";

                this.LoadChartDataPenjual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data analitik: " + ex.Message, "Waduh Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewPenjual()
        {
            this.dgvLaporan.AutoGenerateColumns = false;
            this.dgvLaporan.Columns.Clear();

            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pembeli",
                HeaderText = "Pembeli",
                DataPropertyName = "nama_pembeli",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Waktu Selesai",
                DataPropertyName = "tanggal_format",
                Width = 130
            });
            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Cuan (Rp)",
                DataPropertyName = "total_format",
                Width = 110
            });
        }

        private void LoadChartDataPenjual()
        {
            this.chartPenjualan.Series.Clear();
            Series series = new Series("Pendapatan Harian");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(200, 182, 255);
            series.BorderColor = Color.FromArgb(36, 0, 70);
            series.BorderWidth = 1;

            if (this._dtRaw != null && this._dtRaw.Rows.Count > 0)
            {
                var query = this._dtRaw.AsEnumerable()
                    .Where(row => row["tanggal_pesanan"] != DBNull.Value)
                    .GroupBy(row => Convert.ToDateTime(row["tanggal_pesanan"]).ToString("dd MMM"))
                    .Select(g => new
                    {
                        Tanggal = g.Key,
                        Total = g.Sum(row => row["total_harga"] != DBNull.Value
                                       ? Convert.ToInt64(row["total_harga"]) : 0L)
                    })
                    .Reverse();

                foreach (var item in query)
                    series.Points.AddXY(item.Tanggal, item.Total);
            }

            this.chartPenjualan.Series.Add(series);
            this.chartPenjualan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chartPenjualan.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        // =======================================================
        //  LAYOUT & RESIZE
        // =======================================================

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);

            int cardW = (int)(w * 0.36);
            this.pnlCuan.Width = cardW;

            this.pnlOrder.Left = margin + cardW + 14;
            this.pnlOrder.Width = (int)(w * 0.31);

            this.btnUnduhPdf.Left = this.pnlOrder.Left + this.pnlOrder.Width + 14;
            this.btnUnduhPdf.Width = this.Width - this.btnUnduhPdf.Left - margin;

            this.pnlGrid.Width = w;
            this.pnlGrid.Height = this.Height - this.pnlGrid.Top - margin;

            int innerW = this.pnlGrid.Width - 48;
            int gridW = (int)(innerW * 0.47);

            this.dgvLaporan.Width = gridW;
            this.dgvLaporan.Height = this.pnlGrid.Height - this.dgvLaporan.Top - 20;

            this.chartPenjualan.Left = this.dgvLaporan.Left + gridW + 16;
            this.chartPenjualan.Width = innerW - gridW - 16;
            this.chartPenjualan.Height = this.pnlGrid.Height - this.chartPenjualan.Top - 20;

            this.btnRefresh.Left = this.pnlGrid.Width - this.btnRefresh.Width - 24;
        }

        // =======================================================
        //  CETAK / EXPORT PDF
        // =======================================================

        private void btnUnduhPdf_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += new PrintPageEventHandler(this.DrawPdfContent);
            printDialog.Document = printDocument;

            MessageBox.Show(
                "Tips: Pilih printer 'Microsoft Print to PDF' untuk menyimpan sebagai file PDF.",
                "Info Cetak Laporan", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try { printDocument.Print(); }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mencetak: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DrawPdfContent(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontJudul = new Font("Segoe UI Black", 18, FontStyle.Bold);
            Font fontSub = new Font("Segoe UI", 12, FontStyle.Regular);
            Font fontTabelHeader = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fontTabelIsi = new Font("Segoe UI", 10, FontStyle.Regular);
            Brush brushHitam = Brushes.Black;
            Pen penGaris = new Pen(Color.Gray, 1);

            int yPos = 50;
            int marginKiri = 50;

            bool isAdmin = this._currentUser.Peran == "Admin";

            if (isAdmin)
            {
                // ── Header Admin ──
                g.DrawString("LAPORAN SISTEM BISNIS COLLABBUY", fontJudul, brushHitam, marginKiri, yPos);
                yPos += 40;
                g.DrawString($"Dicetak oleh : {this._currentUser.Nama} (Admin)", fontSub, brushHitam, marginKiri, yPos);
                yPos += 25;
                g.DrawString($"Waktu cetak  : {DateTime.Now:dd MMMM yyyy, HH:mm}", fontSub, brushHitam, marginKiri, yPos);
                yPos += 30;
                g.DrawLine(penGaris, marginKiri, yPos, 750, yPos);
                yPos += 20;

                // ── Statistik Ringkasan ──
                g.DrawString($"Total Pengguna  : {this.lblTotalCuan.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 25;
                g.DrawString($"Total Transaksi : {this.lblTotalOrder.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 30;

                // ── Grafik ──
                g.DrawString("Grafik Top Omzet Penjual:", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 25;
                int chartW = this.chartPenjualan.Width > 0 ? this.chartPenjualan.Width : 650;
                int chartH = this.chartPenjualan.Height > 0 ? this.chartPenjualan.Height : 250;
                using (System.Drawing.Bitmap chartBmp = new System.Drawing.Bitmap(chartW, chartH))
                {
                    this.chartPenjualan.DrawToBitmap(chartBmp, new Rectangle(0, 0, chartW, chartH));
                    g.DrawImage(chartBmp, new Rectangle(marginKiri, yPos, 650, 250));
                }
                yPos += 270;

                // ── Tabel Performa Penjual ──
                g.DrawString("Tabel Klasifikasi Performa Penjual:", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 25;
                g.DrawRectangle(penGaris, marginKiri, yPos, 700, 25);
                g.DrawString("Nama Penjual", fontTabelHeader, brushHitam, marginKiri + 5, yPos + 4);
                g.DrawString("Omzet Bersih", fontTabelHeader, brushHitam, marginKiri + 280, yPos + 4);
                g.DrawString("Tier", fontTabelHeader, brushHitam, marginKiri + 500, yPos + 4);
                yPos += 25;

                if (this._dtRaw != null)
                {
                    foreach (DataRow row in this._dtRaw.Rows)
                    {
                        string nama = row["nama_penjual"]?.ToString() ?? "-";
                        string omzet = row["total_omzet_bersih"] != DBNull.Value
                            ? "Rp " + Convert.ToInt64(row["total_omzet_bersih"]).ToString("N0") : "Rp 0";
                        string tier = row["tier_penjual"]?.ToString() ?? "-";

                        if (nama.Length > 25) nama = nama.Substring(0, 25) + "..";

                        g.DrawString(nama, fontTabelIsi, brushHitam, marginKiri + 5, yPos + 4);
                        g.DrawString(omzet, fontTabelIsi, Brushes.Green, marginKiri + 280, yPos + 4);
                        g.DrawString(tier, fontTabelIsi, brushHitam, marginKiri + 500, yPos + 4);
                        g.DrawLine(penGaris, marginKiri, yPos + 25, marginKiri + 700, yPos + 25);
                        yPos += 25;
                    }
                }
            }
            else
            {
                // ── Header Penjual (kode asli) ──
                g.DrawString("LAPORAN PERTANGGUNGJAWABAN (LPJ) DANUS", fontJudul, brushHitam, marginKiri, yPos);
                yPos += 40;
                g.DrawString($"Nama Lapak/Penjual : {this._currentUser.Nama}", fontSub, brushHitam, marginKiri, yPos);
                yPos += 25;
                g.DrawString($"Waktu Cetak Dokumen: {DateTime.Now:dd MMMM yyyy, HH:mm}", fontSub, brushHitam, marginKiri, yPos);
                yPos += 30;
                g.DrawLine(penGaris, marginKiri, yPos, 750, yPos);
                yPos += 20;

                g.DrawString($"Total Pesanan Kelar : {this.lblTotalOrder.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 25;
                g.DrawString($"Total Cuan Bersih   : {this.lblTotalCuan.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 30;

                g.DrawString("Grafik Pendapatan Harian:", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 25;
                int chartW2 = this.chartPenjualan.Width > 0 ? this.chartPenjualan.Width : 650;
                int chartH2 = this.chartPenjualan.Height > 0 ? this.chartPenjualan.Height : 250;
                using (System.Drawing.Bitmap chartBmp2 = new System.Drawing.Bitmap(chartW2, chartH2))
                {
                    this.chartPenjualan.DrawToBitmap(chartBmp2, new Rectangle(0, 0, chartW2, chartH2));
                    g.DrawImage(chartBmp2, new Rectangle(marginKiri, yPos, 650, 250));
                }
                yPos += 270;

                g.DrawString("Rincian Penjualan per Barang & PO:", fontTabelHeader, brushHitam, marginKiri, yPos);
                yPos += 25;

                g.DrawRectangle(penGaris, marginKiri, yPos, 700, 25);
                g.DrawString("Sesi PO", fontTabelHeader, brushHitam, marginKiri + 5, yPos + 4);
                g.DrawString("Nama Produk", fontTabelHeader, brushHitam, marginKiri + 150, yPos + 4);
                g.DrawString("Terjual", fontTabelHeader, brushHitam, marginKiri + 350, yPos + 4);
                g.DrawString("Refund GR", fontTabelHeader, brushHitam, marginKiri + 450, yPos + 4);
                g.DrawString("Omzet Bersih", fontTabelHeader, brushHitam, marginKiri + 580, yPos + 4);
                yPos += 25;

                DataTable dtLpj = this._laporanController.GetLpjDanusPerPo(this._currentUser.IdUser);
                if (dtLpj != null && dtLpj.Rows.Count > 0)
                {
                    foreach (DataRow row in dtLpj.Rows)
                    {
                        string judulPo = row.IsNull("judul_po") ? "Reguler"
                                         : row["judul_po"].ToString();
                        string namaProduk = row["nama_produk"] != DBNull.Value ? row["nama_produk"].ToString() : "-";
                        string terjual = row["total_barang_terjual"] != DBNull.Value
                                         ? row["total_barang_terjual"] + " pcs" : "0 pcs";
                        string refund = row["total_refund_dicairkan"] != DBNull.Value
                                         ? "Rp " + Convert.ToInt64(row["total_refund_dicairkan"]).ToString("N0") : "Rp 0";
                        string omzet = row["omzet_bersih_lpj"] != DBNull.Value
                                         ? "Rp " + Convert.ToInt64(row["omzet_bersih_lpj"]).ToString("N0") : "Rp 0";

                        if (judulPo.Length > 15) judulPo = judulPo.Substring(0, 15) + "..";
                        if (namaProduk.Length > 20) namaProduk = namaProduk.Substring(0, 20) + "..";

                        g.DrawString(judulPo, fontTabelIsi, brushHitam, marginKiri + 5, yPos + 4);
                        g.DrawString(namaProduk, fontTabelIsi, brushHitam, marginKiri + 150, yPos + 4);
                        g.DrawString(terjual, fontTabelIsi, brushHitam, marginKiri + 350, yPos + 4);
                        g.DrawString(refund, fontTabelIsi, Brushes.Red, marginKiri + 450, yPos + 4);
                        g.DrawString(omzet, fontTabelIsi, Brushes.Green, marginKiri + 580, yPos + 4);
                        g.DrawLine(penGaris, marginKiri, yPos + 25, marginKiri + 700, yPos + 25);
                        yPos += 25;
                    }
                }
                else
                {
                    g.DrawString("Belum ada data barang yang selesai terjual.", fontTabelIsi,
                        Brushes.Gray, marginKiri + 5, yPos + 4);
                    yPos += 25;
                }
            }

            // Footer
            yPos += 30;
            g.DrawString("Laporan ini di-generate otomatis oleh Sistem CollabBuy.", fontSub, Brushes.Gray, marginKiri, yPos);
        }
    }
}
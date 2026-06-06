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
                // Ambil semua data yang dibutuhkan
                var stats = this._adminController.GetStatsDashboard();
                DataTable dtPerforma = this._laporanController.GetKlasifikasiPerformaPenjual();
                DataTable dtKeuangan = this._laporanController.GetLaporanKeuanganRollup();
                DataTable dtSultan = this._laporanController.GetSultanMemberIntersect();
                DataTable dtPasif = this._laporanController.GetPenggunaPasifExcept();
                DataTable dtKritis = this._laporanController.GetProdukSisaKuotaKritis();
                DataTable dtBarang = this._laporanController.GetTotalBarangTerjual();

                // Hitung grand total keuangan dari rollup (baris dengan tahun & bulan = null = grand total)
                long grandOmzetKotor = 0;
                long grandRefund = 0;
                long grandOmzetBersih = 0;
                if (dtKeuangan != null)
                {
                    foreach (DataRow row in dtKeuangan.Rows)
                    {
                        bool isTahunNull = row["tahun"] == DBNull.Value;
                        bool isBulanNull = row["bulan"] == DBNull.Value;
                        if (isTahunNull && isBulanNull) // baris grand total dari ROLLUP
                        {
                            grandOmzetKotor = row["omzet_kotor"] != DBNull.Value ? Convert.ToInt64(row["omzet_kotor"]) : 0;
                            grandRefund = row["total_refund"] != DBNull.Value ? Convert.ToInt64(row["total_refund"]) : 0;
                            grandOmzetBersih = row["omzet_bersih"] != DBNull.Value ? Convert.ToInt64(row["omzet_bersih"]) : 0;
                        }
                    }
                }

                Font fontJudulLpj = new Font("Segoe UI Black", 13, FontStyle.Bold);
                Font fontSeksi = new Font("Segoe UI", 10, FontStyle.Bold);
                Font fontIsi = new Font("Segoe UI", 9, FontStyle.Regular);
                Font fontItalic = new Font("Segoe UI", 9, FontStyle.Italic);
                Brush brushUngu = new SolidBrush(Color.FromArgb(60, 0, 120));
                Brush brushHijau = new SolidBrush(Color.FromArgb(0, 120, 50));
                Brush brushMerah = new SolidBrush(Color.FromArgb(180, 0, 0));
                Pen penTebal = new Pen(Color.FromArgb(60, 0, 120), 2);
                Pen penTipis = new Pen(Color.LightGray, 1);
                int lebar = 650;

                // ═══════════════════════════════════════════
                // I. KOP
                // ═══════════════════════════════════════════
                g.DrawString("LAPORAN OPERASIONAL SISTEM COLLABBUY", fontJudulLpj, brushUngu, marginKiri, yPos);
                yPos += 28;
                g.DrawString("Sistem Agregator Dana Usaha Mahasiswa — Laporan Pengelola", fontIsi, Brushes.Gray, marginKiri, yPos);
                yPos += 18;
                g.DrawLine(penTebal, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 12;

                g.DrawString($"Dicetak oleh : {this._currentUser.Nama} (Administrator)", fontIsi, brushHitam, marginKiri, yPos);
                yPos += 16;
                g.DrawString($"Tanggal cetak: {DateTime.Now:dd MMMM yyyy, HH:mm}", fontIsi, brushHitam, marginKiri, yPos);
                yPos += 20;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 14;

                // ═══════════════════════════════════════════
                // II. RINGKASAN STATISTIK SISTEM
                // ═══════════════════════════════════════════
                g.DrawString("A. RINGKASAN STATISTIK SISTEM", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 18;

                string[] labelStat = { "Total Pengguna", "Total Transaksi", "Sesi PO Aktif", "Aduan Masuk" };
                string[] nilaiStat =
                {
                    stats.ContainsKey("users")     ? stats["users"].ToString()     : "0",
                    stats.ContainsKey("transaksi")  ? stats["transaksi"].ToString() : "0",
                    stats.ContainsKey("po_aktif")   ? stats["po_aktif"].ToString()  : "0",
                    stats.ContainsKey("aduan")      ? stats["aduan"].ToString()     : "0"
                };

                int colW4 = lebar / 4;
                for (int i = 0; i < 4; i++)
                {
                    int x = marginKiri + (i * colW4);
                    g.DrawRectangle(new Pen(Color.FromArgb(200, 182, 255), 1), x, yPos, colW4 - 4, 44);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 255)), x + 1, yPos + 1, colW4 - 6, 42);
                    g.DrawString(labelStat[i], fontItalic, Brushes.Gray, x + 6, yPos + 4);
                    g.DrawString(nilaiStat[i], fontSeksi, brushUngu, x + 6, yPos + 22);
                }
                yPos += 56;

                // ═══════════════════════════════════════════
                // III. RINGKASAN KEUANGAN SISTEM
                // ═══════════════════════════════════════════
                g.DrawString("B. RINGKASAN KEUANGAN KESELURUHAN (Transaksi Selesai)", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 18;

                string[] labelKeu = { "Omzet Kotor Platform", "Total Refund GR Dicairkan", "Omzet Bersih Platform" };
                string[] nilaiKeu =
                {
                    "Rp " + grandOmzetKotor.ToString("N0"),
                    "Rp " + grandRefund.ToString("N0"),
                    "Rp " + grandOmzetBersih.ToString("N0")
                };
                Brush[] brushKeu = { brushHitam, brushMerah, brushHijau };

                int colW3 = lebar / 3;
                for (int i = 0; i < 3; i++)
                {
                    int x = marginKiri + (i * colW3);
                    g.DrawRectangle(new Pen(Color.FromArgb(200, 182, 255), 1), x, yPos, colW3 - 4, 44);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 255)), x + 1, yPos + 1, colW3 - 6, 42);
                    g.DrawString(labelKeu[i], fontItalic, Brushes.Gray, x + 6, yPos + 4);
                    g.DrawString(nilaiKeu[i], fontSeksi, brushKeu[i], x + 6, yPos + 22);
                }
                yPos += 56;

                // Tabel keuangan per bulan (dari ROLLUP, hanya baris yang ada tahun & bulan)
                g.DrawString("Rincian Keuangan per Bulan:", fontSeksi, brushHitam, marginKiri, yPos);
                yPos += 16;

                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 182, 255)), marginKiri, yPos, lebar, 20);
                g.DrawString("Tahun", fontSeksi, brushHitam, marginKiri + 5, yPos + 2);
                g.DrawString("Bulan", fontSeksi, brushHitam, marginKiri + 80, yPos + 2);
                g.DrawString("Omzet Kotor (Rp)", fontSeksi, brushHitam, marginKiri + 160, yPos + 2);
                g.DrawString("Refund GR (Rp)", fontSeksi, brushHitam, marginKiri + 360, yPos + 2);
                g.DrawString("Omzet Bersih (Rp)", fontSeksi, brushHitam, marginKiri + 520, yPos + 2);
                yPos += 20;

                string[] namaBulan = { "", "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agu", "Sep", "Okt", "Nov", "Des" };
                if (dtKeuangan != null)
                {
                    foreach (DataRow row in dtKeuangan.Rows)
                    {
                        if (row["tahun"] == DBNull.Value || row["bulan"] == DBNull.Value) continue; // skip grand total
                        int tahun = Convert.ToInt32(row["tahun"]);
                        int bulan = Convert.ToInt32(row["bulan"]);
                        long ko = row["omzet_kotor"] != DBNull.Value ? Convert.ToInt64(row["omzet_kotor"]) : 0;
                        long re = row["total_refund"] != DBNull.Value ? Convert.ToInt64(row["total_refund"]) : 0;
                        long be = row["omzet_bersih"] != DBNull.Value ? Convert.ToInt64(row["omzet_bersih"]) : 0;
                        string bln = bulan >= 1 && bulan <= 12 ? namaBulan[bulan] : bulan.ToString();

                        g.DrawString(tahun.ToString(), fontIsi, brushHitam, marginKiri + 5, yPos + 2);
                        g.DrawString(bln, fontIsi, brushHitam, marginKiri + 80, yPos + 2);
                        g.DrawString("Rp " + ko.ToString("N0"), fontIsi, brushHitam, marginKiri + 160, yPos + 2);
                        g.DrawString("Rp " + re.ToString("N0"), fontIsi, brushMerah, marginKiri + 360, yPos + 2);
                        g.DrawString("Rp " + be.ToString("N0"), fontIsi, brushHijau, marginKiri + 520, yPos + 2);
                        g.DrawLine(penTipis, marginKiri, yPos + 18, marginKiri + lebar, yPos + 18);
                        yPos += 18;
                    }
                }
                yPos += 10;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 14;

                // ═══════════════════════════════════════════
                // IV. GRAFIK TOP OMZET
                // ═══════════════════════════════════════════
                g.DrawString("C. GRAFIK TOP OMZET PENJUAL", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 16;
                int chartWA = Math.Max(this.chartPenjualan.Width, 400);
                int chartHA = Math.Max(this.chartPenjualan.Height, 200);
                using (var bmp = new System.Drawing.Bitmap(chartWA, chartHA))
                {
                    this.chartPenjualan.DrawToBitmap(bmp, new Rectangle(0, 0, chartWA, chartHA));
                    g.DrawImage(bmp, new Rectangle(marginKiri, yPos, lebar, 180));
                }
                yPos += 192;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 14;

                // ═══════════════════════════════════════════
                // V. PERFORMA PENJUAL
                // ═══════════════════════════════════════════
                g.DrawString("D. KLASIFIKASI PERFORMA PENJUAL", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 16;

                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 182, 255)), marginKiri, yPos, lebar, 20);
                g.DrawString("Nama Penjual", fontSeksi, brushHitam, marginKiri + 5, yPos + 2);
                g.DrawString("Omzet Bersih (Rp)", fontSeksi, brushHitam, marginKiri + 280, yPos + 2);
                g.DrawString("Tier", fontSeksi, brushHitam, marginKiri + 500, yPos + 2);
                yPos += 20;

                if (dtPerforma != null)
                {
                    foreach (DataRow row in dtPerforma.Rows)
                    {
                        string nama = row["nama_penjual"]?.ToString() ?? "-";
                        long omzet = row["total_omzet_bersih"] != DBNull.Value ? Convert.ToInt64(row["total_omzet_bersih"]) : 0;
                        string tier = row["tier_penjual"]?.ToString() ?? "-";
                        if (nama.Length > 28) nama = nama.Substring(0, 28) + "..";

                        g.DrawString(nama, fontIsi, brushHitam, marginKiri + 5, yPos + 2);
                        g.DrawString("Rp " + omzet.ToString("N0"), fontIsi, brushHijau, marginKiri + 280, yPos + 2);
                        g.DrawString(tier, fontIsi, brushUngu, marginKiri + 500, yPos + 2);
                        g.DrawLine(penTipis, marginKiri, yPos + 18, marginKiri + lebar, yPos + 18);
                        yPos += 18;
                    }
                }
                yPos += 10;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 14;

                // ═══════════════════════════════════════════
                // VI. ANALISIS PENGGUNA
                // ═══════════════════════════════════════════
                g.DrawString("E. ANALISIS PENGGUNA", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 16;

                // Dua kolom: kiri = penjual aktif bertransaksi, kanan = pengguna pasif
                g.DrawString($"Penjual Terverifikasi & Bertransaksi ({(dtSultan?.Rows.Count ?? 0)} orang):",
                    fontSeksi, brushHijau, marginKiri, yPos);
                g.DrawString($"Pengguna Belum Pernah Transaksi ({(dtPasif?.Rows.Count ?? 0)} orang):",
                    fontSeksi, brushMerah, marginKiri + 370, yPos);
                yPos += 16;

                int maxBaris = Math.Max(dtSultan?.Rows.Count ?? 0, dtPasif?.Rows.Count ?? 0);
                maxBaris = Math.Min(maxBaris, 8); // cap 8 baris agar tidak overflow halaman

                for (int i = 0; i < maxBaris; i++)
                {
                    if (dtSultan != null && i < dtSultan.Rows.Count)
                        g.DrawString("• " + dtSultan.Rows[i]["nama"].ToString(), fontIsi, brushHitam, marginKiri + 5, yPos + 2);
                    if (dtPasif != null && i < dtPasif.Rows.Count)
                        g.DrawString("• " + dtPasif.Rows[i]["nama"].ToString(), fontIsi, brushHitam, marginKiri + 375, yPos + 2);
                    yPos += 16;
                }

                if ((dtSultan?.Rows.Count ?? 0) > 8 || (dtPasif?.Rows.Count ?? 0) > 8)
                {
                    g.DrawString("(dan lainnya — lihat data lengkap di aplikasi)",
                        fontItalic, Brushes.Gray, marginKiri + 5, yPos + 2);
                    yPos += 16;
                }
                yPos += 6;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 14;

                // ═══════════════════════════════════════════
                // VII. PRODUK KUOTA KRITIS
                // ═══════════════════════════════════════════
                g.DrawString("F. PRODUK DENGAN SISA KUOTA KRITIS (≤ 5 unit)", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 16;

                if (dtKritis == null || dtKritis.Rows.Count == 0)
                {
                    g.DrawString("✅ Tidak ada produk dengan kuota kritis saat ini.", fontIsi, brushHijau, marginKiri + 5, yPos + 2);
                    yPos += 18;
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(200, 182, 255)), marginKiri, yPos, lebar, 20);
                    g.DrawString("Nama Produk", fontSeksi, brushHitam, marginKiri + 5, yPos + 2);
                    g.DrawString("Target Kuota", fontSeksi, brushHitam, marginKiri + 500, yPos + 2);
                    yPos += 20;
                    foreach (DataRow row in dtKritis.Rows)
                    {
                        string produk = row["nama_produk"]?.ToString() ?? "-";
                        string kuota = row["target_kuota"] != DBNull.Value ? row["target_kuota"].ToString() + " unit" : "-";
                        if (produk.Length > 55) produk = produk.Substring(0, 55) + "..";
                        g.DrawString(produk, fontIsi, brushMerah, marginKiri + 5, yPos + 2);
                        g.DrawString(kuota, fontIsi, brushHitam, marginKiri + 500, yPos + 2);
                        g.DrawLine(penTipis, marginKiri, yPos + 18, marginKiri + lebar, yPos + 18);
                        yPos += 18;
                    }
                }
            }
            else
            {
                // Ambil data LPJ lengkap
                DataTable dtLpj = this._laporanController.GetLpjDanusPerPo(this._currentUser.IdUser);

                // Hitung total keseluruhan
                long grandOmzetKotor = 0;
                long grandRefund = 0;
                long grandOmzetBersih = 0;
                int grandUnitTerjual = 0;

                if (dtLpj != null)
                {
                    foreach (DataRow row in dtLpj.Rows)
                    {
                        grandOmzetKotor += row["omzet_kotor"] != DBNull.Value ? Convert.ToInt64(row["omzet_kotor"]) : 0;
                        grandRefund += row["total_refund_dicairkan"] != DBNull.Value ? Convert.ToInt64(row["total_refund_dicairkan"]) : 0;
                        grandOmzetBersih += row["omzet_bersih_lpj"] != DBNull.Value ? Convert.ToInt64(row["omzet_bersih_lpj"]) : 0;
                        grandUnitTerjual += row["total_barang_terjual"] != DBNull.Value ? Convert.ToInt32(row["total_barang_terjual"]) : 0;
                    }
                }

                // Nama toko (kalau Penjual terverifikasi, pakai NamaToko)
                string namaToko = this._currentUser.Nama;
                if (this._currentUser is Models.Penjual penjual && !string.IsNullOrWhiteSpace(penjual.NamaToko))
                    namaToko = penjual.NamaToko;

                Font fontJudulLpj = new Font("Segoe UI Black", 14, FontStyle.Bold);
                Font fontSeksi = new Font("Segoe UI", 10, FontStyle.Bold);
                Font fontIsi = new Font("Segoe UI", 9, FontStyle.Regular);
                Font fontItalic = new Font("Segoe UI", 9, FontStyle.Italic);
                Brush brushUngu = new SolidBrush(Color.FromArgb(60, 0, 120));
                Brush brushHijau = new SolidBrush(Color.FromArgb(0, 120, 50));
                Brush brushMerah = new SolidBrush(Color.FromArgb(180, 0, 0));
                Pen penTebal = new Pen(Color.FromArgb(60, 0, 120), 2);
                Pen penTipis = new Pen(Color.LightGray, 1);
                int lebar = 700;

                // ═══════════════════════════════════════════════
                // I. KOP DOKUMEN
                // ═══════════════════════════════════════════════
                g.DrawString("LAPORAN PERTANGGUNGJAWABAN DANA USAHA (LPJ DANUS)", fontJudulLpj, brushUngu, marginKiri, yPos);
                yPos += 30;
                g.DrawString($"Sistem Agregator Dana Usaha Mahasiswa — CollabBuy", fontIsi, Brushes.Gray, marginKiri, yPos);
                yPos += 20;
                g.DrawLine(penTebal, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 12;

                // ═══════════════════════════════════════════════
                // II. IDENTITAS LAPAK
                // ═══════════════════════════════════════════════
                g.DrawString("A. IDENTITAS LAPAK / DANUS", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 20;

                void TulisField(string label, string nilai)
                {
                    g.DrawString(label, fontSeksi, brushHitam, marginKiri + 10, yPos);
                    g.DrawString(": " + nilai, fontIsi, brushHitam, marginKiri + 190, yPos);
                    yPos += 18;
                }

                TulisField("Nama Lapak / Toko", namaToko);
                TulisField("Penanggungjawab", this._currentUser.Nama);
                TulisField("Username Sistem", "@" + this._currentUser.Username);
                TulisField("Periode Laporan", $"{DateTime.Now:MMMM yyyy}");
                TulisField("Tanggal Cetak", DateTime.Now.ToString("dd MMMM yyyy, HH:mm"));
                TulisField("Status Akun", this._currentUser.DapatkanStatusAkun());
                yPos += 6;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 12;

                // ═══════════════════════════════════════════════
                // III. RINGKASAN EKSEKUTIF
                // ═══════════════════════════════════════════════
                g.DrawString("B. RINGKASAN EKSEKUTIF", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 20;

                // Kotak ringkasan
                string[] labelRingkasan = { "Total Unit Terjual", "Omzet Kotor", "Total Refund GR", "Omzet Bersih" };
                string[] nilaiRingkasan =
                {
                    grandUnitTerjual + " pcs",
                    "Rp " + grandOmzetKotor.ToString("N0"),
                    "Rp " + grandRefund.ToString("N0"),
                    "Rp " + grandOmzetBersih.ToString("N0")
                };

                int colW = lebar / 4;
                for (int i = 0; i < 4; i++)
                {
                    int x = marginKiri + (i * colW);
                    g.DrawRectangle(new Pen(Color.FromArgb(200, 182, 255), 1), x, yPos, colW - 4, 46);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 255)), x + 1, yPos + 1, colW - 6, 44);
                    g.DrawString(labelRingkasan[i], fontItalic, Brushes.Gray, x + 6, yPos + 4);
                    Brush brushNilai = (i == 3) ? brushHijau : brushHitam;
                    if (i == 2) brushNilai = brushMerah;
                    g.DrawString(nilaiRingkasan[i], fontSeksi, brushNilai, x + 6, yPos + 24);
                }
                yPos += 58;

                // Grafik pendapatan harian
                g.DrawString("Grafik Pendapatan Harian (Transaksi Selesai):", fontSeksi, brushHitam, marginKiri, yPos);
                yPos += 16;
                int chartW2 = Math.Max(this.chartPenjualan.Width, 400);
                int chartH2 = Math.Max(this.chartPenjualan.Height, 200);
                using (var bmp = new System.Drawing.Bitmap(chartW2, chartH2))
                {
                    this.chartPenjualan.DrawToBitmap(bmp, new Rectangle(0, 0, chartW2, chartH2));
                    g.DrawImage(bmp, new Rectangle(marginKiri, yPos, lebar, 180));
                }
                yPos += 192;
                g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                yPos += 12;

                // ═══════════════════════════════════════════════
                // IV. REALISASI PER SESI PO
                // ═══════════════════════════════════════════════
                g.DrawString("C. REALISASI PENJUALAN PER SESI PRE-ORDER", fontSeksi, brushUngu, marginKiri, yPos);
                yPos += 18;

                if (dtLpj == null || dtLpj.Rows.Count == 0)
                {
                    g.DrawString("Belum ada data penjualan yang selesai.", fontItalic, Brushes.Gray, marginKiri + 10, yPos);
                    yPos += 20;
                }
                else
                {
                    // Header tabel
                    int[] colX = {
                        marginKiri,           // 50  → Sesi PO / Jenis
                        marginKiri + 160,     // 210 → Nama Produk
                        marginKiri + 310,     // 360 → Harga Dasar
                        marginKiri + 395,     // 445 → Unit Terjual
                        marginKiri + 460,     // 510 → Omzet Kotor
                        marginKiri + 555,     // 605 → Refund GR
                        marginKiri + 605      // 655 → Bersih (sisa 45px, cukup untuk nilai pendek; atau geser jika perlu)
                    };

                    int[] colWArr = { 135, 145, 70, 75, 95, 95, 60 };
                    string[] hdr = { "Sesi PO / Jenis", "Nama Produk", "Harga Dasar", "Unit Terjual", "Omzet Kotor", "Refund GR", "Bersih" };

                    g.FillRectangle(new SolidBrush(Color.FromArgb(200, 182, 255)),
    marginKiri, yPos, lebar, 22);
                    for (int i = 0; i < hdr.Length; i++)
                        g.DrawString(hdr[i], fontSeksi, brushHitam, colX[i] + 3, yPos + 3);
                    yPos += 22;

                    // Variabel subtotal
                    string poSebelumnya = "";
                    long subOmzet = 0, subRefund = 0, subBersih = 0;
                    int subUnit = 0;

                    foreach (DataRow row in dtLpj.Rows)
                    {
                        string judulPo = row["judul_po"]?.ToString() ?? "-";
                        string jenisPo = row["jenis_po"]?.ToString() ?? "";
                        string statusPo = row["status_po"]?.ToString() ?? "";
                        string produk = row["nama_produk"]?.ToString() ?? "-";
                        long hargaDsr = row["harga_dasar"] != DBNull.Value ? Convert.ToInt64(row["harga_dasar"]) : 0;
                        int unit = row["total_barang_terjual"] != DBNull.Value ? Convert.ToInt32(row["total_barang_terjual"]) : 0;
                        long kotor = row["omzet_kotor"] != DBNull.Value ? Convert.ToInt64(row["omzet_kotor"]) : 0;
                        long refund = row["total_refund_dicairkan"] != DBNull.Value ? Convert.ToInt64(row["total_refund_dicairkan"]) : 0;
                        long bersih = row["omzet_bersih_lpj"] != DBNull.Value ? Convert.ToInt64(row["omzet_bersih_lpj"]) : 0;

                        // ── Header PO baru ──────────────────────────────────────────────
                        if (judulPo != poSebelumnya)
                        {
                            // Cetak subtotal PO sebelumnya terlebih dahulu
                            if (poSebelumnya != "")
                            {
                                g.FillRectangle(new SolidBrush(Color.FromArgb(235, 225, 255)), marginKiri, yPos, lebar, 18);
                                g.DrawString($"Sub-total: {poSebelumnya}", fontItalic, Brushes.Gray, colX[0] + 3, yPos + 2);
                                g.DrawString(subUnit + " pcs", fontItalic, Brushes.Gray, colX[3] + 3, yPos + 2);
                                g.DrawString("Rp " + subOmzet.ToString("N0"), fontItalic, Brushes.Gray, colX[4] + 3, yPos + 2);
                                g.DrawString(subRefund > 0
                                    ? "Rp " + subRefund.ToString("N0") : "Rp 0", fontItalic, brushMerah, colX[5] + 3, yPos + 2);
                                g.DrawString("Rp " + subBersih.ToString("N0"), fontItalic, brushHijau, colX[6] + 3, yPos + 2);
                                yPos += 18;
                                subOmzet = 0; subRefund = 0; subBersih = 0; subUnit = 0;
                            }

                            // Potong judul PO agar tidak overflow ke kolom berikutnya (maks ~22 karakter)
                            string judulTampil = judulPo.Length > 22 ? judulPo.Substring(0, 22) + ".." : judulPo;

                            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 235, 255)), marginKiri, yPos, lebar, 18);
                            g.DrawString($"📦 {judulTampil}  [{jenisPo}]  — {statusPo}", fontSeksi, brushUngu, colX[0] + 3, yPos + 2);
                            yPos += 18;
                            poSebelumnya = judulPo;
                        }

                        // ── Baris produk ────────────────────────────────────────────────
                        // Nama produk: maks ~20 karakter agar tidak nabrak kolom "Harga Dasar" (colX[2])
                        if (produk.Length > 20) produk = produk.Substring(0, 20) + "..";

                        g.DrawString(produk, fontIsi, brushHitam, colX[1] + 3, yPos + 2);
                        g.DrawString("Rp " + hargaDsr.ToString("N0"), fontIsi, brushHitam, colX[2] + 3, yPos + 2);
                        g.DrawString(unit + " pcs", fontIsi, brushHitam, colX[3] + 3, yPos + 2);
                        g.DrawString("Rp " + kotor.ToString("N0"), fontIsi, brushHitam, colX[4] + 3, yPos + 2);
                        g.DrawString(refund > 0
                            ? "Rp " + refund.ToString("N0") : "-", fontIsi, brushMerah, colX[5] + 3, yPos + 2);
                        g.DrawString("Rp " + bersih.ToString("N0"), fontIsi, brushHijau, colX[6] + 3, yPos + 2);
                        g.DrawLine(penTipis, marginKiri, yPos + 18, marginKiri + lebar, yPos + 18);
                        yPos += 18;

                        subUnit += unit;
                        subOmzet += kotor;
                        subRefund += refund;
                        subBersih += bersih;
                    }

                    // ── Subtotal PO terakhir ────────────────────────────────────────────
                    if (poSebelumnya != "")
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(235, 225, 255)), marginKiri, yPos, lebar, 18);
                        g.DrawString($"Sub-total: {poSebelumnya}", fontItalic, Brushes.Gray, colX[0] + 3, yPos + 2);
                        g.DrawString(subUnit + " pcs", fontItalic, Brushes.Gray, colX[3] + 3, yPos + 2);
                        g.DrawString("Rp " + subOmzet.ToString("N0"), fontItalic, Brushes.Gray, colX[4] + 3, yPos + 2);
                        g.DrawString(subRefund > 0
                            ? "Rp " + subRefund.ToString("N0") : "Rp 0", fontItalic, brushMerah, colX[5] + 3, yPos + 2);
                        g.DrawString("Rp " + subBersih.ToString("N0"), fontItalic, brushHijau, colX[6] + 3, yPos + 2);
                        yPos += 18;
                    }

                    // ── Grand total ─────────────────────────────────────────────────────
                    yPos += 4;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(60, 0, 120)), marginKiri, yPos, lebar, 26);
                    g.DrawString("TOTAL KESELURUHAN", fontSeksi, Brushes.White, colX[0] + 3, yPos + 4);
                    g.DrawString(grandUnitTerjual + " pcs", fontSeksi, Brushes.White, colX[3] + 3, yPos + 4);
                    g.DrawString("Rp " + grandOmzetKotor.ToString("N0"), fontSeksi, Brushes.White, colX[4] + 3, yPos + 4);
                    g.DrawString("Rp " + grandRefund.ToString("N0"),
                        fontSeksi, new SolidBrush(Color.FromArgb(255, 180, 180)), colX[5] + 3, yPos + 4);
                    g.DrawString("Rp " + grandOmzetBersih.ToString("N0"),
                        fontSeksi, new SolidBrush(Color.FromArgb(160, 255, 200)), colX[6] + 3, yPos + 4);
                    yPos += 28;

                    g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                    yPos += 14;

                    // ═══════════════════════════════════════════════
                    // V. TANDA TANGAN
                    // ═══════════════════════════════════════════════
                    g.DrawString("D. PERNYATAAN PERTANGGUNGJAWABAN", fontSeksi, brushUngu, marginKiri, yPos);
                    yPos += 18;
                    g.DrawString(
                        "Dengan ini saya menyatakan bahwa laporan ini adalah benar dan dapat dipertanggungjawabkan.",
                        fontIsi, brushHitam, marginKiri + 10, yPos);
                    yPos += 30;

                    // Tanda tangan kiri: penanggung jawab
                    g.DrawString("Dibuat oleh,", fontIsi, brushHitam, marginKiri + 40, yPos);
                    g.DrawString("Diketahui oleh,", fontIsi, brushHitam, marginKiri + 370, yPos);
                    yPos += 60;
                    g.DrawLine(new Pen(Color.Black, 1), marginKiri + 20, yPos, marginKiri + 220, yPos);
                    g.DrawLine(new Pen(Color.Black, 1), marginKiri + 350, yPos, marginKiri + 560, yPos);
                    yPos += 6;
                    g.DrawString(this._currentUser.Nama, fontSeksi, brushHitam, marginKiri + 40, yPos);
                    g.DrawString("____________________", fontIsi, Brushes.Gray, marginKiri + 360, yPos);
                    yPos += 16;
                    g.DrawString("Penanggungjawab Danus", fontItalic, Brushes.Gray, marginKiri + 40, yPos);
                    g.DrawString("Bendahara / Supervisor", fontItalic, Brushes.Gray, marginKiri + 360, yPos);
                }

                // Footer
                yPos += 30;
                g.DrawString("Laporan ini di-generate otomatis oleh Sistem CollabBuy.", fontSub, Brushes.Gray, marginKiri, yPos);
            }
        }
    }
}
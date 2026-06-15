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
        private int _adminTabAktif = 0;
        private int _pdfSectionAdmin = 0;
        private int _pdfIndexPerforma = 0;
        private int _pdfIndexPengguna = 0;
        private int _pdfIndexTerlaris = 0;
        private int _pdfSectionPenjual = 0;
        private int _pdfIndexLpj = 0;
        private string _pdfLpjPoSebelumnya = "";
        private long _pdfLpjSubOmzet = 0, _pdfLpjSubRefund = 0, _pdfLpjSubBersih = 0;
        private int _pdfLpjSubUnit = 0, _pdfLpjSubUnitPending = 0;

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

            this.TampilTombolTabAdmin();
            this.LoadAdminStats();

            switch (_adminTabAktif)
            {
                case 1: this.LoadAdminTabTransaksi(); break;
                case 2: this.LoadAdminTabHimpunan(); break;
                case 3: this.LoadAdminTabAnalisisPasar(); break;
                default:
                    this.SetupDataGridViewAdmin();
                    this.LoadAdminTable();
                    this.LoadAdminChart(); break;
            }
        }

        private void TampilTombolTabAdmin()
        {
            // Hapus panel tab lama jika ada, supaya tidak menumpuk setiap refresh
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                if (this.Controls[i] is Panel p && p.Name == "pnlTabAdmin")
                {
                    this.Controls.RemoveAt(i);
                    break;
                }
            }

            // Letakkan panel tab tepat di bawah subtitle (lblSubtitle.Bottom + jarak 8px)
            // dan di atas kartu statistik (pnlCuan.Top)
            int tabY = this.lblSubtitle.Bottom + 8;

            Panel pnlTab = new Panel
            {
                Name = "pnlTabAdmin",
                Height = 44,
                Left = 30,
                Top = tabY,
                Width = this.Width - 60,
                BackColor = Color.FromArgb(240, 235, 255),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            string[] labels = {
        "📊 Performa Penjual",
        "🧾 Semua Transaksi",
        "🔗 Teori Himpunan",
        "🧊 Analisis Pasar"
    };

            int x = 6;
            for (int i = 0; i < labels.Length; i++)
            {
                int idx = i;
                Button btn = new Button
                {
                    Text = labels[i],
                    Location = new Point(x, 6),
                    Size = new Size(170, 32),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    BackColor = idx == _adminTabAktif
                                ? Color.FromArgb(36, 0, 70)
                                : Color.White,
                    ForeColor = idx == _adminTabAktif
                                ? Color.FromArgb(253, 255, 182)
                                : Color.FromArgb(36, 0, 70),
                    Tag = idx
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
                btn.FlatAppearance.BorderSize = 1;
                btn.Click += (s, e) =>
                {
                    _adminTabAktif = idx;
                    this.LoadModeAdmin();
                };
                pnlTab.Controls.Add(btn);
                x += 178;
            }

            this.Controls.Add(pnlTab);

            // Geser kartu statistik ke bawah panel tab supaya tidak tertimpa
            int kartuY = pnlTab.Bottom + 10;
            this.pnlCuan.Top = kartuY;
            this.pnlOrder.Top = kartuY;
            this.btnUnduhPdf.Top = kartuY;

            // Geser pnlGrid mengikuti posisi kartu
            this.pnlGrid.Top = kartuY + this.pnlCuan.Height + 10;
            this.pnlGrid.Height = this.Height - this.pnlGrid.Top - 36;
        }

        private void LoadAdminTabTransaksi()
        {
            try
            {
                DataTable dt = this._laporanController.GetTransaksiLengkap();

                this.dgvLaporan.AutoGenerateColumns = false;
                this.dgvLaporan.Columns.Clear();

                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "IdTrx", HeaderText = "ID Trx", DataPropertyName = "id_transaksi", Width = 60 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "IdKoord", HeaderText = "ID Koordinator", DataPropertyName = "id_koordinator", Width = 90 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Tanggal", HeaderText = "Tanggal", DataPropertyName = "tanggal_transaksi", Width = 140 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Status", HeaderText = "Status", DataPropertyName = "status_pesanan", Width = 100 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "AdaBukti", HeaderText = "Bukti Bayar", DataPropertyName = "ada_bukti", Width = 90 });

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_transaksi", typeof(int));
                dtUI.Columns.Add("id_koordinator", typeof(int));
                dtUI.Columns.Add("tanggal_transaksi", typeof(string));
                dtUI.Columns.Add("status_pesanan", typeof(string));
                dtUI.Columns.Add("ada_bukti", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    bool adaBukti = row["bukti_bayar"] != DBNull.Value
                                    && row["bukti_bayar"] is byte[] b
                                    && b.Length > 0;

                    dtUI.Rows.Add(
                        row["id_transaksi"],
                        row["id_koordinator"],
                        Convert.ToDateTime(row["tanggal_transaksi"]).ToString("dd/MM/yyyy HH:mm"),
                        row["status_pesanan"].ToString(),
                        adaBukti ? "✅ Ada" : "❌ Belum"
                    );
                }

                this.dgvLaporan.DataSource = dtUI;
                this.dgvLaporan.ClearSelection();
                this.lblGridTitle.Text = "🧾 Semua Transaksi (vw_transaksi_lengkap)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat transaksi: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAdminTabHimpunan()
        {
            try
            {
                // Tampilkan UNION, INTERSECT, EXCEPT digabung dalam satu tabel dengan label pembeda
                DataTable dtUnion = this._laporanController.GetTransaksiAktifUnion();
                DataTable dtIntersect = this._laporanController.GetSultanMemberIntersect();
                DataTable dtExcept = this._laporanController.GetPenggunaPasifExcept();

                // Buat tabel gabungan dengan kolom keterangan
                DataTable dtGabung = new DataTable();
                dtGabung.Columns.Add("jenis_operasi", typeof(string));
                dtGabung.Columns.Add("data_1", typeof(string));
                dtGabung.Columns.Add("data_2", typeof(string));

                if (dtUnion != null)
                    foreach (DataRow r in dtUnion.Rows)
                        dtGabung.Rows.Add("UNION: Transaksi Aktif",
                            r["id_transaksi"].ToString(), r["status_pesanan"].ToString());

                if (dtIntersect != null)
                    foreach (DataRow r in dtIntersect.Rows)
                        dtGabung.Rows.Add("INTERSECT: Sultan Member",
                            r["id_user"].ToString(), r["nama"].ToString());

                if (dtExcept != null)
                    foreach (DataRow r in dtExcept.Rows)
                        dtGabung.Rows.Add("EXCEPT: Pengguna Pasif",
                            r["id_user"].ToString(), r["nama"].ToString());

                this.dgvLaporan.AutoGenerateColumns = false;
                this.dgvLaporan.Columns.Clear();
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Operasi", HeaderText = "Operasi Himpunan", DataPropertyName = "jenis_operasi", Width = 220 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Data1", HeaderText = "ID / Nilai", DataPropertyName = "data_1", Width = 100 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Data2",
                    HeaderText = "Nama / Status",
                    DataPropertyName = "data_2",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                this.dgvLaporan.DataSource = dtGabung;
                this.dgvLaporan.ClearSelection();
                this.lblGridTitle.Text = "🔗 Teori Himpunan (UNION · INTERSECT · EXCEPT)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data himpunan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAdminTabAnalisisPasar()
        {
            try
            {
                DataTable dtCube = this._laporanController.GetAnalisisPasarCube();
                DataTable dtGrouping = this._laporanController.GetRingkasanGlobalGroupingSets();

                // Tampilkan CUBE dulu, lalu append GROUPING SETS dengan label pembeda
                DataTable dtGabung = new DataTable();
                dtGabung.Columns.Add("jenis_analisis", typeof(string));
                dtGabung.Columns.Add("dimensi_1", typeof(string));
                dtGabung.Columns.Add("dimensi_2", typeof(string));
                dtGabung.Columns.Add("total", typeof(string));

                if (dtCube != null)
                    foreach (DataRow r in dtCube.Rows)
                        dtGabung.Rows.Add("CUBE: Kategori × Jenis PO",
                            r["kategori"].ToString(),
                            r["jenis_po"].ToString(),
                            r["total_barang_terjual"].ToString() + " unit");

                if (dtGrouping != null)
                    foreach (DataRow r in dtGrouping.Rows)
                    {
                        string penjual = r["nama_penjual"] == DBNull.Value ? "-" : r["nama_penjual"].ToString();
                        string kat = r["nama_kategori"] == DBNull.Value ? "-" : r["nama_kategori"].ToString();
                        dtGabung.Rows.Add("GROUPING SETS: Rekap",
                            penjual, kat,
                            r["unit_terjual"].ToString() + " unit");
                    }

                this.dgvLaporan.AutoGenerateColumns = false;
                this.dgvLaporan.Columns.Clear();
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Analisis", HeaderText = "Jenis Analisis", DataPropertyName = "jenis_analisis", Width = 220 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Dim1", HeaderText = "Dimensi 1", DataPropertyName = "dimensi_1", Width = 180 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Dim2", HeaderText = "Dimensi 2", DataPropertyName = "dimensi_2", Width = 180 });
                this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Total",
                    HeaderText = "Total",
                    DataPropertyName = "total",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                this.dgvLaporan.DataSource = dtGabung;
                this.dgvLaporan.ClearSelection();
                this.lblGridTitle.Text = "🧊 Analisis Pasar (CUBE · GROUPING SETS)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat analisis pasar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

                // ✅ TAMBAH: Kartu 3 — Pesanan Berjalan (semua penjual)
                var (totalNilaiAktif, totalPesananAktif) =
                    this._laporanController.GetRingkasanPesananAktifAdmin();

                this.lblPesananAktif.Text = totalPesananAktif + " Pesanan (Rp " + totalNilaiAktif.ToString("N0") + ")";
                this.lblPesananAktifTitle.Text = "PESANAN BERJALAN ⏳";
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
            if (this._dtRaw == null || this._dtRaw.Rows.Count == 0) return;

            var rows = this._dtRaw.AsEnumerable()
                .Where(r => r["total_omzet_bersih"] != DBNull.Value)
                .OrderByDescending(r => Convert.ToInt64(r["total_omzet_bersih"]))
                .Take(10)
                .ToList();

            if (rows.Count == 0) return;

            this.chartPenjualan.Series.Clear();
            this.chartPenjualan.ChartAreas[0].AxisX.CustomLabels.Clear();

            // Pakai Column (vertikal) — jauh lebih stabil dari Bar horizontal di WinForms Chart
            Series series = new Series("Omzet Penjual");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(90, 24, 154);
            series.BorderColor = Color.FromArgb(36, 0, 70);
            series.BorderWidth = 1;

            for (int i = 0; i < rows.Count; i++)
            {
                string nama = rows[i]["nama_penjual"]?.ToString() ?? "-";
                if (nama.Length > 12) nama = nama.Substring(0, 12) + "..";
                long omzet = Convert.ToInt64(rows[i]["total_omzet_bersih"]);

                // Pakai index numerik sebagai X, set AxisLabel secara eksplisit
                int idx = series.Points.AddXY(i + 1, omzet);
                series.Points[idx].AxisLabel = nama;
                series.Points[idx].ToolTip = nama + "\nRp " + omzet.ToString("N0");
            }

            this.chartPenjualan.Series.Add(series);

            var area = this.chartPenjualan.ChartAreas[0];
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 7.5f);
            area.AxisX.LabelStyle.Angle = -30;
            area.AxisX.IsMarginVisible = true;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.LabelStyle.Format = "#,##0";
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 7);

            this.chartPenjualan.Titles.Clear();
            this.chartPenjualan.Titles.Add("Top Omzet Penjual (Rp)");

            this.chartPenjualan.Invalidate();
            this.chartPenjualan.Update();
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

                var (totalNilaiAktif, totalPesananAktif) =
                this._laporanController.GetRingkasanPesananAktif(this._currentUser.IdUser);

                this.lblPesananAktif.Text = totalPesananAktif + " Pesanan (Rp " + totalNilaiAktif.ToString("N0") + ")";
                this.lblPesananAktifTitle.Text = "PESANAN BERJALAN ⏳";

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
            if (this._dtRaw == null || this._dtRaw.Rows.Count == 0) return;

            var grouped = this._dtRaw.AsEnumerable()
                .Where(r => r["tanggal_pesanan"] != DBNull.Value)
                .GroupBy(r => Convert.ToDateTime(r["tanggal_pesanan"]).Date)
                .Select(g => new
                {
                    TanggalAsli = g.Key,
                    Label = g.Key.ToString("dd MMM"),
                    Total = g.Sum(r => r["total_harga"] != DBNull.Value
                                   ? Convert.ToInt64(r["total_harga"]) : 0L)
                })
                .OrderBy(x => x.TanggalAsli)
                .ToList();

            if (grouped.Count == 0) return;

            this.chartPenjualan.Series.Clear();
            this.chartPenjualan.ChartAreas[0].AxisX.CustomLabels.Clear();

            Series series = new Series("Pendapatan Harian");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(200, 182, 255);
            series.BorderColor = Color.FromArgb(36, 0, 70);
            series.BorderWidth = 1;

            for (int i = 0; i < grouped.Count; i++)
            {
                int idx = series.Points.AddXY(i + 1, grouped[i].Total);
                series.Points[idx].AxisLabel = grouped[i].Label;
                series.Points[idx].ToolTip = grouped[i].Label + "\nRp " + grouped[i].Total.ToString("N0");
            }

            this.chartPenjualan.Series.Add(series);

            var area = this.chartPenjualan.ChartAreas[0];
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 7.5f);
            area.AxisX.LabelStyle.Angle = -30;
            area.AxisX.IsMarginVisible = true;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.LabelStyle.Format = "#,##0";
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 7);

            this.chartPenjualan.Titles.Clear();
            this.chartPenjualan.Titles.Add("Pendapatan Harian (Rp)");

            this.chartPenjualan.Invalidate();
            this.chartPenjualan.Update();
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

            // 1. Yang diatur posisinya adalah panel pembungkusnya (pnlChartScroll), bukan chart-nya
            this.pnlChartScroll.Left = this.dgvLaporan.Left + gridW + 16;
            this.pnlChartScroll.Width = innerW - gridW - 16;
            this.pnlChartScroll.Height = this.pnlGrid.Height - this.pnlChartScroll.Top - 20;

            // 2. Buat chart terpasang pas (0,0) memenuhi pnlChartScroll
            this.chartPenjualan.Left = 0;
            this.chartPenjualan.Top = 0;
            this.chartPenjualan.Width = this.pnlChartScroll.Width;
            this.chartPenjualan.Height = this.pnlChartScroll.Height;

            this.btnRefresh.Left = this.pnlGrid.Width - this.btnRefresh.Width - 24;
        }

        // =======================================================
        //  CETAK / EXPORT PDF
        // =======================================================

        // =======================================================
        //  CETAK / EXPORT PDF (MULTI-PAGE PAGINATION)
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
                try
                {
                    // 1. RESET SEMUA INGATAN PAGINATION SEBELUM MULAI CETAK
                    _pdfSectionAdmin = 0; _pdfIndexPerforma = 0; _pdfIndexPengguna = 0; _pdfIndexTerlaris = 0;
                    _pdfSectionPenjual = 0; _pdfIndexLpj = 0;
                    _pdfLpjPoSebelumnya = "";
                    _pdfLpjSubOmzet = 0; _pdfLpjSubRefund = 0; _pdfLpjSubBersih = 0; _pdfLpjSubUnit = 0; _pdfLpjSubUnitPending = 0;

                    printDocument.Print();
                }
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
            Font fontJudulLpj = new Font("Segoe UI Black", 14, FontStyle.Bold);
            Font fontSeksi = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fontIsi = new Font("Segoe UI", 9, FontStyle.Regular);
            Font fontItalic = new Font("Segoe UI", 9, FontStyle.Italic);
            Font fontSub = new Font("Segoe UI", 10, FontStyle.Italic);
            Brush brushHitam = Brushes.Black;
            Brush brushUngu = new SolidBrush(Color.FromArgb(60, 0, 120));
            Brush brushHijau = new SolidBrush(Color.FromArgb(0, 120, 50));
            Brush brushMerah = new SolidBrush(Color.FromArgb(180, 0, 0));
            Pen penTebal = new Pen(Color.FromArgb(60, 0, 120), 2);
            Pen penTipis = new Pen(Color.LightGray, 1);

            int yPos = 50;
            int marginKiri = 50;
            int lebar = 750;
            int batasBawah = e.PageBounds.Height - 120; // Batas mentok sebelum pindah halaman baru

            bool isAdmin = this._currentUser.Peran == "Admin";

            if (isAdmin)
            {
                // =========================================================
                // LOGIKA PDF ADMIN MULTI-PAGE
                // =========================================================
                var stats = this._adminController.GetStatsDashboard();
                DataTable dtPerforma = this._laporanController.GetKlasifikasiPerformaPenjual();
                DataTable dtKeuangan = this._laporanController.GetLaporanKeuanganRollup();
                DataTable dtSultan = this._laporanController.GetSultanMemberIntersect();
                DataTable dtPasif = this._laporanController.GetPenggunaPasifExcept();
                DataTable dtBarang = this._laporanController.GetTotalBarangTerjual(); // Produk Terlaris

                if (_pdfSectionAdmin == 0)
                {
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

                    g.DrawString("A. RINGKASAN STATISTIK SISTEM", fontSeksi, brushUngu, marginKiri, yPos);
                    yPos += 18;
                    string[] labelStat = { "Total Pengguna", "Total Transaksi", "Sesi PO Aktif", "Aduan Masuk" };
                    string[] nilaiStat = {
                        stats.ContainsKey("users") ? stats["users"].ToString() : "0",
                        stats.ContainsKey("transaksi") ? stats["transaksi"].ToString() : "0",
                        stats.ContainsKey("po_aktif") ? stats["po_aktif"].ToString() : "0",
                        stats.ContainsKey("aduan") ? stats["aduan"].ToString() : "0"
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

                    g.DrawString("B. GRAFIK TOP OMZET PENJUAL", fontSeksi, brushUngu, marginKiri, yPos);
                    yPos += 16;
                    this.LoadAdminChart();
                    int chartWA = 700; int chartHA = 280;
                    using (var bmp = new System.Drawing.Bitmap(chartWA, chartHA))
                    {
                        this.chartPenjualan.DrawToBitmap(bmp, new Rectangle(0, 0, chartWA, chartHA));
                        g.DrawImage(bmp, new Rectangle(marginKiri, yPos, lebar, 220));
                    }
                    yPos += 232;
                    g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                    yPos += 14;

                    _pdfSectionAdmin = 1;
                }

                if (_pdfSectionAdmin == 1)
                {
                    if (_pdfIndexPerforma == 0)
                    {
                        g.DrawString("C. KLASIFIKASI PERFORMA PENJUAL", fontSeksi, brushUngu, marginKiri, yPos);
                        yPos += 16;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(200, 182, 255)), marginKiri, yPos, lebar, 20);
                        g.DrawString("Nama Penjual", fontSeksi, brushHitam, marginKiri + 5, yPos + 2);
                        g.DrawString("Omzet Bersih (Rp)", fontSeksi, brushHitam, marginKiri + 280, yPos + 2);
                        g.DrawString("Tier", fontSeksi, brushHitam, marginKiri + 500, yPos + 2);
                        yPos += 20;
                    }

                    if (dtPerforma != null)
                    {
                        for (; _pdfIndexPerforma < dtPerforma.Rows.Count; _pdfIndexPerforma++)
                        {
                            if (yPos > batasBawah) { e.HasMorePages = true; return; }

                            DataRow row = dtPerforma.Rows[_pdfIndexPerforma];
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
                    _pdfSectionAdmin = 2;
                }

                if (_pdfSectionAdmin == 2)
                {
                    if (yPos > batasBawah - 60) { e.HasMorePages = true; return; } // Pastikan judul tidak terpotong

                    int maxBaris = Math.Max(dtSultan?.Rows.Count ?? 0, dtPasif?.Rows.Count ?? 0);

                    if (_pdfIndexPengguna == 0)
                    {
                        g.DrawString("D. ANALISIS PENGGUNA", fontSeksi, brushUngu, marginKiri, yPos);
                        yPos += 16;
                        g.DrawString($"Sultan Member (Penjual yg juga Belanja) ({(dtSultan?.Rows.Count ?? 0)} org):", fontSeksi, brushHijau, marginKiri, yPos);
                        g.DrawString($"Pengguna Pasif/Belum Beli ({(dtPasif?.Rows.Count ?? 0)} org):", fontSeksi, brushMerah, marginKiri + 370, yPos);
                        yPos += 16;
                    }

                    for (; _pdfIndexPengguna < maxBaris; _pdfIndexPengguna++)
                    {
                        if (yPos > batasBawah) { e.HasMorePages = true; return; }

                        if (dtSultan != null && _pdfIndexPengguna < dtSultan.Rows.Count)
                            g.DrawString("• " + dtSultan.Rows[_pdfIndexPengguna]["nama"].ToString(), fontIsi, brushHitam, marginKiri + 5, yPos + 2);
                        if (dtPasif != null && _pdfIndexPengguna < dtPasif.Rows.Count)
                            g.DrawString("• " + dtPasif.Rows[_pdfIndexPengguna]["nama"].ToString(), fontIsi, brushHitam, marginKiri + 375, yPos + 2);
                        yPos += 16;
                    }
                    yPos += 10;
                    g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                    yPos += 14;
                    _pdfSectionAdmin = 3;
                }

                if (_pdfSectionAdmin == 3)
                {
                    if (yPos > batasBawah - 60) { e.HasMorePages = true; return; }

                    if (_pdfIndexTerlaris == 0)
                    {
                        g.DrawString("E. PRODUK TERLARIS (Top 15 Berdasarkan Unit Terjual)", fontSeksi, brushUngu, marginKiri, yPos);
                        yPos += 16;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(200, 182, 255)), marginKiri, yPos, lebar, 20);
                        g.DrawString("Nama Produk", fontSeksi, brushHitam, marginKiri + 5, yPos + 2);
                        g.DrawString("Total Terjual", fontSeksi, brushHitam, marginKiri + 500, yPos + 2);
                        yPos += 20;
                    }

                    if (dtBarang != null)
                    {
                        for (; _pdfIndexTerlaris < dtBarang.Rows.Count; _pdfIndexTerlaris++)
                        {
                            if (_pdfIndexTerlaris >= 15) break; // Batasi cuma cetak top 15 saja
                            if (yPos > batasBawah) { e.HasMorePages = true; return; }

                            DataRow row = dtBarang.Rows[_pdfIndexTerlaris];
                            string produk = row["nama_produk"]?.ToString() ?? "-";
                            string terjual = row["total_terjual"] != DBNull.Value ? row["total_terjual"].ToString() + " unit" : "0 unit";
                            if (produk.Length > 55) produk = produk.Substring(0, 55) + "..";

                            g.DrawString($"{_pdfIndexTerlaris + 1}. {produk}", fontIsi, brushHitam, marginKiri + 5, yPos + 2);
                            g.DrawString(terjual, fontSeksi, brushHijau, marginKiri + 500, yPos + 2);
                            g.DrawLine(penTipis, marginKiri, yPos + 18, marginKiri + lebar, yPos + 18);
                            yPos += 18;
                        }
                    }
                    _pdfSectionAdmin = 4;
                }

                if (_pdfSectionAdmin == 4)
                {
                    if (yPos + 30 > batasBawah) { e.HasMorePages = true; return; }
                    yPos += 30;
                    g.DrawString($"Laporan ini di-generate otomatis oleh Sistem CollabBuy (Halaman Terakhir).", fontSub, Brushes.Gray, marginKiri, yPos);
                    e.HasMorePages = false; // Selesai!
                }
            }
            else
            {
                // =========================================================
                // LOGIKA PDF PENJUAL (LPJ DANUS) MULTI-PAGE
                // =========================================================
                DataTable dtLpj = this._laporanController.GetLpjDanusPerPo(this._currentUser.IdUser);

                long grandOmzetKotor = 0, grandRefund = 0, grandOmzetBersih = 0;
                int grandUnitTerjual = 0;
                int grandUnitPending = 0;
                if (dtLpj != null)
                {
                    foreach (DataRow row in dtLpj.Rows)
                    {
                        grandOmzetKotor += row["omzet_kotor"] != DBNull.Value ? Convert.ToInt64(row["omzet_kotor"]) : 0;
                        grandRefund += row["total_refund_dicairkan"] != DBNull.Value ? Convert.ToInt64(row["total_refund_dicairkan"]) : 0;
                        grandOmzetBersih += row["omzet_bersih_lpj"] != DBNull.Value ? Convert.ToInt64(row["omzet_bersih_lpj"]) : 0;
                        grandUnitTerjual += row["total_barang_terjual"] != DBNull.Value ? Convert.ToInt32(row["total_barang_terjual"]) : 0;
                        grandUnitPending += row["unit_pending"] != DBNull.Value ? Convert.ToInt32(row["unit_pending"]) : 0;
                    }
                }

                string namaToko = this._currentUser.Nama;
                if (this._currentUser is Models.Penjual penjual && !string.IsNullOrWhiteSpace(penjual.NamaToko))
                    namaToko = penjual.NamaToko;

                if (_pdfSectionPenjual == 0)
                {
                    g.DrawString("LAPORAN PERTANGGUNGJAWABAN DANA USAHA (LPJ DANUS)", fontJudulLpj, brushUngu, marginKiri, yPos);
                    yPos += 30;
                    g.DrawString($"Sistem Agregator Dana Usaha Mahasiswa — CollabBuy", fontIsi, Brushes.Gray, marginKiri, yPos);
                    yPos += 20;
                    g.DrawLine(penTebal, marginKiri, yPos, marginKiri + lebar, yPos);
                    yPos += 12;

                    g.DrawString("A. IDENTITAS LAPAK / DANUS", fontSeksi, brushUngu, marginKiri, yPos);
                    yPos += 20;
                    g.DrawString("Nama Lapak / Toko", fontSeksi, brushHitam, marginKiri + 10, yPos);
                    g.DrawString(": " + namaToko, fontIsi, brushHitam, marginKiri + 190, yPos); yPos += 18;
                    g.DrawString("Penanggungjawab", fontSeksi, brushHitam, marginKiri + 10, yPos);
                    g.DrawString(": " + this._currentUser.Nama, fontIsi, brushHitam, marginKiri + 190, yPos); yPos += 18;
                    g.DrawString("Tanggal Cetak", fontSeksi, brushHitam, marginKiri + 10, yPos);
                    g.DrawString(": " + DateTime.Now.ToString("dd MMM yyyy, HH:mm"), fontIsi, brushHitam, marginKiri + 190, yPos); yPos += 18;
                    yPos += 6;
                    g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                    yPos += 12;

                    g.DrawString("B. RINGKASAN EKSEKUTIF", fontSeksi, brushUngu, marginKiri, yPos);
                    yPos += 20;
                    string[] labelRingkasan = { "Total Unit Terjual", "Omzet Kotor", "Total Refund GR", "Omzet Bersih" };
                    string[] nilaiRingkasan = {
                        grandUnitTerjual + " pcs", "Rp " + grandOmzetKotor.ToString("N0"),
                        "Rp " + grandRefund.ToString("N0"), "Rp " + grandOmzetBersih.ToString("N0")
                    };

                    int colW = lebar / 4;
                    for (int i = 0; i < 4; i++)
                    {
                        int x = marginKiri + (i * colW);
                        g.DrawRectangle(new Pen(Color.FromArgb(200, 182, 255), 1), x, yPos, colW - 4, 46);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 255)), x + 1, yPos + 1, colW - 6, 44);
                        g.DrawString(labelRingkasan[i], fontItalic, Brushes.Gray, x + 6, yPos + 4);
                        Brush brushNilai = (i == 3) ? brushHijau : (i == 2 ? brushMerah : brushHitam);
                        g.DrawString(nilaiRingkasan[i], fontSeksi, brushNilai, x + 6, yPos + 24);
                    }
                    yPos += 58;
                    g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                    yPos += 14;

                    _pdfSectionPenjual = 1;
                }

                if (_pdfSectionPenjual == 1)
                {
                    int tinggiBarisProduk = 36; // 2 baris @18px
                    int tinggiSubtotal = 18;

                    if (_pdfIndexLpj == 0)
                    {
                        g.DrawString("C. REALISASI PENJUALAN PER SESI PRE-ORDER", fontSeksi, brushUngu, marginKiri, yPos);
                        yPos += 18;
                    }

                    if (dtLpj != null)
                    {
                        for (; _pdfIndexLpj < dtLpj.Rows.Count; _pdfIndexLpj++)
                        {
                            if (yPos > batasBawah - (tinggiBarisProduk + tinggiSubtotal + 10)) { e.HasMorePages = true; return; }

                            DataRow row = dtLpj.Rows[_pdfIndexLpj];
                            string judulPo = row["judul_po"]?.ToString() ?? "-";
                            string jenisPo = row["jenis_po"]?.ToString() ?? "";
                            string statusPo = row["status_po"]?.ToString() ?? "";
                            string produk = row["nama_produk"]?.ToString() ?? "-";
                            long hargaDsr = row["harga_dasar"] != DBNull.Value ? Convert.ToInt64(row["harga_dasar"]) : 0;
                            int unit = row["total_barang_terjual"] != DBNull.Value ? Convert.ToInt32(row["total_barang_terjual"]) : 0;
                            int unitPending = row["unit_pending"] != DBNull.Value ? Convert.ToInt32(row["unit_pending"]) : 0;
                            long kotor = row["omzet_kotor"] != DBNull.Value ? Convert.ToInt64(row["omzet_kotor"]) : 0;
                            long refund = row["total_refund_dicairkan"] != DBNull.Value ? Convert.ToInt64(row["total_refund_dicairkan"]) : 0;
                            long bersih = row["omzet_bersih_lpj"] != DBNull.Value ? Convert.ToInt64(row["omzet_bersih_lpj"]) : 0;

                            // Header sesi PO baru
                            if (judulPo != _pdfLpjPoSebelumnya)
                            {
                                if (_pdfLpjPoSebelumnya != "")
                                {
                                    if (yPos > batasBawah - tinggiSubtotal) { e.HasMorePages = true; return; }
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(235, 225, 255)), marginKiri, yPos, lebar, tinggiSubtotal);
                                    string ringkasSub = $"Sub-total {_pdfLpjPoSebelumnya}:  " +
                                        $"Unit {_pdfLpjSubUnit} pcs" +
                                        (_pdfLpjSubUnitPending > 0 ? $"  |  Pending {_pdfLpjSubUnitPending} pcs" : "") +
                                        $"  |  Kotor Rp {_pdfLpjSubOmzet:N0}" +
                                        (_pdfLpjSubRefund > 0 ? $"  |  Refund Rp {_pdfLpjSubRefund:N0}" : "") +
                                        $"  |  Bersih Rp {_pdfLpjSubBersih:N0}";
                                    g.DrawString(ringkasSub, fontItalic, brushUngu, marginKiri + 5, yPos + 2);
                                    yPos += tinggiSubtotal;
                                    _pdfLpjSubOmzet = 0; _pdfLpjSubRefund = 0; _pdfLpjSubBersih = 0; _pdfLpjSubUnit = 0; _pdfLpjSubUnitPending = 0;
                                }

                                if (yPos > batasBawah - 18) { e.HasMorePages = true; return; }

                                string judulTampil = judulPo.Length > 40 ? judulPo.Substring(0, 40) + ".." : judulPo;
                                g.FillRectangle(new SolidBrush(Color.FromArgb(240, 235, 255)), marginKiri, yPos, lebar, 18);
                                g.DrawString($"📦 {judulTampil}  [{jenisPo}]  — {statusPo}", fontSeksi, brushUngu, marginKiri + 3, yPos + 2);
                                yPos += 18;
                                _pdfLpjPoSebelumnya = judulPo;
                            }

                            // Baris 1: nama produk (wrap/ellipsis) + harga satuan rata kanan
                            RectangleF rectNama = new RectangleF(marginKiri + 5, yPos, lebar - 180, 16);
                            g.DrawString(produk, fontSeksi, brushHitam, rectNama,
                                new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap });

                            string hargaText = "Rp " + hargaDsr.ToString("N0") + " / unit";
                            SizeF hargaSize = g.MeasureString(hargaText, fontIsi);
                            g.DrawString(hargaText, fontIsi, Brushes.Gray, marginKiri + lebar - hargaSize.Width - 5, yPos + 1);
                            yPos += 17;

                            // Baris 2: grid info 5 kolom
                            int[] colInfo = { marginKiri + 5, marginKiri + 160, marginKiri + 320, marginKiri + 480, marginKiri + 620 };
                            g.DrawString($"Unit: {unit} pcs", fontItalic, brushHitam, colInfo[0], yPos);
                            g.DrawString(unitPending > 0 ? $"Pending: {unitPending} pcs" : "Pending: -", fontItalic, Brushes.Gray, colInfo[1], yPos);
                            g.DrawString($"Kotor: Rp {kotor:N0}", fontItalic, brushHitam, colInfo[2], yPos);
                            g.DrawString(refund > 0 ? $"Refund: Rp {refund:N0}" : "Refund: -", fontItalic, brushMerah, colInfo[3], yPos);
                            g.DrawString($"Bersih: Rp {bersih:N0}", fontItalic, brushHijau, colInfo[4], yPos);
                            yPos += 18;

                            g.DrawLine(penTipis, marginKiri, yPos, marginKiri + lebar, yPos);
                            yPos += 1;

                            _pdfLpjSubUnit += unit; _pdfLpjSubUnitPending += unitPending; _pdfLpjSubOmzet += kotor; _pdfLpjSubRefund += refund; _pdfLpjSubBersih += bersih;
                        }
                    }

                    // Sub-total PO terakhir
                    if (_pdfLpjPoSebelumnya != "")
                    {
                        if (yPos > batasBawah - tinggiSubtotal) { e.HasMorePages = true; return; }
                        g.FillRectangle(new SolidBrush(Color.FromArgb(235, 225, 255)), marginKiri, yPos, lebar, tinggiSubtotal);
                        string ringkasSub = $"Sub-total {_pdfLpjPoSebelumnya}:  " +
                            $"Unit {_pdfLpjSubUnit} pcs" +
                            (_pdfLpjSubUnitPending > 0 ? $"  |  Pending {_pdfLpjSubUnitPending} pcs" : "") +
                            $"  |  Kotor Rp {_pdfLpjSubOmzet:N0}" +
                            (_pdfLpjSubRefund > 0 ? $"  |  Refund Rp {_pdfLpjSubRefund:N0}" : "") +
                            $"  |  Bersih Rp {_pdfLpjSubBersih:N0}";
                        g.DrawString(ringkasSub, fontItalic, brushUngu, marginKiri + 5, yPos + 2);
                        yPos += tinggiSubtotal;
                    }

                    // Grand total
                    int tinggiTotal = 40;
                    if (yPos + tinggiTotal > batasBawah) { e.HasMorePages = true; return; }
                    yPos += 4;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(60, 0, 120)), marginKiri, yPos, lebar, tinggiTotal);
                    g.DrawString("TOTAL KESELURUHAN", fontSeksi, Brushes.White, marginKiri + 5, yPos + 4);

                    string ringkasTotal = $"Unit {grandUnitTerjual} pcs" +
                        (grandUnitPending > 0 ? $"   |   Pending {grandUnitPending} pcs" : "") +
                        $"   |   Kotor Rp {grandOmzetKotor:N0}" +
                        $"   |   Refund Rp {grandRefund:N0}" +
                        $"   |   Bersih Rp {grandOmzetBersih:N0}";
                    g.DrawString(ringkasTotal, fontIsi, Brushes.White, marginKiri + 5, yPos + 21);

                    yPos += tinggiTotal + 4;

                    _pdfSectionPenjual = 2;
                }

                if (_pdfSectionPenjual == 2)
                {
                    if (yPos + 160 > batasBawah) { e.HasMorePages = true; return; }

                    yPos += 14;
                    g.DrawString("D. PERNYATAAN PERTANGGUNGJAWABAN", fontSeksi, brushUngu, marginKiri, yPos);
                    yPos += 18;
                    g.DrawString("Dengan ini saya menyatakan bahwa laporan ini adalah benar dan dapat dipertanggungjawabkan.", fontIsi, brushHitam, marginKiri + 10, yPos);
                    yPos += 30;

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

                    yPos += 40;
                    g.DrawString("Laporan ini di-generate otomatis oleh Sistem CollabBuy.", fontSub, Brushes.Gray, marginKiri, yPos);

                    e.HasMorePages = false; // Selesai mencetak semuanya!
                }
            }
        }
    }
}
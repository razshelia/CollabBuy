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
        private DataTable _dtRaw;

        public AnalitikPenjualanControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._currentUser = currentUser;
            this._laporanController = new LaporanController();

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void AnalitikPenjualanControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataAnalitik();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataAnalitik();
        }

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

        private void SetupDataGridView()
        {
            this.dgvLaporan.AutoGenerateColumns = false;
            this.dgvLaporan.Columns.Clear();

            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pembeli", HeaderText = "Pembeli", DataPropertyName = "nama_pembeli", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Waktu Selesai", DataPropertyName = "tanggal_format", Width = 130 });
            this.dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Cuan (Rp)", DataPropertyName = "total_format", Width = 110 });
        }

        private void LoadDataAnalitik()
        {
            try
            {
                // 1. Load Ringkasan (Cards)
                var (totalPendapatan, totalPesanan) = this._laporanController.GetRingkasanLapak(this._currentUser.GetIdUser());

                this.lblTotalCuan.Text = $"Rp {totalPendapatan:N0}";
                this.lblTotalOrder.Text = totalPesanan.ToString() + " Pesanan";

                // 2. Load Tabel History Cuan
                this._dtRaw = this._laporanController.GetDetailRiwayatCuan(this._currentUser.GetIdUser());

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("nama_pembeli", typeof(string));
                dtUI.Columns.Add("tanggal_format", typeof(string));
                dtUI.Columns.Add("total_format", typeof(string));

                if (this._dtRaw != null)
                {
                    foreach (DataRow row in this._dtRaw.Rows)
                    {
                        string pembeli;
                        if (row["nama_pembeli"] != DBNull.Value)
                        {
                            pembeli = row["nama_pembeli"].ToString();
                        }
                        else
                        {
                            pembeli = "Anonim";
                        }

                        string tanggal;
                        if (row["tanggal_pesanan"] != DBNull.Value)
                        {
                            tanggal = Convert.ToDateTime(row["tanggal_pesanan"]).ToString("dd MMM yyyy");
                        }
                        else
                        {
                            tanggal = "-";
                        }

                        string total;
                        if (row["total_harga"] != DBNull.Value)
                        {
                            total = "Rp " + Convert.ToInt32(row["total_harga"]).ToString("N0");
                        }
                        else
                        {
                            total = "Rp 0";
                        }

                        dtUI.Rows.Add(pembeli, tanggal, total);
                    }
                }
                else
                {
                    bool tabelKosong = true;
                }

                this.dgvLaporan.DataSource = dtUI;
                this.dgvLaporan.ClearSelection();

                // 3. Load Chart 
                this.LoadChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data analitik nih: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChartData()
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
                    .GroupBy(row => row.Field<DateTime>("tanggal_pesanan").ToString("dd MMM"))
                    .Select(g => new {
                        Tanggal = g.Key,
                        Total = g.Sum(row => row.Field<int>("total_harga"))
                    }).Reverse();

                foreach (var item in query)
                {
                    series.Points.AddXY(item.Tanggal, item.Total);
                }
            }
            else
            {
                bool chartKosong = true;
            }

            this.chartPenjualan.Series.Add(series);
            this.chartPenjualan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chartPenjualan.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        // =======================================================
        // FITUR CETAK / EXPORT PDF (NATIVE WINFORMS)
        // =======================================================
        private void btnUnduhPdf_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += new PrintPageEventHandler(this.DrawPdfContent);
            printDialog.Document = printDocument;

            MessageBox.Show("Tips: Pada jendela print yang muncul, pilih printer 'Microsoft Print to PDF' untuk menyimpannya sebagai file PDF ya bestie!", "Info Cetak LPJ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mencetak: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // User menekan Cancel pada dialog
                bool batalCetak = true;
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

            // 1. Gambar Judul & Header Laporan
            g.DrawString("LAPORAN PERTANGGUNGJAWABAN (LPJ) DANUS", fontJudul, brushHitam, marginKiri, yPos);
            yPos += 40;
            g.DrawString($"Nama Lapak/Penjual : {this._currentUser.GetNama()}", fontSub, brushHitam, marginKiri, yPos);
            yPos += 25;
            g.DrawString($"Waktu Cetak Dokumen: {DateTime.Now.ToString("dd MMMM yyyy, HH:mm")}", fontSub, brushHitam, marginKiri, yPos);
            yPos += 30;

            g.DrawLine(penGaris, marginKiri, yPos, 750, yPos); // Garis Pembatas
            yPos += 20;

            // 2. Gambar Ringkasan (Cards)
            g.DrawString($"Total Pesanan Kelar : {this.lblTotalOrder.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 25;
            g.DrawString($"Total Cuan Bersih   : {this.lblTotalCuan.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 30;

            // 3. Render Chart sebagai Gambar ke dalam PDF
            g.DrawString("Grafik Pendapatan Harian:", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 25;
            Rectangle chartRect = new Rectangle(marginKiri, yPos, 650, 250);

            using (Bitmap chartBmp = new Bitmap(this.chartPenjualan.Width, this.chartPenjualan.Height))
            {
                this.chartPenjualan.DrawToBitmap(chartBmp, new Rectangle(0, 0, this.chartPenjualan.Width, this.chartPenjualan.Height));
                g.DrawImage(chartBmp, chartRect);
            }
            yPos += 270;

            // 4. TABEL RINCIAN DARI VIEW (vw_lpj_danus_per_po)
            g.DrawString("Rincian Penjualan per Barang & PO:", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 25;

            // Header Tabel
            g.DrawRectangle(penGaris, marginKiri, yPos, 700, 25);
            g.DrawString("Sesi PO", fontTabelHeader, brushHitam, marginKiri + 5, yPos + 4);
            g.DrawString("Nama Produk", fontTabelHeader, brushHitam, marginKiri + 150, yPos + 4);
            g.DrawString("Terjual", fontTabelHeader, brushHitam, marginKiri + 350, yPos + 4);
            g.DrawString("Refund GR", fontTabelHeader, brushHitam, marginKiri + 450, yPos + 4);
            g.DrawString("Omzet Bersih", fontTabelHeader, brushHitam, marginKiri + 580, yPos + 4);
            yPos += 25;

            // Isi Tabel (Looping Data)
            // PEMANGGILAN AMAN: Mengambil rekapan khusus LPJ
            DataTable dtLpj = this._laporanController.GetLpjDanusPerPo(this._currentUser.GetIdUser());

            if (dtLpj != null && dtLpj.Rows.Count > 0)
            {
                foreach (DataRow row in dtLpj.Rows)
                {
                    string judulPo;
                    if (row.IsNull("judul_po"))
                    {
                        judulPo = "Reguler";
                    }
                    else
                    {
                        judulPo = row["judul_po"].ToString();
                    }

                    string namaProduk;
                    if (row["nama_produk"] != DBNull.Value)
                    {
                        namaProduk = row["nama_produk"].ToString();
                    }
                    else
                    {
                        namaProduk = "-";
                    }

                    // Potong teks jika terlalu panjang agar tidak nabrak kolom sebelahnya
                    if (judulPo.Length > 15)
                    {
                        judulPo = judulPo.Substring(0, 15) + "..";
                    }
                    else
                    {
                        bool judulAman = true;
                    }

                    if (namaProduk.Length > 20)
                    {
                        namaProduk = namaProduk.Substring(0, 20) + "..";
                    }
                    else
                    {
                        bool namaAman = true;
                    }

                    string terjual;
                    if (row["total_barang_terjual"] != DBNull.Value)
                    {
                        terjual = row["total_barang_terjual"].ToString() + " pcs";
                    }
                    else
                    {
                        terjual = "0 pcs";
                    }

                    string refund;
                    if (row["total_refund_dicairkan"] != DBNull.Value)
                    {
                        refund = "Rp " + Convert.ToInt64(row["total_refund_dicairkan"]).ToString("N0");
                    }
                    else
                    {
                        refund = "Rp 0";
                    }

                    string omzet;
                    if (row["omzet_bersih_lpj"] != DBNull.Value)
                    {
                        omzet = "Rp " + Convert.ToInt64(row["omzet_bersih_lpj"]).ToString("N0");
                    }
                    else
                    {
                        omzet = "Rp 0";
                    }

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
                g.DrawString("Belum ada data barang yang selesai terjual.", fontTabelIsi, Brushes.Gray, marginKiri + 5, yPos + 4);
                yPos += 25;
            }

            // 5. Footer Laporan
            yPos += 30;
            g.DrawString("Laporan ini di-generate otomatis dan sah oleh Sistem Danus CollabBuy.", fontSub, Brushes.Gray, marginKiri, yPos);
        }
    }
}
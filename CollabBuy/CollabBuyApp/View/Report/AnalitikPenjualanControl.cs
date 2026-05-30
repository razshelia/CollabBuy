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
        private readonly User _currentUser;
        private readonly LaporanController _laporanController;
        private DataTable _dtRaw; // Simpan data raw untuk print

        public AnalitikPenjualanControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _laporanController = new LaporanController();
        }

        private void AnalitikPenjualanControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataAnalitik();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataAnalitik();
        }

        private void SetupDataGridView()
        {
            dgvLaporan.AutoGenerateColumns = false;
            dgvLaporan.Columns.Clear();

            dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pembeli", HeaderText = "Pembeli", DataPropertyName = "nama_pembeli", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Waktu Selesai", DataPropertyName = "tanggal_format", Width = 130 });
            dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Cuan (Rp)", DataPropertyName = "total_format", Width = 110 });
        }

        private void LoadDataAnalitik()
        {
            try
            {
                // 1. Load Ringkasan (Cards)
                var (totalPendapatan, totalPesanan) = _laporanController.GetRingkasanLapak(_currentUser.GetIdUser());

                lblTotalCuan.Text = $"Rp {totalPendapatan:N0}";
                lblTotalOrder.Text = totalPesanan.ToString() + " Pesanan";

                // 2. Load Tabel History Cuan
                _dtRaw = _laporanController.GetDetailRiwayatCuan(_currentUser.GetIdUser());

                // Format tabel untuk UI
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("nama_pembeli", typeof(string));
                dtUI.Columns.Add("tanggal_format", typeof(string));
                dtUI.Columns.Add("total_format", typeof(string));

                foreach (DataRow row in _dtRaw.Rows)
                {
                    string tanggal = Convert.ToDateTime(row["tanggal_pesanan"]).ToString("dd MMM yyyy");
                    string total = "Rp " + Convert.ToInt32(row["total_harga"]).ToString("N0");

                    dtUI.Rows.Add(row["nama_pembeli"], tanggal, total);
                }

                dgvLaporan.DataSource = dtUI;
                dgvLaporan.ClearSelection();

                // 3. Load Chart (Kelompokkan berdasarkan Tanggal)
                LoadChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data analitik nih: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChartData()
        {
            chartPenjualan.Series.Clear();
            Series series = new Series("Pendapatan Harian");
            series.ChartType = SeriesChartType.Column; // Bisa ganti ke Line atau Bar
            series.Color = Color.FromArgb(200, 182, 255); // Ungu pastel
            series.BorderColor = Color.FromArgb(36, 0, 70);
            series.BorderWidth = 1;

            if (_dtRaw != null && _dtRaw.Rows.Count > 0)
            {
                // Agregasi LINQ sederhana untuk menjumlahkan total harga per hari
                var query = _dtRaw.AsEnumerable()
                    .GroupBy(row => row.Field<DateTime>("tanggal_pesanan").ToString("dd MMM"))
                    .Select(g => new {
                        Tanggal = g.Key,
                        Total = g.Sum(row => row.Field<int>("total_harga"))
                    }).Reverse(); // Reverse biar yang lama di bawah/kiri

                foreach (var item in query)
                {
                    series.Points.AddXY(item.Tanggal, item.Total);
                }
            }

            chartPenjualan.Series.Add(series);
            chartPenjualan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartPenjualan.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        // =======================================================
        // FITUR CETAK / EXPORT PDF (NATIVE WINFORMS)
        // =======================================================
        private void btnUnduhPdf_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += new PrintPageEventHandler(DrawPdfContent);
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
            g.DrawString($"Nama Lapak/Penjual : {_currentUser.GetNama()}", fontSub, brushHitam, marginKiri, yPos);
            yPos += 25;
            g.DrawString($"Waktu Cetak Dokumen: {DateTime.Now.ToString("dd MMMM yyyy, HH:mm")}", fontSub, brushHitam, marginKiri, yPos);
            yPos += 30;

            g.DrawLine(penGaris, marginKiri, yPos, 750, yPos); // Garis Pembatas
            yPos += 20;

            // 2. Gambar Ringkasan (Cards)
            g.DrawString($"Total Pesanan Kelar : {lblTotalOrder.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 25;
            g.DrawString($"Total Cuan Bersih   : {lblTotalCuan.Text}", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 30;

            // 3. Render Chart sebagai Gambar ke dalam PDF
            g.DrawString("Grafik Pendapatan Harian:", fontTabelHeader, brushHitam, marginKiri, yPos);
            yPos += 25;
            Rectangle chartRect = new Rectangle(marginKiri, yPos, 650, 250);
            using (Bitmap chartBmp = new Bitmap(chartPenjualan.Width, chartPenjualan.Height))
            {
                chartPenjualan.DrawToBitmap(chartBmp, new Rectangle(0, 0, chartPenjualan.Width, chartPenjualan.Height));
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
            DataTable dtLpj = _laporanController.GetLpjDanusPerPo(_currentUser.GetIdUser());

            if (dtLpj.Rows.Count > 0)
            {
                foreach (DataRow row in dtLpj.Rows)
                {
                    string judulPo = row.IsNull("judul_po") ? "Reguler" : row["judul_po"].ToString();
                    string namaProduk = row["nama_produk"].ToString();

                    // Potong teks jika terlalu panjang agar tidak nabrak kolom sebelahnya
                    if (judulPo.Length > 15) judulPo = judulPo.Substring(0, 15) + "..";
                    if (namaProduk.Length > 20) namaProduk = namaProduk.Substring(0, 20) + "..";

                    string terjual = row["total_barang_terjual"].ToString() + " pcs";
                    string refund = "Rp " + Convert.ToInt64(row["total_refund_dicairkan"]).ToString("N0");
                    string omzet = "Rp " + Convert.ToInt64(row["omzet_bersih_lpj"]).ToString("N0");

                    g.DrawString(judulPo, fontTabelIsi, brushHitam, marginKiri + 5, yPos + 4);
                    g.DrawString(namaProduk, fontTabelIsi, brushHitam, marginKiri + 150, yPos + 4);
                    g.DrawString(terjual, fontTabelIsi, brushHitam, marginKiri + 350, yPos + 4);
                    g.DrawString(refund, fontTabelIsi, Brushes.Red, marginKiri + 450, yPos + 4); // Refund warna merah
                    g.DrawString(omzet, fontTabelIsi, Brushes.Green, marginKiri + 580, yPos + 4); // Omzet warna hijau

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
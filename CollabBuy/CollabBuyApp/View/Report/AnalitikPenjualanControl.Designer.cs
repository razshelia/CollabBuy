namespace CollabBuy.CollabBuyApp.View.Report
{
    partial class AnalitikPenjualanControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlCuan = new System.Windows.Forms.Panel();
            this.lblTotalCuan = new System.Windows.Forms.Label();
            this.lblCuanTitle = new System.Windows.Forms.Label();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.lblTotalOrder = new System.Windows.Forms.Label();
            this.lblOrderTitle = new System.Windows.Forms.Label();
            this.btnUnduhPdf = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvLaporan = new System.Windows.Forms.DataGridView();
            this.pnlChartScroll = new System.Windows.Forms.Panel();
            this.chartPenjualan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlCuan.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlChartScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPenjualan)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Analitik Cuan Kamu 💸";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Pantau terus pemasukan lapak kamu, biar makin semangat jualan!";

            // pnlCuan — kartu ringkasan kiri
            this.pnlCuan.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);
            this.pnlCuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCuan.Controls.Add(this.lblTotalCuan);
            this.pnlCuan.Controls.Add(this.lblCuanTitle);
            this.pnlCuan.Location = new System.Drawing.Point(36, 100);
            this.pnlCuan.Name = "pnlCuan";
            this.pnlCuan.Size = new System.Drawing.Size(340, 110);
            this.pnlCuan.TabIndex = 2;

            // lblTotalCuan
            this.lblTotalCuan.AutoSize = true;
            this.lblTotalCuan.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalCuan.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTotalCuan.Location = new System.Drawing.Point(15, 45);
            this.lblTotalCuan.Name = "lblTotalCuan";
            this.lblTotalCuan.TabIndex = 1;
            this.lblTotalCuan.Text = "Rp 0";

            // lblCuanTitle
            this.lblCuanTitle.AutoSize = true;
            this.lblCuanTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCuanTitle.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCuanTitle.Location = new System.Drawing.Point(19, 15);
            this.lblCuanTitle.Name = "lblCuanTitle";
            this.lblCuanTitle.TabIndex = 0;
            this.lblCuanTitle.Text = "TOTAL CUAN MASUK 🤑";

            // pnlOrder — kartu ringkasan tengah
            this.pnlOrder.BackColor = System.Drawing.Color.White;
            this.pnlOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrder.Controls.Add(this.lblTotalOrder);
            this.pnlOrder.Controls.Add(this.lblOrderTitle);
            this.pnlOrder.Location = new System.Drawing.Point(390, 100);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(300, 110);
            this.pnlOrder.TabIndex = 3;

            // lblTotalOrder
            this.lblTotalOrder.AutoSize = true;
            this.lblTotalOrder.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrder.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTotalOrder.Location = new System.Drawing.Point(15, 45);
            this.lblTotalOrder.Name = "lblTotalOrder";
            this.lblTotalOrder.TabIndex = 1;
            this.lblTotalOrder.Text = "0";

            // lblOrderTitle
            this.lblOrderTitle.AutoSize = true;
            this.lblOrderTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblOrderTitle.Location = new System.Drawing.Point(19, 15);
            this.lblOrderTitle.Name = "lblOrderTitle";
            this.lblOrderTitle.TabIndex = 0;
            this.lblOrderTitle.Text = "ORDERAN KELAR ✅";

            // btnUnduhPdf — tombol cetak kanan
            this.btnUnduhPdf.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnUnduhPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnduhPdf.FlatAppearance.BorderSize = 0;
            this.btnUnduhPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnduhPdf.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUnduhPdf.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnUnduhPdf.Location = new System.Drawing.Point(710, 100);
            this.btnUnduhPdf.Name = "btnUnduhPdf";
            this.btnUnduhPdf.Size = new System.Drawing.Size(240, 110);
            this.btnUnduhPdf.TabIndex = 5;
            this.btnUnduhPdf.Text = "📄 Unduh Laporan (PDF)";
            this.btnUnduhPdf.UseVisualStyleBackColor = false;
            this.btnUnduhPdf.Click += new System.EventHandler(this.btnUnduhPdf_Click);

            // pnlGrid — panel utama konten (tabel + chart)
            // AutoScroll = true agar chart bisa memanjang ke bawah saat penjual banyak
            this.pnlGrid.AutoScroll = false;
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Controls.Add(this.btnRefresh);
            this.pnlGrid.Controls.Add(this.dgvLaporan);
            this.pnlGrid.Controls.Add(this.pnlChartScroll);
            this.pnlGrid.Location = new System.Drawing.Point(36, 230);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(914, 400);
            this.pnlGrid.TabIndex = 4;

            // lblGridTitle
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblGridTitle.Location = new System.Drawing.Point(20, 20);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.TabIndex = 2;
            this.lblGridTitle.Text = "Riwayat Transaksi Berhasil (Done)";

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Location = new System.Drawing.Point(744, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvLaporan — tabel data di sisi kiri pnlGrid
            this.dgvLaporan.AllowUserToAddRows = false;
            this.dgvLaporan.AllowUserToDeleteRows = false;
            this.dgvLaporan.BackgroundColor = System.Drawing.Color.White;
            this.dgvLaporan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLaporan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLaporan.ColumnHeadersHeight = 40;
            this.dgvLaporan.EnableHeadersVisualStyles = false;
            this.dgvLaporan.Location = new System.Drawing.Point(24, 60);
            this.dgvLaporan.MultiSelect = false;
            this.dgvLaporan.Name = "dgvLaporan";
            this.dgvLaporan.ReadOnly = true;
            this.dgvLaporan.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            this.dgvLaporan.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLaporan.RowTemplate.Height = 40;
            this.dgvLaporan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLaporan.Size = new System.Drawing.Size(430, 330);
            this.dgvLaporan.TabIndex = 0;

            // pnlChartScroll — wrapper scroll untuk chart, di sisi kanan pnlGrid
            // Ukuran tetap, chart di dalamnya yang memanjang secara dinamis dari kode
            this.pnlChartScroll.AutoScroll = true;
            this.pnlChartScroll.Controls.Add(this.chartPenjualan);
            this.pnlChartScroll.Location = new System.Drawing.Point(464, 55);
            this.pnlChartScroll.Name = "pnlChartScroll";
            this.pnlChartScroll.Size = new System.Drawing.Size(432, 335);
            this.pnlChartScroll.TabIndex = 6;

            // chartPenjualan — posisi (0,0) di dalam pnlChartScroll, bukan relatif ke pnlGrid
            chartArea1.Name = "ChartArea1";
            this.chartPenjualan.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPenjualan.Legends.Add(legend1);
            this.chartPenjualan.Location = new System.Drawing.Point(0, 0);
            this.chartPenjualan.Name = "chartPenjualan";
            this.chartPenjualan.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Pendapatan (Rp)";
            this.chartPenjualan.Series.Add(series1);
            this.chartPenjualan.Size = new System.Drawing.Size(430, 330);
            this.chartPenjualan.TabIndex = 3;
            this.chartPenjualan.Text = "Grafik Pendapatan";

            // AnalitikPenjualanControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.btnUnduhPdf);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlOrder);
            this.Controls.Add(this.pnlCuan);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "AnalitikPenjualanControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.AnalitikPenjualanControl_Load);
            this.pnlCuan.ResumeLayout(false);
            this.pnlCuan.PerformLayout();
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.pnlChartScroll.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPenjualan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlCuan;
        private System.Windows.Forms.Label lblTotalCuan;
        private System.Windows.Forms.Label lblCuanTitle;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Label lblTotalOrder;
        private System.Windows.Forms.Label lblOrderTitle;
        private System.Windows.Forms.Button btnUnduhPdf;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvLaporan;
        private System.Windows.Forms.Panel pnlChartScroll;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPenjualan;
    }
}
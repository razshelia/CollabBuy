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
            // ---- instansiasi semua kontrol ----
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDeskripsiTab = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.pnlKartuKPI = new System.Windows.Forms.Panel();
            this.pnlPendapatan = new System.Windows.Forms.Panel();
            this.pnlTransaksi = new System.Windows.Forms.Panel();
            this.pnlProduk = new System.Windows.Forms.Panel();
            this.pnlKuotaKritis = new System.Windows.Forms.Panel();

            this.lblTitlePendapatan = new System.Windows.Forms.Label();
            this.lblValPendapatan = new System.Windows.Forms.Label();
            this.lblTitleTransaksi = new System.Windows.Forms.Label();
            this.lblValTransaksi = new System.Windows.Forms.Label();
            this.lblTitleProduk = new System.Windows.Forms.Label();
            this.lblValProduk = new System.Windows.Forms.Label();
            this.lblTitleKuotaKritis = new System.Windows.Forms.Label();
            this.lblValKuotaKritis = new System.Windows.Forms.Label();

            this.pnlTabBar = new System.Windows.Forms.Panel();
            this.btnTab0 = new System.Windows.Forms.Button();
            this.btnTab1 = new System.Windows.Forms.Button();
            this.btnTab2 = new System.Windows.Forms.Button();
            this.btnTab3 = new System.Windows.Forms.Button();

            this.dgvLaporan = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).BeginInit();
            this.pnlKartuKPI.SuspendLayout();
            this.pnlTabBar.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // lblTitle
            // =============================================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 22);
            this.lblTitle.Text = "Analitik Penjualan";

            // =============================================
            // lblSubtitle
            // =============================================
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 58);
            this.lblSubtitle.Text = "Ringkasan performa, laporan keuangan, dan insight produk.";

            // =============================================
            // btnRefresh
            // =============================================
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Location = new System.Drawing.Point(836, 30);
            this.btnRefresh.Size = new System.Drawing.Size(130, 36);
            this.btnRefresh.Text = "🔄 Perbarui";
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // =============================================
            // KARTU KPI — Panel induk
            // =============================================
            this.pnlKartuKPI.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlKartuKPI.Location = new System.Drawing.Point(30, 82);
            this.pnlKartuKPI.Size = new System.Drawing.Size(940, 110);
            this.pnlKartuKPI.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlPendapatan, this.pnlTransaksi, this.pnlProduk, this.pnlKuotaKritis
            });

            // Helper lokal untuk membuat panel kartu
            BuildKartuPanel(this.pnlPendapatan, this.lblTitlePendapatan, "💰 Total Pendapatan",
                            this.lblValPendapatan, "Rp 0",
                            System.Drawing.Color.FromArgb(225, 215, 255), 0);

            BuildKartuPanel(this.pnlTransaksi, this.lblTitleTransaksi, "📦 Transaksi Selesai",
                            this.lblValTransaksi, "0 Transaksi",
                            System.Drawing.Color.FromArgb(215, 240, 255), 1);

            BuildKartuPanel(this.pnlProduk, this.lblTitleProduk, "🛍 Produk Aktif",
                            this.lblValProduk, "0 Produk",
                            System.Drawing.Color.FromArgb(215, 255, 228), 2);

            BuildKartuPanel(this.pnlKuotaKritis, this.lblTitleKuotaKritis, "⚠ Kuota Kritis",
                            this.lblValKuotaKritis, "0 Produk",
                            System.Drawing.Color.FromArgb(255, 235, 215), 3);

            // =============================================
            // TAB BAR
            // =============================================
            this.pnlTabBar.BackColor = System.Drawing.Color.White;
            this.pnlTabBar.Location = new System.Drawing.Point(30, 205);
            this.pnlTabBar.Size = new System.Drawing.Size(940, 44);
            this.pnlTabBar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnTab0, this.btnTab1, this.btnTab2, this.btnTab3
            });

            BuildTabButton(this.btnTab0, "⚠ Kuota Kritis", 0, true);
            BuildTabButton(this.btnTab1, "📊 Laporan Keuangan", 238, false);
            BuildTabButton(this.btnTab2, "🏆 Top Produk", 476, false);
            BuildTabButton(this.btnTab3, "🥇 Leaderboard", 714, false);

            this.btnTab0.Click += new System.EventHandler(this.btnTab0_Click);
            this.btnTab1.Click += new System.EventHandler(this.btnTab1_Click);
            this.btnTab2.Click += new System.EventHandler(this.btnTab2_Click);
            this.btnTab3.Click += new System.EventHandler(this.btnTab3_Click);

            // =============================================
            // lblDeskripsiTab
            // =============================================
            this.lblDeskripsiTab.AutoSize = true;
            this.lblDeskripsiTab.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDeskripsiTab.ForeColor = System.Drawing.Color.DimGray;
            this.lblDeskripsiTab.Location = new System.Drawing.Point(34, 258);
            this.lblDeskripsiTab.Size = new System.Drawing.Size(800, 18);
            this.lblDeskripsiTab.Text = "";

            // =============================================
            // dgvLaporan
            // =============================================
            this.dgvLaporan.Location = new System.Drawing.Point(30, 282);
            this.dgvLaporan.Size = new System.Drawing.Size(940, 330);
            this.dgvLaporan.AllowUserToAddRows = false;
            this.dgvLaporan.AllowUserToDeleteRows = false;
            this.dgvLaporan.BackgroundColor = System.Drawing.Color.White;
            this.dgvLaporan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLaporan.RowHeadersVisible = false;
            this.dgvLaporan.ReadOnly = true;
            this.dgvLaporan.RowTemplate.Height = 38;
            this.dgvLaporan.AutoGenerateColumns = false;

            // =============================================
            // lblStatus
            // =============================================
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(34, 626);
            this.lblStatus.Text = "Menunggu data...";

            // =============================================
            // AnalitikPenjualanControl
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Size = new System.Drawing.Size(1010, 650);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblSubtitle, this.btnRefresh,
                this.pnlKartuKPI, this.pnlTabBar,
                this.lblDeskripsiTab, this.dgvLaporan, this.lblStatus
            });
            this.Load += new System.EventHandler(this.AnalitikPenjualanControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).EndInit();
            this.pnlKartuKPI.ResumeLayout(false);
            this.pnlTabBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // =============================================
        // HELPER BUILDER — agar InitializeComponent tetap bersih
        // =============================================

        private void BuildKartuPanel(
            System.Windows.Forms.Panel panel,
            System.Windows.Forms.Label lblTitle,
            string titleText,
            System.Windows.Forms.Label lblVal,
            string valDefault,
            System.Drawing.Color bgColor,
            int index)
        {
            int w = 225, gap = 10;
            panel.BackColor = bgColor;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            panel.Size = new System.Drawing.Size(w, 95);
            panel.Location = new System.Drawing.Point(index * (w + gap), 7);

            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            lblTitle.Location = new System.Drawing.Point(12, 12);
            lblTitle.Text = titleText;

            lblVal.AutoSize = true;
            lblVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblVal.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            lblVal.Location = new System.Drawing.Point(12, 38);
            lblVal.Text = valDefault;

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblVal);
        }

        private void BuildTabButton(
            System.Windows.Forms.Button btn,
            string text,
            int x,
            bool active)
        {
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Size = new System.Drawing.Size(228, 38);
            btn.Location = new System.Drawing.Point(x, 3);
            btn.Text = text;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.UseVisualStyleBackColor = false;

            if (active)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
                btn.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
                btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            }
            else
            {
                btn.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
                btn.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            }
        }

        #endregion
    }
}
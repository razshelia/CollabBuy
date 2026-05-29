namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class RiwayatPesananControl
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblJumlahData = new System.Windows.Forms.Label();

            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummarySelesai = new System.Windows.Forms.Label();
            this.lblSummaryProses = new System.Windows.Forms.Label();
            this.lblSummaryTunggu = new System.Windows.Forms.Label();
            this.lblSummaryTotalLbl = new System.Windows.Forms.Label();
            this.lblSummaryTotal = new System.Windows.Forms.Label();

            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.dgvRiwayat = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // lblTitle
            // =============================================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 22);
            this.lblTitle.Text = "Riwayat Pesanan";

            // =============================================
            // lblSubtitle
            // =============================================
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 58);
            this.lblSubtitle.Text = "Pantau seluruh riwayat dan status transaksi Anda.";

            // =============================================
            // PANEL SUMMARY (4 badge)
            // =============================================
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Location = new System.Drawing.Point(30, 85);
            this.pnlSummary.Size = new System.Drawing.Size(940, 52);
            this.pnlSummary.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSummarySelesai, this.lblSummaryProses,
                this.lblSummaryTunggu, this.lblSummaryTotalLbl, this.lblSummaryTotal
            });

            // Badge Selesai
            this.lblSummarySelesai.AutoSize = true;
            this.lblSummarySelesai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummarySelesai.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.lblSummarySelesai.Location = new System.Drawing.Point(16, 14);
            this.lblSummarySelesai.Text = "✔ Selesai: 0";

            // Badge Diproses
            this.lblSummaryProses.AutoSize = true;
            this.lblSummaryProses.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryProses.ForeColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.lblSummaryProses.Location = new System.Drawing.Point(190, 14);
            this.lblSummaryProses.Text = "🕐 Diproses: 0";

            // Badge Menunggu
            this.lblSummaryTunggu.AutoSize = true;
            this.lblSummaryTunggu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTunggu.ForeColor = System.Drawing.Color.FromArgb(210, 150, 0);
            this.lblSummaryTunggu.Location = new System.Drawing.Point(370, 14);
            this.lblSummaryTunggu.Text = "⏳ Menunggu: 0";

            // Total Belanja Label
            this.lblSummaryTotalLbl.AutoSize = true;
            this.lblSummaryTotalLbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSummaryTotalLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblSummaryTotalLbl.Location = new System.Drawing.Point(600, 14);
            this.lblSummaryTotalLbl.Text = "Total Belanja Selesai:";

            // Total Belanja Nilai
            this.lblSummaryTotal.AutoSize = true;
            this.lblSummaryTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTotal.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblSummaryTotal.Location = new System.Drawing.Point(740, 14);
            this.lblSummaryTotal.Text = "Rp 0";

            // =============================================
            // PANEL TOOLBAR (filter + refresh)
            // =============================================
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlToolbar.Location = new System.Drawing.Point(30, 148);
            this.pnlToolbar.Size = new System.Drawing.Size(940, 44);
            this.pnlToolbar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFilterStatus, this.cmbFilterStatus, this.btnRefresh
            });

            // Label filter
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFilterStatus.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblFilterStatus.Location = new System.Drawing.Point(0, 12);
            this.lblFilterStatus.Text = "Filter Status:";

            // ComboBox filter
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbFilterStatus.Location = new System.Drawing.Point(88, 7);
            this.cmbFilterStatus.Size = new System.Drawing.Size(180, 28);
            this.cmbFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);

            // Tombol Refresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Location = new System.Drawing.Point(808, 4);
            this.btnRefresh.Size = new System.Drawing.Size(130, 36);
            this.btnRefresh.Text = "🔄 Perbarui Data";
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // =============================================
            // lblJumlahData
            // =============================================
            this.lblJumlahData.AutoSize = true;
            this.lblJumlahData.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblJumlahData.ForeColor = System.Drawing.Color.DimGray;
            this.lblJumlahData.Location = new System.Drawing.Point(34, 198);
            this.lblJumlahData.Text = "";

            // =============================================
            // dgvRiwayat
            // =============================================
            this.dgvRiwayat.Location = new System.Drawing.Point(30, 218);
            this.dgvRiwayat.Size = new System.Drawing.Size(940, 390);
            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.AllowUserToDeleteRows = false;
            this.dgvRiwayat.BackgroundColor = System.Drawing.Color.White;
            this.dgvRiwayat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRiwayat.RowHeadersVisible = false;
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.AutoGenerateColumns = false;
            this.dgvRiwayat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRiwayat_CellContentClick);

            // =============================================
            // lblStatus
            // =============================================
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(34, 620);
            this.lblStatus.Text = "Menunggu data...";

            // =============================================
            // RiwayatPesananControl
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Size = new System.Drawing.Size(1010, 650);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblSubtitle,
                this.pnlSummary, this.pnlToolbar,
                this.lblJumlahData, this.dgvRiwayat,
                this.lblStatus
            });
            this.Load += new System.EventHandler(this.RiwayatPesananControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblJumlahData;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblSummarySelesai;
        private System.Windows.Forms.Label lblSummaryProses;
        private System.Windows.Forms.Label lblSummaryTunggu;
        private System.Windows.Forms.Label lblSummaryTotalLbl;
        private System.Windows.Forms.Label lblSummaryTotal;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvRiwayat;
    }
}
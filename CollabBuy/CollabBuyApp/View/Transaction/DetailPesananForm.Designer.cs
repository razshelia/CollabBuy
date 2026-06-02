namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class DetailPesananForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvRowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            // ============================================================
            // DEKLARASI KONTROL
            // ============================================================
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblIdTransaksiLabel = new System.Windows.Forms.Label();
            this.lblIdTransaksi = new System.Windows.Forms.Label();

            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblNamaPembeliLabel = new System.Windows.Forms.Label();
            this.lblNamaPembeli = new System.Windows.Forms.Label();
            this.lblTanggalLabel = new System.Windows.Forms.Label();
            this.lblTanggal = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            this.lblRincianTitle = new System.Windows.Forms.Label();
            this.dgvRincian = new System.Windows.Forms.DataGridView();
            this.lblGrandTotal = new System.Windows.Forms.Label();

            this.lblBuktiTitle = new System.Windows.Forms.Label();
            this.pnlBukti = new System.Windows.Forms.Panel();
            this.picBuktiBayar = new System.Windows.Forms.PictureBox();
            this.lblTidakAdaBukti = new System.Windows.Forms.Label();

            this.btnSimpanBukti = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRincian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBuktiBayar)).BeginInit();
            this.pnlBukti.SuspendLayout();
            this.SuspendLayout();

            // ============================================================
            // pnlHeader — strip ungu di atas
            // ============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 70;
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Controls.Add(this.lblIdTransaksiLabel);
            this.pnlHeader.Controls.Add(this.lblIdTransaksi);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.TabIndex = 0;

            // lblFormTitle
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.lblFormTitle.Location = new System.Drawing.Point(20, 15);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Text = "🔍 Detail Pesanan Masuk";
            this.lblFormTitle.TabIndex = 0;

            // lblIdTransaksiLabel
            this.lblIdTransaksiLabel.AutoSize = true;
            this.lblIdTransaksiLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdTransaksiLabel.ForeColor = System.Drawing.Color.FromArgb(200, 182, 255);
            this.lblIdTransaksiLabel.Location = new System.Drawing.Point(22, 48);
            this.lblIdTransaksiLabel.Name = "lblIdTransaksiLabel";
            this.lblIdTransaksiLabel.Text = "No. Transaksi:";
            this.lblIdTransaksiLabel.TabIndex = 1;

            // lblIdTransaksi
            this.lblIdTransaksi.AutoSize = true;
            this.lblIdTransaksi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblIdTransaksi.ForeColor = System.Drawing.Color.White;
            this.lblIdTransaksi.Location = new System.Drawing.Point(120, 48);
            this.lblIdTransaksi.Name = "lblIdTransaksi";
            this.lblIdTransaksi.Text = "-";
            this.lblIdTransaksi.TabIndex = 2;

            // ============================================================
            // pnlInfo — info pembeli, tanggal, status
            // ============================================================
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            this.pnlInfo.Location = new System.Drawing.Point(15, 80);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(750, 65);
            this.pnlInfo.TabIndex = 1;
            this.pnlInfo.Controls.Add(this.lblNamaPembeliLabel);
            this.pnlInfo.Controls.Add(this.lblNamaPembeli);
            this.pnlInfo.Controls.Add(this.lblTanggalLabel);
            this.pnlInfo.Controls.Add(this.lblTanggal);
            this.pnlInfo.Controls.Add(this.lblStatusLabel);
            this.pnlInfo.Controls.Add(this.lblStatus);

            // lblNamaPembeliLabel
            this.lblNamaPembeliLabel.AutoSize = true;
            this.lblNamaPembeliLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNamaPembeliLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblNamaPembeliLabel.Location = new System.Drawing.Point(10, 10);
            this.lblNamaPembeliLabel.Text = "Pembeli";
            this.lblNamaPembeliLabel.TabIndex = 0;

            // lblNamaPembeli
            this.lblNamaPembeli.AutoSize = true;
            this.lblNamaPembeli.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamaPembeli.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNamaPembeli.Location = new System.Drawing.Point(10, 30);
            this.lblNamaPembeli.Name = "lblNamaPembeli";
            this.lblNamaPembeli.Text = "-";
            this.lblNamaPembeli.TabIndex = 1;

            // lblTanggalLabel
            this.lblTanggalLabel.AutoSize = true;
            this.lblTanggalLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTanggalLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblTanggalLabel.Location = new System.Drawing.Point(260, 10);
            this.lblTanggalLabel.Text = "Tanggal Order";
            this.lblTanggalLabel.TabIndex = 2;

            // lblTanggal
            this.lblTanggal.AutoSize = true;
            this.lblTanggal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTanggal.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTanggal.Location = new System.Drawing.Point(260, 30);
            this.lblTanggal.Name = "lblTanggal";
            this.lblTanggal.Text = "-";
            this.lblTanggal.TabIndex = 3;

            // lblStatusLabel
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatusLabel.Location = new System.Drawing.Point(560, 10);
            this.lblStatusLabel.Text = "Status";
            this.lblStatusLabel.TabIndex = 4;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblStatus.Location = new System.Drawing.Point(560, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "-";
            this.lblStatus.TabIndex = 5;

            // ============================================================
            // lblRincianTitle
            // ============================================================
            this.lblRincianTitle.AutoSize = true;
            this.lblRincianTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRincianTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblRincianTitle.Location = new System.Drawing.Point(15, 157);
            this.lblRincianTitle.Name = "lblRincianTitle";
            this.lblRincianTitle.Text = "📋 Rincian Produk Milik Kamu";
            this.lblRincianTitle.TabIndex = 2;

            // ============================================================
            // dgvRincian — tabel item
            // ============================================================
            this.dgvRincian.AllowUserToAddRows = false;
            this.dgvRincian.AllowUserToDeleteRows = false;
            this.dgvRincian.BackgroundColor = System.Drawing.Color.White;
            this.dgvRincian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            dgvHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            dgvHeaderStyle.SelectionForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.dgvRincian.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvRincian.ColumnHeadersHeight = 40;
            this.dgvRincian.EnableHeadersVisualStyles = false;
            dgvRowStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvRowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            dgvRowStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvRowStyle.Padding = new System.Windows.Forms.Padding(3);
            this.dgvRincian.RowsDefaultCellStyle = dgvRowStyle;
            this.dgvRincian.RowTemplate.Height = 40;
            this.dgvRincian.Location = new System.Drawing.Point(15, 182);
            this.dgvRincian.MultiSelect = false;
            this.dgvRincian.Name = "dgvRincian";
            this.dgvRincian.ReadOnly = true;
            this.dgvRincian.RowHeadersVisible = false;
            this.dgvRincian.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRincian.Size = new System.Drawing.Size(750, 180);
            this.dgvRincian.TabIndex = 3;

            // ============================================================
            // lblGrandTotal
            // ============================================================
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblGrandTotal.Location = new System.Drawing.Point(15, 370);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Text = "Total Produk Kamu: Rp 0";
            this.lblGrandTotal.TabIndex = 4;

            // ============================================================
            // lblBuktiTitle
            // ============================================================
            this.lblBuktiTitle.AutoSize = true;
            this.lblBuktiTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBuktiTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblBuktiTitle.Location = new System.Drawing.Point(15, 400);
            this.lblBuktiTitle.Name = "lblBuktiTitle";
            this.lblBuktiTitle.Text = "🧾 Bukti Pembayaran Pembeli";
            this.lblBuktiTitle.TabIndex = 5;

            // ============================================================
            // pnlBukti — area preview bukti bayar
            // ============================================================
            this.pnlBukti.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBukti.Location = new System.Drawing.Point(15, 425);
            this.pnlBukti.Name = "pnlBukti";
            this.pnlBukti.Size = new System.Drawing.Size(750, 260);
            this.pnlBukti.TabIndex = 6;
            this.pnlBukti.Controls.Add(this.picBuktiBayar);
            this.pnlBukti.Controls.Add(this.lblTidakAdaBukti);

            // picBuktiBayar
            this.picBuktiBayar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBuktiBayar.Name = "picBuktiBayar";
            this.picBuktiBayar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBuktiBayar.TabIndex = 0;
            this.picBuktiBayar.TabStop = false;
            this.picBuktiBayar.Visible = false;

            // lblTidakAdaBukti
            this.lblTidakAdaBukti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTidakAdaBukti.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTidakAdaBukti.ForeColor = System.Drawing.Color.Gray;
            this.lblTidakAdaBukti.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTidakAdaBukti.Name = "lblTidakAdaBukti";
            this.lblTidakAdaBukti.Text = "Pembeli belum mengupload bukti pembayaran.";
            this.lblTidakAdaBukti.TabIndex = 1;
            this.lblTidakAdaBukti.Visible = true;

            // ============================================================
            // btnSimpanBukti
            // ============================================================
            this.btnSimpanBukti.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);
            this.btnSimpanBukti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanBukti.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.btnSimpanBukti.FlatAppearance.BorderSize = 2;
            this.btnSimpanBukti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanBukti.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSimpanBukti.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnSimpanBukti.Location = new System.Drawing.Point(15, 695);
            this.btnSimpanBukti.Name = "btnSimpanBukti";
            this.btnSimpanBukti.Size = new System.Drawing.Size(185, 38);
            this.btnSimpanBukti.TabIndex = 7;
            this.btnSimpanBukti.Text = "💾 Simpan Bukti Bayar";
            this.btnSimpanBukti.UseVisualStyleBackColor = false;
            this.btnSimpanBukti.Visible = false;
            this.btnSimpanBukti.Click += new System.EventHandler(this.btnSimpanBukti_Click);

            // ============================================================
            // btnTutup
            // ============================================================
            this.btnTutup.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnTutup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTutup.FlatAppearance.BorderSize = 0;
            this.btnTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTutup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTutup.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnTutup.Location = new System.Drawing.Point(580, 695);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(185, 38);
            this.btnTutup.TabIndex = 8;
            this.btnTutup.Text = "✖ Tutup";
            this.btnTutup.UseVisualStyleBackColor = false;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);

            // ============================================================
            // DetailPesananForm
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(785, 750);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.lblRincianTitle);
            this.Controls.Add(this.dgvRincian);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.lblBuktiTitle);
            this.Controls.Add(this.pnlBukti);
            this.Controls.Add(this.btnSimpanBukti);
            this.Controls.Add(this.btnTutup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailPesananForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detail Pesanan";
            this.Load += new System.EventHandler(this.DetailPesananForm_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRincian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBuktiBayar)).EndInit();
            this.pnlBukti.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        // ============================================================
        // FIELD DEKLARASI
        // ============================================================
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblIdTransaksiLabel;
        private System.Windows.Forms.Label lblIdTransaksi;

        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblNamaPembeliLabel;
        private System.Windows.Forms.Label lblNamaPembeli;
        private System.Windows.Forms.Label lblTanggalLabel;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.Label lblRincianTitle;
        private System.Windows.Forms.DataGridView dgvRincian;
        private System.Windows.Forms.Label lblGrandTotal;

        private System.Windows.Forms.Label lblBuktiTitle;
        private System.Windows.Forms.Panel pnlBukti;
        private System.Windows.Forms.PictureBox picBuktiBayar;
        private System.Windows.Forms.Label lblTidakAdaBukti;

        private System.Windows.Forms.Button btnSimpanBukti;
        private System.Windows.Forms.Button btnTutup;
    }
}
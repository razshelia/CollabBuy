namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class DetailPesananControl
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
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvRowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            // ============================================================
            // DEKLARASI KONTROL
            // ============================================================
            this.pnlOuter = new System.Windows.Forms.Panel();
            this.scrollContent = new System.Windows.Forms.Panel();
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
            this.pnlTombol = new System.Windows.Forms.Panel();
            this.btnSimpanBukti = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.lblCashbackInfo = new System.Windows.Forms.Label();

            this.pnlOuter.SuspendLayout();
            this.scrollContent.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRincian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBuktiBayar)).BeginInit();
            this.pnlBukti.SuspendLayout();
            this.pnlTombol.SuspendLayout();
            this.SuspendLayout();

            // ============================================================
            // pnlOuter — mengisi seluruh UserControl, berisi scrollContent
            // ============================================================
            this.pnlOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOuter.AutoScroll = true;
            this.pnlOuter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlOuter.Controls.Add(this.scrollContent);
            this.pnlOuter.Name = "pnlOuter";
            this.pnlOuter.TabIndex = 0;

            // ============================================================
            // scrollContent — panel konten dengan lebar tetap, tinggi auto
            // ============================================================
            this.scrollContent.AutoSize = true;
            this.scrollContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scrollContent.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.scrollContent.Location = new System.Drawing.Point(0, 0);
            this.scrollContent.Name = "scrollContent";
            this.scrollContent.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.scrollContent.Controls.Add(this.pnlHeader);
            this.scrollContent.Controls.Add(this.pnlInfo);
            this.scrollContent.Controls.Add(this.lblRincianTitle);
            this.scrollContent.Controls.Add(this.dgvRincian);
            this.scrollContent.Controls.Add(this.lblGrandTotal);
            this.scrollContent.Controls.Add(this.lblBuktiTitle);
            this.scrollContent.Controls.Add(this.pnlBukti);
            this.scrollContent.Controls.Add(this.lblCashbackInfo);
            this.scrollContent.Controls.Add(this.pnlTombol);
            this.scrollContent.TabIndex = 0;

            // ============================================================
            // pnlHeader — strip ungu header
            // ============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.pnlHeader.Location = new System.Drawing.Point(30, 20);
            this.pnlHeader.Height = 72;
            this.pnlHeader.Width = 760;
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Controls.Add(this.lblIdTransaksiLabel);
            this.pnlHeader.Controls.Add(this.lblIdTransaksi);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.TabIndex = 0;

            // lblFormTitle
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.lblFormTitle.Location = new System.Drawing.Point(18, 12);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Text = "🔍 Detail Pesanan Masuk";
            this.lblFormTitle.TabIndex = 0;

            // lblIdTransaksiLabel
            this.lblIdTransaksiLabel.AutoSize = true;
            this.lblIdTransaksiLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdTransaksiLabel.ForeColor = System.Drawing.Color.FromArgb(200, 182, 255);
            this.lblIdTransaksiLabel.Location = new System.Drawing.Point(20, 46);
            this.lblIdTransaksiLabel.Name = "lblIdTransaksiLabel";
            this.lblIdTransaksiLabel.Text = "No. Transaksi:";
            this.lblIdTransaksiLabel.TabIndex = 1;

            // lblIdTransaksi
            this.lblIdTransaksi.AutoSize = true;
            this.lblIdTransaksi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblIdTransaksi.ForeColor = System.Drawing.Color.White;
            this.lblIdTransaksi.Location = new System.Drawing.Point(120, 46);
            this.lblIdTransaksi.Name = "lblIdTransaksi";
            this.lblIdTransaksi.Text = "-";
            this.lblIdTransaksi.TabIndex = 2;

            // ============================================================
            // pnlInfo — info pembeli, tanggal, status
            // ============================================================
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            this.pnlInfo.Location = new System.Drawing.Point(30, 102);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(760, 88);   // ← naik dari 68 ke 88
            this.pnlInfo.TabIndex = 1;
            // lblNomorTelepon  ← LETAKNYA DI BAWAH lblNamaPembeli
            this.lblNomorTelepon = new System.Windows.Forms.Label();
            this.lblNomorTelepon.AutoSize = true;
            this.lblNomorTelepon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNomorTelepon.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblNomorTelepon.Location = new System.Drawing.Point(12, 54);  // Y=54, tepat di bawah nama
            this.lblNomorTelepon.Name = "lblNomorTelepon";
            this.lblNomorTelepon.Text = "-";
            this.lblNomorTelepon.TabIndex = 6;

            this.pnlInfo.Controls.Add(this.lblNamaPembeliLabel);
            this.pnlInfo.Controls.Add(this.lblNamaPembeli);
            this.pnlInfo.Controls.Add(this.lblNomorTelepon);        // ← WAJIB ADA DI SINI
            this.pnlInfo.Controls.Add(this.lblTanggalLabel);
            this.pnlInfo.Controls.Add(this.lblTanggal);
            this.pnlInfo.Controls.Add(this.lblStatusLabel);
            this.pnlInfo.Controls.Add(this.lblStatus);

            // lblNamaPembeliLabel
            this.lblNamaPembeliLabel.AutoSize = true;
            this.lblNamaPembeliLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNamaPembeliLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblNamaPembeliLabel.Location = new System.Drawing.Point(12, 10);
            this.lblNamaPembeliLabel.Text = "Pembeli";
            this.lblNamaPembeliLabel.TabIndex = 0;

            // lblNamaPembeli
            this.lblNamaPembeli.AutoSize = true;
            this.lblNamaPembeli.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamaPembeli.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNamaPembeli.Location = new System.Drawing.Point(12, 30);
            this.lblNamaPembeli.Name = "lblNamaPembeli";
            this.lblNamaPembeli.Text = "-";
            this.lblNamaPembeli.TabIndex = 1;

            // lblTanggalLabel
            this.lblTanggalLabel.AutoSize = true;
            this.lblTanggalLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTanggalLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblTanggalLabel.Location = new System.Drawing.Point(270, 10);
            this.lblTanggalLabel.Text = "Tanggal Order";
            this.lblTanggalLabel.TabIndex = 2;

            // lblTanggal
            this.lblTanggal.AutoSize = true;
            this.lblTanggal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTanggal.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTanggal.Location = new System.Drawing.Point(270, 30);
            this.lblTanggal.Name = "lblTanggal";
            this.lblTanggal.Text = "-";
            this.lblTanggal.TabIndex = 3;

            // lblStatusLabel
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatusLabel.Location = new System.Drawing.Point(580, 10);
            this.lblStatusLabel.Text = "Status";
            this.lblStatusLabel.TabIndex = 4;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblStatus.Location = new System.Drawing.Point(580, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "-";
            this.lblStatus.TabIndex = 5;

            // ============================================================
            // lblRincianTitle
            // ============================================================
            this.lblRincianTitle.AutoSize = true;
            this.lblRincianTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRincianTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblRincianTitle.Location = new System.Drawing.Point(30, 204);
            this.lblRincianTitle.Name = "lblRincianTitle";
            this.lblRincianTitle.Text = "📋 Rincian Produk Milik Kamu";
            this.lblRincianTitle.TabIndex = 2;

            // ============================================================
            // dgvRincian
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
            this.dgvRincian.Location = new System.Drawing.Point(30, 230);
            this.dgvRincian.MultiSelect = false;
            this.dgvRincian.Name = "dgvRincian";
            this.dgvRincian.ReadOnly = true;
            this.dgvRincian.RowHeadersVisible = false;
            this.dgvRincian.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvRincian.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRincian.Size = new System.Drawing.Size(760, 185);
            this.dgvRincian.TabIndex = 3;

            // ============================================================
            // lblGrandTotal
            // ============================================================
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblGrandTotal.Location = new System.Drawing.Point(30, 426);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Text = "Total Produk Kamu: Rp 0";
            this.lblGrandTotal.TabIndex = 4;

            this.lblCashbackInfo.AutoSize = false;
            this.lblCashbackInfo.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashbackInfo.ForeColor = System.Drawing.Color.FromArgb(0, 130, 60);
            this.lblCashbackInfo.BackColor = System.Drawing.Color.FromArgb(210, 255, 230);
            this.lblCashbackInfo.Padding = new System.Windows.Forms.Padding(10);
            this.lblCashbackInfo.Size = new System.Drawing.Size(760, 44);
            this.lblCashbackInfo.Location = new System.Drawing.Point(30, 772);
            this.lblCashbackInfo.Text = "";
            this.lblCashbackInfo.Visible = false;

            // ============================================================
            // lblBuktiTitle

            // ============================================================
            // lblBuktiTitle
            // ============================================================
            this.lblBuktiTitle.AutoSize = true;
            this.lblBuktiTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBuktiTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblBuktiTitle.Location = new System.Drawing.Point(30, 458);
            this.lblBuktiTitle.Name = "lblBuktiTitle";
            this.lblBuktiTitle.Text = "🧾 Bukti Pembayaran Pembeli";
            this.lblBuktiTitle.TabIndex = 5;

            // ============================================================
            // pnlBukti — area preview bukti bayar
            // ============================================================
            this.pnlBukti.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBukti.Location = new System.Drawing.Point(30, 484);
            this.pnlBukti.Name = "pnlBukti";
            this.pnlBukti.Size = new System.Drawing.Size(760, 280);
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
            // pnlTombol — panel baris tombol di bawah
            // ============================================================
            this.pnlTombol.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlTombol.Location = new System.Drawing.Point(30, 824);
            this.pnlTombol.Name = "pnlTombol";
            this.pnlTombol.Size = new System.Drawing.Size(760, 52);
            this.pnlTombol.TabIndex = 7;
            this.pnlTombol.Controls.Add(this.btnSimpanBukti);
            this.pnlTombol.Controls.Add(this.btnKembali);

            // btnSimpanBukti
            this.btnSimpanBukti.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);
            this.btnSimpanBukti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanBukti.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.btnSimpanBukti.FlatAppearance.BorderSize = 2;
            this.btnSimpanBukti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanBukti.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSimpanBukti.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnSimpanBukti.Location = new System.Drawing.Point(0, 7);
            this.btnSimpanBukti.Name = "btnSimpanBukti";
            this.btnSimpanBukti.Size = new System.Drawing.Size(190, 38);
            this.btnSimpanBukti.TabIndex = 0;
            this.btnSimpanBukti.Text = "💾 Simpan Bukti Bayar";
            this.btnSimpanBukti.UseVisualStyleBackColor = false;
            this.btnSimpanBukti.Visible = false;
            this.btnSimpanBukti.Click += new System.EventHandler(this.btnSimpanBukti_Click);

            // btnKembali
            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.FlatAppearance.BorderSize = 0;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKembali.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnKembali.Location = new System.Drawing.Point(570, 7);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(190, 38);
            this.btnKembali.TabIndex = 1;
            this.btnKembali.Text = "◀ Kembali ke Pesanan";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);

            // ============================================================
            // DetailPesananControl (UserControl root)
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.pnlOuter);
            this.MinimumSize = new System.Drawing.Size(820, 400);
            this.Name = "DetailPesananControl";
            this.Load += new System.EventHandler(this.DetailPesananControl_Load);

            this.pnlOuter.ResumeLayout(false);
            this.pnlOuter.PerformLayout();
            this.scrollContent.ResumeLayout(false);
            this.scrollContent.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRincian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBuktiBayar)).EndInit();
            this.pnlBukti.ResumeLayout(false);
            this.pnlTombol.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        // ============================================================
        // FIELD DEKLARASI
        // ============================================================
        private System.Windows.Forms.Panel pnlOuter;
        private System.Windows.Forms.Panel scrollContent;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblIdTransaksiLabel;
        private System.Windows.Forms.Label lblIdTransaksi;

        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblNamaPembeliLabel;
        private System.Windows.Forms.Label lblNamaPembeli;
        private System.Windows.Forms.Label lblNomorTelepon;
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

        private System.Windows.Forms.Panel pnlTombol;
        private System.Windows.Forms.Button btnSimpanBukti;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblCashbackInfo;
    }
}

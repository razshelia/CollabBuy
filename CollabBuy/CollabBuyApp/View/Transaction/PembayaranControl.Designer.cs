namespace CollabBuy.CollabBuyApp.View.User
{
    partial class PembayaranControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlScroll = new System.Windows.Forms.Panel();

            // Ringkasan
            this.pnlRingkasan = new System.Windows.Forms.Panel();
            this.lblRingkasanTitle = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();

            // Info rekening
            this.pnlRekening = new System.Windows.Forms.Panel();
            this.lblRekeningTitle = new System.Windows.Forms.Label();
            this.lblRekeningInfo = new System.Windows.Forms.Label();

            // Upload bukti
            this.lblUploadTitle = new System.Windows.Forms.Label();
            this.btnPilihFile = new System.Windows.Forms.Button();
            this.lblNamaFile = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();

            // ID Transaksi (setelah checkout)
            this.lblIdTrxLabel = new System.Windows.Forms.Label();
            this.txtIdTransaksi = new System.Windows.Forms.TextBox();
            this.lblIdTrxHint = new System.Windows.Forms.Label();

            // Tombol
            this.btnBatalKembali = new System.Windows.Forms.Button();
            this.btnKonfirmasiCheckout = new System.Windows.Forms.Button();

            // Status
            this.lblStatus = new System.Windows.Forms.Label();

            // Suspend
            this.pnlHeader.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.pnlRingkasan.SuspendLayout();
            this.pnlRekening.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // pnlHeader
            // ============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 90;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Text = "💳  Pembayaran";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(210, 185, 255);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 55);
            this.lblSubtitle.Text = "Transfer ke rekening penjual, lalu upload bukti pembayaranmu!";

            // ============================================================
            // pnlScroll
            // ============================================================
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlScroll.Controls.Add(this.pnlRingkasan);
            this.pnlScroll.Controls.Add(this.pnlRekening);
            this.pnlScroll.Controls.Add(this.lblUploadTitle);
            this.pnlScroll.Controls.Add(this.btnPilihFile);
            this.pnlScroll.Controls.Add(this.lblNamaFile);
            this.pnlScroll.Controls.Add(this.picPreview);
            this.pnlScroll.Controls.Add(this.lblIdTrxLabel);
            this.pnlScroll.Controls.Add(this.txtIdTransaksi);
            this.pnlScroll.Controls.Add(this.lblIdTrxHint);
            this.pnlScroll.Controls.Add(this.btnBatalKembali);
            this.pnlScroll.Controls.Add(this.btnKonfirmasiCheckout);
            this.pnlScroll.Controls.Add(this.lblStatus);

            // ============================================================
            // pnlRingkasan
            // ============================================================
            this.pnlRingkasan.BackColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.pnlRingkasan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRingkasan.Location = new System.Drawing.Point(30, 20);
            this.pnlRingkasan.Size = new System.Drawing.Size(500, 90);
            this.pnlRingkasan.Controls.Add(this.lblRingkasanTitle);
            this.pnlRingkasan.Controls.Add(this.lblTotalLabel);
            this.pnlRingkasan.Controls.Add(this.lblTotal);

            this.lblRingkasanTitle.AutoSize = true;
            this.lblRingkasanTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblRingkasanTitle.ForeColor = System.Drawing.Color.FromArgb(80, 60, 0);
            this.lblRingkasanTitle.Location = new System.Drawing.Point(14, 10);
            this.lblRingkasanTitle.Text = "📋 Ringkasan Pesanan";

            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(80, 60, 0);
            this.lblTotalLabel.Location = new System.Drawing.Point(14, 40);
            this.lblTotalLabel.Text = "Total yang harus dibayar:";

            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblTotal.Location = new System.Drawing.Point(220, 35);
            this.lblTotal.Text = "Rp 0";

            // ============================================================
            // pnlRekening
            // ============================================================
            this.pnlRekening.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.pnlRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRekening.Location = new System.Drawing.Point(30, 126);
            this.pnlRekening.Size = new System.Drawing.Size(500, 85);
            this.pnlRekening.Controls.Add(this.lblRekeningTitle);
            this.pnlRekening.Controls.Add(this.lblRekeningInfo);

            this.lblRekeningTitle.AutoSize = true;
            this.lblRekeningTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblRekeningTitle.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblRekeningTitle.Location = new System.Drawing.Point(14, 10);
            this.lblRekeningTitle.Text = "🏦 Transfer ke Rekening:";

            this.lblRekeningInfo.AutoSize = false;
            this.lblRekeningInfo.Size = new System.Drawing.Size(460, 50);
            this.lblRekeningInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRekeningInfo.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblRekeningInfo.Location = new System.Drawing.Point(14, 32);
            this.lblRekeningInfo.Text = "Mohon tunggu, info rekening akan tersedia setelah checkout dikonfirmasi...";

            // ============================================================
            // Upload bukti
            // ============================================================
            this.lblUploadTitle.AutoSize = true;
            this.lblUploadTitle.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblUploadTitle.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblUploadTitle.Location = new System.Drawing.Point(30, 228);
            this.lblUploadTitle.Text = "📎 Upload Bukti Transfer";

            this.btnPilihFile.BackColor = System.Drawing.Color.FromArgb(200, 170, 255);
            this.btnPilihFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPilihFile.FlatAppearance.BorderSize = 0;
            this.btnPilihFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPilihFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnPilihFile.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.btnPilihFile.Location = new System.Drawing.Point(30, 258);
            this.btnPilihFile.Size = new System.Drawing.Size(160, 36);
            this.btnPilihFile.Text = "📂 Pilih Gambar...";
            this.btnPilihFile.UseVisualStyleBackColor = false;
            this.btnPilihFile.Click += new System.EventHandler(this.btnPilihFile_Click);

            this.lblNamaFile.AutoSize = true;
            this.lblNamaFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNamaFile.ForeColor = System.Drawing.Color.FromArgb(100, 70, 150);
            this.lblNamaFile.Location = new System.Drawing.Point(205, 268);
            this.lblNamaFile.Text = "(Belum ada file dipilih)";

            this.picPreview.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(30, 305);
            this.picPreview.Size = new System.Drawing.Size(200, 160);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.Visible = false;

            // ============================================================
            // Nomor transaksi (opsional, untuk upload bukti ke transaksi yang sudah ada)
            // ============================================================
            this.lblIdTrxLabel.AutoSize = true;
            this.lblIdTrxLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblIdTrxLabel.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblIdTrxLabel.Location = new System.Drawing.Point(30, 480);
            this.lblIdTrxLabel.Text = "ID Transaksi (setelah checkout):";
            this.lblIdTrxLabel.Visible = false; // Muncul setelah checkout berhasil

            this.txtIdTransaksi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIdTransaksi.Location = new System.Drawing.Point(240, 477);
            this.txtIdTransaksi.Size = new System.Drawing.Size(120, 26);
            this.txtIdTransaksi.ReadOnly = true;
            this.txtIdTransaksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIdTransaksi.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.txtIdTransaksi.Visible = false;

            this.lblIdTrxHint.AutoSize = true;
            this.lblIdTrxHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblIdTrxHint.ForeColor = System.Drawing.Color.FromArgb(130, 100, 180);
            this.lblIdTrxHint.Location = new System.Drawing.Point(30, 510);
            this.lblIdTrxHint.Text = "* Catat ID Transaksi ini untuk keperluan konfirmasi pembayaran.";
            this.lblIdTrxHint.Visible = false;

            // ============================================================
            // Tombol aksi
            // ============================================================
            this.btnBatalKembali.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
            this.btnBatalKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatalKembali.FlatAppearance.BorderSize = 0;
            this.btnBatalKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatalKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBatalKembali.ForeColor = System.Drawing.Color.FromArgb(150, 0, 0);
            this.btnBatalKembali.Location = new System.Drawing.Point(30, 540);
            this.btnBatalKembali.Size = new System.Drawing.Size(170, 48);
            this.btnBatalKembali.Text = "← Kembali";
            this.btnBatalKembali.UseVisualStyleBackColor = false;
            this.btnBatalKembali.Click += new System.EventHandler(this.btnBatalKembali_Click);

            this.btnKonfirmasiCheckout.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnKonfirmasiCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKonfirmasiCheckout.FlatAppearance.BorderSize = 0;
            this.btnKonfirmasiCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKonfirmasiCheckout.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnKonfirmasiCheckout.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnKonfirmasiCheckout.Location = new System.Drawing.Point(220, 540);
            this.btnKonfirmasiCheckout.Size = new System.Drawing.Size(230, 48);
            this.btnKonfirmasiCheckout.Text = "✅ Konfirmasi & Checkout";
            this.btnKonfirmasiCheckout.UseVisualStyleBackColor = false;
            this.btnKonfirmasiCheckout.Click += new System.EventHandler(this.btnKonfirmasiCheckout_Click);

            // ============================================================
            // lblStatus
            // ============================================================
            this.lblStatus.AutoSize = false;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(30, 605);
            this.lblStatus.Size = new System.Drawing.Size(600, 28);
            this.lblStatus.Text = "";
            this.lblStatus.Visible = false;

            // ============================================================
            // PembayaranControl
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.pnlHeader);
            this.Name = "PembayaranControl";
            this.Size = new System.Drawing.Size(980, 700);
            this.Load += new System.EventHandler(this.PembayaranControl_Load);
            this.Resize += new System.EventHandler(this.PembayaranControl_Resize);

            // Resume
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlRingkasan.ResumeLayout(false);
            this.pnlRingkasan.PerformLayout();
            this.pnlRekening.ResumeLayout(false);
            this.pnlRekening.PerformLayout();
            this.pnlScroll.ResumeLayout(false);
            this.pnlScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Panel pnlRingkasan;
        private System.Windows.Forms.Label lblRingkasanTitle;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlRekening;
        private System.Windows.Forms.Label lblRekeningTitle;
        private System.Windows.Forms.Label lblRekeningInfo;
        private System.Windows.Forms.Label lblUploadTitle;
        private System.Windows.Forms.Button btnPilihFile;
        private System.Windows.Forms.Label lblNamaFile;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblIdTrxLabel;
        private System.Windows.Forms.TextBox txtIdTransaksi;
        private System.Windows.Forms.Label lblIdTrxHint;
        private System.Windows.Forms.Button btnBatalKembali;
        private System.Windows.Forms.Button btnKonfirmasiCheckout;
        private System.Windows.Forms.Label lblStatus;
    }
}
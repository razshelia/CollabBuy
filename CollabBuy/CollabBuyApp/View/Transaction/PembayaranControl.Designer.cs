namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class PembayaranControl
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
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblSectionInfo = new System.Windows.Forms.Label();
            this.lblIdTransaksi = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotalBayar = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatusBayar = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblSectionUpload = new System.Windows.Forms.Label();
            this.lblInstruksi = new System.Windows.Forms.Label();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.lblNamaFile = new System.Windows.Forms.Label();
            this.btnPilihBukti = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnNanti = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ────────────────────────────────────────────────
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "💳 Selesaikan Pembayaran";

            // ── lblSubtitle ─────────────────────────────────────────────
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(33, 68);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Pesanan kamu sedang menunggu konfirmasi pembayaran~";

            // ── pnlCard ─────────────────────────────────────────────────
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Location = new System.Drawing.Point(36, 110);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(700, 520);
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.Top |
                                        System.Windows.Forms.AnchorStyles.Left |
                                        System.Windows.Forms.AnchorStyles.Right;
            this.pnlCard.Controls.Add(this.lblSectionInfo);
            this.pnlCard.Controls.Add(this.lblIdTransaksi);
            this.pnlCard.Controls.Add(this.lblTotalLabel);
            this.pnlCard.Controls.Add(this.lblTotalBayar);
            this.pnlCard.Controls.Add(this.lblStatusLabel);
            this.pnlCard.Controls.Add(this.lblStatusBayar);
            this.pnlCard.Controls.Add(this.pnlDivider);
            this.pnlCard.Controls.Add(this.lblSectionUpload);
            this.pnlCard.Controls.Add(this.lblInstruksi);
            this.pnlCard.Controls.Add(this.pbPreview);
            this.pnlCard.Controls.Add(this.lblNamaFile);
            this.pnlCard.Controls.Add(this.btnPilihBukti);
            this.pnlCard.Controls.Add(this.btnUpload);
            this.pnlCard.Controls.Add(this.btnNanti);

            // ── lblSectionInfo ──────────────────────────────────────────
            this.lblSectionInfo.AutoSize = true;
            this.lblSectionInfo.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblSectionInfo.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblSectionInfo.Location = new System.Drawing.Point(30, 25);
            this.lblSectionInfo.Text = "📦 Ringkasan Pesanan";

            // ── lblIdTransaksi ──────────────────────────────────────────
            this.lblIdTransaksi.AutoSize = true;
            this.lblIdTransaksi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblIdTransaksi.ForeColor = System.Drawing.Color.DimGray;
            this.lblIdTransaksi.Location = new System.Drawing.Point(30, 58);
            this.lblIdTransaksi.Name = "lblIdTransaksi";
            this.lblIdTransaksi.Text = "ID Transaksi  :  #...";

            // ── lblTotalLabel ───────────────────────────────────────────
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalLabel.Location = new System.Drawing.Point(30, 90);
            this.lblTotalLabel.Text = "Total Tagihan :";

            // ── lblTotalBayar ───────────────────────────────────────────
            this.lblTotalBayar.AutoSize = true;
            this.lblTotalBayar.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalBayar.ForeColor = System.Drawing.Color.FromArgb(88, 56, 163);
            this.lblTotalBayar.Location = new System.Drawing.Point(30, 115);
            this.lblTotalBayar.Name = "lblTotalBayar";
            this.lblTotalBayar.Text = "Rp 0";

            // ── lblStatusLabel ──────────────────────────────────────────
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatusLabel.Location = new System.Drawing.Point(30, 162);
            this.lblStatusLabel.Text = "Status :";

            // ── lblStatusBayar ──────────────────────────────────────────
            this.lblStatusBayar.AutoSize = true;
            this.lblStatusBayar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusBayar.ForeColor = System.Drawing.Color.FromArgb(200, 120, 0);
            this.lblStatusBayar.Location = new System.Drawing.Point(90, 162);
            this.lblStatusBayar.Name = "lblStatusBayar";
            this.lblStatusBayar.Text = "⏳ Menunggu...";

            // ── pnlDivider ──────────────────────────────────────────────
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(220, 210, 255);
            this.pnlDivider.Location = new System.Drawing.Point(30, 195);
            this.pnlDivider.Size = new System.Drawing.Size(630, 1);

            // ── lblSectionUpload ────────────────────────────────────────
            this.lblSectionUpload.AutoSize = true;
            this.lblSectionUpload.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblSectionUpload.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblSectionUpload.Location = new System.Drawing.Point(30, 210);
            this.lblSectionUpload.Text = "📎 Upload Bukti Pembayaran";

            // ── lblInstruksi ────────────────────────────────────────────
            this.lblInstruksi.AutoSize = false;
            this.lblInstruksi.Width = 420;
            this.lblInstruksi.Height = 55;
            this.lblInstruksi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInstruksi.ForeColor = System.Drawing.Color.DimGray;
            this.lblInstruksi.Location = new System.Drawing.Point(30, 245);
            this.lblInstruksi.Text =
                "Transfer ke rekening BCA 1234-5678-90 a/n CollabBuy HIMATIF.\n" +
                "Setelah transfer, upload screenshot buktinya di sini ya!";

            // ── pbPreview ───────────────────────────────────────────────
            this.pbPreview.BackColor = System.Drawing.Color.FromArgb(240, 235, 255);
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Location = new System.Drawing.Point(30, 310);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(160, 120);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // ── lblNamaFile ─────────────────────────────────────────────
            this.lblNamaFile.AutoSize = false;
            this.lblNamaFile.Width = 260;
            this.lblNamaFile.Height = 20;
            this.lblNamaFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNamaFile.ForeColor = System.Drawing.Color.DimGray;
            this.lblNamaFile.Location = new System.Drawing.Point(205, 310);
            this.lblNamaFile.Name = "lblNamaFile";
            this.lblNamaFile.Text = "Belum ada file dipilih";

            // ── btnPilihBukti ───────────────────────────────────────────
            this.btnPilihBukti.BackColor = System.Drawing.Color.FromArgb(210, 195, 255);
            this.btnPilihBukti.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnPilihBukti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPilihBukti.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(167, 139, 250);
            this.btnPilihBukti.FlatAppearance.BorderSize = 1;
            this.btnPilihBukti.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPilihBukti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPilihBukti.Location = new System.Drawing.Point(205, 338);
            this.btnPilihBukti.Name = "btnPilihBukti";
            this.btnPilihBukti.Size = new System.Drawing.Size(180, 36);
            this.btnPilihBukti.Text = "📁 Pilih Gambar";
            this.btnPilihBukti.Click += new System.EventHandler(this.btnPilihBukti_Click);

            // ── btnUpload ───────────────────────────────────────────────
            this.btnUpload.BackColor = System.Drawing.Color.Gray;
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.FlatAppearance.BorderSize = 0;
            this.btnUpload.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpload.Location = new System.Drawing.Point(205, 385);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(180, 36);
            this.btnUpload.Text = "⬆ Upload Bukti";
            this.btnUpload.Enabled = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);

            // ── btnNanti ────────────────────────────────────────────────
            this.btnNanti.BackColor = System.Drawing.Color.White;
            this.btnNanti.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnNanti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNanti.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnNanti.FlatAppearance.BorderSize = 1;
            this.btnNanti.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNanti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNanti.Location = new System.Drawing.Point(30, 460);
            this.btnNanti.Name = "btnNanti";
            this.btnNanti.Size = new System.Drawing.Size(160, 36);
            this.btnNanti.Text = "← Bayar Nanti";
            this.btnNanti.Click += new System.EventHandler(this.btnNanti_Click);

            // ── PembayaranControl ───────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 244, 255);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "PembayaranControl";
            this.Size = new System.Drawing.Size(800, 700);
            this.Load += new System.EventHandler(this.PembayaranControl_Load);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblSectionInfo;
        private System.Windows.Forms.Label lblIdTransaksi;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalBayar;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatusBayar;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblSectionUpload;
        private System.Windows.Forms.Label lblInstruksi;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Label lblNamaFile;
        private System.Windows.Forms.Button btnPilihBukti;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnNanti;
    }
}
namespace CollabBuy.CollabBuyApp.View.Product
{
    partial class DetailProdukControl
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
            this.btnKembali = new System.Windows.Forms.Button();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.flpThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.pnlHarga = new System.Windows.Forms.Panel();
            this.lblHarga = new System.Windows.Forms.Label();
            this.pnlInfoPo = new System.Windows.Forms.Panel();
            this.lblTipePoLabel = new System.Windows.Forms.Label();
            this.lblTipePoNilai = new System.Windows.Forms.Label();
            this.lblSlotLabel = new System.Windows.Forms.Label();
            this.lblSlotNilai = new System.Windows.Forms.Label();
            this.lblMinOrderLabel = new System.Windows.Forms.Label();
            this.lblMinOrderNilai = new System.Windows.Forms.Label();
            this.lblDeskripsiTitle = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.pnlBeli = new System.Windows.Forms.Panel();
            this.lblQtyLabel = new System.Windows.Forms.Label();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.btnMasukKeranjang = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnLihatKeranjang = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.pnlHarga.SuspendLayout();
            this.pnlInfoPo.SuspendLayout();
            this.pnlBeli.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // pnlHeader
            // ============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlHeader.Controls.Add(this.btnKembali);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;

            this.btnKembali.BackColor = System.Drawing.Color.White;
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnKembali.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnKembali.Location = new System.Drawing.Point(30, 15);
            this.btnKembali.Size = new System.Drawing.Size(100, 30);
            this.btnKembali.Text = "← Kembali";
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);

            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblHeaderTitle.Location = new System.Drawing.Point(150, 16);
            this.lblHeaderTitle.Text = "Loading...";

            // ============================================================
            // picFoto & flpThumbnails
            // ============================================================
            this.picFoto.BackColor = System.Drawing.Color.White;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFoto.Location = new System.Drawing.Point(30, 80);
            this.picFoto.Size = new System.Drawing.Size(300, 300);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.flpThumbnails.AutoScroll = true;
            this.flpThumbnails.Location = new System.Drawing.Point(30, 390);
            this.flpThumbnails.Size = new System.Drawing.Size(300, 80);
            this.flpThumbnails.WrapContents = false;

            // ============================================================
            // lblNamaProduk
            // ============================================================
            this.lblNamaProduk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNamaProduk.Location = new System.Drawing.Point(360, 80);
            this.lblNamaProduk.Size = new System.Drawing.Size(590, 50);
            this.lblNamaProduk.Text = "Nama Produk";

            // ============================================================
            // pnlHarga
            // ============================================================
            this.pnlHarga.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHarga.BackColor = System.Drawing.Color.FromArgb(235, 204, 255); // Soft Purple
            this.pnlHarga.Controls.Add(this.lblHarga);
            this.pnlHarga.Location = new System.Drawing.Point(360, 130);
            this.pnlHarga.Size = new System.Drawing.Size(590, 60);

            this.lblHarga.AutoSize = true;
            this.lblHarga.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblHarga.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblHarga.Location = new System.Drawing.Point(10, 10);
            this.lblHarga.Text = "Rp 0";

            // ============================================================
            // pnlInfoPo
            // ============================================================
            this.pnlInfoPo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfoPo.BackColor = System.Drawing.Color.FromArgb(210, 255, 230); // Soft Green
            this.pnlInfoPo.Controls.Add(this.lblTipePoLabel);
            this.pnlInfoPo.Controls.Add(this.lblTipePoNilai);
            this.pnlInfoPo.Controls.Add(this.lblSlotLabel);
            this.pnlInfoPo.Controls.Add(this.lblSlotNilai);
            this.pnlInfoPo.Controls.Add(this.lblMinOrderLabel);
            this.pnlInfoPo.Controls.Add(this.lblMinOrderNilai);
            this.pnlInfoPo.Location = new System.Drawing.Point(360, 205);
            this.pnlInfoPo.Size = new System.Drawing.Size(590, 80);

            this.lblTipePoLabel.AutoSize = true;
            this.lblTipePoLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipePoLabel.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblTipePoLabel.Location = new System.Drawing.Point(10, 15);
            this.lblTipePoLabel.Text = "Status Barang :";

            this.lblTipePoNilai.AutoSize = true;
            this.lblTipePoNilai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTipePoNilai.Location = new System.Drawing.Point(110, 14);

            this.lblSlotLabel.AutoSize = true;
            this.lblSlotLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSlotLabel.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblSlotLabel.Location = new System.Drawing.Point(10, 45);
            this.lblSlotLabel.Text = "Ketersediaan :";

            this.lblSlotNilai.AutoSize = true;
            this.lblSlotNilai.Font = new System.Drawing.Font("Segoe UI Black", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSlotNilai.Location = new System.Drawing.Point(110, 44);

            this.lblMinOrderLabel.AutoSize = true;
            this.lblMinOrderLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMinOrderLabel.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblMinOrderLabel.Location = new System.Drawing.Point(350, 15);
            this.lblMinOrderLabel.Text = "Min Order :";

            this.lblMinOrderNilai.AutoSize = true;
            this.lblMinOrderNilai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMinOrderNilai.Location = new System.Drawing.Point(430, 14);

            // ============================================================
            // Deskripsi
            // ============================================================
            this.lblDeskripsiTitle.AutoSize = true;
            this.lblDeskripsiTitle.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblDeskripsiTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDeskripsiTitle.Location = new System.Drawing.Point(360, 305);
            this.lblDeskripsiTitle.Text = "📖 Deskripsi Barangnya Nih:";

            this.txtDeskripsi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeskripsi.BackColor = System.Drawing.Color.White;
            this.txtDeskripsi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtDeskripsi.Location = new System.Drawing.Point(360, 335);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.ReadOnly = true;
            this.txtDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDeskripsi.Size = new System.Drawing.Size(590, 135);

            // ============================================================
            // pnlBeli (Form Masukkan Keranjang yang sudah dirampingkan)
            // ============================================================
            this.pnlBeli.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBeli.BackColor = System.Drawing.Color.FromArgb(253, 255, 182); // Soft Yellow
            this.pnlBeli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBeli.Controls.Add(this.lblQtyLabel);
            this.pnlBeli.Controls.Add(this.nudQty);
            this.pnlBeli.Controls.Add(this.btnMasukKeranjang);
            this.pnlBeli.Controls.Add(this.btnLihatKeranjang);
            this.pnlBeli.Location = new System.Drawing.Point(30, 490);
            this.pnlBeli.Size = new System.Drawing.Size(920, 80);

            this.lblQtyLabel.AutoSize = true;
            this.lblQtyLabel.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblQtyLabel.Location = new System.Drawing.Point(20, 28);
            this.lblQtyLabel.Text = "Mau beli berapa pcs?";

            this.nudQty.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.nudQty.Location = new System.Drawing.Point(200, 24);
            this.nudQty.Size = new System.Drawing.Size(100, 32);

            this.btnMasukKeranjang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMasukKeranjang.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnMasukKeranjang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMasukKeranjang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasukKeranjang.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnMasukKeranjang.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnMasukKeranjang.Location = new System.Drawing.Point(670, 16);
            this.btnMasukKeranjang.Size = new System.Drawing.Size(230, 45);
            this.btnMasukKeranjang.Text = "🛒 Sikat Ke Keranjang!";
            this.btnMasukKeranjang.UseVisualStyleBackColor = false;
            this.btnMasukKeranjang.Click += new System.EventHandler(this.btnMasukKeranjang_Click);
            //
            // btnLihatKeranjang
            //
            this.btnLihatKeranjang.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnLihatKeranjang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLihatKeranjang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnLihatKeranjang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLihatKeranjang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLihatKeranjang.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnLihatKeranjang.Name = "btnLihatKeranjang";
            this.btnLihatKeranjang.Text = "🛒 Lihat Keranjang";
            this.btnLihatKeranjang.UseVisualStyleBackColor = false;
            this.btnLihatKeranjang.Click += new System.EventHandler(this.btnLihatKeranjang_Click);

            // ============================================================
            // lblStatus
            // ============================================================
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(30, 590);
            this.lblStatus.Size = new System.Drawing.Size(920, 30);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;

            // ============================================================
            // Setup Utama Control
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlBeli);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.lblDeskripsiTitle);
            this.Controls.Add(this.pnlInfoPo);
            this.Controls.Add(this.pnlHarga);
            this.Controls.Add(this.lblNamaProduk);
            this.Controls.Add(this.flpThumbnails);
            this.Controls.Add(this.picFoto);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DetailProdukControl";
            this.Size = new System.Drawing.Size(980, 650);
            this.Load += new System.EventHandler(this.DetailProdukControl_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.pnlHarga.ResumeLayout(false);
            this.pnlHarga.PerformLayout();
            this.pnlInfoPo.ResumeLayout(false);
            this.pnlInfoPo.PerformLayout();
            this.pnlBeli.ResumeLayout(false);
            this.pnlBeli.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.FlowLayoutPanel flpThumbnails;
        private System.Windows.Forms.Label lblNamaProduk;
        private System.Windows.Forms.Panel pnlHarga;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Panel pnlInfoPo;
        private System.Windows.Forms.Label lblTipePoLabel;
        private System.Windows.Forms.Label lblTipePoNilai;
        private System.Windows.Forms.Label lblSlotLabel;
        private System.Windows.Forms.Label lblSlotNilai;
        private System.Windows.Forms.Label lblMinOrderLabel;
        private System.Windows.Forms.Label lblMinOrderNilai;
        private System.Windows.Forms.Label lblDeskripsiTitle;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Panel pnlBeli;
        private System.Windows.Forms.Label lblQtyLabel;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.Button btnMasukKeranjang;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnLihatKeranjang;
    }
}
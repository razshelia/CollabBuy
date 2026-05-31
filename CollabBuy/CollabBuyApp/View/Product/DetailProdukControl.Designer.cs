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

            this.pnlScroll = new System.Windows.Forms.Panel();

            // Foto produk
            this.picFoto = new System.Windows.Forms.PictureBox();

            // Info produk
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.pnlHarga = new System.Windows.Forms.Panel();
            this.lblHargaLabel = new System.Windows.Forms.Label();
            this.lblHarga = new System.Windows.Forms.Label();

            // Info PO / Slot
            this.pnlInfoPo = new System.Windows.Forms.Panel();
            this.lblInfoPoTitle = new System.Windows.Forms.Label();
            this.lblTipePo = new System.Windows.Forms.Label();
            this.lblSlotLabel = new System.Windows.Forms.Label();
            this.lblSlotNilai = new System.Windows.Forms.Label();
            this.lblMinOrderLabel = new System.Windows.Forms.Label();
            this.lblMinOrder = new System.Windows.Forms.Label();
            this.lblBatasLabel = new System.Windows.Forms.Label();
            this.lblBatas = new System.Windows.Forms.Label();

            // Deskripsi
            this.lblDeskripsiTitle = new System.Windows.Forms.Label();
            this.pnlDeskripsi = new System.Windows.Forms.Panel();
            this.lblDeskripsi = new System.Windows.Forms.Label();

            // Form beli
            this.pnlBeli = new System.Windows.Forms.Panel();
            this.lblQtyLabel = new System.Windows.Forms.Label();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.lblCatatanLabel = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.btnMasukKeranjang = new System.Windows.Forms.Button();

            // Status bar
            this.lblStatus = new System.Windows.Forms.Label();

            // Suspend
            this.pnlHeader.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.pnlHarga.SuspendLayout();
            this.pnlInfoPo.SuspendLayout();
            this.pnlDeskripsi.SuspendLayout();
            this.pnlBeli.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // pnlHeader
            // ============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 68;
            this.pnlHeader.Controls.Add(this.btnKembali);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);

            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.FlatAppearance.BorderSize = 0;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnKembali.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnKembali.Location = new System.Drawing.Point(20, 18);
            this.btnKembali.Size = new System.Drawing.Size(80, 32);
            this.btnKembali.Text = "← Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);

            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.lblHeaderTitle.Location = new System.Drawing.Point(115, 20);
            this.lblHeaderTitle.Text = "Detail Produk";

            // ============================================================
            // pnlScroll — container dengan AutoScroll
            // ============================================================
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);

            // Tambahkan semua komponen ke pnlScroll
            this.pnlScroll.Controls.Add(this.picFoto);
            this.pnlScroll.Controls.Add(this.lblNamaProduk);
            this.pnlScroll.Controls.Add(this.pnlHarga);
            this.pnlScroll.Controls.Add(this.pnlInfoPo);
            this.pnlScroll.Controls.Add(this.lblDeskripsiTitle);
            this.pnlScroll.Controls.Add(this.pnlDeskripsi);
            this.pnlScroll.Controls.Add(this.pnlBeli);
            this.pnlScroll.Controls.Add(this.lblStatus);

            // ============================================================
            // picFoto
            // ============================================================
            this.picFoto.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFoto.Location = new System.Drawing.Point(30, 20);
            this.picFoto.Size = new System.Drawing.Size(220, 220);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // ============================================================
            // lblNamaProduk
            // ============================================================
            this.lblNamaProduk.AutoSize = false;
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblNamaProduk.Location = new System.Drawing.Point(270, 20);
            this.lblNamaProduk.Size = new System.Drawing.Size(600, 50);
            this.lblNamaProduk.Text = "Nama Produk";

            // ============================================================
            // pnlHarga
            // ============================================================
            this.pnlHarga.BackColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.pnlHarga.Location = new System.Drawing.Point(270, 78);
            this.pnlHarga.Size = new System.Drawing.Size(280, 65);
            this.pnlHarga.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlHarga.Controls.Add(this.lblHargaLabel);
            this.pnlHarga.Controls.Add(this.lblHarga);

            this.lblHargaLabel.AutoSize = true;
            this.lblHargaLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHargaLabel.ForeColor = System.Drawing.Color.FromArgb(130, 100, 0);
            this.lblHargaLabel.Location = new System.Drawing.Point(14, 8);
            this.lblHargaLabel.Text = "Harga";

            this.lblHarga.AutoSize = true;
            this.lblHarga.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblHarga.ForeColor = System.Drawing.Color.FromArgb(80, 60, 0);
            this.lblHarga.Location = new System.Drawing.Point(14, 26);
            this.lblHarga.Text = "Rp 0";

            // ============================================================
            // pnlInfoPo
            // ============================================================
            this.pnlInfoPo.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.pnlInfoPo.Location = new System.Drawing.Point(270, 155);
            this.pnlInfoPo.Size = new System.Drawing.Size(500, 85);
            this.pnlInfoPo.Controls.Add(this.lblInfoPoTitle);
            this.pnlInfoPo.Controls.Add(this.lblTipePo);
            this.pnlInfoPo.Controls.Add(this.lblSlotLabel);
            this.pnlInfoPo.Controls.Add(this.lblSlotNilai);
            this.pnlInfoPo.Controls.Add(this.lblMinOrderLabel);
            this.pnlInfoPo.Controls.Add(this.lblMinOrder);
            this.pnlInfoPo.Controls.Add(this.lblBatasLabel);
            this.pnlInfoPo.Controls.Add(this.lblBatas);

            this.lblInfoPoTitle.AutoSize = true;
            this.lblInfoPoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfoPoTitle.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblInfoPoTitle.Location = new System.Drawing.Point(12, 8);
            this.lblInfoPoTitle.Text = "Info Pre-Order:";

            this.lblTipePo.AutoSize = true;
            this.lblTipePo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipePo.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblTipePo.Location = new System.Drawing.Point(12, 26);
            this.lblTipePo.Text = "Tipe: -";

            this.lblSlotLabel.AutoSize = true;
            this.lblSlotLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSlotLabel.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblSlotLabel.Location = new System.Drawing.Point(12, 44);
            this.lblSlotLabel.Text = "Slot Tersedia: -";

            this.lblSlotNilai.AutoSize = true;
            this.lblSlotNilai.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSlotNilai.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblSlotNilai.Location = new System.Drawing.Point(100, 44);
            this.lblSlotNilai.Text = "";

            this.lblMinOrderLabel.AutoSize = true;
            this.lblMinOrderLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMinOrderLabel.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblMinOrderLabel.Location = new System.Drawing.Point(200, 26);
            this.lblMinOrderLabel.Text = "Min Order: -";

            this.lblMinOrder.AutoSize = true;
            this.lblMinOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMinOrder.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblMinOrder.Location = new System.Drawing.Point(280, 26);
            this.lblMinOrder.Text = "";

            this.lblBatasLabel.AutoSize = true;
            this.lblBatasLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBatasLabel.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblBatasLabel.Location = new System.Drawing.Point(200, 44);
            this.lblBatasLabel.Text = "Batas Waktu: -";

            this.lblBatas.AutoSize = true;
            this.lblBatas.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatas.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblBatas.Location = new System.Drawing.Point(296, 44);
            this.lblBatas.Text = "";

            // ============================================================
            // Deskripsi
            // ============================================================
            this.lblDeskripsiTitle.AutoSize = true;
            this.lblDeskripsiTitle.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblDeskripsiTitle.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblDeskripsiTitle.Location = new System.Drawing.Point(30, 260);
            this.lblDeskripsiTitle.Text = "📋 Deskripsi Produk";

            this.pnlDeskripsi.BackColor = System.Drawing.Color.White;
            this.pnlDeskripsi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDeskripsi.Location = new System.Drawing.Point(30, 288);
            this.pnlDeskripsi.Size = new System.Drawing.Size(860, 100);
            this.pnlDeskripsi.Controls.Add(this.lblDeskripsi);

            this.lblDeskripsi.AutoSize = false;
            this.lblDeskripsi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblDeskripsi.Padding = new System.Windows.Forms.Padding(12);
            this.lblDeskripsi.Text = "-";

            // ============================================================
            // pnlBeli — Form tambah ke keranjang
            // ============================================================
            this.pnlBeli.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlBeli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBeli.Location = new System.Drawing.Point(30, 406);
            this.pnlBeli.Size = new System.Drawing.Size(860, 110);
            this.pnlBeli.Controls.Add(this.lblQtyLabel);
            this.pnlBeli.Controls.Add(this.nudQty);
            this.pnlBeli.Controls.Add(this.lblCatatanLabel);
            this.pnlBeli.Controls.Add(this.txtCatatan);
            this.pnlBeli.Controls.Add(this.btnMasukKeranjang);

            this.lblQtyLabel.AutoSize = true;
            this.lblQtyLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblQtyLabel.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblQtyLabel.Location = new System.Drawing.Point(16, 18);
            this.lblQtyLabel.Text = "Jumlah:";

            this.nudQty.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.nudQty.Location = new System.Drawing.Point(80, 14);
            this.nudQty.Minimum = 1;
            this.nudQty.Maximum = 999;
            this.nudQty.Value = 1;
            this.nudQty.Width = 80;

            this.lblCatatanLabel.AutoSize = true;
            this.lblCatatanLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCatatanLabel.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblCatatanLabel.Location = new System.Drawing.Point(16, 60);
            this.lblCatatanLabel.Text = "Catatan (opsional):";

            this.txtCatatan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCatatan.Location = new System.Drawing.Point(150, 57);
            this.txtCatatan.Size = new System.Drawing.Size(350, 24);
            this.txtCatatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.btnMasukKeranjang.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnMasukKeranjang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMasukKeranjang.FlatAppearance.BorderSize = 0;
            this.btnMasukKeranjang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasukKeranjang.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnMasukKeranjang.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnMasukKeranjang.Location = new System.Drawing.Point(540, 25);
            this.btnMasukKeranjang.Size = new System.Drawing.Size(200, 48);
            this.btnMasukKeranjang.Text = "🛒  Masukkan Keranjang";
            this.btnMasukKeranjang.UseVisualStyleBackColor = false;
            this.btnMasukKeranjang.Click += new System.EventHandler(this.btnMasukKeranjang_Click);

            // ============================================================
            // lblStatus
            // ============================================================
            this.lblStatus.AutoSize = false;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(210, 255, 230);
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblStatus.Location = new System.Drawing.Point(30, 530);
            this.lblStatus.Size = new System.Drawing.Size(500, 28);
            this.lblStatus.Text = "";
            this.lblStatus.Visible = false;

            // ============================================================
            // DetailProdukControl
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DetailProdukControl";
            this.Size = new System.Drawing.Size(980, 700);
            this.Load += new System.EventHandler(this.DetailProdukControl_Load);
            this.Resize += new System.EventHandler(this.DetailProdukControl_Resize);

            // Resume
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlHarga.ResumeLayout(false);
            this.pnlHarga.PerformLayout();
            this.pnlInfoPo.ResumeLayout(false);
            this.pnlInfoPo.PerformLayout();
            this.pnlDeskripsi.ResumeLayout(false);
            this.pnlBeli.ResumeLayout(false);
            this.pnlBeli.PerformLayout();
            this.pnlScroll.ResumeLayout(false);
            this.pnlScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Label lblNamaProduk;
        private System.Windows.Forms.Panel pnlHarga;
        private System.Windows.Forms.Label lblHargaLabel;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Panel pnlInfoPo;
        private System.Windows.Forms.Label lblInfoPoTitle;
        private System.Windows.Forms.Label lblTipePo;
        private System.Windows.Forms.Label lblSlotLabel;
        private System.Windows.Forms.Label lblSlotNilai;
        private System.Windows.Forms.Label lblMinOrderLabel;
        private System.Windows.Forms.Label lblMinOrder;
        private System.Windows.Forms.Label lblBatasLabel;
        private System.Windows.Forms.Label lblBatas;
        private System.Windows.Forms.Label lblDeskripsiTitle;
        private System.Windows.Forms.Panel pnlDeskripsi;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.Panel pnlBeli;
        private System.Windows.Forms.Label lblQtyLabel;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.Label lblCatatanLabel;
        private System.Windows.Forms.TextBox txtCatatan;
        private System.Windows.Forms.Button btnMasukKeranjang;
        private System.Windows.Forms.Label lblStatus;
    }
}
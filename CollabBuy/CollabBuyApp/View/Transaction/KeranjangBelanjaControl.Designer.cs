namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class KeranjangBelanjaControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnKosongkan = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTotalText = new System.Windows.Forms.Label();
            this.lblTotalHarga = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.pnlTitipan = new System.Windows.Forms.Panel();
            this.btnTambahTitipan = new System.Windows.Forms.Button();
            this.btnSimpanTitipan = new System.Windows.Forms.Button();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.lblCatatan = new System.Windows.Forms.Label();
            this.txtPenitip = new System.Windows.Forms.TextBox();
            this.lblPenitip = new System.Windows.Forms.Label();
            this.txtProduk = new System.Windows.Forms.TextBox();
            this.lblProduk = new System.Windows.Forms.Label();
            this.lblTitipanTitle = new System.Windows.Forms.Label();
            this.dgvKeranjang = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlTitipan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblInfo);
            this.pnlTop.Controls.Add(this.btnKosongkan);
            this.pnlTop.Controls.Add(this.lblSubtitle);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(30, 20, 30, 10);
            this.pnlTop.Size = new System.Drawing.Size(1000, 120);
            this.pnlTop.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblInfo.Location = new System.Drawing.Point(30, 80);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(940, 30);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Visible = false;
            // 
            // btnKosongkan
            // 
            this.btnKosongkan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKosongkan.BackColor = System.Drawing.Color.White;
            this.btnKosongkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKosongkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKosongkan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKosongkan.ForeColor = System.Drawing.Color.LightCoral;
            this.btnKosongkan.Location = new System.Drawing.Point(810, 25);
            this.btnKosongkan.Name = "btnKosongkan";
            this.btnKosongkan.Size = new System.Drawing.Size(160, 35);
            this.btnKosongkan.TabIndex = 2;
            this.btnKosongkan.Text = "🗑️ Hapus Semua";
            this.btnKosongkan.UseVisualStyleBackColor = false;
            this.btnKosongkan.Click += new System.EventHandler(this.btnKosongkan_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(30, 60);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(434, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Klik baris barang di tabel untuk pisah pesanan / edit nama penitip! ✨";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(341, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Keranjang Jajan Kamu 🛒";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Controls.Add(this.lblTotalText);
            this.pnlSummary.Controls.Add(this.lblTotalHarga);
            this.pnlSummary.Controls.Add(this.btnCheckout);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Location = new System.Drawing.Point(0, 560);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1000, 90);
            this.pnlSummary.TabIndex = 1;
            // 
            // lblTotalText
            // 
            this.lblTotalText.AutoSize = true;
            this.lblTotalText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalText.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalText.Location = new System.Drawing.Point(30, 33);
            this.lblTotalText.Name = "lblTotalText";
            this.lblTotalText.Size = new System.Drawing.Size(101, 21);
            this.lblTotalText.TabIndex = 2;
            this.lblTotalText.Text = "Total Jajan :";
            // 
            // lblTotalHarga
            // 
            this.lblTotalHarga.AutoSize = true;
            this.lblTotalHarga.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalHarga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTotalHarga.Location = new System.Drawing.Point(140, 25);
            this.lblTotalHarga.Name = "lblTotalHarga";
            this.lblTotalHarga.Size = new System.Drawing.Size(65, 32);
            this.lblTotalHarga.TabIndex = 1;
            this.lblTotalHarga.Text = "Rp 0";
            // 
            // btnCheckout
            // 
            this.btnCheckout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnCheckout.Location = new System.Drawing.Point(720, 20);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(250, 50);
            this.btnCheckout.TabIndex = 0;
            this.btnCheckout.Text = "💳 Checkout Sekarang! 🚀";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // pnlTitipan
            // 
            this.pnlTitipan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlTitipan.Controls.Add(this.btnTambahTitipan);
            this.pnlTitipan.Controls.Add(this.btnSimpanTitipan);
            this.pnlTitipan.Controls.Add(this.numQty);
            this.pnlTitipan.Controls.Add(this.lblQty);
            this.pnlTitipan.Controls.Add(this.txtCatatan);
            this.pnlTitipan.Controls.Add(this.lblCatatan);
            this.pnlTitipan.Controls.Add(this.txtPenitip);
            this.pnlTitipan.Controls.Add(this.lblPenitip);
            this.pnlTitipan.Controls.Add(this.txtProduk);
            this.pnlTitipan.Controls.Add(this.lblProduk);
            this.pnlTitipan.Controls.Add(this.lblTitipanTitle);
            this.pnlTitipan.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTitipan.Location = new System.Drawing.Point(650, 120);
            this.pnlTitipan.Name = "pnlTitipan";
            this.pnlTitipan.Padding = new System.Windows.Forms.Padding(15);
            this.pnlTitipan.Size = new System.Drawing.Size(350, 440);
            this.pnlTitipan.TabIndex = 2;
            // 
            // btnTambahTitipan
            // 
            this.btnTambahTitipan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnTambahTitipan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambahTitipan.FlatAppearance.BorderSize = 0;
            this.btnTambahTitipan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahTitipan.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.btnTambahTitipan.ForeColor = System.Drawing.Color.White;
            this.btnTambahTitipan.Location = new System.Drawing.Point(20, 290);
            this.btnTambahTitipan.Name = "btnTambahTitipan";
            this.btnTambahTitipan.Size = new System.Drawing.Size(300, 40);
            this.btnTambahTitipan.TabIndex = 10;
            this.btnTambahTitipan.Text = "➕ Pisah Jadi Titipan Baru";
            this.btnTambahTitipan.UseVisualStyleBackColor = false;
            this.btnTambahTitipan.Click += new System.EventHandler(this.btnTambahTitipan_Click);
            // 
            // btnSimpanTitipan
            // 
            this.btnSimpanTitipan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnSimpanTitipan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanTitipan.FlatAppearance.BorderSize = 0;
            this.btnSimpanTitipan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanTitipan.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.btnSimpanTitipan.Location = new System.Drawing.Point(20, 240);
            this.btnSimpanTitipan.Name = "btnSimpanTitipan";
            this.btnSimpanTitipan.Size = new System.Drawing.Size(300, 40);
            this.btnSimpanTitipan.TabIndex = 9;
            this.btnSimpanTitipan.Text = "💾 Update Titipan Ini";
            this.btnSimpanTitipan.UseVisualStyleBackColor = false;
            this.btnSimpanTitipan.Click += new System.EventHandler(this.btnSimpanTitipan_Click);
            // 
            // numQty
            // 
            this.numQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numQty.Location = new System.Drawing.Point(20, 190);
            this.numQty.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numQty.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(100, 25);
            this.numQty.TabIndex = 8;
            this.numQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.lblQty.Location = new System.Drawing.Point(20, 170);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(76, 15);
            this.lblQty.TabIndex = 7;
            this.lblQty.Text = "Jumlah Pcs:";
            // 
            // txtCatatan
            // 
            this.txtCatatan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCatatan.Location = new System.Drawing.Point(135, 190);
            this.txtCatatan.Name = "txtCatatan";
            this.txtCatatan.Size = new System.Drawing.Size(185, 25);
            this.txtCatatan.TabIndex = 6;
            // 
            // lblCatatan
            // 
            this.lblCatatan.AutoSize = true;
            this.lblCatatan.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.lblCatatan.Location = new System.Drawing.Point(135, 170);
            this.lblCatatan.Name = "lblCatatan";
            this.lblCatatan.Size = new System.Drawing.Size(104, 15);
            this.lblCatatan.TabIndex = 5;
            this.lblCatatan.Text = "Catatan Khusus:";
            // 
            // txtPenitip
            // 
            this.txtPenitip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPenitip.Location = new System.Drawing.Point(20, 135);
            this.txtPenitip.Name = "txtPenitip";
            this.txtPenitip.Size = new System.Drawing.Size(300, 25);
            this.txtPenitip.TabIndex = 4;
            // 
            // lblPenitip
            // 
            this.lblPenitip.AutoSize = true;
            this.lblPenitip.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.lblPenitip.Location = new System.Drawing.Point(20, 115);
            this.lblPenitip.Name = "lblPenitip";
            this.lblPenitip.Size = new System.Drawing.Size(129, 15);
            this.lblPenitip.TabIndex = 3;
            this.lblPenitip.Text = "Atas Nama (Penitip):";
            // 
            // txtProduk
            // 
            this.txtProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProduk.Location = new System.Drawing.Point(20, 80);
            this.txtProduk.Name = "txtProduk";
            this.txtProduk.ReadOnly = true;
            this.txtProduk.Size = new System.Drawing.Size(300, 25);
            this.txtProduk.TabIndex = 2;
            // 
            // lblProduk
            // 
            this.lblProduk.AutoSize = true;
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.lblProduk.Location = new System.Drawing.Point(20, 60);
            this.lblProduk.Name = "lblProduk";
            this.lblProduk.Size = new System.Drawing.Size(53, 15);
            this.lblProduk.TabIndex = 1;
            this.lblProduk.Text = "Barang:";
            // 
            // lblTitipanTitle
            // 
            this.lblTitipanTitle.AutoSize = true;
            this.lblTitipanTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitipanTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitipanTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitipanTitle.Name = "lblTitipanTitle";
            this.lblTitipanTitle.Size = new System.Drawing.Size(229, 25);
            this.lblTitipanTitle.TabIndex = 0;
            this.lblTitipanTitle.Text = "📝 Kelola Titipan Temen";
            // 
            // dgvKeranjang
            // 
            this.dgvKeranjang.AllowUserToAddRows = false;
            this.dgvKeranjang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKeranjang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKeranjang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeranjang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKeranjang.Location = new System.Drawing.Point(0, 120);
            this.dgvKeranjang.Name = "dgvKeranjang";
            this.dgvKeranjang.RowHeadersVisible = false;
            this.dgvKeranjang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKeranjang.Size = new System.Drawing.Size(650, 440);
            this.dgvKeranjang.TabIndex = 3;
            this.dgvKeranjang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKeranjang_CellClick);
            // 
            // KeranjangBelanjaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvKeranjang);
            this.Controls.Add(this.pnlTitipan);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlTop);
            this.Name = "KeranjangBelanjaControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.KeranjangBelanjaControl_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlTitipan.ResumeLayout(false);
            this.pnlTitipan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnKosongkan;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTotalHarga;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Label lblTotalText;
        private System.Windows.Forms.Panel pnlTitipan;
        private System.Windows.Forms.Label lblTitipanTitle;
        private System.Windows.Forms.Label lblProduk;
        private System.Windows.Forms.TextBox txtProduk;
        private System.Windows.Forms.Label lblPenitip;
        private System.Windows.Forms.TextBox txtPenitip;
        private System.Windows.Forms.Label lblCatatan;
        private System.Windows.Forms.TextBox txtCatatan;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Button btnSimpanTitipan;
        private System.Windows.Forms.Button btnTambahTitipan;
        private System.Windows.Forms.DataGridView dgvKeranjang;
    }
}
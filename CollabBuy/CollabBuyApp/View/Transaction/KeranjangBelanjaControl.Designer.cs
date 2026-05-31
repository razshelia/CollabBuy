namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class KeranjangBelanjaControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvKeranjang = new System.Windows.Forms.DataGridView();

            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnKosongkan = new System.Windows.Forms.Button();

            this.lblInfo = new System.Windows.Forms.Label();

            // Suspend
            this.pnlHeader.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).BeginInit();
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
            this.lblTitle.Text = "🛒  Keranjang Belanja";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(210, 185, 255);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 55);
            this.lblSubtitle.Text = "Review pesanan sebelum checkout, ya!";

            // ============================================================
            // pnlGrid + dgvKeranjang
            // ============================================================
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrid.Location = new System.Drawing.Point(30, 106);
            this.pnlGrid.Controls.Add(this.dgvKeranjang);

            headerStyle.BackColor = System.Drawing.Color.FromArgb(200, 170, 255);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            rowStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;

            this.dgvKeranjang.AllowUserToAddRows = false;
            this.dgvKeranjang.AllowUserToDeleteRows = false;
            this.dgvKeranjang.AutoGenerateColumns = false;
            this.dgvKeranjang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKeranjang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKeranjang.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvKeranjang.ColumnHeadersHeight = 38;
            this.dgvKeranjang.DefaultCellStyle = rowStyle;
            this.dgvKeranjang.EnableHeadersVisualStyles = false;
            this.dgvKeranjang.Location = new System.Drawing.Point(2, 2);
            this.dgvKeranjang.ReadOnly = true;
            this.dgvKeranjang.RowHeadersVisible = false;
            this.dgvKeranjang.RowTemplate.Height = 46;
            this.dgvKeranjang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKeranjang.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            System.Windows.Forms.DataGridViewTextBoxColumn colNama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNama.Name = "colNama"; colNama.HeaderText = "Nama Produk";
            colNama.DataPropertyName = "NamaItem";
            colNama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            System.Windows.Forms.DataGridViewTextBoxColumn colPenitip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPenitip.Name = "colPenitip"; colPenitip.HeaderText = "Penitip";
            colPenitip.DataPropertyName = "NamaPenitip"; colPenitip.Width = 130;

            System.Windows.Forms.DataGridViewTextBoxColumn colCatatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCatatan.Name = "colCatatan"; colCatatan.HeaderText = "Catatan";
            colCatatan.DataPropertyName = "Catatan"; colCatatan.Width = 120;

            System.Windows.Forms.DataGridViewTextBoxColumn colHarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colHarga.Name = "colHarga"; colHarga.HeaderText = "Harga Satuan";
            colHarga.DataPropertyName = "HargaDisplay"; colHarga.Width = 115;

            System.Windows.Forms.DataGridViewTextBoxColumn colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQty.Name = "colQty"; colQty.HeaderText = "Qty";
            colQty.DataPropertyName = "Kuantitas"; colQty.Width = 55;

            System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSubtotal.Name = "colSubtotal"; colSubtotal.HeaderText = "Subtotal";
            colSubtotal.DataPropertyName = "SubtotalDisplay"; colSubtotal.Width = 120;

            this.dgvKeranjang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colNama, colPenitip, colCatatan, colHarga, colQty, colSubtotal
            });

            // ============================================================
            // lblInfo — notifikasi inline
            // ============================================================
            this.lblInfo.AutoSize = false;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Location = new System.Drawing.Point(30, 100);
            this.lblInfo.Size = new System.Drawing.Size(500, 0);
            this.lblInfo.Text = "";
            this.lblInfo.Visible = false;

            // ============================================================
            // pnlBottom — ringkasan & tombol
            // ============================================================
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.pnlBottom.Height = 80;
            this.pnlBottom.Controls.Add(this.lblTotalLabel);
            this.pnlBottom.Controls.Add(this.lblTotal);
            this.pnlBottom.Controls.Add(this.btnKosongkan);
            this.pnlBottom.Controls.Add(this.btnHapus);
            this.pnlBottom.Controls.Add(this.btnCheckout);

            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblTotalLabel.Location = new System.Drawing.Point(20, 28);
            this.lblTotalLabel.Text = "Total Tagihan:";

            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.lblTotal.Location = new System.Drawing.Point(148, 24);
            this.lblTotal.Text = "Rp 0";

            this.btnKosongkan.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
            this.btnKosongkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKosongkan.FlatAppearance.BorderSize = 0;
            this.btnKosongkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKosongkan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnKosongkan.ForeColor = System.Drawing.Color.FromArgb(150, 0, 0);
            this.btnKosongkan.Location = new System.Drawing.Point(350, 22);
            this.btnKosongkan.Size = new System.Drawing.Size(130, 36);
            this.btnKosongkan.Text = "🗑 Kosongkan";
            this.btnKosongkan.UseVisualStyleBackColor = false;
            this.btnKosongkan.Click += new System.EventHandler(this.btnKosongkan_Click);

            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(255, 230, 150);
            this.btnHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnHapus.ForeColor = System.Drawing.Color.FromArgb(100, 70, 0);
            this.btnHapus.Location = new System.Drawing.Point(495, 22);
            this.btnHapus.Size = new System.Drawing.Size(140, 36);
            this.btnHapus.Text = "✂️ Hapus Dipilih";
            this.btnHapus.UseVisualStyleBackColor = false;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);

            // ← Tombol Checkout Sekarang mengarah ke halaman PEMBAYARAN, bukan langsung proses
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnCheckout.Location = new System.Drawing.Point(650, 15);
            this.btnCheckout.Size = new System.Drawing.Size(200, 50);
            this.btnCheckout.Text = "Checkout Sekarang →";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);

            // ============================================================
            // KeranjangBelanjaControl
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlBottom);
            this.Name = "KeranjangBelanjaControl";
            this.Size = new System.Drawing.Size(980, 700);
            this.Load += new System.EventHandler(this.KeranjangBelanjaControl_Load);
            this.Resize += new System.EventHandler(this.KeranjangBelanjaControl_Resize);

            // Resume
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvKeranjang;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnKosongkan;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnCheckout;
    }
}
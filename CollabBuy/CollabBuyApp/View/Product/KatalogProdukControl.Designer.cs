namespace CollabBuy.CollabBuyApp.View.User
{
    partial class KatalogProdukControl
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

            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblCari = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();

            this.pnlKatalog = new System.Windows.Forms.Panel();
            this.dgvKatalog = new System.Windows.Forms.DataGridView();

            this.lblInfo = new System.Windows.Forms.Label();

            // Suspend
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlKatalog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKatalog)).BeginInit();
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
            this.lblTitle.Text = "🛍️  Katalog Produk";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(210, 185, 255);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 55);
            this.lblSubtitle.Text = "Temukan produk impianmu di sini, bestie!";

            // ============================================================
            // pnlFilter
            // ============================================================
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlFilter.Height = 68;
            this.pnlFilter.Location = new System.Drawing.Point(0, 90);
            this.pnlFilter.Controls.Add(this.lblCari);
            this.pnlFilter.Controls.Add(this.txtCari);
            this.pnlFilter.Controls.Add(this.btnCari);
            this.pnlFilter.Controls.Add(this.btnReset);

            this.lblCari.AutoSize = true;
            this.lblCari.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCari.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblCari.Location = new System.Drawing.Point(30, 24);
            this.lblCari.Text = "Cari Produk:";

            this.txtCari.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCari.Location = new System.Drawing.Point(120, 20);
            this.txtCari.Size = new System.Drawing.Size(320, 28);
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.BackColor = System.Drawing.Color.White;
            this.txtCari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCari_KeyPress);

            this.btnCari.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnCari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCari.FlatAppearance.BorderSize = 0;
            this.btnCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCari.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCari.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnCari.Location = new System.Drawing.Point(455, 18);
            this.btnCari.Size = new System.Drawing.Size(90, 32);
            this.btnCari.Text = "🔍 Cari";
            this.btnCari.UseVisualStyleBackColor = false;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);

            this.btnReset.BackColor = System.Drawing.Color.FromArgb(200, 170, 255);
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.btnReset.Location = new System.Drawing.Point(556, 18);
            this.btnReset.Size = new System.Drawing.Size(90, 32);
            this.btnReset.Text = "↺ Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // ============================================================
            // pnlKatalog + dgvKatalog
            // ============================================================
            this.pnlKatalog.BackColor = System.Drawing.Color.White;
            this.pnlKatalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKatalog.Location = new System.Drawing.Point(30, 165);
            this.pnlKatalog.Controls.Add(this.dgvKatalog);

            headerStyle.BackColor = System.Drawing.Color.FromArgb(200, 170, 255);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            rowStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;

            this.dgvKatalog.AllowUserToAddRows = false;
            this.dgvKatalog.AllowUserToDeleteRows = false;
            this.dgvKatalog.AutoGenerateColumns = false;
            this.dgvKatalog.BackgroundColor = System.Drawing.Color.White;
            this.dgvKatalog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKatalog.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvKatalog.ColumnHeadersHeight = 38;
            this.dgvKatalog.DefaultCellStyle = rowStyle;
            this.dgvKatalog.EnableHeadersVisualStyles = false;
            this.dgvKatalog.Location = new System.Drawing.Point(2, 2);
            this.dgvKatalog.ReadOnly = true;
            this.dgvKatalog.RowHeadersVisible = false;
            this.dgvKatalog.RowTemplate.Height = 46;
            this.dgvKatalog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKatalog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // Kolom grid
            System.Windows.Forms.DataGridViewTextBoxColumn colNama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNama.Name = "colNama"; colNama.HeaderText = "Nama Produk";
            colNama.DataPropertyName = "nama_produk";
            colNama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            System.Windows.Forms.DataGridViewTextBoxColumn colPenjual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPenjual.Name = "colPenjual"; colPenjual.HeaderText = "Penjual";
            colPenjual.DataPropertyName = "nama_penjual"; colPenjual.Width = 140;

            System.Windows.Forms.DataGridViewTextBoxColumn colHarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colHarga.Name = "colHarga"; colHarga.HeaderText = "Harga";
            colHarga.DataPropertyName = "harga_display"; colHarga.Width = 130;

            System.Windows.Forms.DataGridViewTextBoxColumn colSlot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSlot.Name = "colSlot"; colSlot.HeaderText = "Slot Tersedia";
            colSlot.DataPropertyName = "slot_tersedia"; colSlot.Width = 110;

            System.Windows.Forms.DataGridViewTextBoxColumn colPo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPo.Name = "colPo"; colPo.HeaderText = "Tipe PO";
            colPo.DataPropertyName = "tipe_po"; colPo.Width = 110;

            // Tombol "Lihat Detail" — navigasi ke halaman detail produk
            System.Windows.Forms.DataGridViewButtonColumn colDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            colDetail.Name = "colDetail"; colDetail.HeaderText = "";
            colDetail.Text = "Lihat Detail"; colDetail.UseColumnTextForButtonValue = true;
            colDetail.Width = 100;
            colDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colDetail.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            colDetail.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            colDetail.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            colDetail.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(100, 0, 160);
            colDetail.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            // Tombol "Masukkan Keranjang"
            System.Windows.Forms.DataGridViewButtonColumn colKeranjang = new System.Windows.Forms.DataGridViewButtonColumn();
            colKeranjang.Name = "colKeranjang"; colKeranjang.HeaderText = "";
            colKeranjang.Text = "+ Keranjang"; colKeranjang.UseColumnTextForButtonValue = true;
            colKeranjang.Width = 110;
            colKeranjang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colKeranjang.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(254, 245, 150);
            colKeranjang.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(80, 60, 0);
            colKeranjang.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            colKeranjang.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(240, 220, 80);
            colKeranjang.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(80, 60, 0);

            this.dgvKatalog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colNama, colPenjual, colHarga, colSlot, colPo, colDetail, colKeranjang
            });

            this.dgvKatalog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKatalog_CellClick);

            // ============================================================
            // lblInfo — notifikasi sukses/error inline (tanpa MessageBox)
            // ============================================================
            this.lblInfo.AutoSize = false;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblInfo.BackColor = System.Drawing.Color.FromArgb(210, 255, 230);
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblInfo.Location = new System.Drawing.Point(30, 133);
            this.lblInfo.Size = new System.Drawing.Size(500, 26);
            this.lblInfo.Text = "";
            this.lblInfo.Visible = false;

            // ============================================================
            // KatalogProdukControl
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pnlKatalog);
            this.Name = "KatalogProdukControl";
            this.Size = new System.Drawing.Size(980, 700);
            this.Load += new System.EventHandler(this.KatalogProdukControl_Load);
            this.Resize += new System.EventHandler(this.KatalogProdukControl_Resize);

            // Resume
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlKatalog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKatalog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlKatalog;
        private System.Windows.Forms.DataGridView dgvKatalog;
        private System.Windows.Forms.Label lblInfo;
    }
}
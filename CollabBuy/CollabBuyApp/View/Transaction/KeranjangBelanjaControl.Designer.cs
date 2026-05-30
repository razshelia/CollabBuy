namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class KeranjangBelanjaControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnKosongkan = new System.Windows.Forms.Button();
            this.dgvKeranjang = new System.Windows.Forms.DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTotalText = new System.Windows.Forms.Label();
            this.lblTotalHarga = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();

            // Komponen Titipan
            this.pnlTitipan = new System.Windows.Forms.Panel();
            this.lblTitipanTitle = new System.Windows.Forms.Label();
            this.lblProduk = new System.Windows.Forms.Label();
            this.txtProduk = new System.Windows.Forms.TextBox();
            this.lblPenitip = new System.Windows.Forms.Label();
            this.txtPenitip = new System.Windows.Forms.TextBox();
            this.lblCatatan = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.btnSimpanTitipan = new System.Windows.Forms.Button();
            this.btnTambahTitipan = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.pnlTitipan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Text = "Keranjang Jajan Kamu 🛒";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(35, 65);
            this.lblSubtitle.Text = "Klik baris barang di tabel untuk edit nama temen yang nitip atau pisah pesanan! ✨";

            // btnKosongkan
            this.btnKosongkan.BackColor = System.Drawing.Color.White;
            this.btnKosongkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKosongkan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKosongkan.ForeColor = System.Drawing.Color.LightCoral;
            this.btnKosongkan.Location = new System.Drawing.Point(400, 60);
            this.btnKosongkan.Size = new System.Drawing.Size(160, 35);
            this.btnKosongkan.Text = "🗑️ Hapus Semua";
            this.btnKosongkan.Click += new System.EventHandler(this.btnKosongkan_Click);

            // dgvKeranjang
            this.dgvKeranjang.AllowUserToAddRows = false;
            this.dgvKeranjang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKeranjang.RowHeadersVisible = false;
            this.dgvKeranjang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKeranjang.Location = new System.Drawing.Point(36, 110);
            this.dgvKeranjang.Size = new System.Drawing.Size(550, 360);
            this.dgvKeranjang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKeranjang_CellClick);

            // pnlTitipan (Panel Kanan)
            this.pnlTitipan.BackColor = System.Drawing.Color.FromArgb(235, 204, 255);
            this.pnlTitipan.Location = new System.Drawing.Point(610, 110);
            this.pnlTitipan.Size = new System.Drawing.Size(350, 360);
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

            // Konten pnlTitipan
            this.lblTitipanTitle.AutoSize = true;
            this.lblTitipanTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitipanTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitipanTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitipanTitle.Text = "📝 Kelola Titipan Temen";

            this.lblProduk.Location = new System.Drawing.Point(20, 60);
            this.lblProduk.Text = "Barang:";
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.txtProduk.Location = new System.Drawing.Point(20, 80);
            this.txtProduk.Size = new System.Drawing.Size(300, 25);
            this.txtProduk.ReadOnly = true;

            this.lblPenitip.Location = new System.Drawing.Point(20, 115);
            this.lblPenitip.Text = "Atas Nama (Penitip):";
            this.lblPenitip.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.txtPenitip.Location = new System.Drawing.Point(20, 135);
            this.txtPenitip.Size = new System.Drawing.Size(300, 25);

            this.lblQty.Location = new System.Drawing.Point(20, 170);
            this.lblQty.Text = "Jumlah Pcs:";
            this.lblQty.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.numQty.Location = new System.Drawing.Point(20, 190);
            this.numQty.Size = new System.Drawing.Size(100, 25);

            this.lblCatatan.Location = new System.Drawing.Point(135, 170);
            this.lblCatatan.Text = "Catatan Khusus:";
            this.lblCatatan.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.txtCatatan.Location = new System.Drawing.Point(135, 190);
            this.txtCatatan.Size = new System.Drawing.Size(185, 25);

            this.btnSimpanTitipan.BackColor = System.Drawing.Color.FromArgb(155, 246, 255);
            this.btnSimpanTitipan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanTitipan.FlatAppearance.BorderSize = 0;
            this.btnSimpanTitipan.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.btnSimpanTitipan.Location = new System.Drawing.Point(20, 240);
            this.btnSimpanTitipan.Size = new System.Drawing.Size(300, 40);
            this.btnSimpanTitipan.Text = "💾 Update Titipan Ini";
            this.btnSimpanTitipan.Click += new System.EventHandler(this.btnSimpanTitipan_Click);

            this.btnTambahTitipan.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnTambahTitipan.ForeColor = System.Drawing.Color.White;
            this.btnTambahTitipan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahTitipan.FlatAppearance.BorderSize = 0;
            this.btnTambahTitipan.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            this.btnTambahTitipan.Location = new System.Drawing.Point(20, 290);
            this.btnTambahTitipan.Size = new System.Drawing.Size(300, 40);
            this.btnTambahTitipan.Text = "➕ Pisah Jadi Titipan Baru";
            this.btnTambahTitipan.Click += new System.EventHandler(this.btnTambahTitipan_Click);

            // pnlSummary
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Location = new System.Drawing.Point(36, 490);
            this.pnlSummary.Size = new System.Drawing.Size(924, 90);
            this.pnlSummary.Controls.Add(this.lblTotalText);
            this.pnlSummary.Controls.Add(this.lblTotalHarga);
            this.pnlSummary.Controls.Add(this.btnCheckout);

            this.lblTotalText.AutoSize = true;
            this.lblTotalText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalText.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalText.Location = new System.Drawing.Point(20, 33);
            this.lblTotalText.Text = "Total Jajan :";

            this.lblTotalHarga.AutoSize = true;
            this.lblTotalHarga.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalHarga.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTotalHarga.Location = new System.Drawing.Point(130, 25);
            this.lblTotalHarga.Text = "Rp 0";

            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnCheckout.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.Location = new System.Drawing.Point(650, 20);
            this.btnCheckout.Size = new System.Drawing.Size(250, 50);
            this.btnCheckout.Text = "💳 Checkout Sekarang! 🚀";
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);

            // Setup Utama
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.pnlTitipan);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.dgvKeranjang);
            this.Controls.Add(this.btnKosongkan);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.KeranjangBelanjaControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlTitipan.ResumeLayout(false);
            this.pnlTitipan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblTotalText, lblTotalHarga;
        private System.Windows.Forms.Button btnKosongkan, btnCheckout;
        private System.Windows.Forms.DataGridView dgvKeranjang;
        private System.Windows.Forms.Panel pnlSummary, pnlTitipan;
        private System.Windows.Forms.Label lblTitipanTitle, lblProduk, lblPenitip, lblCatatan, lblQty;
        private System.Windows.Forms.TextBox txtProduk, txtPenitip, txtCatatan;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Button btnSimpanTitipan, btnTambahTitipan;
    }
}
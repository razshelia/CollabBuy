namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerReportControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlRingkasan = new System.Windows.Forms.Panel();
            this.pnlNavigasi = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBarangTerlaris = new System.Windows.Forms.Button();
            this.btnCube = new System.Windows.Forms.Button();
            this.btnOmzetBulanan = new System.Windows.Forms.Button();
            this.btnGroupingSets = new System.Windows.Forms.Button();
            this.btnKuotaMenipis = new System.Windows.Forms.Button();
            this.btnUnion = new System.Windows.Forms.Button();
            this.btnIntersect = new System.Windows.Forms.Button();
            this.btnExcept = new System.Windows.Forms.Button();
            this.dgvLaporan = new System.Windows.Forms.DataGridView();
            this.pnlNavigasi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRingkasan
            // 
            this.pnlRingkasan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlRingkasan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRingkasan.Location = new System.Drawing.Point(0, 0);
            this.pnlRingkasan.Size = new System.Drawing.Size(1046, 100);
            // 
            // pnlNavigasi
            // 
            this.pnlNavigasi.AutoScroll = true; // JAGA-JAGA BIAR BISA SCROLL KALAU LAYAR KECIL
            this.pnlNavigasi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlNavigasi.Width = 240;
            this.pnlNavigasi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavigasi.Controls.Add(this.btnExcept);
            this.pnlNavigasi.Controls.Add(this.btnIntersect);
            this.pnlNavigasi.Controls.Add(this.btnUnion);
            this.pnlNavigasi.Controls.Add(this.btnKuotaMenipis);
            this.pnlNavigasi.Controls.Add(this.btnGroupingSets);
            this.pnlNavigasi.Controls.Add(this.btnOmzetBulanan);
            this.pnlNavigasi.Controls.Add(this.btnCube);
            this.pnlNavigasi.Controls.Add(this.btnBarangTerlaris);
            this.pnlNavigasi.Controls.Add(this.btnRefresh);
            // 
            // TATA LETAK BUTTON BERURUTAN (Tinggi 42px per tombol, anti-menumpuk)
            // 
            int y = 10;
            SetupBtn(this.btnBarangTerlaris, "📊 Produk Terlaris", ref y);
            SetupBtn(this.btnCube, "🎲 Kombinasi Kategori", ref y);
            SetupBtn(this.btnOmzetBulanan, "📈 Akumulasi Omzet", ref y);
            SetupBtn(this.btnGroupingSets, "📋 Ringkasan Grup", ref y);
            SetupBtn(this.btnKuotaMenipis, "⚠️ Sisa Kuota Menipis", ref y);
            SetupBtn(this.btnUnion, "🔄 Semua Transaksi", ref y);
            SetupBtn(this.btnIntersect, "🤝 Produk Populer Bersama", ref y);
            SetupBtn(this.btnExcept, "👥 Daftar Akun Pasif", ref y);

            this.btnBarangTerlaris.Click += new System.EventHandler(this.btnBarangTerlaris_Click);
            this.btnCube.Click += new System.EventHandler(this.btnCube_Click);
            this.btnOmzetBulanan.Click += new System.EventHandler(this.btnOmzetBulanan_Click);
            this.btnGroupingSets.Click += new System.EventHandler(this.btnGroupingSets_Click);
            this.btnKuotaMenipis.Click += new System.EventHandler(this.btnKuotaMenipis_Click);
            this.btnUnion.Click += new System.EventHandler(this.btnUnion_Click);
            this.btnIntersect.Click += new System.EventHandler(this.btnIntersect_Click);
            this.btnExcept.Click += new System.EventHandler(this.btnExcept_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Black", 10F, FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Size = new System.Drawing.Size(240, 50);
            this.btnRefresh.Text = "🔄 REFRESH DATA";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvLaporan
            // 
            this.dgvLaporan.AllowUserToAddRows = false;
            this.dgvLaporan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLaporan.Location = new System.Drawing.Point(240, 100);
            this.dgvLaporan.ReadOnly = true;
            // 
            // SellerReportControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvLaporan);
            this.Controls.Add(this.pnlNavigasi);
            this.Controls.Add(this.pnlRingkasan);
            this.Size = new System.Drawing.Size(1046, 650);
            this.pnlNavigasi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).EndInit();
            this.ResumeLayout(false);
        }

        private void SetupBtn(System.Windows.Forms.Button btn, string text, ref int y)
        {
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.Size = new System.Drawing.Size(220, 38);
            btn.Location = new System.Drawing.Point(10, y);
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            y += 42;
        }

        private System.Windows.Forms.Panel pnlRingkasan, pnlNavigasi;
        private System.Windows.Forms.Button btnBarangTerlaris, btnCube, btnOmzetBulanan, btnGroupingSets, btnKuotaMenipis, btnUnion, btnIntersect, btnExcept, btnRefresh;
        private System.Windows.Forms.DataGridView dgvLaporan;
        private System.Windows.Forms.Label lblTotalProduk, lblTotalPO, lblTotalOmzet;
    }
}
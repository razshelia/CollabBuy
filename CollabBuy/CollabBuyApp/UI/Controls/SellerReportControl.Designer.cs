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
            pnlRingkasan = new Panel();
            lblTotalProduk = new Label();
            lblTotalPO = new Label();
            lblTotalOmzet = new Label();
            pnlNavigasi = new Panel();
            btnBarangTerlaris = new Button();
            btnKuotaMenipis = new Button();
            btnOmzetBulanan = new Button();
            btnRefresh = new Button();
            dgvLaporan = new DataGridView();
            pnlNavigasi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).BeginInit();
            SuspendLayout();
            // 
            // pnlRingkasan
            // 
            pnlRingkasan.BackColor = Color.FromArgb(45, 27, 79);
            pnlRingkasan.Dock = DockStyle.Top;
            pnlRingkasan.Location = new Point(0, 0);
            pnlRingkasan.Name = "pnlRingkasan";
            pnlRingkasan.Size = new Size(1046, 100);
            pnlRingkasan.TabIndex = 2;
            // 
            // lblTotalProduk
            // 
            lblTotalProduk.Location = new Point(0, 0);
            lblTotalProduk.Name = "lblTotalProduk";
            lblTotalProduk.Size = new Size(100, 23);
            lblTotalProduk.TabIndex = 0;
            // 
            // lblTotalPO
            // 
            lblTotalPO.Location = new Point(0, 0);
            lblTotalPO.Name = "lblTotalPO";
            lblTotalPO.Size = new Size(100, 23);
            lblTotalPO.TabIndex = 0;
            // 
            // lblTotalOmzet
            // 
            lblTotalOmzet.Location = new Point(0, 0);
            lblTotalOmzet.Name = "lblTotalOmzet";
            lblTotalOmzet.Size = new Size(100, 23);
            lblTotalOmzet.TabIndex = 0;
            // 
            // pnlNavigasi
            // 
            pnlNavigasi.BackColor = Color.FromArgb(167, 139, 250);
            pnlNavigasi.Controls.Add(btnBarangTerlaris);
            pnlNavigasi.Controls.Add(btnKuotaMenipis);
            pnlNavigasi.Controls.Add(btnOmzetBulanan);
            pnlNavigasi.Controls.Add(btnRefresh);
            pnlNavigasi.Dock = DockStyle.Left;
            pnlNavigasi.Location = new Point(0, 100);
            pnlNavigasi.Name = "pnlNavigasi";
            pnlNavigasi.Size = new Size(200, 176);
            pnlNavigasi.TabIndex = 1;
            // 
            // btnBarangTerlaris
            // 
            btnBarangTerlaris.BackColor = Color.FromArgb(167, 139, 250);
            btnBarangTerlaris.Dock = DockStyle.Top;
            btnBarangTerlaris.FlatStyle = FlatStyle.Flat;
            btnBarangTerlaris.ForeColor = Color.White;
            btnBarangTerlaris.Location = new Point(0, 90);
            btnBarangTerlaris.Name = "btnBarangTerlaris";
            btnBarangTerlaris.Size = new Size(200, 45);
            btnBarangTerlaris.TabIndex = 0;
            btnBarangTerlaris.Text = "📊 Barang Terlaris";
            btnBarangTerlaris.UseVisualStyleBackColor = false;
            btnBarangTerlaris.Click += btnBarangTerlaris_Click;
            // 
            // btnKuotaMenipis
            // 
            btnKuotaMenipis.BackColor = Color.FromArgb(167, 139, 250);
            btnKuotaMenipis.Dock = DockStyle.Top;
            btnKuotaMenipis.FlatStyle = FlatStyle.Flat;
            btnKuotaMenipis.ForeColor = Color.White;
            btnKuotaMenipis.Location = new Point(0, 45);
            btnKuotaMenipis.Name = "btnKuotaMenipis";
            btnKuotaMenipis.Size = new Size(200, 45);
            btnKuotaMenipis.TabIndex = 1;
            btnKuotaMenipis.Text = "⚠️ Kuota Menipis";
            btnKuotaMenipis.UseVisualStyleBackColor = false;
            btnKuotaMenipis.Click += btnKuotaMenipis_Click;
            // 
            // btnOmzetBulanan
            // 
            btnOmzetBulanan.BackColor = Color.FromArgb(167, 139, 250);
            btnOmzetBulanan.Dock = DockStyle.Top;
            btnOmzetBulanan.FlatStyle = FlatStyle.Flat;
            btnOmzetBulanan.ForeColor = Color.White;
            btnOmzetBulanan.Location = new Point(0, 0);
            btnOmzetBulanan.Name = "btnOmzetBulanan";
            btnOmzetBulanan.Size = new Size(200, 45);
            btnOmzetBulanan.TabIndex = 2;
            btnOmzetBulanan.Text = "📅 Omzet Bulanan";
            btnOmzetBulanan.UseVisualStyleBackColor = false;
            btnOmzetBulanan.Click += btnOmzetBulanan_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(167, 139, 250);
            btnRefresh.Dock = DockStyle.Bottom;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(0, 131);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(200, 45);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvLaporan
            // 
            dgvLaporan.AllowUserToAddRows = false;
            dgvLaporan.BackgroundColor = Color.White;
            dgvLaporan.Dock = DockStyle.Fill;
            dgvLaporan.Location = new Point(200, 100);
            dgvLaporan.Name = "dgvLaporan";
            dgvLaporan.ReadOnly = true;
            dgvLaporan.Size = new Size(846, 176);
            dgvLaporan.TabIndex = 0;
            // 
            // SellerReportControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(dgvLaporan);
            Controls.Add(pnlNavigasi);
            Controls.Add(pnlRingkasan);
            Name = "SellerReportControl";
            Size = new Size(1046, 276);
            pnlNavigasi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel BuatCardRingkasan(string judul, ref System.Windows.Forms.Label lblValue, int x)
        {
            Panel card = new Panel();
            card.Size = new System.Drawing.Size(150, 80);
            card.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            card.Location = new System.Drawing.Point(x, 10);
            Label lblJudul = new Label()
            {
                Text = judul,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9F),
                Size = new System.Drawing.Size(130, 25),
                Location = new System.Drawing.Point(10, 5)
            };
            lblValue = new Label()
            {
                Text = "0",
                ForeColor = System.Drawing.Color.FromArgb(253, 224, 71),
                Font = new System.Drawing.Font("Segoe UI Black", 18F),
                Size = new System.Drawing.Size(130, 40),
                Location = new System.Drawing.Point(10, 30)
            };
            card.Controls.Add(lblJudul);
            card.Controls.Add(lblValue);
            return card;
        }

        private System.Windows.Forms.Panel pnlRingkasan, pnlNavigasi;
        private System.Windows.Forms.Label lblTotalProduk, lblTotalPO, lblTotalOmzet;
        private System.Windows.Forms.Button btnBarangTerlaris, btnKuotaMenipis, btnOmzetBulanan, btnRefresh;
        private System.Windows.Forms.DataGridView dgvLaporan;
    }
}
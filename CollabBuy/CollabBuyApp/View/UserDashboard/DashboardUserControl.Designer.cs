namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class DashboardUserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            lblSapaan = new Label();
            lblSubtitle = new Label();
            pnlPesanan = new Panel();
            lblIkonPesanan = new Label();
            lblTitlePesanan = new Label();
            lblValPesanan = new Label();
            pnlKeranjang = new Panel();
            lblIkonKeranjang = new Label();
            lblTitleKeranjang = new Label();
            lblValKeranjang = new Label();
            pnlSaldo = new Panel();
            lblIkonSaldo = new Label();
            lblTitleSaldo = new Label();
            lblValSaldo = new Label();
            lblKatalogTitle = new Label();
            btnLihatSemua = new Button();
            pnlKatalog = new Panel();
            flpDashboard = new FlowLayoutPanel();
            pnlMain.SuspendLayout();
            pnlPesanan.SuspendLayout();
            pnlKeranjang.SuspendLayout();
            pnlSaldo.SuspendLayout();
            pnlKatalog.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = Color.FromArgb(248, 245, 255);
            pnlMain.Controls.Add(lblSapaan);
            pnlMain.Controls.Add(lblSubtitle);
            pnlMain.Controls.Add(pnlPesanan);
            pnlMain.Controls.Add(pnlKeranjang);
            pnlMain.Controls.Add(pnlSaldo);
            pnlMain.Controls.Add(lblKatalogTitle);
            pnlMain.Controls.Add(btnLihatSemua);
            pnlMain.Controls.Add(pnlKatalog);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(4, 3, 4, 3);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1143, 808);
            pnlMain.TabIndex = 0;
            // 
            // lblSapaan
            // 
            lblSapaan.AutoSize = true;
            lblSapaan.Font = new Font("Segoe UI Black", 20F, FontStyle.Bold);
            lblSapaan.ForeColor = Color.FromArgb(72, 0, 120);
            lblSapaan.Location = new Point(35, 32);
            lblSapaan.Margin = new Padding(4, 0, 4, 0);
            lblSapaan.Name = "lblSapaan";
            lblSapaan.Size = new Size(233, 37);
            lblSapaan.TabIndex = 0;
            lblSapaan.Text = "Halo, Bestie! 👋";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI Semibold", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(130, 80, 180);
            lblSubtitle.Location = new Point(40, 81);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(233, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Yuk cek belanjaan kamu hari ini! ✨";
            // 
            // pnlPesanan
            // 
            pnlPesanan.BackColor = Color.FromArgb(230, 210, 255);
            pnlPesanan.Controls.Add(lblIkonPesanan);
            pnlPesanan.Controls.Add(lblTitlePesanan);
            pnlPesanan.Controls.Add(lblValPesanan);
            pnlPesanan.Location = new Point(35, 127);
            pnlPesanan.Margin = new Padding(4, 3, 4, 3);
            pnlPesanan.Name = "pnlPesanan";
            pnlPesanan.Size = new Size(233, 127);
            pnlPesanan.TabIndex = 2;
            // 
            // lblIkonPesanan
            // 
            lblIkonPesanan.AutoSize = true;
            lblIkonPesanan.Font = new Font("Segoe UI", 22F);
            lblIkonPesanan.Location = new Point(16, 12);
            lblIkonPesanan.Margin = new Padding(4, 0, 4, 0);
            lblIkonPesanan.Name = "lblIkonPesanan";
            lblIkonPesanan.Size = new Size(59, 41);
            lblIkonPesanan.TabIndex = 0;
            lblIkonPesanan.Text = "📦";
            // 
            // lblTitlePesanan
            // 
            lblTitlePesanan.AutoSize = true;
            lblTitlePesanan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitlePesanan.ForeColor = Color.FromArgb(72, 0, 120);
            lblTitlePesanan.Location = new Point(16, 58);
            lblTitlePesanan.Margin = new Padding(4, 0, 4, 0);
            lblTitlePesanan.Name = "lblTitlePesanan";
            lblTitlePesanan.Size = new Size(83, 15);
            lblTitlePesanan.TabIndex = 1;
            lblTitlePesanan.Text = "Pesanan Aktif";
            // 
            // lblValPesanan
            // 
            lblValPesanan.AutoSize = true;
            lblValPesanan.Font = new Font("Segoe UI Black", 22F, FontStyle.Bold);
            lblValPesanan.ForeColor = Color.FromArgb(72, 0, 120);
            lblValPesanan.Location = new Point(16, 78);
            lblValPesanan.Margin = new Padding(4, 0, 4, 0);
            lblValPesanan.Name = "lblValPesanan";
            lblValPesanan.Size = new Size(36, 41);
            lblValPesanan.TabIndex = 2;
            lblValPesanan.Text = "0";
            // 
            // pnlKeranjang
            // 
            pnlKeranjang.BackColor = Color.FromArgb(254, 252, 200);
            pnlKeranjang.Controls.Add(lblIkonKeranjang);
            pnlKeranjang.Controls.Add(lblTitleKeranjang);
            pnlKeranjang.Controls.Add(lblValKeranjang);
            pnlKeranjang.Location = new Point(292, 127);
            pnlKeranjang.Margin = new Padding(4, 3, 4, 3);
            pnlKeranjang.Name = "pnlKeranjang";
            pnlKeranjang.Size = new Size(233, 127);
            pnlKeranjang.TabIndex = 3;
            // 
            // lblIkonKeranjang
            // 
            lblIkonKeranjang.AutoSize = true;
            lblIkonKeranjang.Font = new Font("Segoe UI", 22F);
            lblIkonKeranjang.Location = new Point(16, 12);
            lblIkonKeranjang.Margin = new Padding(4, 0, 4, 0);
            lblIkonKeranjang.Name = "lblIkonKeranjang";
            lblIkonKeranjang.Size = new Size(59, 41);
            lblIkonKeranjang.TabIndex = 0;
            lblIkonKeranjang.Text = "\U0001f6d2";
            // 
            // lblTitleKeranjang
            // 
            lblTitleKeranjang.AutoSize = true;
            lblTitleKeranjang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleKeranjang.ForeColor = Color.FromArgb(130, 100, 0);
            lblTitleKeranjang.Location = new Point(16, 58);
            lblTitleKeranjang.Margin = new Padding(4, 0, 4, 0);
            lblTitleKeranjang.Name = "lblTitleKeranjang";
            lblTitleKeranjang.Size = new Size(106, 15);
            lblTitleKeranjang.TabIndex = 1;
            lblTitleKeranjang.Text = "Item di Keranjang";
            // 
            // lblValKeranjang
            // 
            lblValKeranjang.AutoSize = true;
            lblValKeranjang.Font = new Font("Segoe UI Black", 22F, FontStyle.Bold);
            lblValKeranjang.ForeColor = Color.FromArgb(130, 100, 0);
            lblValKeranjang.Location = new Point(16, 78);
            lblValKeranjang.Margin = new Padding(4, 0, 4, 0);
            lblValKeranjang.Name = "lblValKeranjang";
            lblValKeranjang.Size = new Size(36, 41);
            lblValKeranjang.TabIndex = 2;
            lblValKeranjang.Text = "0";
            // 
            // pnlSaldo
            // 
            pnlSaldo.BackColor = Color.FromArgb(210, 255, 230);
            pnlSaldo.Controls.Add(lblIkonSaldo);
            pnlSaldo.Controls.Add(lblTitleSaldo);
            pnlSaldo.Controls.Add(lblValSaldo);
            pnlSaldo.Location = new Point(548, 127);
            pnlSaldo.Margin = new Padding(4, 3, 4, 3);
            pnlSaldo.Name = "pnlSaldo";
            pnlSaldo.Size = new Size(233, 127);
            pnlSaldo.TabIndex = 4;
            // 
            // lblIkonSaldo
            // 
            lblIkonSaldo.AutoSize = true;
            lblIkonSaldo.Font = new Font("Segoe UI", 22F);
            lblIkonSaldo.Location = new Point(16, 12);
            lblIkonSaldo.Margin = new Padding(4, 0, 4, 0);
            lblIkonSaldo.Name = "lblIkonSaldo";
            lblIkonSaldo.Size = new Size(59, 41);
            lblIkonSaldo.TabIndex = 0;
            lblIkonSaldo.Text = "🎫";
            // 
            // lblTitleSaldo
            // 
            lblTitleSaldo.AutoSize = true;
            lblTitleSaldo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleSaldo.ForeColor = Color.FromArgb(0, 100, 60);
            lblTitleSaldo.Location = new Point(16, 58);
            lblTitleSaldo.Margin = new Padding(4, 0, 4, 0);
            lblTitleSaldo.Name = "lblTitleSaldo";
            lblTitleSaldo.Size = new Size(72, 15);
            lblTitleSaldo.TabIndex = 1;
            lblTitleSaldo.Text = "PO Tersedia";
            // 
            // lblValSaldo
            // 
            lblValSaldo.AutoSize = true;
            lblValSaldo.Font = new Font("Segoe UI Black", 22F, FontStyle.Bold);
            lblValSaldo.ForeColor = Color.FromArgb(0, 100, 60);
            lblValSaldo.Location = new Point(16, 78);
            lblValSaldo.Margin = new Padding(4, 0, 4, 0);
            lblValSaldo.Name = "lblValSaldo";
            lblValSaldo.Size = new Size(36, 41);
            lblValSaldo.TabIndex = 2;
            lblValSaldo.Text = "0";
            // 
            // lblKatalogTitle
            // 
            lblKatalogTitle.AutoSize = true;
            lblKatalogTitle.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
            lblKatalogTitle.ForeColor = Color.FromArgb(200, 50, 50);
            lblKatalogTitle.Location = new Point(35, 286);
            lblKatalogTitle.Margin = new Padding(4, 0, 4, 0);
            lblKatalogTitle.Name = "lblKatalogTitle";
            lblKatalogTitle.Size = new Size(266, 21);
            lblKatalogTitle.TabIndex = 5;
            lblKatalogTitle.Text = "🔥 FOMO ALERT: PO Mau Habis!";
            // 
            // btnLihatSemua
            // 
            btnLihatSemua.BackColor = Color.FromArgb(72, 0, 120);
            btnLihatSemua.Cursor = Cursors.Hand;
            btnLihatSemua.FlatAppearance.BorderSize = 0;
            btnLihatSemua.FlatStyle = FlatStyle.Flat;
            btnLihatSemua.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnLihatSemua.ForeColor = Color.FromArgb(254, 252, 200);
            btnLihatSemua.Location = new Point(875, 282);
            btnLihatSemua.Margin = new Padding(4, 3, 4, 3);
            btnLihatSemua.Name = "btnLihatSemua";
            btnLihatSemua.Size = new Size(163, 39);
            btnLihatSemua.TabIndex = 6;
            btnLihatSemua.Text = "Ke Katalog Lengkap →";
            btnLihatSemua.UseVisualStyleBackColor = false;
            btnLihatSemua.Click += btnLihatSemua_Click;
            // 
            // pnlKatalog
            // 
            pnlKatalog.BackColor = Color.Transparent;
            pnlKatalog.Controls.Add(flpDashboard);
            pnlKatalog.Location = new Point(35, 335);
            pnlKatalog.Margin = new Padding(4, 3, 4, 3);
            pnlKatalog.Name = "pnlKatalog";
            pnlKatalog.Size = new Size(1050, 462);
            pnlKatalog.TabIndex = 7;
            // 
            // flpDashboard
            // 
            flpDashboard.AutoScroll = true;
            flpDashboard.Dock = DockStyle.Fill;
            flpDashboard.Location = new Point(0, 0);
            flpDashboard.Margin = new Padding(4, 3, 4, 3);
            flpDashboard.Name = "flpDashboard";
            flpDashboard.Padding = new Padding(0, 0, 0, 35);
            flpDashboard.Size = new Size(1050, 462);
            flpDashboard.TabIndex = 0;
            // 
            // DashboardUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 245, 255);
            Controls.Add(pnlMain);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DashboardUserControl";
            Size = new Size(1143, 808);
            Load += DashboardUserControl_Load;
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlPesanan.ResumeLayout(false);
            pnlPesanan.PerformLayout();
            pnlKeranjang.ResumeLayout(false);
            pnlKeranjang.PerformLayout();
            pnlSaldo.ResumeLayout(false);
            pnlSaldo.PerformLayout();
            pnlKatalog.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblSapaan;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlPesanan;
        private System.Windows.Forms.Label lblIkonPesanan;
        private System.Windows.Forms.Label lblTitlePesanan;
        private System.Windows.Forms.Label lblValPesanan;

        private System.Windows.Forms.Panel pnlKeranjang;
        private System.Windows.Forms.Label lblIkonKeranjang;
        private System.Windows.Forms.Label lblTitleKeranjang;
        private System.Windows.Forms.Label lblValKeranjang;

        private System.Windows.Forms.Panel pnlSaldo;
        private System.Windows.Forms.Label lblIkonSaldo;
        private System.Windows.Forms.Label lblTitleSaldo;
        private System.Windows.Forms.Label lblValSaldo;

        private System.Windows.Forms.Label lblKatalogTitle;
        private System.Windows.Forms.Button btnLihatSemua;
        private System.Windows.Forms.Panel pnlKatalog;
        private System.Windows.Forms.FlowLayoutPanel flpDashboard; // Card Container!
    }
}
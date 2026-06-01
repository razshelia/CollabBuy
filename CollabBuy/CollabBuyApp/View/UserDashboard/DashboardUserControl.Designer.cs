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
            // === DEKLARASI KONTROL ===
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblSapaan = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            // Kartu statistik
            this.pnlPesanan = new System.Windows.Forms.Panel();
            this.lblIkonPesanan = new System.Windows.Forms.Label();
            this.lblTitlePesanan = new System.Windows.Forms.Label();
            this.lblValPesanan = new System.Windows.Forms.Label();

            this.pnlKeranjang = new System.Windows.Forms.Panel();
            this.lblIkonKeranjang = new System.Windows.Forms.Label();
            this.lblTitleKeranjang = new System.Windows.Forms.Label();
            this.lblValKeranjang = new System.Windows.Forms.Label();

            this.pnlSaldo = new System.Windows.Forms.Panel();
            this.lblIkonSaldo = new System.Windows.Forms.Label();
            this.lblTitleSaldo = new System.Windows.Forms.Label();
            this.lblValSaldo = new System.Windows.Forms.Label();

            // Section Katalog Terbaru / FOMO
            this.lblKatalogTitle = new System.Windows.Forms.Label();
            this.btnLihatSemua = new System.Windows.Forms.Button();
            this.pnlKatalog = new System.Windows.Forms.Panel();
            this.flpDashboard = new System.Windows.Forms.FlowLayoutPanel(); // PENGGANTI TABEL!

            // === SUSPEND LAYOUT ===
            this.pnlMain.SuspendLayout();
            this.pnlPesanan.SuspendLayout();
            this.pnlKeranjang.SuspendLayout();
            this.pnlSaldo.SuspendLayout();
            this.pnlKatalog.SuspendLayout();
            this.SuspendLayout();

            // ============================================================
            // pnlMain
            // ============================================================
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlMain.Controls.Add(this.lblSapaan);
            this.pnlMain.Controls.Add(this.lblSubtitle);
            this.pnlMain.Controls.Add(this.pnlPesanan);
            this.pnlMain.Controls.Add(this.pnlKeranjang);
            this.pnlMain.Controls.Add(this.pnlSaldo);
            this.pnlMain.Controls.Add(this.lblKatalogTitle);
            this.pnlMain.Controls.Add(this.btnLihatSemua);
            this.pnlMain.Controls.Add(this.pnlKatalog);

            // ============================================================
            // lblSapaan & lblSubtitle
            // ============================================================
            this.lblSapaan.AutoSize = true;
            this.lblSapaan.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblSapaan.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblSapaan.Location = new System.Drawing.Point(30, 28);
            this.lblSapaan.Text = "Halo, Bestie! 👋";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(130, 80, 180);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 70);
            this.lblSubtitle.Text = "Yuk cek belanjaan kamu hari ini! ✨";

            // ============================================================
            // pnlPesanan — Kartu "Pesanan Aktif"
            // ============================================================
            this.pnlPesanan.BackColor = System.Drawing.Color.FromArgb(230, 210, 255);
            this.pnlPesanan.Location = new System.Drawing.Point(30, 110);
            this.pnlPesanan.Size = new System.Drawing.Size(200, 110);
            this.pnlPesanan.Controls.Add(this.lblIkonPesanan);
            this.pnlPesanan.Controls.Add(this.lblTitlePesanan);
            this.pnlPesanan.Controls.Add(this.lblValPesanan);

            this.lblIkonPesanan.AutoSize = true;
            this.lblIkonPesanan.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblIkonPesanan.Location = new System.Drawing.Point(14, 10);
            this.lblIkonPesanan.Text = "📦";

            this.lblTitlePesanan.AutoSize = true;
            this.lblTitlePesanan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitlePesanan.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblTitlePesanan.Location = new System.Drawing.Point(14, 50);
            this.lblTitlePesanan.Text = "Pesanan Aktif";

            this.lblValPesanan.AutoSize = true;
            this.lblValPesanan.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblValPesanan.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblValPesanan.Location = new System.Drawing.Point(14, 68);
            this.lblValPesanan.Text = "0";

            // ============================================================
            // pnlKeranjang — Kartu "Item Keranjang"
            // ============================================================
            this.pnlKeranjang.BackColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.pnlKeranjang.Location = new System.Drawing.Point(250, 110);
            this.pnlKeranjang.Size = new System.Drawing.Size(200, 110);
            this.pnlKeranjang.Controls.Add(this.lblIkonKeranjang);
            this.pnlKeranjang.Controls.Add(this.lblTitleKeranjang);
            this.pnlKeranjang.Controls.Add(this.lblValKeranjang);

            this.lblIkonKeranjang.AutoSize = true;
            this.lblIkonKeranjang.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblIkonKeranjang.Location = new System.Drawing.Point(14, 10);
            this.lblIkonKeranjang.Text = "🛒";

            this.lblTitleKeranjang.AutoSize = true;
            this.lblTitleKeranjang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleKeranjang.ForeColor = System.Drawing.Color.FromArgb(130, 100, 0);
            this.lblTitleKeranjang.Location = new System.Drawing.Point(14, 50);
            this.lblTitleKeranjang.Text = "Item di Keranjang";

            this.lblValKeranjang.AutoSize = true;
            this.lblValKeranjang.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblValKeranjang.ForeColor = System.Drawing.Color.FromArgb(130, 100, 0);
            this.lblValKeranjang.Location = new System.Drawing.Point(14, 68);
            this.lblValKeranjang.Text = "0";

            // ============================================================
            // pnlSaldo — Kartu "PO Tersedia"
            // ============================================================
            this.pnlSaldo.BackColor = System.Drawing.Color.FromArgb(210, 255, 230);
            this.pnlSaldo.Location = new System.Drawing.Point(470, 110);
            this.pnlSaldo.Size = new System.Drawing.Size(200, 110);
            this.pnlSaldo.Controls.Add(this.lblIkonSaldo);
            this.pnlSaldo.Controls.Add(this.lblTitleSaldo);
            this.pnlSaldo.Controls.Add(this.lblValSaldo);

            this.lblIkonSaldo.AutoSize = true;
            this.lblIkonSaldo.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblIkonSaldo.Location = new System.Drawing.Point(14, 10);
            this.lblIkonSaldo.Text = "🎫";

            this.lblTitleSaldo.AutoSize = true;
            this.lblTitleSaldo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleSaldo.ForeColor = System.Drawing.Color.FromArgb(0, 100, 60);
            this.lblTitleSaldo.Location = new System.Drawing.Point(14, 50);
            this.lblTitleSaldo.Text = "PO Tersedia";

            this.lblValSaldo.AutoSize = true;
            this.lblValSaldo.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblValSaldo.ForeColor = System.Drawing.Color.FromArgb(0, 100, 60);
            this.lblValSaldo.Location = new System.Drawing.Point(14, 68);
            this.lblValSaldo.Text = "0";

            // ============================================================
            // lblKatalogTitle & btnLihatSemua
            // ============================================================
            this.lblKatalogTitle.AutoSize = true;
            this.lblKatalogTitle.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblKatalogTitle.ForeColor = System.Drawing.Color.FromArgb(200, 50, 50); // Red for FOMO
            this.lblKatalogTitle.Location = new System.Drawing.Point(30, 248);
            this.lblKatalogTitle.Text = "🔥 FOMO ALERT: PO Mau Habis!";

            this.btnLihatSemua.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnLihatSemua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLihatSemua.FlatAppearance.BorderSize = 0;
            this.btnLihatSemua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLihatSemua.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnLihatSemua.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnLihatSemua.Location = new System.Drawing.Point(750, 244);
            this.btnLihatSemua.Size = new System.Drawing.Size(140, 34);
            this.btnLihatSemua.Text = "Ke Katalog Lengkap →";
            this.btnLihatSemua.Click += new System.EventHandler(this.btnLihatSemua_Click);

            // ============================================================
            // pnlKatalog & flpDashboard (Pengganti Tabel)
            // ============================================================
            this.pnlKatalog.BackColor = System.Drawing.Color.Transparent;
            this.pnlKatalog.Location = new System.Drawing.Point(30, 290);
            this.pnlKatalog.Size = new System.Drawing.Size(900, 400); // Lebih tinggi buat muat cards
            this.pnlKatalog.Controls.Add(this.flpDashboard);

            this.flpDashboard.AutoScroll = true;
            this.flpDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDashboard.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);

            // ============================================================
            // DashboardUserControl Setup
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.Controls.Add(this.pnlMain);
            this.Name = "DashboardUserControl";
            this.Size = new System.Drawing.Size(980, 700);
            this.Load += new System.EventHandler(this.DashboardUserControl_Load);
            this.Resize += new System.EventHandler(this.DashboardUserControl_Resize);

            this.pnlPesanan.ResumeLayout(false);
            this.pnlPesanan.PerformLayout();
            this.pnlKeranjang.ResumeLayout(false);
            this.pnlKeranjang.PerformLayout();
            this.pnlSaldo.ResumeLayout(false);
            this.pnlSaldo.PerformLayout();
            this.pnlKatalog.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
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
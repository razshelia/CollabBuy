namespace CollabBuy.CollabBuyApp.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlSidebarTop = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblLogoSub = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.pnlDivider2 = new System.Windows.Forms.Panel();

            // Section labels
            this.lblSectionAdmin = new System.Windows.Forms.Label();
            this.lblSectionBuyer = new System.Windows.Forms.Label();
            this.lblSectionSeller = new System.Windows.Forms.Label();

            // Admin buttons
            this.btnAdminDashboard = new System.Windows.Forms.Button();
            this.btnAdminVerifikasi = new System.Windows.Forms.Button();
            this.btnAdminKategori = new System.Windows.Forms.Button();
            this.btnAdminKeluhan = new System.Windows.Forms.Button();

            // Buyer buttons
            this.btnUserKatalog = new System.Windows.Forms.Button();
            this.btnUserCheckout = new System.Windows.Forms.Button();
            this.btnUserRiwayat = new System.Windows.Forms.Button();
            this.btnUserAduan = new System.Windows.Forms.Button();
            this.btnUserBukaLapak = new System.Windows.Forms.Button();

            // Seller buttons
            this.btnSellerKatalog = new System.Windows.Forms.Button();
            this.btnSellerPesanan = new System.Windows.Forms.Button();
            this.btnSellerAnalitik = new System.Windows.Forms.Button();
            this.btnSellerUlasan = new System.Windows.Forms.Button();

            // Bottom static buttons
            this.btnProfil = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            // Main container
            this.pnlMainContainer = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();

            // ── FORM ─────────────────────────────────────────────
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CollabBuy – Solusi Gotong Royong Mahasiswa ✨";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // ── SIDEBAR ──────────────────────────────────────────
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(30, 27, 50);
            this.pnlSidebar.Width = 260;
            this.pnlSidebar.Visible = false;

            // Logo area
            this.pnlSidebarTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSidebarTop.Height = 110;
            this.pnlSidebarTop.BackColor = System.Drawing.Color.FromArgb(40, 37, 65);

            this.lblLogo.Text = "COLLABBUY";
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(255, 235, 133);
            this.lblLogo.AutoSize = false;
            this.lblLogo.Size = new System.Drawing.Size(240, 40);
            this.lblLogo.Location = new System.Drawing.Point(10, 18);
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblLogoSub.Text = "✨ Platform Danus Kampus";
            this.lblLogoSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogoSub.ForeColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.lblLogoSub.AutoSize = false;
            this.lblLogoSub.Size = new System.Drawing.Size(240, 20);
            this.lblLogoSub.Location = new System.Drawing.Point(10, 60);
            this.lblLogoSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblUserInfo.Text = "";
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
            this.lblUserInfo.AutoSize = false;
            this.lblUserInfo.Size = new System.Drawing.Size(240, 20);
            this.lblUserInfo.Location = new System.Drawing.Point(10, 85);
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pnlSidebarTop.Controls.Add(this.lblLogo);
            this.pnlSidebarTop.Controls.Add(this.lblLogoSub);
            this.pnlSidebarTop.Controls.Add(this.lblUserInfo);

            // Section labels
            this.lblSectionAdmin.Text = "👑 ADMIN MENU";
            this.lblSectionAdmin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSectionAdmin.ForeColor = System.Drawing.Color.FromArgb(255, 235, 133);
            this.lblSectionAdmin.AutoSize = false;
            this.lblSectionAdmin.Size = new System.Drawing.Size(240, 25);
            this.lblSectionAdmin.Location = new System.Drawing.Point(10, 120);
            this.lblSectionAdmin.Visible = false;

            this.lblSectionBuyer.Text = "🛒 BUYER MENU";
            this.lblSectionBuyer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSectionBuyer.ForeColor = System.Drawing.Color.FromArgb(255, 235, 133);
            this.lblSectionBuyer.AutoSize = false;
            this.lblSectionBuyer.Size = new System.Drawing.Size(240, 25);
            this.lblSectionBuyer.Location = new System.Drawing.Point(10, 300);
            this.lblSectionBuyer.Visible = false;

            this.lblSectionSeller.Text = "🏪 SELLER MENU";
            this.lblSectionSeller.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSectionSeller.ForeColor = System.Drawing.Color.FromArgb(255, 235, 133);
            this.lblSectionSeller.AutoSize = false;
            this.lblSectionSeller.Size = new System.Drawing.Size(240, 25);
            this.lblSectionSeller.Location = new System.Drawing.Point(10, 460);
            this.lblSectionSeller.Visible = false;

            // ── Admin Buttons ────────────────────────────────────
            Button[] adminBtns = { btnAdminDashboard, btnAdminVerifikasi, btnAdminKategori, btnAdminKeluhan };
            string[] adminTexts = { "🏠 Dashboard", "✅ Verifikasi Penjual", "📂 Kategori", "📩 Keluhan" };
            for (int i = 0; i < adminBtns.Length; i++)
            {
                var b = adminBtns[i];
                b.Text = adminTexts[i];
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.Font = new System.Drawing.Font("Segoe UI", 10F);
                b.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
                b.Size = new System.Drawing.Size(240, 40);
                b.Location = new System.Drawing.Point(10, 150 + i * 45);
                b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                b.Visible = false;
            }
            // Event handlers
            this.btnAdminDashboard.Click += new System.EventHandler(this.btnAdminDashboard_Click);
            this.btnAdminVerifikasi.Click += new System.EventHandler(this.btnAdminVerifikasi_Click);
            this.btnAdminKategori.Click += new System.EventHandler(this.btnAdminKategori_Click);
            this.btnAdminKeluhan.Click += new System.EventHandler(this.btnAdminKeluhan_Click);

            // ── Buyer Buttons ────────────────────────────────────
            Button[] buyerBtns = { btnUserKatalog, btnUserCheckout, btnUserRiwayat, btnUserAduan, btnUserBukaLapak };
            string[] buyerTexts = { "🛍️ Katalog Produk", "💳 Checkout", "📋 Riwayat Pesanan", "📝 Aduan", "🚀 Buka Lapak" };
            for (int i = 0; i < buyerBtns.Length; i++)
            {
                var b = buyerBtns[i];
                b.Text = buyerTexts[i];
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.Font = new System.Drawing.Font("Segoe UI", 10F);
                b.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
                b.Size = new System.Drawing.Size(240, 40);
                b.Location = new System.Drawing.Point(10, 330 + i * 45);
                b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                b.Visible = false;
            }
            this.btnUserKatalog.Click += new System.EventHandler(this.btnUserKatalog_Click);
            this.btnUserCheckout.Click += new System.EventHandler(this.btnUserCheckout_Click);
            this.btnUserRiwayat.Click += new System.EventHandler(this.btnUserRiwayat_Click);
            this.btnUserAduan.Click += new System.EventHandler(this.btnUserAduan_Click);
            this.btnUserBukaLapak.Click += new System.EventHandler(this.btnUserBukaLapak_Click);

            // ── Seller Buttons ───────────────────────────────────
            Button[] sellerBtns = { btnSellerKatalog, btnSellerPesanan, btnSellerAnalitik, btnSellerUlasan };
            string[] sellerTexts = { "📦 Produk Saya", "📋 Pesanan Masuk", "📊 Analitik", "⭐ Ulasan" };
            for (int i = 0; i < sellerBtns.Length; i++)
            {
                var b = sellerBtns[i];
                b.Text = sellerTexts[i];
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.Font = new System.Drawing.Font("Segoe UI", 10F);
                b.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
                b.Size = new System.Drawing.Size(240, 40);
                b.Location = new System.Drawing.Point(10, 490 + i * 45);
                b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                b.Visible = false;
            }
            this.btnSellerKatalog.Click += new System.EventHandler(this.btnSellerKatalog_Click);
            this.btnSellerPesanan.Click += new System.EventHandler(this.btnSellerPesanan_Click);
            this.btnSellerAnalitik.Click += new System.EventHandler(this.btnSellerAnalitik_Click);
            this.btnSellerUlasan.Click += new System.EventHandler(this.btnSellerUlasan_Click);

            // ── Bottom Buttons ───────────────────────────────────
            this.btnProfil.Text = "👤 Profil";
            this.btnProfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfil.FlatAppearance.BorderSize = 0;
            this.btnProfil.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProfil.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
            this.btnProfil.Size = new System.Drawing.Size(240, 40);
            this.btnProfil.Location = new System.Drawing.Point(10, 620);
            this.btnProfil.Click += new System.EventHandler(this.btnProfil_Click);

            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
            this.btnLogout.Size = new System.Drawing.Size(240, 40);
            this.btnLogout.Location = new System.Drawing.Point(10, 665);
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // ── Main Container ───────────────────────────────────
            this.pnlMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContainer.BackColor = System.Drawing.Color.White;

            // ── Add controls to sidebar ──────────────────────────
            this.pnlSidebar.Controls.Add(this.pnlSidebarTop);
            this.pnlSidebar.Controls.Add(this.lblSectionAdmin);
            this.pnlSidebar.Controls.Add(this.btnAdminDashboard);
            this.pnlSidebar.Controls.Add(this.btnAdminVerifikasi);
            this.pnlSidebar.Controls.Add(this.btnAdminKategori);
            this.pnlSidebar.Controls.Add(this.btnAdminKeluhan);
            this.pnlSidebar.Controls.Add(this.lblSectionBuyer);
            this.pnlSidebar.Controls.Add(this.btnUserKatalog);
            this.pnlSidebar.Controls.Add(this.btnUserCheckout);
            this.pnlSidebar.Controls.Add(this.btnUserRiwayat);
            this.pnlSidebar.Controls.Add(this.btnUserAduan);
            this.pnlSidebar.Controls.Add(this.btnUserBukaLapak);
            this.pnlSidebar.Controls.Add(this.lblSectionSeller);
            this.pnlSidebar.Controls.Add(this.btnSellerKatalog);
            this.pnlSidebar.Controls.Add(this.btnSellerPesanan);
            this.pnlSidebar.Controls.Add(this.btnSellerAnalitik);
            this.pnlSidebar.Controls.Add(this.btnSellerUlasan);
            this.pnlSidebar.Controls.Add(this.btnProfil);
            this.pnlSidebar.Controls.Add(this.btnLogout);

            // ── Add to form ──────────────────────────────────────
            this.Controls.Add(this.pnlMainContainer);
            this.Controls.Add(this.pnlSidebar);

            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlSidebarTop;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblLogoSub;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Panel pnlDivider2;
        private System.Windows.Forms.Label lblSectionAdmin;
        private System.Windows.Forms.Label lblSectionBuyer;
        private System.Windows.Forms.Label lblSectionSeller;
        private System.Windows.Forms.Button btnAdminDashboard;
        private System.Windows.Forms.Button btnAdminVerifikasi;
        private System.Windows.Forms.Button btnAdminKategori;
        private System.Windows.Forms.Button btnAdminKeluhan;
        private System.Windows.Forms.Button btnUserKatalog;
        private System.Windows.Forms.Button btnUserCheckout;
        private System.Windows.Forms.Button btnUserRiwayat;
        private System.Windows.Forms.Button btnUserAduan;
        private System.Windows.Forms.Button btnUserBukaLapak;
        private System.Windows.Forms.Button btnSellerKatalog;
        private System.Windows.Forms.Button btnSellerPesanan;
        private System.Windows.Forms.Button btnSellerAnalitik;
        private System.Windows.Forms.Button btnSellerUlasan;
        private System.Windows.Forms.Button btnProfil;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlMainContainer;
    }
}
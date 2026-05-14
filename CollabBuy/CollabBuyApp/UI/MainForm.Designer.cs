namespace CollabBuy.CollabBuyApp.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlSidebar       = new System.Windows.Forms.Panel();
            pnlSidebarTop    = new System.Windows.Forms.Panel();
            pnlSidebarBottom = new System.Windows.Forms.Panel();
            pnlMainContainer = new System.Windows.Forms.Panel();
            lblLogo          = new System.Windows.Forms.Label();
            lblLogoSub       = new System.Windows.Forms.Label();
            lblUserInfo      = new System.Windows.Forms.Label();
            pnlDivider       = new System.Windows.Forms.Panel();
            pnlDivider2      = new System.Windows.Forms.Panel();

            // Nav buttons - Admin (4 menu)
            btnAdminDashboard      = new System.Windows.Forms.Button();
            btnAdminVerifikasi     = new System.Windows.Forms.Button();
            btnAdminKategori       = new System.Windows.Forms.Button();
            btnAdminKeluhan        = new System.Windows.Forms.Button();

            // Nav buttons - User/Buyer (5 menu)
            btnUserKatalog         = new System.Windows.Forms.Button();
            btnUserCheckout        = new System.Windows.Forms.Button();
            btnUserRiwayat         = new System.Windows.Forms.Button();
            btnUserAduan           = new System.Windows.Forms.Button();
            btnUserBukaLapak       = new System.Windows.Forms.Button();

            // Nav buttons - Seller (4 menu, hidden by default)
            btnSellerKatalog       = new System.Windows.Forms.Button();
            btnSellerPesanan       = new System.Windows.Forms.Button();
            btnSellerAnalitik      = new System.Windows.Forms.Button();
            btnSellerUlasan        = new System.Windows.Forms.Button();

            // Static bottom buttons
            btnProfil              = new System.Windows.Forms.Button();
            btnLogout              = new System.Windows.Forms.Button();

            pnlSidebar.SuspendLayout();
            SuspendLayout();

            // ── FORM ─────────────────────────────────────────────
            BackColor        = System.Drawing.Color.White;
            ClientSize       = new System.Drawing.Size(1280, 720);
            FormBorderStyle  = System.Windows.Forms.FormBorderStyle.Sizable;
            StartPosition    = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text             = "CollabBuy – Solusi Gotong Royong Mahasiswa ✨";
            WindowState      = System.Windows.Forms.FormWindowState.Maximized;

            // ── SIDEBAR ──────────────────────────────────────────
            pnlSidebar.Dock      = System.Windows.Forms.DockStyle.Left;
            pnlSidebar.BackColor = System.Drawing.Color.FromArgb(30, 27, 50);   // Dark navy
            pnlSidebar.Width     = 260;
            pnlSidebar.Visible   = false;
            pnlSidebar.Name      = "pnlSidebar";

            // Logo area
            pnlSidebarTop.Dock      = System.Windows.Forms.DockStyle.Top;
            pnlSidebarTop.Height    = 110;
            pnlSidebarTop.BackColor = System.Drawing.Color.FromArgb(40, 37, 65);
            pnlSidebarTop.Name      = "pnlSidebarTop";

            lblLogo.Text      = "COLLABBUY";
            lblLogo.Font      = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            lblLogo.ForeColor = System.Drawing.Color.FromArgb(255, 235, 133);
            lblLogo.AutoSize  = false;
            lblLogo.Size      = new System.Drawing.Size(240, 40);
            lblLogo.Location  = new System.Drawing.Point(10, 18);
            lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblLogo.Name      = "lblLogo";

            lblLogoSub.Text      = "✨ Platform Danus Kampus";
            lblLogoSub.Font      = new System.Drawing.Font("Segoe UI", 9F);
            lblLogoSub.ForeColor = System.Drawing.Color.FromArgb(170, 150, 218);
            lblLogoSub.AutoSize  = false;
            lblLogoSub.Size      = new System.Drawing.Size(240, 20);
            lblLogoSub.Location  = new System.Drawing.Point(10, 60);
            lblLogoSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblLogoSub.Name      = "lblLogoSub";

            pnlSidebarTop.Controls.Add(lblLogo);
            pnlSidebarTop.Controls.Add(lblLogoSub);

            // User info label (just below logo)
            lblUserInfo.Text      = "";
            lblUserInfo.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblUserInfo.ForeColor = System.Drawing.Color.Silver;
            lblUserInfo.AutoSize  = false;
            lblUserInfo.Size      = new System.Drawing.Size(240, 22);
            lblUserInfo.Location  = new System.Drawing.Point(10, 118);
            lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblUserInfo.Name      = "lblUserInfo";
            pnlSidebar.Controls.Add(lblUserInfo);

            // Thin divider line
            pnlDivider.BackColor = System.Drawing.Color.FromArgb(60, 57, 85);
            pnlDivider.Size      = new System.Drawing.Size(230, 1);
            pnlDivider.Location  = new System.Drawing.Point(15, 146);
            pnlDivider.Name      = "pnlDivider";
            pnlSidebar.Controls.Add(pnlDivider);

            // ── Nav button factory helper ─────────────────────────
            int navY = 158;

            System.Windows.Forms.Button MakeNavBtn(string text, System.Drawing.Color? accent = null)
            {
                var btn = new System.Windows.Forms.Button();
                btn.Text             = text;
                btn.Font             = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btn.ForeColor        = System.Drawing.Color.FromArgb(210, 210, 230);
                btn.BackColor        = System.Drawing.Color.Transparent;
                btn.FlatStyle        = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderSize         = 0;
                btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 57, 85);
                btn.TextAlign        = System.Drawing.ContentAlignment.MiddleLeft;
                btn.Padding          = new System.Windows.Forms.Padding(12, 0, 0, 0);
                btn.Size             = new System.Drawing.Size(250, 44);
                btn.Location         = new System.Drawing.Point(5, navY);
                btn.Cursor           = System.Windows.Forms.Cursors.Hand;
                btn.Visible          = false;
                navY += 46;
                return btn;
            }

            System.Windows.Forms.Label MakeSectionLabel(string text)
            {
                var lbl = new System.Windows.Forms.Label();
                lbl.Text      = text;
                lbl.Font      = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
                lbl.ForeColor = System.Drawing.Color.FromArgb(130, 120, 170);
                lbl.AutoSize  = false;
                lbl.Size      = new System.Drawing.Size(230, 22);
                lbl.Location  = new System.Drawing.Point(15, navY);
                lbl.Visible   = false;
                navY += 24;
                return lbl;
            }

            // ── ADMIN SECTION ─────────────────────────────────────
            lblSectionAdmin = MakeSectionLabel("  🛡️  ADMIN");
            btnAdminDashboard  = MakeNavBtn("📊  Dashboard Utama");
            btnAdminVerifikasi = MakeNavBtn("🆔  Verifikasi Penjual");
            btnAdminKategori   = MakeNavBtn("🏷️  Kelola Kategori");
            btnAdminKeluhan    = MakeNavBtn("🚨  Keluhan & Pengguna");

            // ── USER/BUYER SECTION ───────────────────────────────
            lblSectionBuyer = MakeSectionLabel("  🛒  PEMBELI");
            btnUserKatalog   = MakeNavBtn("🛍️  Katalog PO");
            btnUserCheckout  = MakeNavBtn("📝  Checkout Kolektif");
            btnUserRiwayat   = MakeNavBtn("🧾  Riwayat & Pembayaran");
            btnUserAduan     = MakeNavBtn("💬  Pusat Aduan & Ulasan");
            btnUserBukaLapak = MakeNavBtn("🏪  Buka Lapak Danus");

            // ── SELLER SECTION ───────────────────────────────────
            lblSectionSeller = MakeSectionLabel("  🏪  LAPAK DANUS");
            btnSellerKatalog  = MakeNavBtn("📦  Kelola Katalog & PO");
            btnSellerPesanan  = MakeNavBtn("📥  Pesanan Masuk");
            btnSellerAnalitik = MakeNavBtn("📈  Analitik Penjualan");
            btnSellerUlasan   = MakeNavBtn("📨  Balasan Ulasan");

            // ── Divider before static menu ─────────────────────
            pnlDivider2.BackColor = System.Drawing.Color.FromArgb(60, 57, 85);
            pnlDivider2.Size      = new System.Drawing.Size(230, 1);
            pnlDivider2.Location  = new System.Drawing.Point(15, navY + 4);
            pnlDivider2.Name      = "pnlDivider2";

            // ── BOTTOM STATIC BUTTONS (Docked) ───────────────────
            pnlSidebarBottom.Dock      = System.Windows.Forms.DockStyle.Bottom;
            pnlSidebarBottom.Height    = 110;
            pnlSidebarBottom.BackColor = System.Drawing.Color.FromArgb(25, 22, 45);
            pnlSidebarBottom.Name      = "pnlSidebarBottom";

            btnProfil.Text                              = "👤  Pengaturan Profil";
            btnProfil.Font                              = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnProfil.ForeColor                         = System.Drawing.Color.FromArgb(210, 210, 230);
            btnProfil.BackColor                         = System.Drawing.Color.Transparent;
            btnProfil.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            btnProfil.FlatAppearance.BorderSize         = 0;
            btnProfil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 57, 85);
            btnProfil.TextAlign                         = System.Drawing.ContentAlignment.MiddleLeft;
            btnProfil.Padding                           = new System.Windows.Forms.Padding(12, 0, 0, 0);
            btnProfil.Size                              = new System.Drawing.Size(250, 44);
            btnProfil.Location                          = new System.Drawing.Point(5, 5);
            btnProfil.Cursor                            = System.Windows.Forms.Cursors.Hand;
            btnProfil.Name                              = "btnProfil";
            btnProfil.Click                            += new System.EventHandler(this.btnProfil_Click);

            btnLogout.Text                              = "🚪  Keluar (Logout)";
            btnLogout.Font                              = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnLogout.ForeColor                         = System.Drawing.Color.White;
            btnLogout.BackColor                         = System.Drawing.Color.FromArgb(200, 60, 60);
            btnLogout.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize         = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            btnLogout.TextAlign                         = System.Drawing.ContentAlignment.MiddleLeft;
            btnLogout.Padding                           = new System.Windows.Forms.Padding(12, 0, 0, 0);
            btnLogout.Size                              = new System.Drawing.Size(250, 50);
            btnLogout.Location                          = new System.Drawing.Point(5, 54);
            btnLogout.Cursor                            = System.Windows.Forms.Cursors.Hand;
            btnLogout.Name                              = "btnLogout";
            btnLogout.Click                            += new System.EventHandler(this.btnLogout_Click);

            pnlSidebarBottom.Controls.Add(btnProfil);
            pnlSidebarBottom.Controls.Add(btnLogout);

            // ── Wire up nav click handlers ────────────────────────
            btnAdminDashboard.Click  += (s, e) => this.btnAdminDashboard_Click(s, e);
            btnAdminVerifikasi.Click += (s, e) => this.btnAdminVerifikasi_Click(s, e);
            btnAdminKategori.Click   += (s, e) => this.btnAdminKategori_Click(s, e);
            btnAdminKeluhan.Click    += (s, e) => this.btnAdminKeluhan_Click(s, e);

            btnUserKatalog.Click    += (s, e) => this.btnUserKatalog_Click(s, e);
            btnUserCheckout.Click   += (s, e) => this.btnUserCheckout_Click(s, e);
            btnUserRiwayat.Click    += (s, e) => this.btnUserRiwayat_Click(s, e);
            btnUserAduan.Click      += (s, e) => this.btnUserAduan_Click(s, e);
            btnUserBukaLapak.Click  += (s, e) => this.btnUserBukaLapak_Click(s, e);

            btnSellerKatalog.Click  += (s, e) => this.btnSellerKatalog_Click(s, e);
            btnSellerPesanan.Click  += (s, e) => this.btnSellerPesanan_Click(s, e);
            btnSellerAnalitik.Click += (s, e) => this.btnSellerAnalitik_Click(s, e);
            btnSellerUlasan.Click   += (s, e) => this.btnSellerUlasan_Click(s, e);

            // ── Add all to Sidebar ────────────────────────────────
            pnlSidebar.Controls.Add(pnlSidebarBottom);
            pnlSidebar.Controls.Add(pnlDivider2);
            pnlSidebar.Controls.Add(lblSectionSeller);
            pnlSidebar.Controls.Add(btnSellerUlasan);
            pnlSidebar.Controls.Add(btnSellerAnalitik);
            pnlSidebar.Controls.Add(btnSellerPesanan);
            pnlSidebar.Controls.Add(btnSellerKatalog);
            pnlSidebar.Controls.Add(lblSectionBuyer);
            pnlSidebar.Controls.Add(btnUserBukaLapak);
            pnlSidebar.Controls.Add(btnUserAduan);
            pnlSidebar.Controls.Add(btnUserRiwayat);
            pnlSidebar.Controls.Add(btnUserCheckout);
            pnlSidebar.Controls.Add(btnUserKatalog);
            pnlSidebar.Controls.Add(lblSectionAdmin);
            pnlSidebar.Controls.Add(btnAdminKeluhan);
            pnlSidebar.Controls.Add(btnAdminKategori);
            pnlSidebar.Controls.Add(btnAdminVerifikasi);
            pnlSidebar.Controls.Add(btnAdminDashboard);
            pnlSidebar.Controls.Add(pnlDivider);
            pnlSidebar.Controls.Add(lblUserInfo);
            pnlSidebar.Controls.Add(pnlSidebarTop);

            // ── MAIN CONTAINER ───────────────────────────────────
            pnlMainContainer.Dock      = System.Windows.Forms.DockStyle.Fill;
            pnlMainContainer.BackColor = System.Drawing.Color.White;
            pnlMainContainer.Name      = "pnlMainContainer";

            Controls.Add(pnlMainContainer);
            Controls.Add(pnlSidebar);

            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Field declarations ───────────────────────────────────
        private System.Windows.Forms.Panel   pnlSidebar, pnlSidebarTop, pnlSidebarBottom;
        private System.Windows.Forms.Panel   pnlMainContainer;
        private System.Windows.Forms.Panel   pnlDivider, pnlDivider2;
        private System.Windows.Forms.Label   lblLogo, lblLogoSub, lblUserInfo;

        private System.Windows.Forms.Label   lblSectionAdmin, lblSectionBuyer, lblSectionSeller;

        // Admin nav
        private System.Windows.Forms.Button  btnAdminDashboard, btnAdminVerifikasi;
        private System.Windows.Forms.Button  btnAdminKategori, btnAdminKeluhan;

        // User/Buyer nav
        private System.Windows.Forms.Button  btnUserKatalog, btnUserCheckout;
        private System.Windows.Forms.Button  btnUserRiwayat, btnUserAduan, btnUserBukaLapak;

        // Seller nav
        private System.Windows.Forms.Button  btnSellerKatalog, btnSellerPesanan;
        private System.Windows.Forms.Button  btnSellerAnalitik, btnSellerUlasan;

        // Static
        private System.Windows.Forms.Button  btnProfil, btnLogout;
    }
}

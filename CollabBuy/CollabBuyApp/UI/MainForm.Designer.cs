using System.Drawing;
using System.Windows.Forms;

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
            // ── Form ──
            this.Text = "CollabBuy – Solusi Danus Mahasiswa ✨";
            this.BackColor = Color.FromArgb(255, 249, 230);
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // ── Sidebar ──
            this.pnlSidebar = new Panel();
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 260;
            this.pnlSidebar.BackColor = Color.FromArgb(45, 27, 79);

            // Logo
            this.lblLogo = new Label();
            this.lblLogo.Text = "COLLABBUY";
            this.lblLogo.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.FromArgb(253, 224, 71);
            this.lblLogo.Size = new Size(240, 40);
            this.lblLogo.Location = new Point(10, 20);
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // Info user
            this.lblUserInfo = new Label();
            this.lblUserInfo.Text = "";
            this.lblUserInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblUserInfo.ForeColor = Color.FromArgb(167, 139, 250);
            this.lblUserInfo.Size = new Size(240, 20);
            this.lblUserInfo.Location = new Point(10, 65);
            this.lblUserInfo.TextAlign = ContentAlignment.MiddleCenter;

            // ── Panel Admin (4 tombol = 25 + 3×40 = 145, total tinggi 175) ──
            this.pnlAdmin = new Panel();
            this.pnlAdmin.Location = new Point(0, 100);
            this.pnlAdmin.Size = new Size(260, 185);
            this.lblAdminTitle = new Label();
            this.lblAdminTitle.Text = "👑 ADMIN";
            this.lblAdminTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblAdminTitle.ForeColor = Color.FromArgb(253, 224, 71);
            this.lblAdminTitle.Size = new Size(240, 20);
            this.lblAdminTitle.Location = new Point(10, 0);

            btnAdminDashboard = BuatTombolSidebar("🏠 Dashboard", 25);
            btnAdminVerifikasi = BuatTombolSidebar("✅ Verifikasi Penjual", 65);
            btnAdminKategori = BuatTombolSidebar("📂 Kategori", 105);
            btnAdminKeluhan = BuatTombolSidebar("📩 Keluhan", 145);
            pnlAdmin.Controls.Add(lblAdminTitle);
            pnlAdmin.Controls.Add(btnAdminDashboard);
            pnlAdmin.Controls.Add(btnAdminVerifikasi);
            pnlAdmin.Controls.Add(btnAdminKategori);
            pnlAdmin.Controls.Add(btnAdminKeluhan);

            // ── Panel Buyer (4 tombol: Katalog, Checkout, Riwayat Checkout, Aduan)
            //    Tombol BukaLapak ditambah jika belum seller → total max 5 tombol
            //    25 + 4×40 = 185, tambah BukaLapak jadi 225. Size panel = 235 ──
            this.pnlBuyer = new Panel();
            this.pnlBuyer.Location = new Point(0, 310);
            this.pnlBuyer.Size = new Size(260, 235);
            this.lblBuyerTitle = new Label();
            this.lblBuyerTitle.Text = "🛒 BUYER";
            this.lblBuyerTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblBuyerTitle.ForeColor = Color.FromArgb(253, 224, 71);
            this.lblBuyerTitle.Size = new Size(240, 20);
            this.lblBuyerTitle.Location = new Point(10, 0);

            btnUserKatalog = BuatTombolSidebar("🛍️ Katalog", 25);
            btnUserCheckout = BuatTombolSidebar("💳 Checkout", 65);
            btnUserRiwayat = BuatTombolSidebar("🧾 Riwayat Checkout", 105);
            btnUserAduan = BuatTombolSidebar("📝 Aduan", 145);
            btnUserBukaLapak = BuatTombolSidebar("🚀 Buka Lapak", 185);
            pnlBuyer.Controls.Add(lblBuyerTitle);
            pnlBuyer.Controls.Add(btnUserKatalog);
            pnlBuyer.Controls.Add(btnUserCheckout);
            pnlBuyer.Controls.Add(btnUserRiwayat);
            pnlBuyer.Controls.Add(btnUserAduan);
            pnlBuyer.Controls.Add(btnUserBukaLapak);

            // ── Panel Seller (4 tombol = 25 + 3×40 = 145, total tinggi 175) ──
            this.pnlSeller = new Panel();
            this.pnlSeller.Location = new Point(0, 560);
            this.pnlSeller.Size = new Size(260, 175);
            this.lblSellerTitle = new Label();
            this.lblSellerTitle.Text = "🏪 PENJUAL";
            this.lblSellerTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSellerTitle.ForeColor = Color.FromArgb(253, 224, 71);
            this.lblSellerTitle.Size = new Size(240, 20);
            this.lblSellerTitle.Location = new Point(10, 0);

            btnSellerKatalog = BuatTombolSidebar("📦 Kelola Produk & PO", 25);
            btnSellerPesanan = BuatTombolSidebar("📋 Pesanan Masuk", 65);
            btnSellerAnalitik = BuatTombolSidebar("📊 Analitik", 105);
            btnSellerUlasan = BuatTombolSidebar("⭐ Ulasan", 145);
            pnlSeller.Controls.Add(lblSellerTitle);
            pnlSeller.Controls.Add(btnSellerKatalog);
            pnlSeller.Controls.Add(btnSellerPesanan);
            pnlSeller.Controls.Add(btnSellerAnalitik);
            pnlSeller.Controls.Add(btnSellerUlasan);

            // ── Tombol statis (posisi Y diatur dinamis di AturSidebarBerdasarkanPeran) ──
            btnProfil = BuatTombolSidebar("👤 Profil", 630);
            btnLogout = BuatTombolSidebar("🚪 Logout", 670);

            // ── Event handlers ──
            btnAdminDashboard.Click += btnAdminDashboard_Click;
            btnAdminVerifikasi.Click += btnAdminVerifikasi_Click;
            btnAdminKategori.Click += btnAdminKategori_Click;
            btnAdminKeluhan.Click += btnAdminKeluhan_Click;
            btnUserKatalog.Click += btnUserKatalog_Click;
            btnUserCheckout.Click += btnUserCheckout_Click;
            btnUserRiwayat.Click += btnUserRiwayat_Click;
            btnUserAduan.Click += btnUserAduan_Click;
            btnUserBukaLapak.Click += btnUserBukaLapak_Click;
            btnSellerKatalog.Click += btnSellerKatalog_Click;
            btnSellerPesanan.Click += btnSellerPesanan_Click;
            btnSellerAnalitik.Click += btnSellerAnalitik_Click;
            btnSellerUlasan.Click += btnSellerUlasan_Click;
            btnProfil.Click += btnProfil_Click;
            btnLogout.Click += btnLogout_Click;

            // ── Container utama ──
            pnlMainContainer = new Panel();
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.BackColor = Color.FromArgb(255, 249, 230);

            // Tambah ke sidebar
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Controls.Add(lblUserInfo);
            pnlSidebar.Controls.Add(pnlAdmin);
            pnlSidebar.Controls.Add(pnlBuyer);
            pnlSidebar.Controls.Add(pnlSeller);
            pnlSidebar.Controls.Add(btnProfil);
            pnlSidebar.Controls.Add(btnLogout);

            // Tambah ke form
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlSidebar);
        }

        private Button BuatTombolSidebar(string teks, int y)
        {
            Button btn = new Button();
            btn.Text = teks;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F);
            btn.ForeColor = Color.FromArgb(210, 210, 230);
            btn.Size = new Size(240, 35);
            btn.Location = new Point(10, y);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            return btn;
        }

        // ── Deklarasi kontrol ──
        private Panel pnlSidebar, pnlMainContainer;
        private Panel pnlAdmin, pnlBuyer, pnlSeller;
        private Label lblLogo, lblUserInfo, lblAdminTitle, lblBuyerTitle, lblSellerTitle;
        private Button btnAdminDashboard, btnAdminVerifikasi, btnAdminKategori, btnAdminKeluhan;
        private Button btnUserKatalog, btnUserCheckout, btnUserRiwayat, btnUserAduan, btnUserBukaLapak;
        private Button btnSellerKatalog, btnSellerPesanan, btnSellerAnalitik, btnSellerUlasan;
        private Button btnProfil, btnLogout;
    }
}
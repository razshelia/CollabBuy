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
            this.Text = "CollabBuy – Solusi Danus Mahasiswa ✨";
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // ── Sidebar Container ──
            this.pnlSidebar = new Panel();
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 260;
            this.pnlSidebar.BackColor = Color.FromArgb(36, 0, 70); // Dark Purple Retro Solid

            // Brand Logo Utama
            this.lblLogo = new Label();
            this.lblLogo.Text = "COLLABBUY ✨";
            this.lblLogo.Font = new Font("Segoe UI Black", 20F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.FromArgb(253, 255, 182); // Kuning Pastel
            this.lblLogo.Size = new Size(240, 45);
            this.lblLogo.Location = new Point(10, 20);
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // Info Label Akun Aktif
            this.lblUserInfo = new Label();
            this.lblUserInfo.Text = "";
            this.lblUserInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblUserInfo.ForeColor = Color.FromArgb(200, 182, 255); // Ungu Pastel
            this.lblUserInfo.Size = new Size(240, 25);
            this.lblUserInfo.Location = new Point(10, 70);
            this.lblUserInfo.TextAlign = ContentAlignment.MiddleCenter;

            // ── Panel Menu Admin ──
            this.pnlAdmin = new Panel();
            this.pnlAdmin.Location = new Point(0, 110);
            this.pnlAdmin.Size = new Size(260, 195);
            this.lblAdminTitle = new Label();
            this.lblAdminTitle.Text = "⚡ MANAGEMENT";
            this.lblAdminTitle.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            this.lblAdminTitle.ForeColor = Color.FromArgb(253, 255, 182);
            this.lblAdminTitle.Size = new Size(240, 20);
            this.lblAdminTitle.Location = new Point(15, 0);

            btnAdminDashboard = BuatTombolSidebar("🏠 Dashboard Admin", 25);
            btnAdminVerifikasi = BuatTombolSidebar("✅ Verifikasi Toko", 65);
            btnAdminKategori = BuatTombolSidebar("📂 Manajemen Kategori", 105);
            btnAdminKeluhan = BuatTombolSidebar("📩 Aduan Masuk", 145);
            pnlAdmin.Controls.Add(lblAdminTitle);
            pnlAdmin.Controls.Add(btnAdminDashboard);
            pnlAdmin.Controls.Add(btnAdminVerifikasi);
            pnlAdmin.Controls.Add(btnAdminKategori);
            pnlAdmin.Controls.Add(btnAdminKeluhan);

            // ── Panel Menu Buyer ──
            this.pnlBuyer = new Panel();
            this.pnlBuyer.Location = new Point(0, 320);
            this.pnlBuyer.Size = new Size(260, 240);
            this.lblBuyerTitle = new Label();
            this.lblBuyerTitle.Text = "🛒 MENU PENITIP";
            this.lblBuyerTitle.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            this.lblBuyerTitle.ForeColor = Color.FromArgb(253, 255, 182);
            this.lblBuyerTitle.Size = new Size(240, 20);
            this.lblBuyerTitle.Location = new Point(15, 0);

            btnUserKatalog = BuatTombolSidebar("🛍️ Katalog Utama", 25);
            btnUserCheckout = BuatTombolSidebar("💳 Sesi Checkout", 65);
            btnUserRiwayat = BuatTombolSidebar("🧾 Riwayat Transaksi", 105);
            btnUserAduan = BuatTombolSidebar("📝 Spill Kendala", 145);
            btnUserBukaLapak = BuatTombolSidebar("🚀 Buka Lapak Danus", 185);
            pnlBuyer.Controls.Add(lblBuyerTitle);
            pnlBuyer.Controls.Add(btnUserKatalog);
            pnlBuyer.Controls.Add(btnUserCheckout);
            pnlBuyer.Controls.Add(btnUserRiwayat);
            pnlBuyer.Controls.Add(btnUserAduan);
            pnlBuyer.Controls.Add(btnUserBukaLapak);

            // ── Panel Menu Seller ──
            this.pnlSeller = new Panel();
            this.pnlSeller.Location = new Point(0, 570);
            this.pnlSeller.Size = new Size(260, 240);
            this.lblSellerTitle = new Label();
            this.lblSellerTitle.Text = "🏪 MENU SELLER";
            this.lblSellerTitle.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            this.lblSellerTitle.ForeColor = Color.FromArgb(253, 255, 182);
            this.lblSellerTitle.Size = new Size(240, 20);
            this.lblSellerTitle.Location = new Point(15, 0);

            btnSellerProduk = BuatTombolSidebar("📦 Produk Master", 25);
            btnSellerPO = BuatTombolSidebar("📢 Sesi PO Jualan", 65);
            btnSellerPesanan = BuatTombolSidebar("🛒 Pesanan Masuk", 105);
            btnSellerAnalitik = BuatTombolSidebar("📊 Analitik Lapak", 145);
            btnSellerUlasan = BuatTombolSidebar("⭐ Ulasan Bintang", 185);
            pnlSeller.Controls.Add(lblSellerTitle);
            pnlSeller.Controls.Add(btnSellerProduk);
            pnlSeller.Controls.Add(btnSellerPO);
            pnlSeller.Controls.Add(btnSellerPesanan);
            pnlSeller.Controls.Add(btnSellerAnalitik);
            pnlSeller.Controls.Add(btnSellerUlasan);

            // Tombol Statis Bawah
            btnProfil = BuatTombolSidebar("👤 Kelola Profil", 630);
            btnLogout = BuatTombolSidebar("🚪 Keluar Aplikasi", 670);

            // Memasang Event Click Navigasi
            btnAdminDashboard.Click += btnAdminDashboard_Click;
            btnAdminVerifikasi.Click += btnAdminVerifikasi_Click;
            btnAdminKategori.Click += btnAdminKategori_Click;
            btnAdminKeluhan.Click += btnAdminKeluhan_Click;
            btnUserKatalog.Click += btnUserKatalog_Click;
            btnUserCheckout.Click += btnUserCheckout_Click;
            btnUserRiwayat.Click += btnUserRiwayat_Click;
            btnUserAduan.Click += btnUserAduan_Click;
            btnUserBukaLapak.Click += btnUserBukaLapak_Click;
            btnSellerProduk.Click += btnSellerProduk_Click;
            btnSellerPO.Click += btnSellerPO_Click;
            btnSellerPesanan.Click += btnSellerPesanan_Click;
            btnSellerAnalitik.Click += btnSellerAnalitik_Click;
            btnSellerUlasan.Click += btnSellerUlasan_Click;
            btnProfil.Click += btnProfil_Click;
            btnLogout.Click += btnLogout_Click;

            // ── Main Content Container ──
            pnlMainContainer = new Panel();
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.BackColor = Color.FromArgb(248, 249, 250);

            // Memasukkan kontrol ke dalam Sidebar
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Controls.Add(lblUserInfo);
            pnlSidebar.Controls.Add(pnlAdmin);
            pnlSidebar.Controls.Add(pnlBuyer);
            pnlSidebar.Controls.Add(pnlSeller);
            pnlSidebar.Controls.Add(btnProfil);
            pnlSidebar.Controls.Add(btnLogout);

            // Memasukkan panel utama ke Windows Form
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlSidebar);
        }

        private Button BuatTombolSidebar(string teks, int y)
        {
            Button btn = new Button();
            btn.Text = teks;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.ForeColor = Color.FromArgb(200, 182, 255); // Default Text Color
            btn.Size = new Size(230, 36);
            btn.Location = new Point(15, y);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        // Variabel Kontrol Form
        private Panel pnlSidebar, pnlMainContainer;
        private Panel pnlAdmin, pnlBuyer, pnlSeller;
        private Label lblLogo, lblUserInfo, lblAdminTitle, lblBuyerTitle, lblSellerTitle;
        private Button btnAdminDashboard, btnAdminVerifikasi, btnAdminKategori, btnAdminKeluhan;
        private Button btnUserKatalog, btnUserCheckout, btnUserRiwayat, btnUserAduan, btnUserBukaLapak;
        private Button btnSellerProduk, btnSellerPO, btnSellerPesanan, btnSellerAnalitik, btnSellerUlasan;
        private Button btnProfil, btnLogout;
    }
}
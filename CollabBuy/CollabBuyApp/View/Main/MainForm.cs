using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.View.Admin;
using CollabBuy.CollabBuyApp.View.Feedback;
using CollabBuy.CollabBuyApp.View.Main;
using CollabBuy.CollabBuyApp.View.PreOrder;
using CollabBuy.CollabBuyApp.View.Product;
using CollabBuy.CollabBuyApp.View.Report;
using CollabBuy.CollabBuyApp.View.Transaction;
using CollabBuy.CollabBuyApp.View.UserDashboard;

namespace CollabBuy.CollabBuyApp.View.Main
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private readonly UserController _userController;
        private Panel pnlContent;
        private Panel pnlSidebar;
        private Label lblUserInfo;

        public MainForm()
        {
            InitializeComponent();
            _userController = new UserController();

            // Setup Form Global
            this.Text = "CollabBuy v1.0 - Sistem Agregator Dana Usaha (Danus) Mahasiswa";
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250); // Background Putih Keabuan

            CreateUI();
        }

        private void CreateUI()
        {
            // === SIDEBAR (Kiri) ===
            pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 260,
                BackColor = System.Drawing.Color.FromArgb(36, 0, 70), // Dark Purple
                AutoScroll = true
            };
            this.Controls.Add(pnlSidebar);

            // === CONTENT AREA (Kanan) ===
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(248, 249, 250),
                Padding = new Padding(10)
            };
            this.Controls.Add(pnlContent);

            ShowLoginControl();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void ShowLoginControl()
        {
            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();

            // Logo sederhana saat di menu login
            Label lblLogo = new Label
            {
                Text = "CollabBuy",
                ForeColor = System.Drawing.Color.FromArgb(253, 255, 182),
                Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 150,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            pnlSidebar.Controls.Add(lblLogo);

            LoginControl loginCtrl = new LoginControl();
            loginCtrl.OnLoginSuccess += HandleLoginSuccess;
            loginCtrl.OnNavigateToRegister += () => ShowRegisterControl();
            loginCtrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(loginCtrl);
        }

        private void ShowRegisterControl()
        {
            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();

            RegisterControl registerCtrl = new RegisterControl();
            registerCtrl.OnRegistrationComplete += (s, e) => ShowLoginControl();

            registerCtrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(registerCtrl);
        }

        private void ShowDashboard()
        {
            if (_currentUser == null) return;

            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();

            BuildSidebarMenu();

            string peran = _currentUser.GetPeran();

            if (peran == "Admin")
            {
                DashboardAdminControl adminDash = new DashboardAdminControl();
                adminDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(adminDash);
            }
            else if (peran == "Penjual")
            {
                DashboardUserControl sellerDash = new DashboardUserControl(_currentUser);
                sellerDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(sellerDash);
            }
            else
            {
                DashboardUserControl buyerDash = new DashboardUserControl(_currentUser);
                buyerDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(buyerDash);
            }
        }

        private void BuildSidebarMenu()
        {
            pnlSidebar.Controls.Clear();

            // 1. LOGO UTAMA
            Label lblLogo = new Label
            {
                Text = "CollabBuy",
                ForeColor = System.Drawing.Color.FromArgb(253, 255, 182), // Kuning Pastel
                Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 15, 0, 0)
            };
            pnlSidebar.Controls.Add(lblLogo);

            // 2. USER INFO
            lblUserInfo = new Label
            {
                Text = "Halo, " + (_currentUser != null ? _currentUser.GetNama() : "User"),
                ForeColor = System.Drawing.Color.FromArgb(200, 182, 255), // Ungu Pastel
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, 10)
            };
            pnlSidebar.Controls.Add(lblUserInfo);

            // Fungsi helper pembuat tombol
            Action<string, Action> AddMenuButton = (text, onClick) =>
            {
                Button btn = new Button
                {
                    Text = "   " + text,
                    Dock = DockStyle.Top,
                    Height = 45,
                    BackColor = System.Drawing.Color.FromArgb(36, 0, 70), // Dark Purple
                    ForeColor = System.Drawing.Color.FromArgb(200, 182, 255), // Ungu Pastel
                    FlatStyle = FlatStyle.Flat,
                    Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) => onClick();
                btn.MouseEnter += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(70, 20, 110);
                btn.MouseLeave += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
                pnlSidebar.Controls.Add(btn);
                btn.BringToFront();
            };

            // Fungsi helper pembuat label kategori
            Action<string> AddCategoryLabel = (text) =>
            {
                Label lblCat = new Label
                {
                    Text = text,
                    ForeColor = System.Drawing.Color.FromArgb(253, 255, 182), // Kuning Pastel
                    Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 35,
                    TextAlign = System.Drawing.ContentAlignment.BottomLeft,
                    Padding = new Padding(10, 0, 0, 5)
                };
                pnlSidebar.Controls.Add(lblCat);
                lblCat.BringToFront();
            };

            lblLogo.BringToFront();
            lblUserInfo.BringToFront();

            string peran = _currentUser.GetPeran();

            // Menu Universal
            AddMenuButton("📊 Dashboard", ShowDashboard);
            AddMenuButton("👤 Kelola Profil", () => ShowUserControl(new KelolaProfilControl(_currentUser)));

            // === MENU BERDASARKAN ROLE ===
            if (peran == "Admin")
            {
                AddCategoryLabel("MANAGEMENT");
                AddMenuButton("🏢 Verifikasi Toko", () => ShowUserControl(new VerifikasiTokoControl()));
                AddMenuButton("📁 Kelola Kategori", () => ShowUserControl(new KelolaKategoriControl()));
                AddMenuButton("📣 Tanggapan Aduan", () => ShowUserControl(new TanggapanAduanControl()));
                AddMenuButton("📊 Laporan Sistem", () => ShowUserControl(new AnalitikPenjualanControl(_currentUser)));
            }
            else if (peran == "Penjual")
            {
                AddCategoryLabel("SELLER");
                AddMenuButton("📦 Manajemen Produk", () => ShowUserControl(new ManajemenProdukControl(_currentUser)));
                AddMenuButton("🎁 Buka Sesi PO", () => ShowUserControl(new BukaSesiPOControl(_currentUser)));
                AddMenuButton("📋 Sesi PO Aktif", () => ShowUserControl(new SesiPOAktifControl(_currentUser)));
                AddMenuButton("📥 Pesanan Masuk", () => ShowUserControl(new PesananMasukControl(_currentUser)));
                AddMenuButton("⭐ Balas Ulasan", () => ShowUserControl(new UlasanLapakControl(_currentUser)));
                AddMenuButton("📊 Analitik Penjualan", () => ShowUserControl(new AnalitikPenjualanControl(_currentUser)));
            }
            else
            {
                AddCategoryLabel("BUYER");
                AddMenuButton("🏪 Katalog Produk", () => ShowUserControl(new KatalogProdukControl(_currentUser)));
                AddMenuButton("🛒 Keranjang Belanja", () => ShowUserControl(new KeranjangBelanjaControl(_currentUser)));
                AddMenuButton("📋 Riwayat Pesanan", () => ShowUserControl(new RiwayatPesananControl(_currentUser)));
                AddMenuButton("⭐ Beri Ulasan", () => ShowUserControl(new BeriUlasanControl(_currentUser)));
                AddMenuButton("📝 Laporkan Kendala", () => ShowUserControl(new SpillKendalaControl(_currentUser)));
                AddMenuButton("🏢 Daftar Toko", () => ShowUserControl(new DaftarTokoControl(_currentUser)));
            }

            // Tombol Logout dikunci ke bagian bawah (Bottom)
            Button btnLogout = new Button
            {
                Text = "🚪 Logout",
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = System.Drawing.Color.FromArgb(253, 100, 100),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => HandleLogout();
            pnlSidebar.Controls.Add(btnLogout);
        }

        private void ShowUserControl(UserControl control)
        {
            pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
        }

        private void HandleLoginSuccess(User user)
        {
            _currentUser = user;
            SetCurrentUser(user);
            ShowDashboard();
        }

        private void SetCurrentUser(User user)
        {
            if (user == null)
            {
                if (lblUserInfo != null) lblUserInfo.Text = "Status: Belum Login";
                return;
            }

            string roleEmoji = user.GetPeran() switch
            {
                "Admin" => "👮",
                "Penjual" => "🏪",
                _ => "👤"
            };

            if (lblUserInfo != null)
                lblUserInfo.Text = $"{roleEmoji} {user.GetNama()} ({user.GetPeran()})";
        }

        private void HandleLogout()
        {
            DialogResult dr = MessageBox.Show(
                "Apakah Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr == DialogResult.Yes)
            {
                _currentUser = null;
                ShowLoginControl();
            }
        }
    }
}
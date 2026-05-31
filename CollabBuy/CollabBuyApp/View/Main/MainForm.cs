using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

// MENGGUNAKAN ALIAS UNTUK MENGHINDARI BENTROK NAMA (AMBIGUITY)
using ViewAdmin = CollabBuy.CollabBuyApp.View.Admin;
using ViewFeedback = CollabBuy.CollabBuyApp.View.Feedback;
using ViewPreOrder = CollabBuy.CollabBuyApp.View.PreOrder;
using ViewProduct = CollabBuy.CollabBuyApp.View.Product;
using ViewReport = CollabBuy.CollabBuyApp.View.Report;
using ViewTransaction = CollabBuy.CollabBuyApp.View.Transaction;
using ViewUser = CollabBuy.CollabBuyApp.View.UserDashboard;

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
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.WindowState = FormWindowState.Maximized;
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
     

            // === CONTENT AREA (Kanan) ===
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(248, 249, 250),
                Padding = new Padding(10),
                AutoScroll = true
            };
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlSidebar);

            ShowLoginControl();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void ShowLoginControl()
        {
            pnlContent.Controls.Clear();
            pnlSidebar.Visible = false;

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
            pnlSidebar.Visible = false;

            RegisterControl registerCtrl = new RegisterControl();
            registerCtrl.OnRegistrationComplete += (s, e) => ShowLoginControl();

            registerCtrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(registerCtrl);
        }

        private void ShowDashboard()
        {
            if (_currentUser == null) return;

            pnlSidebar.Visible = true;
            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();

            BuildSidebarMenu();

            string peran = _currentUser.GetPeran();

            if (peran == "Admin")
            {
                // PERBAIKAN: Menambahkan _currentUser sebagai parameter
                ViewAdmin.DashboardAdminControl adminDash = new ViewAdmin.DashboardAdminControl(_currentUser);
                adminDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(adminDash);
            }
            else if (peran == "Penjual")
            {
                ViewUser.DashboardUserControl sellerDash = new ViewUser.DashboardUserControl(_currentUser);
                sellerDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(sellerDash);
            }
            else
            {
                ViewUser.DashboardUserControl buyerDash = new ViewUser.DashboardUserControl(_currentUser);
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
                Padding = new Padding(20, 15, 0, 0)
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
            AddMenuButton("👤 Kelola Profil", () => ShowUserControl(new ViewUser.KelolaProfilControl(_currentUser)));

            // === MENU BERDASARKAN ROLE ===
            if (peran == "Admin")
            {
                AddCategoryLabel("MANAGEMENT");
                AddMenuButton("🏢 Verifikasi Toko", () => ShowUserControl(new ViewAdmin.VerifikasiTokoControl()));
                AddMenuButton("📁 Kelola Kategori", () => ShowUserControl(new ViewAdmin.KelolaKategoriControl()));
                AddMenuButton("📣 Kelola Aduan", () => ShowUserControl(new ViewAdmin.TanggapanAduanControl(_currentUser)));
                AddMenuButton("👥 Kelola User", () => ShowUserControl(new ViewAdmin.KelolaUserControl()));
                AddMenuButton("📊 Laporan Sistem", () => ShowUserControl(new ViewReport.AnalitikPenjualanControl(_currentUser)));
                AddMenuButton("📋 Log Aktivitas", () => ShowUserControl(new ViewAdmin.LogAktivitasControl()));
            }
            else if (peran == "Penjual")
            {
                AddCategoryLabel("SELLER");
                AddMenuButton("📦 Manajemen Produk", () => ShowUserControl(new ViewProduct.ManajemenProdukControl(_currentUser)));
                AddMenuButton("🎁 Buka Sesi PO", () => ShowUserControl(new ViewPreOrder.BukaSesiPOControl(_currentUser)));
                AddMenuButton("📋 Sesi PO Aktif", () => ShowUserControl(new ViewPreOrder.SesiPOAktifControl(_currentUser)));
                AddMenuButton("📥 Pesanan Masuk", () => ShowUserControl(new ViewTransaction.PesananMasukControl(_currentUser)));
                AddMenuButton("⭐ Balas Ulasan", () => ShowUserControl(new ViewFeedback.UlasanLapakControl(_currentUser)));
                AddMenuButton("📊 Analitik Penjualan", () => ShowUserControl(new ViewReport.AnalitikPenjualanControl(_currentUser)));
            }
            else
            {
                AddCategoryLabel("BUYER");
                AddMenuButton("🏪 Katalog Produk", () => ShowKatalogProduk());
                AddMenuButton("🛒 Keranjang Belanja", () => ShowKeranjangBelanja());
                AddMenuButton("📋 Riwayat Pesanan", () => ShowUserControl(new ViewTransaction.RiwayatPesananControl(_currentUser)));
                AddMenuButton("⭐ Beri Ulasan", () => ShowUserControl(new ViewFeedback.BeriUlasanControl(_currentUser)));
                AddMenuButton("📝 Laporkan Kendala", () => ShowUserControl(new ViewFeedback.SpillKendalaControl(_currentUser)));
                AddMenuButton("🏢 Daftar Toko", () => ShowUserControl(new ViewUser.DaftarTokoControl(_currentUser)));
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

            PictureBox picLogoKecil = new PictureBox
            {
                Image = System.Drawing.Image.FromFile("logo_sidebar.jpeg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(40, 60),
                Location = new System.Drawing.Point(10, 12), // Jarak dari kiri dan atas
                BackColor = System.Drawing.Color.Transparent
            };
            pnlSidebar.Controls.Add(picLogoKecil);
            picLogoKecil.BringToFront();
        }

        private void ShowKatalogProduk()
        {
            var ctrl = new ViewProduct.KatalogProdukControl(_currentUser);
            ctrl.OnLihatDetail += (idProduk) =>
            {
                // Halaman detail produk belum dibuat.
                // Saat sudah ada, ganti MessageBox ini dengan:
                // ShowUserControl(new ViewProduct.DetailProdukControl(_currentUser, idProduk));
                System.Windows.Forms.MessageBox.Show(
                    "Halaman detail produk sedang dikembangkan.\nID Produk: " + idProduk,
                    "Coming Soon",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            };
            ShowUserControl(ctrl);
        }

        private void ShowKeranjangBelanja()
        {
            var ctrl = new ViewTransaction.KeranjangBelanjaControl(_currentUser);
            ctrl.OnCheckoutBerhasil += (idTransaksi, totalTagihan) =>
            {
                ShowPembayaran(idTransaksi, totalTagihan);
            };
            ShowUserControl(ctrl);
        }

        private void ShowPembayaran(int idTransaksi, long totalTagihan)
        {
            var ctrl = new ViewTransaction.PembayaranControl(_currentUser, idTransaksi, totalTagihan);
            ctrl.OnPembayaranSelesai += () =>
            {
                ShowUserControl(new ViewTransaction.RiwayatPesananControl(_currentUser));
            };
            ShowUserControl(ctrl);
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
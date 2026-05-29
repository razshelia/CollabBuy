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
    /// <summary>
    /// MainForm: Shell utama aplikasi CollabBuy.
    /// Menjalankan prinsip desain Repository Pattern + MVC:
    /// 
    /// Tanggung Jawab:
    /// - Menampilkan user yang sedang login di info bar
    /// - Menampilkan menu navigasi dinamis sesuai role (Pembeli, Penjual, Admin)
    /// - Mengelola switching UserControl (CardLayout pattern) di content area
    /// - Menangani logout dan kembali ke form login
    /// - Tidak boleh memuat logika bisnis atau query database langsung
    /// 
    /// Arsitektur (Sesuai PRD):
    /// View Layer ← MainForm menampilkan tampilan dan tangkap event
    /// Controller Layer ← UserController menangani login/logout
    /// Model Layer ← User, Pembeli, Penjual berisi business logic
    /// Repository Layer ← UserRepository menangani database
    /// </summary>
    public partial class MainForm : Form
    {
        // === FIELD PENYIMPANAN STATE ===
        /// <summary>
        /// Menyimpan referensi user yang sedang login.
        /// Digunakan untuk filter menu navigasi berdasarkan role.
        /// </summary>
        private User _currentUser;

        /// <summary>
        /// Controller untuk fungsi login/logout.
        /// MainForm tidak query database langsung (Repository Pattern).
        /// Semua akses data didelegasikan ke Controller.
        /// </summary>
        private readonly UserController _userController;

        /// <summary>
        /// Panel utama (content area) untuk menampung UserControl secara bergantian.
        /// Menggunakan prinsip single responsibility: MainForm hanya switch UserControl,
        /// detail UI ditangani oleh masing-masing UserControl.
        /// </summary>
        private Panel pnlContent;

        /// <summary>
        /// Panel sidebar untuk menampilkan tombol navigasi yang dinamis.
        /// </summary>
        private Panel pnlSidebar;

        /// <summary>
        /// Label untuk menampilkan nama user yang sedang login.
        /// Update otomatis saat user berganti melalui method SetCurrentUser().
        /// </summary>
        private Label lblUserInfo;


        // === KONSTRUKTOR ===
        public MainForm()
        {
            InitializeComponent();
            _userController = new UserController();

            // Dekorasi form
            this.Text = "CollabBuy v1.0 - Sistem Agregator Dana Usaha (Danus) Mahasiswa";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Setup UI
            CreateUI();
        }


        // === METHOD LIFECYCLE ===

        /// <summary>
        /// Membangun UI MainForm secara programmatik.
        /// Alternatif dari Designer untuk kontrol lebih detail.
        /// </summary>
        private void CreateUI()
        {
            // === INFO BAR (Top) ===
            Panel pnlTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = System.Drawing.Color.FromArgb(36, 0, 70)
            };

            Label lblLogo = new Label
            {
                Text = "🛒 CollabBuy",
                ForeColor = System.Drawing.Color.FromArgb(253, 255, 182),
                Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Left,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            pnlTopBar.Controls.Add(lblLogo);

            lblUserInfo = new Label
            {
                Text = "Tidak Ada User",
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                Dock = DockStyle.Right,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 20, 0)
            };
            pnlTopBar.Controls.Add(lblUserInfo);

            Button btnLogout = new Button
            {
                Text = "🚪 Logout",
                Dock = DockStyle.Right,
                Width = 100,
                BackColor = System.Drawing.Color.FromArgb(253, 100, 100),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
            };
            btnLogout.Click += (s, e) => HandleLogout();
            pnlTopBar.Controls.Add(btnLogout);

            this.Controls.Add(pnlTopBar);

            // === SIDEBAR (Left) ===
            pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = System.Drawing.Color.FromArgb(250, 250, 250),
                AutoScroll = true
            };
            this.Controls.Add(pnlSidebar);

            // === CONTENT AREA (Center) ===
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White,
                Padding = new Padding(10)
            };
            this.Controls.Add(pnlContent);

            // Show login form awalnya
            ShowLoginControl();
        }

        /// <summary>
        /// Event Form Load: Inisialisasi aplikasi.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Startup logic (jika diperlukan)
        }


        // === METHOD NAVIGASI VIEW ===

        /// <summary>
        /// Menampilkan LoginControl di content area.
        /// Diakses saat:
        /// 1. Form pertama kali dibuka
        /// 2. User menekan tombol Logout
        /// </summary>
        private void ShowLoginControl()
        {
            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();
            lblUserInfo.Text = "Status: Belum Login";

            LoginControl loginCtrl = new LoginControl();
            // CUSTOM EVENT: LoginControl memanggil method ini saat login sukses
            // (Implementasi di LoginControl.cs harus memiliki event atau callback)
            loginCtrl.OnLoginSuccess += HandleLoginSuccess;

            loginCtrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(loginCtrl);
        }

        /// <summary>
        /// Menampilkan RegisterControl di content area.
        /// Diakses saat user klik tombol "Daftar" di LoginControl.
        /// </summary>
        private void ShowRegisterControl()
        {
            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();

            RegisterControl registerCtrl = new RegisterControl();
            registerCtrl.OnRegistrationComplete += (s, e) => ShowLoginControl();

            registerCtrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(registerCtrl);
        }

        /// <summary>
        /// Menampilkan halaman utama (dashboard) sesuai role user.
        /// Membangun menu navigasi dinamis di sidebar berdasarkan role.
        /// </summary>
        private void ShowDashboard()
        {
            if (_currentUser == null) return;

            pnlContent.Controls.Clear();
            pnlSidebar.Controls.Clear();

            // === SETUP SIDEBAR MENU (DINAMIS BERDASARKAN ROLE) ===
            BuildSidebarMenu();

            // === TAMPILKAN DASHBOARD SESUAI ROLE ===
            string peran = _currentUser.GetPeran();

            if (peran == "Admin")
            {
                // Admin Dashboard
                DashboardAdminControl adminDash = new DashboardAdminControl();
                adminDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(adminDash);
            }
            else if (peran == "Penjual")
            {
                // Penjual Dashboard
                DashboardUserControl sellerDash = new DashboardUserControl(_currentUser);
                sellerDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(sellerDash);
            }
            else
            {
                // Pembeli/Koordinator Dashboard
                DashboardUserControl buyerDash = new DashboardUserControl(_currentUser);
                buyerDash.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(buyerDash);
            }
        }

        /// <summary>
        /// Membangun menu navigasi sidebar secara dinamis sesuai role user.
        /// Implementasi prinsip Role-Based Access Control (RBAC).
        /// </summary>
        private void BuildSidebarMenu()
        {
            pnlSidebar.Controls.Clear();

            string peran = _currentUser.GetPeran();

            // Button factory untuk mengurangi duplikasi kode
            Action<string, Action> AddMenuButton = (text, onClick) =>
            {
                Button btn = new Button
                {
                    Text = text,
                    Dock = DockStyle.Top,
                    Height = 50,
                    BackColor = System.Drawing.Color.FromArgb(250, 250, 250),
                    ForeColor = System.Drawing.Color.FromArgb(36, 0, 70),
                    FlatStyle = FlatStyle.Flat,
                    Font = new System.Drawing.Font("Segoe UI", 10F),
                    Cursor = Cursors.Hand
                };
                btn.Click += (s, e) => onClick();
                btn.MouseEnter += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
                btn.MouseLeave += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
                pnlSidebar.Controls.Add(btn);
            };

            // === MENU UNIVERSAL (SEMUA ROLE) ===
            AddMenuButton("📊 Dashboard", ShowDashboard);
            AddMenuButton("👤 Kelola Profil", () => ShowUserControl(new KelolaProfilControl(_currentUser)));

            // === MENU BERDASARKAN ROLE ===
            if (peran == "Admin")
            {
                AddMenuButton("🏢 Verifikasi Toko", () => ShowUserControl(new VerifikasiTokoControl()));
                AddMenuButton("📁 Kelola Kategori", () => ShowUserControl(new KelolaKategoriControl()));
                AddMenuButton("📣 Tanggapan Aduan", () => ShowUserControl(new TanggapanAduanControl()));
                AddMenuButton("📊 Laporan Sistem", () => ShowUserControl(new AnalitikPenjualanControl(_currentUser)));
            }
            else if (peran == "Penjual")
            {
                AddMenuButton("📦 Manajemen Produk", () => ShowUserControl(new ManajemenProdukControl(_currentUser)));
                AddMenuButton("🎁 Buka Sesi PO", () => ShowUserControl(new BukaSesiPOControl(_currentUser)));
                AddMenuButton("📋 Sesi PO Aktif", () => ShowUserControl(new SesiPOAktifControl(_currentUser)));
                AddMenuButton("📥 Pesanan Masuk", () => ShowUserControl(new PesananMasukControl(_currentUser)));
                AddMenuButton("⭐ Balas Ulasan", () => ShowUserControl(new UlasanLapakControl(_currentUser)));
                AddMenuButton("📊 Analitik Penjualan", () => ShowUserControl(new AnalitikPenjualanControl(_currentUser)));
            }
            else // Pembeli/Koordinator
            {
                AddMenuButton("🏪 Katalog Produk", () => ShowUserControl(new KatalogProdukControl(_currentUser)));
                AddMenuButton("🛒 Keranjang Belanja", () => ShowUserControl(new KeranjangBelanjaControl(_currentUser)));
                AddMenuButton("📋 Riwayat Pesanan", () => ShowUserControl(new RiwayatPesananControl(_currentUser)));
                AddMenuButton("⭐ Beri Ulasan", () => ShowUserControl(new BeriUlasanControl(_currentUser)));
                AddMenuButton("📝 Laporkan Kendala", () => ShowUserControl(new SpillKendalaControl(_currentUser)));
                AddMenuButton("🏢 Daftar Toko", () => ShowUserControl(new DaftarTokoControl(_currentUser)));
            }
        }

        /// <summary>
        /// Helper method untuk switch UserControl di content area.
        /// Menerapkan prinsip View yang clean: MainForm hanya container,
        /// tidak mengetahui detail logika setiap UserControl.
        /// </summary>
        private void ShowUserControl(UserControl control)
        {
            pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
        }


        // === EVENT HANDLERS ===

        /// <summary>
        /// Callback saat login berhasil.
        /// Method ini dipanggil oleh LoginControl setelah verifikasi user.
        /// </summary>
        private void HandleLoginSuccess(User user)
        {
            _currentUser = user;
            SetCurrentUser(user);
            ShowDashboard();
        }

        /// <summary>
        /// Set informasi user di info bar dan label.
        /// Diupdate setiap kali user berganti atau login.
        /// </summary>
        private void SetCurrentUser(User user)
        {
            if (user == null)
            {
                lblUserInfo.Text = "Status: Belum Login";
                return;
            }

            string roleEmoji = user.GetPeran() switch
            {
                "Admin" => "👮",
                "Penjual" => "🏪",
                _ => "👤"
            };

            lblUserInfo.Text = $"{roleEmoji} {user.GetNama()} ({user.GetPeran()})";
        }

        /// <summary>
        /// Handle logout: kembalikan ke LoginControl.
        /// Clear state user saat ini.
        /// </summary>
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

    // === CUSTOM EVENT DELEGATE ===
    // Digunakan untuk komunikasi dari LoginControl ke MainForm
    // Alternatif modern: gunakan event dengan EventArgs
    public delegate void LoginSuccessEventHandler(User user);
}
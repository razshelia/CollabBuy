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
            this.InitializeComponent();
            this._userController = new UserController();

            // Setup Form Global
            this.Text = "CollabBuy v1.0 - Sistem Agregator Dana Usaha (Danus) Mahasiswa";
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250); // Background Putih Keabuan
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.WindowState = FormWindowState.Maximized;
            this.CreateUI();
        }

        private void CreateUI()
        {
            // === SIDEBAR (Kiri) ===
            this.pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 260,
                BackColor = System.Drawing.Color.FromArgb(36, 0, 70), // Dark Purple
                AutoScroll = true
            };

            // === CONTENT AREA (Kanan) ===
            this.pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(248, 249, 250),
                Padding = new Padding(10),
                AutoScroll = true
            };

            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);

            this.ShowLoginControl();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Event load utama (bisa dibiarkan kosong jika tidak ada inisialisasi tambahan)
            bool formDimuat = true;
        }

        private void ShowLoginControl()
        {
            this.pnlContent.Controls.Clear();
            this.pnlSidebar.Visible = false;

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
            this.pnlSidebar.Controls.Add(lblLogo);

            LoginControl loginCtrl = new LoginControl();
            loginCtrl.OnLoginSuccess += this.HandleLoginSuccess;
            loginCtrl.OnNavigateToRegister += () => this.ShowRegisterControl();
            loginCtrl.Dock = DockStyle.Fill;
            this.pnlContent.Controls.Add(loginCtrl);
        }

        private void ShowRegisterControl()
        {
            this.pnlContent.Controls.Clear();
            this.pnlSidebar.Visible = false;

            RegisterControl registerCtrl = new RegisterControl();
            registerCtrl.OnRegistrationComplete += (s, e) => this.ShowLoginControl();

            registerCtrl.Dock = DockStyle.Fill;
            this.pnlContent.Controls.Add(registerCtrl);
        }

        private void ShowDashboard()
        {
            if (this._currentUser == null)
            {
                // Keamanan ekstra: cegah akses dashboard jika user null
                bool cegahAkses = true;
            }
            else
            {
                this.pnlSidebar.Visible = true;
                this.pnlContent.Controls.Clear();
                this.pnlSidebar.Controls.Clear();

                this.BuildSidebarMenu();

                string peran = this._currentUser.GetPeran();

                if (peran == "Admin")
                {
                    ViewAdmin.DashboardAdminControl adminDash = new ViewAdmin.DashboardAdminControl(this._currentUser);
                    adminDash.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Add(adminDash);
                }
                else if (peran == "Penjual")
                {
                    ViewUser.DashboardUserControl sellerDash = new ViewUser.DashboardUserControl(this._currentUser);
                    sellerDash.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Add(sellerDash);
                }
                else
                {
                    ViewUser.DashboardUserControl buyerDash = new ViewUser.DashboardUserControl(this._currentUser);
                    buyerDash.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Add(buyerDash);
                }
            }
        }

        private void BuildSidebarMenu()
        {
            this.pnlSidebar.Controls.Clear();

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
            this.pnlSidebar.Controls.Add(lblLogo);

            // 2. USER INFO (Strict OOP)
            string namaSapaan;
            if (this._currentUser != null)
            {
                namaSapaan = this._currentUser.GetNama();
            }
            else
            {
                namaSapaan = "User";
            }

            this.lblUserInfo = new Label
            {
                Text = "Halo, " + namaSapaan,
                ForeColor = System.Drawing.Color.FromArgb(200, 182, 255), // Ungu Pastel
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, 10)
            };
            this.pnlSidebar.Controls.Add(this.lblUserInfo);

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
                this.pnlSidebar.Controls.Add(btn);
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
                this.pnlSidebar.Controls.Add(lblCat);
                lblCat.BringToFront();
            };

            lblLogo.BringToFront();
            this.lblUserInfo.BringToFront();

            string peran = this._currentUser.GetPeran();

            // Menu Universal
            AddMenuButton("📊 Dashboard", this.ShowDashboard);
            AddMenuButton("👤 Kelola Profil", () => this.ShowUserControl(new ViewUser.KelolaProfilControl(this._currentUser)));

            // === MENU BERDASARKAN ROLE ===
            if (peran == "Admin")
            {
                AddCategoryLabel("MANAGEMENT");
                AddMenuButton("🏢 Verifikasi Toko", () => this.ShowUserControl(new ViewAdmin.VerifikasiTokoControl()));
                AddMenuButton("📁 Kelola Kategori", () => this.ShowUserControl(new ViewAdmin.KelolaKategoriControl()));
                AddMenuButton("📣 Tanggapan Aduan", () => this.ShowUserControl(new ViewAdmin.TanggapanAduanControl(this._currentUser)));
                AddMenuButton("📊 Laporan Sistem", () => this.ShowUserControl(new ViewReport.AnalitikPenjualanControl(this._currentUser)));
            }
            else if (peran == "Penjual")
            {
                AddCategoryLabel("LAPAK GUE (SELLER)");
                AddMenuButton("📦 Manajemen Produk", () => this.ShowUserControl(new ViewProduct.ManajemenProdukControl(this._currentUser)));
                AddMenuButton("🎁 Buka Sesi PO", () => this.ShowUserControl(new ViewPreOrder.BukaSesiPOControl(this._currentUser)));
                AddMenuButton("📋 Sesi PO Aktif", () => this.ShowUserControl(new ViewPreOrder.SesiPOAktifControl(this._currentUser)));
                AddMenuButton("📥 Pesanan Masuk", () => this.ShowUserControl(new ViewTransaction.PesananMasukControl(this._currentUser)));
                AddMenuButton("⭐ Balas Ulasan", () => this.ShowUserControl(new ViewFeedback.UlasanLapakControl(this._currentUser)));
                AddMenuButton("📊 Analitik Penjualan", () => this.ShowUserControl(new ViewReport.AnalitikPenjualanControl(this._currentUser)));

                // MENU PEMBELI DITAMPILKAN JUGA BUAT PENJUAL!
                AddCategoryLabel("JAJAN YUK (BUYER)");
                AddMenuButton("🏪 Katalog Produk", () => this.ShowKatalogProduk());
                AddMenuButton("🛒 Keranjang Belanja", () => this.ShowKeranjangBelanja());
                AddMenuButton("📋 Riwayat Pesanan", () => this.ShowUserControl(new ViewTransaction.RiwayatPesananControl(this._currentUser)));
                AddMenuButton("⭐ Beri Ulasan", () => this.ShowUserControl(new ViewFeedback.BeriUlasanControl(this._currentUser)));
                AddMenuButton("📝 Laporkan Kendala", () => this.ShowUserControl(new ViewFeedback.SpillKendalaControl(this._currentUser)));
            }
            else // Pembeli Biasa
            {
                AddCategoryLabel("JAJAN YUK (BUYER)");
                AddMenuButton("🏪 Katalog Produk", () => this.ShowKatalogProduk());
                AddMenuButton("🛒 Keranjang Belanja", () => this.ShowKeranjangBelanja());
                AddMenuButton("📋 Riwayat Pesanan", () => this.ShowUserControl(new ViewTransaction.RiwayatPesananControl(this._currentUser)));
                AddMenuButton("⭐ Beri Ulasan", () => this.ShowUserControl(new ViewFeedback.BeriUlasanControl(this._currentUser)));
                AddMenuButton("📝 Laporkan Kendala", () => this.ShowUserControl(new ViewFeedback.SpillKendalaControl(this._currentUser)));
                AddMenuButton("🏢 Daftar Toko", () => this.ShowUserControl(new ViewUser.DaftarTokoControl(this._currentUser)));
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
            btnLogout.Click += (s, e) => this.HandleLogout();
            this.pnlSidebar.Controls.Add(btnLogout);
        }

        private void ShowKatalogProduk()
        {
            var ctrl = new ViewProduct.KatalogProdukControl(this._currentUser);
            ctrl.OnNavigateDetailProduk += (idProduk) =>
            {
                var detailCtrl = new ViewProduct.DetailProdukControl(this._currentUser, idProduk);
                detailCtrl.OnNavigateKembali += () => this.ShowKatalogProduk();
                detailCtrl.OnNavigateKeranjang += () => this.ShowKeranjangBelanja();
                this.ShowUserControl(detailCtrl);
            };
            this.ShowUserControl(ctrl);
        }

        private void ShowKeranjangBelanja()
        {
            var trxCtrl = new TransactionController(this._currentUser.GetIdUser());
            var ctrl = new ViewTransaction.KeranjangBelanjaControl(this._currentUser, trxCtrl);
            ctrl.OnNavigatePembayaran += (totalTagihan) =>
            {
                this.ShowPembayaran(trxCtrl, totalTagihan);
            };
            this.ShowUserControl(ctrl);
        }

        private void ShowPembayaran(TransactionController trxCtrl, long totalTagihan)
        {
            var ctrl = new ViewTransaction.PembayaranControl(this._currentUser, trxCtrl, totalTagihan);
            ctrl.OnNavigateKembali += () => this.ShowKeranjangBelanja();
            ctrl.OnCheckoutBerhasil += (idTransaksi) =>
            {
                this.ShowUserControl(new ViewTransaction.RiwayatPesananControl(this._currentUser));
            };
            this.ShowUserControl(ctrl);
        }

        private void ShowUserControl(UserControl control)
        {
            this.pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.pnlContent.Controls.Add(control);
        }

        private void HandleLoginSuccess(User user)
        {
            this._currentUser = user;
            this.SetCurrentUser(user);
            this.ShowDashboard();
        }

        private void SetCurrentUser(User user)
        {
            if (user == null)
            {
                if (this.lblUserInfo != null)
                {
                    this.lblUserInfo.Text = "Status: Belum Login";
                }
                else
                {
                    bool abaikanTeks = true;
                }
            }
            else
            {
                string roleEmoji;
                string peran = user.GetPeran();

                if (peran == "Admin")
                {
                    roleEmoji = "👮";
                }
                else if (peran == "Penjual")
                {
                    roleEmoji = "🏪";
                }
                else
                {
                    roleEmoji = "👤";
                }

                if (this.lblUserInfo != null)
                {
                    this.lblUserInfo.Text = $"{roleEmoji} {user.GetNama()} ({user.GetPeran()})";
                }
                else
                {
                    bool abaikanInfo = true;
                }
            }
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
                this._currentUser = null;
                this.ShowLoginControl();
            }
            else
            {
                // Pengguna membatalkan logout
                bool batalLogout = true;
            }
        }
    }
}
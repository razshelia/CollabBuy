using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

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
        private TransactionController _trxCtrl;

        public MainForm()
        {
            this.InitializeComponent();
            this._userController = new UserController();

            this.Text = "CollabBuy v1.0 - Sistem Agregator Dana Usaha (Danus) Mahasiswa";
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.WindowState = FormWindowState.Maximized;
            this.CreateUI();
        }

        // ---------------------------------------------------------------
        // SETUP UI
        // ---------------------------------------------------------------

        private void CreateUI()
        {
            this.pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 260,
                BackColor = System.Drawing.Color.FromArgb(45, 0, 87),
                AutoScroll = false
            };

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
            //form dimuat
        }

        // ---------------------------------------------------------------
        // NAVIGASI HALAMAN
        // ---------------------------------------------------------------

        private void ShowLoginControl()
        {
            this.pnlContent.Controls.Clear();
            this.pnlSidebar.Controls.Clear();
            this.pnlSidebar.Visible = false;

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
            if (this._currentUser == null) return;

            this.pnlSidebar.Visible = true;
            this.pnlContent.Controls.Clear();
            this.BuildSidebarMenu();

            string peran = this._currentUser.Peran;

            if (peran == "Admin")
            {
                var adminDash = new ViewAdmin.DashboardAdminControl(this._currentUser);
                adminDash.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Add(adminDash);
            }
            else
            {
                var userDash = new ViewUser.DashboardUserControl(this._currentUser);
                userDash.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Add(userDash);
            }
        }

        private void ShowKatalogProduk()
        {
            var ctrl = new ViewProduct.KatalogProdukControl(this._currentUser);
            ctrl.OnNavigateDetailProduk += (idProduk) =>
            {
                var detail = new ViewProduct.DetailProdukControl(this._currentUser, idProduk);
                detail.OnNavigateKembali += () => this.ShowKatalogProduk();
                detail.OnNavigateKeranjang += () => this.ShowKeranjangBelanja();
                this.ShowUserControl(detail);
            };
            this.ShowUserControl(ctrl);
        }
        private void ShowSesiPOAktif()
        {
            var ctrl = new ViewPreOrder.SesiPOAktifControl(this._currentUser);
            ctrl.OnNavigateKeProdukPO += (idPO) =>
            {
                this.ShowKatalogProdukPO(idPO);
            };
            ctrl.OnNavigateKeKeranjang += () => this.ShowKeranjangBelanja();
            this.ShowUserControl(ctrl);
        }

        private void ShowKatalogProdukPO(int idPO)
        {
            var prodCtrl = new ViewProduct.KatalogProdukControl(this._currentUser, idPO);

            prodCtrl.OnNavigateKembali += () => this.ShowSesiPOAktif();

            prodCtrl.OnNavigateDetailProduk += (idProduk) =>
            {
                var detail = new ViewProduct.DetailProdukControl(this._currentUser, idProduk);
                detail.OnNavigateKembali += () => this.ShowKatalogProdukPO(idPO);
                detail.OnNavigateKeranjang += () => this.ShowKeranjangBelanja();
                this.ShowUserControl(detail);
            };
            this.ShowUserControl(prodCtrl);
        }

        private void ShowKeranjangBelanja()
        {
            var ctrl = new ViewTransaction.KeranjangBelanjaControl(this._currentUser, this._trxCtrl);
            ctrl.OnNavigatePembayaran += (totalTagihan) => this.ShowPembayaran(this._trxCtrl, totalTagihan);
            this.ShowUserControl(ctrl);
        }

        private void ShowPembayaran(TransactionController trxCtrl, long totalTagihan)
        {
            var ctrl = new ViewTransaction.PembayaranControl(this._currentUser, trxCtrl, totalTagihan);
            ctrl.OnNavigateKembali += () => this.ShowKeranjangBelanja();
            ctrl.OnCheckoutBerhasil += (_) => this.ShowUserControl(
                new ViewTransaction.RiwayatPesananControl(this._currentUser));
            this.ShowUserControl(ctrl);
        }

        private void ShowPesananMasuk()
        {
            var ctrl = new ViewTransaction.PesananMasukControl(this._currentUser);
            ctrl.OnNavigateDetail += (idTrx, dtDetail) =>
            {
                var detailCtrl = new ViewTransaction.DetailPesananControl(idTrx, dtDetail);
                detailCtrl.OnNavigateKembali += () => this.ShowPesananMasuk();
                this.ShowUserControl(detailCtrl);
            };
            this.ShowUserControl(ctrl);
        }

        private void ShowUserControl(UserControl control)
        {
            this.pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.pnlContent.Controls.Add(control);
        }

        // ---------------------------------------------------------------
        // SIDEBAR
        // ---------------------------------------------------------------

        private void BuildSidebarMenu()
        {
            this.pnlSidebar.Controls.Clear();

            // Logout (Dock=Bottom) — ditambah PERTAMA agar tidak tertimpa Fill
            Button btnLogout = new Button
            {
                Text = "🚪 Logout",
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = System.Drawing.Color.FromArgb(200, 50, 50),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => this.HandleLogout();
            this.pnlSidebar.Controls.Add(btnLogout);

            // Area menu scrollable — manual height agar tidak numpuk logout
            Panel pnlMenu = new Panel
            {
                BackColor = System.Drawing.Color.FromArgb(45, 0, 87),
                AutoScroll = true,
                Location = new System.Drawing.Point(0, 130), // di bawah pnlHeader (130px)
                Width = 260,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left
            };

            // Hitung tinggi: total sidebar - header (130) - logout (50)
            pnlMenu.Height = this.pnlSidebar.ClientSize.Height - 130 - 50;

            // Update tinggi dinamis saat sidebar di-resize
            this.pnlSidebar.Resize += (s, e) =>
            {
                pnlMenu.Height = this.pnlSidebar.ClientSize.Height - 130 - 50;
            };

            this.pnlSidebar.Controls.Add(pnlMenu);

            // Header logo + info user (Dock=Top) — ditambah TERAKHIR agar tampil paling atas
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 130,
                BackColor = System.Drawing.Color.FromArgb(35, 0, 68)
            };
            this.pnlSidebar.Controls.Add(pnlHeader);

            string logoPath = System.IO.Path.Combine(
                System.Windows.Forms.Application.StartupPath, "logo_sidebar.jpeg");

            if (System.IO.File.Exists(logoPath))
            {
                PictureBox picLogo = new PictureBox
                {
                    Image = System.Drawing.Image.FromFile(logoPath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new System.Drawing.Size(70, 70),
                    Location = new System.Drawing.Point(15, 12),
                    BackColor = System.Drawing.Color.Transparent
                };
                pnlHeader.Controls.Add(picLogo);

                Label lblAppName = new Label
                {
                    Text = "CollabBuy",
                    ForeColor = System.Drawing.Color.FromArgb(253, 255, 182),
                    Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold),
                    Location = new System.Drawing.Point(92, 32),
                    AutoSize = true,
                    BackColor = System.Drawing.Color.Transparent
                };
                pnlHeader.Controls.Add(lblAppName);
            }
            else
            {
                Label lblLogoText = new Label
                {
                    Text = "CollabBuy",
                    ForeColor = System.Drawing.Color.FromArgb(253, 255, 182),
                    Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 70,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };
                pnlHeader.Controls.Add(lblLogoText);
            }

            string namaSapaan = this._currentUser?.Nama ?? "User";
            string peranUser = this._currentUser?.Peran ?? "User";
            string peranLabel;
            if (peranUser == "Admin") peranLabel = "👮 Admin";
            else if (peranUser == "Penjual") peranLabel = "🏪 Penjual Terverifikasi";
            else peranLabel = "👤 Pembeli";

            this.lblUserInfo = new Label
            {
                Text = namaSapaan + Environment.NewLine + peranLabel,
                ForeColor = System.Drawing.Color.FromArgb(200, 182, 255),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(0, 86),
                Size = new System.Drawing.Size(260, 40),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.Transparent
            };
            pnlHeader.Controls.Add(this.lblUserInfo);

            // Helper lokal
            var items = new System.Collections.Generic.List<System.Windows.Forms.Control>();

            Action<string, Action> AddBtn = (text, onClick) =>
            {
                Button btn = new Button
                {
                    Text = "   " + text,
                    Dock = DockStyle.Top,
                    Height = 45,
                    BackColor = System.Drawing.Color.FromArgb(45, 0, 87),
                    ForeColor = System.Drawing.Color.FromArgb(200, 182, 255),
                    FlatStyle = FlatStyle.Flat,
                    Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) => onClick();
                btn.MouseEnter += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(75, 10, 130);
                btn.MouseLeave += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(45, 0, 87);
                items.Add(btn);
            };

            Action<string> AddCat = (text) =>
            {
                Label lbl = new Label
                {
                    Text = text,
                    ForeColor = System.Drawing.Color.FromArgb(253, 255, 182),
                    Font = new System.Drawing.Font("Segoe UI Black", 8F, System.Drawing.FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 32,
                    TextAlign = System.Drawing.ContentAlignment.BottomLeft,
                    Padding = new Padding(12, 0, 0, 4),
                    BackColor = System.Drawing.Color.FromArgb(35, 0, 68)
                };
                items.Add(lbl);
            };

            Action AddSep = () =>
            {
                items.Add(new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 1,
                    BackColor = System.Drawing.Color.FromArgb(80, 20, 130)
                });
            };

            // === MENU UNIVERSAL ===
            AddSep();
            AddBtn("📊 Dashboard", this.ShowDashboard);
            AddBtn("👤 Kelola Profil", () => this.ShowUserControl(
                new ViewUser.KelolaProfilControl(this._currentUser)));
            AddSep();

            // === MENU BERDASARKAN PERAN ===
            if (peranUser == "Admin")
            {
                AddCat("MANAGEMENT");
                AddBtn("👥 Kelola User", () => this.ShowUserControl(new ViewAdmin.KelolaUserControl(this._currentUser)));
                AddBtn("🏢 Verifikasi Toko", () => this.ShowUserControl(new ViewAdmin.VerifikasiTokoControl()));
                AddBtn("📁 Kelola Kategori", () => this.ShowUserControl(new ViewAdmin.KelolaKategoriControl()));
                AddBtn("📣 Tanggapan Aduan", () => this.ShowUserControl(
                    new ViewAdmin.TanggapanAduanControl(this._currentUser)));
                AddBtn("📋 Log Aktivitas", () => this.ShowUserControl(
                    new ViewAdmin.LogAktivitasControl()));
                AddBtn("📊 Laporan Sistem", () => this.ShowUserControl(
                    new ViewReport.AnalitikPenjualanControl(this._currentUser)));
            }
            else if (peranUser == "Penjual")
            {
                AddCat("LAPAK GUE (SELLER)");
                AddBtn("📦 Manajemen Produk", () => this.ShowUserControl(
                    new ViewProduct.ManajemenProdukControl(this._currentUser)));
                AddBtn("🎁 Buka Sesi PO", () => this.ShowUserControl(
                    new ViewPreOrder.BukaSesiPOControl(this._currentUser)));
                AddBtn("⚙️ Kelola Sesi PO", () => this.ShowUserControl(
                    new ViewPreOrder.KelolaSesiPOControl(this._currentUser)));
                AddBtn("📥 Pesanan Masuk", () => this.ShowPesananMasuk());
                AddBtn("⭐ Balas Ulasan", () => this.ShowUserControl(
                    new ViewFeedback.UlasanLapakControl(this._currentUser)));
                AddBtn("📊 Analitik Penjualan", () => this.ShowUserControl(
                    new ViewReport.AnalitikPenjualanControl(this._currentUser)));
                AddSep();
                AddCat("JAJAN YUK (BUYER)");
                AddBtn("🏪 Katalog Produk", () => this.ShowKatalogProduk());
                AddBtn("📋 Sesi PO Aktif", () => this.ShowSesiPOAktif());
                AddBtn("🛒 Keranjang Belanja", () => this.ShowKeranjangBelanja());
                AddBtn("📋 Riwayat Pesanan", () => this.ShowUserControl(
                    new ViewTransaction.RiwayatPesananControl(this._currentUser)));
                AddBtn("⭐ Beri Ulasan", () => this.ShowUserControl(
                    new ViewFeedback.BeriUlasanControl(this._currentUser)));
                AddBtn("📝 Laporkan Kendala", () => this.ShowUserControl(
                    new ViewFeedback.SpillKendalaControl(this._currentUser)));
            }
            else // Pembeli biasa
            {
                AddCat("JAJAN YUK (BUYER)");
                AddBtn("🏪 Katalog Produk", () => this.ShowKatalogProduk());
                // SESUDAH:
                AddBtn("📋 Sesi PO Aktif", () => this.ShowSesiPOAktif());
                AddBtn("🛒 Keranjang Belanja", () => this.ShowKeranjangBelanja());
                AddBtn("📋 Riwayat Pesanan", () => this.ShowUserControl(
                    new ViewTransaction.RiwayatPesananControl(this._currentUser)));
                AddBtn("⭐ Beri Ulasan", () => this.ShowUserControl(
                    new ViewFeedback.BeriUlasanControl(this._currentUser)));
                AddBtn("📝 Laporkan Kendala", () => this.ShowUserControl(
                    new ViewFeedback.SpillKendalaControl(this._currentUser)));
                AddBtn("🏢 Daftar Jadi Penjual", () => this.ShowUserControl(
                    new ViewUser.DaftarTokoControl(this._currentUser)));
            }

            // Tambah ke pnlMenu secara TERBALIK agar urutan visual benar (Dock=Top)
            for (int i = items.Count - 1; i >= 0; i--)
                pnlMenu.Controls.Add(items[i]);
        }

        // ---------------------------------------------------------------
        // HANDLER
        // ---------------------------------------------------------------

        private void HandleLoginSuccess(User user)
        {
            this._currentUser = user;
            this._trxCtrl = new TransactionController(user.IdUser);
            this.ShowDashboard();
        }

        private void SetCurrentUser(User user)
        {
            if (this.lblUserInfo == null) return;

            if (user == null)
            {
                this.lblUserInfo.Text = "Status: Belum Login";
            }
            else
            {
                string emoji = user.Peran == "Admin" ? "👮" :
                user.Peran == "Penjual" ? "🏪" : "👤";

                string badgeInfo = "";

                if (user.Peran == "Penjual")
                {
                    Models.Penjual penjual = user as Models.Penjual;
                    if (penjual != null)
                    {
                        var produkCtrl = new Controllers.ProductController();
                        produkCtrl.SyncKatalogKePenjual(penjual);
                        int totalProduk = penjual.DapatkanTotalProdukAktif();
                        badgeInfo = $"\n📦 {totalProduk} produk aktif";
                    }
                }
                else if (user.Peran == "User")
                {
                    Models.Pembeli pembeli = user as Models.Pembeli;
                    if (pembeli != null)
                    {
                        var trxCtrl = new Controllers.TransactionController(user.IdUser);
                        trxCtrl.SyncRiwayatKePembeli(pembeli);
                        badgeInfo = "\n" + pembeli.DapatkanLevelPembeli();
                    }
                }

                this.lblUserInfo.Text = emoji + " " + user.Nama + " (" + user.Peran + ")" + badgeInfo;
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
                try
                {
                    if (this._currentUser != null)
                    {
                        int idPembeli = this._currentUser.IdUser;
                        Services.CartManager.BersihkanSesiPembeli(idPembeli);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Terjadi peringatan sistem saat membersihkan memori keranjang: {ex.Message}",
                        "Peringatan Logika Bisnis",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                finally
                {
                    this._currentUser = null;
                    this._trxCtrl = null;
                    this.ShowLoginControl();
                }
            }
        }
    }
}
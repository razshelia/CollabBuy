using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.UI.Controls;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI
{
    public partial class MainForm : Form
    {
        private User _userAktif;
        private Button _activeNavButton = null;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            TampilkanLogin();
        }

        public User AmbilUserAktif() => _userAktif;

        public void GantiHalaman(UserControl halamanBaru)
        {
            if (halamanBaru == null) return;
            pnlMainContainer.Controls.Clear();
            halamanBaru.Dock = DockStyle.Fill;
            pnlMainContainer.Controls.Add(halamanBaru);
        }

        public void RefreshSidebar()
        {
            if (_userAktif != null)
                AturSidebarBerdasarkanPeran();
        }

        private void TampilkanLogin()
        {
            pnlSidebar.Visible = false;
            var login = new LoginControl();
            login.PindahKeRegister += (s, ev) => TampilkanRegister();
            login.LoginBerhasil += (user) =>
            {
                _userAktif = user;
                TampilkanDashboard();
            };
            GantiHalaman(login);
        }

        private void TampilkanRegister()
        {
            pnlSidebar.Visible = false;
            var reg = new RegisterControl();
            reg.PindahKeLogin += (s, ev) => TampilkanLogin();
            GantiHalaman(reg);
        }

        private void TampilkanDashboard()
        {
            pnlSidebar.Visible = true;
            AturSidebarBerdasarkanPeran();

            if (_userAktif is Admin)
            {
                HighlightNav(btnAdminDashboard);
                GantiHalaman(new AdminDashboardControl());
            }
            else
            {
                HighlightNav(btnUserKatalog);
                GantiHalaman(new UserDashboardControl(_userAktif));
            }
        }

        private void AturSidebarBerdasarkanPeran()
        {
            bool isAdmin = _userAktif is Admin;
            bool isUser = !isAdmin;
            bool isSeller = false;

            if (_userAktif is RegularUser regUser)
            {
                var verifService = new VerificationService();
                var verif = verifService.AmbilVerifikasiByUser(regUser.IdUser);
                isSeller = verif != null && verif.IsVerifikasi;
            }

            lblUserInfo.Text = isAdmin
                ? $"✨ Admin: {_userAktif.Nama}"
                : $"🛍️ {_userAktif.Nama}";

            // Sembunyikan semua panel dulu
            pnlAdmin.Visible = false;
            pnlBuyer.Visible = false;
            pnlSeller.Visible = false;

            int currentY = 100;

            if (isAdmin)
            {
                pnlAdmin.Top = currentY;
                pnlAdmin.Visible = true;
                currentY += pnlAdmin.Height + 10;
            }

            if (isUser)
            {
                // Tampilkan/sembunyikan tombol BukaLapak sebelum set tinggi panel
                btnUserBukaLapak.Visible = !isSeller;

                // Hitung tinggi panel buyer dinamis:
                // Label judul (25) + 4 tombol tetap × 40 + BukaLapak jika tampil × 40
                int jumlahTombolBuyer = 4 + (isSeller ? 0 : 1); // Katalog, Checkout, Riwayat, Aduan, (+BukaLapak)
                pnlBuyer.Height = 25 + (jumlahTombolBuyer * 40) + 10;
                pnlBuyer.Top = currentY;
                pnlBuyer.Visible = true;
                currentY += pnlBuyer.Height + 10;
            }

            if (isSeller)
            {
                pnlSeller.Top = currentY;
                pnlSeller.Visible = true;
                currentY += pnlSeller.Height + 10;
            }

            // Profil dan Logout selalu di bawah panel terakhir
            btnProfil.Top = currentY;
            btnLogout.Top = currentY + 40;
        }

        private void HighlightNav(Button btn)
        {
            Button[] allNav =
            {
                btnAdminDashboard, btnAdminVerifikasi, btnAdminKategori, btnAdminKeluhan,
                btnUserKatalog, btnUserCheckout, btnUserRiwayat, btnUserAduan, btnUserBukaLapak,
                btnSellerKatalog, btnSellerPesanan, btnSellerAnalitik, btnSellerUlasan,
                btnProfil
            };

            foreach (var b in allNav)
            {
                b.BackColor = Color.Transparent;
                b.ForeColor = Color.FromArgb(210, 210, 230);
                b.FlatAppearance.BorderSize = 0;
            }

            if (btn != null)
            {
                btn.BackColor = Color.FromArgb(167, 139, 250);
                btn.ForeColor = Color.White;
                _activeNavButton = btn;
            }
        }

        // ── ADMIN CLICK ──
        private void btnAdminDashboard_Click(object sender, EventArgs e)
        {
            HighlightNav(btnAdminDashboard);
            GantiHalaman(new AdminDashboardControl());
        }

        private void btnAdminVerifikasi_Click(object sender, EventArgs e)
        {
            HighlightNav(btnAdminVerifikasi);
            GantiHalaman(new AdminUserManagementControl());
        }

        private void btnAdminKategori_Click(object sender, EventArgs e)
        {
            HighlightNav(btnAdminKategori);
            GantiHalaman(new AdminCategoryControl());
        }

        private void btnAdminKeluhan_Click(object sender, EventArgs e)
        {
            HighlightNav(btnAdminKeluhan);
            GantiHalaman(new ComplaintListControl());
        }

        // ── USER CLICK ──
        private void btnUserKatalog_Click(object sender, EventArgs e)
        {
            HighlightNav(btnUserKatalog);
            GantiHalaman(new UserDashboardControl(_userAktif));
        }

        private void btnUserCheckout_Click(object sender, EventArgs e)
        {
            HighlightNav(btnUserCheckout);
            UXHelper.TampilkanError("Silakan pilih produk dari Katalog dulu ya, bestie! 🛒");
            GantiHalaman(new UserDashboardControl(_userAktif));
        }

        // Riwayat sekarang = Riwayat Checkout
        private void btnUserRiwayat_Click(object sender, EventArgs e)
        {
            HighlightNav(btnUserRiwayat);
            if (_userAktif != null)
                GantiHalaman(new CheckoutHistoryControl(_userAktif.IdUser));
        }

        private void btnUserAduan_Click(object sender, EventArgs e)
        {
            HighlightNav(btnUserAduan);
            GantiHalaman(new ComplaintControl());
        }

        private void btnUserBukaLapak_Click(object sender, EventArgs e)
        {
            HighlightNav(btnUserBukaLapak);
            if (_userAktif is RegularUser reg)
            {
                var verifService = new VerificationService();
                var verif = verifService.AmbilVerifikasiByUser(reg.IdUser);
                if (verif == null || !verif.IsVerifikasi)
                    GantiHalaman(new SellerVerificationControl(reg, RefreshSidebar));
                else
                    GantiHalaman(new SellerPOListControl(reg.IdUser));
            }
        }

        // ── SELLER CLICK ──
        private void btnSellerKatalog_Click(object sender, EventArgs e)
        {
            HighlightNav(btnSellerKatalog);
            GantiHalaman(new SellerPOListControl(_userAktif.IdUser));
        }

        private void btnSellerPesanan_Click(object sender, EventArgs e)
        {
            HighlightNav(btnSellerPesanan);
            GantiHalaman(new SellerOrderControl(_userAktif.IdUser));
        }

        private void btnSellerAnalitik_Click(object sender, EventArgs e)
        {
            HighlightNav(btnSellerAnalitik);
            GantiHalaman(new SellerReportControl(_userAktif.IdUser));
        }

        private void btnSellerUlasan_Click(object sender, EventArgs e)
        {
            HighlightNav(btnSellerUlasan);
            GantiHalaman(new ReviewControl(_userAktif.IdUser));
        }

        // ── STATIC ──
        private void btnProfil_Click(object sender, EventArgs e)
        {
            HighlightNav(btnProfil);
            GantiHalaman(new EditProfileControl(_userAktif));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (UXHelper.TampilkanKonfirmasi("Yakin mau logout, bestie? 😢"))
            {
                _userAktif = null;
                TampilkanLogin();
            }
        }
    }
}
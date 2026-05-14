using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.UI.Controls;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI
{
    public partial class MainForm : Form
    {
        private Akun userAktif;
        private Button activeNavButton = null;

        public MainForm()
        {
            this.InitializeComponent();
            this.TampilkanHalamanLogin();
        }

        // ── Ganti halaman di content area ─────────────────────────
        public void GantiHalaman(UserControl halamanBaru)
        {
            if (halamanBaru == null) return;
            this.pnlMainContainer.Controls.Clear();
            halamanBaru.Dock = DockStyle.Fill;
            this.pnlMainContainer.Controls.Add(halamanBaru);
        }
        public Akun AmbilUserAktif()
        {
            return this.userAktif;
        }

        // ── Highlight tombol navigasi aktif ───────────────────────
        private void SetActiveNav(Button btn)
        {
            Button[] allNav = {
                btnAdminDashboard, btnAdminVerifikasi, btnAdminKategori, btnAdminKeluhan,
                btnUserKatalog, btnUserCheckout, btnUserRiwayat, btnUserAduan, btnUserBukaLapak,
                btnSellerKatalog, btnSellerPesanan, btnSellerAnalitik, btnSellerUlasan,
                btnProfil
            };

            foreach (var b in allNav)
            {
                b.BackColor = System.Drawing.Color.Transparent;
                b.ForeColor = System.Drawing.Color.FromArgb(210, 210, 230);
                b.FlatAppearance.BorderSize = 0;
            }

            if (btn != null)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
                btn.ForeColor = System.Drawing.Color.White;
                activeNavButton = btn;
            }
        }

        // ── NAVIGASI HALAMAN ─────────────────────────────────────
        public void TampilkanHalamanLogin()
        {
            this.pnlSidebar.Visible = false;
            var login = new LoginControl();
            login.PindahKeRegister += (s, e) => this.TampilkanHalamanRegister();
            login.LoginBerhasil += (akun) =>
            {
                this.userAktif = akun;
                this.TampilkanHalamanDashboard();
            };
            this.GantiHalaman(login);
        }

        public void TampilkanHalamanRegister()
        {
            this.pnlSidebar.Visible = false;
            var reg = new RegisterControl();
            reg.PindahKeLogin += (s, e) => this.TampilkanHalamanLogin();
            this.GantiHalaman(reg);
        }

        public void TampilkanHalamanDashboard()
        {
            this.pnlSidebar.Visible = true;
            this.KonfigurasiSidebarBerdasarkanPeran();

            if (this.userAktif is Admin)
            {
                this.SetActiveNav(btnAdminDashboard);
                this.GantiHalaman(new AdminDashboardControl());
            }
            else
            {
                this.SetActiveNav(btnUserKatalog);
                this.GantiHalaman(new UserDashboardControl(this.userAktif));
            }
        }

        // ── Atur visibility menu berdasarkan Peran ───────────────
        private void KonfigurasiSidebarBerdasarkanPeran()
        {
            bool isAdmin = (this.userAktif is Admin);
            bool isUser = !isAdmin;
            bool isSeller = false;

            if (this.userAktif is User u)
                isSeller = u.IsVerifikasi;

            // Label info user
            this.lblUserInfo.Text = isAdmin
                ? $" Admin: {this.userAktif.Username}"
                : $" {this.userAktif.Username}";

            // ── Admin menus ─────────────────────────────────────
            lblSectionAdmin.Visible = isAdmin;
            btnAdminDashboard.Visible = isAdmin;
            btnAdminVerifikasi.Visible = isAdmin;
            btnAdminKategori.Visible = isAdmin;
            btnAdminKeluhan.Visible = isAdmin;

            // ── Buyer menus ─────────────────────────────────────
            lblSectionBuyer.Visible = isUser;
            btnUserKatalog.Visible = isUser;
            btnUserCheckout.Visible = isUser;
            btnUserRiwayat.Visible = isUser;
            btnUserAduan.Visible = isUser;
            btnUserBukaLapak.Visible = isUser && !isSeller; // hilang setelah jadi seller

            // ── Seller menus (hanya jika is_verifikasi = true) ─
            lblSectionSeller.Visible = isSeller;
            btnSellerKatalog.Visible = isSeller;
            btnSellerPesanan.Visible = isSeller;
            btnSellerAnalitik.Visible = isSeller;
            btnSellerUlasan.Visible = isSeller;
        }

        // ── PUBLIC helper untuk refresh sidebar setelah verifikasi ─
        public void RefreshSidebar()
        {
            if (this.userAktif != null)
                this.KonfigurasiSidebarBerdasarkanPeran();
        }

        // ── ADMIN CLICK HANDLERS ─────────────────────────────────
        private void btnAdminDashboard_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnAdminDashboard);
            GantiHalaman(new AdminDashboardControl());
        }

        private void btnAdminVerifikasi_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnAdminVerifikasi);
            // **PERBAIKAN**: Admin sekarang melihat daftar user yang mengajukan verifikasi,
            // BUKAN form pengajuan.
            GantiHalaman(new AdminUserManagementControl());
        }

        private void btnAdminKategori_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnAdminKategori);
            // TODO: Ganti dengan KategoriControl jika sudah dibuat.
            GantiHalaman(new AdminDashboardControl());
        }

        private void btnAdminKeluhan_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnAdminKeluhan);
            // **PERBAIKAN**: Admin sekarang melihat daftar keluhan,
            // BUKAN manajemen user.
            GantiHalaman(new ComplaintControl());
        }

        // ── USER/BUYER CLICK HANDLERS ────────────────────────────
        private void btnUserKatalog_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnUserKatalog);
            GantiHalaman(new CatalogControl());
        }

        private void btnUserCheckout_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnUserCheckout);
            // **PERBAIKAN**: Karena CheckoutControl butuh parameter produk,
            // arahkan user ke Katalog dulu untuk memilih produk.
            UXHelper.TampilkanError("Silakan pilih produk dari Katalog terlebih dahulu.");
        }

        private void btnUserRiwayat_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnUserRiwayat);
            // **PERBAIKAN**: Sekarang membuka RiwayatControl, bukan UserDashboardControl.
            if (this.userAktif != null)
                GantiHalaman(new RiwayatControl(this.userAktif.IdUser));
            else
                UXHelper.TampilkanError("Anda harus login terlebih dahulu.");
        }

        private void btnUserAduan_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnUserAduan);
            GantiHalaman(new ComplaintControl());
        }

        private void btnUserBukaLapak_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnUserBukaLapak);
            // Form pengajuan seller (hanya muncul untuk user yang belum terverifikasi)
            GantiHalaman(new SellerVerificationControl(this.userAktif, this.RefreshSidebar));
        }

        // ── SELLER CLICK HANDLERS ────────────────────────────────
        private void btnSellerKatalog_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnSellerKatalog);
            GantiHalaman(new SellerProductControl(this.userAktif.IdUser));
        }

        private void btnSellerPesanan_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnSellerPesanan);
            GantiHalaman(new SellerOrderControl(this.userAktif.IdUser));
        }

        private void btnSellerAnalitik_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnSellerAnalitik);
            GantiHalaman(new ReportDashboardControl());
        }

        private void btnSellerUlasan_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnSellerUlasan);
            GantiHalaman(new ReviewControl());
        }

        // ── STATIC MENU HANDLERS ─────────────────────────────────
        private void btnProfil_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnProfil);
            GantiHalaman(new EditProfileControl(this.userAktif));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (UXHelper.TampilkanKonfirmasi("Yakin mau logout sekarang, Bestie? "))
            {
                this.userAktif = null;
                this.TampilkanHalamanLogin();
            }
        }
    }
}
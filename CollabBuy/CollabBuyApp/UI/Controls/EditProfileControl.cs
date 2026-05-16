using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class EditProfileControl : UserControl
    {
        private readonly User _user;
        private readonly UserService _userService;

        public EditProfileControl(User user)
        {
            InitializeComponent();
            _user = user;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _userService = new UserService(new UserRepository());

            // CenterCard dipanggil otomatis saat layar diubah ukurannya
            this.Resize += (s, e) => CenterCard();

            LoadProfile();
        }

        private void CenterCard()
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
            }
        }

        private void LoadProfile()
        {
            txtNama.Text = _user.Nama;
            txtTelepon.Text = _user.NomorTelepon ?? "";
            txtEmail.Text = _user.Email;
            txtUsername.Text = _user.Username;

            // Username tidak bisa diedit
            txtUsername.Enabled = false;
        }

        private void chkLihatPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPasswordBaru.UseSystemPasswordChar = !chkLihatPassword.Checked;
            txtKonfirmasiPassword.UseSystemPasswordChar = !chkLihatPassword.Checked;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            string telepon = txtTelepon.Text.Trim();
            string email = txtEmail.Text.Trim();
            string passBaru = txtPasswordBaru.Text.Trim();
            string passKonfirmasi = txtKonfirmasiPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(email))
            {
                UXHelper.TampilkanError("Nama dan email wajib diisi, bestie!");
                return;
            }

            if (!string.IsNullOrEmpty(passBaru))
            {
                if (passBaru != passKonfirmasi)
                {
                    UXHelper.TampilkanError("Password dan konfirmasi tidak cocok!");
                    return;
                }
                if (passBaru.Length < 8)
                {
                    UXHelper.TampilkanError("Password minimal 8 karakter ya~");
                    return;
                }
            }

            // Update model user
            _user.Nama = nama;
            _user.NomorTelepon = telepon;
            _user.Email = email;

            // Memanggil service yang sudah diinjeksi
            bool sukses = _userService.UpdateProfil(_user, string.IsNullOrEmpty(passBaru) ? null : passBaru);
            if (sukses)
            {
                UXHelper.TampilkanSukses("Profil berhasil diperbarui! ✨");
                // Kembali ke halaman sebelumnya
                if (ParentForm is MainForm main)
                {
                    main.GantiHalaman(new UserDashboardControl(_user));
                }
            }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class LoginControl : UserControl
    {
        public event EventHandler PindahKeRegister;
        public event Action<User> LoginBerhasil;

        private readonly AuthService _authService;

        public LoginControl()
        {
            InitializeComponent();

            // TAHAP 4: INJEKSI MANUAL DI UI
            _authService = new AuthService(new UserRepository());

            // Center card saat ukuran berubah atau pertama kali dimuat
            this.Resize += (s, e) => CenterCard();
            this.Load += (s, e) => CenterCard();
        }

        private void CenterCard()
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
            }
        }

        private void chkLihatPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkLihatPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                UXHelper.TampilkanError("Isi dulu username & passwordnya ya, bestie! 😊");
                return;
            }

            // Memanggil service yang sudah disuntik Repository
            var akun = _authService.Login(user, pass);
            if (akun != null)
                LoginBerhasil?.Invoke(akun);
        }

        private void lblRegisterLink_Click(object sender, EventArgs e)
        {
            PindahKeRegister?.Invoke(this, EventArgs.Empty);
        }
    }
}
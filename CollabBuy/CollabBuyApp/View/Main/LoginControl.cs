using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Main
{
    public partial class LoginControl : UserControl
    {
        public event Action<User> OnLoginSuccess;
        public event Action OnNavigateToRegister;
        private readonly UserController _userController;

        public LoginControl()
        {
            InitializeComponent();
            _userController = new UserController();
            this.Resize += LoginControl_Resize;
        }

        private void LoginControl_Resize(object sender, EventArgs e)
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.Height - pnlCard.Height) / 2;
            }
        }

        // FUNGSI BARU: Nampilin dan nyembunyiin password
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan password diisi dulu ya bestie!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (user, pesan) = _userController.Login(username, password);

                if (user != null) OnLoginSuccess?.Invoke(user);
                else MessageBox.Show(pesan, "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Aduh error nih: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            OnNavigateToRegister?.Invoke();
        }
    }
}
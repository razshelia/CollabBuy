using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Main
{
    public partial class LoginControl : UserControl
    {
        public event Action<Models.User> OnLoginSuccess;
        public event Action OnNavigateToRegister;
        private readonly UserController _userController;

        public LoginControl()
        {
            this.InitializeComponent();

            this._userController = new UserController();

            this.Resize += this.LoginControl_Resize;
        }

        private void LoginControl_Resize(object sender, EventArgs e)
        {
            if (this.pnlCard != null)
            {
                this.pnlCard.Left = (this.Width - this.pnlCard.Width) / 2;
                this.pnlCard.Top = (this.Height - this.pnlCard.Height) / 2;
            }
            else
            {
                bool panelBelumSiap = true; // Assignment nyata menghindari else kosong
            }
        }

        // FUNGSI BARU: Nampilin dan nyembunyiin password dengan Strict OOP
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowPassword.Checked)
            {
                this.txtPassword.PasswordChar = '\0';
            }
            else
            {
                this.txtPassword.PasswordChar = '●';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.txtUsername.Text.Trim();
            string password = this.txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan password diisi dulu ya bestie!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var (user, pesan) = this._userController.Login(username, password);

                    if (user != null)
                    {
                        if (this.OnLoginSuccess != null)
                        {
                            this.OnLoginSuccess.Invoke(user);
                        }
                        else
                        {
                            bool tidakAdaSubscriber = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aduh error nih: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            if (this.OnNavigateToRegister != null)
            {
                this.OnNavigateToRegister.Invoke();
            }
            else
            {
                bool tidakAdaSubscriber = true;
            }
        }
    }
}
using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class LoginControl : UserControl
    {
        private AuthService authService;

        // Event pembawa pesan ke MainForm
        public event EventHandler PindahKeRegister;
        public event Action<Akun> LoginBerhasil;

        public LoginControl()
        {
            InitializeComponent();
            this.authService = new AuthService();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowPassword.Checked)
            {
                // Hilangkan titik-titik (tampilkan teks asli)
                this.txtPassword.PasswordChar = '\0';
            }
            else
            {
                // Kembalikan jadi titik-titik
                this.txtPassword.PasswordChar = '●';
            }
        }

        private void btnMasuk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtUsername.Text) || string.IsNullOrWhiteSpace(this.txtPassword.Text))
            {
                MessageBox.Show("Eh bestie, username sama passwordnya diisi dulu dong!", "Oopss!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Akun akun = this.authService.ProsesLogin(this.txtUsername.Text, this.txtPassword.Text);

                if (akun != null)
                {
                    // Kirim sinyal ke MainForm beserta data akun yang login
                    if (this.LoginBerhasil != null)
                    {
                        this.LoginBerhasil.Invoke(akun);
                    }
                    else
                    {
                        // Fallback jika MainForm tidak melanggan event ini
                    }
                }
                else
                {
                    // AuthService.ProsesLogin sudah memunculkan UXHelper error, jadi tidak perlu MessageBox lagi di sini
                }
            }
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            if (this.PindahKeRegister != null)
            {
                this.PindahKeRegister.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // Fallback
            }
        }
    }
}
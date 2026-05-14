using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class RegisterControl : UserControl
    {
        private UserService userService;

        public event EventHandler PindahKeLogin;

        public RegisterControl()
        {
            InitializeComponent();
            this.userService = new UserService();
        }

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

        private void btnDaftarBaru_Click(object sender, EventArgs e)
        {
            // Bersihkan nomor telepon dari spasi (MaskedTextBox)
            string nomorTeleponBersih = this.txtTelp.Text.Replace(" ", "").Trim();

            if (string.IsNullOrWhiteSpace(this.txtNama.Text) ||
                string.IsNullOrWhiteSpace(this.txtUsername.Text) ||
                string.IsNullOrWhiteSpace(this.txtPassword.Text) ||
                string.IsNullOrWhiteSpace(this.txtEmail.Text))
            {
                MessageBox.Show("Jangan ada yang kosong ya bestie, diisi semua dong!", "Ups!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                User userBaru = new User();
                userBaru.NamaLengkap = this.txtNama.Text.Trim();
                userBaru.Username = this.txtUsername.Text.Trim();

                // ✅ FIX: Set Password dan Email yang sebelumnya tidak pernah di-set!
                userBaru.Password = this.txtPassword.Text;
                userBaru.Email = this.txtEmail.Text.Trim();

                // Hanya set nomor telepon jika diisi (tidak wajib kosong)
                if (!string.IsNullOrWhiteSpace(nomorTeleponBersih))
                {
                    userBaru.NomorTelepon = nomorTeleponBersih;
                }

                bool sukses = this.userService.DaftarPenggunaBaru(userBaru);

                if (sukses)
                {
                    if (this.PindahKeLogin != null)
                    {
                        this.PindahKeLogin.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Waduh, data kurang valid nih!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            if (this.PindahKeLogin != null)
            {
                this.PindahKeLogin.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
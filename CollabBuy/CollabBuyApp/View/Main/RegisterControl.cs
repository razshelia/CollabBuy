using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Main
{
    public partial class RegisterControl : UserControl
    {
        public event EventHandler OnRegistrationComplete;
        private readonly UserController _userController;

        public RegisterControl()
        {
            InitializeComponent();
            _userController = new UserController();
            this.Resize += RegisterControl_Resize;
        }

        private void RegisterControl_Resize(object sender, EventArgs e)
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.Height - pnlCard.Height) / 2;
            }
        }

        // --- FITUR BARU: Nahan Input Huruf di No WA ---
        private void txtNoTelepon_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Hanya izinkan angka (0-9) dan tombol kontrol (seperti Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Batalkan input jika yang diketik bukan angka
                e.Handled = true;
            }
        }
        // ----------------------------------------------

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char pwChar = chkShowPassword.Checked ? '\0' : '●';
            txtPassword.PasswordChar = pwChar;
            txtKonfirmasiPassword.PasswordChar = pwChar;
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            string email = txtEmail.Text.Trim();
            string noTelepon = txtNoTelepon.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string konfirmasiPassword = txtKonfirmasiPassword.Text;

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(noTelepon))
            {
                MessageBox.Show("Nama, Email, No WA, Username, sama Password jangan ada yang dikosongin ya bestie.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != konfirmasiPassword)
            {
                MessageBox.Show("Password atas bawah beda tuh, ketik ulang ya biar gak keliru!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKonfirmasiPassword.Clear();
                txtKonfirmasiPassword.Focus();
                return;
            }

            try
            {
                var (sukses, pesan) = _userController.RegistrasiPembeli(nama, email, noTelepon, username, password);

                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses Banget!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnRegistrationComplete?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show(pesan, "Yah Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Waduh error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            OnRegistrationComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
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
            this.InitializeComponent();

            this._userController = new UserController();

            this.Resize += this.RegisterControl_Resize;
        }

        private void RegisterControl_Resize(object sender, EventArgs e)
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

        // --- FITUR BARU: Nahan Input Huruf di No WA ---
        private void txtNoTelepon_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Hanya izinkan angka (0-9) dan tombol kontrol (seperti Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Batalkan input jika yang diketik bukan angka
                e.Handled = true;
            }
            else
            {
                bool inputValid = true;
            }
        }
        // ----------------------------------------------

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowPassword.Checked)
            {
                this.txtPassword.PasswordChar = '\0';
                this.txtKonfirmasiPassword.PasswordChar = '\0';
            }
            else
            {
                this.txtPassword.PasswordChar = '●';
                this.txtKonfirmasiPassword.PasswordChar = '●';
            }
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            string nama = this.txtNama.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string noTelepon = this.txtNoTelepon.Text.Trim();
            string username = this.txtUsername.Text.Trim();
            string password = this.txtPassword.Text;
            string konfirmasiPassword = this.txtKonfirmasiPassword.Text;

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(noTelepon))
            {
                MessageBox.Show("Nama, Email, No WA, Username, sama Password jangan ada yang dikosongin ya bestie.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (password != konfirmasiPassword)
                {
                    MessageBox.Show("Password atas bawah beda tuh, ketik ulang ya biar gak keliru!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtKonfirmasiPassword.Clear();
                    this.txtKonfirmasiPassword.Focus();
                }
                else
                {
                    try
                    {
                        var (sukses, pesan) = this._userController.RegistrasiPembeli(nama, email, noTelepon, username, password);

                        if (sukses)
                        {
                            MessageBox.Show(pesan, "Sukses Banget!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (this.OnRegistrationComplete != null)
                            {
                                this.OnRegistrationComplete.Invoke(this, EventArgs.Empty);
                            }
                            else
                            {
                                bool tidakAdaSubscriber = true;
                            }
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
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (this.OnRegistrationComplete != null)
            {
                this.OnRegistrationComplete.Invoke(this, EventArgs.Empty);
            }
            else
            {
                bool tidakAdaSubscriber = true;
            }
        }
    }
}
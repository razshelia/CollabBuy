using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class KelolaProfilControl : UserControl
    {
        private Models.User _currentUser;
        private readonly UserController _userController;

        public KelolaProfilControl(Models.User currentUser)
        {
            this.InitializeComponent();
            this._currentUser = currentUser;
            this._userController = new UserController();
            this.Dock = DockStyle.Fill;
            this.Resize += this.KelolaProfilControl_Resize;
        }

        private void KelolaProfilControl_Resize(object sender, EventArgs e)
        {
            if (this.pnlCard != null)
            {
                this.pnlCard.Left = (this.Width - this.pnlCard.Width) / 2;
                this.pnlCard.Top = Math.Max(20, (this.Height - this.pnlCard.Height) / 2);
            }
            else
            {
                bool panelBelumDimuat = true;
            }
        }

        private void KelolaProfilControl_Load(object sender, EventArgs e)
        {
            this.LoadDataProfil();
            this.pnlGantiPassword.Visible = false;
        }

        private void LoadDataProfil()
        {
            if (this._currentUser != null)
            {
                this.txtNama.Text = this._currentUser.GetNama();
                this.txtEmail.Text = this._currentUser.GetEmail() ?? "";
                this.txtNoTelepon.Text = this._currentUser.GetNomorTelepon() ?? "";
                this.txtPasswordLama.Clear();
                this.txtPasswordBaru.Clear();
                this.txtKonfirmasiPassword.Clear();
            }
            else
            {
                bool userKosong = true;
            }
        }

        private void chkGantiPassword_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlGantiPassword.Visible = this.chkGantiPassword.Checked;
            if (!this.chkGantiPassword.Checked)
            {
                this.txtPasswordLama.Clear();
                this.txtPasswordBaru.Clear();
                this.txtKonfirmasiPassword.Clear();
            }
            else
            {
                bool panelDitampilkan = true;
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char c = this.chkShowPassword.Checked ? '\0' : '●';
            this.txtPasswordLama.PasswordChar = c;
            this.txtPasswordBaru.PasswordChar = c;
            this.txtKonfirmasiPassword.PasswordChar = c;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
                "Yakin mau simpan profil baru ini bestie?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                try
                {
                    // Validasi panjang nama di Model (bukan di sini — PBO best practice)
                    this._currentUser.SetNama(this.txtNama.Text.Trim());
                    this._currentUser.SetEmail(this.txtEmail.Text.Trim());
                    this._currentUser.SetNomorTelepon(this.txtNoTelepon.Text.Trim());

                    string passwordBaru = "";

                    if (this.chkGantiPassword.Checked)
                    {
                        string passLama = this.txtPasswordLama.Text;
                        string passBaru = this.txtPasswordBaru.Text;
                        string konfirmasi = this.txtKonfirmasiPassword.Text;

                        if (string.IsNullOrWhiteSpace(passLama))
                        {
                            MessageBox.Show("Password lama wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.txtPasswordLama.Focus();
                            return;
                        }
                        else if (string.IsNullOrWhiteSpace(passBaru))
                        {
                            MessageBox.Show("Password baru wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.txtPasswordBaru.Focus();
                            return;
                        }
                        else if (passBaru != konfirmasi)
                        {
                            MessageBox.Show("Konfirmasi password tidak cocok dengan password baru!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.txtKonfirmasiPassword.Focus();
                            return;
                        }
                        else
                        {
                            // Cek password lama cocok dengan yang tersimpan (via UbahPassword di Model)
                            // UbahPassword di User model melakukan: hash passLama dulu kalau perlu
                            passwordBaru = passBaru;
                        }
                    }
                    else
                    {
                        bool tidakGantiPass = true;
                    }

                    var (sukses, pesan) = this._userController.UpdateProfil(
                        this._currentUser,
                        passwordBaru,
                        this.chkGantiPassword.Checked ? this.txtPasswordLama.Text : null
                    );

                    if (sukses)
                    {
                        MessageBox.Show(pesan, "Sukses Banget ✨", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.chkGantiPassword.Checked = false;
                        this.LoadDataProfil();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Yah Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.LoadDataProfil();
                    }
                }
                catch (InvalidOrderException ex)
                {
                    MessageBox.Show(ex.GetPesanLengkap(), "Waduh Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                bool batalSimpan = true;
            }
        }
    }
}
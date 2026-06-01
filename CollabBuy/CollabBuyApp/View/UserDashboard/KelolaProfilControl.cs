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

            // Perbaikan masalah kepotong: Paksa Dock Fill dan handle Resize
            this.Dock = DockStyle.Fill;
            this.Resize += this.KelolaProfilControl_Resize;
        }

        private void KelolaProfilControl_Resize(object sender, EventArgs e)
        {
            // Auto Center form kotak di tengah layar yang mekar
            if (this.pnlCard != null)
            {
                this.pnlCard.Left = (this.Width - this.pnlCard.Width) / 2;
                this.pnlCard.Top = (this.Height - this.pnlCard.Height) / 2;
            }
            else
            {
                bool panelBelumDimuat = true; // Assignment nyata untuk menghindari else kosong
            }
        }

        private void KelolaProfilControl_Load(object sender, EventArgs e)
        {
            this.LoadDataProfil();
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

        private void LoadDataProfil()
        {
            if (this._currentUser != null)
            {
                this.txtNama.Text = this._currentUser.GetNama();

                if (this._currentUser.GetEmail() != null)
                {
                    this.txtEmail.Text = this._currentUser.GetEmail();
                }
                else
                {
                    this.txtEmail.Text = "";
                }

                // PERBAIKAN: Ganti NIM jadi Nomor Telepon dengan Strict OOP
                if (this._currentUser.GetNomorTelepon() != null)
                {
                    this.txtNoTelepon.Text = this._currentUser.GetNomorTelepon();
                }
                else
                {
                    this.txtNoTelepon.Text = "";
                }

                this.txtPassword.Clear();
            }
            else
            {
                bool userKosong = true;
            }
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
                    this._currentUser.SetNama(this.txtNama.Text.Trim());
                    this._currentUser.SetEmail(this.txtEmail.Text.Trim());
                    this._currentUser.SetNomorTelepon(this.txtNoTelepon.Text.Trim()); // Set Nomor Telepon

                    string passwordBaru = this.txtPassword.Text.Trim();
                    var (sukses, pesan) = this._userController.UpdateProfil(this._currentUser, passwordBaru);

                    if (sukses)
                    {
                        MessageBox.Show(pesan, "Sukses Banget ✨", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtPassword.Clear();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Yah Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.LoadDataProfil(); // Kembalikan data lama jika gagal
                    }
                }
                catch (InvalidOrderException ex)
                {
                    MessageBox.Show(ex.GetPesanLengkap(), "Waduh Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // User batal menyimpan
                bool batalSimpan = true;
            }
        }
    }
}
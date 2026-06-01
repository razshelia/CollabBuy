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
        private User _currentUser;
        private readonly UserController _userController;

        public KelolaProfilControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userController = new UserController();

            // Perbaikan masalah kepotong: Paksa Dock Fill dan handle Resize
            this.Dock = DockStyle.Fill;
            this.Resize += KelolaProfilControl_Resize;
        }

        private void KelolaProfilControl_Resize(object sender, EventArgs e)
        {
            // Auto Center form kotak di tengah layar yang mekar
            if (pnlCard != null)
            {
                pnlCard.Left = (this.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.Height - pnlCard.Height) / 2;
            }
        }

        private void KelolaProfilControl_Load(object sender, EventArgs e)
        {
            LoadDataProfil();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void LoadDataProfil()
        {
            if (_currentUser != null)
            {
                txtNama.Text = _currentUser.GetNama();
                txtEmail.Text = _currentUser.GetEmail() ?? "";

                // PERBAIKAN: Ganti NIM jadi Nomor Telepon
                txtNoTelepon.Text = _currentUser.GetNomorTelepon() ?? "";

                txtPassword.Clear();
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
                    _currentUser.SetNama(txtNama.Text.Trim());
                    _currentUser.SetEmail(txtEmail.Text.Trim());
                    _currentUser.SetNomorTelepon(txtNoTelepon.Text.Trim()); // Set Nomor Telepon

                    string passwordBaru = txtPassword.Text.Trim();
                    var (sukses, pesan) = _userController.UpdateProfil(_currentUser, passwordBaru);

                    if (sukses)
                    {
                        MessageBox.Show(pesan, "Sukses Banget ✨", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Yah Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadDataProfil(); // Kembalikan data lama jika gagal
                    }
                }
                catch (InvalidOrderException ex)
                {
                    MessageBox.Show(ex.GetPesanLengkap(), "Waduh Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
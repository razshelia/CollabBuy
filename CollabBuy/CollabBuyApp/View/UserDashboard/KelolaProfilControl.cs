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
                this.pnlCard.Left = Math.Max(20, (this.Width - this.pnlCard.Width) / 2);
        }

        private void KelolaProfilControl_Load(object sender, EventArgs e)
        {
            this.LoadDataProfil();
        }

        private void LoadDataProfil()
        {
            if (this._currentUser == null) return;

            this.txtNama.Text = this._currentUser.Nama;
            this.txtUsername.Text = this._currentUser.Username;
            this.txtEmail.Text = this._currentUser.Email ?? "";
            this.txtNoTelepon.Text = this._currentUser.NomorTelepon ?? "";

            this.txtPasswordLama.Clear();
            this.txtPasswordBaru.Clear();
            this.txtKonfirmasiPassword.Clear();
            this.chkGantiPassword.Checked = false;
            this.pnlGantiPassword.Visible = false;

            Penjual penjual = this._currentUser as Penjual;
            if (penjual != null)
                this.txtNamaToko.Text = penjual.NamaToko ?? "";

            this.UpdateLayout();
        }

        private void UpdateLayout()
        {
            this.pnlCard.SuspendLayout();

            bool isPenjual = this._currentUser is Penjual;

            // Tampilkan/sembunyikan nama toko
            this.lblNamaToko.Visible = isPenjual;
            this.txtNamaToko.Visible = isPenjual;

            // Mulai dari bawah txtNoTelepon (Y=303, tinggi=29, margin=20)
            int y = this.txtNoTelepon.Bottom + 20;

            // Nama Toko — hanya untuk penjual
            if (isPenjual)
            {
                this.lblNamaToko.Top = y;
                y += this.lblNamaToko.Height + 5;
                this.txtNamaToko.Top = y;
                y = this.txtNamaToko.Bottom + 20;
            }

            // Checkbox Ganti Password
            this.chkGantiPassword.Top = y;
            y = this.chkGantiPassword.Bottom + 8;

            // Panel password (muncul/sembunyikan sesuai checkbox)
            if (this.chkGantiPassword.Checked)
            {
                this.pnlGantiPassword.Top = y;
                this.pnlGantiPassword.Visible = true;
                y = this.pnlGantiPassword.Bottom + 16;
            }
            else
            {
                this.pnlGantiPassword.Visible = false;
            }

            // Tombol Simpan
            this.btnSimpan.Top = y;
            y = this.btnSimpan.Bottom + 30;

            // Panjangkan kartu sesuai konten
            this.pnlCard.Height = y;

            this.pnlCard.ResumeLayout(true);

            // Aktifkan scroll kalau kartu lebih tinggi dari area
            this.AutoScrollMinSize = new Size(0, this.pnlCard.Bottom + 20);

            // Tengahkan kartu
            this.KelolaProfilControl_Resize(null, null);
        }

        private void chkGantiPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkGantiPassword.Checked)
            {
                this.txtPasswordLama.Clear();
                this.txtPasswordBaru.Clear();
                this.txtKonfirmasiPassword.Clear();
            }
            this.UpdateLayout();
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

            if (dialog != DialogResult.Yes) return;

            try
            {
                string usernameBaru = this.txtUsername.Text.Trim();
                if (usernameBaru.Length < 4)
                {
                    MessageBox.Show("Username minimal 4 karakter!", "Validasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtUsername.Focus();
                    return;
                }

                if (usernameBaru != this._currentUser.Username)
                {
                    bool tersedia = this._userController.IsUsernameAvailable(
                        this._currentUser.IdUser, usernameBaru);
                    if (!tersedia)
                    {
                        MessageBox.Show(
                            "Username \"" + usernameBaru + "\" sudah dipakai orang lain. Coba yang lain!",
                            "Username Tidak Tersedia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtUsername.Focus();
                        return;
                    }
                }

                this._currentUser.Nama = this.txtNama.Text.Trim();
                this._currentUser.Username = usernameBaru;
                this._currentUser.Email = this.txtEmail.Text.Trim();
                this._currentUser.NomorTelepon = this.txtNoTelepon.Text.Trim();

                Penjual penjualEdit = this._currentUser as Penjual;
                if (penjualEdit != null)
                {
                    string namaTokoBaru = this.txtNamaToko.Text.Trim();
                    if (string.IsNullOrWhiteSpace(namaTokoBaru))
                    {
                        MessageBox.Show("Nama toko tidak boleh kosong!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtNamaToko.Focus();
                        return;
                    }
                    penjualEdit.NamaToko = namaTokoBaru;
                }

                string passwordBaru = "";

                if (this.chkGantiPassword.Checked)
                {
                    string passLama = this.txtPasswordLama.Text;
                    string passBaru = this.txtPasswordBaru.Text;
                    string konfirmasi = this.txtKonfirmasiPassword.Text;

                    if (string.IsNullOrWhiteSpace(passLama))
                    {
                        MessageBox.Show("Password lama wajib diisi!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtPasswordLama.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(passBaru))
                    {
                        MessageBox.Show("Password baru wajib diisi!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtPasswordBaru.Focus();
                        return;
                    }
                    if (passBaru.Length < 8)
                    {
                        MessageBox.Show("Password baru minimal 8 karakter!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtPasswordBaru.Focus();
                        return;
                    }
                    if (passBaru != konfirmasi)
                    {
                        MessageBox.Show("Konfirmasi password tidak cocok!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtKonfirmasiPassword.Focus();
                        return;
                    }
                    passwordBaru = passBaru;
                }

                var (sukses, pesan) = this._userController.UpdateProfil(
                    this._currentUser,
                    passwordBaru,
                    this.chkGantiPassword.Checked ? this.txtPasswordLama.Text : null
                );

                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses Banget ✨",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.chkGantiPassword.Checked = false;
                    this.LoadDataProfil();
                }
                else
                {
                    MessageBox.Show(pesan, "Yah Gagal",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtPasswordLama.Clear();
                    this.txtPasswordBaru.Clear();
                    this.txtKonfirmasiPassword.Clear();
                    if (this.chkGantiPassword.Checked) this.txtPasswordLama.Focus();
                }
            }
            catch (InvalidOrderException ex)
            {
                MessageBox.Show(ex.GetPesanLengkap(), "Waduh Validasi Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
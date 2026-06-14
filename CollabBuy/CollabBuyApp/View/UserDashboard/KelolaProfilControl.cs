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
                this.txtNama.Text = this._currentUser.Nama;
                this.txtUsername.Text = this._currentUser.Username;
                this.txtEmail.Text = this._currentUser.Email ?? "";
                this.txtNoTelepon.Text = this._currentUser.NomorTelepon ?? "";

                this.txtPasswordLama.Clear();
                this.txtPasswordBaru.Clear();
                this.txtKonfirmasiPassword.Clear();

                Penjual penjual = this._currentUser as Penjual;
                if (penjual != null)
                    this.txtNamaToko.Text = penjual.NamaToko ?? "";

                // Panggil layout dinamis
                this.UpdateLayout();
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
                // Validasi username tidak kosong
                string usernameBaru = this.txtUsername.Text.Trim();
                if (usernameBaru.Length < 4)
                {
                    MessageBox.Show("Username minimal 4 karakter!", "Validasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtUsername.Focus();
                    return;
                }

                // Cek keunikan username jika berubah
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

                // Set data ke model (validasi dilakukan di setter)
                this._currentUser.Nama = this.txtNama.Text.Trim();
                this._currentUser.Username = usernameBaru;
                this._currentUser.Email = this.txtEmail.Text.Trim();
                this._currentUser.NomorTelepon = this.txtNoTelepon.Text.Trim();

                // Update nama toko jika user adalah Penjual
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
                    else if (string.IsNullOrWhiteSpace(passBaru))
                    {
                        MessageBox.Show("Password baru wajib diisi!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtPasswordBaru.Focus();
                        return;
                    }
                    else if (passBaru.Length < 8)
                    {
                        MessageBox.Show("Password baru minimal 8 karakter!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtPasswordBaru.Focus();
                        return;
                    }
                    else if (passBaru != konfirmasi)
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

                    // PERBAIKAN: Hapus LoadDataProfil(), cukup kosongkan password-nya saja
                    // Biarkan nama/email yang barusan diketik tetap ada di layar
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
        private void UpdateLayout()
        {
            try
            {
                this.pnlCard.SuspendLayout(); // Tahan render sesaat agar tidak kedip

                // 1. PASTIKAN SEMUA KOMPONEN ADA DI DALAM KARTU UNGU
                if (this.lblNamaToko.Parent != this.pnlCard) this.pnlCard.Controls.Add(this.lblNamaToko);
                if (this.txtNamaToko.Parent != this.pnlCard) this.pnlCard.Controls.Add(this.txtNamaToko);
                if (this.chkGantiPassword.Parent != this.pnlCard) this.pnlCard.Controls.Add(this.chkGantiPassword);
                if (this.pnlGantiPassword.Parent != this.pnlCard) this.pnlCard.Controls.Add(this.pnlGantiPassword);
                if (this.btnSimpan.Parent != this.pnlCard) this.pnlCard.Controls.Add(this.btnSimpan);

                // Kembalikan ke posisi X (kiri) yang rata
                int paddingX = 39;
                this.lblNamaToko.Left = 35;
                this.txtNamaToko.Left = paddingX;
                this.chkGantiPassword.Left = paddingX;
                this.pnlGantiPassword.Left = paddingX;
                this.btnSimpan.Left = paddingX;

                // 2. KITA GUNAKAN ANGKA PASTI (Titik aman di bawah No WhatsApp adalah Y = 360)
                int currentY = 360;

                // 3. Logika Nama Toko (Hanya Penjual)
                bool isPenjual = (this._currentUser is Penjual);
                this.lblNamaToko.Visible = isPenjual;
                this.txtNamaToko.Visible = isPenjual;

                if (isPenjual)
                {
                    this.lblNamaToko.Top = currentY;
                    this.txtNamaToko.Top = currentY + 25;
                    currentY = this.txtNamaToko.Top + 45; // Turunkan Y sejauh tinggi textbox
                }

                // 4. Logika Checkbox Ganti Password
                this.chkGantiPassword.Top = currentY;
                currentY = currentY + 40; // Turunkan Y sejauh tinggi checkbox

                // 5. Logika Panel Password
                if (this.chkGantiPassword.Checked)
                {
                    this.pnlGantiPassword.Visible = true;
                    this.pnlGantiPassword.Top = currentY;
                    currentY = currentY + 240; // Turunkan Y sejauh tinggi panel (220) + margin
                }
                else
                {
                    this.pnlGantiPassword.Visible = false;
                }

                // 6. Posisikan Tombol Simpan
                this.btnSimpan.Top = currentY;

                // 7. BUKA SEGEL UKURAN KARTU DAN PANJANGKAN
                this.pnlCard.MaximumSize = new Size(0, 0);
                this.pnlCard.Height = this.btnSimpan.Top + 80; // Pastikan kartu membungkus tombol

                this.pnlCard.ResumeLayout(true);

                // 8. AKTIFKAN SCROLLBAR HALAMAN
                // Memberi tahu halaman kalau kartunya memanjang ke bawah
                this.AutoScrollMinSize = new Size(0, this.pnlCard.Bottom + 50);

                // Pertahankan kartu di tengah layar
                this.KelolaProfilControl_Resize(null, null);
            }
            catch (Exception ex)
            {
                // Jika masih ada sistem yang error diam-diam, kita tangkap dan tampilkan!
                MessageBox.Show("Gagal mengatur layout UI: " + ex.Message);
            }
        }
    }
}
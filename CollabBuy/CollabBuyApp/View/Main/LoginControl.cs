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
        private void btnLupaPassword_Click(object sender, EventArgs e)
        {
            this.TampilkanFormLupaPassword();
        }

        private void TampilkanFormLupaPassword()
        {
            Color purple = Color.FromArgb(36, 0, 70);
            Color lilac = Color.FromArgb(235, 204, 255);
            Color btnPurple = Color.FromArgb(200, 182, 255);
            Color yellow = Color.FromArgb(253, 255, 182);

            Form frm = new Form
            {
                Text = "🔑 Lupa Password — CollabBuy",
                Size = new Size(420, 520),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Font = new Font("Segoe UI", 10F)
            };

            // ── Panel verifikasi ──
            Panel pnlVerif = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(30, 20, 30, 20) };

            pnlVerif.Controls.Add(new Label
            {
                Text = "Verifikasi Identitas Kamu 🔍",
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = purple,
                AutoSize = true,
                Location = new Point(0, 0)
            });
            pnlVerif.Controls.Add(new Label
            {
                Text = "Masukkan 3 data yang kamu daftarkan.\nSemuanya harus cocok ya!",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(0, 38)
            });

            // Username
            pnlVerif.Controls.Add(new Label { Text = "Username", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new Point(0, 88) });
            TextBox txtUser = new TextBox { Location = new Point(0, 110), Size = new Size(340, 29), Font = new Font("Segoe UI", 11F), BackColor = Color.FromArgb(250, 250, 250) };
            pnlVerif.Controls.Add(txtUser);

            // Email
            pnlVerif.Controls.Add(new Label { Text = "Email", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new Point(0, 152) });
            TextBox txtEmail = new TextBox { Location = new Point(0, 174), Size = new Size(340, 29), Font = new Font("Segoe UI", 11F), BackColor = Color.FromArgb(250, 250, 250) };
            pnlVerif.Controls.Add(txtEmail);

            // Nomor Telepon
            pnlVerif.Controls.Add(new Label { Text = "Nomor WhatsApp / Telepon", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new Point(0, 216) });
            TextBox txtTelp = new TextBox { Location = new Point(0, 238), Size = new Size(340, 29), Font = new Font("Segoe UI", 11F), BackColor = Color.FromArgb(250, 250, 250) };
            pnlVerif.Controls.Add(txtTelp);

            // Label error
            Label lblErr = new Label
            {
                Text = "",
                ForeColor = Color.FromArgb(180, 0, 0),
                BackColor = Color.FromArgb(255, 220, 220),
                AutoSize = false,
                Size = new Size(340, 30),
                Location = new Point(0, 280),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Visible = false
            };
            pnlVerif.Controls.Add(lblErr);

            // Tombol Verifikasi
            Button btnVerif = new Button
            {
                Text = "🔍 Verifikasi Identitas",
                Location = new Point(0, 322),
                Size = new Size(340, 44),
                BackColor = btnPurple,
                ForeColor = purple,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnVerif.FlatAppearance.BorderColor = purple;
            btnVerif.FlatAppearance.BorderSize = 2;
            pnlVerif.Controls.Add(btnVerif);

            pnlVerif.Controls.Add(new Label
            {
                Text = "💡 Pastikan data persis sama seperti saat daftar.",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(0, 376)
            });

            frm.Controls.Add(pnlVerif);

            // ── Panel reset password (tersembunyi awalnya) ──
            Panel pnlReset = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(30, 20, 30, 20), Visible = false };

            pnlReset.Controls.Add(new Label
            {
                Text = "Buat Password Baru 🔐",
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = purple,
                AutoSize = true,
                Location = new Point(0, 0)
            });
            pnlReset.Controls.Add(new Label
            {
                Text = "Identitas terverifikasi ✅\nSekarang buat password baru kamu.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(0, 38)
            });

            pnlReset.Controls.Add(new Label { Text = "Password Baru (min. 6 karakter)", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new Point(0, 95) });
            TextBox txtBaru = new TextBox { Location = new Point(0, 117), Size = new Size(340, 29), Font = new Font("Segoe UI", 11F), PasswordChar = '●', BackColor = Color.FromArgb(250, 250, 250) };
            pnlReset.Controls.Add(txtBaru);

            pnlReset.Controls.Add(new Label { Text = "Konfirmasi Password Baru", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new Point(0, 160) });
            TextBox txtKonfirm = new TextBox { Location = new Point(0, 182), Size = new Size(340, 29), Font = new Font("Segoe UI", 11F), PasswordChar = '●', BackColor = Color.FromArgb(250, 250, 250) };
            pnlReset.Controls.Add(txtKonfirm);

            CheckBox chkShow = new CheckBox
            {
                Text = "Tampilkan password",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(150, 100, 200),
                AutoSize = true,
                Location = new Point(0, 224),
                Cursor = Cursors.Hand
            };
            chkShow.CheckedChanged += (s, ev) =>
            {
                txtBaru.PasswordChar = chkShow.Checked ? '\0' : '●';
                txtKonfirm.PasswordChar = chkShow.Checked ? '\0' : '●';
            };
            pnlReset.Controls.Add(chkShow);

            Label lblErrReset = new Label
            {
                Text = "",
                ForeColor = Color.FromArgb(180, 0, 0),
                BackColor = Color.FromArgb(255, 220, 220),
                AutoSize = false,
                Size = new Size(340, 30),
                Location = new Point(0, 252),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Visible = false
            };
            pnlReset.Controls.Add(lblErrReset);

            Button btnSimpan = new Button
            {
                Text = "💾 Simpan Password Baru",
                Location = new Point(0, 294),
                Size = new Size(340, 44),
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = yellow,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSimpan.FlatAppearance.BorderSize = 0;
            pnlReset.Controls.Add(btnSimpan);

            frm.Controls.Add(pnlReset);

            // ── Logika: simpan idUser yang terverifikasi ──
            int terverifikasiIdUser = 0;

            btnVerif.Click += (s, ev) =>
            {
                string uname = txtUser.Text.Trim();
                string email = txtEmail.Text.Trim();
                string telp = txtTelp.Text.Trim();

                if (string.IsNullOrWhiteSpace(uname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(telp))
                {
                    lblErr.Text = "Semua field wajib diisi!";
                    lblErr.Visible = true;
                    return;
                }

                int? idUser = this._userController.VerifikasiIdentitasUser(uname, email, telp);

                if (idUser.HasValue)
                {
                    terverifikasiIdUser = idUser.Value;
                    lblErr.Visible = false;
                    pnlVerif.Visible = false;
                    pnlReset.Visible = true;
                }
                else
                {
                    lblErr.Text = "Data tidak cocok. Cek lagi ya!";
                    lblErr.Visible = true;
                }
            };

            btnSimpan.Click += (s, ev) =>
            {
                string pw1 = txtBaru.Text;
                string pw2 = txtKonfirm.Text;

                if (pw1.Length < 6)
                {
                    lblErrReset.Text = "Password minimal 6 karakter!";
                    lblErrReset.Visible = true;
                    return;
                }
                if (pw1 != pw2)
                {
                    lblErrReset.Text = "Password tidak cocok!";
                    lblErrReset.Visible = true;
                    return;
                }

                bool ok = this._userController.ResetPasswordUser(terverifikasiIdUser, pw1);
                if (ok)
                {
                    MessageBox.Show(
                        "Password berhasil diganti! Silakan login dengan password baru. 🎉",
                        "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frm.Close();
                }
                else
                {
                    lblErrReset.Text = "Gagal menyimpan. Coba lagi.";
                    lblErrReset.Visible = true;
                }
            };

            frm.ShowDialog();
        }
    }
}
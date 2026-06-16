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
        }
        private void btnLupaPassword_Click(object sender, EventArgs e)
        {
            this.TampilkanFormLupaPassword();
        }

        private void TampilkanFormLupaPassword()
        {
            // 1. Sembunyikan panel login utama
            this.pnlCard.Visible = false;

            System.Drawing.Color purple = System.Drawing.Color.FromArgb(36, 0, 70);
            System.Drawing.Color btnPurple = System.Drawing.Color.FromArgb(200, 182, 255);
            System.Drawing.Color yellow = System.Drawing.Color.FromArgb(253, 255, 182);

            // 2. Buat panel Lupa Password yang posisinya presisi menimpa pnlCard
            Panel pnlLupaPasswordMain = new Panel();
            pnlLupaPasswordMain.Size = new System.Drawing.Size(400, 560);

            // PERBAIKAN: Posisi langsung dikunci ke tengah layar
            pnlLupaPasswordMain.Location = new System.Drawing.Point((this.Width - pnlLupaPasswordMain.Width) / 2, (this.Height - pnlLupaPasswordMain.Height) / 2);
            pnlLupaPasswordMain.BackColor = System.Drawing.Color.White;
            pnlLupaPasswordMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // PERBAIKAN: Agar tetap di tengah saat layar dibesarkan/dikecilkan
            System.EventHandler resizeHandler = null;
            resizeHandler = (s, ev) =>
            {
                if (pnlLupaPasswordMain != null)
                {
                    pnlLupaPasswordMain.Left = (this.Width - pnlLupaPasswordMain.Width) / 2;
                    pnlLupaPasswordMain.Top = (this.Height - pnlLupaPasswordMain.Height) / 2;
                }
            };
            this.Resize += resizeHandler;

            this.Controls.Add(pnlLupaPasswordMain);
            pnlLupaPasswordMain.BringToFront();

            // =========================================================
            // PANEL VERIFIKASI IDENTITAS
            // =========================================================
            Panel pnlVerif = new Panel();
            pnlVerif.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlVerif.BackColor = System.Drawing.Color.White;

            pnlVerif.Controls.Add(new Label
            {
                Text = "Verifikasi Identitas Kamu 🔍",
                Font = new System.Drawing.Font("Segoe UI Black", 13F, System.Drawing.FontStyle.Bold),
                ForeColor = purple,
                AutoSize = true,
                Location = new System.Drawing.Point(30, 30)
            });

            pnlVerif.Controls.Add(new Label
            {
                Text = "Masukkan 3 data yang kamu daftarkan.\nSemuanya harus cocok ya!",
                Font = new System.Drawing.Font("Segoe UI", 9.5F),
                ForeColor = System.Drawing.Color.DimGray,
                AutoSize = true,
                Location = new System.Drawing.Point(30, 65)
            });

            pnlVerif.Controls.Add(new Label { Text = "Username", Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new System.Drawing.Point(30, 120) });
            TextBox txtUser = new TextBox { Location = new System.Drawing.Point(30, 145), Size = new System.Drawing.Size(340, 29), Font = new System.Drawing.Font("Segoe UI", 11F), BackColor = System.Drawing.Color.FromArgb(250, 250, 250) };
            pnlVerif.Controls.Add(txtUser);

            pnlVerif.Controls.Add(new Label { Text = "Email", Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new System.Drawing.Point(30, 190) });
            TextBox txtEmail = new TextBox { Location = new System.Drawing.Point(30, 215), Size = new System.Drawing.Size(340, 29), Font = new System.Drawing.Font("Segoe UI", 11F), BackColor = System.Drawing.Color.FromArgb(250, 250, 250) };
            pnlVerif.Controls.Add(txtEmail);

            pnlVerif.Controls.Add(new Label { Text = "Nomor WhatsApp / Telepon", Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new System.Drawing.Point(30, 260) });
            TextBox txtTelp = new TextBox { Location = new System.Drawing.Point(30, 285), Size = new System.Drawing.Size(340, 29), Font = new System.Drawing.Font("Segoe UI", 11F), BackColor = System.Drawing.Color.FromArgb(250, 250, 250) };

            // PERBAIKAN: Membatasi input agar HANYA BISA mengetik ANGKA 
            txtTelp.KeyPress += (s, ev) =>
            {
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar))
                {
                    ev.Handled = true; // Tolak karakter selain angka
                }
            };
            pnlVerif.Controls.Add(txtTelp);

            Label lblErr = new Label
            {
                Text = "",
                ForeColor = System.Drawing.Color.FromArgb(180, 0, 0),
                BackColor = System.Drawing.Color.FromArgb(255, 220, 220),
                AutoSize = false,
                Size = new System.Drawing.Size(340, 30),
                Location = new System.Drawing.Point(30, 330),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new System.Windows.Forms.Padding(6, 0, 0, 0),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                Visible = false
            };
            pnlVerif.Controls.Add(lblErr);

            Button btnVerif = new Button
            {
                Text = "🔍 Verifikasi Identitas",
                Location = new System.Drawing.Point(30, 370),
                Size = new System.Drawing.Size(340, 44),
                BackColor = btnPurple,
                ForeColor = purple,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            btnVerif.FlatAppearance.BorderColor = purple;
            btnVerif.FlatAppearance.BorderSize = 2;
            pnlVerif.Controls.Add(btnVerif);

            // FITUR BARU: TOMBOL KEMBALI AGAR USER TIDAK TERJEBAK
            Button btnKembaliVerif = new Button
            {
                Text = "⬅ Kembali ke Login",
                Location = new System.Drawing.Point(30, 425),
                Size = new System.Drawing.Size(340, 35),
                BackColor = System.Drawing.Color.White,
                ForeColor = purple,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            btnKembaliVerif.FlatAppearance.BorderSize = 0;
            pnlVerif.Controls.Add(btnKembaliVerif);

            btnKembaliVerif.Click += (s, ev) =>
            {
                this.Controls.Remove(pnlLupaPasswordMain);
                pnlLupaPasswordMain.Dispose();
                this.pnlCard.Visible = true; // Munculkan Login kembali
                this.Resize -= resizeHandler; // Lepas event biar gak bocor memori
            };

            pnlLupaPasswordMain.Controls.Add(pnlVerif);

            // =========================================================
            // PANEL RESET PASSWORD
            // =========================================================
            Panel pnlReset = new Panel();
            pnlReset.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlReset.BackColor = System.Drawing.Color.White;
            pnlReset.Visible = false; // Sembunyikan dulu

            pnlReset.Controls.Add(new Label
            {
                Text = "Buat Password Baru 🔐",
                Font = new System.Drawing.Font("Segoe UI Black", 13F, System.Drawing.FontStyle.Bold),
                ForeColor = purple,
                AutoSize = true,
                Location = new System.Drawing.Point(30, 30)
            });

            pnlReset.Controls.Add(new Label
            {
                Text = "Identitas terverifikasi ✅\nSekarang buat password baru kamu.",
                Font = new System.Drawing.Font("Segoe UI", 9.5F),
                ForeColor = System.Drawing.Color.DimGray,
                AutoSize = true,
                Location = new System.Drawing.Point(30, 65)
            });

            pnlReset.Controls.Add(new Label { Text = "Password Baru (min. 6 karakter)", Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new System.Drawing.Point(30, 130) });
            TextBox txtBaru = new TextBox { Location = new System.Drawing.Point(30, 155), Size = new System.Drawing.Size(340, 29), Font = new System.Drawing.Font("Segoe UI", 11F), PasswordChar = '●', BackColor = System.Drawing.Color.FromArgb(250, 250, 250) };
            pnlReset.Controls.Add(txtBaru);

            pnlReset.Controls.Add(new Label { Text = "Konfirmasi Password Baru", Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = purple, AutoSize = true, Location = new System.Drawing.Point(30, 200) });
            TextBox txtKonfirm = new TextBox { Location = new System.Drawing.Point(30, 225), Size = new System.Drawing.Size(340, 29), Font = new System.Drawing.Font("Segoe UI", 11F), PasswordChar = '●', BackColor = System.Drawing.Color.FromArgb(250, 250, 250) };
            pnlReset.Controls.Add(txtKonfirm);

            CheckBox chkShow = new CheckBox
            {
                Text = "Tampilkan password",
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(150, 100, 200),
                AutoSize = true,
                Location = new System.Drawing.Point(30, 270),
                Cursor = System.Windows.Forms.Cursors.Hand
            };

            // Strict OOP CheckBox
            chkShow.CheckedChanged += (s, ev) =>
            {
                if (chkShow.Checked)
                {
                    txtBaru.PasswordChar = '\0';
                    txtKonfirm.PasswordChar = '\0';
                }
                else
                {
                    txtBaru.PasswordChar = '●';
                    txtKonfirm.PasswordChar = '●';
                }
            };
            pnlReset.Controls.Add(chkShow);

            Label lblErrReset = new Label
            {
                Text = "",
                ForeColor = System.Drawing.Color.FromArgb(180, 0, 0),
                BackColor = System.Drawing.Color.FromArgb(255, 220, 220),
                AutoSize = false,
                Size = new System.Drawing.Size(340, 30),
                Location = new System.Drawing.Point(30, 310),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new System.Windows.Forms.Padding(6, 0, 0, 0),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                Visible = false
            };
            pnlReset.Controls.Add(lblErrReset);

            Button btnSimpan = new Button
            {
                Text = "💾 Simpan Password Baru",
                Location = new System.Drawing.Point(30, 355),
                Size = new System.Drawing.Size(340, 44),
                BackColor = System.Drawing.Color.FromArgb(36, 0, 70),
                ForeColor = yellow,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            btnSimpan.FlatAppearance.BorderSize = 0;
            pnlReset.Controls.Add(btnSimpan);

            Button btnBatalReset = new Button
            {
                Text = "❌ Batal",
                Location = new System.Drawing.Point(30, 410),
                Size = new System.Drawing.Size(340, 35),
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(180, 0, 0),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            btnBatalReset.FlatAppearance.BorderSize = 0;
            pnlReset.Controls.Add(btnBatalReset);

            btnBatalReset.Click += (s, ev) =>
            {
                this.Controls.Remove(pnlLupaPasswordMain);
                pnlLupaPasswordMain.Dispose();
                this.pnlCard.Visible = true;
                this.Resize -= resizeHandler;
            };

            pnlLupaPasswordMain.Controls.Add(pnlReset);

            // =========================================================
            // LOGIKA PROSES & STRICT OOP ENCAPSULATION
            // =========================================================
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
                }
                else
                {
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
                }
            };

            btnSimpan.Click += (s, ev) =>
            {
                string pw1 = txtBaru.Text;
                string pw2 = txtKonfirm.Text;

                // ENKAPSULASI: Memanfaatkan Model User/Pembeli untuk memeriksa format Password
                try
                {
                    Models.Pembeli dummyUser = new Models.Pembeli("Dummy", "dummy_uname", pw1);
                    dummyUser.Validate(); // Method Validate() di Model akan memproteksi jika password invalid

                    if (pw1 != pw2)
                    {
                        lblErrReset.Text = "Password atas bawah tidak cocok!";
                        lblErrReset.Visible = true;
                    }
                    else
                    {
                        bool ok = this._userController.ResetPasswordUser(terverifikasiIdUser, pw1);

                        if (ok)
                        {
                            MessageBox.Show(
                                "Password berhasil diganti! Silakan login dengan password baru. 🎉",
                                "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Controls.Remove(pnlLupaPasswordMain);
                            pnlLupaPasswordMain.Dispose();
                            this.pnlCard.Visible = true;
                            this.Resize -= resizeHandler;
                        }
                        else
                        {
                            lblErrReset.Text = "Gagal menyimpan. Coba lagi.";
                            lblErrReset.Visible = true;
                        }
                    }
                }
                catch (Exceptions.InvalidOrderException ex)
                {
                    // Menangkap penolakan dari Model (enkapsulasi bekerja dengan baik!)
                    lblErrReset.Text = ex.GetPesanLengkap();
                    lblErrReset.Visible = true;
                }
            };
        }
    }
}
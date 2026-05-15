namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class RegisterControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlCard = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            txtNama = new TextBox();
            txtNomorTelepon = new TextBox();
            txtEmail = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtKonfirmasiPassword = new TextBox();
            chkLihatPassword = new CheckBox();
            chkSetuju = new CheckBox();
            lblSyaratKetentuan = new Label();
            btnDaftar = new Button();
            lblLoginLink = new Label();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.FromArgb(45, 27, 79);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(txtNama);
            pnlCard.Controls.Add(txtNomorTelepon);
            pnlCard.Controls.Add(txtEmail);
            pnlCard.Controls.Add(txtUsername);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(txtKonfirmasiPassword);
            pnlCard.Controls.Add(chkLihatPassword);
            pnlCard.Controls.Add(chkSetuju);
            pnlCard.Controls.Add(lblSyaratKetentuan);
            pnlCard.Controls.Add(btnDaftar);
            pnlCard.Controls.Add(lblLoginLink);
            pnlCard.Location = new Point(673, 316);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(460, 660);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Black", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(253, 224, 71);
            lblTitle.Location = new Point(40, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(380, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "DAFTAR DULU, GENGS! 🌟";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(167, 139, 250);
            lblSubtitle.Location = new Point(40, 70);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(380, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Gabung komunitas paling solid se-kampus!";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtNama
            // 
            txtNama.Font = new Font("Segoe UI", 11F);
            txtNama.Location = new Point(65, 105);
            txtNama.Name = "txtNama";
            txtNama.PlaceholderText = "Nama lengkap kamu...";
            txtNama.Size = new Size(330, 27);
            txtNama.TabIndex = 2;
            // 
            // txtNomorTelepon
            // 
            txtNomorTelepon.Font = new Font("Segoe UI", 11F);
            txtNomorTelepon.Location = new Point(65, 145);
            txtNomorTelepon.Name = "txtNomorTelepon";
            txtNomorTelepon.PlaceholderText = "Nomor telepon (angka aja)";
            txtNomorTelepon.Size = new Size(330, 27);
            txtNomorTelepon.TabIndex = 3;
            txtNomorTelepon.KeyPress += txtNomorTelepon_KeyPress;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(65, 185);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email aktif kamu...";
            txtEmail.Size = new Size(330, 27);
            txtEmail.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(65, 225);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username kece (min 5 karakter)";
            txtUsername.Size = new Size(330, 27);
            txtUsername.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(65, 265);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password (min 8 karakter) \U0001f92b";
            txtPassword.Size = new Size(330, 27);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtKonfirmasiPassword
            // 
            txtKonfirmasiPassword.Font = new Font("Segoe UI", 11F);
            txtKonfirmasiPassword.Location = new Point(65, 305);
            txtKonfirmasiPassword.Name = "txtKonfirmasiPassword";
            txtKonfirmasiPassword.PlaceholderText = "Ulangi password tadi...";
            txtKonfirmasiPassword.Size = new Size(330, 27);
            txtKonfirmasiPassword.TabIndex = 7;
            txtKonfirmasiPassword.UseSystemPasswordChar = true;
            // 
            // chkLihatPassword
            // 
            chkLihatPassword.Font = new Font("Segoe UI", 9F);
            chkLihatPassword.ForeColor = Color.FromArgb(167, 139, 250);
            chkLihatPassword.Location = new Point(65, 345);
            chkLihatPassword.Name = "chkLihatPassword";
            chkLihatPassword.Size = new Size(200, 20);
            chkLihatPassword.TabIndex = 8;
            chkLihatPassword.Text = "Lihat Password 👀";
            chkLihatPassword.CheckedChanged += chkLihatPassword_CheckedChanged;
            // 
            // chkSetuju
            // 
            chkSetuju.Font = new Font("Segoe UI", 9F);
            chkSetuju.ForeColor = Color.FromArgb(167, 139, 250);
            chkSetuju.Location = new Point(65, 375);
            chkSetuju.Name = "chkSetuju";
            chkSetuju.Size = new Size(330, 20);
            chkSetuju.TabIndex = 9;
            chkSetuju.Text = "Aku setuju sama Syarat & Ketentuan yang berlaku 📜";
            // 
            // lblSyaratKetentuan
            // 
            lblSyaratKetentuan.Cursor = Cursors.Hand;
            lblSyaratKetentuan.Font = new Font("Segoe UI", 8F, FontStyle.Underline);
            lblSyaratKetentuan.ForeColor = Color.FromArgb(253, 224, 71);
            lblSyaratKetentuan.Location = new Point(85, 395);
            lblSyaratKetentuan.Name = "lblSyaratKetentuan";
            lblSyaratKetentuan.Size = new Size(290, 18);
            lblSyaratKetentuan.TabIndex = 10;
            lblSyaratKetentuan.Text = "📋 Lihat Syarat & Ketentuan";
            lblSyaratKetentuan.TextAlign = ContentAlignment.MiddleLeft;
            lblSyaratKetentuan.Click += lblSyaratKetentuan_Click;
            // 
            // btnDaftar
            // 
            btnDaftar.BackColor = Color.FromArgb(167, 139, 250);
            btnDaftar.FlatAppearance.BorderSize = 0;
            btnDaftar.FlatStyle = FlatStyle.Flat;
            btnDaftar.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
            btnDaftar.ForeColor = Color.White;
            btnDaftar.Location = new Point(65, 430);
            btnDaftar.Name = "btnDaftar";
            btnDaftar.Size = new Size(330, 45);
            btnDaftar.TabIndex = 11;
            btnDaftar.Text = "GABUNG SEKARANG 💜";
            btnDaftar.UseVisualStyleBackColor = false;
            btnDaftar.Click += btnDaftar_Click;
            // 
            // lblLoginLink
            // 
            lblLoginLink.Cursor = Cursors.Hand;
            lblLoginLink.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            lblLoginLink.ForeColor = Color.FromArgb(253, 224, 71);
            lblLoginLink.Location = new Point(65, 490);
            lblLoginLink.Name = "lblLoginLink";
            lblLoginLink.Size = new Size(330, 25);
            lblLoginLink.TabIndex = 12;
            lblLoginLink.Text = "Udah punya akun? Login aja, bestie! 🔑";
            lblLoginLink.TextAlign = ContentAlignment.MiddleCenter;
            lblLoginLink.Click += lblLoginLink_Click;
            // 
            // RegisterControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(pnlCard);
            Name = "RegisterControl";
            Size = new Size(1046, 333);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblLoginLink, lblSyaratKetentuan;
        private System.Windows.Forms.TextBox txtNama, txtNomorTelepon, txtEmail, txtUsername, txtPassword, txtKonfirmasiPassword;
        private System.Windows.Forms.CheckBox chkLihatPassword, chkSetuju;
        private System.Windows.Forms.Button btnDaftar;
    }
}
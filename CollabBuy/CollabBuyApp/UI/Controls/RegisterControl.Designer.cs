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
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtNomorTelepon = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtKonfirmasiPassword = new System.Windows.Forms.TextBox();
            this.chkLihatPassword = new System.Windows.Forms.CheckBox();
            this.chkSetuju = new System.Windows.Forms.CheckBox();
            this.lblSyaratKetentuan = new System.Windows.Forms.Label();
            this.btnDaftar = new System.Windows.Forms.Button();
            this.lblLoginLink = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.txtNama);
            this.pnlCard.Controls.Add(this.txtNomorTelepon);
            this.pnlCard.Controls.Add(this.txtEmail);
            this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.txtKonfirmasiPassword);
            this.pnlCard.Controls.Add(this.chkLihatPassword);
            this.pnlCard.Controls.Add(this.chkSetuju);
            this.pnlCard.Controls.Add(this.lblSyaratKetentuan);
            this.pnlCard.Controls.Add(this.btnDaftar);
            this.pnlCard.Controls.Add(this.lblLoginLink);
            this.pnlCard.Location = new System.Drawing.Point(300, 30);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(460, 660);
            this.pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(460, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DAFTAR DULU, GENGS! 🌟";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 75);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(460, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Gabung komunitas paling solid se-kampus!";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNama.Location = new System.Drawing.Point(55, 125);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(350, 27);
            this.txtNama.TabIndex = 2;
            this.txtNama.PlaceholderText = "Nama lengkap kamu...";
            // 
            // txtNomorTelepon
            // 
            this.txtNomorTelepon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNomorTelepon.Location = new System.Drawing.Point(55, 170);
            this.txtNomorTelepon.Name = "txtNomorTelepon";
            this.txtNomorTelepon.Size = new System.Drawing.Size(350, 27);
            this.txtNomorTelepon.TabIndex = 3;
            this.txtNomorTelepon.PlaceholderText = "Nomor telepon (angka aja)";
            this.txtNomorTelepon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNomorTelepon_KeyPress);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.Location = new System.Drawing.Point(55, 215);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(350, 27);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.PlaceholderText = "Email aktif kamu...";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsername.Location = new System.Drawing.Point(55, 260);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(350, 27);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.PlaceholderText = "Username kece (min 5 karakter)";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.Location = new System.Drawing.Point(55, 305);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(350, 27);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.PlaceholderText = "Password (min 8 karakter) 🤫";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtKonfirmasiPassword
            // 
            this.txtKonfirmasiPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKonfirmasiPassword.Location = new System.Drawing.Point(55, 350);
            this.txtKonfirmasiPassword.Name = "txtKonfirmasiPassword";
            this.txtKonfirmasiPassword.Size = new System.Drawing.Size(350, 27);
            this.txtKonfirmasiPassword.TabIndex = 7;
            this.txtKonfirmasiPassword.PlaceholderText = "Ulangi password tadi...";
            this.txtKonfirmasiPassword.UseSystemPasswordChar = true;
            // 
            // chkLihatPassword
            // 
            this.chkLihatPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkLihatPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkLihatPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.chkLihatPassword.Location = new System.Drawing.Point(55, 390);
            this.chkLihatPassword.Name = "chkLihatPassword";
            this.chkLihatPassword.Size = new System.Drawing.Size(200, 20);
            this.chkLihatPassword.TabIndex = 8;
            this.chkLihatPassword.Text = "Lihat Password 👀";
            this.chkLihatPassword.CheckedChanged += new System.EventHandler(this.chkLihatPassword_CheckedChanged);
            // 
            // chkSetuju
            // 
            this.chkSetuju.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSetuju.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkSetuju.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.chkSetuju.Location = new System.Drawing.Point(55, 420);
            this.chkSetuju.Name = "chkSetuju";
            this.chkSetuju.Size = new System.Drawing.Size(350, 40);
            this.chkSetuju.TabIndex = 9;
            this.chkSetuju.Text = "Aku setuju sama Syarat & Ketentuan yang berlaku 📜";
            // 
            // lblSyaratKetentuan
            // 
            this.lblSyaratKetentuan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSyaratKetentuan.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblSyaratKetentuan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblSyaratKetentuan.Location = new System.Drawing.Point(55, 465);
            this.lblSyaratKetentuan.Name = "lblSyaratKetentuan";
            this.lblSyaratKetentuan.Size = new System.Drawing.Size(350, 20);
            this.lblSyaratKetentuan.TabIndex = 10;
            this.lblSyaratKetentuan.Text = "📋 Lihat Syarat & Ketentuan";
            this.lblSyaratKetentuan.Click += new System.EventHandler(this.lblSyaratKetentuan_Click);
            // 
            // btnDaftar
            // 
            this.btnDaftar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnDaftar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDaftar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnDaftar.FlatAppearance.BorderSize = 2;
            this.btnDaftar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDaftar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnDaftar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnDaftar.Location = new System.Drawing.Point(55, 510);
            this.btnDaftar.Name = "btnDaftar";
            this.btnDaftar.Size = new System.Drawing.Size(350, 50);
            this.btnDaftar.TabIndex = 11;
            this.btnDaftar.Text = "GABUNG SEKARANG 💜";
            this.btnDaftar.UseVisualStyleBackColor = false;
            this.btnDaftar.Click += new System.EventHandler(this.btnDaftar_Click);
            // 
            // lblLoginLink
            // 
            this.lblLoginLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoginLink.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblLoginLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblLoginLink.Location = new System.Drawing.Point(0, 580);
            this.lblLoginLink.Name = "lblLoginLink";
            this.lblLoginLink.Size = new System.Drawing.Size(460, 30);
            this.lblLoginLink.TabIndex = 12;
            this.lblLoginLink.Text = "Udah punya akun? Login aja, bestie! 🔑";
            this.lblLoginLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoginLink.Click += new System.EventHandler(this.lblLoginLink_Click);
            // 
            // RegisterControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlCard);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "RegisterControl";
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblLoginLink, lblSyaratKetentuan;
        private System.Windows.Forms.TextBox txtNama, txtNomorTelepon, txtEmail, txtUsername, txtPassword, txtKonfirmasiPassword;
        private System.Windows.Forms.CheckBox chkLihatPassword, chkSetuju;
        private System.Windows.Forms.Button btnDaftar;
    }
}
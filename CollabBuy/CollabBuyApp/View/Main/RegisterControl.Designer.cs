namespace CollabBuy.CollabBuyApp.View.Main
{
    partial class RegisterControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnDaftar = new System.Windows.Forms.Button();
            this.txtKonfirmasiPassword = new System.Windows.Forms.TextBox();
            this.lblKonfirmasiPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtNoTelepon = new System.Windows.Forms.TextBox();
            this.lblNoTelepon = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblNama = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogoCorner = new System.Windows.Forms.PictureBox();

            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoCorner)).BeginInit();
            this.SuspendLayout();

            // pnlCard
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.picLogoCorner);
            this.pnlCard.Controls.Add(this.chkShowPassword);
            this.pnlCard.Controls.Add(this.btnBatal);
            this.pnlCard.Controls.Add(this.btnDaftar);
            this.pnlCard.Controls.Add(this.txtKonfirmasiPassword);
            this.pnlCard.Controls.Add(this.lblKonfirmasiPassword);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.lblPassword);
            this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.lblUsername);
            this.pnlCard.Controls.Add(this.txtNoTelepon);
            this.pnlCard.Controls.Add(this.lblNoTelepon);
            this.pnlCard.Controls.Add(this.txtEmail);
            this.pnlCard.Controls.Add(this.lblEmail);
            this.pnlCard.Controls.Add(this.txtNama);
            this.pnlCard.Controls.Add(this.lblNama);
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Location = new System.Drawing.Point(320, 80);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(650, 490);
            this.pnlCard.TabIndex = 0;

            // picLogoCorner — pojok kiri atas, lebih besar
            this.picLogoCorner.Image = System.Drawing.Image.FromFile("collabbuy_logo.jpeg");
            this.picLogoCorner.Location = new System.Drawing.Point(20, 15);
            this.picLogoCorner.Size = new System.Drawing.Size(75, 75);
            this.picLogoCorner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogoCorner.TabStop = false;

            // lblTitle — sejajar logo, di kanan logo
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(105, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Join Circle Kita! ✨";

            // lblSubtitle — sejajar logo, di bawah lblTitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(107, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Isi form dulu yaa bestie biar bisa transaksi";

            // === FORM FIELDS (dinaikkan dari y=150 ke y=110) ===

            // lblNama & txtNama
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNama.Location = new System.Drawing.Point(40, 110);
            this.lblNama.Name = "lblNama";
            this.lblNama.TabIndex = 2;
            this.lblNama.Text = "Nama Asli Lo (No Fake)";

            this.txtNama.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtNama.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(40, 132);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(265, 29);
            this.txtNama.TabIndex = 3;

            // lblEmail & txtEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblEmail.Location = new System.Drawing.Point(340, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 15;
            this.lblEmail.Text = "Email Aktif";

            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(340, 132);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(265, 29);
            this.txtEmail.TabIndex = 16;

            // lblNoTelepon & txtNoTelepon
            this.lblNoTelepon.AutoSize = true;
            this.lblNoTelepon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoTelepon.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNoTelepon.Location = new System.Drawing.Point(40, 180);
            this.lblNoTelepon.Name = "lblNoTelepon";
            this.lblNoTelepon.TabIndex = 13;
            this.lblNoTelepon.Text = "No. WhatsApp (Biar Dihubungi)";

            this.txtNoTelepon.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtNoTelepon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoTelepon.Location = new System.Drawing.Point(40, 202);
            this.txtNoTelepon.MaxLength = 15;
            this.txtNoTelepon.Name = "txtNoTelepon";
            this.txtNoTelepon.Size = new System.Drawing.Size(265, 29);
            this.txtNoTelepon.TabIndex = 14;
            this.txtNoTelepon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoTelepon_KeyPress);

            // lblUsername & txtUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblUsername.Location = new System.Drawing.Point(340, 180);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Pilih Username Kamu";

            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(340, 202);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(265, 29);
            this.txtUsername.TabIndex = 5;

            // lblPassword & txtPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblPassword.Location = new System.Drawing.Point(40, 250);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Kata Sandi (Yang Susah Ditebak)";

            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(40, 272);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(265, 29);
            this.txtPassword.TabIndex = 7;

            // lblKonfirmasiPassword & txtKonfirmasiPassword
            this.lblKonfirmasiPassword.AutoSize = true;
            this.lblKonfirmasiPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKonfirmasiPassword.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblKonfirmasiPassword.Location = new System.Drawing.Point(340, 250);
            this.lblKonfirmasiPassword.Name = "lblKonfirmasiPassword";
            this.lblKonfirmasiPassword.TabIndex = 8;
            this.lblKonfirmasiPassword.Text = "Ketik Ulang Sandinya (Biar Gak Typo)";

            this.txtKonfirmasiPassword.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtKonfirmasiPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKonfirmasiPassword.Location = new System.Drawing.Point(340, 272);
            this.txtKonfirmasiPassword.Name = "txtKonfirmasiPassword";
            this.txtKonfirmasiPassword.PasswordChar = '●';
            this.txtKonfirmasiPassword.Size = new System.Drawing.Size(265, 29);
            this.txtKonfirmasiPassword.TabIndex = 9;

            // chkShowPassword
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(150, 100, 200);
            this.chkShowPassword.Location = new System.Drawing.Point(40, 315);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.TabIndex = 12;
            this.chkShowPassword.Text = "Lihatin Semua Passwordnya Dong 👁️";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);

            // btnDaftar
            this.btnDaftar.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            this.btnDaftar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDaftar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnDaftar.FlatAppearance.BorderSize = 2;
            this.btnDaftar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDaftar.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDaftar.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnDaftar.Location = new System.Drawing.Point(40, 345);
            this.btnDaftar.Name = "btnDaftar";
            this.btnDaftar.Size = new System.Drawing.Size(570, 45);
            this.btnDaftar.TabIndex = 10;
            this.btnDaftar.Text = "Let's Gooo! 🔥";
            this.btnDaftar.UseVisualStyleBackColor = false;
            this.btnDaftar.Click += new System.EventHandler(this.btnDaftar_Click);

            // btnBatal
            this.btnBatal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatal.FlatAppearance.BorderSize = 0;
            this.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatal.Font = new System.Drawing.Font("Segoe UI", 9.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatal.ForeColor = System.Drawing.Color.FromArgb(120, 80, 180);
            this.btnBatal.Location = new System.Drawing.Point(175, 400);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(300, 30);
            this.btnBatal.TabIndex = 11;
            this.btnBatal.Text = "Eh udah punya akun deng. Balik ah.";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);

            // RegisterControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.Controls.Add(this.pnlCard);
            this.Name = "RegisterControl";
            this.Size = new System.Drawing.Size(1020, 720);

            ((System.ComponentModel.ISupportInitialize)(this.picLogoCorner)).EndInit();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblNoTelepon;
        private System.Windows.Forms.TextBox txtNoTelepon;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblKonfirmasiPassword;
        private System.Windows.Forms.TextBox txtKonfirmasiPassword;
        private System.Windows.Forms.Button btnDaftar;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.PictureBox picLogoCorner;
    }
}
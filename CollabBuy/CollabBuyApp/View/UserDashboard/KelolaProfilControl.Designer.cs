namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class KelolaProfilControl
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
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblNoTelepon = new System.Windows.Forms.Label();
            this.txtNoTelepon = new System.Windows.Forms.TextBox();
            this.lblNamaToko = new System.Windows.Forms.Label();
            this.txtNamaToko = new System.Windows.Forms.TextBox();
            this.chkGantiPassword = new System.Windows.Forms.CheckBox();
            this.pnlGantiPassword = new System.Windows.Forms.Panel();
            this.lblPasswordLama = new System.Windows.Forms.Label();
            this.txtPasswordLama = new System.Windows.Forms.TextBox();
            this.lblPasswordBaru = new System.Windows.Forms.Label();
            this.txtPasswordBaru = new System.Windows.Forms.TextBox();
            this.lblKonfirmasiPassword = new System.Windows.Forms.Label();
            this.txtKonfirmasiPassword = new System.Windows.Forms.TextBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnSimpan = new System.Windows.Forms.Button();

            this.pnlCard.SuspendLayout();
            this.pnlGantiPassword.SuspendLayout();
            this.SuspendLayout();

            // pnlCard
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(235, 204, 255);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.lblNama);
            this.pnlCard.Controls.Add(this.txtNama);
            this.pnlCard.Controls.Add(this.lblUsername);
            this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.lblEmail);
            this.pnlCard.Controls.Add(this.txtEmail);
            this.pnlCard.Controls.Add(this.lblNoTelepon);
            this.pnlCard.Controls.Add(this.txtNoTelepon);
            this.pnlCard.Controls.Add(this.lblNamaToko);
            this.pnlCard.Controls.Add(this.txtNamaToko);
            this.pnlCard.Controls.Add(this.chkGantiPassword);
            this.pnlCard.Controls.Add(this.pnlGantiPassword);
            this.pnlCard.Controls.Add(this.btnSimpan);
            this.pnlCard.Location = new System.Drawing.Point(250, 40);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(500, 760);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "👤 Setup Profil";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(35, 57);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Pastiin data lo up-to-date biar gampang dikontak ya!";

            // lblNama
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNama.Location = new System.Drawing.Point(35, 85);
            this.lblNama.Name = "lblNama";
            this.lblNama.Text = "Nama Lengkap *";

            // txtNama
            this.txtNama.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtNama.Location = new System.Drawing.Point(39, 108);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(420, 29);

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblUsername.Location = new System.Drawing.Point(35, 150);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "Username *";

            // txtUsername
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.Location = new System.Drawing.Point(39, 173);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(420, 29);

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblEmail.Location = new System.Drawing.Point(35, 215);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "Email Aktif";

            // txtEmail
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtEmail.Location = new System.Drawing.Point(39, 238);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(420, 29);

            // lblNoTelepon
            this.lblNoTelepon.AutoSize = true;
            this.lblNoTelepon.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblNoTelepon.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNoTelepon.Location = new System.Drawing.Point(35, 280);
            this.lblNoTelepon.Name = "lblNoTelepon";
            this.lblNoTelepon.Text = "No WhatsApp";

            // txtNoTelepon
            this.txtNoTelepon.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtNoTelepon.Location = new System.Drawing.Point(39, 303);
            this.txtNoTelepon.Name = "txtNoTelepon";
            this.txtNoTelepon.Size = new System.Drawing.Size(420, 29);

            // lblNamaToko (Sekarang Posisinya di Atas Ganti Password)
            this.lblNamaToko.AutoSize = true;
            this.lblNamaToko.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamaToko.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNamaToko.Location = new System.Drawing.Point(35, 345);
            this.lblNamaToko.Name = "lblNamaToko";
            this.lblNamaToko.Text = "Nama Toko";
            this.lblNamaToko.Visible = false;

            // txtNamaToko (Sekarang Posisinya di Atas Ganti Password & Font Disamakan)
            this.txtNamaToko.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtNamaToko.Location = new System.Drawing.Point(39, 368);
            this.txtNamaToko.Name = "txtNamaToko";
            this.txtNamaToko.Size = new System.Drawing.Size(420, 29);
            this.txtNamaToko.Visible = false;

            // chkGantiPassword (Posisinya Turun)
            this.chkGantiPassword.AutoSize = true;
            this.chkGantiPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.chkGantiPassword.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.chkGantiPassword.Location = new System.Drawing.Point(39, 415);
            this.chkGantiPassword.Name = "chkGantiPassword";
            this.chkGantiPassword.Text = "🔐 Ganti Password";
            this.chkGantiPassword.UseVisualStyleBackColor = true;
            this.chkGantiPassword.CheckedChanged += new System.EventHandler(this.chkGantiPassword_CheckedChanged);

            // pnlGantiPassword (Posisinya Turun)
            this.pnlGantiPassword.BackColor = System.Drawing.Color.FromArgb(245, 220, 255);
            this.pnlGantiPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGantiPassword.Controls.Add(this.lblPasswordLama);
            this.pnlGantiPassword.Controls.Add(this.txtPasswordLama);
            this.pnlGantiPassword.Controls.Add(this.lblPasswordBaru);
            this.pnlGantiPassword.Controls.Add(this.txtPasswordBaru);
            this.pnlGantiPassword.Controls.Add(this.lblKonfirmasiPassword);
            this.pnlGantiPassword.Controls.Add(this.txtKonfirmasiPassword);
            this.pnlGantiPassword.Controls.Add(this.chkShowPassword);
            this.pnlGantiPassword.Location = new System.Drawing.Point(39, 445);
            this.pnlGantiPassword.Name = "pnlGantiPassword";
            this.pnlGantiPassword.Size = new System.Drawing.Size(420, 220);
            this.pnlGantiPassword.Visible = false;

            // lblPasswordLama
            this.lblPasswordLama.AutoSize = true;
            this.lblPasswordLama.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.lblPasswordLama.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblPasswordLama.Location = new System.Drawing.Point(10, 12);
            this.lblPasswordLama.Name = "lblPasswordLama";
            this.lblPasswordLama.Text = "Password Lama *";

            // txtPasswordLama
            this.txtPasswordLama.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPasswordLama.Location = new System.Drawing.Point(10, 33);
            this.txtPasswordLama.Name = "txtPasswordLama";
            this.txtPasswordLama.PasswordChar = '●';
            this.txtPasswordLama.Size = new System.Drawing.Size(395, 27);

            // lblPasswordBaru
            this.lblPasswordBaru.AutoSize = true;
            this.lblPasswordBaru.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.lblPasswordBaru.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblPasswordBaru.Location = new System.Drawing.Point(10, 72);
            this.lblPasswordBaru.Name = "lblPasswordBaru";
            this.lblPasswordBaru.Text = "Password Baru * (min. 8 karakter)";

            // txtPasswordBaru
            this.txtPasswordBaru.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPasswordBaru.Location = new System.Drawing.Point(10, 93);
            this.txtPasswordBaru.Name = "txtPasswordBaru";
            this.txtPasswordBaru.PasswordChar = '●';
            this.txtPasswordBaru.Size = new System.Drawing.Size(395, 27);

            // lblKonfirmasiPassword
            this.lblKonfirmasiPassword.AutoSize = true;
            this.lblKonfirmasiPassword.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.lblKonfirmasiPassword.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblKonfirmasiPassword.Location = new System.Drawing.Point(10, 132);
            this.lblKonfirmasiPassword.Name = "lblKonfirmasiPassword";
            this.lblKonfirmasiPassword.Text = "Konfirmasi Password Baru *";

            // txtKonfirmasiPassword
            this.txtKonfirmasiPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKonfirmasiPassword.Location = new System.Drawing.Point(10, 153);
            this.txtKonfirmasiPassword.Name = "txtKonfirmasiPassword";
            this.txtKonfirmasiPassword.PasswordChar = '●';
            this.txtKonfirmasiPassword.Size = new System.Drawing.Size(395, 27);

            // chkShowPassword
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.chkShowPassword.Location = new System.Drawing.Point(10, 192);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Text = "Lihat Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);

            // btnSimpan (Posisinya Turun Sedikit agar Aman)
            this.btnSimpan.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpan.FlatAppearance.BorderSize = 0;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnSimpan.Location = new System.Drawing.Point(39, 685);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(420, 48);
            this.btnSimpan.Text = "💾 Simpan Perubahan";
            this.btnSimpan.UseVisualStyleBackColor = false;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            // KelolaProfilControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlCard);
            this.Name = "KelolaProfilControl";
            this.Size = new System.Drawing.Size(1000, 760);
            this.Load += new System.EventHandler(this.KelolaProfilControl_Load);

            this.pnlGantiPassword.ResumeLayout(false);
            this.pnlGantiPassword.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblNoTelepon;
        private System.Windows.Forms.TextBox txtNoTelepon;
        private System.Windows.Forms.CheckBox chkGantiPassword;
        private System.Windows.Forms.Panel pnlGantiPassword;
        private System.Windows.Forms.Label lblPasswordLama;
        private System.Windows.Forms.TextBox txtPasswordLama;
        private System.Windows.Forms.Label lblPasswordBaru;
        private System.Windows.Forms.TextBox txtPasswordBaru;
        private System.Windows.Forms.Label lblKonfirmasiPassword;
        private System.Windows.Forms.TextBox txtKonfirmasiPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Label lblNamaToko;
        private System.Windows.Forms.TextBox txtNamaToko;
    }
}
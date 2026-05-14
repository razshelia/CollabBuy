namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class LoginControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlLeft         = new System.Windows.Forms.Panel();
            pnlRight        = new System.Windows.Forms.Panel();
            pnlCard         = new System.Windows.Forms.Panel();
            lblBrandTitle   = new System.Windows.Forms.Label();
            lblBrandSub     = new System.Windows.Forms.Label();
            lblBrandDesc    = new System.Windows.Forms.Label();
            lblJudul        = new System.Windows.Forms.Label();
            lblSubJudul     = new System.Windows.Forms.Label();
            lblUsername     = new System.Windows.Forms.Label();
            txtUsername     = new System.Windows.Forms.TextBox();
            lblPassword     = new System.Windows.Forms.Label();
            txtPassword     = new System.Windows.Forms.TextBox();
            chkShowPassword = new System.Windows.Forms.CheckBox();
            btnMasuk        = new System.Windows.Forms.Button();
            lblAtau         = new System.Windows.Forms.Label();
            btnDaftar       = new System.Windows.Forms.Button();

            SuspendLayout();

            // ROOT
            BackColor = System.Drawing.Color.White;
            Dock      = System.Windows.Forms.DockStyle.Fill;
            Name      = "LoginControl";

            // ── LEFT BRANDING (50%) ──────────────────────────────
            pnlLeft.Dock      = System.Windows.Forms.DockStyle.Left;
            pnlLeft.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            pnlLeft.Width     = 500;
            pnlLeft.Name      = "pnlLeft";

            lblBrandTitle.Text      = "COLLAB\nBUY";
            lblBrandTitle.Font      = new System.Drawing.Font("Segoe UI Black", 56F, System.Drawing.FontStyle.Bold);
            lblBrandTitle.ForeColor = System.Drawing.Color.White;
            lblBrandTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBrandTitle.AutoSize  = false;
            lblBrandTitle.Size      = new System.Drawing.Size(400, 210);
            lblBrandTitle.Location  = new System.Drawing.Point(50, 170);
            lblBrandTitle.Name      = "lblBrandTitle";

            lblBrandSub.Text      = "✨ Solusi Gotong Royong Mahasiswa";
            lblBrandSub.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(255, 235, 133);
            lblBrandSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBrandSub.AutoSize  = false;
            lblBrandSub.Size      = new System.Drawing.Size(400, 36);
            lblBrandSub.Location  = new System.Drawing.Point(50, 390);
            lblBrandSub.Name      = "lblBrandSub";

            lblBrandDesc.Text      = "Beli bareng, hemat bareng, danus sukses!\nPlatform PO Kolektif untuk semua mahasiswa.";
            lblBrandDesc.Font      = new System.Drawing.Font("Segoe UI", 11F);
            lblBrandDesc.ForeColor = System.Drawing.Color.FromArgb(240, 240, 255);
            lblBrandDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBrandDesc.AutoSize  = false;
            lblBrandDesc.Size      = new System.Drawing.Size(400, 70);
            lblBrandDesc.Location  = new System.Drawing.Point(50, 445);
            lblBrandDesc.Name      = "lblBrandDesc";

            pnlLeft.Controls.Add(lblBrandTitle);
            pnlLeft.Controls.Add(lblBrandSub);
            pnlLeft.Controls.Add(lblBrandDesc);

            // ── RIGHT FORM AREA ──────────────────────────────────
            pnlRight.Dock      = System.Windows.Forms.DockStyle.Fill;
            pnlRight.BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            pnlRight.Name      = "pnlRight";

            // Card
            pnlCard.BackColor   = System.Drawing.Color.White;
            pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlCard.Size        = new System.Drawing.Size(440, 570);
            pnlCard.Name        = "pnlCard";

            lblJudul.Text      = "Hola Bestie! 👋";
            lblJudul.Font      = new System.Drawing.Font("Segoe UI Black", 26F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            lblJudul.AutoSize  = false;
            lblJudul.Size      = new System.Drawing.Size(380, 58);
            lblJudul.Location  = new System.Drawing.Point(30, 35);
            lblJudul.Name      = "lblJudul";

            lblSubJudul.Text      = "Masuk dulu yuk buat ikut Gotong Royong 🛒";
            lblSubJudul.Font      = new System.Drawing.Font("Segoe UI", 10F);
            lblSubJudul.ForeColor = System.Drawing.Color.Gray;
            lblSubJudul.AutoSize  = false;
            lblSubJudul.Size      = new System.Drawing.Size(380, 24);
            lblSubJudul.Location  = new System.Drawing.Point(30, 98);
            lblSubJudul.Name      = "lblSubJudul";

            lblUsername.Text      = "Username";
            lblUsername.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblUsername.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
            lblUsername.AutoSize  = true;
            lblUsername.Location  = new System.Drawing.Point(30, 148);
            lblUsername.Name      = "lblUsername";

            txtUsername.Font             = new System.Drawing.Font("Segoe UI", 12F);
            txtUsername.BorderStyle      = System.Windows.Forms.BorderStyle.FixedSingle;
            txtUsername.BackColor        = System.Drawing.Color.FromArgb(248, 248, 252);
            txtUsername.Size             = new System.Drawing.Size(380, 34);
            txtUsername.Location         = new System.Drawing.Point(30, 173);
            txtUsername.Name             = "txtUsername";
            txtUsername.PlaceholderText  = "Masukkan username kamu...";

            lblPassword.Text      = "Password";
            lblPassword.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblPassword.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
            lblPassword.AutoSize  = true;
            lblPassword.Location  = new System.Drawing.Point(30, 232);
            lblPassword.Name      = "lblPassword";

            txtPassword.Font             = new System.Drawing.Font("Segoe UI", 12F);
            txtPassword.BorderStyle      = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPassword.BackColor        = System.Drawing.Color.FromArgb(248, 248, 252);
            txtPassword.PasswordChar     = '●';
            txtPassword.Size             = new System.Drawing.Size(380, 34);
            txtPassword.Location         = new System.Drawing.Point(30, 257);
            txtPassword.Name             = "txtPassword";
            txtPassword.PlaceholderText  = "Masukkan password rahasia...";

            chkShowPassword.Text      = "👁 Tampilkan password";
            chkShowPassword.Font      = new System.Drawing.Font("Segoe UI", 9F);
            chkShowPassword.ForeColor = System.Drawing.Color.Gray;
            chkShowPassword.AutoSize  = true;
            chkShowPassword.Location  = new System.Drawing.Point(30, 302);
            chkShowPassword.Name      = "chkShowPassword";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;

            btnMasuk.Text                              = "GAS MASUK! 🚀";
            btnMasuk.Font                              = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            btnMasuk.BackColor                         = System.Drawing.Color.FromArgb(170, 150, 218);
            btnMasuk.ForeColor                         = System.Drawing.Color.White;
            btnMasuk.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            btnMasuk.FlatAppearance.BorderSize         = 0;
            btnMasuk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(145, 125, 195);
            btnMasuk.Size                              = new System.Drawing.Size(380, 52);
            btnMasuk.Location                          = new System.Drawing.Point(30, 347);
            btnMasuk.Cursor                            = System.Windows.Forms.Cursors.Hand;
            btnMasuk.Name                              = "btnMasuk";
            btnMasuk.Click                            += btnMasuk_Click;

            lblAtau.Text      = "─────────────  atau  ─────────────";
            lblAtau.Font      = new System.Drawing.Font("Segoe UI", 9F);
            lblAtau.ForeColor = System.Drawing.Color.Silver;
            lblAtau.AutoSize  = false;
            lblAtau.Size      = new System.Drawing.Size(380, 22);
            lblAtau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblAtau.Location  = new System.Drawing.Point(30, 413);
            lblAtau.Name      = "lblAtau";

            btnDaftar.Text                             = "Belum punya akun? Skuy Daftar! ✨";
            btnDaftar.Font                             = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnDaftar.BackColor                        = System.Drawing.Color.White;
            btnDaftar.ForeColor                        = System.Drawing.Color.FromArgb(170, 150, 218);
            btnDaftar.FlatStyle                        = System.Windows.Forms.FlatStyle.Flat;
            btnDaftar.FlatAppearance.BorderColor       = System.Drawing.Color.FromArgb(170, 150, 218);
            btnDaftar.FlatAppearance.BorderSize        = 2;
            btnDaftar.Size                             = new System.Drawing.Size(380, 52);
            btnDaftar.Location                         = new System.Drawing.Point(30, 447);
            btnDaftar.Cursor                           = System.Windows.Forms.Cursors.Hand;
            btnDaftar.Name                             = "btnDaftar";
            btnDaftar.Click                           += btnDaftar_Click;

            pnlCard.Controls.Add(lblJudul);
            pnlCard.Controls.Add(lblSubJudul);
            pnlCard.Controls.Add(lblUsername);
            pnlCard.Controls.Add(txtUsername);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(chkShowPassword);
            pnlCard.Controls.Add(btnMasuk);
            pnlCard.Controls.Add(lblAtau);
            pnlCard.Controls.Add(btnDaftar);

            pnlRight.Controls.Add(pnlCard);

            // Centre card on resize
            pnlRight.Resize += (s, e) => {
                pnlCard.Left = (pnlRight.Width  - pnlCard.Width)  / 2;
                pnlCard.Top  = (pnlRight.Height - pnlCard.Height) / 2;
            };
            this.Resize += (s, e) => {
                pnlLeft.Width = this.Width / 2;
                pnlCard.Left  = (pnlRight.Width  - pnlCard.Width)  / 2;
                pnlCard.Top   = (pnlRight.Height - pnlCard.Height) / 2;
            };

            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel   pnlLeft, pnlRight, pnlCard;
        private System.Windows.Forms.Label   lblBrandTitle, lblBrandSub, lblBrandDesc;
        private System.Windows.Forms.Label   lblJudul, lblSubJudul;
        private System.Windows.Forms.Label   lblUsername, lblPassword, lblAtau;
        private System.Windows.Forms.TextBox txtUsername, txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button  btnMasuk, btnDaftar;
    }
}

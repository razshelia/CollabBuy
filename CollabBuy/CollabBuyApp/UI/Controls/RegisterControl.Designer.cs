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
            pnlLeft         = new System.Windows.Forms.Panel();
            pnlRight        = new System.Windows.Forms.Panel();
            pnlScroll       = new System.Windows.Forms.Panel();
            pnlCard         = new System.Windows.Forms.Panel();
            lblBrandTitle   = new System.Windows.Forms.Label();
            lblBrandSub     = new System.Windows.Forms.Label();
            lblJudul        = new System.Windows.Forms.Label();
            lblSubJudul     = new System.Windows.Forms.Label();
            lblNama         = new System.Windows.Forms.Label();
            txtNama         = new System.Windows.Forms.TextBox();
            lblTglLahir     = new System.Windows.Forms.Label();
            dtpTglLahir     = new System.Windows.Forms.DateTimePicker();
            lblUsername     = new System.Windows.Forms.Label();
            txtUsername     = new System.Windows.Forms.TextBox();
            lblTelp         = new System.Windows.Forms.Label();
            txtTelp         = new System.Windows.Forms.MaskedTextBox();
            lblEmail        = new System.Windows.Forms.Label();
            txtEmail        = new System.Windows.Forms.TextBox();
            lblPassword     = new System.Windows.Forms.Label();
            txtPassword     = new System.Windows.Forms.TextBox();
            chkShowPassword = new System.Windows.Forms.CheckBox();
            btnDaftarBaru   = new System.Windows.Forms.Button();
            btnKembali      = new System.Windows.Forms.Button();

            SuspendLayout();

            BackColor = System.Drawing.Color.White;
            Dock      = System.Windows.Forms.DockStyle.Fill;
            Name      = "RegisterControl";

            // ── LEFT BRANDING ─────────────────────────────────────
            pnlLeft.Dock      = System.Windows.Forms.DockStyle.Left;
            pnlLeft.BackColor = System.Drawing.Color.FromArgb(255, 235, 133);
            pnlLeft.Width     = 500;
            pnlLeft.Name      = "pnlLeft";

            lblBrandTitle.Text      = "COLLAB\nBUY";
            lblBrandTitle.Font      = new System.Drawing.Font("Segoe UI Black", 56F, System.Drawing.FontStyle.Bold);
            lblBrandTitle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            lblBrandTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBrandTitle.AutoSize  = false;
            lblBrandTitle.Size      = new System.Drawing.Size(400, 210);
            lblBrandTitle.Location  = new System.Drawing.Point(50, 170);
            lblBrandTitle.Name      = "lblBrandTitle";

            lblBrandSub.Text      = "🏫 OTW BIKIN AKUN!\nGabung & mulai Danus bareng 🚀";
            lblBrandSub.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(80, 60, 140);
            lblBrandSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBrandSub.AutoSize  = false;
            lblBrandSub.Size      = new System.Drawing.Size(400, 70);
            lblBrandSub.Location  = new System.Drawing.Point(50, 400);
            lblBrandSub.Name      = "lblBrandSub";

            pnlLeft.Controls.Add(lblBrandTitle);
            pnlLeft.Controls.Add(lblBrandSub);

            // ── RIGHT FORM (Scrollable) ───────────────────────────
            pnlRight.Dock      = System.Windows.Forms.DockStyle.Fill;
            pnlRight.BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            pnlRight.Name      = "pnlRight";

            pnlScroll.Dock         = System.Windows.Forms.DockStyle.Fill;
            pnlScroll.AutoScroll   = true;
            pnlScroll.BackColor    = System.Drawing.Color.FromArgb(247, 247, 252);
            pnlScroll.Name         = "pnlScroll";

            // Card (tall - will be centred horizontally, scroll vertically)
            pnlCard.BackColor   = System.Drawing.Color.White;
            pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlCard.Size        = new System.Drawing.Size(460, 720);
            pnlCard.Location    = new System.Drawing.Point(30, 20);
            pnlCard.Name        = "pnlCard";

            // ── Card contents ─────────────────────────────────────
            lblJudul.Text      = "OTW Bikin Akun 🚀";
            lblJudul.Font      = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            lblJudul.AutoSize  = false;
            lblJudul.Size      = new System.Drawing.Size(400, 52);
            lblJudul.Location  = new System.Drawing.Point(30, 28);
            lblJudul.Name      = "lblJudul";

            lblSubJudul.Text      = "Isi semua data di bawah ini ya bestie ✨";
            lblSubJudul.Font      = new System.Drawing.Font("Segoe UI", 10F);
            lblSubJudul.ForeColor = System.Drawing.Color.Gray;
            lblSubJudul.AutoSize  = false;
            lblSubJudul.Size      = new System.Drawing.Size(400, 22);
            lblSubJudul.Location  = new System.Drawing.Point(30, 84);
            lblSubJudul.Name      = "lblSubJudul";

            // Helper local method to position label+input pairs
            int y = 125;
            int inputW = 396;

            void AddField(System.Windows.Forms.Label lbl, string labelText, System.Windows.Forms.Control input)
            {
                lbl.Text      = labelText;
                lbl.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                lbl.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
                lbl.AutoSize  = true;
                lbl.Location  = new System.Drawing.Point(32, y);
                y += 26;
                input.Location  = new System.Drawing.Point(32, y);
                input.Width     = inputW;
                input.Font      = new System.Drawing.Font("Segoe UI", 11F);
                if (input is System.Windows.Forms.TextBox tb)   tb.BorderStyle  = System.Windows.Forms.BorderStyle.FixedSingle;
                if (input is System.Windows.Forms.MaskedTextBox mb) mb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                y += 40;
                pnlCard.Controls.Add(lbl);
                pnlCard.Controls.Add(input);
            }

            pnlCard.Controls.Add(lblJudul);
            pnlCard.Controls.Add(lblSubJudul);

            AddField(lblNama,     "Nama Lengkap Kamu:",              txtNama);
            txtNama.PlaceholderText = "Nama kamu...";

            // Tanggal Lahir
            lblTglLahir.Text      = "Tanggal Brojol:";
            lblTglLahir.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTglLahir.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
            lblTglLahir.AutoSize  = true;
            lblTglLahir.Location  = new System.Drawing.Point(32, y);
            y += 26;
            dtpTglLahir.Location  = new System.Drawing.Point(32, y);
            dtpTglLahir.Width     = inputW;
            dtpTglLahir.Font      = new System.Drawing.Font("Segoe UI", 11F);
            dtpTglLahir.Format    = System.Windows.Forms.DateTimePickerFormat.Short;
            y += 40;
            pnlCard.Controls.Add(lblTglLahir);
            pnlCard.Controls.Add(dtpTglLahir);

            AddField(lblUsername, "Username Keren:",                 txtUsername);
            txtUsername.PlaceholderText = "Pilih username unik...";

            AddField(lblTelp,     "No. WhatsApp (Biar gampang di-chat):", txtTelp);
            txtTelp.Mask         = "00000000000000";
            txtTelp.PromptChar   = ' ';

            AddField(lblEmail,    "Email Kampus / Pribadi:",         txtEmail);
            txtEmail.PlaceholderText = "email@kampus.ac.id";

            AddField(lblPassword, "Password Rahasia:",               txtPassword);
            txtPassword.PasswordChar    = '●';
            txtPassword.PlaceholderText = "Min. 6 karakter...";

            // Checkbox show password
            chkShowPassword.Text      = "👁 Tampilkan Password";
            chkShowPassword.Font      = new System.Drawing.Font("Segoe UI", 9F);
            chkShowPassword.ForeColor = System.Drawing.Color.Gray;
            chkShowPassword.AutoSize  = true;
            chkShowPassword.Location  = new System.Drawing.Point(32, y);
            chkShowPassword.Name      = "chkShowPassword";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            y += 36;

            // Tombol DONE
            btnDaftarBaru.Text                              = "DONE! BIKIN AKUN ✨";
            btnDaftarBaru.Font                              = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            btnDaftarBaru.BackColor                         = System.Drawing.Color.FromArgb(255, 235, 133);
            btnDaftarBaru.ForeColor                         = System.Drawing.Color.FromArgb(40, 40, 60);
            btnDaftarBaru.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            btnDaftarBaru.FlatAppearance.BorderSize         = 0;
            btnDaftarBaru.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(240, 210, 80);
            btnDaftarBaru.Size                              = new System.Drawing.Size(396, 52);
            btnDaftarBaru.Location                          = new System.Drawing.Point(32, y);
            btnDaftarBaru.Cursor                            = System.Windows.Forms.Cursors.Hand;
            btnDaftarBaru.Name                              = "btnDaftarBaru";
            btnDaftarBaru.Click                            += btnDaftarBaru_Click;
            y += 62;

            btnKembali.Text                             = "Eh gajadi, balik ke Login aja 👈";
            btnKembali.Font                             = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnKembali.BackColor                        = System.Drawing.Color.White;
            btnKembali.ForeColor                        = System.Drawing.Color.FromArgb(170, 150, 218);
            btnKembali.FlatStyle                        = System.Windows.Forms.FlatStyle.Flat;
            btnKembali.FlatAppearance.BorderColor       = System.Drawing.Color.FromArgb(170, 150, 218);
            btnKembali.FlatAppearance.BorderSize        = 2;
            btnKembali.Size                             = new System.Drawing.Size(396, 48);
            btnKembali.Location                         = new System.Drawing.Point(32, y);
            btnKembali.Cursor                           = System.Windows.Forms.Cursors.Hand;
            btnKembali.Name                             = "btnKembali";
            btnKembali.Click                           += btnKembali_Click;
            y += 68;

            // Make card tall enough
            pnlCard.Height = y + 20;
            pnlCard.Controls.Add(chkShowPassword);
            pnlCard.Controls.Add(btnDaftarBaru);
            pnlCard.Controls.Add(btnKembali);

            pnlScroll.Controls.Add(pnlCard);
            pnlRight.Controls.Add(pnlScroll);

            // Center card horizontally on resize
            pnlScroll.Resize += (s, e) => {
                pnlCard.Left = (pnlScroll.Width - pnlCard.Width) / 2;
                if (pnlCard.Left < 10) pnlCard.Left = 10;
            };
            this.Resize += (s, e) => {
                pnlLeft.Width = this.Width / 2;
                pnlCard.Left  = (pnlScroll.Width - pnlCard.Width) / 2;
                if (pnlCard.Left < 10) pnlCard.Left = 10;
            };

            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel         pnlLeft, pnlRight, pnlScroll, pnlCard;
        private System.Windows.Forms.Label         lblBrandTitle, lblBrandSub;
        private System.Windows.Forms.Label         lblJudul, lblSubJudul;
        private System.Windows.Forms.Label         lblNama, lblTglLahir, lblUsername, lblTelp, lblEmail, lblPassword;
        private System.Windows.Forms.TextBox       txtNama, txtUsername, txtEmail, txtPassword;
        private System.Windows.Forms.MaskedTextBox txtTelp;
        private System.Windows.Forms.DateTimePicker dtpTglLahir;
        private System.Windows.Forms.CheckBox      chkShowPassword;
        private System.Windows.Forms.Button        btnDaftarBaru, btnKembali;
    }
}

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class EditProfileControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlOuter        = new System.Windows.Forms.Panel();
            pnlCard         = new System.Windows.Forms.Panel();
            lblJudul        = new System.Windows.Forms.Label();
            lblSubJudul     = new System.Windows.Forms.Label();
            lblNama         = new System.Windows.Forms.Label();
            txtNama         = new System.Windows.Forms.TextBox();
            lblTelp         = new System.Windows.Forms.Label();
            txtTelp         = new System.Windows.Forms.MaskedTextBox();
            lblPasswordLama = new System.Windows.Forms.Label();
            txtPasswordLama = new System.Windows.Forms.TextBox();
            lblPasswordBaru = new System.Windows.Forms.Label();
            txtPasswordBaru = new System.Windows.Forms.TextBox();
            chkLihatPassword = new System.Windows.Forms.CheckBox();
            btnSimpan       = new System.Windows.Forms.Button();

            SuspendLayout();

            BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            Dock      = System.Windows.Forms.DockStyle.Fill;
            Name      = "EditProfileControl";

            // Outer wrapper (grey background = fills the page)
            pnlOuter.Dock      = System.Windows.Forms.DockStyle.Fill;
            pnlOuter.BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            pnlOuter.Name      = "pnlOuter";

            // Card (white, centred)
            pnlCard.BackColor   = System.Drawing.Color.White;
            pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlCard.Size        = new System.Drawing.Size(480, 560);
            pnlCard.Name        = "pnlCard";

            // Page title
            lblJudul.Text      = "UPDATE PROFIL KAMU ✨";
            lblJudul.Font      = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            lblJudul.AutoSize  = false;
            lblJudul.Size      = new System.Drawing.Size(420, 50);
            lblJudul.Location  = new System.Drawing.Point(30, 28);
            lblJudul.Name      = "lblJudul";

            lblSubJudul.Text      = "Ubah data profilmu di sini ya bestie 🔐";
            lblSubJudul.Font      = new System.Drawing.Font("Segoe UI", 10F);
            lblSubJudul.ForeColor = System.Drawing.Color.Gray;
            lblSubJudul.AutoSize  = false;
            lblSubJudul.Size      = new System.Drawing.Size(420, 22);
            lblSubJudul.Location  = new System.Drawing.Point(30, 82);
            lblSubJudul.Name      = "lblSubJudul";

            // Field helper
            int y     = 126;
            int inputW = 416;

            void AddField(System.Windows.Forms.Label lbl, string labelText, System.Windows.Forms.Control input)
            {
                lbl.Text      = labelText;
                lbl.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                lbl.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
                lbl.AutoSize  = true;
                lbl.Location  = new System.Drawing.Point(32, y);
                y += 26;
                input.Location = new System.Drawing.Point(32, y);
                input.Width    = inputW;
                input.Font     = new System.Drawing.Font("Segoe UI", 11F);
                if (input is System.Windows.Forms.TextBox tb)      tb.BorderStyle   = System.Windows.Forms.BorderStyle.FixedSingle;
                if (input is System.Windows.Forms.MaskedTextBox mb) mb.BorderStyle  = System.Windows.Forms.BorderStyle.FixedSingle;
                y += 40;
                pnlCard.Controls.Add(lbl);
                pnlCard.Controls.Add(input);
            }

            pnlCard.Controls.Add(lblJudul);
            pnlCard.Controls.Add(lblSubJudul);

            AddField(lblNama,         "Nama Lengkap Baru:",                   txtNama);
            txtNama.PlaceholderText  = "Nama lengkapmu...";

            AddField(lblTelp,         "No. WhatsApp Baru:",                   txtTelp);
            txtTelp.Mask            = "00000000000000";
            txtTelp.PromptChar      = ' ';

            AddField(lblPasswordLama, "Password Lama (buat verifikasi):",     txtPasswordLama);
            txtPasswordLama.PasswordChar    = '●';
            txtPasswordLama.PlaceholderText = "Password saat ini...";

            AddField(lblPasswordBaru, "Password Baru (kosongin kalau ga ganti):", txtPasswordBaru);
            txtPasswordBaru.PasswordChar    = '●';
            txtPasswordBaru.PlaceholderText = "Password baru...";

            chkLihatPassword.Text      = "👁 Tampilkan password";
            chkLihatPassword.Font      = new System.Drawing.Font("Segoe UI", 9F);
            chkLihatPassword.ForeColor = System.Drawing.Color.Gray;
            chkLihatPassword.AutoSize  = true;
            chkLihatPassword.Location  = new System.Drawing.Point(32, y);
            chkLihatPassword.Name      = "chkLihatPassword";
            chkLihatPassword.CheckedChanged += chkLihatPassword_CheckedChanged;
            y += 36;

            btnSimpan.Text                              = "SAVE PERUBAHAN 🚀";
            btnSimpan.Font                              = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            btnSimpan.BackColor                         = System.Drawing.Color.FromArgb(170, 150, 218);
            btnSimpan.ForeColor                         = System.Drawing.Color.White;
            btnSimpan.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            btnSimpan.FlatAppearance.BorderSize         = 0;
            btnSimpan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(145, 125, 195);
            btnSimpan.Size                              = new System.Drawing.Size(416, 52);
            btnSimpan.Location                          = new System.Drawing.Point(32, y);
            btnSimpan.Cursor                            = System.Windows.Forms.Cursors.Hand;
            btnSimpan.Name                              = "btnSimpan";
            btnSimpan.Click                            += btnSimpan_Click;
            y += 62;

            pnlCard.Height = y + 10;
            pnlCard.Controls.Add(chkLihatPassword);
            pnlCard.Controls.Add(btnSimpan);

            pnlOuter.Controls.Add(pnlCard);

            // Centre card
            pnlOuter.Resize += (s, e) => {
                pnlCard.Left = (pnlOuter.Width  - pnlCard.Width)  / 2;
                pnlCard.Top  = (pnlOuter.Height - pnlCard.Height) / 2;
                if (pnlCard.Top < 20) pnlCard.Top = 20;
            };
            this.Resize += (s, e) => {
                pnlCard.Left = (pnlOuter.Width  - pnlCard.Width)  / 2;
                pnlCard.Top  = (pnlOuter.Height - pnlCard.Height) / 2;
                if (pnlCard.Top < 20) pnlCard.Top = 20;
            };

            Controls.Add(pnlOuter);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel        pnlOuter, pnlCard;
        private System.Windows.Forms.Label        lblJudul, lblSubJudul;
        private System.Windows.Forms.Label        lblNama, lblTelp, lblPasswordLama, lblPasswordBaru;
        private System.Windows.Forms.TextBox      txtNama, txtPasswordLama, txtPasswordBaru;
        private System.Windows.Forms.MaskedTextBox txtTelp;
        private System.Windows.Forms.CheckBox     chkLihatPassword;
        private System.Windows.Forms.Button       btnSimpan;
    }
}

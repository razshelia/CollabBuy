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
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblTelepon = new System.Windows.Forms.Label();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPasswordBaru = new System.Windows.Forms.Label();
            this.txtPasswordBaru = new System.Windows.Forms.TextBox();
            this.lblKonfirmasiPassword = new System.Windows.Forms.Label();
            this.txtKonfirmasiPassword = new System.Windows.Forms.TextBox();
            this.chkLihatPassword = new System.Windows.Forms.CheckBox();
            this.btnSimpan = new System.Windows.Forms.Button();

            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(255, 249, 230);

            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            this.pnlCard.Size = new System.Drawing.Size(500, 550);
            this.pnlCard.Location = new System.Drawing.Point(
                (this.ClientSize.Width - 500) / 2, (this.ClientSize.Height - 550) / 2);
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;

            // Title
            this.lblTitle.Text = "EDIT PROFIL 👤";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(253, 224, 71);
            this.lblTitle.Size = new System.Drawing.Size(430, 40);
            this.lblTitle.Location = new System.Drawing.Point(35, 25);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Label & TextBox
            int yPos = 80;
            AddField("Nama:", out this.lblNama, out this.txtNama, ref yPos);
            AddField("Telepon:", out this.lblTelepon, out this.txtTelepon, ref yPos);
            AddField("Email:", out this.lblEmail, out this.txtEmail, ref yPos);
            AddField("Username:", out this.lblUsername, out this.txtUsername, ref yPos);
            this.txtUsername.Enabled = false;
            AddField("Password Baru:", out this.lblPasswordBaru, out this.txtPasswordBaru, ref yPos);
            this.txtPasswordBaru.UseSystemPasswordChar = true;
            AddField("Konfirmasi Password:", out this.lblKonfirmasiPassword, out this.txtKonfirmasiPassword, ref yPos);
            this.txtKonfirmasiPassword.UseSystemPasswordChar = true;

            // Lihat Password
            this.chkLihatPassword.Text = "Lihat Password 👀";
            this.chkLihatPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkLihatPassword.ForeColor = System.Drawing.Color.FromArgb(167, 139, 250);
            this.chkLihatPassword.Location = new System.Drawing.Point(40, yPos);
            this.chkLihatPassword.Size = new System.Drawing.Size(200, 20);
            this.chkLihatPassword.CheckedChanged += new System.EventHandler(this.chkLihatPassword_CheckedChanged);
            yPos += 30;

            // Tombol Simpan
            this.btnSimpan.Text = "SIMPAN PERUBAHAN 💾";
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            this.btnSimpan.ForeColor = System.Drawing.Color.White;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Size = new System.Drawing.Size(420, 45);
            this.btnSimpan.Location = new System.Drawing.Point(40, yPos + 10);
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblNama); this.pnlCard.Controls.Add(this.txtNama);
            this.pnlCard.Controls.Add(this.lblTelepon); this.pnlCard.Controls.Add(this.txtTelepon);
            this.pnlCard.Controls.Add(this.lblEmail); this.pnlCard.Controls.Add(this.txtEmail);
            this.pnlCard.Controls.Add(this.lblUsername); this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.lblPasswordBaru); this.pnlCard.Controls.Add(this.txtPasswordBaru);
            this.pnlCard.Controls.Add(this.lblKonfirmasiPassword); this.pnlCard.Controls.Add(this.txtKonfirmasiPassword);
            this.pnlCard.Controls.Add(this.chkLihatPassword);
            this.pnlCard.Controls.Add(this.btnSimpan);

            this.Controls.Add(this.pnlCard);
            this.pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void AddField(string labelText, out System.Windows.Forms.Label lbl, out System.Windows.Forms.TextBox txt, ref int y)
        {
            lbl = new System.Windows.Forms.Label();
            lbl.Text = labelText;
            lbl.ForeColor = System.Drawing.Color.White;
            lbl.Font = new System.Drawing.Font("Segoe UI", 10F);
            lbl.Size = new System.Drawing.Size(420, 20);
            lbl.Location = new System.Drawing.Point(40, y);
            y += 22;

            txt = new System.Windows.Forms.TextBox();
            txt.Font = new System.Drawing.Font("Segoe UI", 11F);
            txt.Size = new System.Drawing.Size(420, 27);
            txt.Location = new System.Drawing.Point(40, y);
            y += 35;
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblNama, lblTelepon, lblEmail, lblUsername, lblPasswordBaru, lblKonfirmasiPassword;
        private System.Windows.Forms.TextBox txtNama, txtTelepon, txtEmail, txtUsername, txtPasswordBaru, txtKonfirmasiPassword;
        private System.Windows.Forms.CheckBox chkLihatPassword;
        private System.Windows.Forms.Button btnSimpan;
    }
}
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
            pnlCard = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            chkLihatPassword = new CheckBox();
            btnLogin = new Button();
            lblRegisterLink = new Label();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.FromArgb(45, 27, 79);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(txtUsername);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(chkLihatPassword);
            pnlCard.Controls.Add(btnLogin);
            pnlCard.Controls.Add(lblRegisterLink);
            pnlCard.Location = new Point(0, 0);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(440, 520);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(253, 224, 71);
            lblTitle.Location = new Point(40, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(360, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "COLLABBUY ✨";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = Color.FromArgb(167, 139, 250);
            lblSubtitle.Location = new Point(40, 95);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(360, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Login dulu, bestie! 🚀";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(55, 150);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username kamu...";
            txtUsername.Size = new Size(330, 27);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(55, 200);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password rahasia \U0001f92b";
            txtPassword.Size = new Size(330, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkLihatPassword
            // 
            chkLihatPassword.Font = new Font("Segoe UI", 9F);
            chkLihatPassword.ForeColor = Color.FromArgb(167, 139, 250);
            chkLihatPassword.Location = new Point(55, 240);
            chkLihatPassword.Name = "chkLihatPassword";
            chkLihatPassword.Size = new Size(200, 20);
            chkLihatPassword.TabIndex = 4;
            chkLihatPassword.Text = "Lihat Password 👀";
            chkLihatPassword.CheckedChanged += chkLihatPassword_CheckedChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(167, 139, 250);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(55, 290);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(330, 45);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "MASUK SEKARANG 💜";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblRegisterLink
            // 
            lblRegisterLink.Cursor = Cursors.Hand;
            lblRegisterLink.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            lblRegisterLink.ForeColor = Color.FromArgb(253, 224, 71);
            lblRegisterLink.Location = new Point(55, 350);
            lblRegisterLink.Name = "lblRegisterLink";
            lblRegisterLink.Size = new Size(330, 25);
            lblRegisterLink.TabIndex = 6;
            lblRegisterLink.Text = "Belum punya akun? Gabung sini, gengs! 🌟";
            lblRegisterLink.TextAlign = ContentAlignment.MiddleCenter;
            lblRegisterLink.Click += lblRegisterLink_Click;
            // 
            // LoginControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(pnlCard);
            Name = "LoginControl";
            Size = new Size(1046, 333);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblRegisterLink;
        private System.Windows.Forms.TextBox txtUsername, txtPassword;
        private System.Windows.Forms.CheckBox chkLihatPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}
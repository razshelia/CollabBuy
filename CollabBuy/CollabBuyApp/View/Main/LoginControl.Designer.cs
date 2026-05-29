namespace CollabBuy.CollabBuyApp.View.Main
{
    partial class LoginControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnDaftar = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(100, 120);
            this.lblUsername.Text = "Username";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(100, 140);
            this.txtUsername.Size = new System.Drawing.Size(250, 23);
            this.txtUsername.Name = "txtUsername";

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(100, 175);
            this.lblPassword.Text = "Password";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(100, 195);
            this.txtPassword.Size = new System.Drawing.Size(250, 23);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Name = "txtPassword";

            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(100, 240);
            this.btnLogin.Size = new System.Drawing.Size(115, 35);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // btnDaftar
            this.btnDaftar.Location = new System.Drawing.Point(235, 240);
            this.btnDaftar.Size = new System.Drawing.Size(115, 35);
            this.btnDaftar.Text = "Daftar";
            this.btnDaftar.Click += new System.EventHandler(this.btnDaftar_Click);

            // LoginControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnDaftar);
            this.Name = "LoginControl";
            this.Size = new System.Drawing.Size(450, 350);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnDaftar;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
    }
}
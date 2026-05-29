namespace CollabBuy.CollabBuyApp.View.Main
{
    partial class RegisterControl
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
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtKonfirmasiPassword = new System.Windows.Forms.TextBox();
            this.btnDaftar = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.lblNama = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblKonfirmasiPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblNama
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(100, 80);
            this.lblNama.Text = "Nama Lengkap";

            // txtNama
            this.txtNama.Location = new System.Drawing.Point(100, 100);
            this.txtNama.Size = new System.Drawing.Size(250, 23);
            this.txtNama.Name = "txtNama";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(100, 135);
            this.lblUsername.Text = "Username";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(100, 155);
            this.txtUsername.Size = new System.Drawing.Size(250, 23);
            this.txtUsername.Name = "txtUsername";

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(100, 190);
            this.lblPassword.Text = "Password";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(100, 210);
            this.txtPassword.Size = new System.Drawing.Size(250, 23);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Name = "txtPassword";

            // lblKonfirmasiPassword
            this.lblKonfirmasiPassword.AutoSize = true;
            this.lblKonfirmasiPassword.Location = new System.Drawing.Point(100, 245);
            this.lblKonfirmasiPassword.Text = "Konfirmasi Password";

            // txtKonfirmasiPassword
            this.txtKonfirmasiPassword.Location = new System.Drawing.Point(100, 265);
            this.txtKonfirmasiPassword.Size = new System.Drawing.Size(250, 23);
            this.txtKonfirmasiPassword.PasswordChar = '*';
            this.txtKonfirmasiPassword.Name = "txtKonfirmasiPassword";

            // btnDaftar
            this.btnDaftar.Location = new System.Drawing.Point(100, 310);
            this.btnDaftar.Size = new System.Drawing.Size(115, 35);
            this.btnDaftar.Text = "Daftar";
            this.btnDaftar.Click += new System.EventHandler(this.btnDaftar_Click);

            // btnBatal
            this.btnBatal.Location = new System.Drawing.Point(235, 310);
            this.btnBatal.Size = new System.Drawing.Size(115, 35);
            this.btnBatal.Text = "Batal";
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);

            // RegisterControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblKonfirmasiPassword);
            this.Controls.Add(this.txtKonfirmasiPassword);
            this.Controls.Add(this.btnDaftar);
            this.Controls.Add(this.btnBatal);
            this.Name = "RegisterControl";
            this.Size = new System.Drawing.Size(450, 400);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtKonfirmasiPassword;
        private System.Windows.Forms.Button btnDaftar;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblKonfirmasiPassword;
    }
}
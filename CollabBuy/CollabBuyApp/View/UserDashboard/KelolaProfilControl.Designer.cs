namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class KelolaProfilControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblNIM = new System.Windows.Forms.Label();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordInfo = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(166, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Kelola Profil";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(359, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Perbarui informasi pribadi dan pengaturan akun Anda di sini.";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.lblNama);
            this.pnlForm.Controls.Add(this.txtNama);
            this.pnlForm.Controls.Add(this.lblNIM);
            this.pnlForm.Controls.Add(this.txtNIM);
            this.pnlForm.Controls.Add(this.lblEmail);
            this.pnlForm.Controls.Add(this.txtEmail);
            this.pnlForm.Controls.Add(this.lblPassword);
            this.pnlForm.Controls.Add(this.txtPassword);
            this.pnlForm.Controls.Add(this.lblPasswordInfo);
            this.pnlForm.Controls.Add(this.btnSimpan);
            this.pnlForm.Location = new System.Drawing.Point(36, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(600, 420);
            this.pnlForm.TabIndex = 2;
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNama.Location = new System.Drawing.Point(30, 30);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(104, 19);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "Nama Lengkap";
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(34, 55);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(530, 27);
            this.txtNama.TabIndex = 1;
            // 
            // lblNIM
            // 
            this.lblNIM.AutoSize = true;
            this.lblNIM.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNIM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNIM.Location = new System.Drawing.Point(30, 100);
            this.lblNIM.Name = "lblNIM";
            this.lblNIM.Size = new System.Drawing.Size(73, 19);
            this.lblNIM.TabIndex = 2;
            this.lblNIM.Text = "NIM / NPM";
            // 
            // txtNIM
            // 
            this.txtNIM.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIM.Location = new System.Drawing.Point(34, 125);
            this.txtNIM.Name = "txtNIM";
            this.txtNIM.ReadOnly = true; // Biasanya NIM tidak bisa diubah sembarangan
            this.txtNIM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtNIM.Size = new System.Drawing.Size(530, 27);
            this.txtNIM.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblEmail.Location = new System.Drawing.Point(30, 170);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(91, 19);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Alamat Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(34, 195);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(530, 27);
            this.txtEmail.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblPassword.Location = new System.Drawing.Point(30, 240);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(104, 19);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password Baru";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(34, 265);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(530, 27);
            this.txtPassword.TabIndex = 7;
            // 
            // lblPasswordInfo
            // 
            this.lblPasswordInfo.AutoSize = true;
            this.lblPasswordInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblPasswordInfo.Location = new System.Drawing.Point(31, 295);
            this.lblPasswordInfo.Name = "lblPasswordInfo";
            this.lblPasswordInfo.Size = new System.Drawing.Size(306, 15);
            this.lblPasswordInfo.TabIndex = 8;
            this.lblPasswordInfo.Text = "*Kosongkan jika Anda tidak ingin mengubah kata sandi.";
            // 
            // btnSimpan
            // 
            this.btnSimpan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpan.FlatAppearance.BorderSize = 0;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimpan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnSimpan.Location = new System.Drawing.Point(34, 345);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(530, 40);
            this.btnSimpan.TabIndex = 9;
            this.btnSimpan.Text = "💾 Simpan Perubahan";
            this.btnSimpan.UseVisualStyleBackColor = false;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // KelolaProfilControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "KelolaProfilControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.KelolaProfilControl_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblNIM;
        private System.Windows.Forms.TextBox txtNIM;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordInfo;
        private System.Windows.Forms.Button btnSimpan;
    }
}

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class DaftarTokoControl
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
            this.lblNamaToko = new System.Windows.Forms.Label();
            this.txtNamaToko = new System.Windows.Forms.TextBox();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.chkSyarat = new System.Windows.Forms.CheckBox();
            this.btnAjukan = new System.Windows.Forms.Button();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatusVerifikasi = new System.Windows.Forms.Label();
            this.pnlForm.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Buka Lapak Jualan";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(437, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Isi formulir di bawah ini untuk mengajukan verifikasi sebagai Penjual.";
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.lblStatusVerifikasi);
            this.pnlStatus.Location = new System.Drawing.Point(36, 100);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(600, 45);
            this.pnlStatus.TabIndex = 2;
            this.pnlStatus.Visible = false; // Disembunyikan secara default, muncul jika sedang pending/ditolak
            // 
            // lblStatusVerifikasi
            // 
            this.lblStatusVerifikasi.AutoSize = true;
            this.lblStatusVerifikasi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusVerifikasi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblStatusVerifikasi.Location = new System.Drawing.Point(15, 12);
            this.lblStatusVerifikasi.Name = "lblStatusVerifikasi";
            this.lblStatusVerifikasi.Size = new System.Drawing.Size(325, 19);
            this.lblStatusVerifikasi.TabIndex = 0;
            this.lblStatusVerifikasi.Text = "⏳ Pengajuan lapak Anda sedang menunggu review Admin.";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.lblNamaToko);
            this.pnlForm.Controls.Add(this.txtNamaToko);
            this.pnlForm.Controls.Add(this.lblDeskripsi);
            this.pnlForm.Controls.Add(this.txtDeskripsi);
            this.pnlForm.Controls.Add(this.chkSyarat);
            this.pnlForm.Controls.Add(this.btnAjukan);
            this.pnlForm.Location = new System.Drawing.Point(36, 160);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(600, 350);
            this.pnlForm.TabIndex = 3;
            // 
            // lblNamaToko
            // 
            this.lblNamaToko.AutoSize = true;
            this.lblNamaToko.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaToko.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNamaToko.Location = new System.Drawing.Point(30, 30);
            this.lblNamaToko.Name = "lblNamaToko";
            this.lblNamaToko.Size = new System.Drawing.Size(125, 19);
            this.lblNamaToko.TabIndex = 0;
            this.lblNamaToko.Text = "Nama Lapak / Toko";
            // 
            // txtNamaToko
            // 
            this.txtNamaToko.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaToko.Location = new System.Drawing.Point(34, 55);
            this.txtNamaToko.Name = "txtNamaToko";
            this.txtNamaToko.Size = new System.Drawing.Size(530, 27);
            this.txtNamaToko.TabIndex = 1;
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoSize = true;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(30, 100);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(107, 19);
            this.lblDeskripsi.TabIndex = 2;
            this.lblDeskripsi.Text = "Deskripsi Jualan";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeskripsi.Location = new System.Drawing.Point(34, 125);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(530, 90);
            this.txtDeskripsi.TabIndex = 3;
            // 
            // chkSyarat
            // 
            this.chkSyarat.AutoSize = true;
            this.chkSyarat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSyarat.Location = new System.Drawing.Point(34, 235);
            this.chkSyarat.Name = "chkSyarat";
            this.chkSyarat.Size = new System.Drawing.Size(423, 21);
            this.chkSyarat.TabIndex = 4;
            this.chkSyarat.Text = "Saya menyetujui syarat dan ketentuan berjualan di aplikasi CollabBuy.";
            this.chkSyarat.UseVisualStyleBackColor = true;
            // 
            // btnAjukan
            // 
            this.btnAjukan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnAjukan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjukan.FlatAppearance.BorderSize = 0;
            this.btnAjukan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjukan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjukan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnAjukan.Location = new System.Drawing.Point(34, 280);
            this.btnAjukan.Name = "btnAjukan";
            this.btnAjukan.Size = new System.Drawing.Size(530, 40);
            this.btnAjukan.TabIndex = 5;
            this.btnAjukan.Text = "🚀 Ajukan Verifikasi";
            this.btnAjukan.UseVisualStyleBackColor = false;
            this.btnAjukan.Click += new System.EventHandler(this.btnAjukan_Click);
            // 
            // DaftarTokoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "DaftarTokoControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.DaftarTokoControl_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatusVerifikasi;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblNamaToko;
        private System.Windows.Forms.TextBox txtNamaToko;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.CheckBox chkSyarat;
        private System.Windows.Forms.Button btnAjukan;
    }
}

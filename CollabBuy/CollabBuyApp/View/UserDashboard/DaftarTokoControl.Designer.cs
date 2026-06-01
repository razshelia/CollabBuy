namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class DaftarTokoControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatusVerifikasi = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblNamaFile = new System.Windows.Forms.Label();
            this.btnUploadKTM = new System.Windows.Forms.Button();
            this.lblKTM = new System.Windows.Forms.Label();
            this.txtTahunMasuk = new System.Windows.Forms.TextBox();
            this.lblTahunMasuk = new System.Windows.Forms.Label();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.lblNIM = new System.Windows.Forms.Label();
            this.txtNamaToko = new System.Windows.Forms.TextBox();
            this.lblNamaToko = new System.Windows.Forms.Label();
            this.chkSyarat = new System.Windows.Forms.CheckBox();
            this.btnAjukan = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.pnlStatus);
            this.pnlCard.Controls.Add(this.pnlForm);
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Location = new System.Drawing.Point(260, 40);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(460, 600);
            this.pnlCard.TabIndex = 0;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.LightGreen;
            this.pnlStatus.Controls.Add(this.lblStatusVerifikasi);
            this.pnlStatus.Location = new System.Drawing.Point(40, 100);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(380, 60);
            this.pnlStatus.TabIndex = 13;
            this.pnlStatus.Visible = false;
            // 
            // lblStatusVerifikasi
            // 
            this.lblStatusVerifikasi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusVerifikasi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusVerifikasi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblStatusVerifikasi.Location = new System.Drawing.Point(0, 0);
            this.lblStatusVerifikasi.Name = "lblStatusVerifikasi";
            this.lblStatusVerifikasi.Size = new System.Drawing.Size(380, 60);
            this.lblStatusVerifikasi.TabIndex = 0;
            this.lblStatusVerifikasi.Text = "Status: Terverifikasi";
            this.lblStatusVerifikasi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.lblNamaFile);
            this.pnlForm.Controls.Add(this.btnUploadKTM);
            this.pnlForm.Controls.Add(this.lblKTM);
            this.pnlForm.Controls.Add(this.txtTahunMasuk);
            this.pnlForm.Controls.Add(this.lblTahunMasuk);
            this.pnlForm.Controls.Add(this.txtNIM);
            this.pnlForm.Controls.Add(this.lblNIM);
            this.pnlForm.Controls.Add(this.txtNamaToko);
            this.pnlForm.Controls.Add(this.lblNamaToko);
            this.pnlForm.Controls.Add(this.chkSyarat);
            this.pnlForm.Controls.Add(this.btnAjukan);
            this.pnlForm.Location = new System.Drawing.Point(40, 100);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(380, 470);
            this.pnlForm.TabIndex = 12;
            // 
            // lblNamaFile
            // 
            this.lblNamaFile.AutoSize = true;
            this.lblNamaFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNamaFile.ForeColor = System.Drawing.Color.Gray;
            this.lblNamaFile.Location = new System.Drawing.Point(120, 245);
            this.lblNamaFile.Name = "lblNamaFile";
            this.lblNamaFile.Size = new System.Drawing.Size(126, 15);
            this.lblNamaFile.TabIndex = 12;
            this.lblNamaFile.Text = "Belum ada file terpilih";
            // 
            // btnUploadKTM
            // 
            this.btnUploadKTM.BackColor = System.Drawing.Color.Gainsboro;
            this.btnUploadKTM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadKTM.FlatAppearance.BorderSize = 0;
            this.btnUploadKTM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadKTM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUploadKTM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnUploadKTM.Location = new System.Drawing.Point(0, 240);
            this.btnUploadKTM.Name = "btnUploadKTM";
            this.btnUploadKTM.Size = new System.Drawing.Size(110, 30);
            this.btnUploadKTM.TabIndex = 11;
            this.btnUploadKTM.Text = "📁 Pilih File...";
            this.btnUploadKTM.UseVisualStyleBackColor = false;
            this.btnUploadKTM.Click += new System.EventHandler(this.btnUploadKTM_Click);
            // 
            // lblKTM
            // 
            this.lblKTM.AutoSize = true;
            this.lblKTM.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKTM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblKTM.Location = new System.Drawing.Point(-3, 215);
            this.lblKTM.Name = "lblKTM";
            this.lblKTM.Size = new System.Drawing.Size(142, 17);
            this.lblKTM.TabIndex = 10;
            this.lblKTM.Text = "Upload Foto KTM (Asli)";
            // 
            // txtTahunMasuk
            // 
            this.txtTahunMasuk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtTahunMasuk.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTahunMasuk.Location = new System.Drawing.Point(0, 165);
            this.txtTahunMasuk.Name = "txtTahunMasuk";
            this.txtTahunMasuk.Size = new System.Drawing.Size(380, 27);
            this.txtTahunMasuk.TabIndex = 7;
            this.txtTahunMasuk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HanyaAngka_KeyPress);
            // 
            // lblTahunMasuk
            // 
            this.lblTahunMasuk.AutoSize = true;
            this.lblTahunMasuk.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTahunMasuk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTahunMasuk.Location = new System.Drawing.Point(-3, 140);
            this.lblTahunMasuk.Name = "lblTahunMasuk";
            this.lblTahunMasuk.Size = new System.Drawing.Size(149, 17);
            this.lblTahunMasuk.TabIndex = 6;
            this.lblTahunMasuk.Text = "Tahun Masuk (Angkatan)";
            // 
            // txtNIM
            // 
            this.txtNIM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtNIM.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNIM.Location = new System.Drawing.Point(0, 95);
            this.txtNIM.Name = "txtNIM";
            this.txtNIM.Size = new System.Drawing.Size(380, 27);
            this.txtNIM.TabIndex = 5;
            this.txtNIM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HanyaAngka_KeyPress);
            // 
            // lblNIM
            // 
            this.lblNIM.AutoSize = true;
            this.lblNIM.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNIM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNIM.Location = new System.Drawing.Point(-3, 70);
            this.lblNIM.Name = "lblNIM";
            this.lblNIM.Size = new System.Drawing.Size(206, 17);
            this.lblNIM.TabIndex = 4;
            this.lblNIM.Text = "Nomor Induk Mahasiswa (NIM)";
            // 
            // txtNamaToko
            // 
            this.txtNamaToko.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtNamaToko.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNamaToko.Location = new System.Drawing.Point(0, 25);
            this.txtNamaToko.Name = "txtNamaToko";
            this.txtNamaToko.Size = new System.Drawing.Size(380, 27);
            this.txtNamaToko.TabIndex = 3;
            // 
            // lblNamaToko
            // 
            this.lblNamaToko.AutoSize = true;
            this.lblNamaToko.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNamaToko.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNamaToko.Location = new System.Drawing.Point(-3, 0);
            this.lblNamaToko.Name = "lblNamaToko";
            this.lblNamaToko.Size = new System.Drawing.Size(126, 17);
            this.lblNamaToko.TabIndex = 2;
            this.lblNamaToko.Text = "Nama Lapak";
            // 
            // chkSyarat
            // 
            this.chkSyarat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSyarat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSyarat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkSyarat.Location = new System.Drawing.Point(0, 310);
            this.chkSyarat.Name = "chkSyarat";
            this.chkSyarat.Size = new System.Drawing.Size(380, 40);
            this.chkSyarat.TabIndex = 13;
            this.chkSyarat.Text = "Saya berjanji akan berjualan dengan jujur dan mematuhi aturan sistem Danus ini ya gy ya~";
            this.chkSyarat.UseVisualStyleBackColor = true;
            // 
            // btnAjukan
            // 
            this.btnAjukan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnAjukan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjukan.FlatAppearance.BorderSize = 0;
            this.btnAjukan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjukan.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnAjukan.ForeColor = System.Drawing.Color.White;
            this.btnAjukan.Location = new System.Drawing.Point(0, 370);
            this.btnAjukan.Name = "btnAjukan";
            this.btnAjukan.Size = new System.Drawing.Size(380, 45);
            this.btnAjukan.TabIndex = 10;
            this.btnAjukan.Text = "Ajukan Lapak! 🚀";
            this.btnAjukan.UseVisualStyleBackColor = false;
            this.btnAjukan.Click += new System.EventHandler(this.btnAjukan_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(36, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(262, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Isi data berikut buat verifikasi jadi Penjual";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(34, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Buka Lapak Skuy! 🏪";
            // 
            // DaftarTokoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlCard);
            this.Name = "DaftarTokoControl";
            this.Size = new System.Drawing.Size(1020, 720);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatusVerifikasi;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblNamaToko;
        private System.Windows.Forms.TextBox txtNamaToko;
        private System.Windows.Forms.Label lblNIM;
        private System.Windows.Forms.TextBox txtNIM;
        private System.Windows.Forms.Label lblTahunMasuk;
        private System.Windows.Forms.TextBox txtTahunMasuk;
        private System.Windows.Forms.Label lblKTM;
        private System.Windows.Forms.Button btnUploadKTM;
        private System.Windows.Forms.Label lblNamaFile;
        private System.Windows.Forms.CheckBox chkSyarat;
        private System.Windows.Forms.Button btnAjukan;
    }
}
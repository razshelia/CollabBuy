namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerVerificationControl
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
            lblNIM = new Label();
            txtNIM = new TextBox();
            lblNamaToko = new Label();
            txtNamaToko = new TextBox();
            lblTahunMasuk = new Label();
            txtTahunMasuk = new TextBox();
            btnUploadKTM = new Button();
            lblStatusKTM = new Label();
            btnKirim = new Button();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.FromArgb(45, 27, 79);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(lblNIM);
            pnlCard.Controls.Add(txtNIM);
            pnlCard.Controls.Add(lblNamaToko);
            pnlCard.Controls.Add(txtNamaToko);
            pnlCard.Controls.Add(lblTahunMasuk);
            pnlCard.Controls.Add(txtTahunMasuk);
            pnlCard.Controls.Add(btnUploadKTM);
            pnlCard.Controls.Add(lblStatusKTM);
            pnlCard.Controls.Add(btnKirim);
            pnlCard.Location = new Point(523, 166);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(500, 480);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Black", 18F);
            lblTitle.ForeColor = Color.FromArgb(253, 224, 71);
            lblTitle.Location = new Point(35, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(430, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BUKA LAPAK, BESTIE! 🚀";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(167, 139, 250);
            lblSubtitle.Location = new Point(35, 75);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(430, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Isi data di bawah buat daftar jadi penjual~";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNIM
            // 
            lblNIM.ForeColor = Color.White;
            lblNIM.Location = new Point(40, 115);
            lblNIM.Name = "lblNIM";
            lblNIM.Size = new Size(100, 23);
            lblNIM.TabIndex = 2;
            lblNIM.Text = "NIM:";
            // 
            // txtNIM
            // 
            txtNIM.Location = new Point(40, 135);
            txtNIM.Name = "txtNIM";
            txtNIM.Size = new Size(420, 23);
            txtNIM.TabIndex = 3;
            // 
            // lblNamaToko
            // 
            lblNamaToko.ForeColor = Color.White;
            lblNamaToko.Location = new Point(40, 170);
            lblNamaToko.Name = "lblNamaToko";
            lblNamaToko.Size = new Size(100, 23);
            lblNamaToko.TabIndex = 4;
            lblNamaToko.Text = "Nama Toko / Danus:";
            // 
            // txtNamaToko
            // 
            txtNamaToko.Location = new Point(40, 190);
            txtNamaToko.Name = "txtNamaToko";
            txtNamaToko.Size = new Size(420, 23);
            txtNamaToko.TabIndex = 5;
            // 
            // lblTahunMasuk
            // 
            lblTahunMasuk.ForeColor = Color.White;
            lblTahunMasuk.Location = new Point(40, 225);
            lblTahunMasuk.Name = "lblTahunMasuk";
            lblTahunMasuk.Size = new Size(100, 23);
            lblTahunMasuk.TabIndex = 6;
            lblTahunMasuk.Text = "Tahun Masuk Kuliah:";
            // 
            // txtTahunMasuk
            // 
            txtTahunMasuk.Location = new Point(40, 245);
            txtTahunMasuk.Name = "txtTahunMasuk";
            txtTahunMasuk.Size = new Size(120, 23);
            txtTahunMasuk.TabIndex = 7;
            // 
            // btnUploadKTM
            // 
            btnUploadKTM.BackColor = Color.FromArgb(167, 139, 250);
            btnUploadKTM.FlatStyle = FlatStyle.Flat;
            btnUploadKTM.ForeColor = Color.White;
            btnUploadKTM.Location = new Point(40, 290);
            btnUploadKTM.Name = "btnUploadKTM";
            btnUploadKTM.Size = new Size(75, 23);
            btnUploadKTM.TabIndex = 8;
            btnUploadKTM.Text = "📸 Upload Foto KTM";
            btnUploadKTM.UseVisualStyleBackColor = false;
            btnUploadKTM.Click += btnUploadKTM_Click;
            // 
            // lblStatusKTM
            // 
            lblStatusKTM.ForeColor = Color.Gray;
            lblStatusKTM.Location = new Point(40, 325);
            lblStatusKTM.Name = "lblStatusKTM";
            lblStatusKTM.Size = new Size(100, 23);
            lblStatusKTM.TabIndex = 9;
            lblStatusKTM.Text = "Belum ada file dipilih";
            // 
            // btnKirim
            // 
            btnKirim.BackColor = Color.FromArgb(167, 139, 250);
            btnKirim.FlatStyle = FlatStyle.Flat;
            btnKirim.Font = new Font("Segoe UI Black", 12F);
            btnKirim.ForeColor = Color.White;
            btnKirim.Location = new Point(40, 370);
            btnKirim.Name = "btnKirim";
            btnKirim.Size = new Size(420, 45);
            btnKirim.TabIndex = 10;
            btnKirim.Text = "KIRIM PENGAJUAN ✨";
            btnKirim.UseVisualStyleBackColor = false;
            btnKirim.Click += btnKirim_Click;
            // 
            // SellerVerificationControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(pnlCard);
            Name = "SellerVerificationControl";
            Size = new Size(1046, 333);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblNIM, lblNamaToko, lblTahunMasuk, lblStatusKTM;
        private System.Windows.Forms.TextBox txtNIM, txtNamaToko, txtTahunMasuk;
        private System.Windows.Forms.Button btnUploadKTM, btnKirim;
    }
}
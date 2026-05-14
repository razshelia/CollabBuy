namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerVerificationControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblNamaToko;
        private System.Windows.Forms.TextBox txtNamaToko;
        private System.Windows.Forms.Label lblNIM;
        private System.Windows.Forms.TextBox txtNIM;
        private System.Windows.Forms.Label lblTahunMasuk;
        private System.Windows.Forms.TextBox txtTahunMasuk;
        private System.Windows.Forms.Button btnUploadKTM;
        private System.Windows.Forms.Label lblStatusKTM;
        private System.Windows.Forms.Button btnKirimPengajuan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNamaToko = new System.Windows.Forms.Label();
            this.txtNamaToko = new System.Windows.Forms.TextBox();
            this.lblNIM = new System.Windows.Forms.Label();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.lblTahunMasuk = new System.Windows.Forms.Label();
            this.txtTahunMasuk = new System.Windows.Forms.TextBox();
            this.btnUploadKTM = new System.Windows.Forms.Button();
            this.lblStatusKTM = new System.Windows.Forms.Label();
            this.btnKirimPengajuan = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Judul
            this.lblJudul.Text = "FORM PENGAJUAN SELLER";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(30, 30);
            this.lblJudul.AutoSize = true;

            // Nama Toko
            this.lblNamaToko.Text = "Nama Toko / Usaha:";
            this.lblNamaToko.Location = new System.Drawing.Point(35, 90);
            this.lblNamaToko.AutoSize = true;
            this.txtNamaToko.Location = new System.Drawing.Point(35, 115);
            this.txtNamaToko.Size = new System.Drawing.Size(300, 27);

            // NIM
            this.lblNIM.Text = "NIM:";
            this.lblNIM.Location = new System.Drawing.Point(35, 160);
            this.lblNIM.AutoSize = true;
            this.txtNIM.Location = new System.Drawing.Point(35, 185);
            this.txtNIM.Size = new System.Drawing.Size(200, 27);

            // Tahun Masuk
            this.lblTahunMasuk.Text = "Tahun Masuk Kuliah:";
            this.lblTahunMasuk.Location = new System.Drawing.Point(35, 230);
            this.lblTahunMasuk.AutoSize = true;
            this.txtTahunMasuk.Location = new System.Drawing.Point(35, 255);
            this.txtTahunMasuk.Size = new System.Drawing.Size(120, 27);

            // Upload KTM
            this.btnUploadKTM.Text = "Upload Scan / Foto KTM";
            this.btnUploadKTM.Location = new System.Drawing.Point(35, 310);
            this.btnUploadKTM.Size = new System.Drawing.Size(200, 40);
            this.btnUploadKTM.Click += new System.EventHandler(this.btnUploadKTM_Click);

            this.lblStatusKTM.Text = "Belum ada file dipilih";
            this.lblStatusKTM.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusKTM.Location = new System.Drawing.Point(35, 355);
            this.lblStatusKTM.AutoSize = true;

            // Tombol Kirim
            this.btnKirimPengajuan.Text = "KIRIM PENGAJUAN";
            this.btnKirimPengajuan.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.btnKirimPengajuan.ForeColor = System.Drawing.Color.White;
            this.btnKirimPengajuan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirimPengajuan.Location = new System.Drawing.Point(35, 410);
            this.btnKirimPengajuan.Size = new System.Drawing.Size(300, 50);
            this.btnKirimPengajuan.Click += new System.EventHandler(this.btnKirimPengajuan_Click);

            // Susun kontrol
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.lblNamaToko);
            this.Controls.Add(this.txtNamaToko);
            this.Controls.Add(this.lblNIM);
            this.Controls.Add(this.txtNIM);
            this.Controls.Add(this.lblTahunMasuk);
            this.Controls.Add(this.txtTahunMasuk);
            this.Controls.Add(this.btnUploadKTM);
            this.Controls.Add(this.lblStatusKTM);
            this.Controls.Add(this.btnKirimPengajuan);

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(500, 550);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
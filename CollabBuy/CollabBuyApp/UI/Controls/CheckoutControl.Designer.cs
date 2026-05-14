namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class CheckoutControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblNamaProduk;
        private System.Windows.Forms.Label lblHargaSatuan;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Button btnUploadBukti;
        private System.Windows.Forms.Label lblStatusUpload;
        private System.Windows.Forms.PictureBox pictureBoxBukti;
        private System.Windows.Forms.Button btnCheckout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.lblHargaSatuan = new System.Windows.Forms.Label();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.btnUploadBukti = new System.Windows.Forms.Button();
            this.lblStatusUpload = new System.Windows.Forms.Label();
            this.pictureBoxBukti = new System.Windows.Forms.PictureBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBukti)).BeginInit();
            this.SuspendLayout();

            // lblJudul
            this.lblJudul.Text = "CHECKOUT";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(30, 20);
            this.lblJudul.AutoSize = true;

            // lblNamaProduk
            this.lblNamaProduk.Text = "Nama Produk";
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.Location = new System.Drawing.Point(30, 60);
            this.lblNamaProduk.AutoSize = true;

            // lblHargaSatuan
            this.lblHargaSatuan.Text = "Rp 0";
            this.lblHargaSatuan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHargaSatuan.Location = new System.Drawing.Point(30, 90);
            this.lblHargaSatuan.AutoSize = true;

            // lblJumlah
            this.lblJumlah.Text = "Jumlah Pesanan:";
            this.lblJumlah.Location = new System.Drawing.Point(30, 130);
            this.lblJumlah.AutoSize = true;

            // txtJumlah
            this.txtJumlah.Location = new System.Drawing.Point(30, 155);
            this.txtJumlah.Size = new System.Drawing.Size(100, 27);
            this.txtJumlah.Text = "1";

            // btnUploadBukti
            this.btnUploadBukti.Text = "Upload Bukti Transfer";
            this.btnUploadBukti.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.btnUploadBukti.ForeColor = System.Drawing.Color.White;
            this.btnUploadBukti.Location = new System.Drawing.Point(30, 200);
            this.btnUploadBukti.Size = new System.Drawing.Size(200, 35);
            this.btnUploadBukti.Click += new System.EventHandler(this.btnUploadBukti_Click);

            // lblStatusUpload
            this.lblStatusUpload.Text = "Belum ada file dipilih";
            this.lblStatusUpload.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusUpload.Location = new System.Drawing.Point(30, 240);
            this.lblStatusUpload.AutoSize = true;

            // pictureBoxBukti
            this.pictureBoxBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBukti.Location = new System.Drawing.Point(30, 270);
            this.pictureBoxBukti.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxBukti.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // btnCheckout
            this.btnCheckout.Text = "KIRIM PESANAN";
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.Location = new System.Drawing.Point(30, 490);
            this.btnCheckout.Size = new System.Drawing.Size(300, 45);
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);

            // Tambahkan kontrol
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.lblNamaProduk);
            this.Controls.Add(this.lblHargaSatuan);
            this.Controls.Add(this.lblJumlah);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.btnUploadBukti);
            this.Controls.Add(this.lblStatusUpload);
            this.Controls.Add(this.pictureBoxBukti);
            this.Controls.Add(this.btnCheckout);

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(500, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBukti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
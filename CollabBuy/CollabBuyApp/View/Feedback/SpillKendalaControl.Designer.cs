namespace CollabBuy.CollabBuyApp.View.Feedback
{
    partial class SpillKendalaControl
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
            this.lblJenisKendala = new System.Windows.Forms.Label();
            this.cbJenisKendala = new System.Windows.Forms.ComboBox();
            this.lblIdPesanan = new System.Windows.Forms.Label();
            this.txtIdPesanan = new System.Windows.Forms.TextBox();
            this.lblIdInfo = new System.Windows.Forms.Label();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.btnKirimAduan = new System.Windows.Forms.Button();
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
            this.lblTitle.Size = new System.Drawing.Size(167, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Spill Kendala";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(434, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Punya masalah dengan pesanan atau aplikasi? Laporkan ke Admin di sini.";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.lblJenisKendala);
            this.pnlForm.Controls.Add(this.cbJenisKendala);
            this.pnlForm.Controls.Add(this.lblIdPesanan);
            this.pnlForm.Controls.Add(this.txtIdPesanan);
            this.pnlForm.Controls.Add(this.lblIdInfo);
            this.pnlForm.Controls.Add(this.lblDeskripsi);
            this.pnlForm.Controls.Add(this.txtDeskripsi);
            this.pnlForm.Controls.Add(this.btnKirimAduan);
            this.pnlForm.Location = new System.Drawing.Point(36, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(650, 480);
            this.pnlForm.TabIndex = 2;
            // 
            // lblJenisKendala
            // 
            this.lblJenisKendala.AutoSize = true;
            this.lblJenisKendala.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJenisKendala.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblJenisKendala.Location = new System.Drawing.Point(30, 25);
            this.lblJenisKendala.Name = "lblJenisKendala";
            this.lblJenisKendala.Size = new System.Drawing.Size(95, 19);
            this.lblJenisKendala.TabIndex = 0;
            this.lblJenisKendala.Text = "Kategori Isu / Kendala";
            // 
            // cbJenisKendala
            // 
            this.cbJenisKendala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJenisKendala.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbJenisKendala.FormattingEnabled = true;
            this.cbJenisKendala.Items.AddRange(new object[] {
            "Masalah Transaksi (Penjual tidak merespon)",
            "Masalah Transaksi (Barang tidak sesuai)",
            "Masalah Transaksi (Pembayaran)",
            "Masalah Teknis Aplikasi (Error / Bug)",
            "Masalah Akun (Gagal Verifikasi / Lupa Password)",
            "Lainnya..."});
            this.cbJenisKendala.Location = new System.Drawing.Point(34, 50);
            this.cbJenisKendala.Name = "cbJenisKendala";
            this.cbJenisKendala.Size = new System.Drawing.Size(580, 28);
            this.cbJenisKendala.TabIndex = 1;
            // 
            // lblIdPesanan
            // 
            this.lblIdPesanan.AutoSize = true;
            this.lblIdPesanan.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdPesanan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblIdPesanan.Location = new System.Drawing.Point(30, 95);
            this.lblIdPesanan.Name = "lblIdPesanan";
            this.lblIdPesanan.Size = new System.Drawing.Size(130, 19);
            this.lblIdPesanan.TabIndex = 2;
            this.lblIdPesanan.Text = "ID Pesanan (Jika Ada)";
            // 
            // txtIdPesanan
            // 
            this.txtIdPesanan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPesanan.Location = new System.Drawing.Point(34, 120);
            this.txtIdPesanan.Name = "txtIdPesanan";
            this.txtIdPesanan.Size = new System.Drawing.Size(260, 27);
            this.txtIdPesanan.TabIndex = 3;
            // 
            // lblIdInfo
            // 
            this.lblIdInfo.AutoSize = true;
            this.lblIdInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblIdInfo.Location = new System.Drawing.Point(300, 125);
            this.lblIdInfo.Name = "lblIdInfo";
            this.lblIdInfo.Size = new System.Drawing.Size(280, 15);
            this.lblIdInfo.TabIndex = 4;
            this.lblIdInfo.Text = "*Kosongkan jika bukan masalah transaksi pesanan.";
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoSize = true;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(30, 165);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(127, 19);
            this.lblDeskripsi.TabIndex = 5;
            this.lblDeskripsi.Text = "Detail Permasalahan";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeskripsi.Location = new System.Drawing.Point(34, 190);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(580, 180);
            this.txtDeskripsi.TabIndex = 6;
            // 
            // btnKirimAduan
            // 
            this.btnKirimAduan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKirimAduan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKirimAduan.FlatAppearance.BorderSize = 0;
            this.btnKirimAduan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirimAduan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKirimAduan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnKirimAduan.Location = new System.Drawing.Point(34, 400);
            this.btnKirimAduan.Name = "btnKirimAduan";
            this.btnKirimAduan.Size = new System.Drawing.Size(580, 45);
            this.btnKirimAduan.TabIndex = 7;
            this.btnKirimAduan.Text = "📩 Kirim Aduan";
            this.btnKirimAduan.UseVisualStyleBackColor = false;
            this.btnKirimAduan.Click += new System.EventHandler(this.btnKirimAduan_Click);
            // 
            // SpillKendalaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "SpillKendalaControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.SpillKendalaControl_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblJenisKendala;
        private System.Windows.Forms.ComboBox cbJenisKendala;
        private System.Windows.Forms.Label lblIdPesanan;
        private System.Windows.Forms.TextBox txtIdPesanan;
        private System.Windows.Forms.Label lblIdInfo;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Button btnKirimAduan;
    }
}

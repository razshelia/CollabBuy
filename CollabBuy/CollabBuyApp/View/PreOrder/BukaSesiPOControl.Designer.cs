namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    partial class BukaSesiPOControl
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
            this.lblNamaSesi = new System.Windows.Forms.Label();
            this.txtNamaSesi = new System.Windows.Forms.TextBox();
            this.lblProduk = new System.Windows.Forms.Label();
            this.cbProduk = new System.Windows.Forms.ComboBox();
            this.lblQuota = new System.Windows.Forms.Label();
            this.numQuota = new System.Windows.Forms.NumericUpDown();
            this.lblBatasWaktu = new System.Windows.Forms.Label();
            this.dtpBatasWaktu = new System.Windows.Forms.DateTimePicker();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.btnSimpanSesi = new System.Windows.Forms.Button();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuota)).BeginInit();
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
            this.lblTitle.Text = "Buka Sesi PO Jualan";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(462, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Buat Sesi Pre-Order (PO) atau Danus baru untuk mulai mengumpulkan titipan.";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.lblNamaSesi);
            this.pnlForm.Controls.Add(this.txtNamaSesi);
            this.pnlForm.Controls.Add(this.lblProduk);
            this.pnlForm.Controls.Add(this.cbProduk);
            this.pnlForm.Controls.Add(this.lblQuota);
            this.pnlForm.Controls.Add(this.numQuota);
            this.pnlForm.Controls.Add(this.lblBatasWaktu);
            this.pnlForm.Controls.Add(this.dtpBatasWaktu);
            this.pnlForm.Controls.Add(this.lblDeskripsi);
            this.pnlForm.Controls.Add(this.txtDeskripsi);
            this.pnlForm.Controls.Add(this.btnSimpanSesi);
            this.pnlForm.Location = new System.Drawing.Point(36, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(650, 490);
            this.pnlForm.TabIndex = 2;
            // 
            // lblNamaSesi
            // 
            this.lblNamaSesi.AutoSize = true;
            this.lblNamaSesi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNamaSesi.Location = new System.Drawing.Point(30, 25);
            this.lblNamaSesi.Name = "lblNamaSesi";
            this.lblNamaSesi.Size = new System.Drawing.Size(149, 19);
            this.lblNamaSesi.TabIndex = 0;
            this.lblNamaSesi.Text = "Nama Sesi PO / Danus";
            // 
            // txtNamaSesi
            // 
            this.txtNamaSesi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaSesi.Location = new System.Drawing.Point(34, 50);
            this.txtNamaSesi.Name = "txtNamaSesi";
            this.txtNamaSesi.Size = new System.Drawing.Size(580, 27);
            this.txtNamaSesi.TabIndex = 1;
            // 
            // lblProduk
            // 
            this.lblProduk.AutoSize = true;
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblProduk.Location = new System.Drawing.Point(30, 95);
            this.lblProduk.Name = "lblProduk";
            this.lblProduk.Size = new System.Drawing.Size(139, 19);
            this.lblProduk.TabIndex = 2;
            this.lblProduk.Text = "Pilih Produk Master";
            // 
            // cbProduk
            // 
            this.cbProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProduk.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProduk.FormattingEnabled = true;
            this.cbProduk.Location = new System.Drawing.Point(34, 120);
            this.cbProduk.Name = "cbProduk";
            this.cbProduk.Size = new System.Drawing.Size(580, 28);
            this.cbProduk.TabIndex = 3;
            // 
            // lblQuota
            // 
            this.lblQuota.AutoSize = true;
            this.lblQuota.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblQuota.Location = new System.Drawing.Point(30, 165);
            this.lblQuota.Name = "lblQuota";
            this.lblQuota.Size = new System.Drawing.Size(161, 19);
            this.lblQuota.TabIndex = 4;
            this.lblQuota.Text = "Target Batas Maks Kuota";
            // 
            // numQuota
            // 
            this.numQuota.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQuota.Location = new System.Drawing.Point(34, 190);
            this.numQuota.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numQuota.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuota.Name = "numQuota";
            this.numQuota.Size = new System.Drawing.Size(260, 27);
            this.numQuota.TabIndex = 5;
            this.numQuota.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblBatasWaktu
            // 
            this.lblBatasWaktu.AutoSize = true;
            this.lblBatasWaktu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatasWaktu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblBatasWaktu.Location = new System.Drawing.Point(340, 165);
            this.lblBatasWaktu.Name = "lblBatasWaktu";
            this.lblBatasWaktu.Size = new System.Drawing.Size(126, 19);
            this.lblBatasWaktu.TabIndex = 6;
            this.lblBatasWaktu.Text = "Tenggat Selesai PO";
            // 
            // dtpBatasWaktu
            // 
            this.dtpBatasWaktu.CustomFormat = "dd MMMM yyyy HH:mm";
            this.dtpBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBatasWaktu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBatasWaktu.Location = new System.Drawing.Point(344, 190);
            this.dtpBatasWaktu.Name = "dtpBatasWaktu";
            this.dtpBatasWaktu.Size = new System.Drawing.Size(270, 27);
            this.dtpBatasWaktu.TabIndex = 7;
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoSize = true;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(30, 240);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(176, 19);
            this.lblDeskripsi.TabIndex = 8;
            this.lblDeskripsi.Text = "Catatan / Deskripsi Sistem";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeskripsi.Location = new System.Drawing.Point(34, 265);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(580, 110);
            this.txtDeskripsi.TabIndex = 9;
            // 
            // btnSimpanSesi
            // 
            this.btnSimpanSesi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnSimpanSesi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanSesi.FlatAppearance.BorderSize = 0;
            this.btnSimpanSesi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanSesi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimpanSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnSimpanSesi.Location = new System.Drawing.Point(34, 405);
            this.btnSimpanSesi.Name = "btnSimpanSesi";
            this.btnSimpanSesi.Size = new System.Drawing.Size(580, 45);
            this.btnSimpanSesi.TabIndex = 10;
            this.btnSimpanSesi.Text = "🚀 Luncurkan Sesi PO Baru";
            this.btnSimpanSesi.UseVisualStyleBackColor = false;
            this.btnSimpanSesi.Click += new System.EventHandler(this.btnSimpanSesi_Click);
            // 
            // BukaSesiPOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "BukaSesiPOControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.BukaSesiPOControl_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuota)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblNamaSesi;
        private System.Windows.Forms.TextBox txtNamaSesi;
        private System.Windows.Forms.Label lblProduk;
        private System.Windows.Forms.ComboBox cbProduk;
        private System.Windows.Forms.Label lblQuota;
        private System.Windows.Forms.NumericUpDown numQuota;
        private System.Windows.Forms.Label lblBatasWaktu;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Button btnSimpanSesi;
    }
}

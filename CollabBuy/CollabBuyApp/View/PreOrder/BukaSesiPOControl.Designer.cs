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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblNamaSesi = new System.Windows.Forms.Label();
            this.txtNamaSesi = new System.Windows.Forms.TextBox();
            this.lblJenis = new System.Windows.Forms.Label();
            this.cbJenisPO = new System.Windows.Forms.ComboBox();
            this.lblProduk = new System.Windows.Forms.Label();
            this.cbProduk = new System.Windows.Forms.ComboBox();
            this.lblQuota = new System.Windows.Forms.Label();
            this.numQuota = new System.Windows.Forms.NumericUpDown();
            this.lblBatasWaktu = new System.Windows.Forms.Label();
            this.dtpBatasWaktu = new System.Windows.Forms.DateTimePicker();
            this.lblRekening = new System.Windows.Forms.Label();
            this.txtRekening = new System.Windows.Forms.TextBox();
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
            this.lblTitle.Size = new System.Drawing.Size(347, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "✨ Launching Sesi PO Baru";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(24)))), ((int)(((byte)(154)))));
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(431, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Spill produk jualan lo di sini, atur kuota, dan cuan bareng! 💸";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(170)))), ((int)(((byte)(255))))); // Soft purple background form
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlForm.Controls.Add(this.lblNamaSesi);
            this.pnlForm.Controls.Add(this.txtNamaSesi);
            this.pnlForm.Controls.Add(this.lblJenis);
            this.pnlForm.Controls.Add(this.cbJenisPO);
            this.pnlForm.Controls.Add(this.lblProduk);
            this.pnlForm.Controls.Add(this.cbProduk);
            this.pnlForm.Controls.Add(this.lblQuota);
            this.pnlForm.Controls.Add(this.numQuota);
            this.pnlForm.Controls.Add(this.lblBatasWaktu);
            this.pnlForm.Controls.Add(this.dtpBatasWaktu);
            this.pnlForm.Controls.Add(this.lblRekening);
            this.pnlForm.Controls.Add(this.txtRekening);
            this.pnlForm.Controls.Add(this.btnSimpanSesi);
            this.pnlForm.Location = new System.Drawing.Point(36, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(650, 490);
            this.pnlForm.TabIndex = 2;
            // 
            // lblNamaSesi
            // 
            this.lblNamaSesi.AutoSize = true;
            this.lblNamaSesi.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNamaSesi.Location = new System.Drawing.Point(30, 25);
            this.lblNamaSesi.Name = "lblNamaSesi";
            this.lblNamaSesi.Size = new System.Drawing.Size(161, 19);
            this.lblNamaSesi.TabIndex = 0;
            this.lblNamaSesi.Text = "Nama Sesi PO / Danus";
            // 
            // txtNamaSesi
            // 
            this.txtNamaSesi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaSesi.Location = new System.Drawing.Point(34, 50);
            this.txtNamaSesi.Name = "txtNamaSesi";
            this.txtNamaSesi.Size = new System.Drawing.Size(350, 27);
            this.txtNamaSesi.TabIndex = 1;
            // 
            // lblJenis
            // 
            this.lblJenis.AutoSize = true;
            this.lblJenis.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJenis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblJenis.Location = new System.Drawing.Point(400, 25);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(66, 19);
            this.lblJenis.TabIndex = 11;
            this.lblJenis.Text = "Tipe PO";
            // 
            // cbJenisPO
            // 
            this.cbJenisPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJenisPO.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbJenisPO.FormattingEnabled = true;
            this.cbJenisPO.Items.AddRange(new object[] { "Biasa", "Gotong Royong" });
            this.cbJenisPO.Location = new System.Drawing.Point(404, 50);
            this.cbJenisPO.Name = "cbJenisPO";
            this.cbJenisPO.Size = new System.Drawing.Size(210, 28);
            this.cbJenisPO.TabIndex = 12;
            // 
            // lblProduk
            // 
            this.lblProduk.AutoSize = true;
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblProduk.Location = new System.Drawing.Point(30, 95);
            this.lblProduk.Name = "lblProduk";
            this.lblProduk.Size = new System.Drawing.Size(133, 19);
            this.lblProduk.TabIndex = 2;
            this.lblProduk.Text = "Barang yang dijual";
            // 
            // cbProduk
            // 
            this.cbProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProduk.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProduk.FormattingEnabled = true;
            this.cbProduk.Location = new System.Drawing.Point(34, 120);
            this.cbProduk.Visible = false;
            this.lblProduk.Visible = false;
            this.cbProduk.Name = "cbProduk";
            this.cbProduk.Size = new System.Drawing.Size(580, 28);
            this.cbProduk.TabIndex = 3;
            // 
            // lblQuota
            // 
            this.lblQuota.AutoSize = true;
            this.lblQuota.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblQuota.Location = new System.Drawing.Point(30, 165);
            this.lblQuota.Name = "lblQuota";
            this.lblQuota.Size = new System.Drawing.Size(92, 19);
            this.lblQuota.TabIndex = 4;
            this.lblQuota.Text = "Target Slot";
            // 
            // numQuota
            // 
            this.numQuota.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQuota.Location = new System.Drawing.Point(34, 190);
            this.numQuota.Visible = false;
            this.lblQuota.Visible = false;
            this.numQuota.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numQuota.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuota.Name = "numQuota";
            this.numQuota.Size = new System.Drawing.Size(260, 27);
            this.numQuota.TabIndex = 5;
            this.numQuota.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblBatasWaktu
            // 
            this.lblBatasWaktu.AutoSize = true;
            this.lblBatasWaktu.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatasWaktu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblBatasWaktu.Location = new System.Drawing.Point(340, 165);
            this.lblBatasWaktu.Name = "lblBatasWaktu";
            this.lblBatasWaktu.Size = new System.Drawing.Size(146, 19);
            this.lblBatasWaktu.TabIndex = 6;
            this.lblBatasWaktu.Text = "Waktu Tutup Lapak";
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
            // lblRekening
            // 
            this.lblRekening.AutoSize = true;
            this.lblRekening.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRekening.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblRekening.Location = new System.Drawing.Point(30, 100);
            this.lblRekening.Name = "lblRekening";
            this.lblRekening.Size = new System.Drawing.Size(155, 19);
            this.lblRekening.TabIndex = 8;
            this.lblRekening.Text = "Info Rekening / Qris";
            // 
            // txtRekening
            // 
            this.txtRekening.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRekening.Location = new System.Drawing.Point(34, 125);
            this.txtRekening.Multiline = true;
            this.txtRekening.Name = "txtRekening";
            this.txtRekening.Size = new System.Drawing.Size(580, 80);
            this.txtRekening.TabIndex = 9;
            this.txtRekening.Text = "BCA 1234567 a.n Jagoan Cuan";
            // 
            // btnSimpanSesi
            // 
            this.btnSimpanSesi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnSimpanSesi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanSesi.FlatAppearance.BorderSize = 0;
            this.btnSimpanSesi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanSesi.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimpanSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182))))); // Soft yellow text
            this.btnSimpanSesi.Location = new System.Drawing.Point(34, 265);
            this.btnSimpanSesi.Name = "btnSimpanSesi";
            this.btnSimpanSesi.Size = new System.Drawing.Size(580, 45);
            this.btnSimpanSesi.TabIndex = 10;
            this.btnSimpanSesi.Text = "🚀 Gas Launching Sesi!";
            this.btnSimpanSesi.UseVisualStyleBackColor = false;
            this.btnSimpanSesi.Click += new System.EventHandler(this.btnSimpanSesi_Click);
            // 
            // BukaSesiPOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
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

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblNamaSesi;
        private System.Windows.Forms.TextBox txtNamaSesi;
        private System.Windows.Forms.Label lblJenis;
        private System.Windows.Forms.ComboBox cbJenisPO;
        private System.Windows.Forms.Label lblProduk;
        private System.Windows.Forms.ComboBox cbProduk;
        private System.Windows.Forms.Label lblQuota;
        private System.Windows.Forms.NumericUpDown numQuota;
        private System.Windows.Forms.Label lblBatasWaktu;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Label lblRekening;
        private System.Windows.Forms.TextBox txtRekening;
        private System.Windows.Forms.Button btnSimpanSesi;
    }
}
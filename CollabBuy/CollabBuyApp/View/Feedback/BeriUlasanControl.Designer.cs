namespace CollabBuy.CollabBuyApp.View.Feedback
{
    partial class BeriUlasanControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvPesananSelesai = new System.Windows.Forms.DataGridView();
            this.pnlFormUlasan = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblProdukTerpilih = new System.Windows.Forms.Label();
            this.txtProdukTerpilih = new System.Windows.Forms.TextBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.cbRating = new System.Windows.Forms.ComboBox();
            this.lblKomentar = new System.Windows.Forms.Label();
            this.txtKomentar = new System.Windows.Forms.TextBox();
            this.btnKirimUlasan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesananSelesai)).BeginInit();
            this.pnlFormUlasan.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Beri Ulasan";
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
            this.lblSubtitle.Text = "Pilih pesanan yang sudah selesai untuk memberikan rating dan ulasan.";
            // 
            // dgvPesananSelesai
            // 
            this.dgvPesananSelesai.AllowUserToAddRows = false;
            this.dgvPesananSelesai.AllowUserToDeleteRows = false;
            this.dgvPesananSelesai.BackgroundColor = System.Drawing.Color.White;
            this.dgvPesananSelesai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPesananSelesai.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPesananSelesai.ColumnHeadersHeight = 35;
            this.dgvPesananSelesai.EnableHeadersVisualStyles = false;
            this.dgvPesananSelesai.Location = new System.Drawing.Point(36, 110);
            this.dgvPesananSelesai.Name = "dgvPesananSelesai";
            this.dgvPesananSelesai.ReadOnly = true;
            this.dgvPesananSelesai.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPesananSelesai.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPesananSelesai.RowTemplate.Height = 35;
            this.dgvPesananSelesai.Size = new System.Drawing.Size(500, 480);
            this.dgvPesananSelesai.TabIndex = 2;
            this.dgvPesananSelesai.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesananSelesai_CellContentClick);
            // 
            // pnlFormUlasan
            // 
            this.pnlFormUlasan.BackColor = System.Drawing.Color.White;
            this.pnlFormUlasan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFormUlasan.Controls.Add(this.lblFormTitle);
            this.pnlFormUlasan.Controls.Add(this.lblProdukTerpilih);
            this.pnlFormUlasan.Controls.Add(this.txtProdukTerpilih);
            this.pnlFormUlasan.Controls.Add(this.lblRating);
            this.pnlFormUlasan.Controls.Add(this.cbRating);
            this.pnlFormUlasan.Controls.Add(this.lblKomentar);
            this.pnlFormUlasan.Controls.Add(this.txtKomentar);
            this.pnlFormUlasan.Controls.Add(this.btnKirimUlasan);
            this.pnlFormUlasan.Enabled = false; // Disable sebelum ada baris yang diklik
            this.pnlFormUlasan.Location = new System.Drawing.Point(560, 110);
            this.pnlFormUlasan.Name = "pnlFormUlasan";
            this.pnlFormUlasan.Size = new System.Drawing.Size(380, 480);
            this.pnlFormUlasan.TabIndex = 3;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblFormTitle.Location = new System.Drawing.Point(20, 20);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(147, 21);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Formulir Penilaian";
            // 
            // lblProdukTerpilih
            // 
            this.lblProdukTerpilih.AutoSize = true;
            this.lblProdukTerpilih.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdukTerpilih.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblProdukTerpilih.Location = new System.Drawing.Point(21, 65);
            this.lblProdukTerpilih.Name = "lblProdukTerpilih";
            this.lblProdukTerpilih.Size = new System.Drawing.Size(124, 17);
            this.lblProdukTerpilih.TabIndex = 1;
            this.lblProdukTerpilih.Text = "Barang yang diulas:";
            // 
            // txtProdukTerpilih
            // 
            this.txtProdukTerpilih.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdukTerpilih.Location = new System.Drawing.Point(25, 90);
            this.txtProdukTerpilih.Name = "txtProdukTerpilih";
            this.txtProdukTerpilih.ReadOnly = true;
            this.txtProdukTerpilih.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtProdukTerpilih.Size = new System.Drawing.Size(330, 25);
            this.txtProdukTerpilih.TabIndex = 2;
            this.txtProdukTerpilih.Text = "Pilih pesanan di tabel kiri...";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblRating.Location = new System.Drawing.Point(21, 135);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(125, 17);
            this.lblRating.TabIndex = 3;
            this.lblRating.Text = "Pilih Bintang (1 - 5):";
            // 
            // cbRating
            // 
            this.cbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRating.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRating.FormattingEnabled = true;
            this.cbRating.Items.AddRange(new object[] {
            "⭐⭐⭐⭐⭐ (5 - Sangat Baik)",
            "⭐⭐⭐⭐ (4 - Baik)",
            "⭐⭐⭐ (3 - Cukup)",
            "⭐⭐ (2 - Kurang)",
            "⭐ (1 - Sangat Kurang)"});
            this.cbRating.Location = new System.Drawing.Point(25, 160);
            this.cbRating.Name = "cbRating";
            this.cbRating.Size = new System.Drawing.Size(330, 28);
            this.cbRating.TabIndex = 4;
            // 
            // lblKomentar
            // 
            this.lblKomentar.AutoSize = true;
            this.lblKomentar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKomentar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblKomentar.Location = new System.Drawing.Point(21, 205);
            this.lblKomentar.Name = "lblKomentar";
            this.lblKomentar.Size = new System.Drawing.Size(183, 17);
            this.lblKomentar.TabIndex = 5;
            this.lblKomentar.Text = "Tulis pengalaman Anda (Opsional):";
            // 
            // txtKomentar
            // 
            this.txtKomentar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKomentar.Location = new System.Drawing.Point(25, 230);
            this.txtKomentar.Multiline = true;
            this.txtKomentar.Name = "txtKomentar";
            this.txtKomentar.Size = new System.Drawing.Size(330, 150);
            this.txtKomentar.TabIndex = 6;
            // 
            // btnKirimUlasan
            // 
            this.btnKirimUlasan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKirimUlasan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKirimUlasan.FlatAppearance.BorderSize = 0;
            this.btnKirimUlasan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirimUlasan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKirimUlasan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnKirimUlasan.Location = new System.Drawing.Point(25, 410);
            this.btnKirimUlasan.Name = "btnKirimUlasan";
            this.btnKirimUlasan.Size = new System.Drawing.Size(330, 45);
            this.btnKirimUlasan.TabIndex = 7;
            this.btnKirimUlasan.Text = "⭐ Kirim Ulasan";
            this.btnKirimUlasan.UseVisualStyleBackColor = false;
            this.btnKirimUlasan.Click += new System.EventHandler(this.btnKirimUlasan_Click);
            // 
            // BeriUlasanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlFormUlasan);
            this.Controls.Add(this.dgvPesananSelesai);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "BeriUlasanControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.BeriUlasanControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesananSelesai)).EndInit();
            this.pnlFormUlasan.ResumeLayout(false);
            this.pnlFormUlasan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.DataGridView dgvPesananSelesai;
        private System.Windows.Forms.Panel pnlFormUlasan;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblProdukTerpilih;
        private System.Windows.Forms.TextBox txtProdukTerpilih;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cbRating;
        private System.Windows.Forms.Label lblKomentar;
        private System.Windows.Forms.TextBox txtKomentar;
        private System.Windows.Forms.Button btnKirimUlasan;
    }
}

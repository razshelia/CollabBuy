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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblProduk = new System.Windows.Forms.Label();
            this.cbProduk = new System.Windows.Forms.ComboBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.numRating = new System.Windows.Forms.NumericUpDown();
            this.lblKomentar = new System.Windows.Forms.Label();
            this.txtKomentar = new System.Windows.Forms.TextBox();
            this.btnKirim = new System.Windows.Forms.Button();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🌟 Kasih Rating Dulu";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(24)))), ((int)(((byte)(154)))));
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(325, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Gimana barangnya bestie? Spill dong ke yang lain!";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182))))); // Soft Yellow
            this.pnlForm.Controls.Add(this.btnKirim);
            this.pnlForm.Controls.Add(this.txtKomentar);
            this.pnlForm.Controls.Add(this.lblKomentar);
            this.pnlForm.Controls.Add(this.numRating);
            this.pnlForm.Controls.Add(this.lblRating);
            this.pnlForm.Controls.Add(this.cbProduk);
            this.pnlForm.Controls.Add(this.lblProduk);
            this.pnlForm.Location = new System.Drawing.Point(38, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(500, 420);
            this.pnlForm.TabIndex = 2;
            // 
            // lblProduk
            // 
            this.lblProduk.AutoSize = true;
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblProduk.Location = new System.Drawing.Point(20, 20);
            this.lblProduk.Name = "lblProduk";
            this.lblProduk.Size = new System.Drawing.Size(155, 19);
            this.lblProduk.TabIndex = 0;
            this.lblProduk.Text = "Pilih Barang Belanjaan";
            // 
            // cbProduk
            // 
            this.cbProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProduk.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbProduk.FormattingEnabled = true;
            this.cbProduk.Location = new System.Drawing.Point(24, 45);
            this.cbProduk.Name = "cbProduk";
            this.cbProduk.Size = new System.Drawing.Size(450, 28);
            this.cbProduk.TabIndex = 1;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblRating.Location = new System.Drawing.Point(20, 90);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(127, 19);
            this.lblRating.TabIndex = 2;
            this.lblRating.Text = "Bintang (1 s/d 5)";
            // 
            // numRating
            // 
            this.numRating.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numRating.Location = new System.Drawing.Point(24, 115);
            this.numRating.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            this.numRating.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numRating.Name = "numRating";
            this.numRating.Size = new System.Drawing.Size(120, 27);
            this.numRating.TabIndex = 3;
            this.numRating.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblKomentar
            // 
            this.lblKomentar.AutoSize = true;
            this.lblKomentar.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblKomentar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblKomentar.Location = new System.Drawing.Point(20, 160);
            this.lblKomentar.Name = "lblKomentar";
            this.lblKomentar.Size = new System.Drawing.Size(161, 19);
            this.lblKomentar.TabIndex = 4;
            this.lblKomentar.Text = "Jujur Aja, Bagus Gak?";
            // 
            // txtKomentar
            // 
            this.txtKomentar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKomentar.Location = new System.Drawing.Point(24, 185);
            this.txtKomentar.Multiline = true;
            this.txtKomentar.Name = "txtKomentar";
            this.txtKomentar.Size = new System.Drawing.Size(450, 140);
            this.txtKomentar.TabIndex = 5;
            // 
            // btnKirim
            // 
            this.btnKirim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70))))); // Deep Purple
            this.btnKirim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKirim.FlatAppearance.BorderSize = 0;
            this.btnKirim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirim.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnKirim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(170)))), ((int)(((byte)(255))))); // Soft Purple
            this.btnKirim.Location = new System.Drawing.Point(24, 345);
            this.btnKirim.Name = "btnKirim";
            this.btnKirim.Size = new System.Drawing.Size(450, 45);
            this.btnKirim.TabIndex = 6;
            this.btnKirim.Text = "🚀 Kirim Review";
            this.btnKirim.UseVisualStyleBackColor = false;
            this.btnKirim.Click += new System.EventHandler(this.btnKirim_Click);
            // 
            // BeriUlasanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "BeriUlasanControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.BeriUlasanControl_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblProduk;
        private System.Windows.Forms.ComboBox cbProduk;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.NumericUpDown numRating;
        private System.Windows.Forms.Label lblKomentar;
        private System.Windows.Forms.TextBox txtKomentar;
        private System.Windows.Forms.Button btnKirim;
    }
}
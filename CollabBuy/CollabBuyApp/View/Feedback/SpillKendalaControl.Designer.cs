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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblSubjek = new System.Windows.Forms.Label();
            this.txtSubjek = new System.Windows.Forms.TextBox();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.btnAduan = new System.Windows.Forms.Button();
            this.lblRiwayat = new System.Windows.Forms.Label();
            this.dgvRiwayat = new System.Windows.Forms.DataGridView();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(263, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🗣️ Spill Kendala Kamu";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(24)))), ((int)(((byte)(154)))));
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(367, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Ada masalah sama pesanan atau sistem? Curhatin aja sini!";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(170)))), ((int)(((byte)(255))))); // Soft Purple
            this.pnlForm.Controls.Add(this.btnAduan);
            this.pnlForm.Controls.Add(this.txtDeskripsi);
            this.pnlForm.Controls.Add(this.lblDeskripsi);
            this.pnlForm.Controls.Add(this.txtSubjek);
            this.pnlForm.Controls.Add(this.lblSubjek);
            this.pnlForm.Location = new System.Drawing.Point(38, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(400, 480);
            this.pnlForm.TabIndex = 2;
            // 
            // lblSubjek
            // 
            this.lblSubjek.AutoSize = true;
            this.lblSubjek.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubjek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblSubjek.Location = new System.Drawing.Point(20, 20);
            this.lblSubjek.Name = "lblSubjek";
            this.lblSubjek.Size = new System.Drawing.Size(120, 19);
            this.lblSubjek.TabIndex = 0;
            this.lblSubjek.Text = "Inti Masalahnya";
            // 
            // txtSubjek
            // 
            this.txtSubjek.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSubjek.Location = new System.Drawing.Point(24, 45);
            this.txtSubjek.Name = "txtSubjek";
            this.txtSubjek.Size = new System.Drawing.Size(350, 27);
            this.txtSubjek.TabIndex = 1;
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoSize = true;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(20, 90);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(125, 19);
            this.lblDeskripsi.TabIndex = 2;
            this.lblDeskripsi.Text = "Kronologi Detail";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDeskripsi.Location = new System.Drawing.Point(24, 115);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(350, 260);
            this.txtDeskripsi.TabIndex = 3;
            // 
            // btnAduan
            // 
            this.btnAduan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70))))); // Deep Purple
            this.btnAduan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAduan.FlatAppearance.BorderSize = 0;
            this.btnAduan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAduan.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnAduan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182))))); // Soft Yellow
            this.btnAduan.Location = new System.Drawing.Point(24, 400);
            this.btnAduan.Name = "btnAduan";
            this.btnAduan.Size = new System.Drawing.Size(350, 45);
            this.btnAduan.TabIndex = 4;
            this.btnAduan.Text = "🚀 Kirim Tombol Aduan";
            this.btnAduan.UseVisualStyleBackColor = false;
            this.btnAduan.Click += new System.EventHandler(this.btnAduan_Click);
            // 
            // lblRiwayat
            // 
            this.lblRiwayat.AutoSize = true;
            this.lblRiwayat.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblRiwayat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblRiwayat.Location = new System.Drawing.Point(460, 110);
            this.lblRiwayat.Name = "lblRiwayat";
            this.lblRiwayat.Size = new System.Drawing.Size(149, 21);
            this.lblRiwayat.TabIndex = 3;
            this.lblRiwayat.Text = "Riwayat Curhatan";
            // 
            // dgvRiwayat
            // 
            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.BackgroundColor = System.Drawing.Color.White;
            this.dgvRiwayat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiwayat.Location = new System.Drawing.Point(464, 145);
            this.dgvRiwayat.Name = "dgvRiwayat";
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.Size = new System.Drawing.Size(480, 445);
            this.dgvRiwayat.TabIndex = 4;
            // 
            // SpillKendalaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvRiwayat);
            this.Controls.Add(this.lblRiwayat);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "SpillKendalaControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.SpillKendalaControl_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblSubjek;
        private System.Windows.Forms.TextBox txtSubjek;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Button btnAduan;
        private System.Windows.Forms.Label lblRiwayat;
        private System.Windows.Forms.DataGridView dgvRiwayat;
    }
}
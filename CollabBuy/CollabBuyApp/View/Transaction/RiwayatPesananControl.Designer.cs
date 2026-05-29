namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class RiwayatPesananControl
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvRiwayat = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(217, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Riwayat Pesanan";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(325, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Pantau status alur transaksi dan belanjaan aktif Anda.";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Windows.Forms.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnRefresh.Location = new System.Drawing.Point(816, 60);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Perbarui Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvRiwayat
            // 
            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.AllowUserToDeleteRows = false;
            this.dgvRiwayat.BackgroundColor = System.Drawing.Color.White;
            this.dgvRiwayat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRiwayat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRiwayat.ColumnHeadersHeight = 40;
            this.dgvRiwayat.EnableHeadersVisualStyles = false;
            this.dgvRiwayat.Location = new System.Drawing.Point(36, 115);
            this.dgvRiwayat.Name = "dgvRiwayat";
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvRiwayat.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRiwayat.RowTemplate.Height = 38;
            this.dgvRiwayat.Size = new System.Drawing.Size(900, 480);
            this.dgvRiwayat.TabIndex = 3;
            this.dgvRiwayat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRiwayat_CellContentClick);
            // 
            // RiwayatPesananControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvRiwayat);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "RiwayatPesananControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.RiwayatPesananControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvRiwayat;
    }
}

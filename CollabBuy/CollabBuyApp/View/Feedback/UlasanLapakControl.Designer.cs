namespace CollabBuy.CollabBuyApp.View.Feedback
{
    partial class UlasanLapakControl
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
            this.dgvUlasan = new System.Windows.Forms.DataGridView();
            this.lblRatingAvg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUlasan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(183, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ulasan Lapak";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(360, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Lihat apa yang dikatakan pelanggan tentang produk Anda.";
            // 
            // dgvUlasan
            // 
            this.dgvUlasan.AllowUserToAddRows = false;
            this.dgvUlasan.AllowUserToDeleteRows = false;
            this.dgvUlasan.BackgroundColor = System.Drawing.Color.White;
            this.dgvUlasan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.dgvUlasan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUlasan.ColumnHeadersHeight = 40;
            this.dgvUlasan.EnableHeadersVisualStyles = false;
            this.dgvUlasan.Location = new System.Drawing.Point(36, 120);
            this.dgvUlasan.Name = "dgvUlasan";
            this.dgvUlasan.ReadOnly = true;
            this.dgvUlasan.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUlasan.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUlasan.RowTemplate.Height = 50;
            this.dgvUlasan.Size = new System.Drawing.Size(920, 480);
            this.dgvUlasan.TabIndex = 2;
            // 
            // lblRatingAvg
            // 
            this.lblRatingAvg.AutoSize = true;
            this.lblRatingAvg.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRatingAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));
            this.lblRatingAvg.Location = new System.Drawing.Point(800, 70);
            this.lblRatingAvg.Name = "lblRatingAvg";
            this.lblRatingAvg.Size = new System.Drawing.Size(150, 21);
            this.lblRatingAvg.TabIndex = 3;
            this.lblRatingAvg.Text = "Rating Rata-rata: -";
            // 
            // UlasanLapakControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.lblRatingAvg);
            this.Controls.Add(this.dgvUlasan);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "UlasanLapakControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.UlasanLapakControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUlasan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.DataGridView dgvUlasan;
        private System.Windows.Forms.Label lblRatingAvg;
    }
}

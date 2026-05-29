namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class DashboardAdminControl
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStatUsers = new System.Windows.Forms.Panel();
            this.lblValueUsers = new System.Windows.Forms.Label();
            this.lblTitleUsers = new System.Windows.Forms.Label();
            this.pnlStatShops = new System.Windows.Forms.Panel();
            this.lblValueShops = new System.Windows.Forms.Label();
            this.lblTitleShops = new System.Windows.Forms.Label();
            this.pnlStatComplaints = new System.Windows.Forms.Panel();
            this.lblValueComplaints = new System.Windows.Forms.Label();
            this.lblTitleComplaints = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvRecentActivity = new System.Windows.Forms.DataGridView();
            this.flpStats.SuspendLayout();
            this.pnlStatUsers.SuspendLayout();
            this.pnlStatShops.SuspendLayout();
            this.pnlStatComplaints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActivity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard Admin";
            // 
            // flpStats
            // 
            this.flpStats.Controls.Add(this.pnlStatUsers);
            this.flpStats.Controls.Add(this.pnlStatShops);
            this.flpStats.Controls.Add(this.pnlStatComplaints);
            this.flpStats.Location = new System.Drawing.Point(36, 80);
            this.flpStats.Name = "flpStats";
            this.flpStats.Size = new System.Drawing.Size(900, 130);
            this.flpStats.TabIndex = 1;
            // 
            // pnlStatUsers
            // 
            this.pnlStatUsers.BackColor = System.Drawing.Color.White;
            this.pnlStatUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatUsers.Controls.Add(this.lblValueUsers);
            this.pnlStatUsers.Controls.Add(this.lblTitleUsers);
            this.pnlStatUsers.Location = new System.Drawing.Point(3, 3);
            this.pnlStatUsers.Name = "pnlStatUsers";
            this.pnlStatUsers.Size = new System.Drawing.Size(250, 110);
            this.pnlStatUsers.TabIndex = 0;
            // 
            // lblValueUsers
            // 
            this.lblValueUsers.AutoSize = true;
            this.lblValueUsers.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueUsers.Location = new System.Drawing.Point(15, 45);
            this.lblValueUsers.Name = "lblValueUsers";
            this.lblValueUsers.Size = new System.Drawing.Size(38, 45);
            this.lblValueUsers.TabIndex = 1;
            this.lblValueUsers.Text = "0";
            // 
            // lblTitleUsers
            // 
            this.lblTitleUsers.AutoSize = true;
            this.lblTitleUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleUsers.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleUsers.Location = new System.Drawing.Point(15, 15);
            this.lblTitleUsers.Name = "lblTitleUsers";
            this.lblTitleUsers.Size = new System.Drawing.Size(108, 19);
            this.lblTitleUsers.TabIndex = 0;
            this.lblTitleUsers.Text = "Total Pengguna";
            // 
            // pnlStatShops
            // 
            this.pnlStatShops.BackColor = System.Drawing.Color.White;
            this.pnlStatShops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatShops.Controls.Add(this.lblValueShops);
            this.pnlStatShops.Controls.Add(this.lblTitleShops);
            this.pnlStatShops.Location = new System.Drawing.Point(276, 3);
            this.pnlStatShops.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.pnlStatShops.Name = "pnlStatShops";
            this.pnlStatShops.Size = new System.Drawing.Size(250, 110);
            this.pnlStatShops.TabIndex = 1;
            // 
            // lblValueShops
            // 
            this.lblValueShops.AutoSize = true;
            this.lblValueShops.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueShops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueShops.Location = new System.Drawing.Point(15, 45);
            this.lblValueShops.Name = "lblValueShops";
            this.lblValueShops.Size = new System.Drawing.Size(38, 45);
            this.lblValueShops.TabIndex = 1;
            this.lblValueShops.Text = "0";
            // 
            // lblTitleShops
            // 
            this.lblTitleShops.AutoSize = true;
            this.lblTitleShops.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleShops.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleShops.Location = new System.Drawing.Point(15, 15);
            this.lblTitleShops.Name = "lblTitleShops";
            this.lblTitleShops.Size = new System.Drawing.Size(127, 19);
            this.lblTitleShops.TabIndex = 0;
            this.lblTitleShops.Text = "Menunggu Verifikasi";
            // 
            // pnlStatComplaints
            // 
            this.pnlStatComplaints.BackColor = System.Drawing.Color.White;
            this.pnlStatComplaints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatComplaints.Controls.Add(this.lblValueComplaints);
            this.pnlStatComplaints.Controls.Add(this.lblTitleComplaints);
            this.pnlStatComplaints.Location = new System.Drawing.Point(549, 3);
            this.pnlStatComplaints.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.pnlStatComplaints.Name = "pnlStatComplaints";
            this.pnlStatComplaints.Size = new System.Drawing.Size(250, 110);
            this.pnlStatComplaints.TabIndex = 2;
            // 
            // lblValueComplaints
            // 
            this.lblValueComplaints.AutoSize = true;
            this.lblValueComplaints.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueComplaints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueComplaints.Location = new System.Drawing.Point(15, 45);
            this.lblValueComplaints.Name = "lblValueComplaints";
            this.lblValueComplaints.Size = new System.Drawing.Size(38, 45);
            this.lblValueComplaints.TabIndex = 1;
            this.lblValueComplaints.Text = "0";
            // 
            // lblTitleComplaints
            // 
            this.lblTitleComplaints.AutoSize = true;
            this.lblTitleComplaints.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleComplaints.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleComplaints.Location = new System.Drawing.Point(15, 15);
            this.lblTitleComplaints.Name = "lblTitleComplaints";
            this.lblTitleComplaints.Size = new System.Drawing.Size(125, 19);
            this.lblTitleComplaints.TabIndex = 0;
            this.lblTitleComplaints.Text = "Aduan Belum Selesai";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblSubtitle.Location = new System.Drawing.Point(32, 230);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(217, 21);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Aktivitas Terkini (Log Sistem)";
            // 
            // dgvRecentActivity
            // 
            this.dgvRecentActivity.AllowUserToAddRows = false;
            this.dgvRecentActivity.AllowUserToDeleteRows = false;
            this.dgvRecentActivity.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentActivity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentActivity.ColumnHeadersHeight = 35;
            this.dgvRecentActivity.EnableHeadersVisualStyles = false;
            this.dgvRecentActivity.Location = new System.Drawing.Point(36, 265);
            this.dgvRecentActivity.Name = "dgvRecentActivity";
            this.dgvRecentActivity.ReadOnly = true;
            this.dgvRecentActivity.RowHeadersVisible = false;
            this.dgvRecentActivity.RowTemplate.Height = 30;
            this.dgvRecentActivity.Size = new System.Drawing.Size(900, 300);
            this.dgvRecentActivity.TabIndex = 3;
            // 
            // DashboardAdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvRecentActivity);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.flpStats);
            this.Controls.Add(this.lblTitle);
            this.Name = "DashboardAdminControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.DashboardAdminControl_Load);
            this.flpStats.ResumeLayout(false);
            this.pnlStatUsers.ResumeLayout(false);
            this.pnlStatUsers.PerformLayout();
            this.pnlStatShops.ResumeLayout(false);
            this.pnlStatShops.PerformLayout();
            this.pnlStatComplaints.ResumeLayout(false);
            this.pnlStatComplaints.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActivity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpStats;
        private System.Windows.Forms.Panel pnlStatUsers;
        private System.Windows.Forms.Label lblValueUsers;
        private System.Windows.Forms.Label lblTitleUsers;
        private System.Windows.Forms.Panel pnlStatShops;
        private System.Windows.Forms.Label lblValueShops;
        private System.Windows.Forms.Label lblTitleShops;
        private System.Windows.Forms.Panel pnlStatComplaints;
        private System.Windows.Forms.Label lblValueComplaints;
        private System.Windows.Forms.Label lblTitleComplaints;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.DataGridView dgvRecentActivity;
    }
}

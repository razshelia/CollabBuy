namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class DashboardUserControl
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.flpStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStatActiveOrders = new System.Windows.Forms.Panel();
            this.lblValueActiveOrders = new System.Windows.Forms.Label();
            this.lblTitleActiveOrders = new System.Windows.Forms.Label();
            this.pnlStatJoinedPO = new System.Windows.Forms.Panel();
            this.lblValueJoinedPO = new System.Windows.Forms.Label();
            this.lblTitleJoinedPO = new System.Windows.Forms.Label();
            this.pnlStatShopStatus = new System.Windows.Forms.Panel();
            this.lblValueShopStatus = new System.Windows.Forms.Label();
            this.lblTitleShopStatus = new System.Windows.Forms.Label();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.dgvActivePO = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.flpStats.SuspendLayout();
            this.pnlStatActiveOrders.SuspendLayout();
            this.pnlStatJoinedPO.SuspendLayout();
            this.pnlStatShopStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivePO)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblWelcome.Location = new System.Drawing.Point(30, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(201, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Halo, Pengguna!";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(393, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Pantau aktivitas belanja dan status verifikasi lapak jualan Anda.";
            // 
            // flpStats
            // 
            this.flpStats.Controls.Add(this.pnlStatActiveOrders);
            this.flpStats.Controls.Add(this.pnlStatJoinedPO);
            this.flpStats.Controls.Add(this.pnlStatShopStatus);
            this.flpStats.Location = new System.Drawing.Point(36, 100);
            this.flpStats.Name = "flpStats";
            this.flpStats.Size = new System.Drawing.Size(900, 130);
            this.flpStats.TabIndex = 2;
            // 
            // pnlStatActiveOrders
            // 
            this.pnlStatActiveOrders.BackColor = System.Drawing.Color.White;
            this.pnlStatActiveOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatActiveOrders.Controls.Add(this.lblValueActiveOrders);
            this.pnlStatActiveOrders.Controls.Add(this.lblTitleActiveOrders);
            this.pnlStatActiveOrders.Location = new System.Drawing.Point(3, 3);
            this.pnlStatActiveOrders.Name = "pnlStatActiveOrders";
            this.pnlStatActiveOrders.Size = new System.Drawing.Size(250, 110);
            this.pnlStatActiveOrders.TabIndex = 0;
            // 
            // lblValueActiveOrders
            // 
            this.lblValueActiveOrders.AutoSize = true;
            this.lblValueActiveOrders.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueActiveOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueActiveOrders.Location = new System.Drawing.Point(15, 45);
            this.lblValueActiveOrders.Name = "lblValueActiveOrders";
            this.lblValueActiveOrders.Size = new System.Drawing.Size(38, 45);
            this.lblValueActiveOrders.TabIndex = 1;
            this.lblValueActiveOrders.Text = "0";
            // 
            // lblTitleActiveOrders
            // 
            this.lblTitleActiveOrders.AutoSize = true;
            this.lblTitleActiveOrders.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleActiveOrders.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleActiveOrders.Location = new System.Drawing.Point(15, 15);
            this.lblTitleActiveOrders.Name = "lblTitleActiveOrders";
            this.lblTitleActiveOrders.Size = new System.Drawing.Size(95, 19);
            this.lblTitleActiveOrders.TabIndex = 0;
            this.lblTitleActiveOrders.Text = "Pesanan Aktif";
            // 
            // pnlStatJoinedPO
            // 
            this.pnlStatJoinedPO.BackColor = System.Drawing.Color.White;
            this.pnlStatJoinedPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatJoinedPO.Controls.Add(this.lblValueJoinedPO);
            this.pnlStatJoinedPO.Controls.Add(this.lblTitleJoinedPO);
            this.pnlStatJoinedPO.Location = new System.Drawing.Point(276, 3);
            this.pnlStatJoinedPO.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.pnlStatJoinedPO.Name = "pnlStatJoinedPO";
            this.pnlStatJoinedPO.Size = new System.Drawing.Size(250, 110);
            this.pnlStatJoinedPO.TabIndex = 1;
            // 
            // lblValueJoinedPO
            // 
            this.lblValueJoinedPO.AutoSize = true;
            this.lblValueJoinedPO.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueJoinedPO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueJoinedPO.Location = new System.Drawing.Point(15, 45);
            this.lblValueJoinedPO.Name = "lblValueJoinedPO";
            this.lblValueJoinedPO.Size = new System.Drawing.Size(38, 45);
            this.lblValueJoinedPO.TabIndex = 1;
            this.lblValueJoinedPO.Text = "0";
            // 
            // lblTitleJoinedPO
            // 
            this.lblTitleJoinedPO.AutoSize = true;
            this.lblTitleJoinedPO.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleJoinedPO.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleJoinedPO.Location = new System.Drawing.Point(15, 15);
            this.lblTitleJoinedPO.Name = "lblTitleJoinedPO";
            this.lblTitleJoinedPO.Size = new System.Drawing.Size(107, 19);
            this.lblTitleJoinedPO.TabIndex = 0;
            this.lblTitleJoinedPO.Text = "Sesi PO Diikuti";
            // 
            // pnlStatShopStatus
            // 
            this.pnlStatShopStatus.BackColor = System.Drawing.Color.White;
            this.pnlStatShopStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatShopStatus.Controls.Add(this.lblValueShopStatus);
            this.pnlStatShopStatus.Controls.Add(this.lblTitleShopStatus);
            this.pnlStatShopStatus.Location = new System.Drawing.Point(549, 3);
            this.pnlStatShopStatus.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.pnlStatShopStatus.Name = "pnlStatShopStatus";
            this.pnlStatShopStatus.Size = new System.Drawing.Size(250, 110);
            this.pnlStatShopStatus.TabIndex = 2;
            // 
            // lblValueShopStatus
            // 
            this.lblValueShopStatus.AutoSize = true;
            this.lblValueShopStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueShopStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.lblValueShopStatus.Location = new System.Drawing.Point(15, 55);
            this.lblValueShopStatus.Name = "lblValueShopStatus";
            this.lblValueShopStatus.Size = new System.Drawing.Size(175, 25);
            this.lblValueShopStatus.TabIndex = 1;
            this.lblValueShopStatus.Text = "Belum Terverifikasi";
            // 
            // lblTitleShopStatus
            // 
            this.lblTitleShopStatus.AutoSize = true;
            this.lblTitleShopStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleShopStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleShopStatus.Location = new System.Drawing.Point(15, 15);
            this.lblTitleShopStatus.Name = "lblTitleShopStatus";
            this.lblTitleShopStatus.Size = new System.Drawing.Size(127, 19);
            this.lblTitleShopStatus.TabIndex = 0;
            this.lblTitleShopStatus.Text = "Status Toko/Lapak";
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblGridTitle.Location = new System.Drawing.Point(32, 250);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(236, 21);
            this.lblGridTitle.TabIndex = 3;
            this.lblGridTitle.Text = "Sesi Pre-Order / Danus Aktif";
            // 
            // dgvActivePO
            // 
            this.dgvActivePO.AllowUserToAddRows = false;
            this.dgvActivePO.AllowUserToDeleteRows = false;
            this.dgvActivePO.BackgroundColor = System.Drawing.Color.White;
            this.dgvActivePO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActivePO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActivePO.ColumnHeadersHeight = 35;
            this.dgvActivePO.EnableHeadersVisualStyles = false;
            this.dgvActivePO.Location = new System.Drawing.Point(36, 285);
            this.dgvActivePO.Name = "dgvActivePO";
            this.dgvActivePO.ReadOnly = true;
            this.dgvActivePO.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvActivePO.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvActivePO.RowTemplate.Height = 30;
            this.dgvActivePO.Size = new System.Drawing.Size(900, 320);
            this.dgvActivePO.TabIndex = 4;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnRefresh.Location = new System.Drawing.Point(816, 243);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DashboardUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvActivePO);
            this.Controls.Add(this.lblGridTitle);
            this.Controls.Add(this.flpStats);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblWelcome);
            this.Name = "DashboardUserControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.DashboardUserControl_Load);
            this.flpStats.ResumeLayout(false);
            this.pnlStatActiveOrders.ResumeLayout(false);
            this.pnlStatActiveOrders.PerformLayout();
            this.pnlStatJoinedPO.ResumeLayout(false);
            this.pnlStatJoinedPO.PerformLayout();
            this.pnlStatShopStatus.ResumeLayout(false);
            this.pnlStatShopStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivePO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flpStats;
        private System.Windows.Forms.Panel pnlStatActiveOrders;
        private System.Windows.Forms.Label lblValueActiveOrders;
        private System.Windows.Forms.Label lblTitleActiveOrders;
        private System.Windows.Forms.Panel pnlStatJoinedPO;
        private System.Windows.Forms.Label lblValueJoinedPO;
        private System.Windows.Forms.Label lblTitleJoinedPO;
        private System.Windows.Forms.Panel pnlStatShopStatus;
        private System.Windows.Forms.Label lblValueShopStatus;
        private System.Windows.Forms.Label lblTitleShopStatus;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvActivePO;
        private System.Windows.Forms.Button btnRefresh;
    }
}

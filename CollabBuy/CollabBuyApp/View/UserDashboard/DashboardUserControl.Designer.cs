namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    partial class DashboardUserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlStatsCard1 = new System.Windows.Forms.Panel();
            this.lblValueShopStatus = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.pnlStatsCard2 = new System.Windows.Forms.Panel();
            this.lblValueActiveOrders = new System.Windows.Forms.Label();
            this.lblStatsTitle = new System.Windows.Forms.Label();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.dgvActivePO = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlStatsCard1.SuspendLayout();
            this.pnlStatsCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivePO)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblWelcome.Location = new System.Drawing.Point(30, 25);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(262, 41);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Halo, Nama User!";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(33, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(394, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Cek katalog terbaru dan status pesanan kamu di sini bestie~";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1020, 120);
            this.pnlHeader.TabIndex = 2;
            // 
            // pnlStatsCard1
            // 
            this.pnlStatsCard1.BackColor = System.Drawing.Color.White;
            this.pnlStatsCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatsCard1.Controls.Add(this.lblValueShopStatus);
            this.pnlStatsCard1.Controls.Add(this.lblStatusTitle);
            this.pnlStatsCard1.Location = new System.Drawing.Point(37, 150);
            this.pnlStatsCard1.Name = "pnlStatsCard1";
            this.pnlStatsCard1.Size = new System.Drawing.Size(280, 110);
            this.pnlStatsCard1.TabIndex = 3;
            // 
            // lblValueShopStatus
            // 
            this.lblValueShopStatus.AutoSize = true;
            this.lblValueShopStatus.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblValueShopStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueShopStatus.Location = new System.Drawing.Point(15, 55);
            this.lblValueShopStatus.Name = "lblValueShopStatus";
            this.lblValueShopStatus.Size = new System.Drawing.Size(225, 30);
            this.lblValueShopStatus.TabIndex = 1;
            this.lblValueShopStatus.Text = "🔒 Terkunci (Buyer)";
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusTitle.Location = new System.Drawing.Point(16, 20);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(125, 19);
            this.lblStatusTitle.TabIndex = 0;
            this.lblStatusTitle.Text = "STATUS LAPAK 🏪";
            // 
            // pnlStatsCard2
            // 
            this.pnlStatsCard2.BackColor = System.Drawing.Color.White;
            this.pnlStatsCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatsCard2.Controls.Add(this.lblValueActiveOrders);
            this.pnlStatsCard2.Controls.Add(this.lblStatsTitle);
            this.pnlStatsCard2.Location = new System.Drawing.Point(340, 150);
            this.pnlStatsCard2.Name = "pnlStatsCard2";
            this.pnlStatsCard2.Size = new System.Drawing.Size(280, 110);
            this.pnlStatsCard2.TabIndex = 4;
            // 
            // lblValueActiveOrders
            // 
            this.lblValueActiveOrders.AutoSize = true;
            this.lblValueActiveOrders.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblValueActiveOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblValueActiveOrders.Location = new System.Drawing.Point(12, 45);
            this.lblValueActiveOrders.Name = "lblValueActiveOrders";
            this.lblValueActiveOrders.Size = new System.Drawing.Size(38, 45);
            this.lblValueActiveOrders.TabIndex = 1;
            this.lblValueActiveOrders.Text = "0";
            // 
            // lblStatsTitle
            // 
            this.lblStatsTitle.AutoSize = true;
            this.lblStatsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatsTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatsTitle.Location = new System.Drawing.Point(16, 20);
            this.lblStatsTitle.Name = "lblStatsTitle";
            this.lblStatsTitle.Size = new System.Drawing.Size(242, 19);
            this.lblStatsTitle.TabIndex = 0;
            this.lblStatsTitle.Text = "PAKET YANG LAGI DITUNGGU 📦🏃";
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblGridTitle.Location = new System.Drawing.Point(32, 290);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(332, 25);
            this.lblGridTitle.TabIndex = 5;
            this.lblGridTitle.Text = "Katalog PO yang Lagi Rame Nih 🔥";
            // 
            // dgvActivePO
            // 
            this.dgvActivePO.AllowUserToAddRows = false;
            this.dgvActivePO.AllowUserToDeleteRows = false;
            this.dgvActivePO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivePO.BackgroundColor = System.Drawing.Color.White;
            this.dgvActivePO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvActivePO.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvActivePO.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActivePO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActivePO.ColumnHeadersHeight = 40;
            this.dgvActivePO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvActivePO.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvActivePO.EnableHeadersVisualStyles = false;
            this.dgvActivePO.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvActivePO.Location = new System.Drawing.Point(37, 335);
            this.dgvActivePO.Name = "dgvActivePO";
            this.dgvActivePO.ReadOnly = true;
            this.dgvActivePO.RowHeadersVisible = false;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.dgvActivePO.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvActivePO.RowTemplate.Height = 40;
            this.dgvActivePO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivePO.Size = new System.Drawing.Size(940, 350);
            this.dgvActivePO.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(827, 285);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 35);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh Dulu Ngab 🔄";
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
            this.Controls.Add(this.pnlStatsCard2);
            this.Controls.Add(this.pnlStatsCard1);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DashboardUserControl";
            this.Size = new System.Drawing.Size(1020, 720);
            this.Load += new System.EventHandler(this.DashboardUserControl_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlStatsCard1.ResumeLayout(false);
            this.pnlStatsCard1.PerformLayout();
            this.pnlStatsCard2.ResumeLayout(false);
            this.pnlStatsCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivePO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlStatsCard1;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblValueShopStatus;
        private System.Windows.Forms.Panel pnlStatsCard2;
        private System.Windows.Forms.Label lblValueActiveOrders;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvActivePO;
        private System.Windows.Forms.Button btnRefresh;
    }
}
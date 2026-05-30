namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class DashboardAdminControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSapaan = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lblTitleUser = new System.Windows.Forms.Label();
            this.lblTotalUser = new System.Windows.Forms.Label();
            this.pnlTrx = new System.Windows.Forms.Panel();
            this.lblTitleTrx = new System.Windows.Forms.Label();
            this.lblTotalTrx = new System.Windows.Forms.Label();
            this.pnlPO = new System.Windows.Forms.Panel();
            this.lblTitlePO = new System.Windows.Forms.Label();
            this.lblTotalPO = new System.Windows.Forms.Label();
            this.pnlAduan = new System.Windows.Forms.Panel();
            this.lblTitleAduan = new System.Windows.Forms.Label();
            this.lblAduan = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlUser.SuspendLayout();
            this.pnlTrx.SuspendLayout();
            this.pnlPO.SuspendLayout();
            this.pnlAduan.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSapaan
            // 
            this.lblSapaan.AutoSize = true;
            this.lblSapaan.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblSapaan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblSapaan.Location = new System.Drawing.Point(30, 30);
            this.lblSapaan.Name = "lblSapaan";
            this.lblSapaan.Size = new System.Drawing.Size(200, 41);
            this.lblSapaan.TabIndex = 0;
            this.lblSapaan.Text = "Hola Mimin!";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(24)))), ((int)(((byte)(154)))));
            this.lblSubtitle.Location = new System.Drawing.Point(35, 75);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(364, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Pantau terus aktivitas CollabBuy hari ini biar aman!";
            // 
            // pnlUser (Warna Soft Yellow)
            // 
            this.pnlUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.pnlUser.Controls.Add(this.lblTotalUser);
            this.pnlUser.Controls.Add(this.lblTitleUser);
            this.pnlUser.Location = new System.Drawing.Point(35, 125);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(200, 120);
            this.pnlUser.TabIndex = 2;
            // 
            // lblTitleUser
            // 
            this.lblTitleUser.AutoSize = true;
            this.lblTitleUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitleUser.Location = new System.Drawing.Point(15, 15);
            this.lblTitleUser.Name = "lblTitleUser";
            this.lblTitleUser.Text = "👥 User Terdaftar";
            // 
            // lblTotalUser
            // 
            this.lblTotalUser.AutoSize = true;
            this.lblTotalUser.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTotalUser.Location = new System.Drawing.Point(15, 50);
            this.lblTotalUser.Text = "0";
            // 
            // pnlTrx (Warna Soft Cyan)
            // 
            this.pnlTrx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlTrx.Controls.Add(this.lblTotalTrx);
            this.pnlTrx.Controls.Add(this.lblTitleTrx);
            this.pnlTrx.Location = new System.Drawing.Point(255, 125);
            this.pnlTrx.Size = new System.Drawing.Size(200, 120);
            this.pnlTrx.TabIndex = 3;
            // 
            // lblTitleTrx
            // 
            this.lblTitleTrx.AutoSize = true;
            this.lblTitleTrx.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleTrx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitleTrx.Location = new System.Drawing.Point(15, 15);
            this.lblTitleTrx.Text = "💸 Total Trx Jajan";
            // 
            // lblTotalTrx
            // 
            this.lblTotalTrx.AutoSize = true;
            this.lblTotalTrx.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalTrx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTotalTrx.Location = new System.Drawing.Point(15, 50);
            this.lblTotalTrx.Text = "0";
            // 
            // pnlPO (Warna Soft Purple)
            // 
            this.pnlPO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.pnlPO.Controls.Add(this.lblTotalPO);
            this.pnlPO.Controls.Add(this.lblTitlePO);
            this.pnlPO.Location = new System.Drawing.Point(475, 125);
            this.pnlPO.Size = new System.Drawing.Size(200, 120);
            this.pnlPO.TabIndex = 4;
            // 
            // lblTitlePO
            // 
            this.lblTitlePO.AutoSize = true;
            this.lblTitlePO.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitlePO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitlePO.Location = new System.Drawing.Point(15, 15);
            this.lblTitlePO.Text = "🛒 PO Yang Hype";
            // 
            // lblTotalPO
            // 
            this.lblTotalPO.AutoSize = true;
            this.lblTotalPO.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalPO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTotalPO.Location = new System.Drawing.Point(15, 50);
            this.lblTotalPO.Text = "0";
            // 
            // pnlAduan (Warna Soft Red)
            // 
            this.pnlAduan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            this.pnlAduan.Controls.Add(this.lblAduan);
            this.pnlAduan.Controls.Add(this.lblTitleAduan);
            this.pnlAduan.Location = new System.Drawing.Point(695, 125);
            this.pnlAduan.Size = new System.Drawing.Size(200, 120);
            this.pnlAduan.TabIndex = 5;
            // 
            // lblTitleAduan
            // 
            this.lblTitleAduan.AutoSize = true;
            this.lblTitleAduan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleAduan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitleAduan.Location = new System.Drawing.Point(15, 15);
            this.lblTitleAduan.Text = "🚨 Curhatan Aktif";
            // 
            // lblAduan
            // 
            this.lblAduan.AutoSize = true;
            this.lblAduan.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblAduan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblAduan.Location = new System.Drawing.Point(15, 50);
            this.lblAduan.Text = "0";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnRefresh.Location = new System.Drawing.Point(35, 270);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 45);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "🔄 Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DashboardAdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.pnlAduan);
            this.Controls.Add(this.pnlPO);
            this.Controls.Add(this.pnlTrx);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblSapaan);
            this.Name = "DashboardAdminControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.DashboardAdminControl_Load);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlTrx.ResumeLayout(false);
            this.pnlTrx.PerformLayout();
            this.pnlPO.ResumeLayout(false);
            this.pnlPO.PerformLayout();
            this.pnlAduan.ResumeLayout(false);
            this.pnlAduan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblSapaan;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Label lblTitleUser;
        private System.Windows.Forms.Label lblTotalUser;
        private System.Windows.Forms.Panel pnlTrx;
        private System.Windows.Forms.Label lblTitleTrx;
        private System.Windows.Forms.Label lblTotalTrx;
        private System.Windows.Forms.Panel pnlPO;
        private System.Windows.Forms.Label lblTitlePO;
        private System.Windows.Forms.Label lblTotalPO;
        private System.Windows.Forms.Panel pnlAduan;
        private System.Windows.Forms.Label lblTitleAduan;
        private System.Windows.Forms.Label lblAduan;
        private System.Windows.Forms.Button btnRefresh;
    }
}
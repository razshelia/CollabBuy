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
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle headerStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

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
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.lblLeaderboardTitle = new System.Windows.Forms.Label();
            this.pnlLeaderboard = new System.Windows.Forms.Panel();
            this.dgvLeaderboard = new System.Windows.Forms.DataGridView();

            this.pnlUser.SuspendLayout();
            this.pnlTrx.SuspendLayout();
            this.pnlPO.SuspendLayout();
            this.pnlAduan.SuspendLayout();
            this.pnlLog.SuspendLayout();
            this.pnlLeaderboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaderboard)).BeginInit();
            this.SuspendLayout();

            // lblSapaan
            this.lblSapaan.AutoSize = true;
            this.lblSapaan.Font = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            this.lblSapaan.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblSapaan.Location = new System.Drawing.Point(30, 30);
            this.lblSapaan.Text = "Hola Mimin!";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(35, 75);
            this.lblSubtitle.Text = "Pantau terus aktivitas CollabBuy hari ini biar aman!";

            // pnlUser
            this.pnlUser.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.pnlUser.Controls.Add(this.lblTotalUser);
            this.pnlUser.Controls.Add(this.lblTitleUser);
            this.pnlUser.Location = new System.Drawing.Point(35, 125);
            this.pnlUser.Size = new System.Drawing.Size(200, 120);
            this.lblTitleUser.AutoSize = true;
            this.lblTitleUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleUser.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitleUser.Location = new System.Drawing.Point(15, 15);
            this.lblTitleUser.Text = "👥 User Terdaftar";
            this.lblTotalUser.AutoSize = true;
            this.lblTotalUser.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUser.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTotalUser.Location = new System.Drawing.Point(15, 50);
            this.lblTotalUser.Text = "0";

            // pnlTrx
            this.pnlTrx.BackColor = System.Drawing.Color.FromArgb(155, 246, 255);
            this.pnlTrx.Controls.Add(this.lblTotalTrx);
            this.pnlTrx.Controls.Add(this.lblTitleTrx);
            this.pnlTrx.Location = new System.Drawing.Point(255, 125);
            this.pnlTrx.Size = new System.Drawing.Size(200, 120);
            this.lblTitleTrx.AutoSize = true;
            this.lblTitleTrx.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleTrx.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitleTrx.Location = new System.Drawing.Point(15, 15);
            this.lblTitleTrx.Text = "💸 Total Trx Jajan";
            this.lblTotalTrx.AutoSize = true;
            this.lblTotalTrx.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalTrx.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTotalTrx.Location = new System.Drawing.Point(15, 50);
            this.lblTotalTrx.Text = "0";

            // pnlPO
            this.pnlPO.BackColor = System.Drawing.Color.FromArgb(224, 170, 255);
            this.pnlPO.Controls.Add(this.lblTotalPO);
            this.pnlPO.Controls.Add(this.lblTitlePO);
            this.pnlPO.Location = new System.Drawing.Point(475, 125);
            this.pnlPO.Size = new System.Drawing.Size(200, 120);
            this.lblTitlePO.AutoSize = true;
            this.lblTitlePO.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitlePO.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitlePO.Location = new System.Drawing.Point(15, 15);
            this.lblTitlePO.Text = "🛒 PO Yang Hype";
            this.lblTotalPO.AutoSize = true;
            this.lblTotalPO.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalPO.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTotalPO.Location = new System.Drawing.Point(15, 50);
            this.lblTotalPO.Text = "0";

            // pnlAduan
            this.pnlAduan.BackColor = System.Drawing.Color.FromArgb(255, 173, 173);
            this.pnlAduan.Controls.Add(this.lblAduan);
            this.pnlAduan.Controls.Add(this.lblTitleAduan);
            this.pnlAduan.Location = new System.Drawing.Point(695, 125);
            this.pnlAduan.Size = new System.Drawing.Size(200, 120);
            this.lblTitleAduan.AutoSize = true;
            this.lblTitleAduan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleAduan.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitleAduan.Location = new System.Drawing.Point(15, 15);
            this.lblTitleAduan.Text = "🚨 Curhatan Aktif";
            this.lblAduan.AutoSize = true;
            this.lblAduan.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblAduan.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblAduan.Location = new System.Drawing.Point(15, 50);
            this.lblAduan.Text = "0";

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Location = new System.Drawing.Point(820, 30);
            this.btnRefresh.Size = new System.Drawing.Size(150, 45);
            this.btnRefresh.Text = "🔄 Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // lblLogTitle
            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblLogTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblLogTitle.Location = new System.Drawing.Point(35, 270);
            this.lblLogTitle.Text = "📋 Aktivitas Terbaru";

            // pnlLog
            this.pnlLog.BackColor = System.Drawing.Color.White;
            this.pnlLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLog.Controls.Add(this.dgvLog);
            this.pnlLog.Location = new System.Drawing.Point(35, 300);
            this.pnlLog.Size = new System.Drawing.Size(455, 310);

            // dgvLog
            headerStyle.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;

            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.Color.White;
            this.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLog.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvLog.ColumnHeadersHeight = 35;
            this.dgvLog.DefaultCellStyle = rowStyle;
            this.dgvLog.EnableHeadersVisualStyles = false;
            this.dgvLog.Location = new System.Drawing.Point(2, 2);
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowTemplate.Height = 38;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(451, 306);

            // lblLeaderboardTitle
            this.lblLeaderboardTitle.AutoSize = true;
            this.lblLeaderboardTitle.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblLeaderboardTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblLeaderboardTitle.Location = new System.Drawing.Point(510, 270);
            this.lblLeaderboardTitle.Text = "🏆 Leaderboard Penjual";

            // pnlLeaderboard
            this.pnlLeaderboard.BackColor = System.Drawing.Color.White;
            this.pnlLeaderboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeaderboard.Controls.Add(this.dgvLeaderboard);
            this.pnlLeaderboard.Location = new System.Drawing.Point(510, 300);
            this.pnlLeaderboard.Size = new System.Drawing.Size(455, 310);

            // dgvLeaderboard
            headerStyle2.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            headerStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle2.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            headerStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            this.dgvLeaderboard.AllowUserToAddRows = false;
            this.dgvLeaderboard.AllowUserToDeleteRows = false;
            this.dgvLeaderboard.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeaderboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLeaderboard.ColumnHeadersDefaultCellStyle = headerStyle2;
            this.dgvLeaderboard.ColumnHeadersHeight = 35;
            this.dgvLeaderboard.EnableHeadersVisualStyles = false;
            this.dgvLeaderboard.Location = new System.Drawing.Point(2, 2);
            this.dgvLeaderboard.ReadOnly = true;
            this.dgvLeaderboard.RowHeadersVisible = false;
            this.dgvLeaderboard.RowTemplate.Height = 38;
            this.dgvLeaderboard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaderboard.Size = new System.Drawing.Size(451, 306);

            // DashboardAdminControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlLeaderboard);
            this.Controls.Add(this.lblLeaderboardTitle);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.lblLogTitle);
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
            this.pnlLog.ResumeLayout(false);
            this.pnlLeaderboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaderboard)).EndInit();
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
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Label lblLeaderboardTitle;
        private System.Windows.Forms.Panel pnlLeaderboard;
        private System.Windows.Forms.DataGridView dgvLeaderboard;
    }
}
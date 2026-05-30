namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class LogAktivitasControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnSemua = new System.Windows.Forms.Button();
            this.btnLoginLogout = new System.Windows.Forms.Button();
            this.btnPerubahan = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.dgvLog = new System.Windows.Forms.DataGridView();

            this.pnlCard.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Text = "📋 Log Aktivitas Sistem";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Text = "Pantau semua aktivitas pengguna di sistem CollabBuy";

            // pnlCard
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.pnlFilter);
            this.pnlCard.Controls.Add(this.lblJumlah);
            this.pnlCard.Controls.Add(this.btnRefresh);
            this.pnlCard.Controls.Add(this.dgvLog);
            this.pnlCard.Location = new System.Drawing.Point(36, 110);
            this.pnlCard.Size = new System.Drawing.Size(920, 500);

            // pnlFilter (tombol filter di atas)
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(245, 240, 255);
            this.pnlFilter.Controls.Add(this.btnSemua);
            this.pnlFilter.Controls.Add(this.btnLoginLogout);
            this.pnlFilter.Controls.Add(this.btnPerubahan);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 55;
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 5);

            // btnSemua
            this.btnSemua.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnSemua.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnSemua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSemua.FlatAppearance.BorderSize = 0;
            this.btnSemua.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.btnSemua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSemua.Location = new System.Drawing.Point(10, 10);
            this.btnSemua.Size = new System.Drawing.Size(130, 32);
            this.btnSemua.Text = "📋 Semua Aktivitas";
            this.btnSemua.Click += new System.EventHandler(this.btnSemua_Click);

            // btnLoginLogout
            this.btnLoginLogout.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            this.btnLoginLogout.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnLoginLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginLogout.FlatAppearance.BorderSize = 0;
            this.btnLoginLogout.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoginLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginLogout.Location = new System.Drawing.Point(150, 10);
            this.btnLoginLogout.Size = new System.Drawing.Size(140, 32);
            this.btnLoginLogout.Text = "🔑 Login / Logout";
            this.btnLoginLogout.Click += new System.EventHandler(this.btnLoginLogout_Click);

            // btnPerubahan
            this.btnPerubahan.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            this.btnPerubahan.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnPerubahan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerubahan.FlatAppearance.BorderSize = 0;
            this.btnPerubahan.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.btnPerubahan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPerubahan.Location = new System.Drawing.Point(300, 10);
            this.btnPerubahan.Size = new System.Drawing.Size(150, 32);
            this.btnPerubahan.Text = "✏️ Perubahan Data";
            this.btnPerubahan.Click += new System.EventHandler(this.btnPerubahan_Click);

            // lblJumlah
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblJumlah.ForeColor = System.Drawing.Color.Gray;
            this.lblJumlah.Location = new System.Drawing.Point(24, 65);
            this.lblJumlah.Text = "Menampilkan 0 aktivitas";

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Location = new System.Drawing.Point(744, 60);
            this.btnRefresh.Size = new System.Drawing.Size(150, 32);
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvLog
            headerStyle.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;
            rowStyle.Padding = new System.Windows.Forms.Padding(5);

            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.Color.White;
            this.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLog.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvLog.ColumnHeadersHeight = 40;
            this.dgvLog.DefaultCellStyle = rowStyle;
            this.dgvLog.EnableHeadersVisualStyles = false;
            this.dgvLog.Location = new System.Drawing.Point(24, 105);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowTemplate.Height = 40;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(870, 370);

            // Main control
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "LogAktivitasControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.LogAktivitasControl_Load);

            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblJumlah;
        private System.Windows.Forms.Panel pnlCard, pnlFilter;
        private System.Windows.Forms.Button btnSemua, btnLoginLogout, btnPerubahan, btnRefresh;
        private System.Windows.Forms.DataGridView dgvLog;
    }
}
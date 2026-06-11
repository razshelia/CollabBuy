namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class KelolaUserControl
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
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.lblNamaLabel = new System.Windows.Forms.Label();
            this.lblDetailNama = new System.Windows.Forms.Label();
            this.lblUsernameLabel = new System.Windows.Forms.Label();
            this.lblDetailUsername = new System.Windows.Forms.Label();
            this.lblEmailLabel = new System.Windows.Forms.Label();
            this.lblDetailEmail = new System.Windows.Forms.Label();
            this.lblTeleponLabel = new System.Windows.Forms.Label();
            this.lblDetailTelepon = new System.Windows.Forms.Label();
            this.lblPeranLabel = new System.Windows.Forms.Label();
            this.lblDetailPeran = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblDetailStatus = new System.Windows.Forms.Label();
            this.btnBlokir = new System.Windows.Forms.Button();
            this.txtCariUser = new System.Windows.Forms.TextBox();

            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Text = "👥 Kelola User";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Text = "Pantau dan kelola semua akun pengguna CollabBuy";

            // --- txtCariUser ---
            this.txtCariUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCariUser.Location = new System.Drawing.Point(14, 14);
            this.txtCariUser.Size = new System.Drawing.Size(260, 28);
            this.txtCariUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCariUser.PlaceholderText = "🔍 Cari nama, username, kontak...";
            this.txtCariUser.Name = "txtCariUser";
            this.txtCariUser.TextChanged += new System.EventHandler(this.txtCariUser_TextChanged);

            // dgvUser
            headerStyle.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;

            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUser.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvUser.ColumnHeadersHeight = 45;
            this.dgvUser.DefaultCellStyle = rowStyle;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.Location = new System.Drawing.Point(34, 30);
            this.dgvUser.MultiSelect = false;
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.RowTemplate.Height = 40;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(510, 380);
            this.dgvUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellClick);

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Location = new System.Drawing.Point(360, 435);
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.Text = "🔄 Refresh Data";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // pnlCard
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.btnRefresh);
            this.pnlCard.Controls.Add(this.txtCariUser);
            this.pnlCard.Controls.Add(this.dgvUser);
            this.pnlCard.Location = new System.Drawing.Point(36, 110);
            this.pnlCard.Size = new System.Drawing.Size(580, 500);

            // ── Semua konten pnlDetail ──

            // lblDetailTitle
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailTitle.Location = new System.Drawing.Point(20, 18);
            this.lblDetailTitle.Text = "📋 Detail Profil User";

            // Setiap pasang label menggunakan jarak y yang konsisten
            // y label = y sebelumnya + 45 (22 tinggi value + 23 gap)
            int y = 55;
            int gap = 45;

            SetInfoLabel(this.lblNamaLabel, "Nama Lengkap", 20, y); y += 22;
            SetValueLabel(this.lblDetailNama, "Klik baris untuk lihat detail", 20, y); y += gap;

            SetInfoLabel(this.lblUsernameLabel, "Username", 20, y); y += 22;
            SetValueLabel(this.lblDetailUsername, "-", 20, y); y += gap;

            SetInfoLabel(this.lblEmailLabel, "Email", 20, y); y += 22;
            SetValueLabel(this.lblDetailEmail, "-", 20, y); y += gap;

            SetInfoLabel(this.lblTeleponLabel, "No. WhatsApp", 20, y); y += 22;
            SetValueLabel(this.lblDetailTelepon, "-", 20, y); y += gap;

            SetInfoLabel(this.lblPeranLabel, "Peran", 20, y); y += 22;
            SetValueLabel(this.lblDetailPeran, "-", 20, y); y += gap;

            SetInfoLabel(this.lblStatusLabel, "Status Akun", 20, y); y += 22;
            SetValueLabel(this.lblDetailStatus, "-", 20, y); y += gap + 10;

            // btnBlokir — letakkan setelah semua label, dengan y yang sudah dihitung
            this.btnBlokir.BackColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.btnBlokir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlokir.FlatAppearance.BorderSize = 0;
            this.btnBlokir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlokir.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnBlokir.ForeColor = System.Drawing.Color.FromArgb(140, 140, 140);
            this.btnBlokir.Location = new System.Drawing.Point(80, y);  // y dihitung otomatis
            this.btnBlokir.Size = new System.Drawing.Size(150, 38);
            this.btnBlokir.Text = "🚫 Blokir Akun";
            this.btnBlokir.Enabled = false;
            this.btnBlokir.Click += new System.EventHandler(this.btnBlokir_Click);

            // pnlDetail — AutoScrollMinSize diset lebih dari y + tinggi tombol
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(235, 204, 255);
            this.pnlDetail.AutoScroll = true;
            this.pnlDetail.AutoScrollMinSize = new System.Drawing.Size(0, y + 100);
            this.pnlDetail.Location = new System.Drawing.Point(636, 110);
            this.pnlDetail.Size = new System.Drawing.Size(320, 500);
            this.pnlDetail.Controls.Add(this.lblDetailTitle);
            this.pnlDetail.Controls.Add(this.lblNamaLabel);
            this.pnlDetail.Controls.Add(this.lblDetailNama);
            this.pnlDetail.Controls.Add(this.lblUsernameLabel);
            this.pnlDetail.Controls.Add(this.lblDetailUsername);
            this.pnlDetail.Controls.Add(this.lblEmailLabel);
            this.pnlDetail.Controls.Add(this.lblDetailEmail);
            this.pnlDetail.Controls.Add(this.lblTeleponLabel);
            this.pnlDetail.Controls.Add(this.lblDetailTelepon);
            this.pnlDetail.Controls.Add(this.lblPeranLabel);
            this.pnlDetail.Controls.Add(this.lblDetailPeran);
            this.pnlDetail.Controls.Add(this.lblStatusLabel);
            this.pnlDetail.Controls.Add(this.lblDetailStatus);
            this.pnlDetail.Controls.Add(this.btnBlokir);

            // Main control
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "KelolaUserControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.KelolaUserControl_Load);

            this.pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Helper supaya tidak mengulang kode yang sama
        private void SetInfoLabel(System.Windows.Forms.Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }

        private void SetValueLabel(System.Windows.Forms.Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = false;
            lbl.Width = 260;
            lbl.Height = 22;
            lbl.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }

        private System.Windows.Forms.Label lblTitle, lblSubtitle;
        private System.Windows.Forms.Panel pnlCard, pnlDetail;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Button btnRefresh, btnBlokir;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblNamaLabel, lblDetailNama;
        private System.Windows.Forms.Label lblUsernameLabel, lblDetailUsername;
        private System.Windows.Forms.Label lblEmailLabel, lblDetailEmail;
        private System.Windows.Forms.Label lblTeleponLabel, lblDetailTelepon;
        private System.Windows.Forms.Label lblPeranLabel, lblDetailPeran;
        private System.Windows.Forms.Label lblStatusLabel, lblDetailStatus;
        private System.Windows.Forms.TextBox txtCariUser;
    }
}
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
            this.pnlCard.Controls.Add(this.dgvUser);
            this.pnlCard.Location = new System.Drawing.Point(36, 110);
            this.pnlCard.Size = new System.Drawing.Size(580, 500);

            // ── pnlDetail: setup semua label DULU, baru Controls.Add ──

            // lblDetailTitle
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailTitle.Location = new System.Drawing.Point(20, 18);
            this.lblDetailTitle.Text = "📋 Detail Profil User";

            // Nama Lengkap
            this.lblNamaLabel.AutoSize = true;
            this.lblNamaLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNamaLabel.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblNamaLabel.Location = new System.Drawing.Point(20, 55);
            this.lblNamaLabel.Text = "Nama Lengkap";

            this.lblDetailNama.AutoSize = false;
            this.lblDetailNama.Width = 260;
            this.lblDetailNama.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailNama.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailNama.Location = new System.Drawing.Point(20, 73);
            this.lblDetailNama.Text = "Klik baris untuk lihat detail";

            // Username
            this.lblUsernameLabel.AutoSize = true;
            this.lblUsernameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsernameLabel.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblUsernameLabel.Location = new System.Drawing.Point(20, 118);
            this.lblUsernameLabel.Text = "Username";

            this.lblDetailUsername.AutoSize = false;
            this.lblDetailUsername.Width = 260;
            this.lblDetailUsername.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailUsername.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailUsername.Location = new System.Drawing.Point(20, 136);
            this.lblDetailUsername.Text = "-";

            // Email
            this.lblEmailLabel.AutoSize = true;
            this.lblEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmailLabel.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblEmailLabel.Location = new System.Drawing.Point(20, 181);
            this.lblEmailLabel.Text = "Email";

            this.lblDetailEmail.AutoSize = false;
            this.lblDetailEmail.Width = 260;
            this.lblDetailEmail.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailEmail.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailEmail.Location = new System.Drawing.Point(20, 199);
            this.lblDetailEmail.Text = "-";

            // No. WhatsApp
            this.lblTeleponLabel.AutoSize = true;
            this.lblTeleponLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTeleponLabel.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblTeleponLabel.Location = new System.Drawing.Point(20, 244);
            this.lblTeleponLabel.Text = "No. WhatsApp";

            this.lblDetailTelepon.AutoSize = false;
            this.lblDetailTelepon.Width = 260;
            this.lblDetailTelepon.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailTelepon.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailTelepon.Location = new System.Drawing.Point(20, 262);
            this.lblDetailTelepon.Text = "-";

            // Peran
            this.lblPeranLabel.AutoSize = true;
            this.lblPeranLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeranLabel.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblPeranLabel.Location = new System.Drawing.Point(20, 307);
            this.lblPeranLabel.Text = "Peran";

            this.lblDetailPeran.AutoSize = false;
            this.lblDetailPeran.Width = 260;
            this.lblDetailPeran.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailPeran.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailPeran.Location = new System.Drawing.Point(20, 325);
            this.lblDetailPeran.Text = "-";

            // Status Akun
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusLabel.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblStatusLabel.Location = new System.Drawing.Point(20, 370);
            this.lblStatusLabel.Text = "Status Akun";

            this.lblDetailStatus.AutoSize = false;
            this.lblDetailStatus.Width = 260;
            this.lblDetailStatus.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailStatus.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDetailStatus.Location = new System.Drawing.Point(20, 388);
            this.lblDetailStatus.Text = "-";

            // btnBlokir
            this.btnBlokir.BackColor = System.Drawing.Color.FromArgb(200, 0, 0);
            this.btnBlokir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlokir.FlatAppearance.BorderSize = 0;
            this.btnBlokir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlokir.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnBlokir.ForeColor = System.Drawing.Color.White;
            this.btnBlokir.Location = new System.Drawing.Point(20, 445);
            this.btnBlokir.Size = new System.Drawing.Size(280, 45);
            this.btnBlokir.Text = "🚫 Blokir Akun";
            this.btnBlokir.Enabled = false;
            this.btnBlokir.BackColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.btnBlokir.ForeColor = System.Drawing.Color.FromArgb(140, 140, 140);
            this.btnBlokir.Click += new System.EventHandler(this.btnBlokir_Click);

            // pnlDetail — Controls.Add SETELAH semua label sudah dikonfigurasi
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(235, 204, 255);
            this.pnlDetail.AutoScroll = true;
            this.pnlDetail.Location = new System.Drawing.Point(636, 110);
            this.pnlDetail.Size = new System.Drawing.Size(320, 510);
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
    }
}
namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class AdminDashboardControl
    {
        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblJudul  = new System.Windows.Forms.Label();
            lblSub    = new System.Windows.Forms.Label();
            pnlStats  = new System.Windows.Forms.Panel();
            cardUsers = new System.Windows.Forms.Panel();
            cardPO    = new System.Windows.Forms.Panel();
            cardDone  = new System.Windows.Forms.Panel();
            lblAdmin  = new System.Windows.Forms.Label();

            SuspendLayout();

            BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            Dock      = System.Windows.Forms.DockStyle.Fill;
            Name      = "AdminDashboardControl";

            // Header
            pnlHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Height    = 110;
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Name      = "pnlHeader";

            lblJudul.Text      = "Dashboard Admin 🛡️";
            lblJudul.Font      = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            lblJudul.AutoSize  = false;
            lblJudul.Size      = new System.Drawing.Size(700, 50);
            lblJudul.Location  = new System.Drawing.Point(30, 18);

            lblSub.Text      = "Pantau kondisi global perputaran dana usaha di kampus.";
            lblSub.Font      = new System.Drawing.Font("Segoe UI", 11F);
            lblSub.ForeColor = System.Drawing.Color.Gray;
            lblSub.AutoSize  = false;
            lblSub.Size      = new System.Drawing.Size(600, 26);
            lblSub.Location  = new System.Drawing.Point(32, 72);

            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(lblSub);

            // Stats row
            pnlStats.Dock      = System.Windows.Forms.DockStyle.Top;
            pnlStats.Height    = 140;
            pnlStats.BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            pnlStats.Padding   = new System.Windows.Forms.Padding(30, 20, 30, 0);
            pnlStats.Name      = "pnlStats";

            System.Windows.Forms.Panel MakeStatCard(string emoji, string title, string val,
                                                     System.Drawing.Color bg, int x)
            {
                var c = new System.Windows.Forms.Panel();
                c.BackColor   = bg;
                c.Size        = new System.Drawing.Size(220, 100);
                c.Location    = new System.Drawing.Point(x, 20);
                c.BorderStyle = System.Windows.Forms.BorderStyle.None;

                var emojiLbl = new System.Windows.Forms.Label();
                emojiLbl.Text      = emoji;
                emojiLbl.Font      = new System.Drawing.Font("Segoe UI", 22F);
                emojiLbl.Location  = new System.Drawing.Point(14, 12);
                emojiLbl.AutoSize  = true;
                c.Controls.Add(emojiLbl);

                var titleLbl = new System.Windows.Forms.Label();
                titleLbl.Text      = title;
                titleLbl.Font      = new System.Drawing.Font("Segoe UI", 9F);
                titleLbl.ForeColor = System.Drawing.Color.FromArgb(80, 80, 100);
                titleLbl.Location  = new System.Drawing.Point(60, 14);
                titleLbl.AutoSize  = true;
                c.Controls.Add(titleLbl);

                var valLbl = new System.Windows.Forms.Label();
                valLbl.Text      = val;
                valLbl.Font      = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
                valLbl.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
                valLbl.Location  = new System.Drawing.Point(58, 34);
                valLbl.AutoSize  = true;
                c.Controls.Add(valLbl);

                return c;
            }

            cardUsers = MakeStatCard("👥", "Total User",         "128", System.Drawing.Color.FromArgb(255, 235, 133), 0);
            cardPO    = MakeStatCard("📦", "PO Aktif",           "24",  System.Drawing.Color.FromArgb(200, 190, 240), 240);
            cardDone  = MakeStatCard("✅", "Transaksi Selesai",   "312", System.Drawing.Color.FromArgb(180, 230, 200), 480);

            pnlStats.Controls.Add(cardUsers);
            pnlStats.Controls.Add(cardPO);
            pnlStats.Controls.Add(cardDone);

            // Placeholder info
            lblAdmin.Text      = "Pilih menu di sidebar untuk mulai mengelola CollabBuy 👈";
            lblAdmin.Font      = new System.Drawing.Font("Segoe UI", 13F);
            lblAdmin.ForeColor = System.Drawing.Color.Silver;
            lblAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblAdmin.Dock      = System.Windows.Forms.DockStyle.Fill;
            lblAdmin.Name      = "lblAdmin";

            Controls.Add(lblAdmin);
            Controls.Add(pnlStats);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader, pnlStats;
        private System.Windows.Forms.Panel cardUsers, cardPO, cardDone;
        private System.Windows.Forms.Label lblJudul, lblSub, lblAdmin;
    }
}

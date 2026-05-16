namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class AdminDashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlReportContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(600, 50);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "DASHBOARD ADMIN 💼";
            // 
            // pnlCards
            // 
            this.pnlCards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCards.BackColor = System.Drawing.Color.Transparent;
            this.pnlCards.Location = new System.Drawing.Point(35, 80);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(980, 130);
            this.pnlCards.TabIndex = 1;
            // 
            // pnlReportContainer
            // 
            this.pnlReportContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReportContainer.BackColor = System.Drawing.Color.White;
            this.pnlReportContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReportContainer.Location = new System.Drawing.Point(35, 225);
            this.pnlReportContainer.Name = "pnlReportContainer";
            this.pnlReportContainer.Size = new System.Drawing.Size(980, 415);
            this.pnlReportContainer.TabIndex = 2;
            // 
            // AdminDashboardControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlReportContainer);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.lblWelcome);
            this.Name = "AdminDashboardControl";
            this.Size = new System.Drawing.Size(1046, 670);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.FlowLayoutPanel pnlCards;
        private System.Windows.Forms.Panel pnlReportContainer;

        // Referensi Label untuk diisi oleh metode LoadStats
        private System.Windows.Forms.Label lblTotalUser;
        private System.Windows.Forms.Label lblTotalProduk;
        private System.Windows.Forms.Label lblTotalTransaksi;
        private System.Windows.Forms.Label lblTotalAduan;
    }
}
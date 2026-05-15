namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class CheckoutHistoryControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblJudul = new Label();
            btnRefresh = new Button();
            flowPanelCheckout = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 27, 79);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1046, 80);
            pnlHeader.TabIndex = 1;
            // 
            // lblJudul
            // 
            lblJudul.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(253, 224, 71);
            lblJudul.Location = new Point(20, 20);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(500, 35);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "RIWAYAT CHECKOUT KAMU 🧾";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(167, 139, 250);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(600, 25);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // flowPanelCheckout
            // 
            flowPanelCheckout.AutoScroll = true;
            flowPanelCheckout.BackColor = Color.FromArgb(255, 249, 230);
            flowPanelCheckout.Dock = DockStyle.Fill;
            flowPanelCheckout.Location = new Point(0, 80);
            flowPanelCheckout.Name = "flowPanelCheckout";
            flowPanelCheckout.Padding = new Padding(10);
            flowPanelCheckout.Size = new Size(1046, 253);
            flowPanelCheckout.TabIndex = 0;
            // 
            // CheckoutHistoryControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelCheckout);
            Controls.Add(pnlHeader);
            Name = "CheckoutHistoryControl";
            Size = new Size(1046, 333);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flowPanelCheckout;
    }
}
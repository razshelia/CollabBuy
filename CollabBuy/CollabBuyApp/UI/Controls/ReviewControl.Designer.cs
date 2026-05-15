namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ReviewControl
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
            flowPanel = new FlowLayoutPanel();
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
            lblJudul.Size = new Size(400, 35);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "ULASAN PRODUK ⭐";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(167, 139, 250);
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
            // flowPanel
            // 
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.FromArgb(255, 249, 230);
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.Location = new Point(0, 80);
            flowPanel.Name = "flowPanel";
            flowPanel.Padding = new Padding(10);
            flowPanel.Size = new Size(1046, 253);
            flowPanel.TabIndex = 0;
            // 
            // ReviewControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanel);
            Controls.Add(pnlHeader);
            Name = "ReviewControl";
            Size = new Size(1046, 333);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
    }
}
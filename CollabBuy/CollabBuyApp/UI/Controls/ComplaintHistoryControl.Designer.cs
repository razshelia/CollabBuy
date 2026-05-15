namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ComplaintHistoryControl
    {
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;

        private void InitializeComponent()
        {
            flowPanel = new FlowLayoutPanel();
            pnlHeader = new Panel();
            lblJudul = new Label();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // flowPanel
            // 
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.FromArgb(255, 249, 230);
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.Location = new Point(0, 70);
            flowPanel.Name = "flowPanel";
            flowPanel.Size = new Size(1046, 206);
            flowPanel.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 27, 79);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1046, 70);
            pnlHeader.TabIndex = 1;
            // 
            // lblJudul
            // 
            lblJudul.Font = new Font("Segoe UI Black", 16F);
            lblJudul.ForeColor = Color.FromArgb(253, 224, 71);
            lblJudul.Location = new Point(20, 20);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(100, 23);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "RIWAYAT ADUAN 📩";
            // 
            // ComplaintHistoryControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanel);
            Controls.Add(pnlHeader);
            Name = "ComplaintHistoryControl";
            Size = new Size(1046, 276);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerPOListControl
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
            btnBuatPO = new Button();
            btnRefresh = new Button();
            flowPanelPO = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 27, 79);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(btnBuatPO);
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
            lblJudul.Size = new Size(300, 35);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "PREORDER KAMU 📦";
            // 
            // btnBuatPO
            // 
            btnBuatPO.BackColor = Color.FromArgb(167, 139, 250);
            btnBuatPO.FlatAppearance.BorderSize = 0;
            btnBuatPO.FlatStyle = FlatStyle.Flat;
            btnBuatPO.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBuatPO.ForeColor = Color.White;
            btnBuatPO.Location = new Point(400, 25);
            btnBuatPO.Name = "btnBuatPO";
            btnBuatPO.Size = new Size(130, 30);
            btnBuatPO.TabIndex = 1;
            btnBuatPO.Text = "➕ Buka PO Baru";
            btnBuatPO.UseVisualStyleBackColor = false;
            btnBuatPO.Click += btnBuatPO_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(167, 139, 250);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(550, 25);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // flowPanelPO
            // 
            flowPanelPO.AutoScroll = true;
            flowPanelPO.BackColor = Color.FromArgb(255, 249, 230);
            flowPanelPO.Dock = DockStyle.Fill;
            flowPanelPO.Location = new Point(0, 80);
            flowPanelPO.Name = "flowPanelPO";
            flowPanelPO.Padding = new Padding(10);
            flowPanelPO.Size = new Size(1046, 253);
            flowPanelPO.TabIndex = 0;
            // 
            // SellerPOListControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelPO);
            Controls.Add(pnlHeader);
            Name = "SellerPOListControl";
            Size = new Size(1046, 333);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnBuatPO, btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flowPanelPO;
    }
}
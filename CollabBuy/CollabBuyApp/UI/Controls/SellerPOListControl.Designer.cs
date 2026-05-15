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
            pnlHeader = new System.Windows.Forms.Panel();
            lblJudul = new System.Windows.Forms.Label();
            btnBuatPO = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            flowPanelPO = new System.Windows.Forms.FlowLayoutPanel();

            pnlHeader.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ─────────────────────────────────────
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(btnBuatPO);
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1046, 80);
            pnlHeader.TabIndex = 1;

            // ── lblJudul ──────────────────────────────────────
            lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(253, 224, 71);
            lblJudul.Location = new System.Drawing.Point(20, 20);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new System.Drawing.Size(350, 35);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "PREORDER KAMU 📦";

            // ── btnBuatPO ─────────────────────────────────────
            btnBuatPO.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            btnBuatPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBuatPO.FlatAppearance.BorderSize = 0;
            btnBuatPO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnBuatPO.ForeColor = System.Drawing.Color.White;
            btnBuatPO.Location = new System.Drawing.Point(750, 25);
            btnBuatPO.Name = "btnBuatPO";
            btnBuatPO.Size = new System.Drawing.Size(140, 30);
            btnBuatPO.TabIndex = 1;
            btnBuatPO.Text = "➕ Buka PO Baru";
            btnBuatPO.UseVisualStyleBackColor = false;
            btnBuatPO.Click += btnBuatPO_Click;

            // ── btnRefresh ────────────────────────────────────
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(900, 25);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(120, 30);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // ── flowPanelPO ───────────────────────────────────
            flowPanelPO.AutoScroll = true;
            flowPanelPO.BackColor = System.Drawing.Color.FromArgb(255, 249, 230);
            flowPanelPO.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanelPO.Name = "flowPanelPO";
            flowPanelPO.Padding = new System.Windows.Forms.Padding(10);
            flowPanelPO.TabIndex = 0;

            // ── UserControl root ──────────────────────────────
            BackColor = System.Drawing.Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelPO);
            Controls.Add(pnlHeader);
            Name = "SellerPOListControl";
            Size = new System.Drawing.Size(1046, 700);

            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnBuatPO;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flowPanelPO;
    }
}
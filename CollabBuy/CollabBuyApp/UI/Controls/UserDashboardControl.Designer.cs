// UserDashboardControl.Designer.cs
namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class UserDashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblGreeting = new Label();
            lblMotivasi = new Label();
            txtSearch = new TextBox();
            cmbKategori = new ComboBox();
            lblCount = new Label();
            flowPanelProduk = new FlowLayoutPanel();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(45, 27, 79);
            pnlTop.Controls.Add(lblGreeting);
            pnlTop.Controls.Add(lblMotivasi);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(cmbKategori);
            pnlTop.Controls.Add(lblCount);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1046, 130);
            pnlTop.TabIndex = 1;
            // 
            // lblGreeting
            // 
            lblGreeting.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            lblGreeting.ForeColor = Color.FromArgb(253, 224, 71);
            lblGreeting.Location = new Point(20, 15);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(400, 35);
            lblGreeting.TabIndex = 0;
            lblGreeting.Text = "Halo, Bestie! 👋";
            // 
            // lblMotivasi
            // 
            lblMotivasi.Font = new Font("Segoe UI", 10F);
            lblMotivasi.ForeColor = Color.FromArgb(167, 139, 250);
            lblMotivasi.Location = new Point(20, 50);
            lblMotivasi.Name = "lblMotivasi";
            lblMotivasi.Size = new Size(400, 25);
            lblMotivasi.TabIndex = 1;
            lblMotivasi.Text = "Yuk temukan produk solid buat danus kampus!";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(20, 85);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Cari produk atau PO...";
            txtSearch.Size = new Size(250, 27);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // cmbKategori
            // 
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategori.Font = new Font("Segoe UI", 11F);
            cmbKategori.Location = new Point(285, 85);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(200, 28);
            cmbKategori.TabIndex = 3;
            cmbKategori.SelectedIndexChanged += cmbKategori_SelectedIndexChanged;
            // 
            // lblCount
            // 
            lblCount.Font = new Font("Segoe UI", 9F);
            lblCount.ForeColor = Color.White;
            lblCount.Location = new Point(500, 95);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(200, 20);
            lblCount.TabIndex = 4;
            lblCount.Text = "Memuat...";
            // 
            // flowPanelProduk
            // 
            flowPanelProduk.AutoScroll = true;
            flowPanelProduk.BackColor = Color.FromArgb(255, 249, 230);
            flowPanelProduk.Dock = DockStyle.Fill;
            flowPanelProduk.Location = new Point(0, 130);
            flowPanelProduk.Name = "flowPanelProduk";
            flowPanelProduk.Padding = new Padding(10);
            flowPanelProduk.Size = new Size(1046, 203);
            flowPanelProduk.TabIndex = 0;
            // 
            // UserDashboardControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelProduk);
            Controls.Add(pnlTop);
            Name = "UserDashboardControl";
            Size = new Size(1046, 333);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblGreeting, lblMotivasi, lblCount;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
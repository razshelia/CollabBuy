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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblGreeting = new System.Windows.Forms.Label();
            this.lblMotivasi = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.flowPanelProduk = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlTop.Controls.Add(this.lblGreeting);
            this.pnlTop.Controls.Add(this.lblMotivasi);
            this.pnlTop.Controls.Add(this.txtSearch);
            this.pnlTop.Controls.Add(this.cmbKategori);
            this.pnlTop.Controls.Add(this.lblCount);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1046, 140);
            this.pnlTop.TabIndex = 1;
            // 
            // lblGreeting
            // 
            this.lblGreeting.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblGreeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblGreeting.Location = new System.Drawing.Point(20, 15);
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Size = new System.Drawing.Size(500, 38);
            this.lblGreeting.TabIndex = 0;
            this.lblGreeting.Text = "HALO, BESTIE! 👋";
            // 
            // lblMotivasi
            // 
            this.lblMotivasi.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblMotivasi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblMotivasi.Location = new System.Drawing.Point(20, 53);
            this.lblMotivasi.Name = "lblMotivasi";
            this.lblMotivasi.Size = new System.Drawing.Size(500, 25);
            this.lblMotivasi.TabIndex = 1;
            this.lblMotivasi.Text = "Yuk temukan produk solid buat danus kampus!";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.txtSearch.Location = new System.Drawing.Point(20, 92);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(260, 28);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.PlaceholderText = "Cari produk atau PO...";
            // 
            // cmbKategori
            // 
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.cmbKategori.Location = new System.Drawing.Point(295, 92);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(210, 28);
            this.cmbKategori.TabIndex = 3;
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(520, 96);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(300, 23);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "Memuat produk...";
            // 
            // flowPanelProduk
            // 
            this.flowPanelProduk.AutoScroll = true;
            this.flowPanelProduk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flowPanelProduk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelProduk.Location = new System.Drawing.Point(0, 140);
            this.flowPanelProduk.Name = "flowPanelProduk";
            this.flowPanelProduk.Padding = new System.Windows.Forms.Padding(15);
            this.flowPanelProduk.Size = new System.Drawing.Size(1046, 590);
            this.flowPanelProduk.TabIndex = 0;
            // 
            // UserDashboardControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.flowPanelProduk);
            this.Controls.Add(this.pnlTop);
            this.Name = "UserDashboardControl";
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblGreeting, lblMotivasi, lblCount;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
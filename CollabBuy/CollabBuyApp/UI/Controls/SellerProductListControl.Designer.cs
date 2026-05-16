namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerProductListControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnKembali = new System.Windows.Forms.Button();
            this.lblJudul = new System.Windows.Forms.Label();
            this.btnTambahProduk = new System.Windows.Forms.Button();
            this.flowPanelProduk = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlHeader.Controls.Add(this.btnKembali);
            this.pnlHeader.Controls.Add(this.lblJudul);
            this.pnlHeader.Controls.Add(this.btnTambahProduk);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1046, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKembali.ForeColor = System.Drawing.Color.White;
            this.btnKembali.Location = new System.Drawing.Point(20, 20);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(140, 40);
            this.btnKembali.TabIndex = 0;
            this.btnKembali.Text = "◀ Kembali ke PO";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // lblJudul
            // 
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblJudul.Location = new System.Drawing.Point(180, 20);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(500, 40);
            this.lblJudul.TabIndex = 1;
            this.lblJudul.Text = "PRODUK MASTER JUALAN 🛍️";
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTambahProduk
            // 
            this.btnTambahProduk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTambahProduk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnTambahProduk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambahProduk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnTambahProduk.FlatAppearance.BorderSize = 2;
            this.btnTambahProduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahProduk.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnTambahProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnTambahProduk.Location = new System.Drawing.Point(860, 20);
            this.btnTambahProduk.Name = "btnTambahProduk";
            this.btnTambahProduk.Size = new System.Drawing.Size(165, 40);
            this.btnTambahProduk.TabIndex = 2;
            this.btnTambahProduk.Text = "➕ TAMBAH PRODUK";
            this.btnTambahProduk.UseVisualStyleBackColor = false;
            this.btnTambahProduk.Click += new System.EventHandler(this.btnTambahProduk_Click);
            // 
            // flowPanelProduk
            // 
            this.flowPanelProduk.AutoScroll = true;
            this.flowPanelProduk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flowPanelProduk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelProduk.Location = new System.Drawing.Point(0, 80);
            this.flowPanelProduk.Name = "flowPanelProduk";
            this.flowPanelProduk.Padding = new System.Windows.Forms.Padding(15);
            this.flowPanelProduk.Size = new System.Drawing.Size(1046, 620);
            this.flowPanelProduk.TabIndex = 1;
            // 
            // SellerProductListControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.flowPanelProduk);
            this.Controls.Add(this.pnlHeader);
            this.Name = "SellerProductListControl";
            this.Size = new System.Drawing.Size(1046, 700);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnTambahProduk;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
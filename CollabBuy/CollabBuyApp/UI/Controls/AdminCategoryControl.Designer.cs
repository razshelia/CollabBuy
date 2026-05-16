namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class AdminCategoryControl
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
            this.lblJudul = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.flowPanelKategori = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlHeader.Controls.Add(this.lblJudul);
            this.pnlHeader.Controls.Add(this.btnTambah);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1046, 80);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblJudul
            // 
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJudul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblJudul.Location = new System.Drawing.Point(20, 20);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(350, 40);
            this.lblJudul.TabIndex = 0;
            this.lblJudul.Text = "KELOLA KATEGORI 📂";
            // 
            // btnTambah
            // 
            this.btnTambah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnTambah.FlatAppearance.BorderSize = 2;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnTambah.Location = new System.Drawing.Point(840, 20);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(180, 40);
            this.btnTambah.TabIndex = 1;
            this.btnTambah.Text = "➕ Tambah Kategori";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // flowPanelKategori
            // 
            this.flowPanelKategori.AutoScroll = true;
            this.flowPanelKategori.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flowPanelKategori.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelKategori.Location = new System.Drawing.Point(0, 80);
            this.flowPanelKategori.Name = "flowPanelKategori";
            this.flowPanelKategori.Padding = new System.Windows.Forms.Padding(15);
            this.flowPanelKategori.Size = new System.Drawing.Size(1046, 196);
            this.flowPanelKategori.TabIndex = 0;
            // 
            // AdminCategoryControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.flowPanelKategori);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AdminCategoryControl";
            this.Size = new System.Drawing.Size(1046, 276);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.FlowLayoutPanel flowPanelKategori;
    }
}
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
            pnlHeader = new Panel();
            lblJudul = new Label();
            btnTambah = new Button();
            flowPanelKategori = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 27, 79);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(btnTambah);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1046, 80);
            pnlHeader.TabIndex = 1;
            // 
            // lblJudul
            // 
            lblJudul.Font = new Font("Segoe UI Black", 16F);
            lblJudul.ForeColor = Color.FromArgb(253, 224, 71);
            lblJudul.Location = new Point(20, 20);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(300, 35);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "KELOLA KATEGORI 📂";
            // 
            // btnTambah
            // 
            btnTambah.BackColor = Color.FromArgb(167, 139, 250);
            btnTambah.FlatStyle = FlatStyle.Flat;
            btnTambah.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTambah.ForeColor = Color.White;
            btnTambah.Location = new Point(500, 25);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(150, 30);
            btnTambah.TabIndex = 1;
            btnTambah.Text = "➕ Tambah Kategori";
            btnTambah.UseVisualStyleBackColor = false;
            btnTambah.Click += btnTambah_Click;
            // 
            // flowPanelKategori
            // 
            flowPanelKategori.AutoScroll = true;
            flowPanelKategori.BackColor = Color.FromArgb(255, 249, 230);
            flowPanelKategori.Dock = DockStyle.Fill;
            flowPanelKategori.Location = new Point(0, 80);
            flowPanelKategori.Name = "flowPanelKategori";
            flowPanelKategori.Padding = new Padding(10);
            flowPanelKategori.Size = new Size(1046, 196);
            flowPanelKategori.TabIndex = 0;
            // 
            // AdminCategoryControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelKategori);
            Controls.Add(pnlHeader);
            Name = "AdminCategoryControl";
            Size = new Size(1046, 276);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.FlowLayoutPanel flowPanelKategori;
    }
}
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
            pnlHeader = new System.Windows.Forms.Panel();
            btnKembali = new System.Windows.Forms.Button();
            lblJudul = new System.Windows.Forms.Label();
            btnTambahProduk = new System.Windows.Forms.Button();
            flowPanelProduk = new System.Windows.Forms.FlowLayoutPanel();

            pnlHeader.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ─────────────────────────────────────
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Height = 60;
            pnlHeader.Name = "pnlHeader";
            pnlHeader.TabIndex = 0;
            pnlHeader.Controls.Add(btnKembali);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(btnTambahProduk);

            // ── btnKembali (kiri) ─────────────────────────────
            btnKembali.BackColor = System.Drawing.Color.FromArgb(100, 100, 120);
            btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnKembali.FlatAppearance.BorderSize = 0;
            btnKembali.Font = new System.Drawing.Font("Segoe UI", 9F);
            btnKembali.ForeColor = System.Drawing.Color.White;
            btnKembali.Location = new System.Drawing.Point(15, 14);
            btnKembali.Name = "btnKembali";
            btnKembali.Size = new System.Drawing.Size(130, 32);
            btnKembali.TabIndex = 0;
            btnKembali.Text = "◀ Kembali ke PO";
            btnKembali.UseVisualStyleBackColor = false;
            btnKembali.Click += btnKembali_Click;

            // ── lblJudul (tengah) ─────────────────────────────
            lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 13F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(253, 224, 71);
            lblJudul.Location = new System.Drawing.Point(160, 16);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new System.Drawing.Size(560, 28);
            lblJudul.TabIndex = 1;
            lblJudul.Text = "🛍️ Daftar Produk";

            // ── btnTambahProduk (kanan) ───────────────────────
            btnTambahProduk.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            btnTambahProduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTambahProduk.FlatAppearance.BorderSize = 0;
            btnTambahProduk.Font = new System.Drawing.Font("Segoe UI Black", 9F);
            btnTambahProduk.ForeColor = System.Drawing.Color.White;
            btnTambahProduk.Location = new System.Drawing.Point(856, 13);
            btnTambahProduk.Name = "btnTambahProduk";
            btnTambahProduk.Size = new System.Drawing.Size(160, 34);
            btnTambahProduk.TabIndex = 2;
            btnTambahProduk.Text = "➕ Tambah Produk";
            btnTambahProduk.UseVisualStyleBackColor = false;
            btnTambahProduk.Click += btnTambahProduk_Click;

            // ── flowPanelProduk ───────────────────────────────
            flowPanelProduk.AutoScroll = true;
            flowPanelProduk.BackColor = System.Drawing.Color.FromArgb(255, 249, 230);
            flowPanelProduk.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanelProduk.Name = "flowPanelProduk";
            flowPanelProduk.Padding = new System.Windows.Forms.Padding(10);
            flowPanelProduk.TabIndex = 1;

            // ── UserControl root ──────────────────────────────
            BackColor = System.Drawing.Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelProduk);
            Controls.Add(pnlHeader);
            Name = "SellerProductListControl";
            Size = new System.Drawing.Size(1046, 700);

            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnTambahProduk;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
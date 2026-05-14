namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class CatalogControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblJudul = new System.Windows.Forms.Label();
            this.txtCariNama = new System.Windows.Forms.TextBox();
            this.btnCariNama = new System.Windows.Forms.Button();
            this.cmbCariKategori = new System.Windows.Forms.ComboBox();
            this.btnCariKategori = new System.Windows.Forms.Button();
            this.flpKatalog = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1200, 800);

            // Panel Atas (Neo-Retro Ungu Pastel)
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 120;

            this.lblJudul.Text = "KATALOG GOTONG ROYONG 🛍️";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F);
            this.lblJudul.Location = new System.Drawing.Point(30, 20);
            this.lblJudul.AutoSize = true;

            // Pencarian 1: Berdasarkan String (Nama)
            this.txtCariNama.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCariNama.Location = new System.Drawing.Point(35, 65);
            this.txtCariNama.Size = new System.Drawing.Size(250, 32);

            this.btnCariNama.BackColor = System.Drawing.Color.White;
            this.btnCariNama.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCariNama.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCariNama.Text = "CARI NAMA 🔍";
            this.btnCariNama.Location = new System.Drawing.Point(295, 65);
            this.btnCariNama.Size = new System.Drawing.Size(150, 32);
            this.btnCariNama.Click += new System.EventHandler(this.btnCariNama_Click);

            // Pencarian 2: Berdasarkan Integer (ID Kategori via ComboBox)
            this.cmbCariKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCariKategori.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbCariKategori.Location = new System.Drawing.Point(600, 65);
            this.cmbCariKategori.Size = new System.Drawing.Size(200, 33);

            this.btnCariKategori.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning
            this.btnCariKategori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCariKategori.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCariKategori.Text = "FILTER KATEGORI 🗂️";
            this.btnCariKategori.Location = new System.Drawing.Point(810, 65);
            this.btnCariKategori.Size = new System.Drawing.Size(200, 32);
            this.btnCariKategori.Click += new System.EventHandler(this.btnCariKategori_Click);

            // FlowLayoutPanel untuk Scrollable Cards
            this.flpKatalog.AutoScroll = true; // SCROLL FEATURE
            this.flpKatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpKatalog.Padding = new System.Windows.Forms.Padding(30);
            this.flpKatalog.BackColor = System.Drawing.Color.White;

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblJudul, this.txtCariNama, this.btnCariNama,
                this.cmbCariKategori, this.btnCariKategori
            });

            this.Controls.Add(this.flpKatalog);
            this.Controls.Add(this.pnlTop);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.TextBox txtCariNama;
        private System.Windows.Forms.ComboBox cmbCariKategori;
        private System.Windows.Forms.Button btnCariNama, btnCariKategori;
        private System.Windows.Forms.FlowLayoutPanel flpKatalog;
    }
}
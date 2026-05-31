namespace CollabBuy.CollabBuyApp.View.Product
{
    partial class KatalogProdukControl
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblCari = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();

            this.lblInfo = new System.Windows.Forms.Label();

            // Card container — scrollable FlowLayoutPanel
            this.flpKartu = new System.Windows.Forms.FlowLayoutPanel();

            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();

            // --- pnlHeader ---
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 90;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Text = "🛍️  Katalog Produk";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(210, 185, 255);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 55);
            this.lblSubtitle.Text = "Temukan produk impianmu di sini, bestie!";

            // --- pnlFilter ---
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.pnlFilter.Height = 62;
            this.pnlFilter.Location = new System.Drawing.Point(0, 90);
            this.pnlFilter.Controls.Add(this.lblCari);
            this.pnlFilter.Controls.Add(this.txtCari);
            this.pnlFilter.Controls.Add(this.btnCari);
            this.pnlFilter.Controls.Add(this.btnReset);

            this.lblCari.AutoSize = true;
            this.lblCari.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCari.ForeColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.lblCari.Location = new System.Drawing.Point(30, 20);
            this.lblCari.Text = "Cari Produk:";

            this.txtCari.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCari.Location = new System.Drawing.Point(120, 16);
            this.txtCari.Size = new System.Drawing.Size(320, 28);
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.BackColor = System.Drawing.Color.White;
            this.txtCari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCari_KeyPress);

            this.btnCari.BackColor = System.Drawing.Color.FromArgb(72, 0, 120);
            this.btnCari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCari.FlatAppearance.BorderSize = 0;
            this.btnCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCari.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCari.ForeColor = System.Drawing.Color.FromArgb(254, 252, 200);
            this.btnCari.Location = new System.Drawing.Point(455, 14);
            this.btnCari.Size = new System.Drawing.Size(90, 32);
            this.btnCari.Text = "🔍 Cari";
            this.btnCari.UseVisualStyleBackColor = false;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);

            this.btnReset.BackColor = System.Drawing.Color.FromArgb(200, 170, 255);
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(40, 0, 80);
            this.btnReset.Location = new System.Drawing.Point(556, 14);
            this.btnReset.Size = new System.Drawing.Size(90, 32);
            this.btnReset.Text = "↺ Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // --- lblInfo ---
            this.lblInfo.AutoSize = false;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(0, 100, 50);
            this.lblInfo.BackColor = System.Drawing.Color.FromArgb(210, 255, 230);
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblInfo.Location = new System.Drawing.Point(30, 158);
            this.lblInfo.Size = new System.Drawing.Size(700, 26);
            this.lblInfo.Text = "";
            this.lblInfo.Visible = false;

            // --- flpKartu ---
            this.flpKartu.AutoScroll = true;
            this.flpKartu.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.flpKartu.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpKartu.WrapContents = true;
            this.flpKartu.Padding = new System.Windows.Forms.Padding(10);
            this.flpKartu.Location = new System.Drawing.Point(0, 190);
            this.flpKartu.Size = new System.Drawing.Size(980, 510);

            // --- KatalogProdukControl ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 255);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.flpKartu);
            this.Name = "KatalogProdukControl";
            this.Size = new System.Drawing.Size(980, 700);
            this.Load += new System.EventHandler(this.KatalogProdukControl_Load);
            this.Resize += new System.EventHandler(this.KatalogProdukControl_Resize);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.FlowLayoutPanel flpKartu;
    }
}

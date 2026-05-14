namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class UserDashboardControl
    {
        private void InitializeComponent()
        {
            pnlHeader   = new System.Windows.Forms.Panel();
            pnlSearch   = new System.Windows.Forms.Panel();
            lblSapaan   = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            txtCari     = new System.Windows.Forms.TextBox();
            cmbKategori = new System.Windows.Forms.ComboBox();
            flpKonten   = new System.Windows.Forms.FlowLayoutPanel();

            SuspendLayout();

            BackColor = System.Drawing.Color.FromArgb(247, 247, 252);
            Dock      = System.Windows.Forms.DockStyle.Fill;
            Name      = "UserDashboardControl";

            // ── Header ─────────────────────────────────────────
            pnlHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Height    = 120;
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Padding   = new System.Windows.Forms.Padding(30, 0, 30, 0);
            pnlHeader.Name      = "pnlHeader";

            lblSapaan.Text      = "SPILL PRODUK HARI INI ✨";
            lblSapaan.Font      = new System.Drawing.Font("Segoe UI Black", 22F, System.Drawing.FontStyle.Bold);
            lblSapaan.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            lblSapaan.AutoSize  = false;
            lblSapaan.Size      = new System.Drawing.Size(700, 52);
            lblSapaan.Location  = new System.Drawing.Point(30, 18);
            lblSapaan.Name      = "lblSapaan";

            lblSubtitle.Text      = "Temukan barang Danus favoritmu sekarang!";
            lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            lblSubtitle.AutoSize  = false;
            lblSubtitle.Size      = new System.Drawing.Size(500, 26);
            lblSubtitle.Location  = new System.Drawing.Point(32, 72);
            lblSubtitle.Name      = "lblSubtitle";

            pnlHeader.Controls.Add(lblSapaan);
            pnlHeader.Controls.Add(lblSubtitle);

            // ── Search bar ─────────────────────────────────────
            pnlSearch.Dock      = System.Windows.Forms.DockStyle.Top;
            pnlSearch.Height    = 64;
            pnlSearch.BackColor = System.Drawing.Color.White;
            pnlSearch.Padding   = new System.Windows.Forms.Padding(30, 10, 30, 10);
            pnlSearch.Name      = "pnlSearch";

            txtCari.PlaceholderText = "🔍  Cari nama produk atau toko...";
            txtCari.Font            = new System.Drawing.Font("Segoe UI", 12F);
            txtCari.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            txtCari.Size            = new System.Drawing.Size(400, 34);
            txtCari.Location        = new System.Drawing.Point(30, 14);
            txtCari.Name            = "txtCari";

            cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbKategori.Font          = new System.Drawing.Font("Segoe UI", 11F);
            cmbKategori.Size          = new System.Drawing.Size(200, 34);
            cmbKategori.Location      = new System.Drawing.Point(445, 14);
            cmbKategori.Name          = "cmbKategori";

            pnlSearch.Controls.Add(txtCari);
            pnlSearch.Controls.Add(cmbKategori);

            // ── Product card area (scrollable) ─────────────────
            flpKonten.Dock        = System.Windows.Forms.DockStyle.Fill;
            flpKonten.AutoScroll  = true;
            flpKonten.Padding     = new System.Windows.Forms.Padding(25);
            flpKonten.BackColor   = System.Drawing.Color.FromArgb(247, 247, 252);
            flpKonten.Name        = "flpKonten";

            Controls.Add(flpKonten);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel          pnlHeader, pnlSearch;
        private System.Windows.Forms.Label          lblSapaan, lblSubtitle;
        private System.Windows.Forms.TextBox        txtCari;
        private System.Windows.Forms.ComboBox       cmbKategori;
        private System.Windows.Forms.FlowLayoutPanel flpKonten;
    }
}

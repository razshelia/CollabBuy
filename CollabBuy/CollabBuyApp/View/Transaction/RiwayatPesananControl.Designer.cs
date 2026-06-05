namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class RiwayatPesananControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvRiwayat = new System.Windows.Forms.DataGridView();
            this.pnlDetail = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Riwayat Jajan Kamu 📜";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 62);
            this.lblSubtitle.Text = "Klik 🔍 Detail untuk lihat rincian & split bill.";

            // splitMain
            this.splitMain.Dock = System.Windows.Forms.DockStyle.None;
            this.splitMain.Location = new System.Drawing.Point(30, 95);
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.Panel1MinSize = 300;
            this.splitMain.Panel2MinSize = 0;
            this.splitMain.Panel2Collapsed = true;  // detail tersembunyi awalnya
            this.splitMain.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);

            // splitMain.Panel1 — tabel riwayat
            this.splitMain.Panel1.Controls.Add(this.dgvRiwayat);
            this.splitMain.Panel1.Controls.Add(this.btnRefresh);
            this.splitMain.Panel1.BackColor = System.Drawing.Color.White;

            // splitMain.Panel2 — detail inline
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Panel2.BackColor = System.Drawing.Color.White;

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.Text = "🔄 Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvRiwayat
            System.Windows.Forms.DataGridViewCellStyle hdrStyle = new System.Windows.Forms.DataGridViewCellStyle();
            hdrStyle.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            hdrStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            hdrStyle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            hdrStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            hdrStyle.SelectionForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            hdrStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;
            rowStyle.Padding = new System.Windows.Forms.Padding(5);

            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.AllowUserToDeleteRows = false;
            this.dgvRiwayat.BackgroundColor = System.Drawing.Color.White;
            this.dgvRiwayat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRiwayat.ColumnHeadersDefaultCellStyle = hdrStyle;
            this.dgvRiwayat.ColumnHeadersHeight = 45;
            this.dgvRiwayat.EnableHeadersVisualStyles = false;
            this.dgvRiwayat.MultiSelect = false;
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.RowHeadersVisible = false;
            this.dgvRiwayat.RowsDefaultCellStyle = rowStyle;
            this.dgvRiwayat.RowTemplate.Height = 45;
            this.dgvRiwayat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRiwayat.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                           | System.Windows.Forms.AnchorStyles.Bottom
                                                           | System.Windows.Forms.AnchorStyles.Left
                                                           | System.Windows.Forms.AnchorStyles.Right;

            // pnlDetail — panel kanan, diisi secara dinamis dari kode
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.AutoScroll = true;
            this.pnlDetail.BackColor = System.Drawing.Color.White;

            // RiwayatPesananControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.RiwayatPesananControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvRiwayat;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlDetail;
    }
}
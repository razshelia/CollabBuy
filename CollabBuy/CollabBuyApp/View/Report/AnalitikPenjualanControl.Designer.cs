namespace CollabBuy.CollabBuyApp.View.Report
{
    partial class AnalitikPenjualanControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTotalPendapatan = new System.Windows.Forms.Panel();
            this.lblValPendapatan = new System.Windows.Forms.Label();
            this.lblTitlePendapatan = new System.Windows.Forms.Label();
            this.dgvLaporan = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).BeginInit();
            this.pnlTotalPendapatan.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 32);
            this.lblTitle.Text = "Analitik Penjualan";
            // 
            // pnlTotalPendapatan
            // 
            this.pnlTotalPendapatan.BackColor = System.Drawing.Color.White;
            this.pnlTotalPendapatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotalPendapatan.Controls.Add(this.lblValPendapatan);
            this.pnlTotalPendapatan.Controls.Add(this.lblTitlePendapatan);
            this.pnlTotalPendapatan.Location = new System.Drawing.Point(36, 80);
            this.pnlTotalPendapatan.Size = new System.Drawing.Size(300, 100);
            // 
            // lblValPendapatan
            // 
            this.lblValPendapatan.AutoSize = true;
            this.lblValPendapatan.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValPendapatan.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblValPendapatan.Location = new System.Drawing.Point(15, 45);
            this.lblValPendapatan.Text = "Rp 0";
            // 
            // lblTitlePendapatan
            // 
            this.lblTitlePendapatan.AutoSize = true;
            this.lblTitlePendapatan.Text = "Total Pendapatan";
            // 
            // dgvLaporan
            // 
            this.dgvLaporan.AllowUserToAddRows = false;
            this.dgvLaporan.BackgroundColor = System.Drawing.Color.White;
            this.dgvLaporan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLaporan.Location = new System.Drawing.Point(36, 200);
            this.dgvLaporan.Size = new System.Drawing.Size(900, 400);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(836, 150);
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.Text = "Export PDF";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // AnalitikPenjualanControl
            // 
            this.Controls.Add(this.dgvLaporan);
            this.Controls.Add(this.pnlTotalPendapatan);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExport);
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.AnalitikPenjualanControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}

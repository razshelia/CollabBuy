namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class AdminReportControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlNavigasi = new Panel();
            btnExcept = new Button();
            btnIntersect = new Button();
            btnUnion = new Button();
            btnSubquery = new Button();
            btnGroupingSets = new Button();
            btnRollup = new Button();
            btnCube = new Button();
            btnBarangTerjual = new Button();
            dgvReport = new DataGridView();
            pnlNavigasi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();
            // 
            // pnlNavigasi
            // 
            pnlNavigasi.BackColor = Color.FromArgb(45, 27, 79);
            pnlNavigasi.Controls.Add(btnExcept);
            pnlNavigasi.Controls.Add(btnIntersect);
            pnlNavigasi.Controls.Add(btnUnion);
            pnlNavigasi.Controls.Add(btnSubquery);
            pnlNavigasi.Controls.Add(btnGroupingSets);
            pnlNavigasi.Controls.Add(btnRollup);
            pnlNavigasi.Controls.Add(btnCube);
            pnlNavigasi.Controls.Add(btnBarangTerjual);
            pnlNavigasi.Dock = DockStyle.Left;
            pnlNavigasi.Location = new Point(0, 0);
            pnlNavigasi.Name = "pnlNavigasi";
            pnlNavigasi.Size = new Size(220, 276);
            pnlNavigasi.TabIndex = 1;
            // 
            // btnExcept
            // 
            btnExcept.Dock = DockStyle.Top;
            btnExcept.FlatStyle = FlatStyle.Flat;
            btnExcept.ForeColor = Color.White;
            btnExcept.Location = new Point(0, 315);
            btnExcept.Name = "btnExcept";
            btnExcept.Size = new Size(220, 45);
            btnExcept.TabIndex = 0;
            btnExcept.Text = "− EXCEPT User";
            btnExcept.Click += btnExcept_Click;
            // 
            // btnIntersect
            // 
            btnIntersect.Dock = DockStyle.Top;
            btnIntersect.FlatStyle = FlatStyle.Flat;
            btnIntersect.ForeColor = Color.White;
            btnIntersect.Location = new Point(0, 270);
            btnIntersect.Name = "btnIntersect";
            btnIntersect.Size = new Size(220, 45);
            btnIntersect.TabIndex = 1;
            btnIntersect.Text = "∩ INTERSECT Penjual";
            btnIntersect.Click += btnIntersect_Click;
            // 
            // btnUnion
            // 
            btnUnion.Dock = DockStyle.Top;
            btnUnion.FlatStyle = FlatStyle.Flat;
            btnUnion.ForeColor = Color.White;
            btnUnion.Location = new Point(0, 225);
            btnUnion.Name = "btnUnion";
            btnUnion.Size = new Size(220, 45);
            btnUnion.TabIndex = 2;
            btnUnion.Text = "∪ UNION Transaksi";
            btnUnion.Click += btnUnion_Click;
            // 
            // btnSubquery
            // 
            btnSubquery.Dock = DockStyle.Top;
            btnSubquery.FlatStyle = FlatStyle.Flat;
            btnSubquery.ForeColor = Color.White;
            btnSubquery.Location = new Point(0, 180);
            btnSubquery.Name = "btnSubquery";
            btnSubquery.Size = new Size(220, 45);
            btnSubquery.TabIndex = 3;
            btnSubquery.Text = "🔍 Subquery Kuota";
            btnSubquery.Click += btnSubquery_Click;
            // 
            // btnGroupingSets
            // 
            btnGroupingSets.Dock = DockStyle.Top;
            btnGroupingSets.FlatStyle = FlatStyle.Flat;
            btnGroupingSets.ForeColor = Color.White;
            btnGroupingSets.Location = new Point(0, 135);
            btnGroupingSets.Name = "btnGroupingSets";
            btnGroupingSets.Size = new Size(220, 45);
            btnGroupingSets.TabIndex = 4;
            btnGroupingSets.Text = "\U0001f9e9 GROUPING SETS";
            btnGroupingSets.Click += btnGroupingSets_Click;
            // 
            // btnRollup
            // 
            btnRollup.Dock = DockStyle.Top;
            btnRollup.FlatStyle = FlatStyle.Flat;
            btnRollup.ForeColor = Color.White;
            btnRollup.Location = new Point(0, 90);
            btnRollup.Name = "btnRollup";
            btnRollup.Size = new Size(220, 45);
            btnRollup.TabIndex = 5;
            btnRollup.Text = "📈 ROLLUP Omzet Waktu";
            btnRollup.Click += btnRollup_Click;
            // 
            // btnCube
            // 
            btnCube.Dock = DockStyle.Top;
            btnCube.FlatStyle = FlatStyle.Flat;
            btnCube.ForeColor = Color.White;
            btnCube.Location = new Point(0, 45);
            btnCube.Name = "btnCube";
            btnCube.Size = new Size(220, 45);
            btnCube.TabIndex = 6;
            btnCube.Text = "\U0001f9ca CUBE (Kategori x PO)";
            btnCube.Click += btnCube_Click;
            // 
            // btnBarangTerjual
            // 
            btnBarangTerjual.Dock = DockStyle.Top;
            btnBarangTerjual.FlatStyle = FlatStyle.Flat;
            btnBarangTerjual.ForeColor = Color.White;
            btnBarangTerjual.Location = new Point(0, 0);
            btnBarangTerjual.Name = "btnBarangTerjual";
            btnBarangTerjual.Size = new Size(220, 45);
            btnBarangTerjual.TabIndex = 7;
            btnBarangTerjual.Text = "📊 Barang Terjual";
            btnBarangTerjual.Click += btnBarangTerjual_Click;
            // 
            // dgvReport
            // 
            dgvReport.AllowUserToAddRows = false;
            dgvReport.BackgroundColor = Color.White;
            dgvReport.Dock = DockStyle.Fill;
            dgvReport.Location = new Point(220, 0);
            dgvReport.Name = "dgvReport";
            dgvReport.ReadOnly = true;
            dgvReport.Size = new Size(826, 276);
            dgvReport.TabIndex = 0;
            // 
            // AdminReportControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(dgvReport);
            Controls.Add(pnlNavigasi);
            Name = "AdminReportControl";
            Size = new Size(1046, 276);
            pnlNavigasi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlNavigasi;
        private System.Windows.Forms.Button btnBarangTerjual, btnCube, btnRollup, btnGroupingSets, btnSubquery, btnUnion, btnIntersect, btnExcept;
        private System.Windows.Forms.DataGridView dgvReport;
    }
}
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
            this.pnlNavigasi = new System.Windows.Forms.Panel();
            this.btnExcept = new System.Windows.Forms.Button();
            this.btnIntersect = new System.Windows.Forms.Button();
            this.btnUnion = new System.Windows.Forms.Button();
            this.btnSubquery = new System.Windows.Forms.Button();
            this.btnGroupingSets = new System.Windows.Forms.Button();
            this.btnRollup = new System.Windows.Forms.Button();
            this.btnCube = new System.Windows.Forms.Button();
            this.btnBarangTerjual = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.pnlNavigasi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNavigasi
            // 
            this.pnlNavigasi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlNavigasi.Controls.Add(this.btnExcept);
            this.pnlNavigasi.Controls.Add(this.btnIntersect);
            this.pnlNavigasi.Controls.Add(this.btnUnion);
            this.pnlNavigasi.Controls.Add(this.btnSubquery);
            this.pnlNavigasi.Controls.Add(this.btnGroupingSets);
            this.pnlNavigasi.Controls.Add(this.btnRollup);
            this.pnlNavigasi.Controls.Add(this.btnCube);
            this.pnlNavigasi.Controls.Add(this.btnBarangTerjual);
            this.pnlNavigasi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavigasi.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigasi.Name = "pnlNavigasi";
            this.pnlNavigasi.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlNavigasi.Size = new System.Drawing.Size(250, 480);
            this.pnlNavigasi.TabIndex = 1;
            // 
            // btnExcept
            // 
            this.btnExcept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcept.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExcept.FlatAppearance.BorderSize = 0;
            this.btnExcept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcept.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExcept.ForeColor = System.Drawing.Color.White;
            this.btnExcept.Location = new System.Drawing.Point(0, 360);
            this.btnExcept.Name = "btnExcept";
            this.btnExcept.Size = new System.Drawing.Size(250, 50);
            this.btnExcept.TabIndex = 0;
            this.btnExcept.Text = "− Daftar Pengguna Pasif";
            this.btnExcept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcept.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnExcept.Click += new System.EventHandler(this.btnExcept_Click);
            // 
            // btnIntersect
            // 
            this.btnIntersect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIntersect.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnIntersect.FlatAppearance.BorderSize = 0;
            this.btnIntersect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntersect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnIntersect.ForeColor = System.Drawing.Color.White;
            this.btnIntersect.Location = new System.Drawing.Point(0, 310);
            this.btnIntersect.Name = "btnIntersect";
            this.btnIntersect.Size = new System.Drawing.Size(250, 50);
            this.btnIntersect.TabIndex = 1;
            this.btnIntersect.Text = "∩ Produk Terpopuler Bersama";
            this.btnIntersect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIntersect.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnIntersect.Click += new System.EventHandler(this.btnIntersect_Click);
            // 
            // btnUnion
            // 
            this.btnUnion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUnion.FlatAppearance.BorderSize = 0;
            this.btnUnion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUnion.ForeColor = System.Drawing.Color.White;
            this.btnUnion.Location = new System.Drawing.Point(0, 260);
            this.btnUnion.Name = "btnUnion";
            this.btnUnion.Size = new System.Drawing.Size(250, 50);
            this.btnUnion.TabIndex = 2;
            this.btnUnion.Text = "∪ Kombinasi Semua Transaksi";
            this.btnUnion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnion.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnUnion.Click += new System.EventHandler(this.btnUnion_Click);
            // 
            // btnSubquery
            // 
            this.btnSubquery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubquery.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubquery.FlatAppearance.BorderSize = 0;
            this.btnSubquery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubquery.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSubquery.ForeColor = System.Drawing.Color.White;
            this.btnSubquery.Location = new System.Drawing.Point(0, 210);
            this.btnSubquery.Name = "btnSubquery";
            this.btnSubquery.Size = new System.Drawing.Size(250, 50);
            this.btnSubquery.TabIndex = 3;
            this.btnSubquery.Text = "🔍 Monitor Kuota Menipis";
            this.btnSubquery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubquery.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSubquery.Click += new System.EventHandler(this.btnSubquery_Click);
            // 
            // btnGroupingSets
            // 
            this.btnGroupingSets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGroupingSets.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGroupingSets.FlatAppearance.BorderSize = 0;
            this.btnGroupingSets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupingSets.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGroupingSets.ForeColor = System.Drawing.Color.White;
            this.btnGroupingSets.Location = new System.Drawing.Point(0, 160);
            this.btnGroupingSets.Name = "btnGroupingSets";
            this.btnGroupingSets.Size = new System.Drawing.Size(250, 50);
            this.btnGroupingSets.TabIndex = 4;
            this.btnGroupingSets.Text = "🧩 Ringkasan Data Multi-Grup";
            this.btnGroupingSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGroupingSets.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnGroupingSets.Click += new System.EventHandler(this.btnGroupingSets_Click);
            // 
            // btnRollup
            // 
            this.btnRollup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRollup.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRollup.FlatAppearance.BorderSize = 0;
            this.btnRollup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRollup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRollup.ForeColor = System.Drawing.Color.White;
            this.btnRollup.Location = new System.Drawing.Point(0, 110);
            this.btnRollup.Name = "btnRollup";
            this.btnRollup.Size = new System.Drawing.Size(250, 50);
            this.btnRollup.TabIndex = 5;
            this.btnRollup.Text = "📈 Laporan Omzet Berkala";
            this.btnRollup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRollup.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnRollup.Click += new System.EventHandler(this.btnRollup_Click);
            // 
            // btnCube
            // 
            this.btnCube.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCube.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCube.FlatAppearance.BorderSize = 0;
            this.btnCube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCube.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCube.ForeColor = System.Drawing.Color.White;
            this.btnCube.Location = new System.Drawing.Point(0, 60);
            this.btnCube.Name = "btnCube";
            this.btnCube.Size = new System.Drawing.Size(250, 50);
            this.btnCube.TabIndex = 6;
            this.btnCube.Text = "🧊 Analisis Silang Kategori";
            this.btnCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCube.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCube.Click += new System.EventHandler(this.btnCube_Click);
            // 
            // btnBarangTerjual
            // 
            this.btnBarangTerjual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBarangTerjual.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBarangTerjual.FlatAppearance.BorderSize = 0;
            this.btnBarangTerjual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBarangTerjual.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBarangTerjual.ForeColor = System.Drawing.Color.White;
            this.btnBarangTerjual.Location = new System.Drawing.Point(0, 10);
            this.btnBarangTerjual.Name = "btnBarangTerjual";
            this.btnBarangTerjual.Size = new System.Drawing.Size(250, 50);
            this.btnBarangTerjual.TabIndex = 7;
            this.btnBarangTerjual.Text = "📊 Barang Terjual";
            this.btnBarangTerjual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBarangTerjual.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBarangTerjual.Click += new System.EventHandler(this.btnBarangTerjual_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(250, 0);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.Size = new System.Drawing.Size(796, 480);
            this.dgvReport.TabIndex = 0;
            // 
            // AdminReportControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.pnlNavigasi);
            this.Name = "AdminReportControl";
            this.Size = new System.Drawing.Size(1046, 480);
            this.pnlNavigasi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlNavigasi;
        private System.Windows.Forms.Button btnBarangTerjual, btnCube, btnRollup, btnGroupingSets, btnSubquery, btnUnion, btnIntersect, btnExcept;
        private System.Windows.Forms.DataGridView dgvReport;
    }
}
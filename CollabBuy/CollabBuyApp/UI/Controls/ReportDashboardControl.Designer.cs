namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ReportDashboardControl
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
            this.lblJudul = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabReport = new System.Windows.Forms.TabControl();
            this.tpCube = new System.Windows.Forms.TabPage();
            this.dgvCube = new System.Windows.Forms.DataGridView();
            this.tpRollup = new System.Windows.Forms.TabPage();
            this.dgvRollup = new System.Windows.Forms.DataGridView();
            this.tabReport.SuspendLayout();
            this.tpCube.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCube)).BeginInit();
            this.tpRollup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRollup)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1200, 800);

            this.lblJudul.Text = "LAPORAN ANALITIK (GEN Z STYLE) 📊";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 18F);
            this.lblJudul.Location = new System.Drawing.Point(30, 30);
            this.lblJudul.AutoSize = true;

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(980, 35);
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.Text = "UPDATE DATA 🔄";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.tabReport.Controls.Add(this.tpCube);
            this.tabReport.Controls.Add(this.tpRollup);
            this.tabReport.Location = new System.Drawing.Point(30, 100);
            this.tabReport.Size = new System.Drawing.Size(1100, 600);

            this.tpCube.Controls.Add(this.dgvCube);
            this.tpCube.Text = "Analisis CUBE";
            this.dgvCube.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCube.BackgroundColor = System.Drawing.Color.White;

            this.tpRollup.Controls.Add(this.dgvRollup);
            this.tpRollup.Text = "Analisis ROLLUP";
            this.dgvRollup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRollup.BackgroundColor = System.Drawing.Color.White;

            this.Controls.Add(this.tabReport);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblJudul);
            this.tabReport.ResumeLayout(false);
            this.tpCube.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCube)).EndInit();
            this.tpRollup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRollup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabControl tabReport;
        private System.Windows.Forms.TabPage tpCube, tpRollup;
        private System.Windows.Forms.DataGridView dgvCube, dgvRollup;
    }
}
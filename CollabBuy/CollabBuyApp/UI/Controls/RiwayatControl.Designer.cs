namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class RiwayatControl
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
            this.dgvRiwayat = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1000, 700);

            this.lblJudul.Text = "RIWAYAT PESANAN KAMU ";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.FromArgb(30, 27, 50);
            this.lblJudul.Location = new System.Drawing.Point(30, 30);
            this.lblJudul.AutoSize = true;

            this.dgvRiwayat.BackgroundColor = System.Drawing.Color.White;
            this.dgvRiwayat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiwayat.Location = new System.Drawing.Point(30, 80);
            this.dgvRiwayat.Size = new System.Drawing.Size(940, 500);
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.AllowUserToDeleteRows = false;
            this.dgvRiwayat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Text = "REFRESH DATA ";
            this.btnRefresh.Location = new System.Drawing.Point(820, 600);
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.dgvRiwayat);
            this.Controls.Add(this.btnRefresh);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.DataGridView dgvRiwayat;
        private System.Windows.Forms.Button btnRefresh;
    }
}
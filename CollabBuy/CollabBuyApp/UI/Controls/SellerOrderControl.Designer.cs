namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerOrderControl
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
            this.dgvPesanan = new System.Windows.Forms.DataGridView();
            this.btnValidasi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1200, 800);

            this.lblJudul.Text = "PESANAN MASUK (CUAN TIME 💸)";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 18F);
            this.lblJudul.Location = new System.Drawing.Point(30, 30);
            this.lblJudul.AutoSize = true;

            // DataGridView Neo-Retro
            this.dgvPesanan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPesanan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPesanan.Location = new System.Drawing.Point(30, 90);
            this.dgvPesanan.Size = new System.Drawing.Size(1100, 500);
            this.dgvPesanan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // Tombol Validasi
            this.btnValidasi.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning
            this.btnValidasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidasi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnValidasi.Text = "VALIDASI PEMBAYARAN ✅";
            this.btnValidasi.Location = new System.Drawing.Point(30, 620);
            this.btnValidasi.Size = new System.Drawing.Size(300, 50);
            this.btnValidasi.Click += new System.EventHandler(this.btnValidasi_Click);

            this.Controls.Add(this.btnValidasi);
            this.Controls.Add(this.dgvPesanan);
            this.Controls.Add(this.lblJudul);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.DataGridView dgvPesanan;
        private System.Windows.Forms.Button btnValidasi;
    }
}
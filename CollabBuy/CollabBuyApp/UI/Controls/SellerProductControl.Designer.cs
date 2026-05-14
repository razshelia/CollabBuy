namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerProductControl
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
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNamaProduk = new System.Windows.Forms.TextBox();
            this.lblKategori = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.lblStok = new System.Windows.Forms.Label();
            this.numStok = new System.Windows.Forms.NumericUpDown();
            this.lblHarga = new System.Windows.Forms.Label();
            this.numHarga = new System.Windows.Forms.NumericUpDown();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHarga)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1200, 800);

            // Panel Card Neo-Retro
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(255, 235, 133);
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.btnSimpan);
            this.pnlForm.Controls.Add(this.numHarga);
            this.pnlForm.Controls.Add(this.lblHarga);
            this.pnlForm.Controls.Add(this.numStok);
            this.pnlForm.Controls.Add(this.lblStok);
            this.pnlForm.Controls.Add(this.cmbKategori);
            this.pnlForm.Controls.Add(this.lblKategori);
            this.pnlForm.Controls.Add(this.txtNamaProduk);
            this.pnlForm.Controls.Add(this.lblNama);
            this.pnlForm.Controls.Add(this.lblJudul);
            this.pnlForm.Location = new System.Drawing.Point(350, 50);
            this.pnlForm.Size = new System.Drawing.Size(500, 600);

            this.lblJudul.Text = "SPILL PRODUK BARU ✨";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(40, 30);
            this.lblJudul.Size = new System.Drawing.Size(400, 40);

            this.lblNama.Text = "Nama Barang (Biar Viral):";
            this.lblNama.Location = new System.Drawing.Point(50, 90);
            this.lblNama.Size = new System.Drawing.Size(200, 20);

            this.txtNamaProduk.Location = new System.Drawing.Point(50, 115);
            this.txtNamaProduk.Size = new System.Drawing.Size(400, 30);

            this.lblKategori.Text = "Vibe Kategori:";
            this.lblKategori.Location = new System.Drawing.Point(50, 170);
            this.lblKategori.Size = new System.Drawing.Size(200, 20);

            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.Location = new System.Drawing.Point(50, 195);
            this.cmbKategori.Size = new System.Drawing.Size(400, 30);

            this.lblStok.Text = "Ready Stok Berapa:";
            this.lblStok.Location = new System.Drawing.Point(50, 250);
            this.lblStok.Size = new System.Drawing.Size(200, 20);

            this.numStok.Location = new System.Drawing.Point(50, 275);
            this.numStok.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this.numStok.Size = new System.Drawing.Size(120, 27);

            this.lblHarga.Text = "Harga Dasar (Rp):";
            this.lblHarga.Location = new System.Drawing.Point(50, 330);
            this.lblHarga.Size = new System.Drawing.Size(200, 20);

            this.numHarga.Location = new System.Drawing.Point(50, 355);
            this.numHarga.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numHarga.Size = new System.Drawing.Size(400, 27);

            this.btnSimpan.BackColor = System.Drawing.Color.FromArgb(170, 150, 218);
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.Location = new System.Drawing.Point(50, 480);
            this.btnSimpan.Size = new System.Drawing.Size(400, 50);
            this.btnSimpan.Text = "POST KE KATALOG! ✨";
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            this.Controls.Add(this.pnlForm);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHarga)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblJudul, lblNama, lblKategori, lblStok, lblHarga;
        private System.Windows.Forms.TextBox txtNamaProduk;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.NumericUpDown numStok, numHarga;
        private System.Windows.Forms.Button btnSimpan;
    }
}
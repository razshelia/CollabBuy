namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class CheckoutControl
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
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.numJumlah = new System.Windows.Forms.NumericUpDown();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblBukti = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnBayar = new System.Windows.Forms.Button();
            this.lblPathBukti = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJumlah)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1000, 700);

            // Panel Card Neo-Retro
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Location = new System.Drawing.Point(300, 100);
            this.pnlCard.Size = new System.Drawing.Size(400, 480);

            this.lblJudul.Text = "CHECKOUT DULU BESTIE 🛒";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(30, 20);
            this.lblJudul.AutoSize = true;

            this.lblNamaProduk.Text = "Produk: [Nama Produk]";
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.Location = new System.Drawing.Point(35, 80);
            this.lblNamaProduk.AutoSize = true;

            this.lblJumlah.Text = "Mau borong berapa?";
            this.lblJumlah.Location = new System.Drawing.Point(35, 130);
            this.lblJumlah.AutoSize = true;

            // NumericUpDown untuk kuota/jumlah beli
            this.numJumlah.Location = new System.Drawing.Point(40, 155);
            this.numJumlah.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numJumlah.Size = new System.Drawing.Size(120, 27);
            this.numJumlah.ValueChanged += new System.EventHandler(this.numJumlah_ValueChanged);

            this.lblTotal.Text = "Total Kasbon: Rp 0";
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(35, 210);
            this.lblTotal.AutoSize = true;

            this.lblBukti.Text = "Spill Bukti Transfer (Non-COD):";
            this.lblBukti.Location = new System.Drawing.Point(35, 260);
            this.lblBukti.AutoSize = true;

            this.btnUpload.BackColor = System.Drawing.Color.White;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Text = "CARI STRUK 📁";
            this.btnUpload.Location = new System.Drawing.Point(40, 285);
            this.btnUpload.Size = new System.Drawing.Size(320, 35);
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);

            this.lblPathBukti.Text = "Belum ada file terpilih.";
            this.lblPathBukti.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblPathBukti.Location = new System.Drawing.Point(40, 325);
            this.lblPathBukti.AutoSize = true;

            this.btnBayar.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning Logo
            this.btnBayar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBayar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBayar.Text = "GAS BAYAR! 🚀";
            this.btnBayar.Location = new System.Drawing.Point(40, 380);
            this.btnBayar.Size = new System.Drawing.Size(320, 50);
            this.btnBayar.Click += new System.EventHandler(this.btnBayar_Click);

            this.pnlCard.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblJudul, this.lblNamaProduk, this.lblJumlah, this.numJumlah,
                this.lblTotal, this.lblBukti, this.btnUpload, this.lblPathBukti, this.btnBayar
            });
            this.Controls.Add(this.pnlCard);

            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJumlah)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblJudul, lblNamaProduk, lblJumlah, lblTotal, lblBukti, lblPathBukti;
        private System.Windows.Forms.NumericUpDown numJumlah;
        private System.Windows.Forms.Button btnUpload, btnBayar;
    }
}
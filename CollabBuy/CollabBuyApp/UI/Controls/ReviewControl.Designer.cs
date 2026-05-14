namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ReviewControl
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
            this.lblRating = new System.Windows.Forms.Label();
            this.numRating = new System.Windows.Forms.NumericUpDown();
            this.lblKomentar = new System.Windows.Forms.Label();
            this.txtKomentar = new System.Windows.Forms.TextBox();
            this.btnKirimTesti = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1000, 700);

            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Location = new System.Drawing.Point(300, 100);
            this.pnlCard.Size = new System.Drawing.Size(400, 450);

            this.lblJudul.Text = "KASIH RATING DONG! ⭐";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F);
            this.lblJudul.Location = new System.Drawing.Point(30, 30);
            this.lblJudul.Size = new System.Drawing.Size(350, 40);

            this.lblRating.Text = "Rating Kamu (1-5):";
            this.lblRating.Location = new System.Drawing.Point(35, 90);

            // Tool Angka Khusus Rating
            this.numRating.Location = new System.Drawing.Point(40, 115);
            this.numRating.Minimum = 1;
            this.numRating.Maximum = 5;
            this.numRating.Value = 5;

            this.lblKomentar.Text = "Ceritain kepuasanmu:";
            this.lblKomentar.Location = new System.Drawing.Point(35, 160);

            this.txtKomentar.Multiline = true;
            this.txtKomentar.Location = new System.Drawing.Point(40, 185);
            this.txtKomentar.Size = new System.Drawing.Size(320, 120);

            this.btnKirimTesti.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning Logo
            this.btnKirimTesti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirimTesti.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirimTesti.Text = "SPILL TESTI! 🚀";
            this.btnKirimTesti.Location = new System.Drawing.Point(40, 340);
            this.btnKirimTesti.Size = new System.Drawing.Size(320, 50);
            this.btnKirimTesti.Click += new System.EventHandler(this.btnKirimTesti_Click);

            this.pnlCard.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblJudul, this.lblRating, this.numRating,
                this.lblKomentar, this.txtKomentar, this.btnKirimTesti
            });
            this.Controls.Add(this.pnlCard);

            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblJudul, lblRating, lblKomentar;
        private System.Windows.Forms.NumericUpDown numRating;
        private System.Windows.Forms.TextBox txtKomentar;
        private System.Windows.Forms.Button btnKirimTesti;
    }
}
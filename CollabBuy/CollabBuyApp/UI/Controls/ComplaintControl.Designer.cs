namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ComplaintControl
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
            this.lblSubjek = new System.Windows.Forms.Label();
            this.txtSubjek = new System.Windows.Forms.TextBox();
            this.lblPesan = new System.Windows.Forms.Label();
            this.txtPesan = new System.Windows.Forms.TextBox();
            this.btnKirim = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1000, 700);

            // Panel Card (Warna Kuning Gold Logo)
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(255, 235, 133);
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Location = new System.Drawing.Point(250, 100);
            this.pnlCard.Size = new System.Drawing.Size(500, 480);

            this.lblJudul.Text = "ADA MASALAH? SPILL SINI ️";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(40, 30);
            this.lblJudul.AutoSize = true;

            this.lblSubjek.Text = "Judul Aduan:";
            this.lblSubjek.Location = new System.Drawing.Point(45, 90);
            this.lblSubjek.AutoSize = true;

            this.txtSubjek.Location = new System.Drawing.Point(50, 115);
            this.txtSubjek.Size = new System.Drawing.Size(400, 27);

            this.lblPesan.Text = "Ceritain detailnya (jangan di-skip):";
            this.lblPesan.Location = new System.Drawing.Point(45, 160);
            this.lblPesan.AutoSize = true;

            // Mode Multiline untuk Pesan
            this.txtPesan.Multiline = true;
            this.txtPesan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPesan.Location = new System.Drawing.Point(50, 185);
            this.txtPesan.Size = new System.Drawing.Size(400, 150);

            this.btnKirim.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.btnKirim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirim.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirim.Text = "KIRIM ADUAN ";
            this.btnKirim.Location = new System.Drawing.Point(50, 370);
            this.btnKirim.Size = new System.Drawing.Size(400, 50);
            this.btnKirim.Click += new System.EventHandler(this.btnKirim_Click);

            this.pnlCard.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblJudul, this.lblSubjek, this.txtSubjek,
                this.lblPesan, this.txtPesan, this.btnKirim
            });

            this.Controls.Add(this.pnlCard);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblJudul, lblSubjek, lblPesan;
        private System.Windows.Forms.TextBox txtSubjek, txtPesan;
        private System.Windows.Forms.Button btnKirim;
    }
}
namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ComplaintControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubjek = new System.Windows.Forms.Label();
            this.txtSubjek = new System.Windows.Forms.TextBox();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.btnKirim = new System.Windows.Forms.Button();
            this.btnLihatAduanSaya = new System.Windows.Forms.Button();

            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            // Fullscreen
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(255, 249, 230);

            // Card
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            this.pnlCard.Size = new System.Drawing.Size(550, 500);
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;

            // Title
            this.lblTitle.Text = "ADA KENDALA? SPILL SINI! 📢";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(253, 224, 71);
            this.lblTitle.Size = new System.Drawing.Size(480, 35);
            this.lblTitle.Location = new System.Drawing.Point(35, 30);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Subjek
            this.lblSubjek.Text = "Subjek Aduan:";
            this.lblSubjek.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubjek.ForeColor = System.Drawing.Color.White;
            this.lblSubjek.Size = new System.Drawing.Size(480, 25);
            this.lblSubjek.Location = new System.Drawing.Point(35, 90);

            this.txtSubjek.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSubjek.Location = new System.Drawing.Point(35, 115);
            this.txtSubjek.Size = new System.Drawing.Size(480, 27);
            this.txtSubjek.Multiline = false;   // ← pastikan single line

            // Deskripsi
            this.lblDeskripsi.Text = "Ceritakan keluhannya:";
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDeskripsi.ForeColor = System.Drawing.Color.White;
            this.lblDeskripsi.Size = new System.Drawing.Size(480, 25);
            this.lblDeskripsi.Location = new System.Drawing.Point(35, 160);

            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDeskripsi.Location = new System.Drawing.Point(35, 190);
            this.txtDeskripsi.Size = new System.Drawing.Size(480, 140);

            // Kirim
            this.btnKirim.Text = "KIRIM ADUAN 🚀";
            this.btnKirim.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirim.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            this.btnKirim.ForeColor = System.Drawing.Color.White;
            this.btnKirim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirim.Size = new System.Drawing.Size(480, 45);
            this.btnKirim.Location = new System.Drawing.Point(35, 355);
            this.btnKirim.Click += new System.EventHandler(this.btnKirim_Click);

            // Lihat Aduan Saya
            this.btnLihatAduanSaya.Text = "📝 Lihat Aduan Saya";
            this.btnLihatAduanSaya.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLihatAduanSaya.BackColor = System.Drawing.Color.FromArgb(253, 224, 71);
            this.btnLihatAduanSaya.ForeColor = System.Drawing.Color.FromArgb(45, 27, 79);
            this.btnLihatAduanSaya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLihatAduanSaya.Size = new System.Drawing.Size(480, 30);
            this.btnLihatAduanSaya.Location = new System.Drawing.Point(35, 410);
            this.btnLihatAduanSaya.Click += new System.EventHandler(this.btnLihatAduanSaya_Click);

            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubjek);
            this.pnlCard.Controls.Add(this.txtSubjek);
            this.pnlCard.Controls.Add(this.lblDeskripsi);
            this.pnlCard.Controls.Add(this.txtDeskripsi);
            this.pnlCard.Controls.Add(this.btnKirim);
            this.pnlCard.Controls.Add(this.btnLihatAduanSaya);

            this.Controls.Add(this.pnlCard);

            this.pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblSubjek, lblDeskripsi;
        private System.Windows.Forms.TextBox txtSubjek, txtDeskripsi;
        private System.Windows.Forms.Button btnKirim, btnLihatAduanSaya;
    }
}
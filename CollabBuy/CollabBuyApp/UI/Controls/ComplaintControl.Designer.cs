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
            pnlCard = new Panel();
            lblTitle = new Label();
            lblSubjek = new Label();
            txtSubjek = new TextBox();
            lblDeskripsi = new Label();
            txtDeskripsi = new TextBox();
            btnKirim = new Button();
            btnLihatAduanSaya = new Button();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.FromArgb(36, 0, 70);
            pnlCard.BorderStyle = BorderStyle.FixedSingle;
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubjek);
            pnlCard.Controls.Add(txtSubjek);
            pnlCard.Controls.Add(lblDeskripsi);
            pnlCard.Controls.Add(txtDeskripsi);
            pnlCard.Controls.Add(btnKirim);
            pnlCard.Controls.Add(btnLihatAduanSaya);
            pnlCard.Location = new Point(252, 48);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(550, 500);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(253, 255, 182);
            lblTitle.Location = new Point(35, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(480, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ADA KENDALA? SPILL SINI! 📢";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubjek
            // 
            lblSubjek.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSubjek.ForeColor = Color.FromArgb(200, 182, 255);
            lblSubjek.Location = new Point(35, 90);
            lblSubjek.Name = "lblSubjek";
            lblSubjek.Size = new Size(480, 25);
            lblSubjek.TabIndex = 1;
            lblSubjek.Text = "Subjek Aduan:";
            // 
            // txtSubjek
            // 
            txtSubjek.Font = new Font("Segoe UI", 11F);
            txtSubjek.Location = new Point(35, 115);
            txtSubjek.Name = "txtSubjek";
            txtSubjek.Size = new Size(480, 27);
            txtSubjek.TabIndex = 2;
            // 
            // lblDeskripsi
            // 
            lblDeskripsi.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDeskripsi.ForeColor = Color.FromArgb(200, 182, 255);
            lblDeskripsi.Location = new Point(35, 160);
            lblDeskripsi.Name = "lblDeskripsi";
            lblDeskripsi.Size = new Size(480, 25);
            lblDeskripsi.TabIndex = 3;
            lblDeskripsi.Text = "Ceritakan keluhannya:";
            // 
            // txtDeskripsi
            // 
            txtDeskripsi.Font = new Font("Segoe UI", 11F);
            txtDeskripsi.Location = new Point(35, 190);
            txtDeskripsi.Multiline = true;
            txtDeskripsi.Name = "txtDeskripsi";
            txtDeskripsi.ScrollBars = ScrollBars.Vertical;
            txtDeskripsi.Size = new Size(480, 140);
            txtDeskripsi.TabIndex = 4;
            // 
            // btnKirim
            // 
            btnKirim.BackColor = Color.FromArgb(200, 182, 255);
            btnKirim.Cursor = Cursors.Hand;
            btnKirim.FlatAppearance.BorderColor = Color.FromArgb(253, 255, 182);
            btnKirim.FlatAppearance.BorderSize = 2;
            btnKirim.FlatStyle = FlatStyle.Flat;
            btnKirim.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
            btnKirim.ForeColor = Color.FromArgb(36, 0, 70);
            btnKirim.Location = new Point(35, 355);
            btnKirim.Name = "btnKirim";
            btnKirim.Size = new Size(480, 45);
            btnKirim.TabIndex = 5;
            btnKirim.Text = "KIRIM ADUAN 🚀";
            btnKirim.UseVisualStyleBackColor = false;
            btnKirim.Click += btnKirim_Click;
            // 
            // btnLihatAduanSaya
            // 
            btnLihatAduanSaya.BackColor = Color.FromArgb(253, 255, 182);
            btnLihatAduanSaya.Cursor = Cursors.Hand;
            btnLihatAduanSaya.FlatAppearance.BorderColor = Color.FromArgb(200, 182, 255);
            btnLihatAduanSaya.FlatAppearance.BorderSize = 2;
            btnLihatAduanSaya.FlatStyle = FlatStyle.Flat;
            btnLihatAduanSaya.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLihatAduanSaya.ForeColor = Color.FromArgb(36, 0, 70);
            btnLihatAduanSaya.Location = new Point(35, 415);
            btnLihatAduanSaya.Name = "btnLihatAduanSaya";
            btnLihatAduanSaya.Size = new Size(480, 40);
            btnLihatAduanSaya.TabIndex = 6;
            btnLihatAduanSaya.Text = "📝 Lihat Aduan Saya";
            btnLihatAduanSaya.UseVisualStyleBackColor = false;
            btnLihatAduanSaya.Click += btnLihatAduanSaya_Click;
            // 
            // ComplaintControl
            // 
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(pnlCard);
            Name = "ComplaintControl";
            Size = new Size(1054, 597);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubjek;
        private System.Windows.Forms.TextBox txtSubjek;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Button btnKirim;
        private System.Windows.Forms.Button btnLihatAduanSaya;
    }
}
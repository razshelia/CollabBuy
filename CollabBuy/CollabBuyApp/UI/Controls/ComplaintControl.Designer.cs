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
            // 
            // pnlCard
            // 
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubjek);
            this.pnlCard.Controls.Add(this.txtSubjek);
            this.pnlCard.Controls.Add(this.lblDeskripsi);
            this.pnlCard.Controls.Add(this.txtDeskripsi);
            this.pnlCard.Controls.Add(this.btnKirim);
            this.pnlCard.Controls.Add(this.btnLihatAduanSaya);
            this.pnlCard.Location = new System.Drawing.Point(248, 115);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(550, 500);
            this.pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblTitle.Location = new System.Drawing.Point(35, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ADA KENDALA? SPILL SINI! 📢";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubjek
            // 
            this.lblSubjek.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubjek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblSubjek.Location = new System.Drawing.Point(35, 90);
            this.lblSubjek.Name = "lblSubjek";
            this.lblSubjek.Size = new System.Drawing.Size(480, 25);
            this.lblSubjek.TabIndex = 1;
            this.lblSubjek.Text = "Subjek Aduan:";
            // 
            // txtSubjek
            // 
            this.txtSubjek.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSubjek.Location = new System.Drawing.Point(35, 115);
            this.txtSubjek.Name = "txtSubjek";
            this.txtSubjek.Size = new System.Drawing.Size(480, 27);
            this.txtSubjek.TabIndex = 2;
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(35, 160);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(480, 25);
            this.lblDeskripsi.TabIndex = 3;
            this.lblDeskripsi.Text = "Ceritakan keluhannya:";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDeskripsi.Location = new System.Drawing.Point(35, 190);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDeskripsi.Size = new System.Drawing.Size(480, 140);
            this.txtDeskripsi.TabIndex = 4;
            // 
            // btnKirim
            // 
            this.btnKirim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnKirim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKirim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnKirim.FlatAppearance.BorderSize = 2;
            this.btnKirim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirim.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKirim.Location = new System.Drawing.Point(35, 355);
            this.btnKirim.Name = "btnKirim";
            this.btnKirim.Size = new System.Drawing.Size(480, 45);
            this.btnKirim.TabIndex = 5;
            this.btnKirim.Text = "KIRIM ADUAN 🚀";
            this.btnKirim.UseVisualStyleBackColor = false;
            this.btnKirim.Click += new System.EventHandler(this.btnKirim_Click);
            // 
            // btnLihatAduanSaya
            // 
            this.btnLihatAduanSaya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnLihatAduanSaya.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLihatAduanSaya.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnLihatAduanSaya.FlatAppearance.BorderSize = 2;
            this.btnLihatAduanSaya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLihatAduanSaya.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLihatAduanSaya.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnLihatAduanSaya.Location = new System.Drawing.Point(35, 415);
            this.btnLihatAduanSaya.Name = "btnLihatAduanSaya";
            this.btnLihatAduanSaya.Size = new System.Drawing.Size(480, 40);
            this.btnLihatAduanSaya.TabIndex = 6;
            this.btnLihatAduanSaya.Text = "📝 Lihat Aduan Saya";
            this.btnLihatAduanSaya.UseVisualStyleBackColor = false;
            this.btnLihatAduanSaya.Click += new System.EventHandler(this.btnLihatAduanSaya_Click);
            // 
            // ComplaintControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlCard);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "ComplaintControl";
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

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
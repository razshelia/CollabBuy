namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class VerifikasiTokoControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvVerifikasi = new System.Windows.Forms.DataGridView();
            this.txtCariVerifikasi = new System.Windows.Forms.TextBox();
            this.pnlKTM = new System.Windows.Forms.Panel();
            this.lblKTM = new System.Windows.Forms.Label();
            this.pbKTM = new System.Windows.Forms.PictureBox();
            this.btnApprove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVerifikasi)).BeginInit();
            this.pnlKTM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKTM)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Text = "🏪 ACC Lapak Baru";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Text = "Cek baik-baik foto KTM-nya sebelum kasih ijin jualan ya min!";

            // txtCariVerifikasi
            this.txtCariVerifikasi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCariVerifikasi.Location = new System.Drawing.Point(38, 88);
            this.txtCariVerifikasi.Size = new System.Drawing.Size(300, 28);
            this.txtCariVerifikasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCariVerifikasi.PlaceholderText = "🔍 Cari nama penjual...";
            this.txtCariVerifikasi.Name = "txtCariVerifikasi";
            this.txtCariVerifikasi.TextChanged += new System.EventHandler(this.txtCariVerifikasi_TextChanged);

            // dgvVerifikasi
            this.dgvVerifikasi.BackgroundColor = System.Drawing.Color.White;
            this.dgvVerifikasi.Location = new System.Drawing.Point(38, 122);
            this.dgvVerifikasi.Size = new System.Drawing.Size(550, 480);
            this.dgvVerifikasi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVerifikasi.ReadOnly = true;
            this.dgvVerifikasi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVerifikasi_CellClick);

            // pnlKTM
            this.pnlKTM.BackColor = System.Drawing.Color.FromArgb(155, 246, 255); // Soft Cyan
            this.pnlKTM.Location = new System.Drawing.Point(610, 110);
            this.pnlKTM.Size = new System.Drawing.Size(350, 480);
            this.pnlKTM.Controls.Add(this.btnApprove);
            this.pnlKTM.Controls.Add(this.pbKTM);
            this.pnlKTM.Controls.Add(this.lblKTM);

            // lblKTM
            this.lblKTM.AutoSize = true;
            this.lblKTM.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblKTM.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblKTM.Location = new System.Drawing.Point(20, 20);
            this.lblKTM.Text = "Preview Foto KTM";

            // pbKTM
            this.pbKTM.BackColor = System.Drawing.Color.White;
            this.pbKTM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbKTM.Location = new System.Drawing.Point(24, 50);
            this.pbKTM.Size = new System.Drawing.Size(300, 350);
            this.pbKTM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // btnApprove
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnApprove.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnApprove.Location = new System.Drawing.Point(24, 415);
            this.btnApprove.Size = new System.Drawing.Size(300, 45);
            this.btnApprove.Text = "✅ ACC Lapak (Verified)";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);

            // Control Setup
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlKTM);
            this.Controls.Add(this.txtCariVerifikasi);
            this.Controls.Add(this.dgvVerifikasi);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.VerifikasiTokoControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvVerifikasi)).EndInit();
            this.pnlKTM.ResumeLayout(false);
            this.pnlKTM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKTM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblKTM;
        private System.Windows.Forms.DataGridView dgvVerifikasi;
        private System.Windows.Forms.Panel pnlKTM;
        private System.Windows.Forms.PictureBox pbKTM;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.TextBox txtCariVerifikasi;
    }
}
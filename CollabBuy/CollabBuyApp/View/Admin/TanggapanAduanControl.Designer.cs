namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class TanggapanAduanControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvAduan = new System.Windows.Forms.DataGridView();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblBalasan = new System.Windows.Forms.Label();
            this.txtBalasan = new System.Windows.Forms.TextBox();
            this.btnBalas = new System.Windows.Forms.Button();
            this.btnBlokir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAduan)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Text = "🚨 Curhatan Bestie (Aduan)";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Text = "Ada yang ribut nih min. Buruan cek dan kasih jalan keluarnya!";

            // dgvAduan
            this.dgvAduan.BackgroundColor = System.Drawing.Color.White;
            this.dgvAduan.Location = new System.Drawing.Point(38, 110);
            this.dgvAduan.Size = new System.Drawing.Size(550, 480);
            this.dgvAduan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAduan.ReadOnly = true;
            this.dgvAduan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAduan_CellClick);

            // pnlForm
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(255, 173, 173); // Soft Red
            this.pnlForm.Location = new System.Drawing.Point(610, 110);
            this.pnlForm.Size = new System.Drawing.Size(350, 480);
            this.pnlForm.Controls.Add(this.btnBlokir);
            this.pnlForm.Controls.Add(this.btnBalas);
            this.pnlForm.Controls.Add(this.txtBalasan);
            this.pnlForm.Controls.Add(this.lblBalasan);

            // lblBalasan
            this.lblBalasan.AutoSize = true;
            this.lblBalasan.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblBalasan.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblBalasan.Location = new System.Drawing.Point(20, 20);
            this.lblBalasan.Text = "Balasan / Tindakan Mimin";

            // txtBalasan
            this.txtBalasan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBalasan.Location = new System.Drawing.Point(24, 50);
            this.txtBalasan.Multiline = true;
            this.txtBalasan.Size = new System.Drawing.Size(300, 250);

            // btnBalas
            this.btnBalas.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnBalas.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnBalas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBalas.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnBalas.Location = new System.Drawing.Point(24, 320);
            this.btnBalas.Size = new System.Drawing.Size(300, 45);
            this.btnBalas.Text = "🚀 Kirim Balasan (Clear)";
            this.btnBalas.Click += new System.EventHandler(this.btnBalas_Click);

            // btnBlokir
            this.btnBlokir.BackColor = System.Drawing.Color.DarkRed;
            this.btnBlokir.ForeColor = System.Drawing.Color.White;
            this.btnBlokir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlokir.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnBlokir.Location = new System.Drawing.Point(24, 380);
            this.btnBlokir.Size = new System.Drawing.Size(300, 45);
            this.btnBlokir.Text = "💥 Blokir Penjual Nakal";
            this.btnBlokir.Click += new System.EventHandler(this.btnBlokir_Click);

            // Control Setup
            this.BackColor = System.Drawing.Color.White;
            this.AutoScroll = true;
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.dgvAduan);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.TanggapanAduanControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvAduan)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblTitle, lblSubtitle, lblBalasan;
        private System.Windows.Forms.DataGridView dgvAduan;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.TextBox txtBalasan;
        private System.Windows.Forms.Button btnBalas, btnBlokir;
    }
}
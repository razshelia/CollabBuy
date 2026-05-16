namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class PODetailControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblJudulPO = new System.Windows.Forms.Label();
            this.lblJenisPO = new System.Windows.Forms.Label();
            this.lblRekening = new System.Windows.Forms.Label();
            this.lblBatasWaktu = new System.Windows.Forms.Label();
            this.btnKembali = new System.Windows.Forms.Button();
            this.flowPanelProduk = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlHeader.Controls.Add(this.lblJudulPO);
            this.pnlHeader.Controls.Add(this.lblJenisPO);
            this.pnlHeader.Controls.Add(this.lblRekening);
            this.pnlHeader.Controls.Add(this.lblBatasWaktu);
            this.pnlHeader.Controls.Add(this.btnKembali);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1046, 140);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblJudulPO
            // 
            this.lblJudulPO.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblJudulPO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblJudulPO.Location = new System.Drawing.Point(20, 15);
            this.lblJudulPO.Name = "lblJudulPO";
            this.lblJudulPO.Size = new System.Drawing.Size(700, 40);
            this.lblJudulPO.TabIndex = 0;
            this.lblJudulPO.Text = "JUDUL PO";
            // 
            // lblJenisPO
            // 
            this.lblJenisPO.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblJenisPO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblJenisPO.Location = new System.Drawing.Point(25, 60);
            this.lblJenisPO.Name = "lblJenisPO";
            this.lblJenisPO.Size = new System.Drawing.Size(250, 25);
            this.lblJenisPO.TabIndex = 1;
            this.lblJenisPO.Text = "🏷️ Jenis: Biasa";
            // 
            // lblRekening
            // 
            this.lblRekening.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRekening.ForeColor = System.Drawing.Color.White;
            this.lblRekening.Location = new System.Drawing.Point(25, 85);
            this.lblRekening.Name = "lblRekening";
            this.lblRekening.Size = new System.Drawing.Size(400, 25);
            this.lblRekening.TabIndex = 2;
            this.lblRekening.Text = "💳 Rekening: -";
            // 
            // lblBatasWaktu
            // 
            this.lblBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBatasWaktu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblBatasWaktu.Location = new System.Drawing.Point(25, 110);
            this.lblBatasWaktu.Name = "lblBatasWaktu";
            this.lblBatasWaktu.Size = new System.Drawing.Size(400, 25);
            this.lblBatasWaktu.TabIndex = 3;
            this.lblBatasWaktu.Text = "⏳ Batas Waktu: -";
            // 
            // btnKembali
            // 
            this.btnKembali.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnKembali.FlatAppearance.BorderSize = 2;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKembali.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKembali.Location = new System.Drawing.Point(820, 20);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(200, 45);
            this.btnKembali.TabIndex = 4;
            this.btnKembali.Text = "⬅ Kembali ke Katalog";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // flowPanelProduk
            // 
            this.flowPanelProduk.AutoScroll = true;
            this.flowPanelProduk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flowPanelProduk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelProduk.Location = new System.Drawing.Point(0, 140);
            this.flowPanelProduk.Name = "flowPanelProduk";
            this.flowPanelProduk.Padding = new System.Windows.Forms.Padding(15);
            this.flowPanelProduk.Size = new System.Drawing.Size(1046, 590);
            this.flowPanelProduk.TabIndex = 0;
            // 
            // PODetailControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.flowPanelProduk);
            this.Controls.Add(this.pnlHeader);
            this.Name = "PODetailControl";
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudulPO, lblJenisPO, lblRekening, lblBatasWaktu;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
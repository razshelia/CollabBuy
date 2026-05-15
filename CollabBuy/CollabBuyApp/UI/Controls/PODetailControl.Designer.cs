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
            pnlHeader = new Panel();
            lblJudulPO = new Label();
            lblJenisPO = new Label();
            lblRekening = new Label();
            lblBatasWaktu = new Label();
            btnKembali = new Button();
            flowPanelProduk = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 27, 79);
            pnlHeader.Controls.Add(lblJudulPO);
            pnlHeader.Controls.Add(lblJenisPO);
            pnlHeader.Controls.Add(lblRekening);
            pnlHeader.Controls.Add(lblBatasWaktu);
            pnlHeader.Controls.Add(btnKembali);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1046, 160);
            pnlHeader.TabIndex = 1;
            // 
            // lblJudulPO
            // 
            lblJudulPO.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            lblJudulPO.ForeColor = Color.FromArgb(253, 224, 71);
            lblJudulPO.Location = new Point(20, 15);
            lblJudulPO.Name = "lblJudulPO";
            lblJudulPO.Size = new Size(700, 40);
            lblJudulPO.TabIndex = 0;
            lblJudulPO.Text = "Judul PO";
            // 
            // lblJenisPO
            // 
            lblJenisPO.Font = new Font("Segoe UI", 10F);
            lblJenisPO.ForeColor = Color.FromArgb(167, 139, 250);
            lblJenisPO.Location = new Point(20, 60);
            lblJenisPO.Name = "lblJenisPO";
            lblJenisPO.Size = new Size(200, 25);
            lblJenisPO.TabIndex = 1;
            lblJenisPO.Text = "Jenis: Biasa";
            // 
            // lblRekening
            // 
            lblRekening.Font = new Font("Segoe UI", 10F);
            lblRekening.ForeColor = Color.White;
            lblRekening.Location = new Point(20, 90);
            lblRekening.Name = "lblRekening";
            lblRekening.Size = new Size(400, 25);
            lblRekening.TabIndex = 2;
            lblRekening.Text = "Rekening: -";
            // 
            // lblBatasWaktu
            // 
            lblBatasWaktu.Font = new Font("Segoe UI", 10F);
            lblBatasWaktu.ForeColor = Color.White;
            lblBatasWaktu.Location = new Point(20, 120);
            lblBatasWaktu.Name = "lblBatasWaktu";
            lblBatasWaktu.Size = new Size(300, 25);
            lblBatasWaktu.TabIndex = 3;
            lblBatasWaktu.Text = "Batas Waktu: -";
            // 
            // btnKembali
            // 
            btnKembali.BackColor = Color.FromArgb(167, 139, 250);
            btnKembali.FlatAppearance.BorderSize = 0;
            btnKembali.FlatStyle = FlatStyle.Flat;
            btnKembali.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnKembali.ForeColor = Color.White;
            btnKembali.Location = new Point(600, 110);
            btnKembali.Name = "btnKembali";
            btnKembali.Size = new Size(160, 35);
            btnKembali.TabIndex = 4;
            btnKembali.Text = "⬅ Kembali ke Katalog";
            btnKembali.UseVisualStyleBackColor = false;
            btnKembali.Click += btnKembali_Click;
            // 
            // flowPanelProduk
            // 
            flowPanelProduk.AutoScroll = true;
            flowPanelProduk.BackColor = Color.FromArgb(255, 249, 230);
            flowPanelProduk.Dock = DockStyle.Fill;
            flowPanelProduk.Location = new Point(0, 160);
            flowPanelProduk.Name = "flowPanelProduk";
            flowPanelProduk.Padding = new Padding(15);
            flowPanelProduk.Size = new Size(1046, 173);
            flowPanelProduk.TabIndex = 0;
            // 
            // PODetailControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(flowPanelProduk);
            Controls.Add(pnlHeader);
            Name = "PODetailControl";
            Size = new Size(1046, 333);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudulPO, lblJenisPO, lblRekening, lblBatasWaktu;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class PreorderControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProduk = new System.Windows.Forms.Label();
            this.cmbProduk = new System.Windows.Forms.ComboBox();
            this.lblJudulPO = new System.Windows.Forms.Label();
            this.txtJudulPO = new System.Windows.Forms.TextBox();
            this.lblJenis = new System.Windows.Forms.Label();
            this.cmbJenis = new System.Windows.Forms.ComboBox();
            this.lblInfoRekening = new System.Windows.Forms.Label();
            this.txtInfoRekening = new System.Windows.Forms.TextBox();
            this.lblBatasWaktu = new System.Windows.Forms.Label();
            this.dtpBatasWaktu = new System.Windows.Forms.DateTimePicker();
            this.lblTargetKuota = new System.Windows.Forms.Label();
            this.txtTargetKuota = new System.Windows.Forms.TextBox();
            this.btnBuat = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.btnBuat);
            this.pnlCard.Controls.Add(this.txtTargetKuota);
            this.pnlCard.Controls.Add(this.lblTargetKuota);
            this.pnlCard.Controls.Add(this.dtpBatasWaktu);
            this.pnlCard.Controls.Add(this.lblBatasWaktu);
            this.pnlCard.Controls.Add(this.txtInfoRekening);
            this.pnlCard.Controls.Add(this.lblInfoRekening);
            this.pnlCard.Controls.Add(this.cmbJenis);
            this.pnlCard.Controls.Add(this.lblJenis);
            this.pnlCard.Controls.Add(this.txtJudulPO);
            this.pnlCard.Controls.Add(this.lblJudulPO);
            this.pnlCard.Controls.Add(this.cmbProduk);
            this.pnlCard.Controls.Add(this.lblProduk);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Location = new System.Drawing.Point(300, 30);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(460, 620);
            this.pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(460, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BUKA LAPAK PO 📢";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProduk
            // 
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblProduk.Location = new System.Drawing.Point(40, 80);
            this.lblProduk.Name = "lblProduk";
            this.lblProduk.Size = new System.Drawing.Size(380, 20);
            this.lblProduk.TabIndex = 1;
            this.lblProduk.Text = "Pilih Produk Master:";
            // 
            // cmbProduk
            // 
            this.cmbProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduk.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbProduk.FormattingEnabled = true;
            this.cmbProduk.Location = new System.Drawing.Point(40, 105);
            this.cmbProduk.Name = "cmbProduk";
            this.cmbProduk.Size = new System.Drawing.Size(380, 28);
            this.cmbProduk.TabIndex = 2;
            // 
            // lblJudulPO
            // 
            this.lblJudulPO.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJudulPO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblJudulPO.Location = new System.Drawing.Point(40, 145);
            this.lblJudulPO.Name = "lblJudulPO";
            this.lblJudulPO.Size = new System.Drawing.Size(380, 20);
            this.lblJudulPO.TabIndex = 3;
            this.lblJudulPO.Text = "Judul PO (Bikin yang Menarik!):";
            // 
            // txtJudulPO
            // 
            this.txtJudulPO.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtJudulPO.Location = new System.Drawing.Point(40, 170);
            this.txtJudulPO.Name = "txtJudulPO";
            this.txtJudulPO.Size = new System.Drawing.Size(380, 27);
            this.txtJudulPO.TabIndex = 4;
            // 
            // lblJenis
            // 
            this.lblJenis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJenis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblJenis.Location = new System.Drawing.Point(40, 210);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(380, 20);
            this.lblJenis.TabIndex = 5;
            this.lblJenis.Text = "Jenis PO:";
            // 
            // cmbJenis
            // 
            this.cmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenis.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbJenis.FormattingEnabled = true;
            this.cmbJenis.Items.AddRange(new object[] {
            "Biasa",
            "Gotong Royong"});
            this.cmbJenis.Location = new System.Drawing.Point(40, 235);
            this.cmbJenis.Name = "cmbJenis";
            this.cmbJenis.Size = new System.Drawing.Size(380, 28);
            this.cmbJenis.TabIndex = 6;
            this.cmbJenis.SelectedIndexChanged += new System.EventHandler(this.cmbJenis_SelectedIndexChanged);
            // 
            // lblInfoRekening
            // 
            this.lblInfoRekening.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInfoRekening.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblInfoRekening.Location = new System.Drawing.Point(40, 275);
            this.lblInfoRekening.Name = "lblInfoRekening";
            this.lblInfoRekening.Size = new System.Drawing.Size(380, 20);
            this.lblInfoRekening.TabIndex = 7;
            this.lblInfoRekening.Text = "Info Rekening (Bank/E-Wallet):";
            // 
            // txtInfoRekening
            // 
            this.txtInfoRekening.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtInfoRekening.Location = new System.Drawing.Point(40, 300);
            this.txtInfoRekening.Name = "txtInfoRekening";
            this.txtInfoRekening.Size = new System.Drawing.Size(380, 27);
            this.txtInfoRekening.TabIndex = 8;
            // 
            // lblBatasWaktu
            // 
            this.lblBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBatasWaktu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblBatasWaktu.Location = new System.Drawing.Point(40, 340);
            this.lblBatasWaktu.Name = "lblBatasWaktu";
            this.lblBatasWaktu.Size = new System.Drawing.Size(380, 20);
            this.lblBatasWaktu.TabIndex = 9;
            this.lblBatasWaktu.Text = "Batas Waktu Penutupan:";
            // 
            // dtpBatasWaktu
            // 
            this.dtpBatasWaktu.CustomFormat = "dd MMMM yyyy HH:mm";
            this.dtpBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpBatasWaktu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBatasWaktu.Location = new System.Drawing.Point(40, 365);
            this.dtpBatasWaktu.Name = "dtpBatasWaktu";
            this.dtpBatasWaktu.Size = new System.Drawing.Size(380, 27);
            this.dtpBatasWaktu.TabIndex = 10;
            // 
            // lblTargetKuota
            // 
            this.lblTargetKuota.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTargetKuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblTargetKuota.Location = new System.Drawing.Point(40, 405);
            this.lblTargetKuota.Name = "lblTargetKuota";
            this.lblTargetKuota.Size = new System.Drawing.Size(380, 20);
            this.lblTargetKuota.TabIndex = 11;
            this.lblTargetKuota.Text = "Target Kuota (Khusus Gotong Royong):";
            this.lblTargetKuota.Visible = false;
            // 
            // txtTargetKuota
            // 
            this.txtTargetKuota.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTargetKuota.Location = new System.Drawing.Point(40, 430);
            this.txtTargetKuota.Name = "txtTargetKuota";
            this.txtTargetKuota.Size = new System.Drawing.Size(380, 27);
            this.txtTargetKuota.TabIndex = 12;
            this.txtTargetKuota.Visible = false;
            // 
            // btnBuat
            // 
            this.btnBuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnBuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnBuat.FlatAppearance.BorderSize = 2;
            this.btnBuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuat.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnBuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnBuat.Location = new System.Drawing.Point(40, 485);
            this.btnBuat.Name = "btnBuat";
            this.btnBuat.Size = new System.Drawing.Size(380, 50);
            this.btnBuat.TabIndex = 13;
            this.btnBuat.Text = "BUKA LAPAK SEKARANG 🚀";
            this.btnBuat.UseVisualStyleBackColor = false;
            this.btnBuat.Click += new System.EventHandler(this.btnBuat_Click);
            // 
            // PreorderControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlCard);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "PreorderControl";
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProduk;
        private System.Windows.Forms.ComboBox cmbProduk;
        private System.Windows.Forms.Label lblJudulPO;
        private System.Windows.Forms.TextBox txtJudulPO;
        private System.Windows.Forms.Label lblJenis;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.Label lblInfoRekening;
        private System.Windows.Forms.TextBox txtInfoRekening;
        private System.Windows.Forms.Label lblBatasWaktu;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Label lblTargetKuota;
        private System.Windows.Forms.TextBox txtTargetKuota;
        private System.Windows.Forms.Button btnBuat;
    }
}
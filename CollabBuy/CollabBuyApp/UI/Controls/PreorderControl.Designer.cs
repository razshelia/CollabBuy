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

            // Produk ComboBox
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
            this.pnlCard.BackColor = System.Drawing.Color.White;
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
            this.pnlCard.Location = new System.Drawing.Point(100, 50);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(400, 560);
            this.pnlCard.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Buka Sesi Pre-Order 📢";

            // 
            // lblProduk
            // 
            this.lblProduk.AutoSize = true;
            this.lblProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProduk.Location = new System.Drawing.Point(25, 75);
            this.lblProduk.Name = "lblProduk";
            this.lblProduk.Size = new System.Drawing.Size(124, 19);
            this.lblProduk.TabIndex = 1;
            this.lblProduk.Text = "Pilih Produk Master";

            // 
            // cmbProduk
            // 
            this.cmbProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbProduk.FormattingEnabled = true;
            this.cmbProduk.Location = new System.Drawing.Point(29, 97);
            this.cmbProduk.Name = "cmbProduk";
            this.cmbProduk.Size = new System.Drawing.Size(340, 25);
            this.cmbProduk.TabIndex = 2;

            // 
            // lblJudulPO
            // 
            this.lblJudulPO.AutoSize = true;
            this.lblJudulPO.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblJudulPO.Location = new System.Drawing.Point(25, 140);
            this.lblJudulPO.Name = "lblJudulPO";
            this.lblJudulPO.Size = new System.Drawing.Size(65, 19);
            this.lblJudulPO.TabIndex = 3;
            this.lblJudulPO.Text = "Judul PO";

            // 
            // txtJudulPO
            // 
            this.txtJudulPO.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtJudulPO.Location = new System.Drawing.Point(29, 162);
            this.txtJudulPO.Name = "txtJudulPO";
            this.txtJudulPO.Size = new System.Drawing.Size(340, 25);
            this.txtJudulPO.TabIndex = 4;

            // 
            // lblJenis
            // 
            this.lblJenis.AutoSize = true;
            this.lblJenis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblJenis.Location = new System.Drawing.Point(25, 205);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(59, 19);
            this.lblJenis.TabIndex = 5;
            this.lblJenis.Text = "Jenis PO";

            // 
            // cmbJenis
            // 
            this.cmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbJenis.FormattingEnabled = true;
            this.cmbJenis.Items.AddRange(new object[] {
            "Biasa",
            "Gotong Royong"});
            this.cmbJenis.Location = new System.Drawing.Point(29, 227);
            this.cmbJenis.Name = "cmbJenis";
            this.cmbJenis.Size = new System.Drawing.Size(340, 25);
            this.cmbJenis.TabIndex = 6;
            this.cmbJenis.SelectedIndexChanged += new System.EventHandler(this.cmbJenis_SelectedIndexChanged);

            // 
            // lblInfoRekening
            // 
            this.lblInfoRekening.AutoSize = true;
            this.lblInfoRekening.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfoRekening.Location = new System.Drawing.Point(25, 270);
            this.lblInfoRekening.Name = "lblInfoRekening";
            this.lblInfoRekening.Size = new System.Drawing.Size(95, 19);
            this.lblInfoRekening.TabIndex = 7;
            this.lblInfoRekening.Text = "Info Rekening";

            // 
            // txtInfoRekening
            // 
            this.txtInfoRekening.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInfoRekening.Location = new System.Drawing.Point(29, 292);
            this.txtInfoRekening.Name = "txtInfoRekening";
            this.txtInfoRekening.Size = new System.Drawing.Size(340, 25);
            this.txtInfoRekening.TabIndex = 8;

            // 
            // lblBatasWaktu
            // 
            this.lblBatasWaktu.AutoSize = true;
            this.lblBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBatasWaktu.Location = new System.Drawing.Point(25, 335);
            this.lblBatasWaktu.Name = "lblBatasWaktu";
            this.lblBatasWaktu.Size = new System.Drawing.Size(86, 19);
            this.lblBatasWaktu.TabIndex = 9;
            this.lblBatasWaktu.Text = "Batas Waktu";

            // 
            // dtpBatasWaktu
            // 
            this.dtpBatasWaktu.CustomFormat = "dd MMMM yyyy HH:mm";
            this.dtpBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpBatasWaktu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBatasWaktu.Location = new System.Drawing.Point(29, 357);
            this.dtpBatasWaktu.Name = "dtpBatasWaktu";
            this.dtpBatasWaktu.Size = new System.Drawing.Size(340, 25);
            this.dtpBatasWaktu.TabIndex = 10;

            // 
            // lblTargetKuota
            // 
            this.lblTargetKuota.AutoSize = true;
            this.lblTargetKuota.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTargetKuota.Location = new System.Drawing.Point(25, 400);
            this.lblTargetKuota.Name = "lblTargetKuota";
            this.lblTargetKuota.Size = new System.Drawing.Size(256, 19);
            this.lblTargetKuota.TabIndex = 11;
            this.lblTargetKuota.Text = "Target Kuota (Khusus Gotong Royong)";
            this.lblTargetKuota.Visible = false;

            // 
            // txtTargetKuota
            // 
            this.txtTargetKuota.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTargetKuota.Location = new System.Drawing.Point(29, 422);
            this.txtTargetKuota.Name = "txtTargetKuota";
            this.txtTargetKuota.Size = new System.Drawing.Size(340, 25);
            this.txtTargetKuota.TabIndex = 12;
            this.txtTargetKuota.Visible = false;

            // 
            // btnBuat
            // 
            this.btnBuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(139)))), ((int)(((byte)(250)))));
            this.btnBuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuat.ForeColor = System.Drawing.Color.White;
            this.btnBuat.Location = new System.Drawing.Point(29, 485);
            this.btnBuat.Name = "btnBuat";
            this.btnBuat.Size = new System.Drawing.Size(340, 40);
            this.btnBuat.TabIndex = 13;
            this.btnBuat.Text = "BUKA LAPAK SEKARANG";
            this.btnBuat.UseVisualStyleBackColor = false;
            this.btnBuat.Click += new System.EventHandler(this.btnBuat_Click);

            // 
            // PreorderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlCard);
            this.Name = "PreorderControl";
            this.Size = new System.Drawing.Size(600, 650);
            this.Resize += new System.EventHandler(this.PreorderControl_Resize);
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
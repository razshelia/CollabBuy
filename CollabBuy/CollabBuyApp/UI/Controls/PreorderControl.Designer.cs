namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class PreorderControl
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
            lblJudul = new Label();
            txtJudulPO = new TextBox();
            lblJenis = new Label();
            cmbJenis = new ComboBox();
            lblRekening = new Label();
            txtInfoRekening = new TextBox();
            lblBatas = new Label();
            dtpBatasWaktu = new DateTimePicker();
            lblTargetKuota = new Label();
            txtTargetKuota = new TextBox();
            btnBuat = new Button();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.FromArgb(45, 27, 79);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblJudul);
            pnlCard.Controls.Add(txtJudulPO);
            pnlCard.Controls.Add(lblJenis);
            pnlCard.Controls.Add(cmbJenis);
            pnlCard.Controls.Add(lblRekening);
            pnlCard.Controls.Add(txtInfoRekening);
            pnlCard.Controls.Add(lblBatas);
            pnlCard.Controls.Add(dtpBatasWaktu);
            pnlCard.Controls.Add(lblTargetKuota);
            pnlCard.Controls.Add(txtTargetKuota);
            pnlCard.Controls.Add(btnBuat);
            pnlCard.Location = new Point(623, 216);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(500, 500);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Black", 18F);
            lblTitle.ForeColor = Color.FromArgb(253, 224, 71);
            lblTitle.Location = new Point(35, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(430, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BUKA PREORDER BARU 🚀";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblJudul
            // 
            lblJudul.AutoSize = true;
            lblJudul.ForeColor = Color.White;
            lblJudul.Location = new Point(40, 90);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(57, 15);
            lblJudul.TabIndex = 1;
            lblJudul.Text = "Judul PO:";
            // 
            // txtJudulPO
            // 
            txtJudulPO.Location = new Point(40, 110);
            txtJudulPO.Name = "txtJudulPO";
            txtJudulPO.PlaceholderText = "cth: PO Kaos BEM 2024";
            txtJudulPO.Size = new Size(420, 23);
            txtJudulPO.TabIndex = 2;
            // 
            // lblJenis
            // 
            lblJenis.AutoSize = true;
            lblJenis.ForeColor = Color.White;
            lblJenis.Location = new Point(40, 150);
            lblJenis.Name = "lblJenis";
            lblJenis.Size = new Size(54, 15);
            lblJenis.TabIndex = 3;
            lblJenis.Text = "Jenis PO:";
            // 
            // cmbJenis
            // 
            cmbJenis.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJenis.Items.AddRange(new object[] { "Biasa", "Gotong Royong" });
            cmbJenis.Location = new Point(40, 170);
            cmbJenis.Name = "cmbJenis";
            cmbJenis.Size = new Size(420, 23);
            cmbJenis.TabIndex = 4;
            cmbJenis.SelectedIndexChanged += cmbJenis_SelectedIndexChanged;
            // 
            // lblRekening
            // 
            lblRekening.AutoSize = true;
            lblRekening.ForeColor = Color.White;
            lblRekening.Location = new Point(40, 210);
            lblRekening.Name = "lblRekening";
            lblRekening.Size = new Size(83, 15);
            lblRekening.TabIndex = 5;
            lblRekening.Text = "Info Rekening:";
            // 
            // txtInfoRekening
            // 
            txtInfoRekening.Location = new Point(40, 230);
            txtInfoRekening.Name = "txtInfoRekening";
            txtInfoRekening.PlaceholderText = "cth: BCA 1234567890 a.n. Budi";
            txtInfoRekening.Size = new Size(420, 23);
            txtInfoRekening.TabIndex = 6;
            // 
            // lblBatas
            // 
            lblBatas.AutoSize = true;
            lblBatas.ForeColor = Color.White;
            lblBatas.Location = new Point(40, 270);
            lblBatas.Name = "lblBatas";
            lblBatas.Size = new Size(75, 15);
            lblBatas.TabIndex = 7;
            lblBatas.Text = "Batas Waktu:";
            // 
            // dtpBatasWaktu
            // 
            dtpBatasWaktu.Location = new Point(40, 290);
            dtpBatasWaktu.Name = "dtpBatasWaktu";
            dtpBatasWaktu.Size = new Size(420, 23);
            dtpBatasWaktu.TabIndex = 8;
            // 
            // lblTargetKuota
            // 
            lblTargetKuota.AutoSize = true;
            lblTargetKuota.ForeColor = Color.White;
            lblTargetKuota.Location = new Point(40, 330);
            lblTargetKuota.Name = "lblTargetKuota";
            lblTargetKuota.Size = new Size(77, 15);
            lblTargetKuota.TabIndex = 9;
            lblTargetKuota.Text = "Target Kuota:";
            lblTargetKuota.Visible = false;
            // 
            // txtTargetKuota
            // 
            txtTargetKuota.Location = new Point(40, 350);
            txtTargetKuota.Name = "txtTargetKuota";
            txtTargetKuota.PlaceholderText = "cth: 50";
            txtTargetKuota.Size = new Size(420, 23);
            txtTargetKuota.TabIndex = 10;
            txtTargetKuota.Visible = false;
            // 
            // btnBuat
            // 
            btnBuat.BackColor = Color.FromArgb(167, 139, 250);
            btnBuat.FlatAppearance.BorderSize = 0;
            btnBuat.FlatStyle = FlatStyle.Flat;
            btnBuat.Font = new Font("Segoe UI Black", 12F);
            btnBuat.ForeColor = Color.White;
            btnBuat.Location = new Point(40, 415);
            btnBuat.Name = "btnBuat";
            btnBuat.Size = new Size(420, 45);
            btnBuat.TabIndex = 11;
            btnBuat.Text = "BUKA PO SEKARANG ✨";
            btnBuat.UseVisualStyleBackColor = false;
            btnBuat.Click += btnBuat_Click;
            // 
            // PreorderControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(pnlCard);
            Name = "PreorderControl";
            Size = new Size(1046, 333);
            Resize += PreorderControl_Resize;
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblJudul, lblJenis, lblRekening, lblBatas, lblTargetKuota;
        private System.Windows.Forms.TextBox txtJudulPO, txtInfoRekening, txtTargetKuota;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Button btnBuat;
    }
}
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
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblJudul = new System.Windows.Forms.Label();
            this.txtJudulPO = new System.Windows.Forms.TextBox();
            this.lblJenis = new System.Windows.Forms.Label();
            this.cmbJenis = new System.Windows.Forms.ComboBox();
            this.lblRekening = new System.Windows.Forms.Label();
            this.txtInfoRekening = new System.Windows.Forms.TextBox();
            this.lblBatas = new System.Windows.Forms.Label();
            this.dtpBatasWaktu = new System.Windows.Forms.DateTimePicker();
            this.lblTargetKuota = new System.Windows.Forms.Label();
            this.txtTargetKuota = new System.Windows.Forms.TextBox();
            this.btnBuat = new System.Windows.Forms.Button();

            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(255, 249, 230);

            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            this.pnlCard.Size = new System.Drawing.Size(500, 500);
            this.pnlCard.Location = new System.Drawing.Point((this.ClientSize.Width - 500) / 2, (this.ClientSize.Height - 500) / 2);
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;

            this.lblTitle.Text = "BUKA PREORDER BARU 🚀";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(253, 224, 71);
            this.lblTitle.Size = new System.Drawing.Size(430, 40);
            this.lblTitle.Location = new System.Drawing.Point(35, 30);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblJudul.Text = "Judul PO:";
            this.lblJudul.ForeColor = System.Drawing.Color.White;
            this.lblJudul.Location = new System.Drawing.Point(40, 90);
            this.txtJudulPO.Location = new System.Drawing.Point(40, 110);
            this.txtJudulPO.Size = new System.Drawing.Size(420, 27);

            this.lblJenis.Text = "Jenis PO:";
            this.lblJenis.ForeColor = System.Drawing.Color.White;
            this.lblJenis.Location = new System.Drawing.Point(40, 150);
            this.cmbJenis.Items.AddRange(new string[] { "Biasa", "Gotong Royong" });
            this.cmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenis.Location = new System.Drawing.Point(40, 170);
            this.cmbJenis.Size = new System.Drawing.Size(420, 27);
            this.cmbJenis.SelectedIndexChanged += new System.EventHandler(this.cmbJenis_SelectedIndexChanged);

            this.lblRekening.Text = "Info Rekening:";
            this.lblRekening.ForeColor = System.Drawing.Color.White;
            this.lblRekening.Location = new System.Drawing.Point(40, 210);
            this.txtInfoRekening.Location = new System.Drawing.Point(40, 230);
            this.txtInfoRekening.Size = new System.Drawing.Size(420, 27);

            this.lblBatas.Text = "Batas Waktu:";
            this.lblBatas.ForeColor = System.Drawing.Color.White;
            this.lblBatas.Location = new System.Drawing.Point(40, 270);
            this.dtpBatasWaktu.Location = new System.Drawing.Point(40, 290);
            this.dtpBatasWaktu.Size = new System.Drawing.Size(420, 27);

            this.lblTargetKuota.Text = "Target Kuota:";
            this.lblTargetKuota.ForeColor = System.Drawing.Color.White;
            this.lblTargetKuota.Location = new System.Drawing.Point(40, 330);
            this.txtTargetKuota.Location = new System.Drawing.Point(40, 350);
            this.txtTargetKuota.Size = new System.Drawing.Size(420, 27);
            this.lblTargetKuota.Visible = this.txtTargetKuota.Visible = false;

            this.btnBuat.Text = "BUKA PO SEKARANG ✨";
            this.btnBuat.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            this.btnBuat.ForeColor = System.Drawing.Color.White;
            this.btnBuat.Font = new System.Drawing.Font("Segoe UI Black", 12F);
            this.btnBuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuat.Location = new System.Drawing.Point(40, 400);
            this.btnBuat.Size = new System.Drawing.Size(420, 45);
            this.btnBuat.Click += new System.EventHandler(this.btnBuat_Click);

            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblJudul);
            this.pnlCard.Controls.Add(this.txtJudulPO);
            this.pnlCard.Controls.Add(this.lblJenis);
            this.pnlCard.Controls.Add(this.cmbJenis);
            this.pnlCard.Controls.Add(this.lblRekening);
            this.pnlCard.Controls.Add(this.txtInfoRekening);
            this.pnlCard.Controls.Add(this.lblBatas);
            this.pnlCard.Controls.Add(this.dtpBatasWaktu);
            this.pnlCard.Controls.Add(this.lblTargetKuota);
            this.pnlCard.Controls.Add(this.txtTargetKuota);
            this.pnlCard.Controls.Add(this.btnBuat);

            this.Controls.Add(this.pnlCard);
            this.pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle, lblJudul, lblJenis, lblRekening, lblBatas, lblTargetKuota;
        private System.Windows.Forms.TextBox txtJudulPO, txtInfoRekening, txtTargetKuota;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Button btnBuat;
    }
}
namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    partial class BukaSesiPOControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblNamaSesi = new System.Windows.Forms.Label();
            this.txtNamaSesi = new System.Windows.Forms.TextBox();
            this.lblJenis = new System.Windows.Forms.Label();
            this.cbJenisPO = new System.Windows.Forms.ComboBox();
            this.lblBatasWaktu = new System.Windows.Forms.Label();
            this.dtpBatasWaktu = new System.Windows.Forms.DateTimePicker();
            this.lblRekening = new System.Windows.Forms.Label();
            this.txtRekening = new System.Windows.Forms.TextBox();
            this.btnSimpanSesi = new System.Windows.Forms.Button();
            // Kontrol tersembunyi — tetap dideklarasikan agar tidak error kompilasi
            this.lblProduk = new System.Windows.Forms.Label();
            this.cbProduk = new System.Windows.Forms.ComboBox();
            this.lblQuota = new System.Windows.Forms.Label();
            this.numQuota = new System.Windows.Forms.NumericUpDown();

            ((System.ComponentModel.ISupportInitialize)(this.numQuota)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Text = "✨ Launching Sesi PO Baru";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 68);
            this.lblSubtitle.Text = "Spill produk jualan lo di sini, atur kuota, dan cuan bareng! 💸";

            // ── Baris 1: Nama Sesi + Tipe PO (Y=25 label, Y=48 input) ──
            this.lblNamaSesi.AutoSize = true;
            this.lblNamaSesi.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamaSesi.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNamaSesi.Location = new System.Drawing.Point(30, 25);
            this.lblNamaSesi.Text = "Nama Sesi PO / Danus";

            this.txtNamaSesi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNamaSesi.Location = new System.Drawing.Point(34, 48);
            this.txtNamaSesi.Size = new System.Drawing.Size(330, 27);

            this.lblJenis.AutoSize = true;
            this.lblJenis.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblJenis.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblJenis.Location = new System.Drawing.Point(384, 25);
            this.lblJenis.Text = "Tipe PO";

            this.cbJenisPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJenisPO.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbJenisPO.Items.AddRange(new object[] { "Biasa", "Gotong Royong" });
            this.cbJenisPO.Location = new System.Drawing.Point(388, 48);
            this.cbJenisPO.Size = new System.Drawing.Size(195, 28);

            // ── Baris 2: Waktu Tutup Lapak (Y=93 label, Y=115 input) ──
            this.lblBatasWaktu.AutoSize = true;
            this.lblBatasWaktu.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblBatasWaktu.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblBatasWaktu.Location = new System.Drawing.Point(30, 93);
            this.lblBatasWaktu.Text = "Waktu Tutup Lapak";

            this.dtpBatasWaktu.CustomFormat = "dd MMMM yyyy HH:mm";
            this.dtpBatasWaktu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpBatasWaktu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBatasWaktu.Location = new System.Drawing.Point(34, 115);
            this.dtpBatasWaktu.Size = new System.Drawing.Size(280, 27);

            // ── Baris 3: Info Rekening / QRIS (Y=160 label, Y=182 input) ──
            this.lblRekening.AutoSize = true;
            this.lblRekening.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lblRekening.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblRekening.Location = new System.Drawing.Point(30, 160);
            this.lblRekening.Text = "Info Rekening / QRIS";

            this.txtRekening.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtRekening.Location = new System.Drawing.Point(34, 182);
            this.txtRekening.Multiline = true;
            this.txtRekening.Size = new System.Drawing.Size(580, 70);
            this.txtRekening.Text = "BCA 1234567 a.n Jagoan Cuan";

            // ── Tombol Simpan (Y=270) ──
            this.btnSimpanSesi.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnSimpanSesi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanSesi.FlatAppearance.BorderSize = 0;
            this.btnSimpanSesi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanSesi.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.btnSimpanSesi.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnSimpanSesi.Location = new System.Drawing.Point(34, 270);
            this.btnSimpanSesi.Size = new System.Drawing.Size(580, 48);
            this.btnSimpanSesi.Text = "🚀 Gas Launching Sesi!";
            this.btnSimpanSesi.UseVisualStyleBackColor = false;
            this.btnSimpanSesi.Click += new System.EventHandler(this.btnSimpanSesi_Click);

            // ── Kontrol tersembunyi (tidak ditampilkan, hanya agar tidak error) ──
            this.lblProduk.Visible = false;
            this.cbProduk.Visible = false;
            this.lblQuota.Visible = false;
            this.numQuota.Visible = false;
            this.numQuota.Minimum = 1;
            this.numQuota.Maximum = 1000;
            this.numQuota.Value = 10;

            // pnlForm
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(224, 170, 255);
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlForm.Location = new System.Drawing.Point(36, 110);
            this.pnlForm.Size = new System.Drawing.Size(650, 340);
            this.pnlForm.Controls.Add(this.lblNamaSesi);
            this.pnlForm.Controls.Add(this.txtNamaSesi);
            this.pnlForm.Controls.Add(this.lblJenis);
            this.pnlForm.Controls.Add(this.cbJenisPO);
            this.pnlForm.Controls.Add(this.lblBatasWaktu);
            this.pnlForm.Controls.Add(this.dtpBatasWaktu);
            this.pnlForm.Controls.Add(this.lblRekening);
            this.pnlForm.Controls.Add(this.txtRekening);
            this.pnlForm.Controls.Add(this.btnSimpanSesi);
            this.pnlForm.Controls.Add(this.lblProduk);
            this.pnlForm.Controls.Add(this.cbProduk);
            this.pnlForm.Controls.Add(this.lblQuota);
            this.pnlForm.Controls.Add(this.numQuota);

            // BukaSesiPOControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "BukaSesiPOControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.BukaSesiPOControl_Load);

            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuota)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblNamaSesi;
        private System.Windows.Forms.TextBox txtNamaSesi;
        private System.Windows.Forms.Label lblJenis;
        private System.Windows.Forms.ComboBox cbJenisPO;
        private System.Windows.Forms.Label lblProduk;
        private System.Windows.Forms.ComboBox cbProduk;
        private System.Windows.Forms.Label lblQuota;
        private System.Windows.Forms.NumericUpDown numQuota;
        private System.Windows.Forms.Label lblBatasWaktu;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Label lblRekening;
        private System.Windows.Forms.TextBox txtRekening;
        private System.Windows.Forms.Button btnSimpanSesi;
    }
}
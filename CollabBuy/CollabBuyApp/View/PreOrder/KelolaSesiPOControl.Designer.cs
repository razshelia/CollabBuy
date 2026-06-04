namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    partial class KelolaSesiPOControl
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPO = new System.Windows.Forms.DataGridView();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.lblEditTitle = new System.Windows.Forms.Label();
            this.lblJudul = new System.Windows.Forms.Label();
            this.txtJudul = new System.Windows.Forms.TextBox();
            this.lblJenis = new System.Windows.Forms.Label();
            this.cbJenis = new System.Windows.Forms.ComboBox();
            this.lblBatas = new System.Windows.Forms.Label();
            this.dtpBatas = new System.Windows.Forms.DateTimePicker();
            this.lblRekening = new System.Windows.Forms.Label();
            this.txtRekening = new System.Windows.Forms.TextBox();
            this.btnSimpanEdit = new System.Windows.Forms.Button();
            this.btnHapusPO = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPO)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Text = "⚙️ Kelola Sesi PO Kamu";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.lblSubtitle.Location = new System.Drawing.Point(34, 63);
            this.lblSubtitle.Text = "Klik baris PO untuk edit atau hapus. Hapus = soft delete (data aman di DB) 🔒";

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Black", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Location = new System.Drawing.Point(36, 95);
            this.btnRefresh.Size = new System.Drawing.Size(120, 32);
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvPO
            this.dgvPO.AllowUserToAddRows = false;
            this.dgvPO.AllowUserToDeleteRows = false;
            this.dgvPO.BackgroundColor = System.Drawing.Color.White;
            this.dgvPO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.dgvPO.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvPO.ColumnHeadersHeight = 40;
            this.dgvPO.EnableHeadersVisualStyles = false;
            this.dgvPO.Location = new System.Drawing.Point(36, 140);
            this.dgvPO.MultiSelect = false;
            this.dgvPO.ReadOnly = true;
            this.dgvPO.RowHeadersVisible = false;
            this.dgvPO.RowTemplate.Height = 38;
            this.dgvPO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPO.Size = new System.Drawing.Size(920, 200);
            this.dgvPO.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPO_CellClick);

            // ── pnlEdit: konfigurasi semua kontrol DULU, baru Controls.Add ──

            // lblEditTitle
            this.lblEditTitle.AutoSize = true;
            this.lblEditTitle.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Bold);
            this.lblEditTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblEditTitle.Location = new System.Drawing.Point(15, 12);
            this.lblEditTitle.Text = "✏️ Edit Sesi PO yang Dipilih";

            // lblJudul + txtJudul
            this.lblJudul.AutoSize = true;
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblJudul.Location = new System.Drawing.Point(15, 48);
            this.lblJudul.Text = "Nama Sesi";

            this.txtJudul.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtJudul.Location = new System.Drawing.Point(15, 66);
            this.txtJudul.Size = new System.Drawing.Size(260, 27);

            // lblJenis + cbJenis
            this.lblJenis.AutoSize = true;
            this.lblJenis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblJenis.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblJenis.Location = new System.Drawing.Point(290, 48);
            this.lblJenis.Text = "Tipe PO";

            this.cbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJenis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbJenis.Items.AddRange(new object[] { "Biasa", "Gotong Royong" });
            this.cbJenis.Location = new System.Drawing.Point(290, 66);
            this.cbJenis.Size = new System.Drawing.Size(160, 28);

            // lblBatas + dtpBatas
            this.lblBatas.AutoSize = true;
            this.lblBatas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatas.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblBatas.Location = new System.Drawing.Point(465, 48);
            this.lblBatas.Text = "Waktu Tutup";

            this.dtpBatas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpBatas.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBatas.Location = new System.Drawing.Point(465, 66);
            this.dtpBatas.MinDate = System.DateTime.Now;
            this.dtpBatas.Size = new System.Drawing.Size(180, 27);

            // btnSimpanEdit + btnHapusPO (di sebelah kanan baris atas)
            this.btnSimpanEdit.BackColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.btnSimpanEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanEdit.Enabled = false;
            this.btnSimpanEdit.FlatAppearance.BorderSize = 0;
            this.btnSimpanEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanEdit.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnSimpanEdit.ForeColor = System.Drawing.Color.White;
            this.btnSimpanEdit.Location = new System.Drawing.Point(660, 58);
            this.btnSimpanEdit.Size = new System.Drawing.Size(130, 40);
            this.btnSimpanEdit.Text = "💾 Simpan Edit";
            this.btnSimpanEdit.UseVisualStyleBackColor = false;
            this.btnSimpanEdit.Click += new System.EventHandler(this.btnSimpanEdit_Click);

            this.btnHapusPO.BackColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.btnHapusPO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapusPO.Enabled = false;
            this.btnHapusPO.FlatAppearance.BorderSize = 0;
            this.btnHapusPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapusPO.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnHapusPO.ForeColor = System.Drawing.Color.White;
            this.btnHapusPO.Location = new System.Drawing.Point(800, 58);
            this.btnHapusPO.Size = new System.Drawing.Size(110, 40);
            this.btnHapusPO.Text = "🗑️ Hapus PO";
            this.btnHapusPO.UseVisualStyleBackColor = false;
            this.btnHapusPO.Click += new System.EventHandler(this.btnHapusPO_Click);

            // lblRekening + txtRekening (baris bawah)
            this.lblRekening.AutoSize = true;
            this.lblRekening.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRekening.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblRekening.Location = new System.Drawing.Point(15, 113);
            this.lblRekening.Text = "Info Rekening / QRIS (wajib diisi ulang) *";

            this.txtRekening.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRekening.Location = new System.Drawing.Point(15, 131);
            this.txtRekening.Size = new System.Drawing.Size(895, 27);

            // pnlEdit — Controls.Add SETELAH semua kontrol dikonfigurasi
            this.pnlEdit.BackColor = System.Drawing.Color.FromArgb(235, 204, 255);
            this.pnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEdit.Location = new System.Drawing.Point(36, 360);
            this.pnlEdit.Size = new System.Drawing.Size(920, 170);
            this.pnlEdit.Controls.Add(this.lblEditTitle);
            this.pnlEdit.Controls.Add(this.lblJudul);
            this.pnlEdit.Controls.Add(this.txtJudul);
            this.pnlEdit.Controls.Add(this.lblJenis);
            this.pnlEdit.Controls.Add(this.cbJenis);
            this.pnlEdit.Controls.Add(this.lblBatas);
            this.pnlEdit.Controls.Add(this.dtpBatas);
            this.pnlEdit.Controls.Add(this.btnSimpanEdit);
            this.pnlEdit.Controls.Add(this.btnHapusPO);
            this.pnlEdit.Controls.Add(this.lblRekening);
            this.pnlEdit.Controls.Add(this.txtRekening);

            // KelolaSesiPOControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.dgvPO);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "KelolaSesiPOControl";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.KelolaSesiPOControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPO;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label lblEditTitle;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.TextBox txtJudul;
        private System.Windows.Forms.Label lblJenis;
        private System.Windows.Forms.ComboBox cbJenis;
        private System.Windows.Forms.Label lblBatas;
        private System.Windows.Forms.DateTimePicker dtpBatas;
        private System.Windows.Forms.Label lblRekening;
        private System.Windows.Forms.TextBox txtRekening;
        private System.Windows.Forms.Button btnSimpanEdit;
        private System.Windows.Forms.Button btnHapusPO;
    }
}
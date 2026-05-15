namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ProductFormControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlForm = new System.Windows.Forms.Panel();
            lblJudul = new System.Windows.Forms.Label();
            lblNamaProduk = new System.Windows.Forms.Label();
            txtNama = new System.Windows.Forms.TextBox();
            lblHarga = new System.Windows.Forms.Label();
            txtHarga = new System.Windows.Forms.TextBox();
            lblDiskon = new System.Windows.Forms.Label();
            txtDiskon = new System.Windows.Forms.TextBox();
            lblTarget = new System.Windows.Forms.Label();
            txtTarget = new System.Windows.Forms.TextBox();
            lblMinOrder = new System.Windows.Forms.Label();
            nudMinOrder = new System.Windows.Forms.NumericUpDown();
            lblKategori = new System.Windows.Forms.Label();
            cmbKategori = new System.Windows.Forms.ComboBox();
            lblDeskripsi = new System.Windows.Forms.Label();
            txtDeskripsi = new System.Windows.Forms.TextBox();
            btnUploadFoto = new System.Windows.Forms.Button();
            lblStatusFoto = new System.Windows.Forms.Label();
            btnSimpan = new System.Windows.Forms.Button();
            btnBatal = new System.Windows.Forms.Button();
            pictureBoxPreview = new System.Windows.Forms.PictureBox();

            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            SuspendLayout();

            // Layout (semua angka literal agar Designer tidak error):
            // ROW 0  Judul           y=15
            // ROW 1  Nama Produk     label y=58,  input y=76
            // ROW 2  Harga|Diskon    label y=112, input y=130
            // ROW 3  Target|MinOrder label y=166, input y=184
            // ROW 4  Kategori        label y=220, input y=238
            // ROW 5  Deskripsi       label y=274, textarea y=292  h=80
            // ROW 6  Upload Foto     y=387
            // ROW 7  Status Foto     y=430
            // ROW 8  Simpan|Batal    y=460

            // ── pnlForm ──────────────────────────────────────
            pnlForm.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            pnlForm.Location = new System.Drawing.Point(10, 10);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new System.Drawing.Size(560, 540);
            pnlForm.TabIndex = 0;
            pnlForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblJudul,
                lblNamaProduk, txtNama,
                lblHarga,      txtHarga,
                lblDiskon,     txtDiskon,
                lblTarget,     txtTarget,
                lblMinOrder,   nudMinOrder,
                lblKategori,   cmbKategori,
                lblDeskripsi,  txtDeskripsi,
                btnUploadFoto, lblStatusFoto,
                btnSimpan,     btnBatal
            });

            // ── ROW 0: Judul ──────────────────────────────────
            lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.FromArgb(253, 224, 71);
            lblJudul.Location = new System.Drawing.Point(20, 15);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new System.Drawing.Size(520, 30);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "Tambah/Edit Produk";

            // ── ROW 1: Nama Produk ───────────────────────────
            lblNamaProduk.AutoSize = true;
            lblNamaProduk.ForeColor = System.Drawing.Color.White;
            lblNamaProduk.Location = new System.Drawing.Point(20, 58);
            lblNamaProduk.Name = "lblNamaProduk";
            lblNamaProduk.TabIndex = 1;
            lblNamaProduk.Text = "Nama Produk:";

            txtNama.Location = new System.Drawing.Point(20, 76);
            txtNama.Name = "txtNama";
            txtNama.PlaceholderText = "cth: Kaos Polo BEM";
            txtNama.Size = new System.Drawing.Size(520, 23);
            txtNama.TabIndex = 2;

            // ── ROW 2: Harga Dasar (kiri) | Harga Diskon (kanan) ──
            lblHarga.AutoSize = true;
            lblHarga.ForeColor = System.Drawing.Color.White;
            lblHarga.Location = new System.Drawing.Point(20, 112);
            lblHarga.Name = "lblHarga";
            lblHarga.TabIndex = 3;
            lblHarga.Text = "Harga Dasar:";

            txtHarga.Location = new System.Drawing.Point(20, 130);
            txtHarga.Name = "txtHarga";
            txtHarga.PlaceholderText = "cth: 85000";
            txtHarga.Size = new System.Drawing.Size(240, 23);
            txtHarga.TabIndex = 4;

            lblDiskon.AutoSize = true;
            lblDiskon.ForeColor = System.Drawing.Color.White;
            lblDiskon.Location = new System.Drawing.Point(300, 112);
            lblDiskon.Name = "lblDiskon";
            lblDiskon.TabIndex = 5;
            lblDiskon.Text = "Harga Diskon (opsional):";

            txtDiskon.Location = new System.Drawing.Point(300, 130);
            txtDiskon.Name = "txtDiskon";
            txtDiskon.PlaceholderText = "cth: 75000";
            txtDiskon.Size = new System.Drawing.Size(240, 23);
            txtDiskon.TabIndex = 6;

            // ── ROW 3: Target Kuota (kiri) | Min Order (kanan) ──
            lblTarget.AutoSize = true;
            lblTarget.ForeColor = System.Drawing.Color.White;
            lblTarget.Location = new System.Drawing.Point(20, 166);
            lblTarget.Name = "lblTarget";
            lblTarget.TabIndex = 7;
            lblTarget.Text = "Target Kuota (opsional):";

            txtTarget.Location = new System.Drawing.Point(20, 184);
            txtTarget.Name = "txtTarget";
            txtTarget.PlaceholderText = "cth: 50";
            txtTarget.Size = new System.Drawing.Size(240, 23);
            txtTarget.TabIndex = 8;

            lblMinOrder.AutoSize = true;
            lblMinOrder.ForeColor = System.Drawing.Color.White;
            lblMinOrder.Location = new System.Drawing.Point(300, 166);
            lblMinOrder.Name = "lblMinOrder";
            lblMinOrder.TabIndex = 9;
            lblMinOrder.Text = "Minimal Order:";

            nudMinOrder.Location = new System.Drawing.Point(300, 184);
            nudMinOrder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMinOrder.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudMinOrder.Name = "nudMinOrder";
            nudMinOrder.Size = new System.Drawing.Size(120, 23);
            nudMinOrder.TabIndex = 10;

            // ── ROW 4: Kategori (penuh) ──────────────────────
            lblKategori.AutoSize = true;
            lblKategori.ForeColor = System.Drawing.Color.White;
            lblKategori.Location = new System.Drawing.Point(20, 220);
            lblKategori.Name = "lblKategori";
            lblKategori.TabIndex = 11;
            lblKategori.Text = "Kategori:";

            cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbKategori.Location = new System.Drawing.Point(20, 238);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new System.Drawing.Size(520, 23);
            cmbKategori.TabIndex = 12;

            // ── ROW 5: Deskripsi (penuh, multiline h=80) ─────
            lblDeskripsi.AutoSize = true;
            lblDeskripsi.ForeColor = System.Drawing.Color.White;
            lblDeskripsi.Location = new System.Drawing.Point(20, 274);
            lblDeskripsi.Name = "lblDeskripsi";
            lblDeskripsi.TabIndex = 13;
            lblDeskripsi.Text = "Deskripsi Produk (opsional):";

            txtDeskripsi.Location = new System.Drawing.Point(20, 292);
            txtDeskripsi.Multiline = true;
            txtDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDeskripsi.Name = "txtDeskripsi";
            txtDeskripsi.PlaceholderText = "Jelaskan detail produk, bahan, ukuran, warna, dll...";
            txtDeskripsi.Size = new System.Drawing.Size(520, 80);
            txtDeskripsi.TabIndex = 14;

            // ── ROW 6: Upload Foto ───────────────────────────
            btnUploadFoto.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            btnUploadFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUploadFoto.FlatAppearance.BorderSize = 0;
            btnUploadFoto.ForeColor = System.Drawing.Color.White;
            btnUploadFoto.Location = new System.Drawing.Point(20, 387);
            btnUploadFoto.Name = "btnUploadFoto";
            btnUploadFoto.Size = new System.Drawing.Size(200, 35);
            btnUploadFoto.TabIndex = 15;
            btnUploadFoto.Text = "📸 Upload Foto Produk";
            btnUploadFoto.UseVisualStyleBackColor = false;
            btnUploadFoto.Click += btnUploadFoto_Click;

            // ── ROW 7: Status Foto ───────────────────────────
            lblStatusFoto.ForeColor = System.Drawing.Color.FromArgb(167, 139, 250);
            lblStatusFoto.Location = new System.Drawing.Point(20, 430);
            lblStatusFoto.Name = "lblStatusFoto";
            lblStatusFoto.Size = new System.Drawing.Size(520, 20);
            lblStatusFoto.TabIndex = 16;
            lblStatusFoto.Text = "Belum ada foto dipilih";

            // ── ROW 8: Simpan & Batal ────────────────────────
            btnSimpan.BackColor = System.Drawing.Color.FromArgb(167, 139, 250);
            btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSimpan.FlatAppearance.BorderSize = 0;
            btnSimpan.Font = new System.Drawing.Font("Segoe UI Black", 10F);
            btnSimpan.ForeColor = System.Drawing.Color.White;
            btnSimpan.Location = new System.Drawing.Point(20, 460);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new System.Drawing.Size(240, 40);
            btnSimpan.TabIndex = 17;
            btnSimpan.Text = "➕ Tambah Produk";
            btnSimpan.UseVisualStyleBackColor = false;
            btnSimpan.Click += btnSimpan_Click;

            btnBatal.BackColor = System.Drawing.Color.FromArgb(100, 100, 120);
            btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBatal.FlatAppearance.BorderSize = 0;
            btnBatal.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnBatal.ForeColor = System.Drawing.Color.White;
            btnBatal.Location = new System.Drawing.Point(300, 460);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new System.Drawing.Size(240, 40);
            btnBatal.TabIndex = 18;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = false;
            btnBatal.Click += btnBatal_Click;

            // ── pictureBoxPreview (di luar pnlForm) ──────────
            pictureBoxPreview.Location = new System.Drawing.Point(590, 10);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new System.Drawing.Size(220, 200);
            pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxPreview.TabIndex = 0;
            pictureBoxPreview.TabStop = false;

            // ── UserControl root ──────────────────────────────
            BackColor = System.Drawing.Color.FromArgb(255, 249, 230);
            Controls.Add(pnlForm);
            Controls.Add(pictureBoxPreview);
            Name = "ProductFormControl";
            Size = new System.Drawing.Size(830, 560);

            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblNamaProduk;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.Label lblDiskon;
        private System.Windows.Forms.TextBox txtDiskon;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label lblMinOrder;
        private System.Windows.Forms.NumericUpDown nudMinOrder;
        private System.Windows.Forms.Label lblKategori;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Button btnUploadFoto;
        private System.Windows.Forms.Label lblStatusFoto;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
    }
}
namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class CheckoutControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblNamaProduk = new Label();
            lblHargaSatuan = new Label();
            lblMinOrder = new Label();
            lblInfoPO = new Label();
            lblInfoRekening = new Label();
            pnlStep1 = new Panel();
            lblNama = new Label();
            txtNamaPenitip = new TextBox();
            lblJml = new Label();
            txtJumlah = new TextBox();
            lblCat = new Label();
            txtCatatan = new TextBox();
            btnTambahPenitip = new Button();
            listBoxPenitip = new ListBox();
            lblTotalPenitip = new Label();
            btnHapusPenitip = new Button();
            btnLanjutkan = new Button();
            pnlStep2 = new Panel();
            lblRingkasanJudul = new Label();
            lblRingkasanProduk = new Label();
            lblRingkasanJumlah = new Label();
            lblRingkasanHargaSatuan = new Label();
            lblRingkasanTotal = new Label();
            lblStep2Rekening = new Label();
            btnUploadBukti = new Button();
            pictureBoxBukti = new PictureBox();
            lblStatusUpload = new Label();
            btnKonfirmasi = new Button();
            btnKembaliStep1 = new Button();
            pnlStep1.SuspendLayout();
            pnlStep2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBukti).BeginInit();
            SuspendLayout();
            // 
            // lblNamaProduk
            // 
            lblNamaProduk.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblNamaProduk.ForeColor = Color.FromArgb(45, 27, 79);
            lblNamaProduk.Location = new Point(20, 15);
            lblNamaProduk.Name = "lblNamaProduk";
            lblNamaProduk.Size = new Size(700, 35);
            lblNamaProduk.TabIndex = 0;
            lblNamaProduk.Text = "Nama Produk";
            // 
            // lblHargaSatuan
            // 
            lblHargaSatuan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHargaSatuan.ForeColor = Color.FromArgb(253, 224, 71);
            lblHargaSatuan.Location = new Point(20, 55);
            lblHargaSatuan.Name = "lblHargaSatuan";
            lblHargaSatuan.Size = new Size(200, 25);
            lblHargaSatuan.TabIndex = 1;
            lblHargaSatuan.Text = "Rp 0";
            // 
            // lblMinOrder
            // 
            lblMinOrder.Font = new Font("Segoe UI", 9F);
            lblMinOrder.ForeColor = Color.Gray;
            lblMinOrder.Location = new Point(20, 80);
            lblMinOrder.Name = "lblMinOrder";
            lblMinOrder.Size = new Size(200, 20);
            lblMinOrder.TabIndex = 2;
            lblMinOrder.Text = "Min order: 1 pcs";
            // 
            // lblInfoPO
            // 
            lblInfoPO.Font = new Font("Segoe UI", 9F);
            lblInfoPO.ForeColor = Color.FromArgb(167, 139, 250);
            lblInfoPO.Location = new Point(20, 105);
            lblInfoPO.Name = "lblInfoPO";
            lblInfoPO.Size = new Size(700, 20);
            lblInfoPO.TabIndex = 3;
            lblInfoPO.Text = "PO: ...";
            // 
            // lblInfoRekening
            // 
            lblInfoRekening.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblInfoRekening.ForeColor = Color.FromArgb(45, 27, 79);
            lblInfoRekening.Location = new Point(20, 125);
            lblInfoRekening.Name = "lblInfoRekening";
            lblInfoRekening.Size = new Size(700, 20);
            lblInfoRekening.TabIndex = 4;
            lblInfoRekening.Text = "Rekening: ...";
            // 
            // pnlStep1
            // 
            pnlStep1.BackColor = Color.White;
            pnlStep1.Controls.Add(lblNama);
            pnlStep1.Controls.Add(txtNamaPenitip);
            pnlStep1.Controls.Add(lblJml);
            pnlStep1.Controls.Add(txtJumlah);
            pnlStep1.Controls.Add(lblCat);
            pnlStep1.Controls.Add(txtCatatan);
            pnlStep1.Controls.Add(btnTambahPenitip);
            pnlStep1.Controls.Add(listBoxPenitip);
            pnlStep1.Controls.Add(lblTotalPenitip);
            pnlStep1.Controls.Add(btnHapusPenitip);
            pnlStep1.Controls.Add(btnLanjutkan);
            pnlStep1.Location = new Point(20, 155);
            pnlStep1.Name = "pnlStep1";
            pnlStep1.Size = new Size(940, 500);
            pnlStep1.TabIndex = 5;
            // 
            // lblNama
            // 
            lblNama.AutoSize = true;
            lblNama.ForeColor = Color.White;
            lblNama.Location = new Point(20, 15);
            lblNama.Name = "lblNama";
            lblNama.Size = new Size(82, 15);
            lblNama.TabIndex = 0;
            lblNama.Text = "Nama Penitip:";
            // 
            // txtNamaPenitip
            //
            txtNamaPenitip.Location = new Point(20, 40);
            txtNamaPenitip.Name = "txtNamaPenitip";
            txtNamaPenitip.Size = new Size(250, 23);
            txtNamaPenitip.TabIndex = 1;
            txtNamaPenitip.PlaceholderText = "cth: Budi Santoso";
            // 
            // lblJml
            // 
            lblJml.AutoSize = true;
            lblJml.ForeColor = Color.White;
            lblJml.Location = new Point(290, 15);
            lblJml.Name = "lblJml";
            lblJml.Size = new Size(95, 15);
            lblJml.TabIndex = 2;
            lblJml.Text = "Jumlah Pesanan:";
            // 
            // txtJumlah
            // 
            txtJumlah.Location = new Point(290, 40);
            txtJumlah.Name = "txtJumlah";
            txtJumlah.Size = new Size(100, 23);
            txtJumlah.TabIndex = 3;
            txtJumlah.PlaceholderText = "cth: 2";
            // 
            // lblCat
            // 
            lblCat.AutoSize = true;
            lblCat.ForeColor = Color.White;
            lblCat.Location = new Point(410, 15);
            lblCat.Name = "lblCat";
            lblCat.Size = new Size(107, 15);
            lblCat.TabIndex = 4;
            lblCat.Text = "Catatan (opsional):";
            // 
            // txtCatatan
            // 
            txtCatatan.Location = new Point(410, 40);
            txtCatatan.Name = "txtCatatan";
            txtCatatan.Size = new Size(300, 23);
            txtCatatan.TabIndex = 5;
            txtCatatan.PlaceholderText = "cth: Ukuran M, warna merah (opsional)";
            // 
            // btnTambahPenitip
            // 
            btnTambahPenitip.BackColor = Color.FromArgb(167, 139, 250);
            btnTambahPenitip.FlatAppearance.BorderSize = 0;
            btnTambahPenitip.FlatStyle = FlatStyle.Flat;
            btnTambahPenitip.ForeColor = Color.White;
            btnTambahPenitip.Location = new Point(20, 80);
            btnTambahPenitip.Name = "btnTambahPenitip";
            btnTambahPenitip.Size = new Size(160, 35);
            btnTambahPenitip.TabIndex = 6;
            btnTambahPenitip.Text = "➕ Tambah Penitip";
            btnTambahPenitip.UseVisualStyleBackColor = false;
            btnTambahPenitip.Click += btnTambahPenitip_Click;
            // 
            // listBoxPenitip
            // 
            listBoxPenitip.Location = new Point(20, 130);
            listBoxPenitip.Name = "listBoxPenitip";
            listBoxPenitip.Size = new Size(600, 244);
            listBoxPenitip.TabIndex = 7;
            // 
            // lblTotalPenitip
            // 
            lblTotalPenitip.Location = new Point(20, 390);
            lblTotalPenitip.Name = "lblTotalPenitip";
            lblTotalPenitip.Size = new Size(100, 23);
            lblTotalPenitip.TabIndex = 8;
            lblTotalPenitip.Text = "0 penitip";
            // 
            // btnHapusPenitip
            // 
            btnHapusPenitip.Location = new Point(150, 385);
            btnHapusPenitip.Name = "btnHapusPenitip";
            btnHapusPenitip.Size = new Size(75, 23);
            btnHapusPenitip.TabIndex = 9;
            btnHapusPenitip.Text = "🗑 Hapus Penitip";
            btnHapusPenitip.Click += btnHapusPenitip_Click;
            // 
            // btnLanjutkan
            // 
            btnLanjutkan.BackColor = Color.FromArgb(167, 139, 250);
            btnLanjutkan.FlatStyle = FlatStyle.Flat;
            btnLanjutkan.Font = new Font("Segoe UI Black", 12F);
            btnLanjutkan.ForeColor = Color.White;
            btnLanjutkan.Location = new Point(20, 420);
            btnLanjutkan.Name = "btnLanjutkan";
            btnLanjutkan.Size = new Size(600, 45);
            btnLanjutkan.TabIndex = 10;
            btnLanjutkan.Text = "LANJUTKAN KE PEMBAYARAN 💳";
            btnLanjutkan.UseVisualStyleBackColor = false;
            btnLanjutkan.Click += btnLanjutkan_Click;
            // 
            // pnlStep2
            // 
            pnlStep2.BackColor = Color.White;
            pnlStep2.Controls.Add(lblRingkasanJudul);
            pnlStep2.Controls.Add(lblRingkasanProduk);
            pnlStep2.Controls.Add(lblRingkasanJumlah);
            pnlStep2.Controls.Add(lblRingkasanHargaSatuan);
            pnlStep2.Controls.Add(lblRingkasanTotal);
            pnlStep2.Controls.Add(lblStep2Rekening);
            pnlStep2.Controls.Add(btnUploadBukti);
            pnlStep2.Controls.Add(pictureBoxBukti);
            pnlStep2.Controls.Add(lblStatusUpload);
            pnlStep2.Controls.Add(btnKonfirmasi);
            pnlStep2.Controls.Add(btnKembaliStep1);
            pnlStep2.Location = new Point(20, 155);
            pnlStep2.Name = "pnlStep2";
            pnlStep2.Size = new Size(940, 520);
            pnlStep2.TabIndex = 6;
            pnlStep2.Visible = false;
            // 
            // lblRingkasanJudul
            // 
            lblRingkasanJudul.Font = new Font("Segoe UI Black", 14F);
            lblRingkasanJudul.ForeColor = Color.FromArgb(45, 27, 79);
            lblRingkasanJudul.Location = new Point(20, 15);
            lblRingkasanJudul.Name = "lblRingkasanJudul";
            lblRingkasanJudul.Size = new Size(500, 30);
            lblRingkasanJudul.TabIndex = 0;
            lblRingkasanJudul.Text = "RINGKASAN PEMBAYARAN";
            // 
            // lblRingkasanProduk
            // 
            lblRingkasanProduk.Location = new Point(20, 55);
            lblRingkasanProduk.Name = "lblRingkasanProduk";
            lblRingkasanProduk.Size = new Size(100, 23);
            lblRingkasanProduk.TabIndex = 1;
            lblRingkasanProduk.Text = "...";
            // 
            // lblRingkasanJumlah
            // 
            lblRingkasanJumlah.Location = new Point(20, 80);
            lblRingkasanJumlah.Name = "lblRingkasanJumlah";
            lblRingkasanJumlah.Size = new Size(100, 23);
            lblRingkasanJumlah.TabIndex = 2;
            lblRingkasanJumlah.Text = "...";
            // 
            // lblRingkasanHargaSatuan
            // 
            lblRingkasanHargaSatuan.Location = new Point(20, 105);
            lblRingkasanHargaSatuan.Name = "lblRingkasanHargaSatuan";
            lblRingkasanHargaSatuan.Size = new Size(100, 23);
            lblRingkasanHargaSatuan.TabIndex = 3;
            lblRingkasanHargaSatuan.Text = "...";
            // 
            // lblRingkasanTotal
            // 
            lblRingkasanTotal.Font = new Font("Segoe UI Black", 16F);
            lblRingkasanTotal.ForeColor = Color.FromArgb(253, 224, 71);
            lblRingkasanTotal.Location = new Point(20, 140);
            lblRingkasanTotal.Name = "lblRingkasanTotal";
            lblRingkasanTotal.Size = new Size(300, 30);
            lblRingkasanTotal.TabIndex = 4;
            lblRingkasanTotal.Text = "Rp ...";
            // 
            // lblStep2Rekening
            // 
            lblStep2Rekening.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStep2Rekening.ForeColor = Color.FromArgb(45, 27, 79);
            lblStep2Rekening.Location = new Point(20, 190);
            lblStep2Rekening.Name = "lblStep2Rekening";
            lblStep2Rekening.Size = new Size(500, 40);
            lblStep2Rekening.TabIndex = 5;
            lblStep2Rekening.Text = "...";
            // 
            // btnUploadBukti
            // 
            btnUploadBukti.BackColor = Color.FromArgb(167, 139, 250);
            btnUploadBukti.FlatStyle = FlatStyle.Flat;
            btnUploadBukti.ForeColor = Color.White;
            btnUploadBukti.Location = new Point(20, 245);
            btnUploadBukti.Name = "btnUploadBukti";
            btnUploadBukti.Size = new Size(250, 40);
            btnUploadBukti.TabIndex = 6;
            btnUploadBukti.Text = "📸 UPLOAD BUKTI TRANSFER";
            btnUploadBukti.UseVisualStyleBackColor = false;
            btnUploadBukti.Click += btnUploadBukti_Click;
            // 
            // pictureBoxBukti
            // 
            pictureBoxBukti.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxBukti.Location = new Point(20, 295);
            pictureBoxBukti.Name = "pictureBoxBukti";
            pictureBoxBukti.Size = new Size(300, 150);
            pictureBoxBukti.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxBukti.TabIndex = 7;
            pictureBoxBukti.TabStop = false;
            // 
            // lblStatusUpload
            // 
            lblStatusUpload.Location = new Point(330, 295);
            lblStatusUpload.Name = "lblStatusUpload";
            lblStatusUpload.Size = new Size(100, 23);
            lblStatusUpload.TabIndex = 8;
            lblStatusUpload.Text = "Belum ada file dipilih";
            // 
            // btnKonfirmasi
            // 
            btnKonfirmasi.BackColor = Color.FromArgb(167, 139, 250);
            btnKonfirmasi.FlatStyle = FlatStyle.Flat;
            btnKonfirmasi.Font = new Font("Segoe UI Black", 12F);
            btnKonfirmasi.ForeColor = Color.White;
            btnKonfirmasi.Location = new Point(20, 455);
            btnKonfirmasi.Name = "btnKonfirmasi";
            btnKonfirmasi.Size = new Size(900, 45);
            btnKonfirmasi.TabIndex = 9;
            btnKonfirmasi.Text = "KONFIRMASI PESANAN ✅";
            btnKonfirmasi.UseVisualStyleBackColor = false;
            btnKonfirmasi.Click += btnKonfirmasi_Click;
            // 
            // btnKembaliStep1
            // 
            btnKembaliStep1.Location = new Point(750, 415);
            btnKembaliStep1.Name = "btnKembaliStep1";
            btnKembaliStep1.Size = new Size(75, 23);
            btnKembaliStep1.TabIndex = 10;
            btnKembaliStep1.Text = "⬅ Kembali edit penitip";
            btnKembaliStep1.Click += btnKembaliStep1_Click;
            // 
            // CheckoutControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(lblNamaProduk);
            Controls.Add(lblHargaSatuan);
            Controls.Add(lblMinOrder);
            Controls.Add(lblInfoPO);
            Controls.Add(lblInfoRekening);
            Controls.Add(pnlStep1);
            Controls.Add(pnlStep2);
            Name = "CheckoutControl";
            Padding = new Padding(20);
            Size = new Size(1046, 333);
            pnlStep1.ResumeLayout(false);
            pnlStep1.PerformLayout();
            pnlStep2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxBukti).EndInit();
            ResumeLayout(false);
        }

        // Deklarasi kontrol
        private System.Windows.Forms.Label lblNamaProduk, lblHargaSatuan, lblMinOrder, lblInfoPO, lblInfoRekening;
        private System.Windows.Forms.Panel pnlStep1, pnlStep2;
        private System.Windows.Forms.TextBox txtNamaPenitip, txtJumlah, txtCatatan;
        private System.Windows.Forms.Button btnTambahPenitip, btnHapusPenitip, btnLanjutkan;
        private System.Windows.Forms.ListBox listBoxPenitip;
        private System.Windows.Forms.Label lblTotalPenitip;
        private System.Windows.Forms.Label lblRingkasanJudul, lblRingkasanProduk, lblRingkasanJumlah, lblRingkasanHargaSatuan, lblRingkasanTotal, lblStep2Rekening, lblStatusUpload;
        private System.Windows.Forms.Button btnUploadBukti, btnKonfirmasi, btnKembaliStep1;
        private System.Windows.Forms.PictureBox pictureBoxBukti;
        private Label lblNama;
        private Label lblJml;
        private Label lblCat;
    }
}
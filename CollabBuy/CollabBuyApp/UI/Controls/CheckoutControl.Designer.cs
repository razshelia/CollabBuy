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
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.lblHargaSatuan = new System.Windows.Forms.Label();
            this.lblMinOrder = new System.Windows.Forms.Label();
            this.lblInfoPO = new System.Windows.Forms.Label();
            this.lblInfoRekening = new System.Windows.Forms.Label();
            this.pnlStep1 = new System.Windows.Forms.Panel();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNamaPenitip = new System.Windows.Forms.TextBox();
            this.lblJml = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.btnTambahPenitip = new System.Windows.Forms.Button();
            this.listBoxPenitip = new System.Windows.Forms.ListBox();
            this.lblTotalPenitip = new System.Windows.Forms.Label();
            this.btnHapusPenitip = new System.Windows.Forms.Button();
            this.btnLanjutkan = new System.Windows.Forms.Button();
            this.pnlStep2 = new System.Windows.Forms.Panel();
            this.lblRingkasanJudul = new System.Windows.Forms.Label();
            this.lblRingkasanProduk = new System.Windows.Forms.Label();
            this.lblRingkasanJumlah = new System.Windows.Forms.Label();
            this.lblRingkasanHargaSatuan = new System.Windows.Forms.Label();
            this.lblRingkasanTotal = new System.Windows.Forms.Label();
            this.lblStep2Rekening = new System.Windows.Forms.Label();
            this.btnUploadBukti = new System.Windows.Forms.Button();
            this.pictureBoxBukti = new System.Windows.Forms.PictureBox();
            this.lblStatusUpload = new System.Windows.Forms.Label();
            this.btnKonfirmasi = new System.Windows.Forms.Button();
            this.btnKembaliStep1 = new System.Windows.Forms.Button();
            this.pnlStep1.SuspendLayout();
            this.pnlStep2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBukti)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNamaProduk
            // 
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblNamaProduk.Location = new System.Drawing.Point(20, 15);
            this.lblNamaProduk.Name = "lblNamaProduk";
            this.lblNamaProduk.Size = new System.Drawing.Size(700, 40);
            this.lblNamaProduk.TabIndex = 0;
            this.lblNamaProduk.Text = "NAMA PRODUK";
            // 
            // lblHargaSatuan
            // 
            this.lblHargaSatuan.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblHargaSatuan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblHargaSatuan.Location = new System.Drawing.Point(20, 55);
            this.lblHargaSatuan.Name = "lblHargaSatuan";
            this.lblHargaSatuan.Size = new System.Drawing.Size(300, 35);
            this.lblHargaSatuan.TabIndex = 1;
            this.lblHargaSatuan.Text = "Rp 0";
            // 
            // lblMinOrder
            // 
            this.lblMinOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMinOrder.ForeColor = System.Drawing.Color.DimGray;
            this.lblMinOrder.Location = new System.Drawing.Point(20, 90);
            this.lblMinOrder.Name = "lblMinOrder";
            this.lblMinOrder.Size = new System.Drawing.Size(300, 25);
            this.lblMinOrder.TabIndex = 2;
            this.lblMinOrder.Text = "Min order: 1 pcs";
            // 
            // lblInfoPO
            // 
            this.lblInfoPO.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInfoPO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(139)))), ((int)(((byte)(250)))));
            this.lblInfoPO.Location = new System.Drawing.Point(20, 115);
            this.lblInfoPO.Name = "lblInfoPO";
            this.lblInfoPO.Size = new System.Drawing.Size(700, 25);
            this.lblInfoPO.TabIndex = 3;
            this.lblInfoPO.Text = "PO: ...";
            // 
            // lblInfoRekening
            // 
            this.lblInfoRekening.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInfoRekening.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblInfoRekening.Location = new System.Drawing.Point(20, 140);
            this.lblInfoRekening.Name = "lblInfoRekening";
            this.lblInfoRekening.Size = new System.Drawing.Size(700, 25);
            this.lblInfoRekening.TabIndex = 4;
            this.lblInfoRekening.Text = "Rekening: ...";
            // 
            // pnlStep1
            // 
            this.pnlStep1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlStep1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStep1.Controls.Add(this.lblNama);
            this.pnlStep1.Controls.Add(this.txtNamaPenitip);
            this.pnlStep1.Controls.Add(this.lblJml);
            this.pnlStep1.Controls.Add(this.txtJumlah);
            this.pnlStep1.Controls.Add(this.lblCat);
            this.pnlStep1.Controls.Add(this.txtCatatan);
            this.pnlStep1.Controls.Add(this.btnTambahPenitip);
            this.pnlStep1.Controls.Add(this.listBoxPenitip);
            this.pnlStep1.Controls.Add(this.lblTotalPenitip);
            this.pnlStep1.Controls.Add(this.btnHapusPenitip);
            this.pnlStep1.Controls.Add(this.btnLanjutkan);
            this.pnlStep1.Location = new System.Drawing.Point(20, 175);
            this.pnlStep1.Name = "pnlStep1";
            this.pnlStep1.Size = new System.Drawing.Size(1000, 520);
            this.pnlStep1.TabIndex = 5;
            // 
            // lblNama
            // 
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblNama.Location = new System.Drawing.Point(20, 20);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(250, 20);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "Nama Penitip:";
            // 
            // txtNamaPenitip
            // 
            this.txtNamaPenitip.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNamaPenitip.Location = new System.Drawing.Point(20, 45);
            this.txtNamaPenitip.Name = "txtNamaPenitip";
            this.txtNamaPenitip.Size = new System.Drawing.Size(250, 27);
            this.txtNamaPenitip.TabIndex = 1;
            // 
            // lblJml
            // 
            this.lblJml.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblJml.Location = new System.Drawing.Point(290, 20);
            this.lblJml.Name = "lblJml";
            this.lblJml.Size = new System.Drawing.Size(120, 20);
            this.lblJml.TabIndex = 2;
            this.lblJml.Text = "Jml Pesanan:";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtJumlah.Location = new System.Drawing.Point(290, 45);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(120, 27);
            this.txtJumlah.TabIndex = 3;
            // 
            // lblCat
            // 
            this.lblCat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblCat.Location = new System.Drawing.Point(430, 20);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(300, 20);
            this.lblCat.TabIndex = 4;
            this.lblCat.Text = "Catatan Khusus (Opsional):";
            // 
            // txtCatatan
            // 
            this.txtCatatan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCatatan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCatatan.Location = new System.Drawing.Point(430, 45);
            this.txtCatatan.Name = "txtCatatan";
            this.txtCatatan.Size = new System.Drawing.Size(370, 27);
            this.txtCatatan.TabIndex = 5;
            // 
            // btnTambahPenitip
            // 
            this.btnTambahPenitip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTambahPenitip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.btnTambahPenitip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambahPenitip.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnTambahPenitip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahPenitip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTambahPenitip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnTambahPenitip.Location = new System.Drawing.Point(820, 40);
            this.btnTambahPenitip.Name = "btnTambahPenitip";
            this.btnTambahPenitip.Size = new System.Drawing.Size(150, 35);
            this.btnTambahPenitip.TabIndex = 6;
            this.btnTambahPenitip.Text = "➕ Tambah";
            this.btnTambahPenitip.UseVisualStyleBackColor = false;
            this.btnTambahPenitip.Click += new System.EventHandler(this.btnTambahPenitip_Click);
            // 
            // listBoxPenitip
            // 
            this.listBoxPenitip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPenitip.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.listBoxPenitip.FormattingEnabled = true;
            this.listBoxPenitip.ItemHeight = 20;
            this.listBoxPenitip.Location = new System.Drawing.Point(20, 100);
            this.listBoxPenitip.Name = "listBoxPenitip";
            this.listBoxPenitip.Size = new System.Drawing.Size(950, 264);
            this.listBoxPenitip.TabIndex = 7;
            // 
            // lblTotalPenitip
            // 
            this.lblTotalPenitip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPenitip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPenitip.ForeColor = System.Drawing.Color.White;
            this.lblTotalPenitip.Location = new System.Drawing.Point(20, 385);
            this.lblTotalPenitip.Name = "lblTotalPenitip";
            this.lblTotalPenitip.Size = new System.Drawing.Size(200, 25);
            this.lblTotalPenitip.TabIndex = 8;
            this.lblTotalPenitip.Text = "Total: 0 penitip";
            // 
            // btnHapusPenitip
            // 
            this.btnHapusPenitip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHapusPenitip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.btnHapusPenitip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapusPenitip.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnHapusPenitip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapusPenitip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHapusPenitip.ForeColor = System.Drawing.Color.Black;
            this.btnHapusPenitip.Location = new System.Drawing.Point(820, 380);
            this.btnHapusPenitip.Name = "btnHapusPenitip";
            this.btnHapusPenitip.Size = new System.Drawing.Size(150, 30);
            this.btnHapusPenitip.TabIndex = 9;
            this.btnHapusPenitip.Text = "🗑 Hapus Dipilih";
            this.btnHapusPenitip.UseVisualStyleBackColor = false;
            this.btnHapusPenitip.Click += new System.EventHandler(this.btnHapusPenitip_Click);
            // 
            // btnLanjutkan
            // 
            this.btnLanjutkan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLanjutkan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnLanjutkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLanjutkan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnLanjutkan.FlatAppearance.BorderSize = 2;
            this.btnLanjutkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLanjutkan.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.btnLanjutkan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnLanjutkan.Location = new System.Drawing.Point(20, 430);
            this.btnLanjutkan.Name = "btnLanjutkan";
            this.btnLanjutkan.Size = new System.Drawing.Size(950, 60);
            this.btnLanjutkan.TabIndex = 10;
            this.btnLanjutkan.Text = "LANJUTKAN KE PEMBAYARAN 💳";
            this.btnLanjutkan.UseVisualStyleBackColor = false;
            this.btnLanjutkan.Click += new System.EventHandler(this.btnLanjutkan_Click);
            // 
            // pnlStep2
            // 
            this.pnlStep2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStep2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.pnlStep2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStep2.Controls.Add(this.lblRingkasanJudul);
            this.pnlStep2.Controls.Add(this.lblRingkasanProduk);
            this.pnlStep2.Controls.Add(this.lblRingkasanJumlah);
            this.pnlStep2.Controls.Add(this.lblRingkasanHargaSatuan);
            this.pnlStep2.Controls.Add(this.lblRingkasanTotal);
            this.pnlStep2.Controls.Add(this.lblStep2Rekening);
            this.pnlStep2.Controls.Add(this.btnUploadBukti);
            this.pnlStep2.Controls.Add(this.pictureBoxBukti);
            this.pnlStep2.Controls.Add(this.lblStatusUpload);
            this.pnlStep2.Controls.Add(this.btnKonfirmasi);
            this.pnlStep2.Controls.Add(this.btnKembaliStep1);
            this.pnlStep2.Location = new System.Drawing.Point(20, 175);
            this.pnlStep2.Name = "pnlStep2";
            this.pnlStep2.Size = new System.Drawing.Size(1000, 520);
            this.pnlStep2.TabIndex = 6;
            this.pnlStep2.Visible = false;
            // 
            // lblRingkasanJudul
            // 
            this.lblRingkasanJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F);
            this.lblRingkasanJudul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblRingkasanJudul.Location = new System.Drawing.Point(30, 20);
            this.lblRingkasanJudul.Name = "lblRingkasanJudul";
            this.lblRingkasanJudul.Size = new System.Drawing.Size(500, 40);
            this.lblRingkasanJudul.TabIndex = 0;
            this.lblRingkasanJudul.Text = "RINGKASAN PEMBAYARAN 🛒";
            // 
            // lblRingkasanProduk
            // 
            this.lblRingkasanProduk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRingkasanProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblRingkasanProduk.Location = new System.Drawing.Point(30, 70);
            this.lblRingkasanProduk.Name = "lblRingkasanProduk";
            this.lblRingkasanProduk.Size = new System.Drawing.Size(500, 25);
            this.lblRingkasanProduk.TabIndex = 1;
            this.lblRingkasanProduk.Text = "...";
            // 
            // lblRingkasanJumlah
            // 
            this.lblRingkasanJumlah.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblRingkasanJumlah.ForeColor = System.Drawing.Color.Black;
            this.lblRingkasanJumlah.Location = new System.Drawing.Point(30, 100);
            this.lblRingkasanJumlah.Name = "lblRingkasanJumlah";
            this.lblRingkasanJumlah.Size = new System.Drawing.Size(500, 25);
            this.lblRingkasanJumlah.TabIndex = 2;
            this.lblRingkasanJumlah.Text = "...";
            // 
            // lblRingkasanHargaSatuan
            // 
            this.lblRingkasanHargaSatuan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblRingkasanHargaSatuan.ForeColor = System.Drawing.Color.Black;
            this.lblRingkasanHargaSatuan.Location = new System.Drawing.Point(30, 130);
            this.lblRingkasanHargaSatuan.Name = "lblRingkasanHargaSatuan";
            this.lblRingkasanHargaSatuan.Size = new System.Drawing.Size(500, 25);
            this.lblRingkasanHargaSatuan.TabIndex = 3;
            this.lblRingkasanHargaSatuan.Text = "...";
            // 
            // lblRingkasanTotal
            // 
            this.lblRingkasanTotal.Font = new System.Drawing.Font("Segoe UI Black", 24F);
            this.lblRingkasanTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblRingkasanTotal.Location = new System.Drawing.Point(25, 170);
            this.lblRingkasanTotal.Name = "lblRingkasanTotal";
            this.lblRingkasanTotal.Size = new System.Drawing.Size(500, 50);
            this.lblRingkasanTotal.TabIndex = 4;
            this.lblRingkasanTotal.Text = "Rp ...";
            // 
            // lblStep2Rekening
            // 
            this.lblStep2Rekening.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStep2Rekening.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblStep2Rekening.Location = new System.Drawing.Point(550, 70);
            this.lblStep2Rekening.Name = "lblStep2Rekening";
            this.lblStep2Rekening.Size = new System.Drawing.Size(400, 80);
            this.lblStep2Rekening.TabIndex = 5;
            this.lblStep2Rekening.Text = "...";
            // 
            // btnUploadBukti
            // 
            this.btnUploadBukti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnUploadBukti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadBukti.FlatAppearance.BorderSize = 0;
            this.btnUploadBukti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadBukti.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUploadBukti.ForeColor = System.Drawing.Color.White;
            this.btnUploadBukti.Location = new System.Drawing.Point(550, 160);
            this.btnUploadBukti.Name = "btnUploadBukti";
            this.btnUploadBukti.Size = new System.Drawing.Size(300, 45);
            this.btnUploadBukti.TabIndex = 6;
            this.btnUploadBukti.Text = "📸 UPLOAD BUKTI TRANSFER";
            this.btnUploadBukti.UseVisualStyleBackColor = false;
            this.btnUploadBukti.Click += new System.EventHandler(this.btnUploadBukti_Click);
            // 
            // pictureBoxBukti
            // 
            this.pictureBoxBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBukti.Location = new System.Drawing.Point(550, 220);
            this.pictureBoxBukti.Name = "pictureBoxBukti";
            this.pictureBoxBukti.Size = new System.Drawing.Size(300, 160);
            this.pictureBoxBukti.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBukti.TabIndex = 7;
            this.pictureBoxBukti.TabStop = false;
            // 
            // lblStatusUpload
            // 
            this.lblStatusUpload.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusUpload.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusUpload.Location = new System.Drawing.Point(550, 390);
            this.lblStatusUpload.Name = "lblStatusUpload";
            this.lblStatusUpload.Size = new System.Drawing.Size(300, 25);
            this.lblStatusUpload.TabIndex = 8;
            this.lblStatusUpload.Text = "Belum ada file dipilih";
            // 
            // btnKonfirmasi
            // 
            this.btnKonfirmasi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKonfirmasi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.btnKonfirmasi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKonfirmasi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKonfirmasi.FlatAppearance.BorderSize = 2;
            this.btnKonfirmasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKonfirmasi.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.btnKonfirmasi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKonfirmasi.Location = new System.Drawing.Point(30, 430);
            this.btnKonfirmasi.Name = "btnKonfirmasi";
            this.btnKonfirmasi.Size = new System.Drawing.Size(940, 60);
            this.btnKonfirmasi.TabIndex = 9;
            this.btnKonfirmasi.Text = "KONFIRMASI PESANAN ✅";
            this.btnKonfirmasi.UseVisualStyleBackColor = false;
            this.btnKonfirmasi.Click += new System.EventHandler(this.btnKonfirmasi_Click);
            // 
            // btnKembaliStep1
            // 
            this.btnKembaliStep1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKembaliStep1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembaliStep1.FlatAppearance.BorderSize = 0;
            this.btnKembaliStep1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembaliStep1.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.btnKembaliStep1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnKembaliStep1.Location = new System.Drawing.Point(800, 20);
            this.btnKembaliStep1.Name = "btnKembaliStep1";
            this.btnKembaliStep1.Size = new System.Drawing.Size(170, 30);
            this.btnKembaliStep1.TabIndex = 10;
            this.btnKembaliStep1.Text = "⬅ Kembali Edit Penitip";
            this.btnKembaliStep1.Click += new System.EventHandler(this.btnKembaliStep1_Click);
            // 
            // CheckoutControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.lblNamaProduk);
            this.Controls.Add(this.lblHargaSatuan);
            this.Controls.Add(this.lblMinOrder);
            this.Controls.Add(this.lblInfoPO);
            this.Controls.Add(this.lblInfoRekening);
            this.Controls.Add(this.pnlStep1);
            this.Controls.Add(this.pnlStep2);
            this.Name = "CheckoutControl";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlStep1.ResumeLayout(false);
            this.pnlStep1.PerformLayout();
            this.pnlStep2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBukti)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblNamaProduk, lblHargaSatuan, lblMinOrder, lblInfoPO, lblInfoRekening;
        private System.Windows.Forms.Panel pnlStep1, pnlStep2;
        private System.Windows.Forms.TextBox txtNamaPenitip, txtJumlah, txtCatatan;
        private System.Windows.Forms.Button btnTambahPenitip, btnHapusPenitip, btnLanjutkan;
        private System.Windows.Forms.ListBox listBoxPenitip;
        private System.Windows.Forms.Label lblTotalPenitip;
        private System.Windows.Forms.Label lblRingkasanJudul, lblRingkasanProduk, lblRingkasanJumlah, lblRingkasanHargaSatuan, lblRingkasanTotal, lblStep2Rekening, lblStatusUpload;
        private System.Windows.Forms.Button btnUploadBukti, btnKonfirmasi, btnKembaliStep1;
        private System.Windows.Forms.PictureBox pictureBoxBukti;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label lblJml;
        private System.Windows.Forms.Label lblCat;
    }
}
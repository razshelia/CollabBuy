namespace CollabBuy.CollabBuyApp.View.Product
{
    partial class ManajemenProdukControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTambahProduk = new System.Windows.Forms.Button();
            this.dgvLapak = new System.Windows.Forms.DataGridView();
            this.txtCariProduk = new System.Windows.Forms.TextBox();
            this.pnlTambahProduk = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.txtNamaProduk = new System.Windows.Forms.TextBox();
            this.lblKategoriProduk = new System.Windows.Forms.Label();
            this.cbKategoriProduk = new System.Windows.Forms.ComboBox();
            this.lblHargaProduk = new System.Windows.Forms.Label();
            this.txtHargaProduk = new System.Windows.Forms.TextBox();
            this.lblMinOrder = new System.Windows.Forms.Label();
            this.lblSesiPO = new System.Windows.Forms.Label();
            this.cbSesiPO = new System.Windows.Forms.ComboBox();
            this.txtMinOrder = new System.Windows.Forms.TextBox();
            this.lblHargaDiskon = new System.Windows.Forms.Label();    // ← TAMBAH
            this.txtHargaDiskon = new System.Windows.Forms.TextBox();
            this.lblTargetKuota = new System.Windows.Forms.Label();   // ← TAMBAH
            this.txtTargetKuota = new System.Windows.Forms.TextBox();
            this.lblDeskripsiProduk = new System.Windows.Forms.Label();
            this.txtDeskripsiProduk = new System.Windows.Forms.TextBox();
            this.picFotoPreview = new System.Windows.Forms.PictureBox();
            this.btnPilihFoto = new System.Windows.Forms.Button();
            this.btnSimpanProduk = new System.Windows.Forms.Button();
            this.btnBatalTambah = new System.Windows.Forms.Button();

            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLapak)).BeginInit();
            this.pnlTambahProduk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoPreview)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Gudang Lapak Kamu 📦";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Pantau, edit, dan hapus barang yang kamu jual!";

            // pnlGrid
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrid.Controls.Add(this.txtCariProduk);
            this.pnlGrid.Controls.Add(this.btnTambahProduk);
            this.pnlGrid.Controls.Add(this.btnRefresh);
            this.pnlGrid.Controls.Add(this.dgvLapak);
            this.pnlGrid.Location = new System.Drawing.Point(36, 110);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(920, 500);
            this.pnlGrid.TabIndex = 2;

            // btnTambahProduk
            this.btnTambahProduk.BackColor = System.Drawing.Color.FromArgb(90, 24, 154);
            this.btnTambahProduk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambahProduk.FlatAppearance.BorderSize = 0;
            this.btnTambahProduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahProduk.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTambahProduk.ForeColor = System.Drawing.Color.White;
            this.btnTambahProduk.Location = new System.Drawing.Point(570, 448);
            this.btnTambahProduk.Name = "btnTambahProduk";
            this.btnTambahProduk.Size = new System.Drawing.Size(160, 38);
            this.btnTambahProduk.TabIndex = 4;
            this.btnTambahProduk.Text = "➕ Tambah Produk";
            this.btnTambahProduk.UseVisualStyleBackColor = false;
            this.btnTambahProduk.Click += new System.EventHandler(this.btnTambahProduk_Click);

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnRefresh.Location = new System.Drawing.Point(743, 448);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 38);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // txtCariProduk
            this.txtCariProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCariProduk.Location = new System.Drawing.Point(34, 8);
            this.txtCariProduk.Size = new System.Drawing.Size(300, 28);
            this.txtCariProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCariProduk.PlaceholderText = "🔍 Cari nama produk atau penitip...";
            this.txtCariProduk.Name = "txtCariProduk";
            this.txtCariProduk.TextChanged += new System.EventHandler(this.txtCariProduk_TextChanged);

            // dgvLapak
            this.dgvLapak.AllowUserToAddRows = false;
            this.dgvLapak.AllowUserToDeleteRows = false;
            this.dgvLapak.BackgroundColor = System.Drawing.Color.White;
            this.dgvLapak.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLapak.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLapak.ColumnHeadersHeight = 45;
            this.dgvLapak.EnableHeadersVisualStyles = false;
            this.dgvLapak.Location = new System.Drawing.Point(34, 44);
            this.dgvLapak.MultiSelect = false;
            this.dgvLapak.Name = "dgvLapak";
            this.dgvLapak.ReadOnly = false;
            this.dgvLapak.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(235, 230, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            this.dgvLapak.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLapak.RowTemplate.Height = 80;
            this.dgvLapak.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLapak.Size = new System.Drawing.Size(858, 400);
            this.dgvLapak.TabIndex = 0;

            // pnlTambahProduk
            this.pnlTambahProduk.BackColor = System.Drawing.Color.FromArgb(224, 170, 255);
            this.pnlTambahProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTambahProduk.Controls.Add(this.lblFormTitle);
            this.pnlTambahProduk.Controls.Add(this.lblNamaProduk);
            this.pnlTambahProduk.Controls.Add(this.txtNamaProduk);
            this.pnlTambahProduk.Controls.Add(this.lblKategoriProduk);
            this.pnlTambahProduk.Controls.Add(this.cbKategoriProduk);
            this.pnlTambahProduk.Controls.Add(this.lblHargaProduk);
            this.pnlTambahProduk.Controls.Add(this.txtHargaProduk);
            this.pnlTambahProduk.Controls.Add(this.lblMinOrder);
            this.pnlTambahProduk.Controls.Add(this.txtMinOrder);
            this.pnlTambahProduk.Controls.Add(this.lblHargaDiskon);   // ← TAMBAH
            this.pnlTambahProduk.Controls.Add(this.txtHargaDiskon);
            this.pnlTambahProduk.Controls.Add(this.lblTargetKuota); 
            this.pnlTambahProduk.Controls.Add(this.txtTargetKuota);
            this.pnlTambahProduk.Controls.Add(this.lblSesiPO);
            this.pnlTambahProduk.Controls.Add(this.cbSesiPO);
            this.pnlTambahProduk.Controls.Add(this.lblDeskripsiProduk);
            this.pnlTambahProduk.Controls.Add(this.txtDeskripsiProduk);
            this.pnlTambahProduk.Controls.Add(this.picFotoPreview);
            this.pnlTambahProduk.Controls.Add(this.btnPilihFoto);
            this.pnlTambahProduk.Controls.Add(this.btnSimpanProduk);
            this.pnlTambahProduk.Controls.Add(this.btnBatalTambah);
            this.pnlTambahProduk.Location = new System.Drawing.Point(36, 625);
            this.pnlTambahProduk.Name = "pnlTambahProduk";
            this.pnlTambahProduk.Size = new System.Drawing.Size(920, 365);
            this.pnlTambahProduk.TabIndex = 3;
            this.pnlTambahProduk.Visible = false;

            // lblFormTitle
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Black", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblFormTitle.Location = new System.Drawing.Point(20, 15);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Text = "➕ Input Produk Baru";

            // lblNamaProduk
            this.lblNamaProduk.AutoSize = true;
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblNamaProduk.Location = new System.Drawing.Point(20, 52);
            this.lblNamaProduk.Text = "Nama Produk *";

            // txtNamaProduk
            this.txtNamaProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNamaProduk.Location = new System.Drawing.Point(20, 70);
            this.txtNamaProduk.Name = "txtNamaProduk";
            this.txtNamaProduk.Size = new System.Drawing.Size(340, 27);

            // lblKategoriProduk
            this.lblKategoriProduk.AutoSize = true;
            this.lblKategoriProduk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKategoriProduk.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblKategoriProduk.Location = new System.Drawing.Point(375, 52);
            this.lblKategoriProduk.Text = "Kategori *";

            // cbKategoriProduk
            this.cbKategoriProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKategoriProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbKategoriProduk.Location = new System.Drawing.Point(375, 70);
            this.cbKategoriProduk.Name = "cbKategoriProduk";
            this.cbKategoriProduk.Size = new System.Drawing.Size(230, 27);

            // lblHargaProduk
            this.lblHargaProduk.AutoSize = true;
            this.lblHargaProduk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHargaProduk.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblHargaProduk.Location = new System.Drawing.Point(20, 112);
            this.lblHargaProduk.Text = "Harga Dasar (Rp) *";

            // txtHargaProduk
            this.txtHargaProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHargaProduk.Location = new System.Drawing.Point(20, 130);
            this.txtHargaProduk.Name = "txtHargaProduk";
            this.txtHargaProduk.Size = new System.Drawing.Size(180, 27);

            // lblMinOrder
            this.lblMinOrder.AutoSize = true;
            this.lblMinOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMinOrder.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblMinOrder.Location = new System.Drawing.Point(215, 112);
            this.lblMinOrder.Text = "Min. Order";

            // lblSesiPO
            this.lblSesiPO.AutoSize = true;
            this.lblSesiPO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSesiPO.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblSesiPO.Location = new System.Drawing.Point(330, 112);
            this.lblSesiPO.Text = "Sesi PO";

            // cbSesiPO
            this.cbSesiPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSesiPO.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbSesiPO.Location = new System.Drawing.Point(330, 130);
            this.cbSesiPO.Name = "cbSesiPO";
            this.cbSesiPO.Size = new System.Drawing.Size(270, 27);

            // txtMinOrder
            this.txtMinOrder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMinOrder.Location = new System.Drawing.Point(215, 130);
            this.txtMinOrder.Name = "txtMinOrder";
            this.txtMinOrder.Size = new System.Drawing.Size(100, 27);
            this.txtMinOrder.Text = "1";

            // lblHargaDiskon   ← TAMBAH BLOK INI
            this.lblHargaDiskon.AutoSize = true;
            this.lblHargaDiskon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHargaDiskon.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblHargaDiskon.Location = new System.Drawing.Point(20, 168);
            this.lblHargaDiskon.Name = "lblHargaDiskon";
            this.lblHargaDiskon.Text = "Potongan Diskon GR (Rp)";

            // txtHargaDiskon   ← TAMBAH BLOK INI
            this.txtHargaDiskon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHargaDiskon.Location = new System.Drawing.Point(20, 186);
            this.txtHargaDiskon.Name = "txtHargaDiskon";
            this.txtHargaDiskon.Size = new System.Drawing.Size(180, 27);
            this.txtHargaDiskon.PlaceholderText = "Kosongkan jika tidak ada";


            // lblTargetKuota                                                        // ← TAMBAH BLOK INI
            this.lblTargetKuota.AutoSize = true;
            this.lblTargetKuota.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTargetKuota.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblTargetKuota.Location = new System.Drawing.Point(625, 162);
            this.lblTargetKuota.Name = "lblTargetKuota";
            this.lblTargetKuota.Text = "Target Kuota (opsional)";

            // txtTargetKuota                                                        // ← TAMBAH BLOK INI
            this.txtTargetKuota.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTargetKuota.Location = new System.Drawing.Point(625, 180);
            this.txtTargetKuota.Name = "txtTargetKuota";
            this.txtTargetKuota.Size = new System.Drawing.Size(100, 27);
            this.txtTargetKuota.PlaceholderText = "cth: 50";

            // lblDeskripsiProduk
            this.lblDeskripsiProduk.AutoSize = true;
            this.lblDeskripsiProduk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDeskripsiProduk.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.lblDeskripsiProduk.Location = new System.Drawing.Point(20, 228);
            this.lblDeskripsiProduk.Text = "Deskripsi";

            // txtDeskripsiProduk
            this.txtDeskripsiProduk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDeskripsiProduk.Location = new System.Drawing.Point(20, 246);
            this.txtDeskripsiProduk.Multiline = true;
            this.txtDeskripsiProduk.Name = "txtDeskripsiProduk";
            this.txtDeskripsiProduk.Size = new System.Drawing.Size(585, 65);

            // picFotoPreview
            this.picFotoPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFotoPreview.Location = new System.Drawing.Point(625, 70);
            this.picFotoPreview.Name = "picFotoPreview";
            this.picFotoPreview.Size = new System.Drawing.Size(80, 80);
            this.picFotoPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFotoPreview.BackColor = System.Drawing.Color.White;

            // btnPilihFoto
            this.btnPilihFoto.BackColor = System.Drawing.Color.FromArgb(200, 182, 255);
            this.btnPilihFoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPilihFoto.FlatAppearance.BorderSize = 0;
            this.btnPilihFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPilihFoto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPilihFoto.ForeColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnPilihFoto.Location = new System.Drawing.Point(715, 90);
            this.btnPilihFoto.Name = "btnPilihFoto";
            this.btnPilihFoto.Size = new System.Drawing.Size(110, 36);
            this.btnPilihFoto.Text = "📷 Pilih Foto";
            this.btnPilihFoto.UseVisualStyleBackColor = false;
            this.btnPilihFoto.Click += new System.EventHandler(this.btnPilihFoto_Click);

            // btnSimpanProduk
            this.btnSimpanProduk.BackColor = System.Drawing.Color.FromArgb(36, 0, 70);
            this.btnSimpanProduk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpanProduk.FlatAppearance.BorderSize = 0;
            this.btnSimpanProduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanProduk.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.btnSimpanProduk.ForeColor = System.Drawing.Color.FromArgb(253, 255, 182);
            this.btnSimpanProduk.Location = new System.Drawing.Point(620, 265);
            this.btnSimpanProduk.Name = "btnSimpanProduk";
            this.btnSimpanProduk.Size = new System.Drawing.Size(185, 40);
            this.btnSimpanProduk.Text = "✅ Simpan Produk";
            this.btnSimpanProduk.UseVisualStyleBackColor = false;
            this.btnSimpanProduk.Click += new System.EventHandler(this.btnSimpanProduk_Click);

            // btnBatalTambah
            this.btnBatalTambah.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnBatalTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatalTambah.FlatAppearance.BorderSize = 0;
            this.btnBatalTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatalTambah.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBatalTambah.ForeColor = System.Drawing.Color.White;
            this.btnBatalTambah.Location = new System.Drawing.Point(620, 312);
            this.btnBatalTambah.Name = "btnBatalTambah";
            this.btnBatalTambah.Size = new System.Drawing.Size(185, 36);
            this.btnBatalTambah.Text = "✖ Batal";
            this.btnBatalTambah.UseVisualStyleBackColor = false;
            this.btnBatalTambah.Click += new System.EventHandler(this.btnBatalTambah_Click);

            // ManajemenProdukControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.pnlTambahProduk);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "ManajemenProdukControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.ManajemenProdukControl_Load);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLapak)).EndInit();
            this.pnlTambahProduk.ResumeLayout(false);
            this.pnlTambahProduk.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvLapak;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTambahProduk;
        private System.Windows.Forms.Panel pnlTambahProduk;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblNamaProduk;
        private System.Windows.Forms.TextBox txtNamaProduk;
        private System.Windows.Forms.Label lblKategoriProduk;
        private System.Windows.Forms.ComboBox cbKategoriProduk;
        private System.Windows.Forms.Label lblHargaProduk;
        private System.Windows.Forms.TextBox txtHargaProduk;
        private System.Windows.Forms.Label lblMinOrder;
        private System.Windows.Forms.TextBox txtMinOrder;
        private System.Windows.Forms.Label lblDeskripsiProduk;
        private System.Windows.Forms.TextBox txtDeskripsiProduk;
        private System.Windows.Forms.PictureBox picFotoPreview;
        private System.Windows.Forms.Button btnPilihFoto;
        private System.Windows.Forms.Button btnSimpanProduk;
        private System.Windows.Forms.Button btnBatalTambah;
        private System.Windows.Forms.Label lblSesiPO;
        private System.Windows.Forms.ComboBox cbSesiPO;
        private System.Windows.Forms.TextBox txtCariProduk;
        private System.Windows.Forms.Label lblHargaDiskon;    // ← TAMBAH
        private System.Windows.Forms.TextBox txtHargaDiskon;
        private System.Windows.Forms.Label lblTargetKuota;    // ← TAMBAH
        private System.Windows.Forms.TextBox txtTargetKuota;
    }
}
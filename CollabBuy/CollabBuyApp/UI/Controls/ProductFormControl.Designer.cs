namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ProductFormControl
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
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnPilihFoto = new System.Windows.Forms.Button();
            this.pbFotoProduk = new System.Windows.Forms.PictureBox();
            this.numMinOrder = new System.Windows.Forms.NumericUpDown();
            this.lblMinOrder = new System.Windows.Forms.Label();
            this.numTargetKuota = new System.Windows.Forms.NumericUpDown();
            this.lblTargetKuota = new System.Windows.Forms.Label();
            this.numHargaDiskon = new System.Windows.Forms.NumericUpDown();
            this.lblHargaDiskon = new System.Windows.Forms.Label();
            this.numHargaDasar = new System.Windows.Forms.NumericUpDown();
            this.lblHargaDasar = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.lblKategori = new System.Windows.Forms.Label();
            this.txtNamaProduk = new System.Windows.Forms.TextBox();
            this.lblNamaProduk = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoProduk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetKuota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHargaDiskon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHargaDasar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.btnBatal);
            this.pnlCard.Controls.Add(this.btnSimpan);
            this.pnlCard.Controls.Add(this.btnPilihFoto);
            this.pnlCard.Controls.Add(this.pbFotoProduk);
            this.pnlCard.Controls.Add(this.numMinOrder);
            this.pnlCard.Controls.Add(this.lblMinOrder);
            this.pnlCard.Controls.Add(this.numTargetKuota);
            this.pnlCard.Controls.Add(this.lblTargetKuota);
            this.pnlCard.Controls.Add(this.numHargaDiskon);
            this.pnlCard.Controls.Add(this.lblHargaDiskon);
            this.pnlCard.Controls.Add(this.numHargaDasar);
            this.pnlCard.Controls.Add(this.lblHargaDasar);
            this.pnlCard.Controls.Add(this.txtDeskripsi);
            this.pnlCard.Controls.Add(this.lblDeskripsi);
            this.pnlCard.Controls.Add(this.cmbKategori);
            this.pnlCard.Controls.Add(this.lblKategori);
            this.pnlCard.Controls.Add(this.txtNamaProduk);
            this.pnlCard.Controls.Add(this.lblNamaProduk);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Location = new System.Drawing.Point(50, 40);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(840, 520);
            this.pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(840, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TAMBAH PRODUK BARU 📦";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNamaProduk
            // 
            this.lblNamaProduk.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblNamaProduk.Location = new System.Drawing.Point(40, 90);
            this.lblNamaProduk.Name = "lblNamaProduk";
            this.lblNamaProduk.Size = new System.Drawing.Size(340, 20);
            this.lblNamaProduk.TabIndex = 1;
            this.lblNamaProduk.Text = "Nama Produk:";
            // 
            // txtNamaProduk
            // 
            this.txtNamaProduk.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNamaProduk.Location = new System.Drawing.Point(40, 115);
            this.txtNamaProduk.Name = "txtNamaProduk";
            this.txtNamaProduk.Size = new System.Drawing.Size(340, 27);
            this.txtNamaProduk.TabIndex = 2;
            // 
            // lblKategori
            // 
            this.lblKategori.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKategori.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblKategori.Location = new System.Drawing.Point(40, 160);
            this.lblKategori.Name = "lblKategori";
            this.lblKategori.Size = new System.Drawing.Size(340, 20);
            this.lblKategori.TabIndex = 3;
            this.lblKategori.Text = "Kategori:";
            // 
            // cmbKategori
            // 
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(40, 185);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(340, 28);
            this.cmbKategori.TabIndex = 4;
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(40, 230);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(340, 20);
            this.lblDeskripsi.TabIndex = 5;
            this.lblDeskripsi.Text = "Deskripsi / Spek:";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDeskripsi.Location = new System.Drawing.Point(40, 255);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDeskripsi.Size = new System.Drawing.Size(340, 125);
            this.txtDeskripsi.TabIndex = 6;
            // 
            // lblHargaDasar
            // 
            this.lblHargaDasar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHargaDasar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblHargaDasar.Location = new System.Drawing.Point(430, 90);
            this.lblHargaDasar.Name = "lblHargaDasar";
            this.lblHargaDasar.Size = new System.Drawing.Size(160, 20);
            this.lblHargaDasar.TabIndex = 7;
            this.lblHargaDasar.Text = "Harga Dasar:";
            // 
            // numHargaDasar
            // 
            this.numHargaDasar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numHargaDasar.Location = new System.Drawing.Point(430, 115);
            this.numHargaDasar.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numHargaDasar.Name = "numHargaDasar";
            this.numHargaDasar.Size = new System.Drawing.Size(160, 27);
            this.numHargaDasar.TabIndex = 8;
            // 
            // lblHargaDiskon
            // 
            this.lblHargaDiskon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHargaDiskon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblHargaDiskon.Location = new System.Drawing.Point(620, 90);
            this.lblHargaDiskon.Name = "lblHargaDiskon";
            this.lblHargaDiskon.Size = new System.Drawing.Size(180, 20);
            this.lblHargaDiskon.TabIndex = 9;
            this.lblHargaDiskon.Text = "Harga Diskon (Opsional):";
            // 
            // numHargaDiskon
            // 
            this.numHargaDiskon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numHargaDiskon.Location = new System.Drawing.Point(620, 115);
            this.numHargaDiskon.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numHargaDiskon.Name = "numHargaDiskon";
            this.numHargaDiskon.Size = new System.Drawing.Size(180, 27);
            this.numHargaDiskon.TabIndex = 10;
            // 
            // lblTargetKuota
            // 
            this.lblTargetKuota.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTargetKuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblTargetKuota.Location = new System.Drawing.Point(430, 160);
            this.lblTargetKuota.Name = "lblTargetKuota";
            this.lblTargetKuota.Size = new System.Drawing.Size(160, 20);
            this.lblTargetKuota.TabIndex = 11;
            this.lblTargetKuota.Text = "Target Kuota (Ops):";
            // 
            // numTargetKuota
            // 
            this.numTargetKuota.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numTargetKuota.Location = new System.Drawing.Point(430, 185);
            this.numTargetKuota.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numTargetKuota.Name = "numTargetKuota";
            this.numTargetKuota.Size = new System.Drawing.Size(160, 27);
            this.numTargetKuota.TabIndex = 12;
            // 
            // lblMinOrder
            // 
            this.lblMinOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMinOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lblMinOrder.Location = new System.Drawing.Point(620, 160);
            this.lblMinOrder.Name = "lblMinOrder";
            this.lblMinOrder.Size = new System.Drawing.Size(180, 20);
            this.lblMinOrder.TabIndex = 13;
            this.lblMinOrder.Text = "Min. Order:";
            // 
            // numMinOrder
            // 
            this.numMinOrder.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numMinOrder.Location = new System.Drawing.Point(620, 185);
            this.numMinOrder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMinOrder.Name = "numMinOrder";
            this.numMinOrder.Size = new System.Drawing.Size(180, 27);
            this.numMinOrder.TabIndex = 14;
            this.numMinOrder.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pbFotoProduk
            // 
            this.pbFotoProduk.BackColor = System.Drawing.Color.White;
            this.pbFotoProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFotoProduk.Location = new System.Drawing.Point(430, 230);
            this.pbFotoProduk.Name = "pbFotoProduk";
            this.pbFotoProduk.Size = new System.Drawing.Size(160, 150);
            this.pbFotoProduk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFotoProduk.TabIndex = 15;
            this.pbFotoProduk.TabStop = false;
            // 
            // btnPilihFoto
            // 
            this.btnPilihFoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnPilihFoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPilihFoto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnPilihFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPilihFoto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPilihFoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnPilihFoto.Location = new System.Drawing.Point(620, 230);
            this.btnPilihFoto.Name = "btnPilihFoto";
            this.btnPilihFoto.Size = new System.Drawing.Size(180, 40);
            this.btnPilihFoto.TabIndex = 16;
            this.btnPilihFoto.Text = "📸 Pilih Foto...";
            this.btnPilihFoto.UseVisualStyleBackColor = false;
            this.btnPilihFoto.Click += new System.EventHandler(this.btnPilihFoto_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.btnBatal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnBatal.FlatAppearance.BorderSize = 2;
            this.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBatal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnBatal.Location = new System.Drawing.Point(40, 430);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(160, 50);
            this.btnBatal.TabIndex = 18;
            this.btnBatal.Text = "BATAL ❌";
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnFile = new System.Windows.Forms.Button();
            this.btnSimpan.FlatAppearance.BorderSize = 2;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnSimpan.Location = new System.Drawing.Point(220, 430);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(580, 50);
            this.btnSimpan.TabIndex = 17;
            this.btnSimpan.Text = "BUAT PRODUK BARU 💾";
            this.btnSimpan.UseVisualStyleBackColor = false;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // ProductFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "ProductFormControl";
            this.Size = new System.Drawing.Size(1046, 730);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoProduk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetKuota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHargaDiskon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHargaDasar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNamaProduk;
        private System.Windows.Forms.TextBox txtNamaProduk;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Label lblKategori;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.NumericUpDown numHargaDasar;
        private System.Windows.Forms.Label lblHargaDasar;
        private System.Windows.Forms.NumericUpDown numHargaDiskon;
        private System.Windows.Forms.Label lblHargaDiskon;
        private System.Windows.Forms.NumericUpDown numTargetKuota;
        private System.Windows.Forms.Label lblTargetKuota;
        private System.Windows.Forms.NumericUpDown numMinOrder;
        private System.Windows.Forms.Label lblMinOrder;
        private System.Windows.Forms.PictureBox pbFotoProduk;
        private System.Windows.Forms.Button btnPilihFoto;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnFile; // Helper
    }
}
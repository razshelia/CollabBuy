namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class ProductFormControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.pnlCard.BackColor = System.Drawing.Color.White;
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
            this.pnlCard.Size = new System.Drawing.Size(800, 450);
            this.pnlCard.TabIndex = 0;
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.LightGray;
            this.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBatal.Location = new System.Drawing.Point(520, 380);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(120, 40);
            this.btnBatal.TabIndex = 18;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.ForeColor = System.Drawing.Color.White;
            this.btnSimpan.Location = new System.Drawing.Point(650, 380);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(120, 40);
            this.btnSimpan.TabIndex = 17;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = false;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnPilihFoto
            // 
            this.btnPilihFoto.Location = new System.Drawing.Point(580, 210);
            this.btnPilihFoto.Name = "btnPilihFoto";
            this.btnPilihFoto.Size = new System.Drawing.Size(190, 30);
            this.btnPilihFoto.TabIndex = 16;
            this.btnPilihFoto.Text = "Pilih Foto...";
            this.btnPilihFoto.UseVisualStyleBackColor = true;
            this.btnPilihFoto.Click += new System.EventHandler(this.btnPilihFoto_Click);
            // 
            // pbFotoProduk
            // 
            this.pbFotoProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFotoProduk.Location = new System.Drawing.Point(410, 210);
            this.pbFotoProduk.Name = "pbFotoProduk";
            this.pbFotoProduk.Size = new System.Drawing.Size(150, 150);
            this.pbFotoProduk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFotoProduk.TabIndex = 15;
            this.pbFotoProduk.TabStop = false;
            // 
            // numMinOrder
            // 
            this.numMinOrder.Location = new System.Drawing.Point(580, 160);
            this.numMinOrder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMinOrder.Name = "numMinOrder";
            this.numMinOrder.Size = new System.Drawing.Size(190, 25);
            this.numMinOrder.TabIndex = 14;
            this.numMinOrder.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblMinOrder
            // 
            this.lblMinOrder.AutoSize = true;
            this.lblMinOrder.Location = new System.Drawing.Point(580, 140);
            this.lblMinOrder.Name = "lblMinOrder";
            this.lblMinOrder.Size = new System.Drawing.Size(73, 17);
            this.lblMinOrder.TabIndex = 13;
            this.lblMinOrder.Text = "Min. Order";
            // 
            // numTargetKuota
            // 
            this.numTargetKuota.Location = new System.Drawing.Point(410, 160);
            this.numTargetKuota.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numTargetKuota.Name = "numTargetKuota";
            this.numTargetKuota.Size = new System.Drawing.Size(150, 25);
            this.numTargetKuota.TabIndex = 12;
            // 
            // lblTargetKuota
            // 
            this.lblTargetKuota.AutoSize = true;
            this.lblTargetKuota.Location = new System.Drawing.Point(410, 140);
            this.lblTargetKuota.Name = "lblTargetKuota";
            this.lblTargetKuota.Size = new System.Drawing.Size(86, 17);
            this.lblTargetKuota.TabIndex = 11;
            this.lblTargetKuota.Text = "Target Kuota";
            // 
            // numHargaDiskon
            // 
            this.numHargaDiskon.Location = new System.Drawing.Point(580, 100);
            this.numHargaDiskon.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numHargaDiskon.Name = "numHargaDiskon";
            this.numHargaDiskon.Size = new System.Drawing.Size(190, 25);
            this.numHargaDiskon.TabIndex = 10;
            // 
            // lblHargaDiskon
            // 
            this.lblHargaDiskon.AutoSize = true;
            this.lblHargaDiskon.Location = new System.Drawing.Point(580, 80);
            this.lblHargaDiskon.Name = "lblHargaDiskon";
            this.lblHargaDiskon.Size = new System.Drawing.Size(123, 17);
            this.lblHargaDiskon.TabIndex = 9;
            this.lblHargaDiskon.Text = "Harga Diskon (Ops)";
            // 
            // numHargaDasar
            // 
            this.numHargaDasar.Location = new System.Drawing.Point(410, 100);
            this.numHargaDasar.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numHargaDasar.Name = "numHargaDasar";
            this.numHargaDasar.Size = new System.Drawing.Size(150, 25);
            this.numHargaDasar.TabIndex = 8;
            // 
            // lblHargaDasar
            // 
            this.lblHargaDasar.AutoSize = true;
            this.lblHargaDasar.Location = new System.Drawing.Point(410, 80);
            this.lblHargaDasar.Name = "lblHargaDasar";
            this.lblHargaDasar.Size = new System.Drawing.Size(84, 17);
            this.lblHargaDasar.TabIndex = 7;
            this.lblHargaDasar.Text = "Harga Dasar";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Location = new System.Drawing.Point(30, 220);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDeskripsi.Size = new System.Drawing.Size(340, 140);
            this.txtDeskripsi.TabIndex = 6;
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoSize = true;
            this.lblDeskripsi.Location = new System.Drawing.Point(30, 200);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(61, 17);
            this.lblDeskripsi.TabIndex = 5;
            this.lblDeskripsi.Text = "Deskripsi";
            // 
            // cmbKategori
            // 
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(30, 160);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(340, 25);
            this.cmbKategori.TabIndex = 4;
            // 
            // lblKategori
            // 
            this.lblKategori.AutoSize = true;
            this.lblKategori.Location = new System.Drawing.Point(30, 140);
            this.lblKategori.Name = "lblKategori";
            this.lblKategori.Size = new System.Drawing.Size(58, 17);
            this.lblKategori.TabIndex = 3;
            this.lblKategori.Text = "Kategori";
            // 
            // txtNamaProduk
            // 
            this.txtNamaProduk.Location = new System.Drawing.Point(30, 100);
            this.txtNamaProduk.Name = "txtNamaProduk";
            this.txtNamaProduk.Size = new System.Drawing.Size(340, 25);
            this.txtNamaProduk.TabIndex = 2;
            // 
            // lblNamaProduk
            // 
            this.lblNamaProduk.AutoSize = true;
            this.lblNamaProduk.Location = new System.Drawing.Point(30, 80);
            this.lblNamaProduk.Name = "lblNamaProduk";
            this.lblNamaProduk.Size = new System.Drawing.Size(91, 17);
            this.lblNamaProduk.TabIndex = 1;
            this.lblNamaProduk.Text = "Nama Produk";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(25, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(225, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tambah Produk Baru";
            // 
            // ProductFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.pnlCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "ProductFormControl";
            this.Size = new System.Drawing.Size(900, 530);
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
    }
}
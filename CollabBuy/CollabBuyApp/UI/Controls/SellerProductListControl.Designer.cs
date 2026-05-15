namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerProductListControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlForm = new Panel();
            lblFormTitle = new Label();
            lblNama = new Label();
            txtNama = new TextBox();
            lblHarga = new Label();
            nudHarga = new NumericUpDown();
            lblDiskon = new Label();
            nudDiskon = new NumericUpDown();
            lblTarget = new Label();
            nudTarget = new NumericUpDown();
            lblMinOrder = new Label();
            nudMinOrder = new NumericUpDown();
            lblKategori = new Label();
            cmbKategori = new ComboBox();
            btnUploadFoto = new Button();
            lblStatusFoto = new Label();
            btnSimpan = new Button();
            btnBatal = new Button();
            flowPanelProduk = new FlowLayoutPanel();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHarga).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiskon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTarget).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinOrder).BeginInit();
            SuspendLayout();
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.FromArgb(45, 27, 79);
            pnlForm.Controls.Add(lblFormTitle);
            pnlForm.Controls.Add(lblNama);
            pnlForm.Controls.Add(txtNama);
            pnlForm.Controls.Add(lblHarga);
            pnlForm.Controls.Add(nudHarga);
            pnlForm.Controls.Add(lblDiskon);
            pnlForm.Controls.Add(nudDiskon);
            pnlForm.Controls.Add(lblTarget);
            pnlForm.Controls.Add(nudTarget);
            pnlForm.Controls.Add(lblMinOrder);
            pnlForm.Controls.Add(nudMinOrder);
            pnlForm.Controls.Add(lblKategori);
            pnlForm.Controls.Add(cmbKategori);
            pnlForm.Controls.Add(btnUploadFoto);
            pnlForm.Controls.Add(lblStatusFoto);
            pnlForm.Controls.Add(btnSimpan);
            pnlForm.Controls.Add(btnBatal);
            pnlForm.Location = new Point(15, 15);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(300, 650);
            pnlForm.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            lblFormTitle.Font = new Font("Segoe UI Black", 11F);
            lblFormTitle.ForeColor = Color.FromArgb(253, 224, 71);
            lblFormTitle.Location = new Point(15, 15);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(260, 25);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Tambah/Edit Produk";
            // 
            // lblNama
            // 
            lblNama.ForeColor = Color.White;
            lblNama.Location = new Point(15, 50);
            lblNama.Name = "lblNama";
            lblNama.Size = new Size(100, 23);
            lblNama.TabIndex = 1;
            lblNama.Text = "Nama Produk:";
            // 
            // txtNama
            // 
            txtNama.Location = new Point(15, 70);
            txtNama.Name = "txtNama";
            txtNama.Size = new Size(260, 23);
            txtNama.TabIndex = 2;
            // 
            // lblHarga
            // 
            lblHarga.ForeColor = Color.White;
            lblHarga.Location = new Point(15, 100);
            lblHarga.Name = "lblHarga";
            lblHarga.Size = new Size(100, 23);
            lblHarga.TabIndex = 3;
            lblHarga.Text = "Harga Dasar:";
            // 
            // nudHarga
            // 
            nudHarga.Location = new Point(15, 120);
            nudHarga.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudHarga.Name = "nudHarga";
            nudHarga.Size = new Size(260, 23);
            nudHarga.TabIndex = 4;
            // 
            // lblDiskon
            // 
            lblDiskon.ForeColor = Color.White;
            lblDiskon.Location = new Point(15, 150);
            lblDiskon.Name = "lblDiskon";
            lblDiskon.Size = new Size(100, 23);
            lblDiskon.TabIndex = 5;
            lblDiskon.Text = "Harga Diskon (opsional):";
            // 
            // nudDiskon
            // 
            nudDiskon.Location = new Point(15, 170);
            nudDiskon.Name = "nudDiskon";
            nudDiskon.Size = new Size(260, 23);
            nudDiskon.TabIndex = 6;
            // 
            // lblTarget
            // 
            lblTarget.ForeColor = Color.White;
            lblTarget.Location = new Point(15, 200);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(100, 23);
            lblTarget.TabIndex = 7;
            lblTarget.Text = "Target Kuota (opsional):";
            // 
            // nudTarget
            // 
            nudTarget.Location = new Point(15, 220);
            nudTarget.Name = "nudTarget";
            nudTarget.Size = new Size(260, 23);
            nudTarget.TabIndex = 8;
            // 
            // lblMinOrder
            // 
            lblMinOrder.ForeColor = Color.White;
            lblMinOrder.Location = new Point(15, 250);
            lblMinOrder.Name = "lblMinOrder";
            lblMinOrder.Size = new Size(100, 23);
            lblMinOrder.TabIndex = 9;
            lblMinOrder.Text = "Minimal Order:";
            // 
            // nudMinOrder
            // 
            nudMinOrder.Location = new Point(15, 270);
            nudMinOrder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMinOrder.Name = "nudMinOrder";
            nudMinOrder.Size = new Size(260, 23);
            nudMinOrder.TabIndex = 10;
            nudMinOrder.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblKategori
            // 
            lblKategori.ForeColor = Color.White;
            lblKategori.Location = new Point(15, 300);
            lblKategori.Name = "lblKategori";
            lblKategori.Size = new Size(100, 23);
            lblKategori.TabIndex = 11;
            lblKategori.Text = "Kategori:";
            // 
            // cmbKategori
            // 
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategori.Location = new Point(15, 320);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(260, 23);
            cmbKategori.TabIndex = 12;
            // 
            // btnUploadFoto
            // 
            btnUploadFoto.BackColor = Color.FromArgb(167, 139, 250);
            btnUploadFoto.FlatStyle = FlatStyle.Flat;
            btnUploadFoto.ForeColor = Color.White;
            btnUploadFoto.Location = new Point(15, 360);
            btnUploadFoto.Name = "btnUploadFoto";
            btnUploadFoto.Size = new Size(260, 30);
            btnUploadFoto.TabIndex = 13;
            btnUploadFoto.Text = "📸 Upload Foto Produk";
            btnUploadFoto.UseVisualStyleBackColor = false;
            btnUploadFoto.Click += btnUploadFoto_Click;
            // 
            // lblStatusFoto
            // 
            lblStatusFoto.ForeColor = Color.White;
            lblStatusFoto.Location = new Point(15, 395);
            lblStatusFoto.Name = "lblStatusFoto";
            lblStatusFoto.Size = new Size(260, 20);
            lblStatusFoto.TabIndex = 14;
            // 
            // btnSimpan
            // 
            btnSimpan.BackColor = Color.FromArgb(167, 139, 250);
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.ForeColor = Color.White;
            btnSimpan.Location = new Point(15, 430);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(120, 35);
            btnSimpan.TabIndex = 15;
            btnSimpan.Text = "➕ Tambah Produk";
            btnSimpan.UseVisualStyleBackColor = false;
            btnSimpan.Click += btnSimpan_Click;
            // 
            // btnBatal
            // 
            btnBatal.BackColor = Color.Gray;
            btnBatal.FlatStyle = FlatStyle.Flat;
            btnBatal.ForeColor = Color.White;
            btnBatal.Location = new Point(145, 430);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(120, 35);
            btnBatal.TabIndex = 16;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = false;
            btnBatal.Click += btnBatal_Click;
            // 
            // flowPanelProduk
            // 
            flowPanelProduk.AutoScroll = true;
            flowPanelProduk.BackColor = Color.FromArgb(255, 249, 230);
            flowPanelProduk.Location = new Point(330, 15);
            flowPanelProduk.Name = "flowPanelProduk";
            flowPanelProduk.Size = new Size(750, 650);
            flowPanelProduk.TabIndex = 1;
            // 
            // SellerProductListControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(pnlForm);
            Controls.Add(flowPanelProduk);
            Name = "SellerProductListControl";
            Size = new Size(1046, 333);
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHarga).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiskon).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTarget).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinOrder).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblFormTitle, lblNama, lblHarga, lblDiskon, lblTarget, lblMinOrder, lblKategori, lblStatusFoto;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.NumericUpDown nudHarga, nudDiskon, nudTarget, nudMinOrder;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Button btnUploadFoto, btnSimpan, btnBatal;
        private System.Windows.Forms.FlowLayoutPanel flowPanelProduk;
    }
}
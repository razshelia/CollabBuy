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
            pnlForm = new Panel();
            lblJudul = new Label();
            txtNama = new TextBox();
            txtHarga = new TextBox();
            txtDiskon = new TextBox();
            txtTarget = new TextBox();
            nudMinOrder = new NumericUpDown();
            cmbKategori = new ComboBox();
            btnUploadFoto = new Button();
            pictureBoxPreview = new PictureBox();
            lblStatusFoto = new Label();
            btnSimpan = new Button();
            btnBatal = new Button();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            SuspendLayout();
            // 
            // pnlForm
            // 
            pnlForm.Anchor = AnchorStyles.Top;
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblJudul);
            pnlForm.Controls.Add(txtNama);
            pnlForm.Controls.Add(txtHarga);
            pnlForm.Controls.Add(txtDiskon);
            pnlForm.Controls.Add(txtTarget);
            pnlForm.Controls.Add(nudMinOrder);
            pnlForm.Controls.Add(cmbKategori);
            pnlForm.Controls.Add(btnUploadFoto);
            pnlForm.Controls.Add(pictureBoxPreview);
            pnlForm.Controls.Add(lblStatusFoto);
            pnlForm.Controls.Add(btnSimpan);
            pnlForm.Controls.Add(btnBatal);
            pnlForm.Location = new Point(673, 30);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(500, 550);
            pnlForm.TabIndex = 0;
            // 
            // lblJudul
            // 
            lblJudul.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(45, 27, 79);
            lblJudul.Location = new Point(50, 20);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(400, 35);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "TAMBAH PRODUK BARU ✨";
            // 
            // txtNama
            // 
            txtNama.Location = new Point(50, 80);
            txtNama.Name = "txtNama";
            txtNama.Size = new Size(400, 23);
            txtNama.TabIndex = 1;
            // 
            // txtHarga
            // 
            txtHarga.Location = new Point(50, 130);
            txtHarga.Name = "txtHarga";
            txtHarga.Size = new Size(100, 23);
            txtHarga.TabIndex = 2;
            // 
            // txtDiskon
            // 
            txtDiskon.Location = new Point(50, 180);
            txtDiskon.Name = "txtDiskon";
            txtDiskon.Size = new Size(100, 23);
            txtDiskon.TabIndex = 3;
            // 
            // txtTarget
            // 
            txtTarget.Location = new Point(50, 230);
            txtTarget.Name = "txtTarget";
            txtTarget.Size = new Size(100, 23);
            txtTarget.TabIndex = 4;
            // 
            // nudMinOrder
            // 
            nudMinOrder.Location = new Point(50, 280);
            nudMinOrder.Name = "nudMinOrder";
            nudMinOrder.Size = new Size(120, 23);
            nudMinOrder.TabIndex = 5;
            // 
            // cmbKategori
            // 
            cmbKategori.Location = new Point(50, 330);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(121, 23);
            cmbKategori.TabIndex = 6;
            // 
            // btnUploadFoto
            // 
            btnUploadFoto.Location = new Point(50, 370);
            btnUploadFoto.Name = "btnUploadFoto";
            btnUploadFoto.Size = new Size(75, 23);
            btnUploadFoto.TabIndex = 7;
            btnUploadFoto.Text = "📸 Upload Foto";
            btnUploadFoto.Click += btnUploadFoto_Click;
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.Location = new Point(200, 370);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(100, 80);
            pictureBoxPreview.TabIndex = 8;
            pictureBoxPreview.TabStop = false;
            // 
            // lblStatusFoto
            // 
            lblStatusFoto.Location = new Point(50, 455);
            lblStatusFoto.Name = "lblStatusFoto";
            lblStatusFoto.Size = new Size(100, 23);
            lblStatusFoto.TabIndex = 9;
            lblStatusFoto.Text = "Belum ada foto";
            // 
            // btnSimpan
            // 
            btnSimpan.BackColor = Color.FromArgb(167, 139, 250);
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.ForeColor = Color.White;
            btnSimpan.Location = new Point(50, 490);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(180, 40);
            btnSimpan.TabIndex = 10;
            btnSimpan.Text = "💾 SIMPAN";
            btnSimpan.UseVisualStyleBackColor = false;
            btnSimpan.Click += btnSimpan_Click;
            // 
            // btnBatal
            // 
            btnBatal.Location = new Point(250, 490);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(180, 40);
            btnBatal.TabIndex = 11;
            btnBatal.Text = "Batal";
            btnBatal.Click += btnBatal_Click;
            // 
            // ProductFormControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(pnlForm);
            Name = "ProductFormControl";
            Size = new Size(1046, 333);
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblJudul, lblStatusFoto;
        private System.Windows.Forms.TextBox txtNama, txtHarga, txtDiskon, txtTarget;
        private System.Windows.Forms.NumericUpDown nudMinOrder;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Button btnUploadFoto, btnSimpan, btnBatal;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
    }
}
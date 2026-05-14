namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerPreOrderControl
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

        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblProduk = new System.Windows.Forms.Label();
            this.cmbProduk = new System.Windows.Forms.ComboBox();
            this.lblJenisPo = new System.Windows.Forms.Label();
            this.cmbJenisPo = new System.Windows.Forms.ComboBox();
            this.lblHargaDiskon = new System.Windows.Forms.Label();
            this.numHargaDiskon = new System.Windows.Forms.NumericUpDown();
            this.lblTarget = new System.Windows.Forms.Label();
            this.numTarget = new System.Windows.Forms.NumericUpDown();
            this.lblBatasWaktu = new System.Windows.Forms.Label();
            this.dtpBatasWaktu = new System.Windows.Forms.DateTimePicker();
            this.btnBukaPo = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHargaDiskon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarget)).BeginInit();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1000, 700);

            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Location = new System.Drawing.Point(250, 50);
            this.pnlCard.Size = new System.Drawing.Size(500, 580);

            this.lblJudul.Text = "BUKA PRE-ORDER 🛒";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 16F);
            this.lblJudul.Location = new System.Drawing.Point(30, 20);
            this.lblJudul.AutoSize = true;

            this.lblProduk.Text = "Pilih Produk dari Katalogmu:";
            this.lblProduk.Location = new System.Drawing.Point(35, 80);
            this.cmbProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduk.Location = new System.Drawing.Point(40, 105);
            this.cmbProduk.Size = new System.Drawing.Size(400, 28);

            this.lblJenisPo.Text = "Jenis PO:";
            this.lblJenisPo.Location = new System.Drawing.Point(35, 150);
            this.cmbJenisPo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisPo.Items.AddRange(new object[] { "Biasa", "GotongRoyong" });
            this.cmbJenisPo.SelectedIndex = 1;
            this.cmbJenisPo.Location = new System.Drawing.Point(40, 175);
            this.cmbJenisPo.Size = new System.Drawing.Size(400, 28);

            this.lblHargaDiskon.Text = "Harga Diskon (Kalau Target Capai):";
            this.lblHargaDiskon.Location = new System.Drawing.Point(35, 220);
            this.numHargaDiskon.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numHargaDiskon.Location = new System.Drawing.Point(40, 245);
            this.numHargaDiskon.Size = new System.Drawing.Size(400, 27);

            this.lblTarget.Text = "Target Kuota (Orang):";
            this.lblTarget.Location = new System.Drawing.Point(35, 290);
            this.numTarget.Location = new System.Drawing.Point(40, 315);
            this.numTarget.Size = new System.Drawing.Size(120, 27);

            this.lblBatasWaktu.Text = "Batas Waktu Buka PO:";
            this.lblBatasWaktu.Location = new System.Drawing.Point(35, 360);
            this.dtpBatasWaktu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBatasWaktu.Location = new System.Drawing.Point(40, 385);
            this.dtpBatasWaktu.Size = new System.Drawing.Size(400, 27);

            this.btnBukaPo.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning Logo
            this.btnBukaPo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBukaPo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBukaPo.Text = "LAUNCHING PO SEKARANG! 🚀";
            this.btnBukaPo.Location = new System.Drawing.Point(40, 460);
            this.btnBukaPo.Size = new System.Drawing.Size(400, 50);
            this.btnBukaPo.Click += new System.EventHandler(this.btnBukaPo_Click);

            this.pnlCard.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblJudul, this.lblProduk, this.cmbProduk, this.lblJenisPo, this.cmbJenisPo,
                this.lblHargaDiskon, this.numHargaDiskon, this.lblTarget, this.numTarget,
                this.lblBatasWaktu, this.dtpBatasWaktu, this.btnBukaPo
            });
            this.Controls.Add(this.pnlCard);

            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHargaDiskon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarget)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblJudul, lblProduk, lblJenisPo, lblHargaDiskon, lblTarget, lblBatasWaktu;
        private System.Windows.Forms.ComboBox cmbProduk, cmbJenisPo;
        private System.Windows.Forms.NumericUpDown numHargaDiskon, numTarget;
        private System.Windows.Forms.DateTimePicker dtpBatasWaktu;
        private System.Windows.Forms.Button btnBukaPo;
    }
}
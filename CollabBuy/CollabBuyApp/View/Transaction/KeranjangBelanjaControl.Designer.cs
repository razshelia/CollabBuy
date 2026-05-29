namespace CollabBuy.CollabBuyApp.View.Transaction
{
    partial class KeranjangBelanjaControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvKeranjang = new System.Windows.Forms.DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTotalText = new System.Windows.Forms.Label();
            this.lblTotalHarga = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnKosongkan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Keranjang Belanja";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(378, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Periksa kembali pesanan Anda sebelum memproses checkout.";
            // 
            // btnKosongkan
            // 
            this.btnKosongkan.BackColor = System.Drawing.Color.White;
            this.btnKosongkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKosongkan.FlatAppearance.BorderColor = System.Drawing.Color.LightCoral;
            this.btnKosongkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKosongkan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKosongkan.ForeColor = System.Drawing.Color.LightCoral;
            this.btnKosongkan.Location = new System.Drawing.Point(776, 60);
            this.btnKosongkan.Name = "btnKosongkan";
            this.btnKosongkan.Size = new System.Drawing.Size(160, 35);
            this.btnKosongkan.TabIndex = 2;
            this.btnKosongkan.Text = "🗑️ Kosongkan Keranjang";
            this.btnKosongkan.UseVisualStyleBackColor = false;
            this.btnKosongkan.Click += new System.EventHandler(this.btnKosongkan_Click);
            // 
            // dgvKeranjang
            // 
            this.dgvKeranjang.AllowUserToAddRows = false;
            this.dgvKeranjang.AllowUserToDeleteRows = false;
            this.dgvKeranjang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKeranjang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKeranjang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKeranjang.ColumnHeadersHeight = 40;
            this.dgvKeranjang.EnableHeadersVisualStyles = false;
            this.dgvKeranjang.Location = new System.Drawing.Point(36, 115);
            this.dgvKeranjang.Name = "dgvKeranjang";
            this.dgvKeranjang.ReadOnly = true;
            this.dgvKeranjang.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvKeranjang.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKeranjang.RowTemplate.Height = 40;
            this.dgvKeranjang.Size = new System.Drawing.Size(900, 360);
            this.dgvKeranjang.TabIndex = 3;
            this.dgvKeranjang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKeranjang_CellContentClick);
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Controls.Add(this.lblTotalText);
            this.pnlSummary.Controls.Add(this.lblTotalHarga);
            this.pnlSummary.Controls.Add(this.btnCheckout);
            this.pnlSummary.Location = new System.Drawing.Point(36, 495);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(900, 80);
            this.pnlSummary.TabIndex = 4;
            // 
            // lblTotalText
            // 
            this.lblTotalText.AutoSize = true;
            this.lblTotalText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalText.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalText.Location = new System.Drawing.Point(20, 28);
            this.lblTotalText.Name = "lblTotalText";
            this.lblTotalText.Size = new System.Drawing.Size(142, 21);
            this.lblTotalText.TabIndex = 0;
            this.lblTotalText.Text = "Total Pembayaran:";
            // 
            // lblTotalHarga
            // 
            this.lblTotalHarga.AutoSize = true;
            this.lblTotalHarga.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHarga.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTotalHarga.Location = new System.Drawing.Point(170, 24);
            this.lblTotalHarga.Name = "lblTotalHarga";
            this.lblTotalHarga.Size = new System.Drawing.Size(56, 30);
            this.lblTotalHarga.TabIndex = 1;
            this.lblTotalHarga.Text = "Rp 0";
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnCheckout.Location = new System.Drawing.Point(670, 18);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(210, 45);
            this.btnCheckout.TabIndex = 2;
            this.btnCheckout.Text = "💳 Proses Checkout";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // KeranjangBelanjaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.dgvKeranjang);
            this.Controls.Add(this.btnKosongkan);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "KeranjangBelanjaControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.KeranjangBelanjaControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnKosongkan;
        private System.Windows.Forms.DataGridView dgvKeranjang;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTotalText;
        private System.Windows.Forms.Label lblTotalHarga;
        private System.Windows.Forms.Button btnCheckout;
    }
}

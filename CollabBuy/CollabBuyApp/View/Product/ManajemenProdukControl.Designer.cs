namespace CollabBuy.CollabBuyApp.View.Product
{
    partial class ManajemenProdukControl
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
            this.btnTambahProduk = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvProduk = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduk)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(242, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manajemen Produk";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(359, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Kelola daftar produk reguler yang Anda jual di aplikasi ini.";
            // 
            // btnTambahProduk
            // 
            this.btnTambahProduk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnTambahProduk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambahProduk.FlatAppearance.BorderSize = 0;
            this.btnTambahProduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahProduk.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambahProduk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnTambahProduk.Location = new System.Drawing.Point(36, 110);
            this.btnTambahProduk.Name = "btnTambahProduk";
            this.btnTambahProduk.Size = new System.Drawing.Size(160, 35);
            this.btnTambahProduk.TabIndex = 2;
            this.btnTambahProduk.Text = "➕ Tambah Produk";
            this.btnTambahProduk.UseVisualStyleBackColor = false;
            this.btnTambahProduk.Click += new System.EventHandler(this.btnTambahProduk_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Location = new System.Drawing.Point(210, 110);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvProduk
            // 
            this.dgvProduk.AllowUserToAddRows = false;
            this.dgvProduk.AllowUserToDeleteRows = false;
            this.dgvProduk.BackgroundColor = System.Drawing.Color.White;
            this.dgvProduk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduk.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProduk.ColumnHeadersHeight = 40;
            this.dgvProduk.EnableHeadersVisualStyles = false;
            this.dgvProduk.Location = new System.Drawing.Point(36, 160);
            this.dgvProduk.Name = "dgvProduk";
            this.dgvProduk.ReadOnly = true;
            this.dgvProduk.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvProduk.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProduk.RowTemplate.Height = 35;
            this.dgvProduk.Size = new System.Drawing.Size(900, 435);
            this.dgvProduk.TabIndex = 4;
            this.dgvProduk.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduk_CellContentClick);
            // 
            // ManajemenProdukControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvProduk);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnTambahProduk);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "ManajemenProdukControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.ManajemenProdukControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnTambahProduk;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvProduk;
    }
}

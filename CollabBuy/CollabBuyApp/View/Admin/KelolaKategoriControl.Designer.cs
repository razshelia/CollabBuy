namespace CollabBuy.CollabBuyApp.View.Admin
{
    partial class KelolaKategoriControl
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
            this.dgvKategori = new System.Windows.Forms.DataGridView();
            this.pnlTambah = new System.Windows.Forms.Panel();
            this.lblKategoriBaru = new System.Windows.Forms.Label();
            this.txtKategoriBaru = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKategori)).BeginInit();
            this.pnlTambah.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manajemen Kategori";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(342, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Kelola daftar kategori produk yang tersedia di aplikasi.";
            // 
            // pnlTambah
            // 
            this.pnlTambah.BackColor = System.Drawing.Color.White;
            this.pnlTambah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTambah.Controls.Add(this.lblKategoriBaru);
            this.pnlTambah.Controls.Add(this.txtKategoriBaru);
            this.pnlTambah.Controls.Add(this.btnTambah);
            this.pnlTambah.Location = new System.Drawing.Point(36, 115);
            this.pnlTambah.Name = "pnlTambah";
            this.pnlTambah.Size = new System.Drawing.Size(900, 80);
            this.pnlTambah.TabIndex = 2;
            // 
            // lblKategoriBaru
            // 
            this.lblKategoriBaru.AutoSize = true;
            this.lblKategoriBaru.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKategoriBaru.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblKategoriBaru.Location = new System.Drawing.Point(20, 29);
            this.lblKategoriBaru.Name = "lblKategoriBaru";
            this.lblKategoriBaru.Size = new System.Drawing.Size(133, 17);
            this.lblKategoriBaru.TabIndex = 0;
            this.lblKategoriBaru.Text = "Nama Kategori Baru:";
            // 
            // txtKategoriBaru
            // 
            this.txtKategoriBaru.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKategoriBaru.Location = new System.Drawing.Point(165, 25);
            this.txtKategoriBaru.Name = "txtKategoriBaru";
            this.txtKategoriBaru.Size = new System.Drawing.Size(400, 27);
            this.txtKategoriBaru.TabIndex = 1;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnTambah.Location = new System.Drawing.Point(585, 23);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(120, 31);
            this.btnTambah.TabIndex = 2;
            this.btnTambah.Text = "➕ Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // dgvKategori
            // 
            this.dgvKategori.AllowUserToAddRows = false;
            this.dgvKategori.AllowUserToDeleteRows = false;
            this.dgvKategori.BackgroundColor = System.Drawing.Color.White;
            this.dgvKategori.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKategori.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKategori.ColumnHeadersHeight = 40;
            this.dgvKategori.EnableHeadersVisualStyles = false;
            this.dgvKategori.Location = new System.Drawing.Point(36, 215);
            this.dgvKategori.Name = "dgvKategori";
            this.dgvKategori.ReadOnly = true;
            this.dgvKategori.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvKategori.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKategori.RowTemplate.Height = 35;
            this.dgvKategori.Size = new System.Drawing.Size(900, 380);
            this.dgvKategori.TabIndex = 3;
            // 
            // KelolaKategoriControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvKategori);
            this.Controls.Add(this.pnlTambah);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "KelolaKategoriControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.KelolaKategoriControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKategori)).EndInit();
            this.pnlTambah.ResumeLayout(false);
            this.pnlTambah.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlTambah;
        private System.Windows.Forms.Label lblKategoriBaru;
        private System.Windows.Forms.TextBox txtKategoriBaru;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.DataGridView dgvKategori;
    }
}

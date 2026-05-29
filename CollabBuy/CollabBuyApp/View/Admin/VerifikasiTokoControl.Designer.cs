namespace CollabBuy.CollabBuyApp.View.Admin
{
        partial class VerifikasiTokoControl
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
                this.dgvVerifikasi = new System.Windows.Forms.DataGridView();
                this.btnRefresh = new System.Windows.Forms.Button();
                ((System.ComponentModel.ISupportInitialize)(this.dgvVerifikasi)).BeginInit();
                this.SuspendLayout();
                // 
                // lblTitle
                // 
                this.lblTitle.AutoSize = true;
                this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
                this.lblTitle.Location = new System.Drawing.Point(30, 30);
                this.lblTitle.Name = "lblTitle";
                this.lblTitle.Size = new System.Drawing.Size(193, 32);
                this.lblTitle.TabIndex = 0;
                this.lblTitle.Text = "Verifikasi Toko";
                // 
                // lblSubtitle
                // 
                this.lblSubtitle.AutoSize = true;
                this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
                this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
                this.lblSubtitle.Name = "lblSubtitle";
                this.lblSubtitle.Size = new System.Drawing.Size(376, 19);
                this.lblSubtitle.TabIndex = 1;
                this.lblSubtitle.Text = "Kelola pengajuan pembuatan lapak/toko dari pengguna baru.";
                // 
                // btnRefresh
                // 
                this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
                this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
                this.btnRefresh.FlatAppearance.BorderSize = 0;
                this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
                this.btnRefresh.Location = new System.Drawing.Point(816, 60);
                this.btnRefresh.Name = "btnRefresh";
                this.btnRefresh.Size = new System.Drawing.Size(120, 35);
                this.btnRefresh.TabIndex = 2;
                this.btnRefresh.Text = "🔄 Refresh Data";
                this.btnRefresh.UseVisualStyleBackColor = false;
                this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
                // 
                // dgvVerifikasi
                // 
                this.dgvVerifikasi.AllowUserToAddRows = false;
                this.dgvVerifikasi.AllowUserToDeleteRows = false;
                this.dgvVerifikasi.BackgroundColor = System.Drawing.Color.White;
                this.dgvVerifikasi.BorderStyle = System.Windows.Forms.BorderStyle.None;
                dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
                dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
                dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
                dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
                dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                this.dgvVerifikasi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
                this.dgvVerifikasi.ColumnHeadersHeight = 40;
                this.dgvVerifikasi.EnableHeadersVisualStyles = false;
                this.dgvVerifikasi.Location = new System.Drawing.Point(36, 115);
                this.dgvVerifikasi.Name = "dgvVerifikasi";
                this.dgvVerifikasi.ReadOnly = true;
                this.dgvVerifikasi.RowHeadersVisible = false;
                dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
                dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
                this.dgvVerifikasi.RowsDefaultCellStyle = dataGridViewCellStyle2;
                this.dgvVerifikasi.RowTemplate.Height = 35;
                this.dgvVerifikasi.Size = new System.Drawing.Size(900, 480);
                this.dgvVerifikasi.TabIndex = 3;
                this.dgvVerifikasi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVerifikasi_CellContentClick);
                // 
                // VerifikasiTokoControl
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
                this.Controls.Add(this.dgvVerifikasi);
                this.Controls.Add(this.btnRefresh);
                this.Controls.Add(this.lblSubtitle);
                this.Controls.Add(this.lblTitle);
                this.Name = "VerifikasiTokoControl";
                this.Size = new System.Drawing.Size(1000, 650);
                this.Load += new System.EventHandler(this.VerifikasiTokoControl_Load);
                ((System.ComponentModel.ISupportInitialize)(this.dgvVerifikasi)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.Button btnRefresh;
            private System.Windows.Forms.DataGridView dgvVerifikasi;
        }
    }

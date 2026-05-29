namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    partial class SesiPOAktifControl
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.flpSesiPO = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sesi Pre-Order Aktif";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(34, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(431, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Ikuti sesi penitipan Danus atau PO sebelum kehabisan kuota dan waktu.";
            // 
            // txtCari
            // 
            this.txtCari.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCari.Location = new System.Drawing.Point(36, 110);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(400, 27);
            this.txtCari.TabIndex = 2;
            this.txtCari.Text = "Cari sesi PO...";
            this.txtCari.ForeColor = System.Drawing.Color.Gray;
            this.txtCari.Enter += new System.EventHandler(this.txtCari_Enter);
            this.txtCari.Leave += new System.EventHandler(this.txtCari_Leave);
            // 
            // btnCari
            // 
            this.btnCari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnCari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCari.FlatAppearance.BorderSize = 0;
            this.btnCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCari.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCari.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(182)))));
            this.btnCari.Location = new System.Drawing.Point(445, 108);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(100, 31);
            this.btnCari.TabIndex = 3;
            this.btnCari.Text = "🔍 Cari";
            this.btnCari.UseVisualStyleBackColor = false;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            this.btnRefresh.Location = new System.Drawing.Point(555, 108);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 31);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // flpSesiPO
            // 
            this.flpSesiPO.AutoScroll = true;
            this.flpSesiPO.Location = new System.Drawing.Point(36, 160);
            this.flpSesiPO.Name = "flpSesiPO";
            this.flpSesiPO.Size = new System.Drawing.Size(900, 450);
            this.flpSesiPO.TabIndex = 5;
            // 
            // SesiPOAktifControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.flpSesiPO);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "SesiPOAktifControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.SesiPOAktifControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flpSesiPO;
    }
}

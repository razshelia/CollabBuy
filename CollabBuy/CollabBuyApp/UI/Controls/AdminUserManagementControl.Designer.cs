namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class AdminUserManagementControl
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
            this.lblJudul = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnBlock = new System.Windows.Forms.Button();
            this.pnlAction = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.pnlAction.SuspendLayout();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1200, 800);

            this.lblJudul.Text = "USER MANAGEMENT AREA 🕵️‍♂️";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 18F);
            this.lblJudul.Location = new System.Drawing.Point(30, 30);
            this.lblJudul.AutoSize = true;

            // DataGridView Neo-Retro
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(30, 100);
            this.dgvUsers.Size = new System.Drawing.Size(1100, 450);
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // Panel Action (Bottom)
            this.pnlAction.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning Logo
            this.pnlAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAction.Controls.Add(this.btnBlock);
            this.pnlAction.Controls.Add(this.btnApprove);
            this.pnlAction.Location = new System.Drawing.Point(30, 580);
            this.pnlAction.Size = new System.Drawing.Size(1100, 100);

            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnApprove.Text = "APPROVE SELLER KAK! ✅";
            this.btnApprove.Location = new System.Drawing.Point(30, 25);
            this.btnApprove.Size = new System.Drawing.Size(250, 50);
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);

            this.btnBlock.BackColor = System.Drawing.Color.White;
            this.btnBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlock.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBlock.Text = "BLOCK USER NAKAL 🚫";
            this.btnBlock.Location = new System.Drawing.Point(310, 25);
            this.btnBlock.Size = new System.Drawing.Size(250, 50);
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);

            this.Controls.Add(this.pnlAction);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lblJudul);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.pnlAction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.Button btnApprove, btnBlock;
    }
}
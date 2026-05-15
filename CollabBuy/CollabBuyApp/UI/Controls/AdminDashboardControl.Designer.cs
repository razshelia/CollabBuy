namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class AdminDashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            pnlCards = new FlowLayoutPanel();
            lblTotalUser = new Label();
            lblTotalProduk = new Label();
            lblTotalTransaksi = new Label();
            lblTotalAduan = new Label();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI Black", 22F);
            lblWelcome.ForeColor = Color.FromArgb(45, 27, 79);
            lblWelcome.Location = new Point(40, 30);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(500, 50);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "DASHBOARD ADMIN 💼";
            // 
            // pnlCards
            // 
            pnlCards.BackColor = Color.Transparent;
            pnlCards.Location = new Point(40, 110);
            pnlCards.Name = "pnlCards";
            pnlCards.Size = new Size(900, 300);
            pnlCards.TabIndex = 1;
            // 
            // lblTotalUser
            // 
            lblTotalUser.Location = new Point(0, 0);
            lblTotalUser.Name = "lblTotalUser";
            lblTotalUser.Size = new Size(100, 23);
            lblTotalUser.TabIndex = 0;
            // 
            // lblTotalProduk
            // 
            lblTotalProduk.Location = new Point(0, 0);
            lblTotalProduk.Name = "lblTotalProduk";
            lblTotalProduk.Size = new Size(100, 23);
            lblTotalProduk.TabIndex = 0;
            // 
            // lblTotalTransaksi
            // 
            lblTotalTransaksi.Location = new Point(0, 0);
            lblTotalTransaksi.Name = "lblTotalTransaksi";
            lblTotalTransaksi.Size = new Size(100, 23);
            lblTotalTransaksi.TabIndex = 0;
            // 
            // lblTotalAduan
            // 
            lblTotalAduan.Location = new Point(0, 0);
            lblTotalAduan.Name = "lblTotalAduan";
            lblTotalAduan.Size = new Size(100, 23);
            lblTotalAduan.TabIndex = 0;
            // 
            // AdminDashboardControl
            // 
            BackColor = Color.FromArgb(255, 249, 230);
            Controls.Add(lblWelcome);
            Controls.Add(pnlCards);
            Name = "AdminDashboardControl";
            Size = new Size(1046, 333);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel BuatCardStat(string judul, ref System.Windows.Forms.Label lblValue)
        {
            Panel card = new Panel();
            card.Size = new Size(200, 120);
            card.BackColor = System.Drawing.Color.FromArgb(45, 27, 79);
            card.Margin = new Padding(10);
            Label lblJudul = new Label()
            {
                Text = judul,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                ForeColor = System.Drawing.Color.White,
                Size = new Size(180, 30),
                Location = new Point(10, 10)
            };
            lblValue = new Label()
            {
                Text = "0",
                Font = new System.Drawing.Font("Segoe UI Black", 24F),
                ForeColor = System.Drawing.Color.FromArgb(253, 224, 71),
                Size = new Size(180, 50),
                Location = new Point(10, 50)
            };
            card.Controls.Add(lblJudul);
            card.Controls.Add(lblValue);
            return card;
        }

        private System.Windows.Forms.Label lblWelcome, lblTotalUser, lblTotalProduk, lblTotalTransaksi, lblTotalAduan;
        private System.Windows.Forms.FlowLayoutPanel pnlCards;
    }
}
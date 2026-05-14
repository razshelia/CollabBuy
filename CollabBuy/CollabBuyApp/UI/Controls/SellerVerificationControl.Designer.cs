namespace CollabBuy.CollabBuyApp.UI.Controls
{
    partial class SellerVerificationControl
    {
        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNamaToko = new System.Windows.Forms.Label();
            this.txtNamaToko = new System.Windows.Forms.TextBox();
            this.lblNim = new System.Windows.Forms.Label();
            this.txtNim = new System.Windows.Forms.MaskedTextBox();
            this.lblTahun = new System.Windows.Forms.Label();
            this.numTahun = new System.Windows.Forms.NumericUpDown();
            this.lblKtm = new System.Windows.Forms.Label();
            this.btnUploadKtm = new System.Windows.Forms.Button();
            this.btnKirim = new System.Windows.Forms.Button();

            // Background & Card Setup
            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(255, 235, 133); // Kuning Logo
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Location = new System.Drawing.Point(250, 80);
            this.pnlCard.Size = new System.Drawing.Size(500, 520);

            // Judul (Gen Z Friendly)
            this.lblJudul.Text = "UPGRADE JADI SELLER 👨‍🎓";
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(40, 30);
            this.lblJudul.AutoSize = true;

            // Input Nama Toko
            this.lblNamaToko.Text = "Nama Toko Kamu:";
            this.lblNamaToko.Location = new System.Drawing.Point(44, 100);
            this.txtNamaToko.Location = new System.Drawing.Point(48, 125);
            this.txtNamaToko.Size = new System.Drawing.Size(400, 30);

            // Input NIM (Masked untuk Angka)
            this.lblNim.Text = "NIM (Biar ketahuan maba mana):";
            this.lblNim.Location = new System.Drawing.Point(44, 170);
            this.txtNim.Mask = "000000000000000000";
            this.txtNim.Location = new System.Drawing.Point(48, 195);
            this.txtNim.Size = new System.Drawing.Size(400, 30);

            // Input Tahun Masuk (Numeric Tool)
            this.lblTahun.Text = "Tahun Masuk (Angka aja ya):";
            this.lblTahun.Location = new System.Drawing.Point(44, 240);
            this.numTahun.Minimum = 2000;
            this.numTahun.Maximum = 2026;
            this.numTahun.Value = 2024;
            this.numTahun.Location = new System.Drawing.Point(48, 265);
            this.numTahun.Size = new System.Drawing.Size(120, 30);

            // Upload KTM (Gaya Neo-Retro)
            this.lblKtm.Text = "Upload Foto KTM (Link/Path):";
            this.lblKtm.Location = new System.Drawing.Point(44, 310);
            this.btnUploadKtm.Text = "CARI FILE 📁";
            this.btnUploadKtm.BackColor = System.Drawing.Color.White;
            this.btnUploadKtm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadKtm.Location = new System.Drawing.Point(48, 335);
            this.btnUploadKtm.Size = new System.Drawing.Size(400, 40);

            // Submit Button
            this.btnKirim.Text = "GAS AJUKAN! ✨";
            this.btnKirim.BackColor = System.Drawing.Color.FromArgb(170, 150, 218); // Ungu Logo
            this.btnKirim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirim.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirim.Location = new System.Drawing.Point(48, 420);
            this.btnKirim.Size = new System.Drawing.Size(400, 50);

            // Assembly
            this.pnlCard.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblJudul, lblNamaToko, txtNamaToko, lblNim, txtNim,
                lblTahun, numTahun, lblKtm, btnUploadKtm, btnKirim
            });
            this.Controls.Add(pnlCard);
        }
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblJudul, lblNamaToko, lblNim, lblTahun, lblKtm;
        private System.Windows.Forms.TextBox txtNamaToko;
        private System.Windows.Forms.MaskedTextBox txtNim;
        private System.Windows.Forms.NumericUpDown numTahun;
        private System.Windows.Forms.Button btnUploadKtm, btnKirim;
    }
}
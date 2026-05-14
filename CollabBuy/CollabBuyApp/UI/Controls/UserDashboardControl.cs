using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class UserDashboardControl : UserControl
    {
        private Akun userAktif;

        public UserDashboardControl(Akun akun)
        {
            InitializeComponent();
            this.userAktif = akun;
            this.lblSapaan.Text = $"Hola {akun.Username}! Spill Produk Hari Ini ✨";
            this.lblSubtitle.Text = "Temukan barang Danus favoritmu sekarang!";
            this.MuatBarangDummy();
        }

        private void MuatBarangDummy()
        {
            this.flpKonten.Controls.Clear();

            string[] namaBarang = {
                "Ganci Himatif 2024", "Hoodie Kampus XL", "Sticker Pack OOP",
                "Mug Coding Night", "Totebag Wisuda", "Pin Set HMI"
            };
            string[] harga = { "Rp 15.000", "Rp 120.000", "Rp 25.000",
                                "Rp 45.000", "Rp 60.000", "Rp 10.000" };

            for (int i = 0; i < namaBarang.Length; i++)
            {
                Panel card = new Panel();
                card.Size      = new Size(220, 300);
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Margin    = new Padding(10);
                card.Cursor    = Cursors.Hand;

                // Foto placeholder (coloured block)
                Panel foto = new Panel();
                foto.Size      = new Size(220, 150);
                foto.Location  = new Point(0, 0);
                foto.BackColor = (i % 2 == 0)
                    ? Color.FromArgb(255, 235, 133)
                    : Color.FromArgb(200, 190, 240);

                Label lblEmoji = new Label();
                lblEmoji.Text      = "🛍️";
                lblEmoji.Font      = new Font("Segoe UI", 28F);
                lblEmoji.AutoSize  = false;
                lblEmoji.Size      = new Size(220, 150);
                lblEmoji.TextAlign = ContentAlignment.MiddleCenter;
                foto.Controls.Add(lblEmoji);

                Label lblNama = new Label();
                lblNama.Text      = namaBarang[i];
                lblNama.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
                lblNama.ForeColor = Color.FromArgb(40, 40, 60);
                lblNama.AutoSize  = false;
                lblNama.Size      = new Size(200, 40);
                lblNama.Location  = new Point(10, 158);
                lblNama.TextAlign = ContentAlignment.MiddleLeft;

                Label lblHarga = new Label();
                lblHarga.Text      = harga[i];
                lblHarga.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
                lblHarga.ForeColor = Color.FromArgb(100, 80, 170);
                lblHarga.AutoSize  = false;
                lblHarga.Size      = new Size(200, 28);
                lblHarga.Location  = new Point(10, 198);

                Button btnBeli = new Button();
                btnBeli.Text                              = "GAS CHECKOUT! 🛒";
                btnBeli.Font                              = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnBeli.BackColor                         = Color.FromArgb(170, 150, 218);
                btnBeli.ForeColor                         = Color.White;
                btnBeli.FlatStyle                         = FlatStyle.Flat;
                btnBeli.FlatAppearance.BorderSize         = 0;
                btnBeli.FlatAppearance.MouseOverBackColor = Color.FromArgb(145, 125, 195);
                btnBeli.Size                              = new Size(200, 38);
                btnBeli.Location                          = new Point(10, 252);
                btnBeli.Cursor                            = Cursors.Hand;

                card.Controls.Add(foto);
                card.Controls.Add(lblNama);
                card.Controls.Add(lblHarga);
                card.Controls.Add(btnBeli);
                this.flpKonten.Controls.Add(card);
            }
        }
    }
}

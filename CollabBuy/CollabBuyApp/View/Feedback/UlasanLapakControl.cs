using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class UlasanLapakControl : UserControl
    {
        private readonly User _seller;
        private readonly ReviewController _controller;

        public UlasanLapakControl(User seller)
        {
            InitializeComponent();
            _seller = seller;
            _controller = new ReviewController();
            LoadUlasan();
        }

        private void LoadUlasan()
        {
            flpUlasan.Controls.Clear();
            DataTable dt = _controller.GetReviewLapak(_seller.GetIdUser());

            if (dt.Rows.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "Belum ada yang review nih, semangat promosinya ya bestie! 🚀",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10)
                };
                flpUlasan.Controls.Add(lblKosong);
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                Panel pnl = new Panel
                {
                    Width = 850,
                    Height = 150,
                    BackColor = Color.FromArgb(224, 170, 255), // Soft purple
                    Margin = new Padding(10, 10, 10, 15)
                };

                Label lblNama = new Label
                {
                    Text = $"{r["nama_pembeli"]} ⭐ {r["rating"]}",
                    Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Deep purple
                    Location = new Point(15, 15),
                    AutoSize = true
                };

                Label lblProduk = new Label
                {
                    Text = $"Barang: {r["nama_produk"]}",
                    Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(90, 24, 154),
                    Location = new Point(15, 40),
                    AutoSize = true
                };

                Label lblKomen = new Label
                {
                    Text = $"\"{r["komentar"]}\"",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(36, 0, 70),
                    Location = new Point(15, 65),
                    AutoSize = true,
                    MaximumSize = new Size(650, 0) // Teks akan otomatis turun (wrap) jika kepanjangan
                };

                pnl.Controls.Add(lblNama);
                pnl.Controls.Add(lblProduk);
                pnl.Controls.Add(lblKomen);

                // Jika belum dibalas oleh penjual
                if (string.IsNullOrEmpty(r["balasan_penjual"].ToString()))
                {
                    Button btnBalas = new Button
                    {
                        Text = "Balas Komen",
                        Location = new Point(700, 50),
                        Size = new Size(120, 40),
                        BackColor = Color.FromArgb(36, 0, 70),
                        ForeColor = Color.FromArgb(253, 255, 182), // Soft yellow
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI Black", 9F, FontStyle.Bold),
                        Cursor = Cursors.Hand
                    };
                    btnBalas.FlatAppearance.BorderSize = 0;
                    btnBalas.Tag = r["id_ulasan"];
                    btnBalas.Click += BtnBalas_Click;
                    pnl.Controls.Add(btnBalas);
                }
                else
                {
                    Label lblBalasan = new Label
                    {
                        Text = $"Balasanmu: {r["balasan_penjual"]}",
                        Font = new Font("Segoe UI Semibold", 9.5F),
                        ForeColor = Color.DarkGreen,
                        Location = new Point(15, 110),
                        AutoSize = true,
                        MaximumSize = new Size(800, 0)
                    };
                    pnl.Controls.Add(lblBalasan);
                }

                flpUlasan.Controls.Add(pnl);
            }
        }

        private void BtnBalas_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUlasan = Convert.ToInt32(btn.Tag);

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Balas ulasan ini (Gen-Z style ya bestie!):",
                "Balas Review Customer",
                "");

            if (!string.IsNullOrWhiteSpace(input))
            {
                // Menambahkan parameter GetIdUser() sesuai format controller sebelumnya
                var res = _controller.BalasUlasanLapak(idUlasan, input, _seller.GetIdUser());

                if (res.sukses)
                {
                    MessageBox.Show(res.pesan, "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUlasan(); // Refresh list otomatis setelah dibalas
                }
                else
                {
                    MessageBox.Show(res.pesan, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
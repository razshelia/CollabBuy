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
        private readonly Models.User _seller;
        private readonly ReviewController _controller;

        public UlasanLapakControl(Models.User seller)
        {
            this.InitializeComponent();

            this._seller = seller;
            this._controller = new ReviewController();

            this.LoadUlasan();
            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void LoadUlasan()
        {
            this.flpUlasan.Controls.Clear();
            DataTable dt = this._controller.GetReviewLapak(this._seller.IdUser);

            if (dt == null || dt.Rows.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "Belum ada yang review nih, semangat promosinya ya bestie! 🚀",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10)
                };
                this.flpUlasan.Controls.Add(lblKosong);
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                int ratingValue = Convert.ToInt32(r["rating"]);
                string komentarValue = r["komentar"].ToString();
                string balasanValue = r["balasan_penjual"].ToString().Trim();

                // Kalkulasi UI langsung dari variable — tidak perlu objek Review
                string bintangUI = new string('⭐', ratingValue);
                string komenUI = komentarValue.Length > 120
                    ? komentarValue.Substring(0, 120) + "..."
                    : komentarValue;
                bool sudahDibalas = !string.IsNullOrWhiteSpace(balasanValue);
                string statusBalasan = sudahDibalas ? "✅ Telah Dibalas Penjual" : "⏳ Menunggu Balasan";

                string tanggalFormatted;
                if (r.Table.Columns.Contains("tanggal_ulasan") && r["tanggal_ulasan"] != DBNull.Value)
                    tanggalFormatted = Convert.ToDateTime(r["tanggal_ulasan"]).ToString("dd MMM yyyy, HH:mm");
                else
                    tanggalFormatted = "Tanggal tidak diketahui";

                Panel pnl = new Panel
                {
                    Width = 850,
                    Height = 175,
                    BackColor = Color.FromArgb(224, 170, 255),
                    Margin = new Padding(10, 10, 10, 15)
                };

                Label lblNama = new Label
                {
                    Text = $"{r["nama_pembeli"]} {bintangUI}",
                    Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70),
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
                    Text = $"\"{komenUI}\"",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(36, 0, 70),
                    Location = new Point(15, 65),
                    AutoSize = true,
                    MaximumSize = new Size(650, 0)
                };

                Label lblTanggal = new Label
                {
                    Text = $"🕐 {tanggalFormatted}",
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.Gray,
                    Location = new Point(15, 130),
                    AutoSize = true
                };

                Label lblStatusBalasan = new Label
                {
                    Text = statusBalasan,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = sudahDibalas ? Color.DarkGreen : Color.DarkOrange,
                    Location = new Point(700, 15),
                    AutoSize = true
                };

                pnl.Controls.Add(lblNama);
                pnl.Controls.Add(lblProduk);
                pnl.Controls.Add(lblKomen);
                pnl.Controls.Add(lblTanggal);
                pnl.Controls.Add(lblStatusBalasan);

                if (!sudahDibalas)
                {
                    Button btnBalas = new Button
                    {
                        Text = "Balas Komen",
                        Location = new Point(700, 50),
                        Size = new Size(120, 40),
                        BackColor = Color.FromArgb(36, 0, 70),
                        ForeColor = Color.FromArgb(253, 255, 182),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI Black", 9F, FontStyle.Bold),
                        Cursor = Cursors.Hand,
                        Tag = r["id_ulasan"]
                    };
                    btnBalas.FlatAppearance.BorderSize = 0;
                    btnBalas.Click += this.BtnBalas_Click;
                    pnl.Controls.Add(btnBalas);
                }
                else
                {
                    Label lblBalasan = new Label
                    {
                        Text = $"Balasanmu: {balasanValue}",
                        Font = new Font("Segoe UI Semibold", 9.5F),
                        ForeColor = Color.DarkGreen,
                        Location = new Point(15, 110),
                        AutoSize = true,
                        MaximumSize = new Size(800, 0)
                    };
                    pnl.Controls.Add(lblBalasan);
                }

                this.flpUlasan.Controls.Add(pnl);
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            this.flpUlasan.Width = this.Width - (margin * 2);
            this.flpUlasan.Height = this.Height - this.flpUlasan.Top - margin;
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
                var (sukses, pesan) = this._controller.BalasUlasanLapak(idUlasan, input, this._seller.IdUser);

                if (sukses)
                {
                    MessageBox.Show(pesan, "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.LoadUlasan();
                }
                else
                {
                    MessageBox.Show(pesan, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Penjual klik cancel atau nge-submit kotak dialog kosong
                bool abaikanBatal = true;
            }
        }
    }
}
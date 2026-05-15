using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ReviewControl : UserControl
    {
        private int _idPenjual;
        private ReviewService _reviewService;

        public ReviewControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _reviewService = new ReviewService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var daftar = _reviewService.AmbilUlasanPenjual(_idPenjual);
                TampilkanUlasan(daftar);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat ulasan: " + ex.Message);
            }
        }

        private void TampilkanUlasan(List<Review> daftar)
        {
            flowPanel.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada ulasan, bestie! 🥺\nSemoga cepet dapet bintang 5~";
                lblKosong.Font = new Font("Segoe UI", 14F);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
                flowPanel.Controls.Add(lblKosong);
                return;
            }

            foreach (var review in daftar)
            {
                Panel card = BuatCardReview(review);
                flowPanel.Controls.Add(card);
            }
        }

        private Panel BuatCardReview(Review review)
        {
            Panel card = new Panel();
            card.Size = new Size(680, 120);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            // Rating
            string bintang = new string('⭐', review.Rating);
            Label lblRating = new Label();
            lblRating.Text = bintang;
            lblRating.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRating.ForeColor = Color.FromArgb(253, 224, 71);
            lblRating.Size = new Size(200, 30);
            lblRating.Location = new Point(15, 10);

            // Komentar
            Label lblKomentar = new Label();
            lblKomentar.Text = review.Komentar ?? "Tidak ada komentar.";
            lblKomentar.Font = new Font("Segoe UI", 9F);
            lblKomentar.ForeColor = Color.Gray;
            lblKomentar.Size = new Size(500, 40);
            lblKomentar.Location = new Point(15, 45);

            card.Controls.Add(lblRating);
            card.Controls.Add(lblKomentar);

            // Balasan atau tombol balas
            if (!string.IsNullOrEmpty(review.BalasanPenjual))
            {
                Label lblBalasan = new Label();
                lblBalasan.Text = $"Balasan kamu: {review.BalasanPenjual}";
                lblBalasan.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                lblBalasan.ForeColor = Color.DarkGreen;
                lblBalasan.Size = new Size(300, 20);
                lblBalasan.Location = new Point(15, 85);
                card.Controls.Add(lblBalasan);
            }
            else
            {
                Button btnBalas = new Button();
                btnBalas.Text = "💬 Balas";
                btnBalas.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                btnBalas.BackColor = Color.FromArgb(167, 139, 250);
                btnBalas.ForeColor = Color.White;
                btnBalas.FlatStyle = FlatStyle.Flat;
                btnBalas.Size = new Size(80, 28);
                btnBalas.Location = new Point(400, 70);
                btnBalas.Click += (s, e) =>
                {
                    string balasan = InputDialog.Show(
                        "Tulis balasanmu:", "Balas Ulasan", "");
                    if (!string.IsNullOrWhiteSpace(balasan))
                    {
                        if (_reviewService.BalasUlasan(review.IdUlasan, balasan))
                            LoadData();
                    }
                };
                card.Controls.Add(btnBalas);
            }

            return card;
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
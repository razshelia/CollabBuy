using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ReviewControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly ReviewService _reviewService;

        public ReviewControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _reviewService = new ReviewService(new ReviewRepository());

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
                Label lblKosong = new Label()
                {
                    Text = "Belum ada ulasan, bestie! 🥺\nSemoga cepet dapet bintang 5~",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
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
            // Desain Card Review bergaya Flat Neo-Retro
            Panel card = new Panel()
            {
                Size = new Size(850, 130), // Lebar untuk full screen
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Garis pinggir tegas
            };

            // Rating
            string bintang = new string('⭐', review.Rating);
            Label lblRating = new Label()
            {
                Text = bintang,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(253, 224, 71), // Kuning cerah
                Size = new Size(200, 35),
                Location = new Point(15, 10)
            };

            // Komentar
            Label lblKomentar = new Label()
            {
                Text = review.Komentar ?? "Tidak ada komentar tertulis.",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                Size = new Size(600, 45),
                Location = new Point(15, 50)
            };

            card.Controls.Add(lblRating);
            card.Controls.Add(lblKomentar);

            // Jika ada balasan atau tombol balas
            if (!string.IsNullOrEmpty(review.BalasanPenjual))
            {
                card.Size = new Size(850, 170); // Perbesar card jika ada balasan

                Label lblBalasan = new Label()
                {
                    Text = $"💬 Balasan Kamu: {review.BalasanPenjual}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic | FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70),
                    BackColor = Color.FromArgb(253, 255, 182), // Kuning pastel bubble
                    Size = new Size(800, 45),
                    Location = new Point(15, 100),
                    Padding = new Padding(10, 5, 5, 5),
                    BorderStyle = BorderStyle.FixedSingle
                };
                card.Controls.Add(lblBalasan);
            }
            else
            {
                Button btnBalas = new Button()
                {
                    Text = "💬 Balas Ulasan",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    BackColor = Color.FromArgb(200, 182, 255), // Ungu pastel
                    ForeColor = Color.FromArgb(36, 0, 70),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(130, 35),
                    Location = new Point(690, 45),
                    Cursor = Cursors.Hand
                };
                btnBalas.FlatAppearance.BorderSize = 1;
                btnBalas.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);

                btnBalas.Click += (s, e) =>
                {
                    string balasan = InputDialog.Show("Tulis balasanmu buat customer ini:", "Balas Ulasan", "");
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
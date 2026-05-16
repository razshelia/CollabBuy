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
    public partial class ComplaintListControl : UserControl
    {
        private readonly ComplaintService _complaintService;

        public ComplaintListControl()
        {
            InitializeComponent();

            // TAHAP 4: INJEKSI MANUAL DI UI
            _complaintService = new ComplaintService(new ComplaintRepository());

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var daftar = _complaintService.AmbilSemuaAduan();
                TampilkanAduan(daftar);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat aduan: " + ex.Message);
            }
        }

        private void TampilkanAduan(List<Complaint> daftar)
        {
            flowPanel.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label()
                {
                    Text = "Belum ada aduan nih, bestie! 🎉\nSemoga tetap aman terkendali~",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
                flowPanel.Controls.Add(lblKosong);
                return;
            }

            foreach (var aduan in daftar)
            {
                Panel card = BuatCardAduan(aduan);
                flowPanel.Controls.Add(card);
            }
        }

        private Panel BuatCardAduan(Complaint aduan)
        {
            // Desain Card Aduan (Admin View)
            Panel card = new Panel()
            {
                Size = new Size(850, 150), // Lebar untuk full screen
                BackColor = Color.FromArgb(253, 255, 182), // Kuning pastel
                Margin = new Padding(10),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Garis pinggir tegas
            };

            Label lblSubjek = new Label()
            {
                Text = $"📢 {aduan.Subjek.ToUpper()}",
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.FromArgb(36, 0, 70),
                Size = new Size(600, 25),
                Location = new Point(15, 15)
            };

            Label lblDeskripsi = new Label()
            {
                Text = aduan.Deskripsi,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(80, 80, 80),
                Size = new Size(600, 45),
                Location = new Point(15, 45)
            };

            Label lblStatus = new Label()
            {
                Text = aduan.IsSelesai ? "✅ SELESAI" : "⏳ MENUNGGU TINDAKAN",
                Font = new Font("Segoe UI Black", 10F),
                ForeColor = aduan.IsSelesai ? Color.FromArgb(0, 150, 0) : Color.FromArgb(220, 120, 0),
                Size = new Size(200, 20),
                Location = new Point(15, 100)
            };

            card.Controls.Add(lblSubjek);
            card.Controls.Add(lblDeskripsi);
            card.Controls.Add(lblStatus);

            // Jika sudah ada balasan, tampilkan kotak balasan dan perbesar card
            if (!string.IsNullOrEmpty(aduan.Balasan))
            {
                card.Size = new Size(850, 190);

                Label lblBalasan = new Label()
                {
                    Text = $"💬 Balasan Anda: {aduan.Balasan}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic),
                    ForeColor = Color.FromArgb(36, 0, 70),
                    BackColor = Color.FromArgb(200, 182, 255), // Latar ungu pastel
                    Size = new Size(800, 45),
                    Location = new Point(15, 125),
                    Padding = new Padding(10, 5, 5, 5),
                    BorderStyle = BorderStyle.FixedSingle
                };
                card.Controls.Add(lblBalasan);
            }

            // Tombol aksi untuk admin jika belum selesai
            if (!aduan.IsSelesai)
            {
                Button btnSelesai = new Button()
                {
                    Text = "✅ Selesai",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    BackColor = Color.FromArgb(182, 255, 200), // Soft Green
                    ForeColor = Color.FromArgb(36, 0, 70),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 35),
                    Location = new Point(620, 95),
                    Cursor = Cursors.Hand
                };
                btnSelesai.FlatAppearance.BorderSize = 1;
                btnSelesai.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
                btnSelesai.Click += (s, e) =>
                {
                    if (_complaintService.TandaiSelesai(aduan.IdAduan))
                        LoadData();
                };
                card.Controls.Add(btnSelesai);

                Button btnBalas = new Button()
                {
                    Text = "💬 Balas",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    BackColor = Color.FromArgb(200, 182, 255), // Pastel Purple
                    ForeColor = Color.FromArgb(36, 0, 70),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 35),
                    Location = new Point(730, 95),
                    Cursor = Cursors.Hand
                };
                btnBalas.FlatAppearance.BorderSize = 1;
                btnBalas.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
                btnBalas.Click += (s, e) =>
                {
                    string balasan = InputDialog.Show("Tulis balasan untuk aduan ini:", "Balas Aduan", "");
                    if (!string.IsNullOrWhiteSpace(balasan))
                    {
                        if (_complaintService.BalasAduan(aduan.IdAduan, balasan))
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
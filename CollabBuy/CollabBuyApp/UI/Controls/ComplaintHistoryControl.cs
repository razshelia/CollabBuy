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
    public partial class ComplaintHistoryControl : UserControl
    {
        private readonly int _idUser;
        private readonly ComplaintService _complaintService;

        public ComplaintHistoryControl(int idUser)
        {
            InitializeComponent();
            _idUser = idUser;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _complaintService = new ComplaintService(new ComplaintRepository());

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var daftar = _complaintService.AmbilAduanByUser(_idUser);
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
                    Text = "Kamu belum pernah kirim aduan, bestie! 🎉",
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
            // Desain Card Riwayat bergaya Flat Neo-Retro
            Panel card = new Panel()
            {
                Size = new Size(850, 110), // Lebar untuk full screen
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

            Label lblStatus = new Label()
            {
                Text = aduan.IsSelesai ? "✅ SELESAI" : "⏳ MENUNGGU RESPON",
                Font = new Font("Segoe UI Black", 10F),
                ForeColor = aduan.IsSelesai ? Color.FromArgb(0, 150, 0) : Color.FromArgb(220, 120, 0),
                Size = new Size(200, 20),
                Location = new Point(15, 45)
            };

            card.Controls.Add(lblSubjek);
            card.Controls.Add(lblStatus);

            // Jika ada balasan, tampilkan di dalam kotak bubble retro
            if (!string.IsNullOrEmpty(aduan.Balasan))
            {
                card.Size = new Size(850, 150); // Perpanjang ukuran kartu

                Label lblBalasan = new Label()
                {
                    Text = $"💬 Balasan Admin: {aduan.Balasan}",
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold | FontStyle.Italic),
                    ForeColor = Color.FromArgb(36, 0, 70),
                    BackColor = Color.FromArgb(200, 182, 255), // Latar ungu pastel untuk balasan
                    Size = new Size(800, 45),
                    Location = new Point(15, 75),
                    Padding = new Padding(10, 10, 5, 5),
                    BorderStyle = BorderStyle.FixedSingle
                };
                card.Controls.Add(lblBalasan);
            }

            return card;
        }
    }
}
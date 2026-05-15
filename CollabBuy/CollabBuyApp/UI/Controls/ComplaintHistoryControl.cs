using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ComplaintHistoryControl : UserControl
    {
        private int _idUser;
        private ComplaintService _complaintService;

        public ComplaintHistoryControl(int idUser)
        {
            InitializeComponent();
            _idUser = idUser;
            _complaintService = new ComplaintService();
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
                Label lblKosong = new Label();
                lblKosong.Text = "Kamu belum pernah kirim aduan, bestie! 🎉";
                lblKosong.Font = new Font("Segoe UI", 14F);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
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
            Panel card = new Panel();
            card.Size = new Size(680, 110);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            Label lblSubjek = new Label()
            {
                Text = $"📢 {aduan.Subjek}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 27, 79),
                Size = new Size(400, 25),
                Location = new Point(15, 10)
            };

            Label lblStatus = new Label()
            {
                Text = aduan.IsSelesai ? "✅ Selesai" : "⏳ Menunggu",
                Font = new Font("Segoe UI", 9F),
                ForeColor = aduan.IsSelesai ? Color.Green : Color.Orange,
                Size = new Size(150, 20),
                Location = new Point(15, 40)
            };

            card.Controls.Add(lblSubjek);
            card.Controls.Add(lblStatus);

            if (!string.IsNullOrEmpty(aduan.Balasan))
            {
                Label lblBalasan = new Label()
                {
                    Text = $"💬 Balasan admin: {aduan.Balasan}",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.DarkGreen,
                    Size = new Size(500, 30),
                    Location = new Point(15, 65)
                };
                card.Controls.Add(lblBalasan);
            }

            return card;
        }
    }
}
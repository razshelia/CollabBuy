using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ComplaintListControl : UserControl
    {
        private ComplaintService _complaintService;

        public ComplaintListControl()
        {
            InitializeComponent();
            _complaintService = new ComplaintService();
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
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada aduan, bestie! 🎉\nSemoga tetap aman terkendali~";
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
            card.Size = new Size(680, 140);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            // Subjek
            Label lblSubjek = new Label();
            lblSubjek.Text = $"📢 {aduan.Subjek}";
            lblSubjek.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubjek.ForeColor = Color.FromArgb(45, 27, 79);
            lblSubjek.Size = new Size(400, 25);
            lblSubjek.Location = new Point(15, 10);

            // Deskripsi
            Label lblDeskripsi = new Label();
            lblDeskripsi.Text = aduan.Deskripsi;
            lblDeskripsi.Font = new Font("Segoe UI", 9F);
            lblDeskripsi.ForeColor = Color.Gray;
            lblDeskripsi.Size = new Size(500, 40);
            lblDeskripsi.Location = new Point(15, 40);

            // Status
            Label lblStatus = new Label();
            lblStatus.Text = aduan.IsSelesai ? "✅ Selesai" : "⏳ Menunggu";
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = aduan.IsSelesai ? Color.Green : Color.Orange;
            lblStatus.Size = new Size(150, 20);
            lblStatus.Location = new Point(15, 85);

            card.Controls.Add(lblSubjek);
            card.Controls.Add(lblDeskripsi);
            card.Controls.Add(lblStatus);

            // Jika sudah ada balasan, tampilkan
            if (!string.IsNullOrEmpty(aduan.Balasan))
            {
                Label lblBalasan = new Label();
                lblBalasan.Text = $"💬 Balasan: {aduan.Balasan}";
                lblBalasan.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                lblBalasan.ForeColor = Color.DarkGreen;
                lblBalasan.Size = new Size(350, 20);
                lblBalasan.Location = new Point(15, 110);
                card.Controls.Add(lblBalasan);
            }

            // Tombol untuk admin: Selesai & Balas
            if (!aduan.IsSelesai)
            {
                Button btnSelesai = new Button();
                btnSelesai.Text = "✅ Selesai";
                btnSelesai.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                btnSelesai.BackColor = Color.Green;
                btnSelesai.ForeColor = Color.White;
                btnSelesai.FlatStyle = FlatStyle.Flat;
                btnSelesai.FlatAppearance.BorderSize = 0;
                btnSelesai.Size = new Size(80, 28);
                btnSelesai.Location = new Point(400, 80);
                btnSelesai.Click += (s, e) =>
                {
                    if (_complaintService.TandaiSelesai(aduan.IdAduan))
                        LoadData();
                };
                card.Controls.Add(btnSelesai);

                Button btnBalas = new Button();
                btnBalas.Text = "💬 Balas";
                btnBalas.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                btnBalas.BackColor = Color.FromArgb(167, 139, 250);
                btnBalas.ForeColor = Color.White;
                btnBalas.FlatStyle = FlatStyle.Flat;
                btnBalas.Size = new Size(80, 28);
                btnBalas.Location = new Point(490, 80);
                btnBalas.Click += (s, e) =>
                {
                    string balasan = InputDialog.Show(
                        "Tulis balasan untuk aduan ini:", "Balas Aduan", "");
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
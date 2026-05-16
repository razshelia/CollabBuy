using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminUserManagementControl : UserControl
    {
        private readonly VerificationService _verifService;
        private List<Verification> _daftarPengajuan;

        public AdminUserManagementControl()
        {
            InitializeComponent();

            // TAHAP 4: INJEKSI MANUAL DI UI
            _verifService = new VerificationService(new VerificationRepository());

            LoadData();
        }

        private void LoadData()
        {
            _daftarPengajuan = _verifService.AmbilPengajuanPending();
            TampilkanData();
        }

        private void TampilkanData()
        {
            flowPanelVerif.Controls.Clear();
            if (_daftarPengajuan.Count == 0)
            {
                Label lblKosong = new Label()
                {
                    Text = "Belum ada pengajuan verifikasi nih, bestie! 😴",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
                flowPanelVerif.Controls.Add(lblKosong);
                return;
            }

            foreach (var verif in _daftarPengajuan)
            {
                Panel card = BuatCardVerifikasi(verif);
                flowPanelVerif.Controls.Add(card);
            }
        }

        private Panel BuatCardVerifikasi(Verification v)
        {
            // Desain Card Verifikasi Neo-Retro
            Panel card = new Panel()
            {
                Size = new Size(850, 110), // Diperlebar agar cocok untuk full screen
                BackColor = Color.FromArgb(253, 255, 182), // Pastel Yellow
                Margin = new Padding(10),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Gaya kotak datar
            };

            Label lblNamaToko = new Label()
            {
                Text = $"🏪 {v.NamaToko.ToUpper()}",
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple
                Size = new Size(400, 25),
                Location = new Point(15, 15)
            };

            Label lblNIM = new Label()
            {
                Text = $"🎓 NIM: {v.Nim}   •   📅 Tahun Masuk: {v.TahunMasuk}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Size = new Size(400, 20),
                Location = new Point(15, 45)
            };

            Label lblStatus = new Label()
            {
                Text = v.IsVerifikasi ? "✅ DISETUJUI" : "⏳ MENUNGGU",
                Font = new Font("Segoe UI Black", 10F),
                ForeColor = v.IsVerifikasi ? Color.FromArgb(0, 150, 0) : Color.FromArgb(220, 120, 0),
                Size = new Size(150, 20),
                Location = new Point(15, 70)
            };

            // Tombol Setujui - Pastel Green
            Button btnSetujui = new Button()
            {
                Text = "✅ Setujui",
                BackColor = Color.FromArgb(182, 255, 200), // Soft Green
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 40),
                Location = new Point(600, 35),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnSetujui.FlatAppearance.BorderSize = 1;
            btnSetujui.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnSetujui.Click += (s, e) =>
            {
                if (_verifService.SetujuiVerifikasi(v.IdVerifikasi))
                    LoadData();
            };

            // Tombol Tolak - Pastel Red
            Button btnTolak = new Button()
            {
                Text = "❌ Tolak",
                BackColor = Color.FromArgb(255, 138, 138), // Soft Red
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 40),
                Location = new Point(720, 35),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnTolak.FlatAppearance.BorderSize = 1;
            btnTolak.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnTolak.Click += (s, e) =>
            {
                if (_verifService.TolakVerifikasi(v.IdVerifikasi))
                    LoadData();
            };

            card.Controls.Add(lblNamaToko);
            card.Controls.Add(lblNIM);
            card.Controls.Add(lblStatus);
            card.Controls.Add(btnSetujui);
            card.Controls.Add(btnTolak);
            return card;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
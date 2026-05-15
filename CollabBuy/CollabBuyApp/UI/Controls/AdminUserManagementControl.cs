using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminUserManagementControl : UserControl
    {
        private VerificationService _verifService;
        private List<Verification> _daftarPengajuan;

        public AdminUserManagementControl()
        {
            InitializeComponent();
            _verifService = new VerificationService();
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
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada pengajuan verifikasi, bestie! 😴";
                lblKosong.Font = new Font("Segoe UI", 14F);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
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
            Panel card = new Panel();
            card.Size = new Size(680, 110);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            Label lblNamaToko = new Label()
            {
                Text = $"🏪 {v.NamaToko}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 27, 79),
                Size = new Size(300, 25),
                Location = new Point(15, 10)
            };

            Label lblNIM = new Label()
            {
                Text = $"🎓 NIM: {v.Nim} • Tahun Masuk: {v.TahunMasuk}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Size = new Size(300, 20),
                Location = new Point(15, 40)
            };

            Label lblStatus = new Label()
            {
                Text = v.IsVerifikasi ? "✅ Disetujui" : "⏳ Menunggu",
                Font = new Font("Segoe UI", 9F),
                ForeColor = v.IsVerifikasi ? Color.Green : Color.Orange,
                Size = new Size(150, 20),
                Location = new Point(15, 65)
            };

            Button btnSetujui = new Button()
            {
                Text = "✅ Setujui",
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 30),
                Location = new Point(400, 55)
            };
            btnSetujui.Click += (s, e) =>
            {
                if (_verifService.SetujuiVerifikasi(v.IdVerifikasi))
                    LoadData();
            };

            Button btnTolak = new Button()
            {
                Text = "❌ Tolak",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 30),
                Location = new Point(510, 55)
            };
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerPOListControl : UserControl
    {
        private int _idPenjual;
        private PreorderService _poService;

        public SellerPOListControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _poService = new PreorderService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var daftar = _poService.AmbilSemuaPOByPenjual(_idPenjual);
                TampilkanPO(daftar);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat PO: " + ex.Message);
            }
        }

        private void TampilkanPO(List<Preorder> daftar)
        {
            flowPanelPO.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "Kamu belum punya PO, bestie! 🥺\nYuk buka PO pertama kamu~",
                    Font = new Font("Segoe UI", 14F),
                    ForeColor = Color.FromArgb(45, 27, 79),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                flowPanelPO.Controls.Add(lblKosong);
                return;
            }

            foreach (var po in daftar)
                flowPanelPO.Controls.Add(BuatCardPO(po));
        }

        private Panel BuatCardPO(Preorder po)
        {
            Panel card = new Panel
            {
                Size = new Size(680, 130),
                BackColor = Color.White,
                Margin = new Padding(5),
                Padding = new Padding(15)
            };

            Label lblJudul = new Label
            {
                Text = $"📦 {po.JudulPo}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 27, 79),
                Size = new Size(400, 25),
                Location = new Point(15, 10)
            };

            Label lblJenis = new Label
            {
                Text = $"Jenis: {po.JenisPo}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(167, 139, 250),
                Size = new Size(200, 20),
                Location = new Point(15, 40)
            };

            Label lblBatas = new Label
            {
                Text = $"⏰ Batas: {po.BatasWaktu:dd MMM yyyy HH:mm}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Size = new Size(250, 20),
                Location = new Point(15, 65)
            };

            Label lblStatus = new Label
            {
                Text = po.IsAktif ? "🟢 Aktif" : "🔴 Tutup",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = po.IsAktif ? Color.Green : Color.Red,
                Size = new Size(100, 20),
                Location = new Point(15, 90)
            };

            // ── Tombol Kelola Produk → SellerProductListControl ──
            Button btnProduk = new Button
            {
                Text = "📦 Produk",
                BackColor = Color.FromArgb(167, 139, 250),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 30),
                Location = new Point(300, 85)
            };
            btnProduk.FlatAppearance.BorderSize = 0;
            btnProduk.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                {
                    // Hapus po.IdPo dan po.JudulPo karena constructor-nya sekarang cuma butuh ID Penjual
                    main.GantiHalaman(new SellerProductListControl(_idPenjual));
                }
            };

            // ── Tombol Tutup PO ───────────────────────────────
            Button btnTutup = new Button
            {
                Text = "🔒 Tutup",
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 30),
                Location = new Point(420, 85),
                Visible = po.IsAktif
            };
            btnTutup.FlatAppearance.BorderSize = 0;
            btnTutup.Click += (s, e) =>
            {
                if (UXHelper.TampilkanKonfirmasi($"Tutup PO \"{po.JudulPo}\"?"))
                    if (_poService.TutupPO(po.IdPo, _idPenjual))
                        LoadData();
            };

            card.Controls.AddRange(new Control[] {
                lblJudul, lblJenis, lblBatas, lblStatus, btnProduk
            });
            if (po.IsAktif) card.Controls.Add(btnTutup);
            return card;
        }

        private void btnBuatPO_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
                main.GantiHalaman(new PreorderControl(_idPenjual));
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
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
    public partial class SellerPOListControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly PreorderService _poService;

        public SellerPOListControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;

            // TAHAP 4: INJEKSI MANUAL DI UI
            // Kita menyuntikkan PreorderRepository ke dalam PreorderService
            _poService = new PreorderService(new PreorderRepository());

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
                    Text = "Kamu belum punya sesi PO nih, bestie! 🥺\nYuk buka sesi PO pertama kamu sekarang!",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
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
            // Desain Card Gen-Z: Kotak tegas, warna pastel cerah, border solid hitam/gelap
            Panel card = new Panel
            {
                Size = new Size(850, 130), // Diperlebar agar nyaman di mode fullscreen
                BackColor = Color.FromArgb(253, 255, 182), // Kuning Pastel Cerah
                Margin = new Padding(10),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Kesan Neo-Retro Flat Box
            };

            Label lblJudul = new Label
            {
                Text = $"📦 {po.JudulPo.ToUpper()}",
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple
                Size = new Size(450, 25),
                Location = new Point(15, 15)
            };

            Label lblJenis = new Label
            {
                Text = $"🏷️ Jenis Sesi: {po.JenisPo}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(167, 139, 250), // Purple Accent
                Size = new Size(250, 20),
                Location = new Point(15, 45)
            };

            Label lblBatas = new Label
            {
                Text = $"⏰ Batas Waktu: {po.BatasWaktu:dd MMM yyyy HH:mm}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Size = new Size(300, 20),
                Location = new Point(15, 70)
            };

            // Status dengan indikator warna tegas
            Color warnaStatus = po.IsAktif ? Color.FromArgb(0, 150, 0) : Color.FromArgb(225, 40, 40);
            Label lblStatus = new Label
            {
                Text = po.IsAktif ? "● LAPAK AKTIF" : "● LAPAK DITUTUP",
                Font = new Font("Segoe UI Black", 9.5F),
                ForeColor = warnaStatus,
                Size = new Size(150, 20),
                Location = new Point(15, 95)
            };

            // Susunan Tombol Aksi di Kanan Card
            Button btnProduk = new Button
            {
                Text = "📦 Kelola Produk",
                BackColor = Color.FromArgb(200, 182, 255), // Ungu Pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(130, 36),
                Location = new Point(550, 45),
                Cursor = Cursors.Hand
            };
            btnProduk.FlatAppearance.BorderSize = 1;
            btnProduk.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnProduk.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                {
                    main.GantiHalaman(new SellerProductListControl(_idPenjual));
                }
            };

            Button btnTutup = new Button
            {
                Text = "🔒 Tutup Lapak",
                BackColor = Color.FromArgb(255, 138, 138), // Soft Red Pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(130, 36),
                Location = new Point(695, 45),
                Cursor = Cursors.Hand,
                Visible = po.IsAktif
            };
            btnTutup.FlatAppearance.BorderSize = 1;
            btnTutup.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnTutup.Click += (s, e) =>
            {
                if (UXHelper.TampilkanKonfirmasi($"Yakin ingin menutup sesi PO \"{po.JudulPo}\"?"))
                {
                    if (_poService.TutupPO(po.IdPo, _idPenjual))
                        LoadData();
                }
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
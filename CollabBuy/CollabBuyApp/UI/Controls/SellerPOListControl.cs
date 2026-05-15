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
                Label lblKosong = new Label();
                lblKosong.Text = "Kamu belum punya PO, bestie! 🥺\nYuk buka PO pertama kamu~";
                lblKosong.Font = new Font("Segoe UI", 14F);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
                flowPanelPO.Controls.Add(lblKosong);
                return;
            }

            foreach (var po in daftar)
            {
                Panel card = BuatCardPO(po);
                flowPanelPO.Controls.Add(card);
            }
        }

        private Panel BuatCardPO(Preorder po)
        {
            Panel card = new Panel();
            card.Size = new Size(680, 130);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            // Judul PO
            Label lblJudul = new Label();
            lblJudul.Text = $"📦 {po.JudulPo}";
            lblJudul.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(45, 27, 79);
            lblJudul.Size = new Size(400, 25);
            lblJudul.Location = new Point(15, 10);

            // Jenis
            Label lblJenis = new Label();
            lblJenis.Text = $"Jenis: {po.JenisPo}";
            lblJenis.Font = new Font("Segoe UI", 9F);
            lblJenis.ForeColor = Color.FromArgb(167, 139, 250);
            lblJenis.Size = new Size(200, 20);
            lblJenis.Location = new Point(15, 40);

            // Batas waktu
            Label lblBatas = new Label();
            lblBatas.Text = $"⏰ Batas: {po.BatasWaktu:dd MMM yyyy HH:mm}";
            lblBatas.Font = new Font("Segoe UI", 9F);
            lblBatas.ForeColor = Color.Gray;
            lblBatas.Size = new Size(250, 20);
            lblBatas.Location = new Point(15, 65);

            // Status
            Label lblStatus = new Label();
            lblStatus.Text = po.IsAktif ? "🟢 Aktif" : "🔴 Tutup";
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = po.IsAktif ? Color.Green : Color.Red;
            lblStatus.Size = new Size(100, 20);
            lblStatus.Location = new Point(15, 90);

            // Tombol Kelola Produk
            Button btnProduk = new Button();
            btnProduk.Text = "📦 Produk";
            btnProduk.BackColor = Color.FromArgb(167, 139, 250);
            btnProduk.ForeColor = Color.White;
            btnProduk.FlatStyle = FlatStyle.Flat;
            btnProduk.FlatAppearance.BorderSize = 0;
            btnProduk.Size = new Size(100, 30);
            btnProduk.Location = new Point(300, 85);
            btnProduk.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                    main.GantiHalaman(new SellerProductListControl(_idPenjual, po.IdPo));
            };

            // Tombol Tutup PO (jika masih aktif)
            Button btnTutup = new Button();
            btnTutup.Text = "🔒 Tutup";
            btnTutup.BackColor = Color.Orange;
            btnTutup.ForeColor = Color.White;
            btnTutup.FlatStyle = FlatStyle.Flat;
            btnTutup.FlatAppearance.BorderSize = 0;
            btnTutup.Size = new Size(100, 30);
            btnTutup.Location = new Point(410, 85);
            btnTutup.Visible = po.IsAktif;
            btnTutup.Click += (s, e) =>
            {
                if (_poService.TutupPO(po.IdPo, _idPenjual))
                    LoadData();
            };

            card.Controls.Add(lblJudul);
            card.Controls.Add(lblJenis);
            card.Controls.Add(lblBatas);
            card.Controls.Add(lblStatus);
            card.Controls.Add(btnProduk);
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
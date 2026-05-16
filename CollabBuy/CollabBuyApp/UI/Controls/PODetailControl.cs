using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class PODetailControl : UserControl
    {
        private readonly int _idPo;
        private readonly User _user;
        private readonly PreorderService _poService;
        private readonly ProductService _productService;
        private Preorder _po;

        public PODetailControl(User user, int idPo)
        {
            InitializeComponent();
            _user = user;
            _idPo = idPo;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _poService = new PreorderService(new PreorderRepository());
            _productService = new ProductService(new ProductRepository());

            LoadPODetail();
        }

        private void LoadPODetail()
        {
            _po = _poService.AmbilPOById(_idPo);
            if (_po == null)
            {
                UXHelper.TampilkanError("PO tidak ditemukan.");
                KembaliKeKatalog();
                return;
            }

            // Tampilkan info PO di header
            lblJudulPO.Text = _po.JudulPo.ToUpper();
            lblJenisPO.Text = $"🏷️ Jenis: {_po.JenisPo}";
            lblRekening.Text = $"💳 Rekening: {_po.InfoRekening}";
            lblBatasWaktu.Text = $"⏳ Batas Waktu: {_po.BatasWaktu:dd MMMM yyyy HH:mm}";

            // Tampilkan produk dalam PO
            List<Product> listProdukDiPO = _productService.AmbilProdukByPo(_idPo);

            if (listProdukDiPO != null)
            {
                TampilkanProdukCard(listProdukDiPO);
            }
        }

        private void TampilkanProdukCard(List<Product> produkList)
        {
            flowPanelProduk.Controls.Clear();

            if (produkList.Count == 0)
            {
                Label lblKosong = new Label()
                {
                    Text = "Belum ada produk di PO ini, bestie! 😴",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark purple
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
                flowPanelProduk.Controls.Add(lblKosong);
                return;
            }

            foreach (var produk in produkList)
            {
                Panel card = BuatCardProduk(produk);
                flowPanelProduk.Controls.Add(card);
            }
        }

        private Panel BuatCardProduk(Product produk)
        {
            // Desain Card Produk Neo-Retro
            Panel card = new Panel()
            {
                Size = new Size(280, 360),
                BackColor = Color.White,
                Margin = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Garis pinggir retro
            };

            PictureBox pic = new PictureBox()
            {
                Size = new Size(256, 150),
                Location = new Point(10, 10),
                BackColor = Color.FromArgb(200, 182, 255), // Pastel purple placeholder
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            if (!string.IsNullOrEmpty(produk.FotoProduk))
            {
                string fullPath = FileHelper.DapatkanFullPath(produk.FotoProduk);
                if (File.Exists(fullPath))
                    pic.Image = Image.FromFile(fullPath);
            }

            Label lblNama = new Label()
            {
                Text = produk.NamaProduk.ToUpper(),
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.FromArgb(36, 0, 70),
                Size = new Size(256, 45),
                Location = new Point(10, 170)
            };

            Label lblHarga = new Label()
            {
                Text = $"Rp {produk.HargaDasar:N0}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 138, 138), // Soft Red
                Size = new Size(256, 25),
                Location = new Point(10, 220)
            };
            // Jika ada diskon, tampilkan dengan jelas
            if (produk.HargaDiskon.HasValue && produk.HargaDiskon > 0)
            {
                lblHarga.Text = $"Rp {produk.HargaDiskon:N0}";
                lblHarga.ForeColor = Color.FromArgb(0, 150, 0); // Green if discounted
            }

            Label lblMinOrder = new Label()
            {
                Text = $"Min order: {produk.MinOrder} pcs",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Size = new Size(256, 20),
                Location = new Point(10, 250)
            };

            // Tombol Titip Sekarang
            Button btnTitip = new Button()
            {
                Text = "TITIP SEKARANG ✨",
                BackColor = Color.FromArgb(253, 255, 182), // Kuning pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(256, 45),
                Location = new Point(10, 290),
                Font = new Font("Segoe UI Black", 10F),
                Cursor = Cursors.Hand
            };
            btnTitip.FlatAppearance.BorderSize = 2;
            btnTitip.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnTitip.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                    main.GantiHalaman(new CheckoutControl(_user.IdUser, produk.IdProduk));
            };

            card.Controls.Add(pic);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblHarga);
            card.Controls.Add(lblMinOrder);
            card.Controls.Add(btnTitip);
            return card;
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            KembaliKeKatalog();
        }

        private void KembaliKeKatalog()
        {
            if (ParentForm is MainForm main)
                main.GantiHalaman(new UserDashboardControl(_user));
        }
    }
}
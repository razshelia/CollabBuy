using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerProductListControl : UserControl
    {
        private int _idPenjual;
        private int _idPo;
        private string _judulPo;
        private ProductService _productService;
        private List<Product> _daftarProduk;

        public SellerProductListControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _productService = new ProductService();

            LoadProduk();
        }

        private void LoadProduk()
        {
            _daftarProduk = _productService.AmbilProdukByPo(_idPo);
            TampilkanProduk();
        }

        private void TampilkanProduk()
        {
            flowPanelProduk.Controls.Clear();

            if (_daftarProduk.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "Belum ada produk di PO ini 🥺\nYuk tambah produk dulu~",
                    Font = new Font("Segoe UI", 13F),
                    ForeColor = Color.FromArgb(45, 27, 79),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                flowPanelProduk.Controls.Add(lblKosong);
                return;
            }

            foreach (var produk in _daftarProduk)
                flowPanelProduk.Controls.Add(BuatCardProduk(produk));
        }

        private Panel BuatCardProduk(Product produk)
        {
            Panel card = new Panel
            {
                Size = new Size(220, 220),
                BackColor = Color.White,
                Margin = new Padding(8),
                Padding = new Padding(8)
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(204, 100),
                Location = new Point(8, 8),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(167, 139, 250)
            };
            if (!string.IsNullOrEmpty(produk.FotoProduk))
            {
                string full = FileHelper.DapatkanFullPath(produk.FotoProduk);
                if (File.Exists(full)) pic.Image = Image.FromFile(full);
            }

            Label lblNama = new Label
            {
                Text = produk.NamaProduk,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 27, 79),
                Size = new Size(204, 20),
                Location = new Point(8, 115)
            };

            Label lblHarga = new Label
            {
                Text = $"Rp {produk.HargaDasar:N0}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(100, 60, 180),
                Size = new Size(204, 18),
                Location = new Point(8, 137)
            };

            Button btnEdit = new Button
            {
                Text = "✏️ Edit",
                BackColor = Color.FromArgb(167, 139, 250),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F),
                Size = new Size(90, 28),
                Location = new Point(8, 162)
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) => NavigasiKeFormProduk(produk);

            Button btnHapus = new Button
            {
                Text = "🗑 Hapus",
                BackColor = Color.FromArgb(220, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F),
                Size = new Size(90, 28),
                Location = new Point(106, 162)
            };
            btnHapus.FlatAppearance.BorderSize = 0;
            btnHapus.Click += (s, e) =>
            {
                if (UXHelper.TampilkanKonfirmasi($"Hapus produk \"{produk.NamaProduk}\"?"))
                {
                    if (_productService.HapusProduk(produk.IdProduk))
                        LoadProduk();
                }
            };

            card.Controls.AddRange(new Control[] { pic, lblNama, lblHarga, btnEdit, btnHapus });
            return card;
        }

        // ── Event Handlers ────────────────────────────────────

        private void btnTambahProduk_Click(object sender, EventArgs e)
        {
            NavigasiKeFormProduk(null);
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
                main.GantiHalaman(new SellerPOListControl(_idPenjual));
        }

        private void NavigasiKeFormProduk(Product produkEdit = null)
        {
            if (ParentForm is MainForm main)
                main.GantiHalaman(new ProductFormControl(_idPenjual, produkEdit));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerProductListControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly ProductService _productService;
        private List<Product> _daftarProduk;

        public SellerProductListControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;

            // TAHAP 4: INJEKSI MANUAL DI UI
            // Menyuntikkan ProductRepository ke dalam ProductService
            _productService = new ProductService(new ProductRepository());

            LoadProduk();
        }

        private void LoadProduk()
        {
            _daftarProduk = _productService.AmbilProdukByPenjual(_idPenjual);
            TampilkanProduk();
        }

        private void TampilkanProduk()
        {
            flowPanelProduk.Controls.Clear();

            if (_daftarProduk == null || _daftarProduk.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "Belum ada produk master nih, bestie! 🥺\nYuk tambah produk jualanmu dulu~",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
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
            // Desain Card Produk bergaya Flat Neo-Retro
            Panel card = new Panel
            {
                Size = new Size(250, 310),
                BackColor = Color.White,
                Margin = new Padding(12),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle // Bingkai kotak datar retro
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(228, 120),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(200, 182, 255), // Ungu Pastel sebagai placeholder
                BorderStyle = BorderStyle.FixedSingle
            };
            if (!string.IsNullOrEmpty(produk.FotoProduk))
            {
                string full = FileHelper.DapatkanFullPath(produk.FotoProduk);
                if (File.Exists(full)) pic.Image = Image.FromFile(full);
            }

            Label lblNama = new Label
            {
                Text = produk.NamaProduk.ToUpper(), // Kapital untuk efek bold pop retro
                Font = new Font("Segoe UI Black", 11F),
                ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple
                Size = new Size(228, 45),
                Location = new Point(10, 140)
            };

            Label lblHarga = new Label
            {
                Text = $"Rp {produk.HargaDasar:N0}",
                Font = new Font("Segoe UI Black", 11F),
                ForeColor = Color.FromArgb(255, 138, 138), // Soft Red Pastel
                Size = new Size(228, 22),
                Location = new Point(10, 190)
            };
            if (produk.HargaDiskon.HasValue && produk.HargaDiskon > 0)
            {
                lblHarga.Text = $"Rp {produk.HargaDiskon:N0}";
                lblHarga.ForeColor = Color.FromArgb(0, 150, 0);
            }

            // Tombol Edit - Kuning Pastel
            Button btnEdit = new Button
            {
                Text = "✏️ Edit",
                BackColor = Color.FromArgb(253, 255, 182), // Kuning Pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(105, 36),
                Location = new Point(10, 255),
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 1;
            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnEdit.Click += (s, e) => NavigasiKeFormProduk(produk);

            // Tombol Hapus - soft red pastel
            Button btnHapus = new Button
            {
                Text = "🗑 Hapus",
                BackColor = Color.FromArgb(255, 138, 138), // Soft Red Pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(105, 36),
                Location = new Point(133, 255),
                Cursor = Cursors.Hand
            };
            btnHapus.FlatAppearance.BorderSize = 1;
            btnHapus.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
            btnHapus.Click += (s, e) =>
            {
                if (UXHelper.TampilkanKonfirmasi($"Hapus produk \"{produk.NamaProduk}\" dari katalog master?"))
                {
                    if (_productService.HapusProduk(produk.IdProduk))
                        LoadProduk();
                }
            };

            card.Controls.AddRange(new Control[] { pic, lblNama, lblHarga, btnEdit, btnHapus });
            return card;
        }

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
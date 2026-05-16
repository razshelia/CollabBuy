using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class UserDashboardControl : UserControl
    {
        private readonly User _user;
        private readonly CatalogService _catalogService;
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private List<Catalog> _semuaProduk;

        public UserDashboardControl(User user)
        {
            InitializeComponent();
            _user = user;

            // TAHAP 4: INJEKSI MANUAL DI UI
            // Menyuntikkan masing-masing repositori ke dalam service terkait
            _catalogService = new CatalogService(new CatalogRepository());
            _categoryService = new CategoryService(new CategoryRepository());
            _productService = new ProductService(new ProductRepository());

            // Set sapaan nama secara dinamis dan kapital agar bergaya bold pop retro
            lblGreeting.Text = $"HALO, {(_user?.Nama ?? "Bestie").ToUpper()}! 👋";

            MuatKatalog();
            MuatKategoriFilter();
        }

        private void MuatKatalog()
        {
            try
            {
                _semuaProduk = _catalogService.AmbilKatalogAktif();
                FilterDanTampilkan();
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat katalog: " + ex.Message);
            }
        }

        private void MuatKategoriFilter()
        {
            var listKategori = _categoryService.AmbilSemua() ?? new List<Category>();
            listKategori.Insert(0, new Category { IdKategori = 0, NamaKategori = "Semua Kategori" });

            cmbKategori.DataSource = listKategori;
            cmbKategori.DisplayMember = "NamaKategori";
            cmbKategori.ValueMember = "IdKategori";
            cmbKategori.SelectedIndex = 0;
        }

        private void FilterDanTampilkan()
        {
            if (_semuaProduk == null) return;

            string keyword = txtSearch.Text.Trim().ToLower();

            int selectedKategoriId = 0;
            if (cmbKategori.SelectedValue != null && cmbKategori.SelectedValue is int)
                selectedKategoriId = (int)cmbKategori.SelectedValue;

            IEnumerable<Catalog> hasil = _semuaProduk;

            if (!string.IsNullOrEmpty(keyword))
                hasil = hasil.Where(p => p.NamaProduk.ToLower().Contains(keyword) ||
                                         p.JudulPo.ToLower().Contains(keyword));

            if (selectedKategoriId > 0)
            {
                var idProdukTerpilih = new List<int>();
                foreach (var p in _semuaProduk)
                {
                    var produk = _productService.AmbilProdukById(p.IdProduk);
                    if (produk != null && produk.IdKategori == selectedKategoriId)
                        idProdukTerpilih.Add(p.IdProduk);
                }
                hasil = hasil.Where(p => idProdukTerpilih.Contains(p.IdProduk));
            }

            var listHasil = hasil.ToList();
            TampilkanCardProduk(listHasil);
            lblCount.Text = $"Menampilkan {listHasil.Count} produk spesial";
        }

        private void TampilkanCardProduk(List<Catalog> daftar)
        {
            flowPanelProduk.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label()
                {
                    Text = "Belum ada produk nih, bestie! 😴\nCoba cek katolog atau filter kategori lain ya~",
                    Font = new Font("Segoe UI Black", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
                flowPanelProduk.Controls.Add(lblKosong);
                return;
            }

            foreach (var item in daftar)
            {
                Panel card = BuatCard(item);
                flowPanelProduk.Controls.Add(card);
            }
        }

        private Panel BuatCard(Catalog produk)
        {
            // Desain Card Katalog Luar Bergaya Flat Neo-Retro Box
            Panel card = new Panel
            {
                Size = new Size(280, 420),
                BackColor = Color.White,
                Margin = new Padding(15),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle // Garis border tegas retro
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(258, 140),
                Location = new Point(10, 10),
                BackColor = Color.FromArgb(200, 182, 255), // Ungu Pastel Placeholder
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            try
            {
                var productDetail = _productService.AmbilProdukById(produk.IdProduk);
                if (productDetail != null && !string.IsNullOrEmpty(productDetail.FotoProduk))
                {
                    string fullPath = FileHelper.DapatkanFullPath(productDetail.FotoProduk);
                    if (File.Exists(fullPath))
                        pic.Image = Image.FromFile(fullPath);
                }
            }
            catch { }

            Label lblJudulPO = new Label
            {
                Text = $"📦 {produk.JudulPo.ToUpper()}",
                Font = new Font("Segoe UI Black", 10.5F),
                ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple
                Size = new Size(258, 40),
                Location = new Point(10, 165),
                Cursor = Cursors.Hand
            };
            lblJudulPO.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                {
                    var user = main.AmbilUserAktif();
                    if (user != null)
                    {
                        var produkDetail = _productService.AmbilProdukById(produk.IdProduk);
                        if (produkDetail != null)
                        {
                            if (produkDetail.IdPo.HasValue)
                            {
                                main.GantiHalaman(new PODetailControl(user, produkDetail.IdPo.Value));
                            }
                            else
                            {
                                UXHelper.TampilkanError("Produk ini belum dimasukkan ke dalam sesi Pre-Order apa pun.");
                            }
                        }
                    }
                }
            };

            Label lblNama = new Label
            {
                Text = produk.NamaProduk,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(120, 120, 120),
                Size = new Size(258, 25),
                Location = new Point(10, 210)
            };

            Label lblHarga = new Label
            {
                Text = $"Rp {produk.HargaDasar:N0}",
                Font = new Font("Segoe UI Black", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 138, 138), // Soft Red Pastel Price
                Size = new Size(258, 35),
                Location = new Point(10, 240)
            };

            Label lblBatas = new Label
            {
                Text = $"⏳ Batas Waktu: {produk.BatasWaktu:dd MMM yyyy HH:mm}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Size = new Size(258, 20),
                Location = new Point(10, 285)
            };

            // Tombol Titip Sekarang - Kuning Pastel Pop Out
            Button btnTitip = new Button
            {
                Text = "TITIP SEKARANG ✨",
                BackColor = Color.FromArgb(253, 255, 182), // Kuning Pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(258, 45),
                Location = new Point(10, 315),
                Font = new Font("Segoe UI Black", 11F),
                Cursor = Cursors.Hand
            };
            btnTitip.FlatAppearance.BorderSize = 2;
            btnTitip.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);

            btnTitip.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                {
                    var user = main.AmbilUserAktif();
                    if (user != null)
                        main.GantiHalaman(new CheckoutControl(user.IdUser, produk.IdProduk));
                    else
                        UXHelper.TampilkanError("Login dulu ya bestie! 🔐");
                }
            };

            card.Controls.Add(pic);
            card.Controls.Add(lblJudulPO);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblHarga);
            card.Controls.Add(lblBatas);
            card.Controls.Add(btnTitip);
            return card;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => FilterDanTampilkan();
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e) => FilterDanTampilkan();
    }
}
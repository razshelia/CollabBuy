// UserDashboardControl.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class UserDashboardControl : UserControl
    {
        private User _user;
        private CatalogService _catalogService;
        private CategoryService _categoryService;
        private ProductService _productService;
        private List<Catalog> _semuaProduk;

        public UserDashboardControl(User user)
        {
            InitializeComponent();
            _user = user;
            _catalogService = new CatalogService();
            _categoryService = new CategoryService();
            _productService = new ProductService();
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
            var listKategori = _categoryService.AmbilSemua();
            listKategori.Insert(0, new Category { IdKategori = 0, NamaKategori = "Semua Kategori" });

            cmbKategori.DataSource = listKategori;
            cmbKategori.DisplayMember = "NamaKategori";
            cmbKategori.ValueMember = "IdKategori";
            cmbKategori.SelectedIndex = 0;
        }

        private void FilterDanTampilkan()
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            // Cek apakah SelectedValue null atau bukan int
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
            lblCount.Text = $"Menampilkan {listHasil.Count} produk";
        }

        private void TampilkanCardProduk(List<Catalog> daftar)
        {
            flowPanelProduk.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada produk nih, bestie! 😴\nCoba cek lagi nanti~";
                lblKosong.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.Size = new Size(600, 80);
                lblKosong.Location = new Point(150, 200);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
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
            Panel card = new Panel();
            card.Size = new Size(280, 400);
            card.BackColor = Color.White;
            card.Margin = new Padding(10);

            Panel content = new Panel();
            content.Size = new Size(280, 400);
            content.BackColor = Color.White;

            PictureBox pic = new PictureBox();
            pic.Size = new Size(256, 140);
            pic.Location = new Point(12, 12);
            pic.BackColor = Color.FromArgb(167, 139, 250);
            pic.SizeMode = PictureBoxSizeMode.Zoom;

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

            Label lblJudulPO = new Label();
            lblJudulPO.Text = $"📦 {produk.JudulPo}";
            lblJudulPO.Font = new Font("Segoe UI Black", 10F, FontStyle.Bold);
            lblJudulPO.ForeColor = Color.FromArgb(45, 27, 79);
            lblJudulPO.Size = new Size(256, 35);
            lblJudulPO.Location = new Point(12, 160);
            lblJudulPO.Cursor = Cursors.Hand;
            lblJudulPO.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                {
                    var user = main.AmbilUserAktif();
                    if (user != null)
                    {
                        // 1. IdProduk langsung saja dipanggil tanpa GetValueOrDefault()
                        var produkDetail = _productService.AmbilProdukById(produk.IdProduk);

                        if (produkDetail != null)
                        {
                            // 2. IdPo tetap dicek .HasValue karena tipenya int? (nullable)
                            if (produkDetail.IdPo.HasValue)
                            {
                                // Ambil nilai aslinya dengan .Value
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

            Label lblNama = new Label();
            lblNama.Text = produk.NamaProduk;
            lblNama.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNama.ForeColor = Color.FromArgb(167, 139, 250);
            lblNama.Size = new Size(256, 25);
            lblNama.Location = new Point(12, 195);

            Label lblHarga = new Label();
            lblHarga.Text = $"Rp {produk.HargaDasar:N0}";
            lblHarga.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
            lblHarga.ForeColor = Color.FromArgb(253, 224, 71);
            lblHarga.Size = new Size(256, 30);
            lblHarga.Location = new Point(12, 225);

            Label lblBatas = new Label();
            lblBatas.Text = $"⏰ {produk.BatasWaktu:dd MMM yyyy HH:mm}";
            lblBatas.Font = new Font("Segoe UI", 8F);
            lblBatas.ForeColor = Color.Gray;
            lblBatas.Size = new Size(256, 20);
            lblBatas.Location = new Point(12, 260);

            Button btnTitip = new Button();
            btnTitip.Text = "Titip Sekarang ✨";
            btnTitip.BackColor = Color.FromArgb(167, 139, 250);
            btnTitip.ForeColor = Color.White;
            btnTitip.FlatStyle = FlatStyle.Flat;
            btnTitip.FlatAppearance.BorderSize = 0;
            btnTitip.Size = new Size(256, 35);
            btnTitip.Location = new Point(12, 295);
            btnTitip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
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

            content.Controls.Add(pic);
            content.Controls.Add(lblJudulPO);
            content.Controls.Add(lblNama);
            content.Controls.Add(lblHarga);
            content.Controls.Add(lblBatas);
            content.Controls.Add(btnTitip);

            card.Controls.Add(content);
            return card;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => FilterDanTampilkan();
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e) => FilterDanTampilkan();
    }
}
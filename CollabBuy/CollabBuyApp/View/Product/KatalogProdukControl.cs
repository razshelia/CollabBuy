using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class KatalogProdukControl : UserControl
    {
        private readonly ProductController _productController;
        private User _currentUser; // Opsional: Untuk mengetahui siapa yang menambahkan ke keranjang

        public KatalogProdukControl(User currentUser)
        {
            InitializeComponent();
            _productController = new ProductController();
            _currentUser = currentUser;
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
            LoadKategori();
            LoadKatalogProduk(""); // Muat semua produk saat awal
        }

        private void LoadKategori()
        {
            // Dummy Kategori (Bisa diganti dengan mengambil dari AdminController.GetAllKategori)
            cbKategori.Items.Add("Semua Kategori");
            cbKategori.Items.Add("Makanan & Minuman");
            cbKategori.Items.Add("Pakaian");
            cbKategori.Items.Add("Jasa");
            cbKategori.SelectedIndex = 0; // Default: Semua Kategori
        }

        private void LoadKatalogProduk(string kataKunci)
        {
            flpKatalog.Controls.Clear(); // Bersihkan katalog sebelum memuat ulang

            try
            {
                // TODO: Ambil List<Product> dari ProductController
                // List<Models.Product> listProduk = _productController.GetAllProducts(kataKunci);

                // --- MOCK DATA ---
                List<dynamic> listProduk = new List<dynamic>
                {
                    new { Id = 1, Nama = "Makaroni Bantet Pedas", Harga = 5000, Toko = "Danus HMTI", Kategori = "Makanan & Minuman" },
                    new { Id = 2, Nama = "Kemeja PDH Custom", Harga = 120000, Toko = "BEM Fasilkom", Kategori = "Pakaian" },
                    new { Id = 3, Nama = "Risol Mayo Lumer", Harga = 3000, Toko = "Siti Jajanan", Kategori = "Makanan & Minuman" },
                    new { Id = 4, Nama = "Jasa Desain Poster", Harga = 35000, Toko = "Budi Studio", Kategori = "Jasa" },
                    new { Id = 5, Nama = "Keripik Kaca Original", Harga = 6000, Toko = "Danus HMTI", Kategori = "Makanan & Minuman" }
                };
                // -----------------

                foreach (var prod in listProduk)
                {
                    // Filter Dummy berdasarkan Pencarian (Abaikan jika menggunakan Controller asli yg sudah mem-filter)
                    if (!string.IsNullOrWhiteSpace(kataKunci) &&
                        !prod.Nama.ToString().ToLower().Contains(kataKunci.ToLower()))
                    {
                        continue;
                    }

                    // Buat Kartu Produk
                    Panel pnlCard = BuatKartuProduk(prod.Id, prod.Nama, prod.Harga, prod.Toko);
                    flpKatalog.Controls.Add(pnlCard);
                }

                if (flpKatalog.Controls.Count == 0)
                {
                    Label lblKosong = new Label
                    {
                        Text = "Produk tidak ditemukan.",
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Margin = new Padding(10)
                    };
                    flpKatalog.Controls.Add(lblKosong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat katalog: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- METHOD PEMBUAT KARTU PRODUK (UI BUILDER) ---
        private Panel BuatKartuProduk(int idProduk, string namaProduk, decimal harga, string namaToko)
        {
            Panel card = new Panel
            {
                Width = 200,
                Height = 260,
                BackColor = Color.White,
                Margin = new Padding(10, 10, 15, 15),
                BorderStyle = BorderStyle.FixedSingle
            };

            // 1. Gambar Placeholder (Atas)
            PictureBox picBox = new PictureBox
            {
                Width = 200,
                Height = 120,
                Top = 0,
                Left = 0,
                BackColor = Color.FromArgb(200, 182, 255), // Ungu Pastel
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            // picBox.Image = Properties.Resources.placeholder; // Opsional: Beri icon box

            // 2. Label Nama Produk
            Label lblNama = new Label
            {
                Text = namaProduk,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70), // Ungu Gelap
                Top = 130,
                Left = 10,
                Width = 180,
                AutoSize = false,
                Height = 45 // Beri ruang jika teks panjang (2 baris)
            };

            // 3. Label Nama Toko
            Label lblToko = new Label
            {
                Text = $"🏪 {namaToko}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.Gray,
                Top = 175,
                Left = 10,
                AutoSize = true
            };

            // 4. Label Harga
            Label lblHarga = new Label
            {
                Text = $"Rp {harga:N0}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Top = 195,
                Left = 10,
                AutoSize = true
            };

            // 5. Tombol Tambah ke Keranjang
            Button btnBeli = new Button
            {
                Text = "🛒 Beli",
                Width = 180,
                Height = 30,
                Top = 220,
                Left = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = Color.FromArgb(253, 255, 182),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Tag = idProduk // Simpan ID Produk di Tag tombol untuk kemudahan referensi
            };
            btnBeli.FlatAppearance.BorderSize = 0;
            btnBeli.Click += BtnBeli_Click; // Daftarkan Event Click

            // Masukkan semua elemen ke dalam card
            card.Controls.Add(picBox);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblToko);
            card.Controls.Add(lblHarga);
            card.Controls.Add(btnBeli);

            return card;
        }

        // --- EVENT HANDLERS ---
        private void BtnBeli_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProduk = Convert.ToInt32(btn.Tag);

            // TODO: Integrasikan ke CartManager / Sesi Keranjang Anda
            // CartManager.Instance.AddToCart(idProduk, 1); // Contoh

            MessageBox.Show($"Produk telah ditambahkan ke keranjang belanja Anda!",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            string keyword = txtCari.Text;
            if (keyword == "Cari produk...") keyword = "";
            LoadKatalogProduk(keyword);
        }

        private void cbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Untuk kesederhanaan, saat ini pencarian di-trigger ulang tanpa filter kategori
            // Jika mau diimplementasikan: LoadKatalogProduk dengan 2 parameter (keyword, kategori)
            btnCari_Click(sender, e);
        }

        // UX Helper: Kosongkan teks "Cari produk..." saat diklik
        private void txtCari_Enter(object sender, EventArgs e)
        {
            if (txtCari.Text == "Cari produk...")
            {
                txtCari.Text = "";
                txtCari.ForeColor = Color.Black;
            }
        }

        private void txtCari_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCari.Text))
            {
                txtCari.Text = "Cari produk...";
                txtCari.ForeColor = Color.Gray;
            }
        }
    }
}

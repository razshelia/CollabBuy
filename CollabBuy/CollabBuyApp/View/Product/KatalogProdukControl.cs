using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class KatalogProdukControl : UserControl
    {
        private readonly User _currentUser;
        private readonly ProductController _productController;
        private readonly CartManager _cartManager;

        public KatalogProdukControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _productController = new ProductController();
            _cartManager = new CartManager(_currentUser.GetIdUser());

            // Supaya tampilan mekar menyesuaikan layar
            this.Dock = DockStyle.Fill;
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
            LoadKatalog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadKatalog();
        }

        private void LoadKatalog()
        {
            flpKatalog.Controls.Clear();
            try
            {
                DataTable dtRaw = _productController.GetKatalogUtama();

                if (dtRaw.Rows.Count == 0)
                {
                    Label lblKosong = new Label
                    {
                        Text = "Yah, lapaknya lagi sepi nih bestie... Belum ada barang yang dijual. 🥲",
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Margin = new Padding(10)
                    };
                    flpKatalog.Controls.Add(lblKosong);
                    return;
                }

                foreach (DataRow row in dtRaw.Rows)
                {
                    Panel card = BuatKartuProduk(row);
                    flpKatalog.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load katalog nih: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel BuatKartuProduk(DataRow row)
        {
            int idProduk = Convert.ToInt32(row["id_produk"]);
            string namaBarang = row["nama_produk"].ToString();
            string judulPo = row.IsNull("judul_po") ? "Ready Stock" : row["judul_po"].ToString();
            string kategori = row.IsNull("nama_kategori") ? "Lainnya" : row["nama_kategori"].ToString();
            int hargaAsli = Convert.ToInt32(row["harga_dasar"]);

            // Bikin Kotak Card-nya (Warna Soft Purple)
            Panel card = new Panel
            {
                Width = 220,
                Height = 340,
                BackColor = Color.FromArgb(235, 204, 255),
                Margin = new Padding(10, 10, 15, 15),
                BorderStyle = BorderStyle.None
            };

            // Tambahkan border melengkung imajiner dengan warna tegas
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.FromArgb(36, 0, 70), ButtonBorderStyle.Solid);
            };

            // 1. Picture Box untuk Foto Produk
            PictureBox pbFoto = new PictureBox
            {
                Width = 200,
                Height = 150,
                Top = 10,
                Left = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // PERBAIKAN ERROR "PARAMETER IS NOT VALID"
            if (row["foto_produk"] != DBNull.Value)
            {
                try
                {
                    byte[] imgBytes = (byte[])row["foto_produk"];
                    if (imgBytes.Length > 0) // Pastikan byte tidak kosong
                    {
                        // Jangan gunakan keyword 'using' di sini karena UI masih butuh stream gambarnya
                        MemoryStream ms = new MemoryStream(imgBytes);
                        pbFoto.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    // Kalau datanya corrupt (\x dummy db), abaikan aja biar nggak crash.
                    pbFoto.Image = null;
                }
            }

            // Label Pengganti kalau fotonya kosong/error
            if (pbFoto.Image == null)
            {
                Label lblNoImage = new Label
                {
                    Text = "No Image",
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                pbFoto.Controls.Add(lblNoImage);
            }

            // 2. Kategori / PO Badge
            Label lblBadge = new Label
            {
                Text = judulPo,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                BackColor = judulPo == "Ready Stock" ? Color.FromArgb(155, 246, 255) : Color.FromArgb(253, 255, 182),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Top = 170,
                Left = 10,
                Padding = new Padding(3)
            };

            // 3. Nama Produk
            Label lblNama = new Label
            {
                Text = namaBarang,
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Top = 195,
                Left = 10,
                Width = 200,
                Height = 45,
                AutoSize = false
            };

            // 4. Harga
            Label lblHarga = new Label
            {
                Text = $"Rp {hargaAsli:N0}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Top = 245,
                Left = 10,
                AutoSize = true
            };

            // 5. Tombol Keranjang
            Button btnTambah = new Button
            {
                Text = "🛒 Sikat Miring!",
                Width = 200,
                Height = 35,
                Top = 290,
                Left = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(36, 0, 70), // Deep Purple
                ForeColor = Color.FromArgb(253, 255, 182), // Soft Yellow
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Black", 9F, FontStyle.Bold),
                Tag = idProduk // Simpan ID produk di tombol
            };
            btnTambah.FlatAppearance.BorderSize = 0;
            btnTambah.Click += (s, e) => BtnTambahKeranjang_Click(idProduk, namaBarang);

            // Masukin semua elemen ke Card
            card.Controls.Add(pbFoto);
            card.Controls.Add(lblBadge);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblHarga);
            card.Controls.Add(btnTambah);

            return card;
        }

        private void BtnTambahKeranjang_Click(int idProduk, string namaBarang)
        {
            // Ambil detail produk utuh pakai controller
            Models.Product p = _productController.GetProdukById(idProduk);

            if (p != null)
            {
                try
                {
                    _cartManager.TambahItem(p, "Saya Sendiri", 1, "");
                    MessageBox.Show($"Asyik! '{namaBarang}' berhasil masuk ke keranjang belanja kamu 🛒", "Sukses Masuk Keranjang", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Waduh Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
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
        private ProductService _productService;
        private CategoryService _categoryService;
        private List<Product> _daftarProduk;
        private string _pathFotoEdit = null; // untuk sementara saat edit

        public SellerProductListControl(int idPenjual, int idPo)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _idPo = idPo;
            _productService = new ProductService();
            _categoryService = new CategoryService();
            LoadKategoriCombo();
            LoadProduk();
            ResetForm();
        }

        private void LoadKategoriCombo()
        {
            var listKat = _categoryService.AmbilSemua();
            listKat.Insert(0, new Category { IdKategori = 0, NamaKategori = "-- Pilih Kategori --" });
            cmbKategori.DataSource = listKat;
            cmbKategori.DisplayMember = "NamaKategori";
            cmbKategori.ValueMember = "IdKategori";
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
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada produk di PO ini, bestie! 🥺\nTambah produk dulu yuk~";
                lblKosong.Font = new Font("Segoe UI", 14F);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
                flowPanelProduk.Controls.Add(lblKosong);
                return;
            }

            foreach (var produk in _daftarProduk)
            {
                Panel card = BuatCardProduk(produk);
                flowPanelProduk.Controls.Add(card);
            }
        }

        private Panel BuatCardProduk(Product produk)
        {
            Panel card = new Panel();
            card.Size = new Size(250, 200);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(8);

            // Foto
            PictureBox pic = new PictureBox()
            {
                Size = new Size(234, 90),
                Location = new Point(8, 8),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(167, 139, 250)
            };
            if (!string.IsNullOrEmpty(produk.FotoProduk))
            {
                string full = FileHelper.DapatkanFullPath(produk.FotoProduk);
                if (File.Exists(full)) pic.Image = Image.FromFile(full);
            }

            // Nama
            Label lblNama = new Label()
            {
                Text = produk.NamaProduk,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 27, 79),
                Size = new Size(234, 20),
                Location = new Point(8, 105)
            };

            // Harga
            Label lblHarga = new Label()
            {
                Text = $"Rp {produk.HargaDasar:N0}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(253, 224, 71),
                Size = new Size(120, 20),
                Location = new Point(8, 125)
            };

            // Tombol edit & hapus
            Button btnEdit = new Button()
            {
                Text = "✏️",
                BackColor = Color.FromArgb(167, 139, 250),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F),
                Size = new Size(35, 25),
                Location = new Point(8, 155)
            };
            btnEdit.Click += (s, e) => IsiFormEdit(produk);

            Button btnHapus = new Button()
            {
                Text = "🗑",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F),
                Size = new Size(35, 25),
                Location = new Point(48, 155)
            };
            btnHapus.Click += (s, e) =>
            {
                if (MessageBox.Show("Hapus produk ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_productService.HapusProduk(produk.IdProduk))
                        LoadProduk();
                }
            };

            card.Controls.Add(pic);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblHarga);
            card.Controls.Add(btnEdit);
            card.Controls.Add(btnHapus);
            return card;
        }

        private void IsiFormEdit(Product p)
        {
            txtNama.Text = p.NamaProduk;
            nudHarga.Value = p.HargaDasar;
            nudDiskon.Value = p.HargaDiskon ?? 0;
            nudTarget.Value = p.TargetKuota ?? 0;
            nudMinOrder.Value = p.MinOrder;
            cmbKategori.SelectedValue = p.IdKategori ?? 0;
            _pathFotoEdit = p.FotoProduk;
            // status foto
            lblStatusFoto.Text = string.IsNullOrEmpty(p.FotoProduk) ? "" : "Foto sudah ada (upload baru utk ganti)";
            btnSimpan.Text = "💾 Update";
            btnSimpan.Tag = p.IdProduk;
        }

        private void ResetForm()
        {
            txtNama.Clear();
            nudHarga.Value = 0;
            nudDiskon.Value = 0;
            nudTarget.Value = 0;
            nudMinOrder.Value = 1;
            cmbKategori.SelectedIndex = 0;
            _pathFotoEdit = null;
            lblStatusFoto.Text = "";
            btnSimpan.Text = "➕ Tambah Produk";
            btnSimpan.Tag = null;
        }

        private void btnUploadFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Images|*.jpg;*.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _pathFotoEdit = FileHelper.SimpanFile(dlg.FileName, "Products");
                    lblStatusFoto.Text = "Foto baru terupload ✨";
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            if (string.IsNullOrWhiteSpace(nama))
            {
                UXHelper.TampilkanError("Nama produk wajib diisi!");
                return;
            }
            int hargaDasar = (int)nudHarga.Value;
            int? hargaDiskon = nudDiskon.Value > 0 ? (int)nudDiskon.Value : (int?)null;
            int? targetKuota = nudTarget.Value > 0 ? (int)nudTarget.Value : (int?)null;
            int minOrder = (int)nudMinOrder.Value;
            int? idKategori = (int)cmbKategori.SelectedValue;
            if (idKategori == 0) idKategori = null;

            if (btnSimpan.Tag is int idEdit)
            {
                bool sukses = _productService.UpdateProduk(idEdit, nama, hargaDasar, hargaDiskon, targetKuota, minOrder, _pathFotoEdit);
                if (sukses)
                {
                    ResetForm();
                    LoadProduk();
                }
            }
            else
            {
                bool sukses = _productService.TambahProduk(_idPo, idKategori, nama, hargaDasar, hargaDiskon, targetKuota, minOrder, _pathFotoEdit);
                if (sukses)
                {
                    ResetForm();
                    LoadProduk();
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
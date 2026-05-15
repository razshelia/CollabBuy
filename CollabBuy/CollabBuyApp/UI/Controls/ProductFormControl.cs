using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ProductFormControl : UserControl
    {
        private int _idPo;
        private Product _produkEdit; // null jika tambah baru
        private string _pathFoto;
        private ProductService _productService;
        private CategoryService _categoryService;

        public ProductFormControl(int idPo, Product produkEdit = null)
        {
            InitializeComponent();
            _idPo = idPo;
            _produkEdit = produkEdit;
            _productService = new ProductService();
            _categoryService = new CategoryService();

            LoadKategoriCombo();

            if (_produkEdit != null)
                IsiFormEdit();
        }

        private void LoadKategoriCombo()
        {
            var listKat = _categoryService.AmbilSemua();
            listKat.Insert(0, new Category { IdKategori = 0, NamaKategori = "-- Pilih Kategori --" });
            cmbKategori.DataSource = listKat;
            cmbKategori.DisplayMember = "NamaKategori";
            cmbKategori.ValueMember = "IdKategori";
            cmbKategori.SelectedIndex = 0;
        }

        private void IsiFormEdit()
        {
            lblJudul.Text = "EDIT PRODUK ✏️";
            txtNama.Text = _produkEdit.NamaProduk;
            txtHarga.Text = _produkEdit.HargaDasar.ToString();
            txtDiskon.Text = _produkEdit.HargaDiskon?.ToString();
            txtTarget.Text = _produkEdit.TargetKuota?.ToString();
            nudMinOrder.Value = _produkEdit.MinOrder;
            txtDeskripsi.Text = _produkEdit.Deskripsi ?? string.Empty; // ← TAMBAHAN
            cmbKategori.SelectedValue = _produkEdit.IdKategori ?? 0;
            _pathFoto = _produkEdit.FotoProduk;

            if (!string.IsNullOrEmpty(_produkEdit.FotoProduk))
            {
                string fullPath = FileHelper.DapatkanFullPath(_produkEdit.FotoProduk);
                if (System.IO.File.Exists(fullPath))
                    pictureBoxPreview.Image = Image.FromFile(fullPath);
            }

            btnSimpan.Text = "💾 UPDATE PRODUK";
        }

        private void btnUploadFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Pilih Foto Produk";
                dlg.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _pathFoto = FileHelper.SimpanFile(dlg.FileName, "Products");
                        pictureBoxPreview.Image = Image.FromFile(FileHelper.DapatkanFullPath(_pathFoto));
                        lblStatusFoto.Text = "Foto berhasil diunggah ✨";
                        lblStatusFoto.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        UXHelper.TampilkanError("Gagal menyimpan foto: " + ex.Message);
                    }
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // --- Validasi ---
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                UXHelper.TampilkanError("Nama produk wajib diisi ya!");
                return;
            }
            if (!int.TryParse(txtHarga.Text, out int harga) || harga < 0)
            {
                UXHelper.TampilkanError("Harga harus berupa angka positif.");
                return;
            }

            int? diskon = int.TryParse(txtDiskon.Text.Trim(), out int d) ? d : (int?)null;
            int? target = int.TryParse(txtTarget.Text.Trim(), out int t) ? t : (int?)null;
            int minOrder = (int)nudMinOrder.Value;
            int? idKat = (int?)cmbKategori.SelectedValue;
            if (idKat == 0) idKat = null;

            // Ambil deskripsi (opsional, boleh kosong)
            string deskripsi = string.IsNullOrWhiteSpace(txtDeskripsi.Text)
                               ? null
                               : txtDeskripsi.Text.Trim();

            bool sukses;
            if (_produkEdit != null)
            {
                // Update — kirim deskripsi ke service
                sukses = _productService.UpdateProduk(
                    _produkEdit.IdProduk,
                    txtNama.Text.Trim(),
                    harga,
                    diskon,
                    target,
                    minOrder,
                    deskripsi,   // ← TAMBAHAN
                    _pathFoto
                );
            }
            else
            {
                // Tambah baru — kirim deskripsi ke service
                sukses = _productService.TambahProduk(
                    _idPo,
                    idKat,
                    txtNama.Text.Trim(),
                    harga,
                    diskon,
                    target,
                    minOrder,
                    deskripsi,   // ← TAMBAHAN
                    _pathFoto
                );
            }

            if (sukses)
            {
                if (ParentForm is MainForm main)
                {
                    var user = main.AmbilUserAktif();
                    main.GantiHalaman(new SellerProductListControl(user.IdUser, _idPo));
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
            {
                var user = main.AmbilUserAktif();
                main.GantiHalaman(new SellerProductListControl(user.IdUser, _idPo));
            }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ProductFormControl : UserControl
    {
        private int _idPenjual;
        private Product _produkEdit;
        private ProductService _productService;
        private CategoryService _categoryService;
        private string _pathFotoTerpilih = "";

        public ProductFormControl(int idPenjual, Product produkEdit = null)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _produkEdit = produkEdit;
            _productService = new ProductService();
            _categoryService = new CategoryService();

            LoadKategoriCombo();

            if (_produkEdit != null) PersiapkanModeEdit();
        }

        private void PersiapkanModeEdit()
        {
            lblTitle.Text = "Edit Produk Master";
            txtNamaProduk.Text = _produkEdit.NamaProduk;
            txtDeskripsi.Text = _produkEdit.Deskripsi;
            numHargaDasar.Value = _produkEdit.HargaDasar;
            numHargaDiskon.Value = _produkEdit.HargaDiskon ?? 0;
            numTargetKuota.Value = _produkEdit.TargetKuota ?? 0;
            numMinOrder.Value = _produkEdit.MinOrder;
            cmbKategori.SelectedValue = _produkEdit.IdKategori ?? 0;
            _pathFotoTerpilih = _produkEdit.FotoProduk;
            pbFotoProduk.ImageLocation = _pathFotoTerpilih;
            btnSimpan.Text = "Simpan Perubahan";
        }

        private void LoadKategoriCombo()
        {
            var list = _categoryService.AmbilSemua() ?? new List<Category>();
            list.Insert(0, new Category { IdKategori = 0, NamaKategori = "-- Pilih Kategori --" });
            cmbKategori.DataSource = list;
            cmbKategori.DisplayMember = "NamaKategori";
            cmbKategori.ValueMember = "IdKategori";
        }
        private void btnPilihFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Foto Produk";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _pathFotoTerpilih = ofd.FileName;
                    pbFotoProduk.ImageLocation = _pathFotoTerpilih;
                }
            }
        }

        private void ProductFormControl_Resize(object sender, EventArgs e)
        {
            // Memastikan kotak form pnlCard selalu berada di tengah layar
            if (pnlCard != null)
            {
                pnlCard.Location = new Point(
                    (this.ClientSize.Width - pnlCard.Width) / 2,
                    (this.ClientSize.Height - pnlCard.Height) / 2
                );
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = _produkEdit ?? new Product();
                p.IdPenjual = _idPenjual;
                p.NamaProduk = txtNamaProduk.Text.Trim();
                p.Deskripsi = txtDeskripsi.Text.Trim();
                p.HargaDasar = (int)numHargaDasar.Value;
                p.HargaDiskon = numHargaDiskon.Value > 0 ? (int?)numHargaDiskon.Value : null;
                p.TargetKuota = numTargetKuota.Value > 0 ? (int?)numTargetKuota.Value : null;
                p.MinOrder = (int)numMinOrder.Value;
                p.FotoProduk = _pathFotoTerpilih;

                int idKat = (int)cmbKategori.SelectedValue;
                p.IdKategori = idKat > 0 ? (int?)idKat : null;

                bool sukses = (_produkEdit == null)
                    ? _productService.TambahProduk(p)
                    : _productService.UpdateProduk(p);

                if (sukses && ParentForm is MainForm main)
                {
                    main.GantiHalaman(new SellerProductListControl(_idPenjual));
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
                main.GantiHalaman(new SellerProductListControl(_idPenjual));
        }
    }
}
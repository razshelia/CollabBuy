using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ProductFormControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly Product _produkEdit;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private string _pathFotoTerpilih = "";

        public ProductFormControl(int idPenjual, Product produkEdit = null)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _produkEdit = produkEdit;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _productService = new ProductService(new ProductRepository());
            _categoryService = new CategoryService(new CategoryRepository());

            LoadKategoriCombo();

            if (_produkEdit != null) PersiapkanModeEdit();

            // Memastikan form selalu di tengah
            this.Resize += (s, e) => CenterCard();
            this.Load += (s, e) => CenterCard();
        }

        private void CenterCard()
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
            }
        }

        private void PersiapkanModeEdit()
        {
            lblTitle.Text = "EDIT PRODUK MASTER ✏️";
            txtNamaProduk.Text = _produkEdit.NamaProduk;
            txtDeskripsi.Text = _produkEdit.Deskripsi;
            numHargaDasar.Value = _produkEdit.HargaDasar;
            numHargaDiskon.Value = _produkEdit.HargaDiskon ?? 0;
            numTargetKuota.Value = _produkEdit.TargetKuota ?? 0;
            numMinOrder.Value = _produkEdit.MinOrder;
            cmbKategori.SelectedValue = _produkEdit.IdKategori ?? 0;
            _pathFotoTerpilih = _produkEdit.FotoProduk;
            pbFotoProduk.ImageLocation = _pathFotoTerpilih;
            btnSimpan.Text = "SIMPAN PERUBAHAN 💾";
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

                int idKat = 0;
                if (cmbKategori.SelectedValue != null)
                {
                    int.TryParse(cmbKategori.SelectedValue.ToString(), out idKat);
                }
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class BukaSesiPOControl : UserControl
    {
        private readonly User _currentSeller;
        private readonly PreOrderController _preOrderController;
        private readonly ProductController _productController;
        private List<Product> _sellerProducts;

        public BukaSesiPOControl(User seller)
        {
            InitializeComponent();
            _currentSeller = seller;
            _preOrderController = new PreOrderController();
            _productController = new ProductController();
        }

        private void BukaSesiPOControl_Load(object sender, EventArgs e)
        {
            // Atur waktu minimal batas waktu adalah saat ini
            dtpBatasWaktu.MinDate = DateTime.Now;
            LoadMasterProduk();
        }

        private void LoadMasterProduk()
        {
            try
            {
                // Ambil daftar produk master milik toko/seller ini
                // Pastikan method ini ada di ProductController Anda, jika belum sesuaikan namanya
                // _sellerProducts = _productController.GetProductsBySeller(_currentSeller.IdUser);

                // --- MOCK DATA UNTUK COMBOB0X ---
                _sellerProducts = new List<Product>
                {
                    new Product { IdProduct = 1, NamaProduct = "Makaroni Bantet Pedas" },
                    new Product { IdProduct = 2, NamaProduct = "Keripik Kaca Original" },
                    new Product { IdProduct = 3, NamaProduct = "Gantungan Kunci Custom" }
                };
                // ---------------------------------

                cbProduk.DisplayMember = "NamaProduct";
                cbProduk.ValueMember = "IdProduct";
                cbProduk.DataSource = _sellerProducts;

                if (_sellerProducts.Count == 0)
                {
                    MessageBox.Show("Anda belum mendaftarkan produk master jualan. Silakan buat produk terlebih dahulu di Manajemen Produk.",
                                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pnlForm.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat produk master: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimpanSesi_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input Dasar
            if (string.IsNullOrWhiteSpace(txtNamaSesi.Text))
            {
                MessageBox.Show("Nama Sesi PO/Danus tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaSesi.Focus();
                return;
            }

            if (cbProduk.SelectedValue == null)
            {
                MessageBox.Show("Silakan pilih produk jualan terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpBatasWaktu.Value <= DateTime.Now)
            {
                MessageBox.Show("Batas waktu tenggat PO harus lebih besar dari waktu sekarang!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Konfirmasi Pembuatan Sesi
            DialogResult confirm = MessageBox.Show($"Luncurkan sesi PO '{txtNamaSesi.Text}'?",
                                                   "CollabBuy - Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ProsesSimpanSesiPO();
            }
        }

        private void ProsesSimpanSesiPO()
        {
            try
            {
                int idProdukTerpilih = Convert.ToInt32(cbProduk.SelectedValue);
                int kuotaMaks = Convert.ToInt32(numQuota.Value);
                DateTime deadline = dtpBatasWaktu.Value;
                string catatan = txtDeskripsi.Text.Trim();

                // Pembuatan instance model Preorder sesuai parameter konstruktor Anda
                // Asumsi konstruktor Preorder: Preorder(int idProduct, int maxQuota, DateTime endTime)
                // Catatan: Sesuaikan instansiasi ini dengan Model Preorder.cs milik Anda

                // Preorder poBaru = new Preorder(idProdukTerpilih, kuotaMaks, deadline);
                // poBaru.Validate(); // Validasi model menggunakan interface IValidatable

                // PANGGIL CONTROLLER UNTUK INSERT DATA KE DATABASE
                // var result = _preOrderController.BuatSesiPreOrder(poBaru);

                // MOCK SUCCESS
                bool sukses = true;
                string pesanResult = "Sesi Pre-Order berhasil diluncurkan ke katalog utama mahasiswa.";

                if (sukses)
                {
                    MessageBox.Show(pesanResult, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan sesi PO.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOrderException ex)
            {
                MessageBox.Show(ex.GetPesanLengkap(), "Peringatan Aturan Bisnis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtNamaSesi.Clear();
            txtDeskripsi.Clear();
            numQuota.Value = 10;
            dtpBatasWaktu.Value = DateTime.Now;
            if (cbProduk.Items.Count > 0) cbProduk.SelectedIndex = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

// Alias eksplisit untuk menghindari konflik nama antara
// kelas Model "Product" dan namespace "Product" (jika ada).
using ProductModel = CollabBuy.CollabBuyApp.Models.Product;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    /// <summary>
    /// BukaSesiPOControl: UserControl untuk membuka sesi Pre-Order (PO) baru.
    ///
    /// Tanggung Jawab:
    /// - Menampilkan form input data sesi PO.
    /// - Memuat daftar produk milik seller dari ProductController.
    /// - Mendelegasikan penyimpanan sesi PO ke PreOrderController.
    ///
    /// Perbaikan OOP:
    /// - Menggunakan alias "ProductModel" untuk menghindari ambiguitas namespace CS0118.
    /// - Mock data menggunakan setter method sesuai enkapsulasi model Product.
    /// </summary>
    public partial class BukaSesiPOControl : UserControl
    {
        // === FIELDS ===
        private readonly User _currentSeller;
        private readonly PreOrderController _preOrderController;
        private readonly ProductController _productController;
        private List<ProductModel> _sellerProducts;

        // === KONSTRUKTOR ===
        public BukaSesiPOControl(User seller)
        {
            InitializeComponent();
            _currentSeller = seller;
            _preOrderController = new PreOrderController();
            _productController = new ProductController();
        }

        // === EVENT HANDLERS ===

        private void BukaSesiPOControl_Load(object sender, EventArgs e)
        {
            dtpBatasWaktu.MinDate = DateTime.Now;
            LoadMasterProduk();
        }

        private void btnSimpanSesi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaSesi.Text))
            {
                MessageBox.Show("Nama Sesi PO/Danus tidak boleh kosong!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaSesi.Focus();
                return;
            }

            if (cbProduk.SelectedValue == null)
            {
                MessageBox.Show("Silakan pilih produk jualan terlebih dahulu!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpBatasWaktu.Value <= DateTime.Now)
            {
                MessageBox.Show("Batas waktu tenggat PO harus lebih besar dari waktu sekarang!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Luncurkan sesi PO '{txtNamaSesi.Text}'?",
                "CollabBuy - Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ProsesSimpanSesiPO();
            }
        }

        // === PRIVATE METHODS ===

        /// <summary>
        /// Memuat daftar produk milik seller untuk ditampilkan di ComboBox.
        /// Menggunakan getter method sesuai enkapsulasi kelas Product.
        /// </summary>
        private void LoadMasterProduk()
        {
            try
            {
                // TODO: Ganti mock data dengan pemanggilan controller sebenarnya:
                // _sellerProducts = _productController.GetProductsBySeller(_currentSeller.GetIdUser());

                // --- MOCK DATA ---
                // Catatan: Product tidak menggunakan object initializer karena field-nya private.
                // Gunakan setter method yang disediakan oleh model.
                _sellerProducts = new List<ProductModel>();

                var produk1 = new ProductModel(
                    idPenjual: _currentSeller != null ? _currentSeller.GetIdUser() : 0,
                    idKategori: 1,
                    namaProduk: "Makaroni Bantet Pedas",
                    hargaDasar: 10000);
                produk1.SetIdProduk(1);

                var produk2 = new ProductModel(
                    idPenjual: _currentSeller != null ? _currentSeller.GetIdUser() : 0,
                    idKategori: 1,
                    namaProduk: "Keripik Kaca Original",
                    hargaDasar: 15000);
                produk2.SetIdProduk(2);

                var produk3 = new ProductModel(
                    idPenjual: _currentSeller != null ? _currentSeller.GetIdUser() : 0,
                    idKategori: 1,
                    namaProduk: "Gantungan Kunci Custom",
                    hargaDasar: 20000);
                produk3.SetIdProduk(3);

                _sellerProducts.Add(produk1);
                _sellerProducts.Add(produk2);
                _sellerProducts.Add(produk3);
                // --- END MOCK DATA ---

                // Bind ke ComboBox menggunakan nama method getter sebagai DisplayMember tidak bisa,
                // karena WinForms membutuhkan properti, bukan method.
                // Solusi: gunakan anonymous wrapper atau BindingList dengan adapter.
                cbProduk.DataSource = null;
                cbProduk.Items.Clear();

                foreach (var p in _sellerProducts)
                {
                    cbProduk.Items.Add(new ProductComboItem(p.GetIdProduk(), p.GetNamaProduk()));
                }

                cbProduk.DisplayMember = "NamaProduk";
                cbProduk.ValueMember = "IdProduk";

                if (_sellerProducts.Count == 0)
                {
                    MessageBox.Show(
                        "Anda belum mendaftarkan produk master jualan. " +
                        "Silakan buat produk terlebih dahulu di Manajemen Produk.",
                        "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pnlForm.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat produk master: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Memproses penyimpanan sesi PO setelah validasi lulus.
        /// </summary>
        private void ProsesSimpanSesiPO()
        {
            try
            {
                var selectedItem = cbProduk.SelectedItem as ProductComboItem;
                if (selectedItem == null) return;

                int idProdukTerpilih = selectedItem.IdProduk;
                int kuotaMaks = Convert.ToInt32(numQuota.Value);
                DateTime deadline = dtpBatasWaktu.Value;

                // TODO: Ganti mock success dengan pemanggilan controller:
                // Preorder poBaru = new Preorder(idProdukTerpilih, kuotaMaks, deadline);
                // poBaru.Validate();
                // bool sukses = _preOrderController.BuatSesiPreOrder(poBaru);

                bool sukses = true; // MOCK
                string pesanResult = "Sesi Pre-Order berhasil diluncurkan ke katalog utama mahasiswa.";

                if (sukses)
                {
                    MessageBox.Show(pesanResult, "CollabBuy - Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan sesi PO.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOrderException ex)
            {
                MessageBox.Show(ex.GetPesanLengkap(), "Peringatan Aturan Bisnis",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sistem: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mereset form ke kondisi awal setelah penyimpanan berhasil.
        /// </summary>
        private void ResetForm()
        {
            txtNamaSesi.Clear();
            txtDeskripsi.Clear();
            numQuota.Value = 10;
            dtpBatasWaktu.Value = DateTime.Now;
            if (cbProduk.Items.Count > 0) cbProduk.SelectedIndex = 0;
        }

        // === INNER CLASS (Adapter untuk ComboBox) ===

        /// <summary>
        /// DTO ringan sebagai adapter antara kelas Product (yang menggunakan method getter)
        /// dengan WinForms ComboBox (yang membutuhkan properti publik untuk data binding).
        ///
        /// Pola: Data Transfer Object (DTO) / Adapter.
        /// </summary>
        private sealed class ProductComboItem
        {
            public int IdProduk { get; }
            public string NamaProduk { get; }

            public ProductComboItem(int idProduk, string namaProduk)
            {
                IdProduk = idProduk;
                NamaProduk = namaProduk;
            }

            public override string ToString() => NamaProduk;
        }
    }
}
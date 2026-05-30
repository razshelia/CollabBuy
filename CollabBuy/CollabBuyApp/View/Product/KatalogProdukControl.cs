using System;
using System.Data;
using System.Drawing;
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

            // Inisialisasi Keranjang In-Memory
            _cartManager = new CartManager(_currentUser.GetIdUser());
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadKatalog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadKatalog();
        }

        private void SetupDataGridView()
        {
            dgvKatalog.AutoGenerateColumns = false;
            dgvKatalog.Columns.Clear();
            dgvKatalog.RowTemplate.Height = 80; // Biar fotonya lega

            dgvKatalog.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProduk", DataPropertyName = "id_produk", Visible = false });

            // Kolom Gambar Baru
            DataGridViewImageColumn colFoto = new DataGridViewImageColumn();
            colFoto.Name = "Foto";
            colFoto.HeaderText = "Foto";
            colFoto.DataPropertyName = "foto_image"; // Ngambil dari kolom buatan kita di LoadKatalog
            colFoto.ImageLayout = DataGridViewImageCellLayout.Zoom; // Biar proporsional
            colFoto.Width = 80;
            dgvKatalog.Columns.Add(colFoto);

            dgvKatalog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Barang", DataPropertyName = "nama_produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvKatalog.Columns.Add(new DataGridViewTextBoxColumn { Name = "PO", HeaderText = "Sesi PO", DataPropertyName = "judul_po", Width = 150 });
            dgvKatalog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kategori", HeaderText = "Kategori", DataPropertyName = "nama_kategori", Width = 120 });
            dgvKatalog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga", DataPropertyName = "harga_format", Width = 120 });
        }

        private void LoadKatalog()
        {
            try
            {
                DataTable dtRaw = _productController.GetKatalogUtama();
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_produk", typeof(int));
                dtUI.Columns.Add("foto_image", typeof(Image)); // Tipe Data Image!
                dtUI.Columns.Add("nama_produk", typeof(string));
                dtUI.Columns.Add("judul_po", typeof(string));
                dtUI.Columns.Add("nama_kategori", typeof(string));
                dtUI.Columns.Add("harga_format", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string judulPo = row.IsNull("judul_po") ? "Non-PO" : row["judul_po"].ToString();
                    string kategori = row.IsNull("nama_kategori") ? "-" : row["nama_kategori"].ToString();
                    string harga = "Rp " + Convert.ToInt32(row["harga_dasar"]).ToString("N0");

                    // Konversi Byte to Image
                    Image foto = null;
                    if (row["foto_produk"] != DBNull.Value)
                    {
                        byte[] imgBytes = (byte[])row["foto_produk"];
                        using (var ms = new System.IO.MemoryStream(imgBytes))
                        {
                            foto = Image.FromStream(ms);
                        }
                    }

                    dtUI.Rows.Add(row["id_produk"], foto, row["nama_produk"], judulPo, kategori, harga);
                }

                dgvKatalog.DataSource = dtUI;
                dgvKatalog.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load katalog: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambahKeranjang_Click(object sender, EventArgs e)
        {
            if (dgvKatalog.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih dulu barang yang mau dibeli di tabel ya bestie!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProduk = Convert.ToInt32(dgvKatalog.SelectedRows[0].Cells["IdProduk"].Value);
            string namaBarang = dgvKatalog.SelectedRows[0].Cells["Nama"].Value.ToString();

            // Panggil Product Controller buat narik objek Product utuh (karena CartManager butuh objek Product)
            Models.Product p = _productController.GetProdukById(idProduk);

            if (p != null)
            {
                try
                {
                    _cartManager.TambahItem(p, "Saya Sendiri", 1, "");
                    MessageBox.Show($"Asyik! '{namaBarang}' berhasil masuk ke keranjang belanja kamu 🛒", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Gagal Masuk Keranjang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
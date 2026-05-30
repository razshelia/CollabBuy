using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class ManajemenProdukControl : UserControl
    {
        private readonly User _currentUser;
        private readonly ProductController _productController;

        public ManajemenProdukControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _productController = new ProductController();
        }

        private void ManajemenProdukControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataProduk();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataProduk();
        }

        private void SetupDataGridView()
        {
            dgvLapak.AutoGenerateColumns = false;
            dgvLapak.Columns.Clear();
            dgvLapak.RowTemplate.Height = 80; // Kasih space buat foto

            // Kolom Gambar Baru
            DataGridViewImageColumn colFoto = new DataGridViewImageColumn();
            colFoto.Name = "Foto";
            colFoto.HeaderText = "Foto";
            colFoto.DataPropertyName = "foto_image";
            colFoto.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colFoto.Width = 80;
            dgvLapak.Columns.Add(colFoto);

            dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Barang", DataPropertyName = "nama_produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kategori", HeaderText = "Kategori", DataPropertyName = "nama_kategori", Width = 150 });
            dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "PO", HeaderText = "Sesi PO", DataPropertyName = "judul_po", Width = 150 });
            dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga Jual", DataPropertyName = "harga_format", Width = 130 });
            dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuota", HeaderText = "Target Kuota", DataPropertyName = "target_kuota", Width = 100 });
        }

        private void LoadDataProduk()
        {
            try
            {
                DataTable dtRaw = _productController.GetProdukLapak(_currentUser.GetIdUser());
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("foto_image", typeof(Image));
                dtUI.Columns.Add("nama_produk", typeof(string));
                dtUI.Columns.Add("nama_kategori", typeof(string));
                dtUI.Columns.Add("judul_po", typeof(string));
                dtUI.Columns.Add("harga_format", typeof(string));
                dtUI.Columns.Add("target_kuota", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string judulPo = row.IsNull("judul_po") ? "Reguler" : row["judul_po"].ToString();
                    string kategori = row.IsNull("nama_kategori") ? "-" : row["nama_kategori"].ToString();
                    string harga = "Rp " + Convert.ToInt32(row["harga_dasar"]).ToString("N0");
                    string kuota = row.IsNull("target_kuota") ? "-" : row["target_kuota"].ToString();

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

                    dtUI.Rows.Add(foto, row["nama_produk"], kategori, judulPo, harga, kuota);
                }

                dgvLapak.DataSource = dtUI;
                dgvLapak.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data lapak: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
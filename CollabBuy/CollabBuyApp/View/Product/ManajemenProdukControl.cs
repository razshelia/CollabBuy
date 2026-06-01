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

            this.Resize += (s, e) => AdjustLayout();
        }

        private void ManajemenProdukControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            SetupDataGridView();
            LoadDataProduk();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataProduk();
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            pnlGrid.Width = w;
            dgvLapak.Width = pnlGrid.Width - 68;
            btnRefresh.Left = pnlGrid.Width - btnRefresh.Width - 34;
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
                dtUI.Columns.Add("nama_kategori", typeof(string)); // Ini yang akan kita proses
                dtUI.Columns.Add("judul_po", typeof(string));
                dtUI.Columns.Add("harga_format", typeof(string));
                dtUI.Columns.Add("target_kuota", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string judulPo = row.IsNull("judul_po") ? "Reguler" : row["judul_po"].ToString();

                    // --- OOP BEST PRACTICE CALL ---
                    // 1. Ambil nama kategori mentah dari DB
                    string namaKatMentah = row.IsNull("nama_kategori") ? "Umum" : row["nama_kategori"].ToString();

                    // 2. Buat objek Category, ini otomatis menjalankan method RapikanNamaKategori() di konstruktornya!
                    Category katObj = new Category(namaKatMentah);

                    // 3. Gunakan hasil yang sudah rapi
                    string kategoriRapi = katObj.GetNamaKategori();
                    // ------------------------------

                    string harga = "Rp " + Convert.ToInt32(row["harga_dasar"]).ToString("N0");
                    string kuota = row.IsNull("target_kuota") ? "-" : row["target_kuota"].ToString();

                    Image foto = null;
                    if (row["foto_produk"] != DBNull.Value)
                    {
                        byte[] imgBytes = (byte[])row["foto_produk"];
                        using (var ms = new System.IO.MemoryStream(imgBytes))
                        {
                            foto = Image.FromStream(ms);
                        }
                    }

                    dtUI.Rows.Add(foto, row["nama_produk"], kategoriRapi, judulPo, harga, kuota);
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
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
        private readonly Models.User _currentUser;
        private readonly ProductController _productController;

        public ManajemenProdukControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._currentUser = currentUser;
            this._productController = new ProductController();

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void ManajemenProdukControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataProduk();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataProduk();
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);

            this.pnlGrid.Width = w;
            this.dgvLapak.Width = this.pnlGrid.Width - 68;
            this.btnRefresh.Left = this.pnlGrid.Width - this.btnRefresh.Width - 34;
        }

        private void SetupDataGridView()
        {
            this.dgvLapak.AutoGenerateColumns = false;
            this.dgvLapak.Columns.Clear();
            this.dgvLapak.RowTemplate.Height = 80; // Kasih space buat foto

            // Kolom Gambar Baru
            DataGridViewImageColumn colFoto = new DataGridViewImageColumn();
            colFoto.Name = "Foto";
            colFoto.HeaderText = "Foto";
            colFoto.DataPropertyName = "foto_image";
            colFoto.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colFoto.Width = 80;

            this.dgvLapak.Columns.Add(colFoto);

            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Barang", DataPropertyName = "nama_produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kategori", HeaderText = "Kategori", DataPropertyName = "nama_kategori", Width = 150 });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "PO", HeaderText = "Sesi PO", DataPropertyName = "judul_po", Width = 150 });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga Jual", DataPropertyName = "harga_format", Width = 130 });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuota", HeaderText = "Target Kuota", DataPropertyName = "target_kuota", Width = 100 });
        }

        private void LoadDataProduk()
        {
            try
            {
                DataTable dtRaw = this._productController.GetProdukLapak(this._currentUser.GetIdUser());
                DataTable dtUI = new DataTable();

                dtUI.Columns.Add("foto_image", typeof(Image));
                dtUI.Columns.Add("nama_produk", typeof(string));
                dtUI.Columns.Add("nama_kategori", typeof(string));
                dtUI.Columns.Add("judul_po", typeof(string));
                dtUI.Columns.Add("harga_format", typeof(string));
                dtUI.Columns.Add("target_kuota", typeof(string));

                if (dtRaw != null)
                {
                    foreach (DataRow row in dtRaw.Rows)
                    {
                        string judulPo;
                        if (row.IsNull("judul_po"))
                        {
                            judulPo = "Reguler";
                        }
                        else
                        {
                            judulPo = row["judul_po"].ToString();
                        }

                        // =======================================================
                        // OOP BEST PRACTICE: Pemanfaatan Model Category
                        // =======================================================
                        string namaKatMentah;
                        if (row.IsNull("nama_kategori"))
                        {
                            namaKatMentah = "Umum";
                        }
                        else
                        {
                            namaKatMentah = row["nama_kategori"].ToString();
                        }

                        Models.Category katObj = new Models.Category(namaKatMentah);
                        string kategoriRapi = katObj.GetNamaKategori();

                        string harga;
                        if (row["harga_dasar"] != DBNull.Value)
                        {
                            harga = "Rp " + Convert.ToInt32(row["harga_dasar"]).ToString("N0");
                        }
                        else
                        {
                            harga = "Rp 0";
                        }

                        string kuota;
                        if (row.IsNull("target_kuota"))
                        {
                            kuota = "-";
                        }
                        else
                        {
                            kuota = row["target_kuota"].ToString();
                        }

                        Image foto = null;
                        if (row["foto_produk"] != DBNull.Value)
                        {
                            try
                            {
                                byte[] imgBytes = (byte[])row["foto_produk"];
                                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imgBytes))
                                {
                                    foto = Image.FromStream(ms);
                                }
                            }
                            catch
                            {
                                foto = null;
                            }
                        }
                        else
                        {
                            bool fotoKosong = true; // Penugasan untuk menghindari else kosong
                        }

                        dtUI.Rows.Add(foto, row["nama_produk"], kategoriRapi, judulPo, harga, kuota);
                    }
                }
                else
                {
                    bool dataKosong = true;
                }

                this.dgvLapak.DataSource = dtUI;
                this.dgvLapak.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data lapak: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
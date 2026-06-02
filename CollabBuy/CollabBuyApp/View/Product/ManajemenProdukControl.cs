using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class ManajemenProdukControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly ProductController _productController;
        private readonly CategoryRepository _categoryRepo;
        private byte[] _fotoProdukBytes = null;

        public ManajemenProdukControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._currentUser = currentUser;
            this._productController = new ProductController();
            this._categoryRepo = new CategoryRepository();

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void ManajemenProdukControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataProduk();
            this.LoadKategori();
            this.pnlTambahProduk.Visible = false; // Panel form tersembunyi saat awal
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataProduk();
        }

        // === TOMBOL TAMBAH PRODUK: toggle show/hide panel ===
        private void btnTambahProduk_Click(object sender, EventArgs e)
        {
            this.pnlTambahProduk.Visible = !this.pnlTambahProduk.Visible;
            if (this.pnlTambahProduk.Visible)
            {
                this.ResetFormTambah();
            }
        }

        private void btnBatalTambah_Click(object sender, EventArgs e)
        {
            this.pnlTambahProduk.Visible = false;
            this.ResetFormTambah();
        }

        private void btnPilihFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Pilih Foto Produk";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length > 2097152)
                    {
                        MessageBox.Show("Ukuran foto maksimal 2MB ya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _fotoProdukBytes = File.ReadAllBytes(ofd.FileName);
                    picFotoPreview.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnSimpanProduk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaProduk.Text))
            {
                MessageBox.Show("Nama produk wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaProduk.Focus();
                return;
            }

            if (cbKategoriProduk.SelectedValue == null)
            {
                MessageBox.Show("Pilih kategori dulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtHargaProduk.Text, out int harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHargaProduk.Focus();
                return;
            }

            if (!int.TryParse(txtMinOrder.Text, out int minOrder) || minOrder <= 0)
            {
                MessageBox.Show("Min. order harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMinOrder.Focus();
                return;
            }

            int idKategori = Convert.ToInt32(cbKategoriProduk.SelectedValue);

            var result = this._productController.TambahProdukBaru(
                idPenjual: this._currentUser.GetIdUser(),
                idKategori: idKategori,
                namaProduk: txtNamaProduk.Text.Trim(),
                hargaDasar: harga,
                idPo: null,
                targetKuota: null,
                minOrder: minOrder,
                fotoProduk: _fotoProdukBytes
            );

            if (result.sukses)
            {
                MessageBox.Show(result.pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.pnlTambahProduk.Visible = false;
                this.ResetFormTambah();
                this.LoadDataProduk();
            }
            else
            {
                MessageBox.Show(result.pesan, "Gagal Simpan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKategori()
        {
            try
            {
                DataTable dt = this._categoryRepo.GetAll();
                cbKategoriProduk.DataSource = dt;
                cbKategoriProduk.DisplayMember = "nama_kategori";
                cbKategoriProduk.ValueMember = "id_kategori";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load kategori: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetFormTambah()
        {
            txtNamaProduk.Clear();
            txtHargaProduk.Clear();
            txtMinOrder.Text = "1";
            txtDeskripsiProduk.Clear();
            picFotoPreview.Image = null;
            _fotoProdukBytes = null;
            if (cbKategoriProduk.Items.Count > 0) cbKategoriProduk.SelectedIndex = 0;
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);

            this.pnlGrid.Width = w;
            this.dgvLapak.Width = this.pnlGrid.Width - 68;
            this.btnRefresh.Left = this.pnlGrid.Width - this.btnRefresh.Width - 34;
            this.btnTambahProduk.Left = this.btnRefresh.Left - this.btnTambahProduk.Width - 12;

            this.pnlTambahProduk.Width = w;
        }

        private void SetupDataGridView()
        {
            this.dgvLapak.AutoGenerateColumns = false;
            this.dgvLapak.Columns.Clear();
            this.dgvLapak.RowTemplate.Height = 80;

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
                                if (imgBytes.Length > 1)
                                {
                                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imgBytes))
                                    {
                                        foto = Image.FromStream(ms);
                                    }
                                }
                            }
                            catch
                            {
                                foto = null;
                            }
                        }
                        else
                        {
                            bool fotoKosong = true;
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
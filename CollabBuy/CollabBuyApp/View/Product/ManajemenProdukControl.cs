using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

        // --- KONFIGURASI TABEL ---
        private void SetupDataGridView()
        {
            dgvProduk.AutoGenerateColumns = false;
            dgvProduk.Columns.Clear();

            dgvProduk.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdProduk",
                DataPropertyName = "Id", // Ganti sesuai property ID di model Product Anda
                Visible = false // Sembunyikan ID dari UI
            });

            dgvProduk.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaProduk",
                HeaderText = "Nama Produk",
                DataPropertyName = "NamaProduk", // Ganti sesuai property di model
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProduk.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Kategori",
                HeaderText = "Kategori",
                DataPropertyName = "NamaKategori", // Ganti sesuai property di model
                Width = 150
            });

            dgvProduk.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Harga",
                HeaderText = "Harga (Rp)",
                DataPropertyName = "Harga",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } // Format uang
            });

            dgvProduk.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stok",
                HeaderText = "Stok",
                DataPropertyName = "Stok",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Kolom Tombol Edit
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
            {
                Name = "BtnEdit",
                HeaderText = "Aksi",
                Text = "✏️ Edit",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            btnEdit.DefaultCellStyle.BackColor = Color.LightSkyBlue;
            btnEdit.DefaultCellStyle.ForeColor = Color.Black;
            dgvProduk.Columns.Add(btnEdit);

            // Kolom Tombol Hapus
            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn
            {
                Name = "BtnHapus",
                HeaderText = "",
                Text = "🗑️ Hapus",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };
            btnHapus.DefaultCellStyle.BackColor = Color.LightCoral;
            btnHapus.DefaultCellStyle.ForeColor = Color.Black;
            dgvProduk.Columns.Add(btnHapus);
        }

        // --- MEMUAT DATA ---
        private void LoadDataProduk()
        {
            try
            {
                // TODO: Panggil method di ProductController untuk mengambil produk berdasarkan ID Penjual/Toko saat ini.
                // var daftarProduk = _productController.GetProductsBySeller(_currentUser.IdUser);

                // --- MOCK DATA UNTUK PREVIEW UI ---
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("NamaProduk", typeof(string));
                dtMock.Columns.Add("NamaKategori", typeof(string));
                dtMock.Columns.Add("Harga", typeof(decimal));
                dtMock.Columns.Add("Stok", typeof(int));

                dtMock.Rows.Add(1, "Makaroni Bantet Pedas", "Makanan & Minuman", 5000, 100);
                dtMock.Rows.Add(2, "Keripik Kaca Original", "Makanan & Minuman", 6000, 50);
                dtMock.Rows.Add(3, "Gantungan Kunci Custom", "Aksesoris", 15000, 20);

                dgvProduk.DataSource = dtMock;
                // ---------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data produk:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- AKSI TOMBOL TAMBAH ---
        private void btnTambahProduk_Click(object sender, EventArgs e)
        {
            // TODO: Tampilkan form/dialog untuk menambah produk baru.
            // Contoh implementasi jika Anda punya Form TambahProdukForm:

            /*
            using (var formTambah = new FormTambahProduk(_currentUser.IdUser))
            {
                if (formTambah.ShowDialog() == DialogResult.OK)
                {
                    LoadDataProduk(); // Refresh data setelah berhasil tambah
                }
            }
            */

            MessageBox.Show("Form Tambah Produk belum diimplementasikan.\nSilakan buat Form dialog baru untuk input data produk.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- AKSI TOMBOL TABEL (EDIT & HAPUS) ---
        private void dgvProduk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Abaikan klik pada Header
            if (e.RowIndex < 0) return;

            int idProduk = Convert.ToInt32(dgvProduk.Rows[e.RowIndex].Cells["IdProduk"].Value);
            string namaProduk = dgvProduk.Rows[e.RowIndex].Cells["NamaProduk"].Value.ToString();

            // AKSI EDIT
            if (dgvProduk.Columns[e.ColumnIndex].Name == "BtnEdit")
            {
                // TODO: Buka Form Edit, passing ID Produk ke constructor form tersebut.
                MessageBox.Show($"Buka Form Edit untuk produk: {namaProduk} (ID: {idProduk})", "Info Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // AKSI HAPUS
            else if (dgvProduk.Columns[e.ColumnIndex].Name == "BtnHapus")
            {
                DialogResult dialog = MessageBox.Show($"Apakah Anda yakin ingin MENGHAPUS produk '{namaProduk}' secara permanen?",
                                                      "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialog == DialogResult.Yes)
                {
                    ProsesHapusProduk(idProduk);
                }
            }
        }

        private void ProsesHapusProduk(int idProduk)
        {
            try
            {
                // TODO: Panggil method hapus di controller
                // bool sukses = _productController.DeleteProduct(idProduk);

                bool sukses = true; // Mock sukses

                if (sukses)
                {
                    MessageBox.Show("Produk berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataProduk(); // Segarkan tabel
                }
                else
                {
                    MessageBox.Show("Gagal menghapus produk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

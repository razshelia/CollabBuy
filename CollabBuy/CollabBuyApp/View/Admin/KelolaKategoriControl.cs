using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class KelolaKategoriControl : UserControl
    {
        private readonly AdminController _adminController;

        public KelolaKategoriControl()
        {
            InitializeComponent();
            _adminController = new AdminController();
        }

        private void KelolaKategoriControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataKategori();
        }

        // --- KONFIGURASI KOLOM TABEL ---
        private void SetupDataGridView()
        {
            dgvKategori.AutoGenerateColumns = false;
            dgvKategori.Columns.Clear();

            // CATATAN: Pastikan 'DataPropertyName' sesuai dengan nama property di model Category.cs Anda
            // Contoh jika di model ada property Id_Kategori dan Nama_Kategori

            dgvKategori.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdKategori",
                HeaderText = "ID Kategori",
                DataPropertyName = "Id_Kategori", // <-- Ganti sesuai nama property ID di model Category Anda
                Width = 150
            });

            dgvKategori.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaKategori",
                HeaderText = "Nama Kategori",
                DataPropertyName = "Nama_Kategori", // <-- Ganti sesuai nama property Nama di model Category Anda
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        // --- MEMUAT DATA KATEGORI ---
        private void LoadDataKategori()
        {
            try
            {
                // Memanggil method GetAllKategori dari AdminController Anda
                List<Category> listKategori = _adminController.GetAllKategori();

                // Bind data ke GridView
                dgvKategori.DataSource = null; // Reset binding
                dgvKategori.DataSource = listKategori;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data kategori:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENT KLIK TOMBOL TAMBAH ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            string namaKategori = txtKategoriBaru.Text.Trim();

            // 1. Validasi Input Kosong
            if (string.IsNullOrWhiteSpace(namaKategori))
            {
                MessageBox.Show("Nama kategori tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKategoriBaru.Focus();
                return;
            }

            // 2. Konfirmasi Tambah Data
            DialogResult confirm = MessageBox.Show($"Apakah Anda yakin ingin menambahkan kategori '{namaKategori}'?",
                                                   "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // 3. Panggil method TambahKategoriBaru dari AdminController yang sudah Anda buat
                var result = _adminController.TambahKategoriBaru(namaKategori);

                if (result.sukses)
                {
                    MessageBox.Show(result.pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Bersihkan form dan Refresh Data
                    txtKategoriBaru.Clear();
                    LoadDataKategori();
                }
                else
                {
                    // Menampilkan pesan error dari Validasi/Unique Constraint database Anda
                    MessageBox.Show(result.pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

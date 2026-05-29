using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class KeranjangBelanjaControl : UserControl
    {
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;

        public KeranjangBelanjaControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // PERBAIKAN: Gunakan konstruktor overload TransactionController(int idPembeli)
            // agar CartManager diinisialisasi dengan ID pembeli yang benar.
            // Sebelumnya memakai new TransactionController() (tanpa parameter) yang menyebabkan
            // CartManager = null dan NullReferenceException saat checkout.
            _transactionController = new TransactionController(_currentUser.GetIdUser());
        }

        private void KeranjangBelanjaControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataKeranjang();
        }

        // --- KONFIGURASI KOLOM TABEL ---
        private void SetupDataGridView()
        {
            dgvKeranjang.AutoGenerateColumns = false;
            dgvKeranjang.Columns.Clear();

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdItem",
                DataPropertyName = "Id",
                Visible = false
            });

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaItem",
                HeaderText = "Nama Produk / PO",
                DataPropertyName = "Nama",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Harga",
                HeaderText = "Harga Satuan",
                DataPropertyName = "Harga",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Kuantitas",
                HeaderText = "Jumlah",
                DataPropertyName = "Kuantitas",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal (Rp)",
                DataPropertyName = "Subtotal",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Font = new Font("Segoe UI", 10F, FontStyle.Bold) }
            });

            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn
            {
                Name = "BtnHapus",
                HeaderText = "Aksi",
                Text = "❌ Hapus",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            btnHapus.DefaultCellStyle.BackColor = Color.LightCoral;
            btnHapus.DefaultCellStyle.ForeColor = Color.Black;
            dgvKeranjang.Columns.Add(btnHapus);
        }

        // --- MEMUAT DATA DARI CART MANAGER ---
        private void LoadDataKeranjang()
        {
            try
            {
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Nama", typeof(string));
                dtMock.Columns.Add("Harga", typeof(decimal));
                dtMock.Columns.Add("Kuantitas", typeof(int));
                dtMock.Columns.Add("Subtotal", typeof(decimal), "Harga * Kuantitas");

                dtMock.Rows.Add(1, "Danus Makaroni HMTI", 5000, 2);
                dtMock.Rows.Add(2, "Kemeja PDH Custom", 120000, 1);

                dgvKeranjang.DataSource = dtMock;

                HitungTotalPembayaran();

                bool adaBarang = dgvKeranjang.Rows.Count > 0;
                btnCheckout.Enabled = adaBarang;
                btnCheckout.BackColor = adaBarang ? Color.FromArgb(36, 0, 70) : Color.Gray;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat keranjang: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HitungTotalPembayaran()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvKeranjang.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
            }
            lblTotalHarga.Text = $"Rp {total:N0}";
        }

        private void dgvKeranjang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKeranjang.Columns[e.ColumnIndex].Name == "BtnHapus")
            {
                string namaItem = dgvKeranjang.Rows[e.RowIndex].Cells["NamaItem"].Value.ToString();

                DialogResult dr = MessageBox.Show($"Keluarkan '{namaItem}' dari keranjang?",
                                                  "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    dgvKeranjang.Rows.RemoveAt(e.RowIndex);
                    HitungTotalPembayaran();

                    if (dgvKeranjang.Rows.Count == 0)
                    {
                        btnCheckout.Enabled = false;
                        btnCheckout.BackColor = Color.Gray;
                    }
                }
            }
        }

        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            if (dgvKeranjang.Rows.Count == 0) return;

            DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin mengosongkan keranjang belanja?",
                                              "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                dgvKeranjang.DataSource = null;
                HitungTotalPembayaran();
                btnCheckout.Enabled = false;
                btnCheckout.BackColor = Color.Gray;
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Proses pembayaran untuk pesanan di keranjang Anda?\nPesanan akan diteruskan ke Penjual.",
                                              "Konfirmasi Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    // PERBAIKAN: Panggil TransactionController yang sudah diinisialisasi dengan idPembeli
                    var (sukses, pesan) = _transactionController.ProsesCheckout();

                    if (sukses)
                    {
                        MessageBox.Show("Checkout Berhasil! 🎉\nSilakan cek tab 'Riwayat Pesanan' untuk melihat status pesanan Anda.",
                                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvKeranjang.DataSource = null;
                        HitungTotalPembayaran();
                        btnCheckout.Enabled = false;
                        btnCheckout.BackColor = Color.Gray;
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Gagal Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
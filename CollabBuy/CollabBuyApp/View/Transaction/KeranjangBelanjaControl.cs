using CollabBuy.CollabBuyApp.Controllers;
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
            _transactionController = new TransactionController();
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
                DataPropertyName = "Id", // ID Produk/PO
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

            // Kolom Tombol Hapus
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
                // TODO: Ambil items dari CartManager
                // var cartItems = CartManager.Instance.GetItems();

                // --- MOCK DATA --- (Hapus ini jika CartManager sudah diimplementasi penuh)
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Nama", typeof(string));
                dtMock.Columns.Add("Harga", typeof(decimal));
                dtMock.Columns.Add("Kuantitas", typeof(int));
                dtMock.Columns.Add("Subtotal", typeof(decimal), "Harga * Kuantitas"); // Computed Column

                dtMock.Rows.Add(1, "Danus Makaroni HMTI", 5000, 2);
                dtMock.Rows.Add(2, "Kemeja PDH Custom", 120000, 1);

                dgvKeranjang.DataSource = dtMock;
                // -----------------

                HitungTotalPembayaran();

                // Atur state tombol checkout
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

        // --- AKSI TABEL (MENGHAPUS ITEM) ---
        private void dgvKeranjang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKeranjang.Columns[e.ColumnIndex].Name == "BtnHapus")
            {
                string namaItem = dgvKeranjang.Rows[e.RowIndex].Cells["NamaItem"].Value.ToString();
                int idItem = Convert.ToInt32(dgvKeranjang.Rows[e.RowIndex].Cells["IdItem"].Value);

                DialogResult dr = MessageBox.Show($"Keluarkan '{namaItem}' dari keranjang?",
                                                  "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    // TODO: Panggil CartManager.Instance.RemoveItem(idItem);

                    // Karena ini pakai Mock DataTable, kita hapus row-nya langsung dari Grid untuk visual
                    dgvKeranjang.Rows.RemoveAt(e.RowIndex);
                    HitungTotalPembayaran();

                    // Cek jika kosong
                    if (dgvKeranjang.Rows.Count == 0)
                    {
                        btnCheckout.Enabled = false;
                        btnCheckout.BackColor = Color.Gray;
                    }
                }
            }
        }

        // --- AKSI KOSONGKAN KERANJANG ---
        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            if (dgvKeranjang.Rows.Count == 0) return;

            DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin mengosongkan keranjang belanja?",
                                              "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                // TODO: Panggil CartManager.Instance.Clear();

                dgvKeranjang.DataSource = null; // Mock clear
                HitungTotalPembayaran();
                btnCheckout.Enabled = false;
                btnCheckout.BackColor = Color.Gray;
            }
        }

        // --- AKSI CHECKOUT ---
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Proses pembayaran untuk pesanan di keranjang Anda?\nPesanan akan diteruskan ke Penjual.",
                                              "Konfirmasi Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    // TODO: Ambil data keranjang, lalu buat instance Transaction
                    // Panggil _transactionController.CreateTransaction(newTransactionData);

                    // MOCK SUCCESS
                    bool sukses = true;

                    if (sukses)
                    {
                        MessageBox.Show("Checkout Berhasil! 🎉\nSilakan cek tab 'Riwayat Pesanan' untuk melihat status pesanan Anda.",
                                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kosongkan keranjang setelah checkout sukses
                        // CartManager.Instance.Clear();
                        dgvKeranjang.DataSource = null;
                        HitungTotalPembayaran();
                        btnCheckout.Enabled = false;
                        btnCheckout.BackColor = Color.Gray;
                    }
                    else
                    {
                        MessageBox.Show("Gagal melakukan checkout. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

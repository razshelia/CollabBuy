using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class PesananMasukControl : UserControl
    {
        private readonly User _currentSeller;
        private readonly TransactionController _transactionController;

        public PesananMasukControl(User seller)
        {
            InitializeComponent();
            _currentSeller = seller;
            _transactionController = new TransactionController();
        }

        private void PesananMasukControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataPesanan();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataPesanan();
        }

        // --- KONFIGURASI KOLOM TABEL ---
        private void SetupDataGridView()
        {
            dgvPesanan.AutoGenerateColumns = false;
            dgvPesanan.Columns.Clear();

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdTransaction",
                DataPropertyName = "Id",
                Visible = false // Sembunyikan ID
            });

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tgl Order",
                DataPropertyName = "Tanggal",
                Width = 120
            });

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pembeli",
                HeaderText = "Nama Pembeli",
                DataPropertyName = "NamaPembeli",
                Width = 150
            });

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Produk",
                HeaderText = "Barang / Sesi PO",
                DataPropertyName = "NamaProduk",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Qty",
                HeaderText = "Qty",
                DataPropertyName = "Kuantitas",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total (Rp)",
                DataPropertyName = "TotalHarga",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Font = new Font("Segoe UI", 9.75F, FontStyle.Bold) }
            });

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status Saat Ini",
                DataPropertyName = "Status",
                Width = 120
            });

            // Kolom Tombol Update Status
            DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn
            {
                Name = "BtnUpdate",
                HeaderText = "Aksi",
                Text = "🚀 Update Status",
                UseColumnTextForButtonValue = true,
                Width = 130,
                FlatStyle = FlatStyle.Flat
            };
            btnUpdate.DefaultCellStyle.BackColor = Color.FromArgb(36, 0, 70);
            btnUpdate.DefaultCellStyle.ForeColor = Color.FromArgb(253, 255, 182);
            dgvPesanan.Columns.Add(btnUpdate);
        }

        // --- MEMUAT DATA DARI CONTROLLER ---
        private void LoadDataPesanan()
        {
            try
            {
                // TODO: Panggil method dari TransactionController khusus untuk pesanan milik toko ini
                // var daftarPesanan = _transactionController.GetIncomingOrdersBySeller(_currentSeller.IdUser);

                // --- MOCK DATA --- (Hapus jika sudah dikoneksikan ke Database)
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Tanggal", typeof(string));
                dtMock.Columns.Add("NamaPembeli", typeof(string));
                dtMock.Columns.Add("NamaProduk", typeof(string));
                dtMock.Columns.Add("Kuantitas", typeof(int));
                dtMock.Columns.Add("TotalHarga", typeof(decimal));
                dtMock.Columns.Add("Status", typeof(string));

                dtMock.Rows.Add(201, "16 Nov", "Budi Santoso", "Danus Makaroni HMTI", 2, 10000, "Pending");
                dtMock.Rows.Add(202, "16 Nov", "Siti Aminah", "Gantungan Kunci Custom", 1, 15000, "Diproses");
                dtMock.Rows.Add(203, "15 Nov", "Andi Wijaya", "Keripik Kaca Original", 5, 30000, "Dikirim"); // Sudah dikirim

                dgvPesanan.DataSource = dtMock;
                // -----------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat pesanan masuk:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENT EVENT KLIK TOMBOL UPDATE STATUS ---
        private void dgvPesanan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPesanan.Columns[e.ColumnIndex].Name == "BtnUpdate")
            {
                int transactionId = Convert.ToInt32(dgvPesanan.Rows[e.RowIndex].Cells["IdTransaction"].Value);
                string statusSaatIni = dgvPesanan.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                string namaPembeli = dgvPesanan.Rows[e.RowIndex].Cells["Pembeli"].Value.ToString();

                string statusBaru = "";
                string pesanKonfirmasi = "";

                // Logika Alur Status Transaksi (State Machine sederhana)
                if (statusSaatIni.ToLower() == "pending")
                {
                    statusBaru = "Diproses";
                    pesanKonfirmasi = $"Terima dan mulai proses pesanan dari '{namaPembeli}'?";
                }
                else if (statusSaatIni.ToLower() == "diproses")
                {
                    statusBaru = "Dikirim"; // Atau "Siap Diambil"
                    pesanKonfirmasi = $"Tandai pesanan '{namaPembeli}' sebagai 'Dikirim / Siap Diambil'?";
                }
                else if (statusSaatIni.ToLower() == "dikirim" || statusSaatIni.ToLower() == "siap diambil")
                {
                    MessageBox.Show("Pesanan sudah dikirim. Menunggu pembeli menekan tombol 'Diterima'.",
                                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (statusSaatIni.ToLower() == "selesai")
                {
                    MessageBox.Show("Pesanan ini sudah selesai.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show($"Status '{statusSaatIni}' tidak dikenali atau tidak dapat diubah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tampilkan dialog konfirmasi
                DialogResult dr = MessageBox.Show(pesanKonfirmasi, "Update Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    ProsesUpdateStatus(transactionId, statusBaru);
                }
            }
        }

        private void ProsesUpdateStatus(int transactionId, string statusBaru)
        {
            try
            {
                // TODO: Panggil method update status transaksi di TransactionController
                // bool sukses = _transactionController.UpdateTransactionStatus(transactionId, statusBaru);

                bool sukses = true; // Mock sukses

                if (sukses)
                {
                    MessageBox.Show($"Status pesanan berhasil diubah menjadi: {statusBaru}!",
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPesanan(); // Refresh tabel setelah status berubah
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate status pesanan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

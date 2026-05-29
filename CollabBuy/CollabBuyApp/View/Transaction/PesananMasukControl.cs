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

            // PERBAIKAN: View ini hanya membaca data (query) tanpa mengelola keranjang,
            // sehingga cukup pakai konstruktor default TransactionController().
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

        private void SetupDataGridView()
        {
            dgvPesanan.AutoGenerateColumns = false;
            dgvPesanan.Columns.Clear();

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTransaction", DataPropertyName = "Id", Visible = false });

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

        private void LoadDataPesanan()
        {
            try
            {
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Tanggal", typeof(string));
                dtMock.Columns.Add("NamaPembeli", typeof(string));
                dtMock.Columns.Add("NamaProduk", typeof(string));
                dtMock.Columns.Add("Kuantitas", typeof(int));
                dtMock.Columns.Add("TotalHarga", typeof(decimal));
                dtMock.Columns.Add("Status", typeof(string));

                dtMock.Rows.Add(201, "16 Nov", "Budi Santoso", "Danus Makaroni HMTI", 2, 10000, "Menunggu");
                dtMock.Rows.Add(202, "16 Nov", "Siti Aminah", "Gantungan Kunci Custom", 1, 15000, "Diproses");

                dgvPesanan.DataSource = dtMock;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat pesanan masuk:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                if (statusSaatIni == "Menunggu")
                {
                    statusBaru = "Diproses";
                    pesanKonfirmasi = $"Terima dan mulai proses pesanan dari '{namaPembeli}'?";
                }
                else if (statusSaatIni == "Diproses")
                {
                    statusBaru = "Selesai";
                    pesanKonfirmasi = $"Tandai pesanan '{namaPembeli}' sebagai 'Selesai'?";
                }
                else
                {
                    MessageBox.Show("Pesanan ini sudah selesai atau dibatalkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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
                // PERBAIKAN: Panggil TransactionController yang sudah diperbaiki
                var (sukses, pesan) = _transactionController.UbahStatusPesanan(transactionId, statusBaru);

                if (sukses)
                {
                    MessageBox.Show($"Status pesanan berhasil diubah menjadi: {statusBaru}!",
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPesanan();
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
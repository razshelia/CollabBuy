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
    public partial class RiwayatPesananControl : UserControl
    {
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;

        public RiwayatPesananControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // PERBAIKAN: View ini hanya membaca riwayat (query), bukan mengelola keranjang.
            // Cukup gunakan konstruktor default TransactionController().
            _transactionController = new TransactionController();
        }

        private void RiwayatPesananControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataRiwayat();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataRiwayat();
        }

        private void SetupDataGridView()
        {
            dgvRiwayat.AutoGenerateColumns = false;
            dgvRiwayat.Columns.Clear();

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTransaction", DataPropertyName = "Id", Visible = false });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tanggal Transaksi",
                DataPropertyName = "Tanggal",
                Width = 150
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total Belanja",
                DataPropertyName = "TotalHarga",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Font = new Font("Segoe UI", 9.75F, FontStyle.Bold) }
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status Pesanan",
                DataPropertyName = "Status",
                Width = 150
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Catatan",
                HeaderText = "Keterangan",
                DataPropertyName = "Catatan",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            DataGridViewButtonColumn btnAksi = new DataGridViewButtonColumn
            {
                Name = "BtnSelesai",
                HeaderText = "Konfirmasi",
                Text = "✔ Diterima",
                UseColumnTextForButtonValue = true,
                Width = 120,
                FlatStyle = FlatStyle.Flat
            };
            btnAksi.DefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255);
            btnAksi.DefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvRiwayat.Columns.Add(btnAksi);
        }

        private void LoadDataRiwayat()
        {
            try
            {
                if (_currentUser == null) return;

                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Tanggal", typeof(string));
                dtMock.Columns.Add("TotalHarga", typeof(decimal));
                dtMock.Columns.Add("Status", typeof(string));
                dtMock.Columns.Add("Catatan", typeof(string));

                dtMock.Rows.Add(101, "20 May 2026", 10000, "Diproses", "Titipan Makaroni HMTI");
                dtMock.Rows.Add(102, "18 May 2026", 120000, "Selesai", "PDH Angkatan selesai diambil");
                dtMock.Rows.Add(103, "25 May 2026", 6000, "Menunggu", "Menunggu konfirmasi pembayaran");

                dgvRiwayat.DataSource = dtMock;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat pesanan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvRiwayat.Columns[e.ColumnIndex].Name == "BtnSelesai")
            {
                string statusSaatIni = dgvRiwayat.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                if (statusSaatIni != "Diproses" && statusSaatIni != "Dikirim")
                {
                    MessageBox.Show("Konfirmasi hanya dapat dilakukan jika pesanan dalam status 'Diproses' atau 'Dikirim'.",
                                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int transactionId = Convert.ToInt32(dgvRiwayat.Rows[e.RowIndex].Cells["IdTransaction"].Value);
                DialogResult dr = MessageBox.Show("Apakah Anda menyatakan bahwa pesanan ini sudah Anda terima dengan baik?",
                                                  "Konfirmasi Penerimaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ProsesKonfirmasiDiterima(transactionId);
                }
            }
        }

        private void ProsesKonfirmasiDiterima(int transactionId)
        {
            try
            {
                var (sukses, pesan) = _transactionController.UbahStatusPesanan(transactionId, "Selesai");

                if (sukses)
                {
                    MessageBox.Show("Terima kasih! Transaksi selesai ditandai.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataRiwayat();
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
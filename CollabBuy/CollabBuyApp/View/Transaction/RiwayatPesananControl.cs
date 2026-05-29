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

        // --- KONFIGURASI KOLOM TABEL ---
        private void SetupDataGridView()
        {
            dgvRiwayat.AutoGenerateColumns = false;
            dgvRiwayat.Columns.Clear();

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdTransaction",
                DataPropertyName = "Id", // Sesuaikan nama property ID model di DB
                Visible = false
            });

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

            // Kolom Tombol Konfirmasi Diterima
            DataGridViewButtonColumn btnAksi = new DataGridViewButtonColumn
            {
                Name = "BtnSelesai",
                HeaderText = "Konfirmasi",
                Text = "✔ Diterima",
                UseColumnTextForButtonValue = true,
                Width = 120,
                FlatStyle = FlatStyle.Flat
            };
            btnAksi.DefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255); // Ungu pastel
            btnAksi.DefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvRiwayat.Columns.Add(btnAksi);
        }

        // --- MEMUAT DATA DARI CONTROLLER ---
        private void LoadDataRiwayat()
        {
            try
            {
                if (_currentUser == null) return;

                // TODO: Panggil method di TransactionController untuk mengambil riwayat berdasarkan Id User
                // List<Models.Transaction> listHistory = _transactionController.GetTransactionHistoryByUser(_currentUser.IdUser);

                // --- MOCK DATA --- (Hapus jika repository terikat penuh)
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Tanggal", typeof(string));
                dtMock.Columns.Add("TotalHarga", typeof(decimal));
                dtMock.Columns.Add("Status", typeof(string));
                dtMock.Columns.Add("Catatan", typeof(string));

                dtMock.Rows.Add(101, "20 May 2026", 10000, "Dikirim", "Titipan Makaroni HMTI sedang di selasar");
                dtMock.Rows.Add(102, "18 May 2026", 120000, "Selesai", "PDH Angkatan selesai diambil");
                dtMock.Rows.Add(103, "25 May 2026", 6000, "Pending", "Menunggu konfirmasi pembayaran lapak");

                dgvRiwayat.DataSource = dtMock;
                // -----------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat pesanan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENT EVENT KLIK TOMBOL DITERIMA ---
        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvRiwayat.Columns[e.ColumnIndex].Name == "BtnSelesai")
            {
                string statusSaatIni = dgvRiwayat.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                // Validasi: Hanya pesanan dengan status 'Dikirim' atau 'Siap Diambil' yang bisa dikonfirmasi
                if (statusSaatIni.ToLower() != "dikirim" && statusSaatIni.ToLower() != "siap diambil")
                {
                    MessageBox.Show("Konfirmasi hanya dapat dilakukan jika barang dalam status 'Dikirim'.",
                                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int transactionId = Convert.ToInt32(dgvRiwayat.Rows[e.RowIndex].Cells["IdTransaction"].Value);

                DialogResult dr = MessageBox.Show("Apakah Anda menyatakan bahwa pesanan/barang ini sudah Anda terima dengan baik?",
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
                // TODO: Panggil method update status transaksi di TransactionController
                // bool sukses = _transactionController.UpdateTransactionStatus(transactionId, "Selesai");

                bool sukses = true; // Mock sukses

                if (sukses)
                {
                    MessageBox.Show("Terima kasih! Transaksi selesai ditandai.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataRiwayat(); // Refresh data
                }
                else
                {
                    MessageBox.Show("Gagal mengonfirmasi status transaksi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

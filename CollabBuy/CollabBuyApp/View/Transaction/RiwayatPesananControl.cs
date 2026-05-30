using System;
using System.Data;
using System.Drawing;
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

            // Inisialisasi controller
            _transactionController = new TransactionController(_currentUser.GetIdUser());
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

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTrx", DataPropertyName = "id_transaksi", Visible = false });
            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Penjual", HeaderText = "Nama Lapak/Penjual", DataPropertyName = "nama_penjual", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Waktu Pemesanan", DataPropertyName = "tanggal_pesanan_format", Width = 180 });
            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total Harga", DataPropertyName = "total_harga_format", Width = 150 });
            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Pesanan", DataPropertyName = "status_pesanan", Width = 150 });
        }

        private void LoadDataRiwayat()
        {
            try
            {
                // Tarik data mentah dari database via Controller
                DataTable dtRaw = _transactionController.GetRiwayatPesanan(_currentUser.GetIdUser());

                // Bikin tabel baru khusus buat nampilin format yang cantik di UI
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_transaksi", typeof(int));
                dtUI.Columns.Add("nama_penjual", typeof(string));
                dtUI.Columns.Add("tanggal_pesanan_format", typeof(string));
                dtUI.Columns.Add("total_harga_format", typeof(string));
                dtUI.Columns.Add("status_pesanan", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    // Diubah dari tanggal_pesanan jadi tanggal_transaksi
                    string tanggal = Convert.ToDateTime(row["tanggal_transaksi"]).ToString("dd MMM yyyy, HH:mm");
                    // Diubah dari total_harga jadi total_tagihan
                    string harga = "Rp " + Convert.ToInt32(row["total_tagihan"]).ToString("N0");

                    dtUI.Rows.Add(
                        row["id_transaksi"],
                        "Pesanan Kolektif", // 1 Transaksi bisa berisi dari banyak lapak penjual
                        tanggal,
                        harga,
                        row["status_pesanan"]
                    );
                }

                dgvRiwayat.DataSource = dtUI;
                dgvRiwayat.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data riwayat: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
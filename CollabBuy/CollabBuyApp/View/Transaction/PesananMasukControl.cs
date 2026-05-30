using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class PesananMasukControl : UserControl
    {
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;

        public PesananMasukControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // Inisialisasi controller
            _transactionController = new TransactionController(_currentUser.GetIdUser());
            this.Resize += (s, e) => AdjustLayout();
        }

        private void PesananMasukControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            SetupDataGridView();
            LoadDataPesanan();
        }

        private void SetupDataGridView()
        {
            dgvPesanan.AutoGenerateColumns = false;
            dgvPesanan.Columns.Clear();

            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTrx", DataPropertyName = "id_transaksi", Visible = false });
            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pembeli", HeaderText = "Nama Pembeli", DataPropertyName = "nama_pembeli", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Tanggal Order", DataPropertyName = "tanggal_transaksi", Width = 150 });
            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total Harga (Rp)", DataPropertyName = "total_harga_lapak", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Saat Ini", DataPropertyName = "status_pesanan", Width = 150 });
        }

        private void LoadDataPesanan()
        {
            try
            {
                // Tarik data asli dari database
                DataTable dt = _transactionController.GetPesananMasuk(_currentUser.GetIdUser());
                dgvPesanan.DataSource = dt;
                dgvPesanan.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data pesanan: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method Helper buat Update Status
        private void ProsesUbahStatus(string statusBaru, string pertanyaan)
        {
            if (dgvPesanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih dulu pesanan mana yang mau di-update bestie!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTrx = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["IdTrx"].Value);
            string namaPembeli = dgvPesanan.SelectedRows[0].Cells["Pembeli"].Value.ToString();
            string statusLama = dgvPesanan.SelectedRows[0].Cells["Status"].Value.ToString();

            // Cegah ubah status kalau udah kelar atau batal
            if (statusLama == "Selesai" || statusLama == "Dibatalkan")
            {
                MessageBox.Show($"Pesanan ini udah '{statusLama}', nggak bisa diubah lagi ya.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show(pertanyaan + $"\n(Pembeli: {namaPembeli})", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                var (sukses, pesan) = _transactionController.UbahStatusPesanan(idTrx, statusBaru);

                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPesanan(); // Refresh Grid
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            ProsesUbahStatus("Diproses", "Yakin mau mulai proses pesanan ini?");
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            ProsesUbahStatus("Selesai", "Udah kelar diproses dan barangnya udah dikasih ke pembeli kan?");
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            ProsesUbahStatus("Dibatalkan", "Yakin banget nih mau ngebatalin pesanan orang? 🥺");
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            pnlCard.Width = w;
            dgvPesanan.Width = pnlCard.Width - 68;
            btnProses.Left = pnlCard.Width - btnProses.Width - 34;
            btnSelesai.Left = btnProses.Left - btnSelesai.Width - 10;
        }
    }
}
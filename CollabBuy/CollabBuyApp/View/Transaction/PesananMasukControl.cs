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
        private ToolTip _gridTooltip = new ToolTip();

        /// <summary>
        /// Event yang dipanggil ketika penjual mau lihat detail pesanan.
        /// Parameter: idTransaksi, dtDetail — diterima oleh MainForm untuk load DetailPesananControl.
        /// </summary>
        public event Action<int, DataTable> OnNavigateDetail;

        public PesananMasukControl(User currentUser)
        {
            InitializeComponent();
            this._currentUser = currentUser;

            // OOP BEST PRACTICE: Gunakan konstruktor default karena ini layar penjual (bukan isi keranjang pembeli)
            this._transactionController = new TransactionController();
            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void PesananMasukControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataPesanan();
        }

        private void SetupDataGridView()
        {
            this.dgvPesanan.AutoGenerateColumns = false;
            this.dgvPesanan.Columns.Clear();

            this.dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdTrx",
                DataPropertyName = "id_transaksi",
                Visible = false
            });
            this.dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pembeli",
                HeaderText = "Nama Pembeli",
                DataPropertyName = "nama_pembeli",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tanggal Order",
                DataPropertyName = "tanggal_transaksi",
                Width = 150
            });
            this.dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total Harga (Rp)",
                DataPropertyName = "total_harga_lapak",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });
            this.dgvPesanan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status Saat Ini",
                DataPropertyName = "status_pesanan",
                Width = 150
            });
            _gridTooltip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ShowAlways = true };
            this.dgvPesanan.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (this.dgvPesanan.Columns[e.ColumnIndex].Name != "Pembeli") return;
                string teks = this.dgvPesanan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                if (teks.Length > 25)
                    _gridTooltip.Show(teks, this.dgvPesanan,
                        this.dgvPesanan.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location, 5000);
            };
            this.dgvPesanan.CellMouseLeave += (s, e) => _gridTooltip.Hide(this.dgvPesanan);
        }

        private void LoadDataPesanan()
        {
            try
            {
                DataTable dt = this._transactionController.GetPesananMasuk(this._currentUser.IdUser);
                this.dgvPesanan.DataSource = dt;
                this.dgvPesanan.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data pesanan: " + ex.Message, "Waduh Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProsesUbahStatus(string statusBaru, string pertanyaan)
        {
            if (this.dgvPesanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih dulu pesanan mana yang mau di-update bestie!", "Oops",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTrx = Convert.ToInt32(this.dgvPesanan.SelectedRows[0].Cells["IdTrx"].Value);
            string namaPembeli = this.dgvPesanan.SelectedRows[0].Cells["Pembeli"].Value.ToString();
            string statusLama = this.dgvPesanan.SelectedRows[0].Cells["Status"].Value.ToString();

            // Validasi transisi berdasarkan aturan bisnis di Model
            // Validasi transisi berdasarkan aturan bisnis
            if (statusLama == "Selesai" || statusLama == "Dibatalkan")
            {
                MessageBox.Show($"Pesanan ini udah '{statusLama}', nggak bisa diubah lagi ya.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (statusLama == "Menunggu" && statusBaru == "Selesai")
            {
                MessageBox.Show(
                    "Hei! Pesanan yang masih 'Menunggu' harus diubah ke 'Diproses' dulu ya.\n\nAlurnya: Menunggu → Diproses → Selesai 😊",
                    "Alur Tidak Valid",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (statusLama == "Diproses" && statusBaru == "Dibatalkan")
            {
                // 🚫 ATURAN BISNIS: Pesanan yang sudah diproses TIDAK boleh dibatalkan oleh penjual
                // Pembeli sudah bayar, penjual sudah mulai garap — batalkan hanya bisa lewat mediasi admin
                MessageBox.Show(
                    "Pesanan yang sudah 'Diproses' tidak bisa dibatalkan begitu saja.\n\n" +
                    "Pembeli sudah melakukan pembayaran. Jika ada masalah, minta pembeli untuk menghubungi Admin. 🙏",
                    "Tidak Diizinkan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show(
                    pertanyaan + $"\n(Pembeli: {namaPembeli})",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    var (sukses, pesan) = this._transactionController.UbahStatusPesanan(idTrx, statusBaru);
                    if (sukses)
                    {
                        MessageBox.Show(pesan, "Sukses!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.LoadDataPesanan();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Gagal Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool dibatalkanAksi = true;
                }
            }
        }

        private void btnLihatDetail_Click(object sender, EventArgs e)
        {
            if (this.dgvPesanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih dulu pesanan yang mau dilihat detailnya!", "Oops",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTrx = Convert.ToInt32(this.dgvPesanan.SelectedRows[0].Cells["IdTrx"].Value);

            try
            {
                DataTable dtDetail = this._transactionController.GetDetailPesananPenjual(
                    idTrx, this._currentUser.IdUser);

                if (dtDetail == null || dtDetail.Rows.Count == 0)
                {
                    MessageBox.Show("Detail pesanan tidak ditemukan atau tidak ada produk milikmu di pesanan ini.",
                        "Data Kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Navigasi inline (bukan ShowDialog) agar bisa di-scroll di dalam pnlContent
                this.OnNavigateDetail?.Invoke(idTrx, dtDetail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka detail pesanan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            this.ProsesUbahStatus("Diproses", "Yakin mau mulai proses pesanan ini?");
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            this.ProsesUbahStatus("Selesai", "Udah kelar diproses dan barangnya udah dikasih ke pembeli kan?");
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.ProsesUbahStatus("Dibatalkan", "Yakin banget nih mau ngebatalin pesanan orang? 🥺");
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            this.pnlCard.Width = w;
            this.dgvPesanan.Width = this.pnlCard.Width - 68;
            this.btnProses.Left = this.pnlCard.Width - this.btnProses.Width - 34;
            this.btnSelesai.Left = this.btnProses.Left - this.btnSelesai.Width - 10;
            this.btnLihatDetail.Left = this.btnBatal.Left + this.btnBatal.Width + 10;
        }
    }
}
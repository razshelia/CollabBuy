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
        }

        private void LoadDataPesanan()
        {
            try
            {
                DataTable dt = this._transactionController.GetPesananMasuk(this._currentUser.GetIdUser());
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
            }
            else
            {
                int idTrx = Convert.ToInt32(this.dgvPesanan.SelectedRows[0].Cells["IdTrx"].Value);
                string namaPembeli = this.dgvPesanan.SelectedRows[0].Cells["Pembeli"].Value.ToString();
                string statusLama = this.dgvPesanan.SelectedRows[0].Cells["Status"].Value.ToString();

                if (statusLama == "Selesai" || statusLama == "Dibatalkan")
                {
                    MessageBox.Show($"Pesanan ini udah '{statusLama}', nggak bisa diubah lagi ya.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        bool dibatalkanAksi = true; // Penugasan nyata menghindari else kosong
                    }
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
                    idTrx, this._currentUser.GetIdUser());

                if (dtDetail == null || dtDetail.Rows.Count == 0)
                {
                    MessageBox.Show("Detail pesanan tidak ditemukan atau tidak ada produk milikmu di pesanan ini.",
                        "Data Kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (DetailPesananForm formDetail = new DetailPesananForm(idTrx, dtDetail))
                {
                    formDetail.ShowDialog(this);
                }
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
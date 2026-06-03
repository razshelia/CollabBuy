using System;
using System.Collections.Generic;
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
            this._currentUser = currentUser;

            // Inisialisasi controller khusus sesi pembeli yang sedang login
            this._transactionController = new TransactionController(this._currentUser.GetIdUser());

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void RiwayatPesananControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataRiwayat();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataRiwayat();
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);

            this.pnlCard.Width = w;
            this.pnlCard.Height = this.Height - this.pnlCard.Top - margin;

            this.dgvRiwayat.Width = this.pnlCard.Width - 68;
            this.dgvRiwayat.Height = this.pnlCard.Height - this.btnRefresh.Height - 70;

            this.btnRefresh.Left = this.pnlCard.Width - this.btnRefresh.Width - 34;
            this.btnRefresh.Top = this.pnlCard.Height - this.btnRefresh.Height - 20;
        }

        private void SetupDataGridView()
        {
            this.dgvRiwayat.AutoGenerateColumns = false;
            this.dgvRiwayat.Columns.Clear();

            // Memperbaiki penamaan dan binding kolom agar lebih masuk akal
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTrx", DataPropertyName = "id_transaksi", Visible = false });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusBayar", HeaderText = "Status Pembayaran", DataPropertyName = "status_bayar", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Waktu Pemesanan", DataPropertyName = "tanggal_pesanan", Width = 180 });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total Harga", DataPropertyName = "total_harga", Width = 180 });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Pesanan", DataPropertyName = "status_pesanan", Width = 150 });
        }

        private void LoadDataRiwayat()
        {
            try
            {
                // OOP BEST PRACTICE: Ambil List Objek, BUKAN DataTable mentah!
                List<Models.Transaction> listTrx = this._transactionController.GetTransaksiByPembeli(this._currentUser.GetIdUser());

                // Bikin tabel baru khusus buat binding ke UI
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_transaksi", typeof(int));
                dtUI.Columns.Add("status_bayar", typeof(string));
                dtUI.Columns.Add("tanggal_pesanan", typeof(string));
                dtUI.Columns.Add("total_harga", typeof(string));
                dtUI.Columns.Add("status_pesanan", typeof(string));

                if (listTrx != null)
                {
                    foreach (Models.Transaction trx in listTrx)
                    {
                        // Memanfaatkan Behavior / Method UI dari kelas Transaction
                        string waktuFormat = trx.TanggalTransaksi.ToString("dd MMM yyyy, HH:mm");
                        string hargaFormat = trx.DapatkanFormatTagihanUI();
                        string statusBayar = trx.DapatkanStatusPembayaranUI();
                        string statusTrx = trx.GetStatus();

                        dtUI.Rows.Add(
                            trx.IdTransaksi,
                            statusBayar,
                            waktuFormat,
                            hargaFormat,
                            statusTrx
                        );
                    }
                }
                else
                {
                    bool listKosong = true; // Assignment nyata menghindari else kosong
                }

                this.dgvRiwayat.DataSource = dtUI;
                this.dgvRiwayat.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal narik data riwayat: " + ex.Message, "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
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

            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTrx", DataPropertyName = "id_transaksi", Visible = false });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "NoInvoice", HeaderText = "No. Invoice", DataPropertyName = "no_invoice", Width = 120 });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Waktu Pemesanan", DataPropertyName = "tanggal_pesanan", Width = 175 });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "JumlahItem", HeaderText = "Jml Item", DataPropertyName = "jumlah_item", Width = 75, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total Tagihan", DataPropertyName = "total_harga", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cashback", HeaderText = "Cashback GR", DataPropertyName = "cashback", Width = 115, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Alignment = DataGridViewContentAlignment.MiddleRight } });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusBayar", HeaderText = "Status Pembayaran", DataPropertyName = "status_bayar", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Pesanan", DataPropertyName = "status_pesanan", Width = 120 });

            var btnDetailCol = new DataGridViewButtonColumn
            {
                Name = "BtnDetail",
                HeaderText = "",
                Text = "🔍 Detail",
                UseColumnTextForButtonValue = true,
                Width = 85,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(36, 0, 70),
                    ForeColor = Color.FromArgb(253, 255, 182),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            this.dgvRiwayat.Columns.Add(btnDetailCol);

            this.dgvRiwayat.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex != this.dgvRiwayat.Columns["BtnDetail"].Index) return;
                var row = ((DataTable)this.dgvRiwayat.DataSource).Rows[e.RowIndex];
                int idTrx = Convert.ToInt32(row["id_transaksi"]);
                DataTable dtDetail = this._transactionController.GetDetailPesananPembeli(idTrx);
                this.TampilkanDetailDanSplitBill(idTrx, dtDetail);
            };
        }

        private void LoadDataRiwayat()
        {
            try
            {
                List<Models.Transaction> listTrx = this._transactionController.GetTransaksiByPembeli(this._currentUser.GetIdUser());

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_transaksi", typeof(int));
                dtUI.Columns.Add("no_invoice", typeof(string));
                dtUI.Columns.Add("tanggal_pesanan", typeof(string));
                dtUI.Columns.Add("jumlah_item", typeof(string));
                dtUI.Columns.Add("total_harga", typeof(string));
                dtUI.Columns.Add("cashback", typeof(string));
                dtUI.Columns.Add("status_bayar", typeof(string));
                dtUI.Columns.Add("status_pesanan", typeof(string));

                if (listTrx != null && listTrx.Count > 0)
                {
                    foreach (Models.Transaction trx in listTrx)
                    {
                        long totalTagihan = trx.HitungTotal();
                        long totalCashback = trx.HitungDiskon();
                        long tagihBersih = totalTagihan - totalCashback;

                        dtUI.Rows.Add(
                            trx.IdTransaksi,
                            $"INV-{trx.IdTransaksi:D6}",
                            trx.TanggalTransaksi.ToString("dd MMM yyyy, HH:mm"),
                            trx.DapatkanTotalItem() > 0 ? $"{trx.DapatkanTotalItem()} pcs" : "-",
                            tagihBersih > 0 ? $"Rp {tagihBersih:N0}" : $"Rp {totalTagihan:N0}",
                            totalCashback > 0 ? $"Rp {totalCashback:N0} ✅" : "-",
                            trx.DapatkanStatusPembayaranUI(),
                            trx.GetStatus()
                        );
                    }
                }

                this.dgvRiwayat.DataSource = dtUI;
                this.dgvRiwayat.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data riwayat: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TampilkanDetailDanSplitBill(int idTrx, DataTable dtDetail)
        {
            Form frmDetail = new Form
            {
                Text = $"Detail & Split Bill — INV-{idTrx:D6}",
                Size = new Size(820, 620),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F)
            };

            // ── Header ──
            Label lblJudul = new Label
            {
                Text = $"📋 Rincian Pesanan INV-{idTrx:D6}",
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            frmDetail.Controls.Add(lblJudul);

            string statusTeks = dtDetail.Rows.Count > 0
                ? $"Status: {dtDetail.Rows[0]["status_pesanan"]}  |  {dtDetail.Rows[0]["tanggal_transaksi"]}"
                : "Tidak ada data";
            Label lblStatus = new Label
            {
                Text = statusTeks,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(90, 24, 154),
                AutoSize = true,
                Location = new Point(22, 45)
            };
            frmDetail.Controls.Add(lblStatus);

            // ── DataGridView rincian ──
            DataGridView dgv = new DataGridView
            {
                Location = new Point(15, 70),
                Size = new Size(775, 220),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Produk", DataPropertyName = "nama_produk", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", DataPropertyName = "nama_penitip", Width = 140 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = "jumlah", Width = 45, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Harga Satuan", DataPropertyName = "harga_satuan", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal (Rp)", DataPropertyName = "subtotal", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback", DataPropertyName = "cashback_str", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Font = new Font("Segoe UI", 9F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Catatan", DataPropertyName = "catatan", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // Buat DataTable untuk grid (tambah kolom cashback_str)
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("nama_produk", typeof(string));
            dtGrid.Columns.Add("nama_penitip", typeof(string));
            dtGrid.Columns.Add("jumlah", typeof(int));
            dtGrid.Columns.Add("harga_satuan", typeof(long));
            dtGrid.Columns.Add("subtotal", typeof(long));
            dtGrid.Columns.Add("cashback_str", typeof(string));
            dtGrid.Columns.Add("catatan", typeof(string));

            long grandTotal = 0;
            long totalCashback = 0;
            foreach (DataRow row in dtDetail.Rows)
            {
                long subtotal = Convert.ToInt64(row["subtotal"]);
                long cashback = Convert.ToInt64(row["selisih_refund"]);
                grandTotal += subtotal;
                totalCashback += cashback;

                dtGrid.Rows.Add(
                    row["nama_produk"].ToString(),
                    row["nama_penitip"].ToString(),
                    Convert.ToInt32(row["jumlah"]),
                    Convert.ToInt64(row["harga_satuan"]),
                    subtotal,
                    cashback > 0 ? $"Rp {cashback:N0} ✅" : "-",
                    row["catatan"].ToString()
                );
            }
            dgv.DataSource = dtGrid;
            frmDetail.Controls.Add(dgv);

            // ── Split Bill Section ──
            Label lblSplitJudul = new Label
            {
                Text = "💰 Split Bill per Penitip",
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Location = new Point(15, 305)
            };
            frmDetail.Controls.Add(lblSplitJudul);

            // Hitung split bill: grup per penitip, jumlahkan subtotal - cashback per penitip
            var splitDict = new System.Collections.Generic.Dictionary<string, long>();
            var cashbackDict = new System.Collections.Generic.Dictionary<string, long>();

            foreach (DataRow row in dtDetail.Rows)
            {
                string penitip = row["nama_penitip"].ToString();
                long subtotal = Convert.ToInt64(row["subtotal"]);
                long cashback = Convert.ToInt64(row["selisih_refund"]);

                if (!splitDict.ContainsKey(penitip)) { splitDict[penitip] = 0; cashbackDict[penitip] = 0; }
                splitDict[penitip] += subtotal;
                cashbackDict[penitip] += cashback;
            }

            DataGridView dgvSplit = new DataGridView
            {
                Location = new Point(15, 330),
                Size = new Size(560, 150),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.FromArgb(235, 204, 255),
                BorderStyle = BorderStyle.None
            };
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nama Penitip", Width = 160 });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Belanja (Rp)", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback (Rp)", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Yang Harus Bayar (Rp)", Width = 155, DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleRight } });

            foreach (var kv in splitDict)
            {
                string penitip = kv.Key;
                long totalBelanja = kv.Value;
                long cashback = cashbackDict[penitip];
                long harusBayar = totalBelanja - cashback;
                dgvSplit.Rows.Add(penitip, totalBelanja, cashback > 0 ? cashback : 0, harusBayar);
            }
            frmDetail.Controls.Add(dgvSplit);

            // ── Summary ──
            // ── Summary ──
            long tagihBersihTotal = grandTotal - totalCashback;
            Label lblTotal = new Label
            {
                Text = $"Grand Total: Rp {grandTotal:N0}" +
                       (totalCashback > 0
                           ? $"   |   Cashback GR: Rp {totalCashback:N0}   |   ✅ Bayar Bersih: Rp {tagihBersihTotal:N0}"
                           : ""),
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                AutoSize = true,
                Location = new Point(15, 490)
            };
            frmDetail.Controls.Add(lblTotal);

            // ── Tombol Salin Split Bill ──
            Button btnSalin = new Button
            {
                Text = "📋 Salin Split Bill ke Clipboard",
                Location = new Point(590, 340),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = Color.FromArgb(253, 255, 182),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSalin.FlatAppearance.BorderSize = 0;
            btnSalin.Click += (s, e) =>
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine($"💰 Split Bill — INV-{idTrx:D6}");
                sb.AppendLine(new string('─', 40));
                foreach (var kv in splitDict)
                {
                    long cb = cashbackDict[kv.Key];
                    long bayar = kv.Value - cb;
                    sb.Append($"• {kv.Key}: Rp {bayar:N0}");
                    if (cb > 0) sb.Append($" (hemat cashback Rp {cb:N0} 🎉)");
                    sb.AppendLine();
                }
                sb.AppendLine(new string('─', 40));
                sb.AppendLine($"Total: Rp {grandTotal:N0}");
                if (totalCashback > 0)
                    sb.AppendLine($"Total Cashback GR: Rp {totalCashback:N0}");
                Clipboard.SetText(sb.ToString());
                MessageBox.Show("Split bill berhasil disalin! Tinggal paste ke WA / chat grup.", "Berhasil ✅", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            frmDetail.Controls.Add(btnSalin);

            Button btnTutup = new Button
            {
                Text = "✖ Tutup",
                Location = new Point(590, 390),
                Size = new Size(200, 35),
                BackColor = Color.FromArgb(210, 210, 210),
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            btnTutup.FlatAppearance.BorderSize = 0;
            btnTutup.Click += (s, e) => frmDetail.Close();
            frmDetail.Controls.Add(btnTutup);

            frmDetail.ShowDialog();
        }
    }
}
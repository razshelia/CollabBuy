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
            // ── Hitung data split bill lebih dulu ──
            var splitDict = new System.Collections.Generic.Dictionary<string, long>();
            var cashbackDict = new System.Collections.Generic.Dictionary<string, long>();
            long grandTotal = 0;
            long totalCashback = 0;

            // Ambil bukti bayar dari baris pertama (sama untuk semua baris transaksi ini)
            byte[] buktiBayar = null;
            if (dtDetail.Columns.Contains("bukti_bayar") && dtDetail.Rows.Count > 0
                && dtDetail.Rows[0]["bukti_bayar"] != DBNull.Value)
                buktiBayar = (byte[])dtDetail.Rows[0]["bukti_bayar"];

            foreach (DataRow row in dtDetail.Rows)
            {
                long subtotal = Convert.ToInt64(row["subtotal"]);
                long cashback = Convert.ToInt64(row["selisih_refund"]);
                grandTotal += subtotal;
                totalCashback += cashback;

                string penitip = row["nama_penitip"].ToString();
                if (!splitDict.ContainsKey(penitip)) { splitDict[penitip] = 0; cashbackDict[penitip] = 0; }
                splitDict[penitip] += subtotal;
                cashbackDict[penitip] += cashback;
            }

            // ── Form utama ──
            Form frmDetail = new Form
            {
                Text = $"Detail & Split Bill — INV-{idTrx:D6}",
                MinimumSize = new Size(820, 600),
                Size = new Size(960, 740),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F)
            };

            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16, 12, 16, 12),
                BackColor = Color.White
            };
            frmDetail.Controls.Add(pnlMain);

            // ── TableLayoutPanel: 6 baris ──
            TableLayoutPanel tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 6,
                BackColor = Color.White
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));  // 0 — Header
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));   // 1 — dgv Rincian
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));  // 2 — Label Split Bill
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));   // 3 — dgv Split
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));   // 4 — Panel Bukti Bayar
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));  // 5 — Footer
            pnlMain.Controls.Add(tbl);

            // ── Baris 0: Header ──
            Panel pnlHeader = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            Label lblJudul = new Label
            {
                Text = $"📋 Rincian Pesanan INV-{idTrx:D6}",
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Location = new Point(2, 2)
            };
            string statusTeks = dtDetail.Rows.Count > 0
                ? $"Status: {dtDetail.Rows[0]["status_pesanan"]}  |  {dtDetail.Rows[0]["tanggal_transaksi"]}"
                : "Tidak ada data";
            Label lblStatus = new Label
            {
                Text = statusTeks,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(90, 24, 154),
                AutoSize = true,
                Location = new Point(4, 36)
            };
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Controls.Add(lblStatus);
            tbl.Controls.Add(pnlHeader, 0, 0);

            // ── Baris 1: DataGridView Rincian ──
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 204, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Produk", DataPropertyName = "nama_produk", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", DataPropertyName = "nama_penitip", Width = 140 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = "jumlah", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Harga Satuan", DataPropertyName = "harga_satuan", Width = 115, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal (Rp)", DataPropertyName = "subtotal", Width = 125, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback", DataPropertyName = "cashback_str", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Font = new Font("Segoe UI", 9F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Catatan", DataPropertyName = "catatan", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 80 });

            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("nama_produk", typeof(string));
            dtGrid.Columns.Add("nama_penitip", typeof(string));
            dtGrid.Columns.Add("jumlah", typeof(int));
            dtGrid.Columns.Add("harga_satuan", typeof(long));
            dtGrid.Columns.Add("subtotal", typeof(long));
            dtGrid.Columns.Add("cashback_str", typeof(string));
            dtGrid.Columns.Add("catatan", typeof(string));

            foreach (DataRow row in dtDetail.Rows)
            {
                long subtotal = Convert.ToInt64(row["subtotal"]);
                long cashback = Convert.ToInt64(row["selisih_refund"]);
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
            tbl.Controls.Add(dgv, 0, 1);

            // ── Baris 2: Label Split Bill ──
            Label lblSplitJudul = new Label
            {
                Text = "💰 Split Bill per Penitip",
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(0, 0, 0, 2)
            };
            tbl.Controls.Add(lblSplitJudul, 0, 2);

            // ── Baris 3: DataGridView Split Bill ──
            DataGridView dgvSplit = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.FromArgb(245, 232, 255),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            };
            dgvSplit.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 160, 240);
            dgvSplit.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvSplit.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSplit.EnableHeadersVisualStyles = false;
            dgvSplit.DefaultCellStyle.BackColor = Color.FromArgb(245, 232, 255);

            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nama Penitip", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 140 });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Belanja (Rp)", Width = 160, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback (Rp)", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Yang Harus Bayar (Rp)", Width = 170, DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleRight } });

            foreach (var kv in splitDict)
            {
                long totalBelanja = kv.Value;
                long cashback = cashbackDict[kv.Key];
                dgvSplit.Rows.Add(kv.Key, totalBelanja, cashback > 0 ? cashback : 0, totalBelanja - cashback);
            }
            tbl.Controls.Add(dgvSplit, 0, 3);

            // ── Baris 4: Panel Bukti Bayar ──
            TableLayoutPanel pnlBukti = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0, 6, 0, 0)
            };
            pnlBukti.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F)); // kolom label
            pnlBukti.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // kolom gambar

            Label lblBuktiJudul = new Label
            {
                Text = "🧾 Bukti Bayar",
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 4)
            };
            pnlBukti.Controls.Add(lblBuktiJudul, 0, 0);

            if (buktiBayar != null && buktiBayar.Length > 10)
            {
                try
                {
                    using (var ms = new System.IO.MemoryStream(buktiBayar))
                    {
                        Image img = Image.FromStream(ms);

                        PictureBox pbBukti = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = img,
                            BorderStyle = BorderStyle.FixedSingle,
                            Cursor = Cursors.Hand
                        };

                        // Klik foto → buka di form besar (zoom)
                        pbBukti.Click += (s, e) =>
                        {
                            Form frmZoom = new Form
                            {
                                Text = $"Bukti Bayar — INV-{idTrx:D6}",
                                Size = new Size(800, 700),
                                StartPosition = FormStartPosition.CenterParent,
                                BackColor = Color.Black
                            };
                            PictureBox pbZoom = new PictureBox
                            {
                                Dock = DockStyle.Fill,
                                SizeMode = PictureBoxSizeMode.Zoom,
                                Image = img
                            };
                            frmZoom.Controls.Add(pbZoom);
                            frmZoom.ShowDialog();
                        };

                        pnlBukti.Controls.Add(pbBukti, 1, 0);
                    }
                }
                catch
                {
                    // Data bukan gambar valid — tampilkan placeholder
                    Label lblNoBukti = new Label
                    {
                        Text = "⚠️ File bukti tidak dapat ditampilkan.",
                        ForeColor = Color.FromArgb(150, 0, 0),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                    };
                    pnlBukti.Controls.Add(lblNoBukti, 1, 0);
                }
            }
            else
            {
                Panel pnlNoBukti = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(245, 245, 245),
                    BorderStyle = BorderStyle.FixedSingle
                };
                Label lblNoBukti = new Label
                {
                    Text = "Belum ada bukti pembayaran yang diupload.",
                    ForeColor = Color.FromArgb(130, 130, 130),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                };
                pnlNoBukti.Controls.Add(lblNoBukti);
                pnlBukti.Controls.Add(pnlNoBukti, 1, 0);
            }

            tbl.Controls.Add(pnlBukti, 0, 4);

            // ── Baris 5: Footer ──
            TableLayoutPanel pnlFooter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0, 6, 0, 0)
            };
            pnlFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 290F));

            long tagihBersihTotal = grandTotal - totalCashback;
            Label lblTotal = new Label
            {
                Text = $"Grand Total: Rp {grandTotal:N0}" +
                       (totalCashback > 0
                           ? $"   |   Cashback GR: Rp {totalCashback:N0}   |   ✅ Bayar Bersih: Rp {tagihBersihTotal:N0}"
                           : ""),
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlFooter.Controls.Add(lblTotal, 0, 0);

            FlowLayoutPanel pnlBtn = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.White
            };

            Button btnTutup = new Button
            {
                Text = "✖ Tutup",
                Size = new Size(110, 36),
                BackColor = Color.FromArgb(210, 210, 210),
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand,
                Margin = new Padding(6, 4, 0, 0)
            };
            btnTutup.FlatAppearance.BorderSize = 0;
            btnTutup.Click += (s, e) => frmDetail.Close();

            Button btnSalin = new Button
            {
                Text = "📋 Salin Split Bill",
                Size = new Size(160, 36),
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = Color.FromArgb(253, 255, 182),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(6, 4, 0, 0)
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

            pnlBtn.Controls.Add(btnTutup);
            pnlBtn.Controls.Add(btnSalin);
            pnlFooter.Controls.Add(pnlBtn, 1, 0);
            tbl.Controls.Add(pnlFooter, 0, 5);

            frmDetail.ShowDialog();
        }
    }
}
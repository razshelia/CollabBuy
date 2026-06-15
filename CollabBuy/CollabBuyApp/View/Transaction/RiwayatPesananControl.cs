using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class RiwayatPesananControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly TransactionController _transactionController;
        private DataTable _dtRiwayatCache;

        public RiwayatPesananControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._currentUser = currentUser;
            this._transactionController = new TransactionController(this._currentUser.IdUser);

            this.Resize += (s, e) => this.AdjustLayout();
        }

        // ── Load ────────────────────────────────────────────────
        private void RiwayatPesananControl_Load(object sender, EventArgs e)
        {
            this.SetupDataGridView();
            this.LoadDataRiwayat();

            this.splitMain.Panel1.SizeChanged += (s, ev) =>
            {
                this.dgvRiwayat.Size = new System.Drawing.Size(930, 420);
                this.btnRefresh.Left = this.splitMain.Panel1.Width - this.btnRefresh.Width - 10;
            };
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataRiwayat();
        }

        // ── Layout ──────────────────────────────────────────────
        private void AdjustLayout()
        {
            int margin = 30;
            int availW = this.Width - margin * 2;
            int availH = this.Height - 95 - margin;

            if (this.splitMain == null) return;

            this.splitMain.Location = new Point(margin, 95);
            this.splitMain.Size = new Size(availW, Math.Max(availH, 200));

            if (this.btnRefresh != null && this.splitMain.Panel1 != null)
                this.btnRefresh.Location = new Point(
                    this.splitMain.Panel1.Width - this.btnRefresh.Width - 10,
                    this.splitMain.Panel1.Height - this.btnRefresh.Height - 10);

            if (this.dgvRiwayat != null && this.splitMain.Panel1 != null)
            {
                // UBAH: dari Point(10, 10) → Point(0, 46) agar tidak nabrak txtCari
                this.dgvRiwayat.Location = new Point(0, 46);   // ← UBAH INI
                int h = this.btnRefresh != null
                    ? this.splitMain.Panel1.Height - this.btnRefresh.Height - 60
                    : this.splitMain.Panel1.Height - 60;
                this.dgvRiwayat.Size = new Size(this.splitMain.Panel1.Width - 20, h);
            }
        }

        // ── DataGridView ────────────────────────────────────────
        private void SetupDataGridView()
        {
            if (this.dgvRiwayat != null)
            {
                this.dgvRiwayat.AutoGenerateColumns = false;
                this.dgvRiwayat.Columns.Clear();

                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTrx", DataPropertyName = "id_transaksi", Visible = false });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "NoInvoice", HeaderText = "No. Invoice", DataPropertyName = "no_invoice", Width = 110 });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Waktu Pemesanan", DataPropertyName = "tanggal_pesanan", Width = 155 });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "JumlahItem", HeaderText = "Item", DataPropertyName = "jumlah_item", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total Tagihan", DataPropertyName = "total_harga", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cashback", HeaderText = "Cashback GR", DataPropertyName = "cashback", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Alignment = DataGridViewContentAlignment.MiddleRight } });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusBayar", HeaderText = "Status Bayar", DataPropertyName = "status_bayar", Width = 150 });
                this.dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Pesanan", DataPropertyName = "status_pesanan", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                
                DataGridViewButtonColumn btnDetailCol = new DataGridViewButtonColumn
                {
                    Name = "BtnDetail",
                    HeaderText = "",
                    Text = "🔍 Detail",
                    UseColumnTextForButtonValue = true,
                    Width = 90,
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

                    var dataSource = this.dgvRiwayat.DataSource as DataTable;
                    if (dataSource == null) return;

                    DataRow row = dataSource.Rows[e.RowIndex];
                    int idTrx = Convert.ToInt32(row["id_transaksi"]);
                    DataTable dtDetail = this._transactionController.GetDetailPesananPembeli(idTrx);
                    this.TampilkanDetailLayarPenuh(idTrx, dtDetail);
                };
            }
            else
            {
                bool tabelKosong = true;
            }
        }

        // ── Load Data ───────────────────────────────────────────
        private void LoadDataRiwayat()
        {
            try
            {
                List<Models.Transaction> listTrx = this._transactionController.GetTransaksiByPembeli(this._currentUser.IdUser);

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_transaksi", typeof(int));
                dtUI.Columns.Add("no_invoice", typeof(string));
                dtUI.Columns.Add("tanggal_pesanan", typeof(string));
                dtUI.Columns.Add("jumlah_item", typeof(string));
                dtUI.Columns.Add("total_harga", typeof(string));
                dtUI.Columns.Add("cashback", typeof(string));
                dtUI.Columns.Add("status_bayar", typeof(string));
                dtUI.Columns.Add("status_pesanan", typeof(string));

                if (listTrx != null)
                {
                    foreach (Models.Transaction trx in listTrx)
                    {
                        long totalTagihan = trx.HitungTotal();
                        long totalCashback = trx.HitungDiskon();
                        long tagihBersih = totalTagihan - totalCashback;

                        string jumlahStr = trx.DapatkanTotalItem() > 0
                            ? $"{trx.DapatkanTotalItem()} pcs" : "-";

                        string tagihStr = tagihBersih > 0
                            ? $"Rp {tagihBersih:N0}" : $"Rp {totalTagihan:N0}";

                        string cbStr = totalCashback > 0
                            ? $"Rp {totalCashback:N0} ✅" : "-";

                        dtUI.Rows.Add(
                            trx.IdTransaksi,
                            $"INV-{trx.IdTransaksi:D6}",
                            trx.TanggalTransaksi.ToString("dd MMM yyyy, HH:mm"),
                            jumlahStr,
                            tagihStr,
                            cbStr,
                            trx.DapatkanStatusPembayaranUI(),
                            trx.GetStatus()
                        );
                    }
                }

                if (this.dgvRiwayat != null)
                {
                    this._dtRiwayatCache = dtUI;
                    this.dgvRiwayat.DataSource = this._dtRiwayatCache;
                    this.dgvRiwayat.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TerapkanFilterRiwayat()
        {
            if (this._dtRiwayatCache == null) return;
            string kata = this.txtCariRiwayat.Text.Trim();
            DataView dv = this._dtRiwayatCache.DefaultView;
            dv.RowFilter = string.IsNullOrEmpty(kata) ? ""
                : $"status_pesanan LIKE '%{kata}%'";
            this.dgvRiwayat.DataSource = dv;
            this.dgvRiwayat.ClearSelection();
        }

        private void txtCariRiwayat_TextChanged(object sender, EventArgs e)
        {
            this.TerapkanFilterRiwayat();
        }

        // ── Tampilkan Detail Layar Penuh (TableLayoutPanel Style) ───────────
        private void TampilkanDetailLayarPenuh(int idTrx, DataTable dtDetail)
        {
            var splitDict = new Dictionary<string, long>();
            var cashbackDict = new Dictionary<string, long>();
            long grandTotal = 0;
            long totalCashback = 0;
            byte[] buktiBayar = null;

            if (dtDetail != null)
            {
                if (dtDetail.Columns.Contains("bukti_bayar"))
                {
                    if (dtDetail.Rows.Count > 0)
                    {
                        if (dtDetail.Rows[0]["bukti_bayar"] != DBNull.Value)
                            buktiBayar = (byte[])dtDetail.Rows[0]["bukti_bayar"];
                    }
                    else { bool noRows = true; }
                }
                else { bool noCol = true; }

                foreach (DataRow row in dtDetail.Rows)
                {
                    long subtotal = Convert.ToInt64(row["subtotal"]);
                    long cashback = Convert.ToInt64(row["selisih_refund"]);
                    grandTotal += subtotal;
                    totalCashback += cashback;

                    string penitip = row["nama_penitip"].ToString();

                    if (!splitDict.ContainsKey(penitip))
                    {
                        splitDict[penitip] = 0;
                        cashbackDict[penitip] = 0;
                    }

                    splitDict[penitip] += subtotal;
                    cashbackDict[penitip] += cashback;
                }
            }
            else
            {
                bool dtKosong = true;
            }

            // Sembunyikan elemen utama halaman
            this.splitMain.Visible = false;
            this.lblTitle.Visible = false;
            this.lblSubtitle.Visible = false;

            // Panel Utama SPA
            Panel pnlFull = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                AutoScroll = true
            };

            // =========================================================
            // PERBAIKAN: Gunakan TableLayoutPanel untuk kunci Grid layout
            // =========================================================
            TableLayoutPanel tblGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 1080, // Fix tinggi absolut agar bisa di-scroll
                ColumnCount = 1,
                RowCount = 7,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            tblGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));   // 0 Header (Tombol & Judul)
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));   // 1 Label Rincian
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));  // 2 DGV Rincian
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));   // 3 Label Split
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));  // 4 DGV Split
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 350F));  // 5 Bukti Bayar (Ukuran Jumbo)
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));   // 6 Footer / Salin Bill

            // [Row 0] Header
            Panel pnlHdr = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };

            Button btnKembali = new Button
            {
                Text = "⬅ Kembali",
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                BackColor = Color.FromArgb(235, 204, 255),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 45),
                Location = new Point(0, 15),
                Cursor = Cursors.Hand
            };
            btnKembali.FlatAppearance.BorderSize = 0;
            btnKembali.Click += (s, ev) =>
            {
                this.Controls.Remove(pnlFull);
                pnlFull.Dispose();

                this.splitMain.Visible = true;
                this.lblTitle.Visible = true;
                this.lblSubtitle.Visible = true;
            };

            Label lblDetailTitle = new Label
            {
                Text = $"📋 Detail Pesanan: INV-{idTrx:D6}",
                Font = new Font("Segoe UI Black", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Location = new Point(130, 15)
            };

            string statusTeks;
            if (dtDetail != null)
            {
                if (dtDetail.Rows.Count > 0)
                {
                    statusTeks = $"Status: {dtDetail.Rows[0]["status_pesanan"]}  |  Waktu: {dtDetail.Rows[0]["tanggal_transaksi"]}";
                }
                else
                {
                    statusTeks = "Tidak ada data transaksi";
                }
            }
            else
            {
                statusTeks = "Tidak ada data transaksi";
            }

            Label lblStatus = new Label
            {
                Text = statusTeks,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(90, 24, 154),
                AutoSize = true,
                Location = new Point(135, 45)
            };

            pnlHdr.Controls.Add(btnKembali);
            pnlHdr.Controls.Add(lblDetailTitle);
            pnlHdr.Controls.Add(lblStatus);
            tblGrid.Controls.Add(pnlHdr, 0, 0);

            // [Row 1] Label Rincian
            Label lblRincian = new Label
            {
                Text = "🧾 Rincian Item",
                Font = new Font("Segoe UI Black", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomLeft,
                Height = 35
            };
            tblGrid.Controls.Add(lblRincian, 0, 1);

            // [Row 2] DGV Rincian
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Margin = new Padding(0, 5, 0, 0)
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 204, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Produk", DataPropertyName = "nama_produk", FillWeight = 25 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", DataPropertyName = "nama_penitip", FillWeight = 15 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = "jumlah", FillWeight = 8, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Harga", DataPropertyName = "harga_satuan", FillWeight = 12, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "subtotal", FillWeight = 15, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback", DataPropertyName = "cashback_str", FillWeight = 15, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Catatan", DataPropertyName = "catatan", FillWeight = 20 });

            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("nama_produk", typeof(string));
            dtGrid.Columns.Add("nama_penitip", typeof(string));
            dtGrid.Columns.Add("jumlah", typeof(int));
            dtGrid.Columns.Add("harga_satuan", typeof(long));
            dtGrid.Columns.Add("subtotal", typeof(long));
            dtGrid.Columns.Add("cashback_str", typeof(string));
            dtGrid.Columns.Add("catatan", typeof(string));

            if (dtDetail != null)
            {
                foreach (DataRow row in dtDetail.Rows)
                {
                    long subtotal = Convert.ToInt64(row["subtotal"]);
                    long cashback = Convert.ToInt64(row["selisih_refund"]);

                    string cbTeks;
                    if (cashback > 0)
                    {
                        cbTeks = $"Rp {cashback:N0} ✅";
                    }
                    else
                    {
                        cbTeks = "-";
                    }

                    dtGrid.Rows.Add(
                        row["nama_produk"].ToString(),
                        row["nama_penitip"].ToString(),
                        Convert.ToInt32(row["jumlah"]),
                        Convert.ToInt64(row["harga_satuan"]),
                        subtotal,
                        cbTeks,
                        row["catatan"].ToString()
                    );
                }
            }
            else
            {
                bool dtDetailKosong = true;
            }
            dgv.DataSource = dtGrid;
            tblGrid.Controls.Add(dgv, 0, 2);

            // [Row 3] Label Split
            Label lblSplit = new Label
            {
                Text = "💰 Split Bill per Penitip",
                Font = new Font("Segoe UI Black", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomLeft,
                Height = 35
            };
            tblGrid.Controls.Add(lblSplit, 0, 3);

            // [Row 4] DGV Split
            DataGridView dgvSplit = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.FromArgb(245, 232, 255),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 5, 0, 0)
            };
            dgvSplit.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 160, 240);
            dgvSplit.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvSplit.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSplit.EnableHeadersVisualStyles = false;
            dgvSplit.DefaultCellStyle.BackColor = Color.FromArgb(245, 232, 255);

            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Belanja (Rp)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback (Rp)", DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Harus Bayar (Rp)", DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleRight } });

            foreach (var kv in splitDict)
            {
                long cb = cashbackDict[kv.Key];
                long netBayar = kv.Value - cb;
                long finalCb = cb > 0 ? cb : 0;

                dgvSplit.Rows.Add(kv.Key, kv.Value, finalCb, netBayar);
            }
            tblGrid.Controls.Add(dgvSplit, 0, 4);

            // [Row 5] Bukti Bayar
            Panel pnlBuktiBox = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250),
                Margin = new Padding(0, 15, 0, 0)
            };

            Label lblBuktiTitle = new Label
            {
                Text = "🧾 Bukti Pembayaran Transaksi",
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            if (buktiBayar != null)
            {
                if (buktiBayar.Length > 10)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream(buktiBayar);
                        Image img = Image.FromStream(ms);

                        PictureBox pb = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.Zoom, // Zoom mengamankan letak gambar tepat di tengah!
                            Image = img,
                            Cursor = Cursors.Hand
                        };

                        pb.Click += (s, e) =>
                        {
                            Form frmZoom = new Form
                            {
                                Text = $"Bukti Bayar — INV-{idTrx:D6}",
                                Size = new Size(800, 700),
                                StartPosition = FormStartPosition.CenterParent,
                                BackColor = Color.Black
                            };
                            frmZoom.Controls.Add(new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, Image = img });
                            frmZoom.ShowDialog();
                        };

                        pnlBuktiBox.Controls.Add(pb);
                        pnlBuktiBox.Controls.Add(lblBuktiTitle);
                    }
                    catch
                    {
                        Label errLbl = new Label
                        {
                            Text = "⚠️ File tidak dapat ditampilkan.",
                            ForeColor = Color.FromArgb(150, 0, 0),
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                        };
                        pnlBuktiBox.Controls.Add(errLbl);
                        pnlBuktiBox.Controls.Add(lblBuktiTitle);
                    }
                }
                else
                {
                    Label noDataLbl = new Label
                    {
                        Text = "Belum ada bukti pembayaran.",
                        ForeColor = Color.Gray,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                    };
                    pnlBuktiBox.Controls.Add(noDataLbl);
                    pnlBuktiBox.Controls.Add(lblBuktiTitle);
                }
            }
            else
            {
                Label noDataLbl = new Label
                {
                    Text = "Belum ada bukti pembayaran.",
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                };
                pnlBuktiBox.Controls.Add(noDataLbl);
                pnlBuktiBox.Controls.Add(lblBuktiTitle);
            }
            tblGrid.Controls.Add(pnlBuktiBox, 0, 5);

            // [Row 6] Footer / Salin Bill
            Panel pnlBot = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 15, 0, 0) };

            long tagihBersihTotal = grandTotal - totalCashback;
            string lblTotalStr;

            if (totalCashback > 0)
            {
                lblTotalStr = $"Tagihan Asli : Rp {grandTotal:N0}\nCashback GR : Rp {totalCashback:N0}\n──────────────────\nTotal Bayar : Rp {tagihBersihTotal:N0} ✅";
            }
            else
            {
                lblTotalStr = $"Total Bayar : Rp {grandTotal:N0}";
            }

            Label lblTotal = new Label
            {
                Text = lblTotalStr,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Dock = DockStyle.Left,
                Width = 400,
                AutoSize = false,
                Height = 100,                              // ← TAMBAH INI
                TextAlign = ContentAlignment.MiddleLeft
            };

            Button btnSalin = new Button
            {
                Text = "📋 Salin Split Bill ke WA",
                Size = new Size(250, 50),
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = Color.FromArgb(253, 255, 182),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Dock = DockStyle.Right
            };

            btnSalin.FlatAppearance.BorderSize = 0;
            btnSalin.Click += (s, e) =>
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"💰 Split Bill — INV-{idTrx:D6}");
                sb.AppendLine(new string('─', 38));

                foreach (var kv in splitDict)
                {
                    long cb = cashbackDict[kv.Key];
                    long bayar = kv.Value - cb;
                    sb.Append($"• {kv.Key}: Rp {bayar:N0}");

                    if (cb > 0) sb.Append($" (hemat Rp {cb:N0} 🎉)");

                    sb.AppendLine();
                }
                sb.AppendLine(new string('─', 38));
                sb.AppendLine($"Total: Rp {grandTotal:N0}");

                if (totalCashback > 0) sb.AppendLine($"Cashback GR: Rp {totalCashback:N0}");

                Clipboard.SetText(sb.ToString());
                MessageBox.Show("Split bill disalin! Tinggal paste ke WA 😊", "✅ Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            pnlBot.Controls.Add(lblTotal);
            pnlBot.Controls.Add(btnSalin);
            tblGrid.Controls.Add(pnlBot, 0, 6);

            // Masukkan grid ke dalam panel utama lalu tampilkan
            pnlFull.Controls.Add(tblGrid);
            this.Controls.Add(pnlFull);
            pnlFull.BringToFront();
        }
    }
}
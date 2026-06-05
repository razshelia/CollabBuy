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
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;

        public RiwayatPesananControl(User currentUser)
        {
            this.InitializeComponent();
            this._currentUser = currentUser;
            this._transactionController = new TransactionController(this._currentUser.GetIdUser());
            this.Resize += (s, e) => this.AdjustLayout();
        }

        // ── Load ────────────────────────────────────────────────
        private void RiwayatPesananControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataRiwayat();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataRiwayat();
            // Tutup panel detail saat refresh
            this.splitMain.Panel2Collapsed = true;
            this.pnlDetail.Controls.Clear();
        }

        // ── Layout ──────────────────────────────────────────────
        private void AdjustLayout()
        {
            int margin = 30;
            int availW = this.Width - margin * 2;
            int availH = this.Height - 95 - margin;

            this.splitMain.Location = new Point(margin, 95);
            this.splitMain.Size = new Size(availW, Math.Max(availH, 200));

            // Tombol refresh di kanan bawah Panel1
            this.btnRefresh.Location = new Point(
                this.splitMain.Panel1.Width - this.btnRefresh.Width - 10,
                this.splitMain.Panel1.Height - this.btnRefresh.Height - 10);

            this.dgvRiwayat.Location = new Point(10, 10);
            this.dgvRiwayat.Size = new Size(
                this.splitMain.Panel1.Width - 20,
                this.splitMain.Panel1.Height - this.btnRefresh.Height - 30);
        }

        // ── DataGridView ────────────────────────────────────────
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
                var row = ((DataTable)this.dgvRiwayat.DataSource).Rows[e.RowIndex];
                int idTrx = Convert.ToInt32(row["id_transaksi"]);
                DataTable dtDetail = this._transactionController.GetDetailPesananPembeli(idTrx);
                this.TampilkanDetailInline(idTrx, dtDetail);
            };
        }

        // ── Load Data ───────────────────────────────────────────
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

                if (listTrx != null)
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
                MessageBox.Show("Gagal memuat riwayat: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Tampilkan Detail Inline ──────────────────────────────
        private void TampilkanDetailInline(int idTrx, DataTable dtDetail)
        {
            // Hitung split bill
            var splitDict = new Dictionary<string, long>();
            var cashbackDict = new Dictionary<string, long>();
            long grandTotal = 0;
            long totalCashback = 0;
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

            // Bersihkan panel detail lama
            this.pnlDetail.Controls.Clear();

            // Buka panel kanan
            this.splitMain.Panel2Collapsed = false;
            // Atur lebar panel kanan (60% dari total atau minimal 520px)
            int panel2W = Math.Max(520, (int)(this.splitMain.Width * 0.55));
            this.splitMain.SplitterDistance = this.splitMain.Width - panel2W - this.splitMain.SplitterWidth;

            // ── TableLayoutPanel utama di pnlDetail ──
            TableLayoutPanel tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 7,
                BackColor = Color.White,
                Padding = new Padding(12, 8, 12, 8)
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));   // 0 Header
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));   // 1 Label rincian
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));    // 2 DGV rincian
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));   // 3 Label split
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));    // 4 DGV split
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 28F));    // 5 Bukti bayar
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));   // 6 Footer

            // Baris 0 — Header
            Panel pnlHdr = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlHdr.Controls.Add(new Label
            {
                Text = $"📋 INV-{idTrx:D6}",
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Location = new Point(0, 2)
            });
            string statusTeks = dtDetail.Rows.Count > 0
                ? $"Status: {dtDetail.Rows[0]["status_pesanan"]}  |  {dtDetail.Rows[0]["tanggal_transaksi"]}"
                : "Tidak ada data";
            pnlHdr.Controls.Add(new Label
            {
                Text = statusTeks,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(90, 24, 154),
                AutoSize = true,
                Location = new Point(2, 32)
            });
            // Tombol tutup di kanan atas
            Button btnTutupInline = new Button
            {
                Text = "✖",
                Size = new Size(32, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(210, 210, 210),
                ForeColor = Color.FromArgb(60, 60, 60),
                Cursor = Cursors.Hand,
                Location = new Point(panel2W - 55, 2),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnTutupInline.FlatAppearance.BorderSize = 0;
            btnTutupInline.Click += (s, e) =>
            {
                this.splitMain.Panel2Collapsed = true;
                this.pnlDetail.Controls.Clear();
            };
            pnlHdr.Controls.Add(btnTutupInline);
            tbl.Controls.Add(pnlHdr, 0, 0);

            // Baris 1 — Label rincian
            tbl.Controls.Add(new Label
            {
                Text = "🧾 Rincian Item",
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft
            }, 0, 1);

            // Baris 2 — DGV rincian
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 204, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Produk", DataPropertyName = "nama_produk", Width = 160 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", DataPropertyName = "nama_penitip", Width = 110 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = "jumlah", Width = 42, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Harga", DataPropertyName = "harga_satuan", Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "subtotal", Width = 105, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback", DataPropertyName = "cashback_str", Width = 105, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Catatan", DataPropertyName = "catatan", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 60 });

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
            tbl.Controls.Add(dgv, 0, 2);

            // Baris 3 — Label split bill
            tbl.Controls.Add(new Label
            {
                Text = "💰 Split Bill per Penitip",
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft
            }, 0, 3);

            // Baris 4 — DGV split
            DataGridView dgvSplit = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.FromArgb(245, 232, 255),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            };
            dgvSplit.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 160, 240);
            dgvSplit.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvSplit.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dgvSplit.EnableHeadersVisualStyles = false;
            dgvSplit.DefaultCellStyle.BackColor = Color.FromArgb(245, 232, 255);

            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 110 });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Belanja (Rp)", Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cashback (Rp)", Width = 115, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(0, 130, 60), Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSplit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Harus Bayar (Rp)", Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleRight } });

            foreach (var kv in splitDict)
            {
                long cb = cashbackDict[kv.Key];
                dgvSplit.Rows.Add(kv.Key, kv.Value, cb > 0 ? cb : 0, kv.Value - cb);
            }
            tbl.Controls.Add(dgvSplit, 0, 4);

            // Baris 5 — Bukti Bayar (label kiri, gambar proporsional di kanan)
            TableLayoutPanel pnlBukti = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0, 6, 0, 0)
            };
            pnlBukti.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            pnlBukti.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            pnlBukti.Controls.Add(new Label
            {
                Text = "🧾 Bukti\nBayar",
                Font = new Font("Segoe UI Black", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 44,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);

            if (buktiBayar != null && buktiBayar.Length > 10)
            {
                try
                {
                    Image img;
                    using (var ms = new MemoryStream(buktiBayar))
                        img = Image.FromStream(ms);

                    PictureBox pb = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = img,
                        BorderStyle = BorderStyle.FixedSingle,
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
                    pnlBukti.Controls.Add(pb, 1, 0);
                }
                catch
                {
                    pnlBukti.Controls.Add(new Label
                    {
                        Text = "⚠️ File tidak dapat ditampilkan.",
                        ForeColor = Color.FromArgb(150, 0, 0),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                    }, 1, 0);
                }
            }
            else
            {
                Panel pnlNo = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 245, 245), BorderStyle = BorderStyle.FixedSingle };
                pnlNo.Controls.Add(new Label
                {
                    Text = "Belum ada bukti pembayaran.",
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                });
                pnlBukti.Controls.Add(pnlNo, 1, 0);
            }
            tbl.Controls.Add(pnlBukti, 0, 5);

            // Baris 6 — Footer
            TableLayoutPanel pnlFooter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0, 4, 0, 0)
            };
            pnlFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));

            long tagihBersihTotal = grandTotal - totalCashback;
            pnlFooter.Controls.Add(new Label
            {
                Text = $"Total: Rp {grandTotal:N0}" +
                            (totalCashback > 0 ? $"   |   Cashback: Rp {totalCashback:N0}   |   ✅ Bayar: Rp {tagihBersihTotal:N0}" : ""),
                Font = new Font("Segoe UI Black", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);

            Button btnSalin = new Button
            {
                Text = "📋 Salin Split Bill",
                Size = new Size(165, 38),
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = Color.FromArgb(253, 255, 182),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
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
            pnlFooter.Controls.Add(btnSalin, 1, 0);
            tbl.Controls.Add(pnlFooter, 0, 6);

            this.pnlDetail.Controls.Add(tbl);
            this.splitMain.Panel2.Refresh();
        }
    }
}
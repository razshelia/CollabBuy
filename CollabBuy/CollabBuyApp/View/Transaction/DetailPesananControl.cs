using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    /// <summary>
    /// UserControl untuk menampilkan detail pesanan masuk milik penjual.
    /// Ditampilkan inline (scrollable) di pnlContent, bukan membuka tab/window baru.
    /// </summary>
    public partial class DetailPesananControl : UserControl
    {
        private readonly int _idTransaksi;
        private readonly DataTable _dtDetail;
        private byte[] _bytesBuktiBayar = null;

        /// <summary>Event untuk kembali ke halaman Pesanan Masuk.</summary>
        public event Action OnNavigateKembali;

        public DetailPesananControl(int idTransaksi, DataTable dtDetail)
        {
            this.InitializeComponent();
            this._idTransaksi = idTransaksi;
            this._dtDetail = dtDetail;
        }

        private void DetailPesananControl_Load(object sender, EventArgs e)
        {
            this.LoadHeaderInfo();
            this.SetupDataGridView();
            this.LoadRincianItem();
            this.LoadBuktiBayar();
        }

        // =======================================================
        // LOAD INFORMASI HEADER PESANAN
        // =======================================================
        private void LoadHeaderInfo()
        {
            if (this._dtDetail == null || this._dtDetail.Rows.Count == 0)
                return;

            DataRow baris = this._dtDetail.Rows[0];

            this.lblIdTransaksi.Text = $"INV-{this._idTransaksi:D6}";
            this.lblNamaPembeli.Text = baris["nama_pembeli"].ToString();

            // ── Nomor Telepon buat baru ──
            Label lblTelp = this.pnlInfo.Controls["lblNomorTelepon"] as Label;
            if (lblTelp == null)
            {
                lblTelp = new Label
                {
                    Name = "lblNomorTelepon",
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 9F),
                    ForeColor = System.Drawing.Color.FromArgb(80, 80, 80),
                    Location = new System.Drawing.Point(12, 54)
                };
                this.pnlInfo.Controls.Add(lblTelp);
            }
            lblTelp.Text = "📞 " + (this._dtDetail.Columns.Contains("nomor_telepon")
                ? baris["nomor_telepon"].ToString()
                : "-");

            this.lblTanggal.Text = baris["tanggal_transaksi"].ToString();

            string status = baris["status_pesanan"].ToString();
            this.lblStatus.Text = status;

            if (status == "Selesai")
                this.lblStatus.ForeColor = Color.ForestGreen;
            else if (status == "Dibatalkan")
                this.lblStatus.ForeColor = Color.LightCoral;
            else if (status == "Diproses")
                this.lblStatus.ForeColor = Color.RoyalBlue;
            else
                this.lblStatus.ForeColor = Color.DarkOrange;
        }

        // =======================================================
        // SETUP & LOAD RINCIAN ITEM PRODUK
        // =======================================================
        private void SetupDataGridView()
        {
            this.dgvRincian.AutoGenerateColumns = false;
            this.dgvRincian.Columns.Clear();

            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaProduk",
                HeaderText = "Nama Produk",
                DataPropertyName = "nama_produk",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Penitip",
                HeaderText = "Nama Penitip",
                DataPropertyName = "nama_penitip",
                Width = 140
            });
            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Jumlah",
                HeaderText = "Qty",
                DataPropertyName = "jumlah",
                Width = 55,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Harga",
                HeaderText = "Harga Satuan",
                DataPropertyName = "harga_satuan",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal (Rp)",
                DataPropertyName = "subtotal",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Catatan",
                HeaderText = "Catatan",
                DataPropertyName = "catatan",
                Width = 120
            });
            this.dgvRincian.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cashback",
                HeaderText = "💸 Cashback",
                DataPropertyName = "cashback_str",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    ForeColor = Color.FromArgb(0, 130, 60),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        private void LoadRincianItem()
        {
            if (this._dtDetail == null || this._dtDetail.Rows.Count == 0)
                return;

            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("nama_produk", typeof(string));
            dtGrid.Columns.Add("nama_penitip", typeof(string));
            dtGrid.Columns.Add("jumlah", typeof(int));
            dtGrid.Columns.Add("harga_satuan", typeof(long));
            dtGrid.Columns.Add("subtotal", typeof(long));
            dtGrid.Columns.Add("catatan", typeof(string));
            dtGrid.Columns.Add("cashback_str", typeof(string));

            long grandTotal = 0;

            foreach (DataRow row in this._dtDetail.Rows)
            {
                long subtotal = Convert.ToInt64(row["subtotal"]);
                grandTotal += subtotal;

                long cashback = this._dtDetail.Columns.Contains("selisih_refund")
                    ? Convert.ToInt64(row["selisih_refund"])
                    : 0;

                string cashbackStr = cashback > 0
                    ? $"Rp {cashback:N0} ✅"
                    : "-";

                dtGrid.Rows.Add(
                    row["nama_produk"].ToString(),
                    row["nama_penitip"].ToString(),
                    Convert.ToInt32(row["jumlah"]),
                    Convert.ToInt64(row["harga_satuan"]),
                    subtotal,
                    row["catatan"].ToString(),
                    cashbackStr
                );
            }

            this.dgvRincian.DataSource = dtGrid;
            this.dgvRincian.ClearSelection();
            this.lblGrandTotal.Text = $"Total Produk Kamu: Rp {grandTotal:N0}";
            long totalCashback = 0;
            foreach (DataRow row in this._dtDetail.Rows)
            {
                if (this._dtDetail.Columns.Contains("selisih_refund"))
                    totalCashback += Convert.ToInt64(row["selisih_refund"]);
            }

            if (totalCashback > 0)
            {
                this.lblCashbackInfo.Text =
                    $"💸 Total cashback Gotong Royong yang harus kamu kembalikan ke pembeli: Rp {totalCashback:N0}";
                this.lblCashbackInfo.Visible = true;
            }
            else
            {
                this.lblCashbackInfo.Visible = false;
            }
        }

        // =======================================================
        // LOAD BUKTI PEMBAYARAN
        // =======================================================
        private void LoadBuktiBayar()
        {
            if (this._dtDetail == null || this._dtDetail.Rows.Count == 0)
            {
                this.ShowTidakAdaBukti("Data pesanan tidak tersedia.");
                return;
            }

            object buktiBayarObj = this._dtDetail.Rows[0]["bukti_bayar"];

            if (buktiBayarObj == null || buktiBayarObj == DBNull.Value)
            {
                this.ShowTidakAdaBukti("Pembeli belum mengupload bukti pembayaran.");
                return;
            }

            byte[] buktiBayar = buktiBayarObj as byte[];

            if (buktiBayar == null || buktiBayar.Length == 0)
            {
                this.ShowTidakAdaBukti("Bukti pembayaran kosong atau tidak valid.");
                return;
            }

            try
            {
                using (MemoryStream ms = new MemoryStream(buktiBayar))
                {
                    Image gambar = Image.FromStream(ms);
                    this.picBuktiBayar.Image = gambar;
                    this.picBuktiBayar.SizeMode = PictureBoxSizeMode.Zoom;
                    this.picBuktiBayar.Visible = true;
                    this.lblTidakAdaBukti.Visible = false;
                    this.btnSimpanBukti.Visible = true;
                    this.btnSimpanBukti.Enabled = true;
                }
                this._bytesBuktiBayar = buktiBayar;
            }
            catch (Exception)
            {
                this.ShowTidakAdaBukti("Bukti pembayaran ada tapi tidak bisa ditampilkan (format tidak dikenal).");
            }
        }

        private void ShowTidakAdaBukti(string pesan)
        {
            this.picBuktiBayar.Visible = false;
            this.lblTidakAdaBukti.Text = pesan;
            this.lblTidakAdaBukti.Visible = true;
            this.btnSimpanBukti.Visible = false;
            this.btnSimpanBukti.Enabled = false;
        }

        // =======================================================
        // TOMBOL SIMPAN BUKTI BAYAR
        // =======================================================
        private void btnSimpanBukti_Click(object sender, EventArgs e)
        {
            if (this._bytesBuktiBayar == null || this._bytesBuktiBayar.Length == 0)
            {
                MessageBox.Show("Tidak ada bukti bayar untuk disimpan.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Simpan Bukti Pembayaran";
                sfd.FileName = $"BuktiBayar_INV{this._idTransaksi:D6}";
                sfd.Filter = "Gambar JPEG (*.jpg)|*.jpg|Gambar PNG (*.png)|*.png|Semua File (*.*)|*.*";
                sfd.DefaultExt = "jpg";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllBytes(sfd.FileName, this._bytesBuktiBayar);
                        MessageBox.Show("Bukti pembayaran berhasil disimpan!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menyimpan file: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =======================================================
        // TOMBOL KEMBALI — kembali ke PesananMasukControl
        // =======================================================
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.OnNavigateKembali?.Invoke();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using TrxModel = CollabBuy.CollabBuyApp.Models.Transaction;
using TrxDetailModel = CollabBuy.CollabBuyApp.Models.TransactionDetail;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    /// <summary>
    /// Control Riwayat Pesanan — tersinkronisasi penuh dengan database.
    /// Menampilkan semua transaksi milik user yang sedang login.
    /// Fitur: filter status, konfirmasi terima, lihat detail item, dan badge status berwarna.
    /// </summary>
    public partial class RiwayatPesananControl : UserControl
    {
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;

        // Cache data transaksi saat ini
        private List<TrxModel> _listTransaksi = new List<TrxModel>();

        // Warna tema
        private static readonly Color ColorPrimary = Color.FromArgb(36, 0, 70);
        private static readonly Color ColorAccent = Color.FromArgb(200, 182, 255);
        private static readonly Color ColorYellow = Color.FromArgb(253, 255, 182);
        private static readonly Color ColorBg = Color.FromArgb(248, 249, 250);
        private static readonly Color ColorSuccess = Color.FromArgb(40, 167, 69);
        private static readonly Color ColorWarning = Color.FromArgb(255, 193, 7);
        private static readonly Color ColorDanger = Color.FromArgb(220, 53, 69);
        private static readonly Color ColorInfo = Color.FromArgb(23, 162, 184);

        public RiwayatPesananControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _transactionController = new TransactionController();
        }

        // ===================================================
        // EVENT HANDLERS
        // ===================================================

        private void RiwayatPesananControl_Load(object sender, EventArgs e)
        {
            SetupDgv();
            IsiFilterStatus();
            LoadDataRiwayat();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataRiwayat();
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TerapkanFilter();
        }

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string kolom = dgvRiwayat.Columns[e.ColumnIndex].Name;

            if (kolom == "BtnKonfirmasi")
            {
                HandleKonfirmasi(e.RowIndex);
            }
            else if (kolom == "BtnDetail")
            {
                HandleLihatDetail(e.RowIndex);
            }
        }

        private void dgvRiwayat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvRiwayat.Columns[e.ColumnIndex].Name != "Status") return;
            if (e.Value == null) return;

            string status = e.Value.ToString();
            switch (status)
            {
                case "Selesai":
                    e.CellStyle.ForeColor = ColorSuccess;
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    break;
                case "Diproses":
                    e.CellStyle.ForeColor = ColorInfo;
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    break;
                case "Menunggu":
                    e.CellStyle.ForeColor = ColorWarning;
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    break;
                case "Dibatalkan":
                    e.CellStyle.ForeColor = ColorDanger;
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Strikeout);
                    break;
            }
        }

        // ===================================================
        // SETUP KOMPONEN
        // ===================================================

        private void SetupDgv()
        {
            dgvRiwayat.AutoGenerateColumns = false;
            dgvRiwayat.Columns.Clear();

            // Kolom tersembunyi
            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdTransaksi",
                DataPropertyName = "IdTransaksi",
                Visible = false
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NoUrut",
                HeaderText = "#",
                Width = 45,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tanggal Transaksi",
                DataPropertyName = "Tanggal",
                Width = 160
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total Belanja (Rp)",
                DataPropertyName = "TotalHarga",
                Width = 175,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
                }
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 130
            });

            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "JmlItem",
                HeaderText = "Jml Item",
                DataPropertyName = "JumlahItem",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Tombol Detail
            var btnDetail = new DataGridViewButtonColumn
            {
                Name = "BtnDetail",
                HeaderText = "Detail",
                Text = "🔍 Detail",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            btnDetail.DefaultCellStyle.BackColor = Color.FromArgb(225, 215, 255);
            btnDetail.DefaultCellStyle.ForeColor = ColorPrimary;
            dgvRiwayat.Columns.Add(btnDetail);

            // Tombol Konfirmasi
            var btnKonfirmasi = new DataGridViewButtonColumn
            {
                Name = "BtnKonfirmasi",
                HeaderText = "Konfirmasi",
                Text = "✔ Diterima",
                UseColumnTextForButtonValue = true,
                Width = 120,
                FlatStyle = FlatStyle.Flat
            };
            btnKonfirmasi.DefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255);
            btnKonfirmasi.DefaultCellStyle.ForeColor = ColorPrimary;
            dgvRiwayat.Columns.Add(btnKonfirmasi);

            // Style header & row
            dgvRiwayat.ColumnHeadersHeight = 42;
            dgvRiwayat.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorPrimary,
                ForeColor = ColorYellow,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            dgvRiwayat.EnableHeadersVisualStyles = false;
            dgvRiwayat.RowTemplate.Height = 40;
            dgvRiwayat.RowsDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9.75F),
                SelectionBackColor = ColorAccent,
                SelectionForeColor = ColorPrimary
            };
            dgvRiwayat.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 245, 255)
            };

            dgvRiwayat.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvRiwayat_CellFormatting);
        }

        private void IsiFilterStatus()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.AddRange(new object[] { "Semua Status", "Menunggu", "Diproses", "Selesai", "Dibatalkan" });
            cmbFilterStatus.SelectedIndex = 0;
        }

        // ===================================================
        // LOAD DATA DARI DATABASE
        // ===================================================

        private void LoadDataRiwayat()
        {
            lblStatus.Text = "⟳ Memuat riwayat pesanan...";
            lblStatus.ForeColor = Color.Gray;
            Application.DoEvents();

            try
            {
                if (_currentUser == null)
                {
                    TampilkanPesan("⚠ Tidak ada sesi pengguna aktif.", ColorDanger);
                    return;
                }

                // Ambil semua transaksi dari database
                _listTransaksi = _transactionController.GetTransaksiByPembeli(_currentUser.GetIdUser());

                TerapkanFilter();

                // Update summary
                AkhirkanSummary();

                lblStatus.Text = "✔ Data dimuat — " + DateTime.Now.ToString("HH:mm:ss");
                lblStatus.ForeColor = ColorSuccess;
            }
            catch (Exception ex)
            {
                TampilkanPesan("⚠ Gagal memuat riwayat pesanan: " + ex.Message, ColorDanger);
                lblStatus.Text = "⚠ Error: " + ex.Message;
                lblStatus.ForeColor = ColorDanger;
            }
        }

        // ===================================================
        // FILTER & BINDING KE DGV
        // ===================================================

        private void TerapkanFilter()
        {
            if (_listTransaksi == null) return;

            string filter = cmbFilterStatus.SelectedItem?.ToString() ?? "Semua Status";

            DataTable dt = new DataTable();
            dt.Columns.Add("IdTransaksi", typeof(int));
            dt.Columns.Add("Tanggal", typeof(string));
            dt.Columns.Add("TotalHarga", typeof(long));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("JumlahItem", typeof(int));

            int noUrut = 1;
            foreach (TrxModel trx in _listTransaksi)
            {
                string status = trx.GetStatus();
                if (filter != "Semua Status" && status != filter) continue;

                int jumlahItem = trx.GetSemuaDetail().Count;
                dt.Rows.Add(
                    trx.GetIdTransaksi(),
                    trx.GetTanggalTransaksi().ToString("dd MMM yyyy HH:mm"),
                    trx.HitungTotal(),
                    status,
                    jumlahItem
                );
            }

            dgvRiwayat.DataSource = dt;

            // Isi kolom NoUrut secara manual
            for (int i = 0; i < dgvRiwayat.Rows.Count; i++)
            {
                dgvRiwayat.Rows[i].Cells["NoUrut"].Value = i + 1;
            }

            lblJumlahData.Text = "Menampilkan " + dt.Rows.Count + " dari " + _listTransaksi.Count + " transaksi";
        }

        private void AkhirkanSummary()
        {
            long totalSelesai = 0;
            int cntSelesai = 0, cntProses = 0, cntTunggu = 0;

            foreach (TrxModel trx in _listTransaksi)
            {
                switch (trx.GetStatus())
                {
                    case "Selesai":
                        totalSelesai += trx.HitungTotal();
                        cntSelesai++;
                        break;
                    case "Diproses": cntProses++; break;
                    case "Menunggu": cntTunggu++; break;
                }
            }

            lblSummarySelesai.Text = "✔ Selesai: " + cntSelesai;
            lblSummaryProses.Text = "🕐 Diproses: " + cntProses;
            lblSummaryTunggu.Text = "⏳ Menunggu: " + cntTunggu;
            lblSummaryTotal.Text = "Rp " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", totalSelesai).Replace(",", ".");
        }

        // ===================================================
        // AKSI TOMBOL
        // ===================================================

        private void HandleKonfirmasi(int rowIndex)
        {
            string status = dgvRiwayat.Rows[rowIndex].Cells["Status"].Value?.ToString() ?? "";

            if (status != "Diproses" && status != "Dikirim")
            {
                MessageBox.Show(
                    "Konfirmasi penerimaan hanya dapat dilakukan jika pesanan sedang dalam status 'Diproses' atau 'Dikirim'.\n\nStatus saat ini: " + status,
                    "Tidak Dapat Dikonfirmasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int idTransaksi = Convert.ToInt32(dgvRiwayat.Rows[rowIndex].Cells["IdTransaksi"].Value);

            DialogResult dr = MessageBox.Show(
                "Apakah Anda menyatakan pesanan ini sudah diterima dengan baik?\n\nID Transaksi: #" + idTransaksi,
                "Konfirmasi Penerimaan Pesanan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                var (sukses, pesan) = _transactionController.UbahStatusPesanan(idTransaksi, "Selesai");

                if (sukses)
                {
                    MessageBox.Show(
                        "Terima kasih! Transaksi #" + idTransaksi + " telah ditandai Selesai.",
                        "Konfirmasi Berhasil",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadDataRiwayat();
                }
                else
                {
                    MessageBox.Show(pesan, "Konfirmasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleLihatDetail(int rowIndex)
        {
            int idTransaksi = Convert.ToInt32(dgvRiwayat.Rows[rowIndex].Cells["IdTransaksi"].Value);

            try
            {
                TrxModel trx = _transactionController.GetDetailTransaksi(idTransaksi);
                if (trx == null)
                {
                    MessageBox.Show("Data transaksi tidak ditemukan.", "Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TampilkanPopupDetail(trx);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat detail transaksi:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TampilkanPopupDetail(TrxModel trx)
        {
            Form popup = new Form
            {
                Text = "Detail Transaksi #" + trx.GetIdTransaksi(),
                Size = new Size(620, 420),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(248, 249, 250),
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lblJudul = new Label
            {
                Text = "Detail Pesanan #" + trx.GetIdTransaksi(),
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = ColorPrimary,
                Location = new Point(20, 15),
                AutoSize = true
            };

            Label lblInfo = new Label
            {
                Text = "Tanggal: " + trx.GetTanggalTransaksi().ToString("dd MMM yyyy HH:mm")
                     + "   |   Status: " + trx.GetStatus()
                     + "   |   Total: Rp " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", trx.HitungTotal()).Replace(",", "."),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.DimGray,
                Location = new Point(22, 45),
                AutoSize = true
            };

            DataGridView dgvDetail = new DataGridView
            {
                Location = new Point(15, 75),
                Size = new Size(578, 250),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowTemplate = { Height = 36 }
            };

            dgvDetail.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorPrimary,
                ForeColor = ColorYellow,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dgvDetail.EnableHeadersVisualStyles = false;
            dgvDetail.ColumnHeadersHeight = 38;

            dgvDetail.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nama Produk", DataPropertyName = "NamaProduk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvDetail.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Penitip", DataPropertyName = "NamaPenitip", Width = 130 });
            dgvDetail.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Jumlah", DataPropertyName = "Jumlah", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Subtotal (Rp)",
                DataPropertyName = "Subtotal",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            DataTable dtDetail = new DataTable();
            dtDetail.Columns.Add("NamaProduk", typeof(string));
            dtDetail.Columns.Add("NamaPenitip", typeof(string));
            dtDetail.Columns.Add("Jumlah", typeof(int));
            dtDetail.Columns.Add("Subtotal", typeof(long));

            foreach (TrxDetailModel d in trx.GetSemuaDetail())
            {
                dtDetail.Rows.Add(
                    d.GetNamaProdukSnapshot() ?? "(Produk #" + d.GetIdProduk() + ")",
                    d.GetNamaPenitip(),
                    d.GetJumlahPesanan(),
                    d.HitungTotal()
                );
            }
            dgvDetail.DataSource = dtDetail;

            Button btnTutup = new Button
            {
                Text = "Tutup",
                Location = new Point(490, 345),
                Size = new Size(100, 34),
                BackColor = ColorPrimary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnTutup.FlatAppearance.BorderSize = 0;
            btnTutup.Click += (s, ev) => popup.Close();

            popup.Controls.AddRange(new System.Windows.Forms.Control[] { lblJudul, lblInfo, dgvDetail, btnTutup });
            popup.ShowDialog(this);
        }

        // ===================================================
        // HELPER
        // ===================================================

        private void TampilkanPesan(string pesan, Color warnaFg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Keterangan");
            dt.Rows.Add(pesan);
            dgvRiwayat.AutoGenerateColumns = false;
            dgvRiwayat.Columns.Clear();
            dgvRiwayat.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Keterangan", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRiwayat.DataSource = dt;
        }
    }
}
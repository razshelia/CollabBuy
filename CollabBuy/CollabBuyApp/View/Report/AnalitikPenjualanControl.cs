using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Report
{
    /// <summary>
    /// Control Analitik Penjualan — tersinkronisasi penuh dengan database.
    /// Menampilkan: ringkasan KPI, laporan keuangan ROLLUP, top produk, 
    /// leaderboard penjual, dan produk kuota kritis.
    /// </summary>
    public partial class AnalitikPenjualanControl : UserControl
    {
        private readonly LaporanController _laporanController;
        private readonly User _currentUser;

        // Indeks tab aktif
        private int _tabAktif = 0;

        // Warna tema
        private static readonly Color ColorPrimary = Color.FromArgb(36, 0, 70);
        private static readonly Color ColorAccent = Color.FromArgb(200, 182, 255);
        private static readonly Color ColorYellow = Color.FromArgb(253, 255, 182);
        private static readonly Color ColorBg = Color.FromArgb(248, 249, 250);
        private static readonly Color ColorWhite = Color.White;
        private static readonly Color ColorSuccess = Color.FromArgb(40, 167, 69);
        private static readonly Color ColorWarning = Color.FromArgb(255, 193, 7);
        private static readonly Color ColorDanger = Color.FromArgb(220, 53, 69);

        public AnalitikPenjualanControl(User seller)
        {
            InitializeComponent();
            _currentUser = seller;
            _laporanController = new LaporanController();
        }

        // ===================================================
        // EVENT HANDLERS
        // ===================================================

        private void AnalitikPenjualanControl_Load(object sender, EventArgs e)
        {
            SetupDgvStyles(dgvLaporan);
            MuatSemua();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MuatSemua();
        }

        private void btnTab0_Click(object sender, EventArgs e) { TampilkanTab(0); }
        private void btnTab1_Click(object sender, EventArgs e) { TampilkanTab(1); }
        private void btnTab2_Click(object sender, EventArgs e) { TampilkanTab(2); }
        private void btnTab3_Click(object sender, EventArgs e) { TampilkanTab(3); }

        // ===================================================
        // LOGIKA NAVIGASI TAB
        // ===================================================

        private void TampilkanTab(int tabIndex)
        {
            _tabAktif = tabIndex;
            UpdateTabStyle();

            switch (tabIndex)
            {
                case 0: MuatRingkasanKPI(); break;
                case 1: MuatLaporanKeuangan(); break;
                case 2: MuatTopProduk(); break;
                case 3: MuatLeaderboard(); break;
            }
        }

        private void UpdateTabStyle()
        {
            Button[] tabs = { btnTab0, btnTab1, btnTab2, btnTab3 };
            for (int i = 0; i < tabs.Length; i++)
            {
                if (i == _tabAktif)
                {
                    tabs[i].BackColor = ColorPrimary;
                    tabs[i].ForeColor = ColorYellow;
                    tabs[i].Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else
                {
                    tabs[i].BackColor = ColorAccent;
                    tabs[i].ForeColor = ColorPrimary;
                    tabs[i].Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                }
            }
        }

        // ===================================================
        // MUAT DATA UTAMA
        // ===================================================

        private void MuatSemua()
        {
            lblStatus.Text = "⟳ Memuat data...";
            lblStatus.ForeColor = Color.Gray;
            Application.DoEvents();

            MuatKartuKPI();
            TampilkanTab(_tabAktif);

            lblStatus.Text = "✔ Data berhasil dimuat — " + DateTime.Now.ToString("HH:mm:ss");
            lblStatus.ForeColor = ColorSuccess;
        }

        // ===================================================
        // TAB 0 — KARTU KPI RINGKASAN (dari fn_statistik_dashboard_penjual)
        // ===================================================

        private void MuatKartuKPI()
        {
            try
            {
                DataTable dt = _laporanController.GetStatistikDashboardPenjual(_currentUser.GetIdUser());

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblValPendapatan.Text = "Rp " + FormatRupiah(GetLong(row, "total_pendapatan"));
                    lblValTransaksi.Text = GetLong(row, "jumlah_transaksi_selesai").ToString() + " Transaksi";
                    lblValProduk.Text = GetLong(row, "jumlah_produk_aktif").ToString() + " Produk";
                    lblValKuotaKritis.Text = GetLong(row, "produk_kuota_kritis").ToString() + " Produk";
                }
                else
                {
                    // Fallback jika fn DB belum ada / kosong
                    lblValPendapatan.Text = "Rp 0";
                    lblValTransaksi.Text = "0 Transaksi";
                    lblValProduk.Text = "0 Produk";
                    lblValKuotaKritis.Text = "0 Produk";
                }

                // Warnai kuota kritis merah jika ada
                long kritis = (dt != null && dt.Rows.Count > 0) ? GetLong(dt.Rows[0], "produk_kuota_kritis") : 0;
                pnlKuotaKritis.BackColor = kritis > 0 ? Color.FromArgb(255, 230, 230) : ColorWhite;
                lblValKuotaKritis.ForeColor = kritis > 0 ? ColorDanger : Color.FromArgb(36, 0, 70);
            }
            catch (Exception ex)
            {
                lblValPendapatan.Text = "Error";
                lblStatus.Text = "⚠ Gagal muat KPI: " + ex.Message;
                lblStatus.ForeColor = ColorDanger;
            }
        }

        private void MuatRingkasanKPI()
        {
            try
            {
                // Tampilkan produk kuota kritis di tab ini
                DataTable dt = _laporanController.GetProdukSisaKuotaKritis();
                SetupDgvKuotaKritis(dgvLaporan);
                dgvLaporan.DataSource = dt ?? new DataTable();
                lblDeskripsiTab.Text = "Produk dengan sisa kuota ≤ 5 unit — perlu segera ditindaklanjuti.";
            }
            catch (Exception ex)
            {
                TampilkanPesanErrorDgv("Gagal muat data kuota kritis: " + ex.Message);
            }
        }

        // ===================================================
        // TAB 1 — LAPORAN KEUANGAN (ROLLUP per Bulan/Tahun)
        // ===================================================

        private void MuatLaporanKeuangan()
        {
            try
            {
                DataTable dt = _laporanController.GetLaporanKeuanganRollup();
                SetupDgvLaporanKeuangan(dgvLaporan);

                // Format kolom omzet sebagai rupiah sebelum bind
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["tahun"] == DBNull.Value) row["tahun"] = "TOTAL";
                        if (row["bulan"] == DBNull.Value) row["bulan"] = "—";
                    }
                }

                dgvLaporan.DataSource = dt ?? new DataTable();
                lblDeskripsiTab.Text = "Laporan keuangan ROLLUP — total omzet per bulan dan per tahun dari transaksi selesai.";
            }
            catch (Exception ex)
            {
                TampilkanPesanErrorDgv("Gagal muat laporan keuangan: " + ex.Message);
            }
        }

        // ===================================================
        // TAB 2 — TOP PRODUK TERJUAL
        // ===================================================

        private void MuatTopProduk()
        {
            try
            {
                DataTable dt = _laporanController.GetTotalBarangTerjual();
                SetupDgvTopProduk(dgvLaporan);
                dgvLaporan.DataSource = dt ?? new DataTable();
                lblDeskripsiTab.Text = "Peringkat produk berdasarkan total unit yang sudah terjual.";
            }
            catch (Exception ex)
            {
                TampilkanPesanErrorDgv("Gagal muat top produk: " + ex.Message);
            }
        }

        // ===================================================
        // TAB 3 — LEADERBOARD PENJUAL
        // ===================================================

        private void MuatLeaderboard()
        {
            try
            {
                DataTable dt = _laporanController.GetKlasifikasiPerformaPenjual();
                SetupDgvLeaderboard(dgvLaporan);
                dgvLaporan.DataSource = dt ?? new DataTable();
                lblDeskripsiTab.Text = "Peringkat performa penjual berdasarkan total omzet — Newbie → Mid → Sultan.";
            }
            catch (Exception ex)
            {
                TampilkanPesanErrorDgv("Gagal muat leaderboard: " + ex.Message);
            }
        }

        // ===================================================
        // SETUP KOLOM DGV PER TAB
        // ===================================================

        private void SetupDgvKuotaKritis(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaProduk", HeaderText = "Nama Produk", DataPropertyName = "nama_produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "SisaKuota", HeaderText = "Target Kuota", DataPropertyName = "target_kuota", Width = 150 });
        }

        private void SetupDgvLaporanKeuangan(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tahun", HeaderText = "Tahun", DataPropertyName = "tahun", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Bulan", HeaderText = "Bulan", DataPropertyName = "bulan", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OmzetKotor",
                HeaderText = "Omzet Kotor (Rp)",
                DataPropertyName = "omzet_kotor",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9.75F, FontStyle.Bold) }
            });
        }

        private void SetupDgvTopProduk(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaProduk", HeaderText = "Nama Produk", DataPropertyName = "nama_produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalTerjual",
                HeaderText = "Total Terjual (Unit)",
                DataPropertyName = "total_terjual",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9.75F, FontStyle.Bold) }
            });
        }

        private void SetupDgvLeaderboard(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Penjual", DataPropertyName = "nama_penjual", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalOmzet",
                HeaderText = "Total Omzet (Rp)",
                DataPropertyName = "total_omzet",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tier", HeaderText = "Tier Penjual", DataPropertyName = "tier_penjual", Width = 230 });
        }

        // ===================================================
        // HELPER
        // ===================================================

        private void SetupDgvStyles(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = ColorWhite;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 38;
            dgv.EnableHeadersVisualStyles = false;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = ColorPrimary,
                ForeColor = ColorYellow,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv.ColumnHeadersHeight = 42;

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9.75F),
                SelectionBackColor = ColorAccent,
                SelectionForeColor = ColorPrimary
            };
            dgv.RowsDefaultCellStyle = rowStyle;

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 245, 255)
            };
        }

        private void TampilkanPesanErrorDgv(string pesan)
        {
            dgvLaporan.AutoGenerateColumns = false;
            dgvLaporan.Columns.Clear();
            dgvLaporan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Keterangan", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            DataTable dt = new DataTable();
            dt.Columns.Add("Keterangan");
            dt.Rows.Add("⚠ " + pesan);
            dgvLaporan.DataSource = dt;
            lblStatus.Text = "⚠ " + pesan;
            lblStatus.ForeColor = ColorDanger;
        }

        private long GetLong(DataRow row, string col)
        {
            try
            {
                if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
                    return Convert.ToInt64(row[col]);
            }
            catch { }
            return 0;
        }

        private string FormatRupiah(long nilai)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0:N0}", nilai).Replace(",", ".");
        }

        // ===================================================
        // FIELD DEKLARASI (dipakai Designer)
        // ===================================================
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDeskripsiTab;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTab0;
        private System.Windows.Forms.Button btnTab1;
        private System.Windows.Forms.Button btnTab2;
        private System.Windows.Forms.Button btnTab3;
        private System.Windows.Forms.Panel pnlKartuKPI;
        private System.Windows.Forms.Panel pnlPendapatan;
        private System.Windows.Forms.Panel pnlTransaksi;
        private System.Windows.Forms.Panel pnlProduk;
        private System.Windows.Forms.Panel pnlKuotaKritis;
        private System.Windows.Forms.Label lblValPendapatan;
        private System.Windows.Forms.Label lblValTransaksi;
        private System.Windows.Forms.Label lblValProduk;
        private System.Windows.Forms.Label lblValKuotaKritis;
        private System.Windows.Forms.Label lblTitlePendapatan;
        private System.Windows.Forms.Label lblTitleTransaksi;
        private System.Windows.Forms.Label lblTitleProduk;
        private System.Windows.Forms.Label lblTitleKuotaKritis;
        private System.Windows.Forms.Panel pnlTabBar;
        private System.Windows.Forms.DataGridView dgvLaporan;
    }
}
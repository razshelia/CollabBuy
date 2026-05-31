using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DashboardUserControl : UserControl
    {
        private readonly Models.User _user;
        private readonly TransactionController _trxCtrl;
        private readonly ProductController _prodCtrl;
        private readonly PreOrderController _poCtrl;

        // Event untuk navigasi antar halaman dari parent form
        public event Action OnNavigateKatalog;
        public event Action<int> OnNavigateDetailProduk;

        public DashboardUserControl(Models.User user)
        {
            InitializeComponent();
            _user = user;
            _trxCtrl = new TransactionController();
            _prodCtrl = new ProductController();
            _poCtrl = new PreOrderController();
        }

        private void DashboardUserControl_Load(object sender, EventArgs e)
        {
            lblSapaan.Text = $"Halo, {_user.GetNama()}! 👋";
            MuatStatistik();
            MuatKatalogTerbaru();
            AturLayout();
        }

        private void DashboardUserControl_Resize(object sender, EventArgs e)
        {
            AturLayout();
        }

        private void MuatStatistik()
        {
            try
            {
                // Pesanan aktif
                int totalPesanan = _trxCtrl.GetTotalPesananAktif(_user.GetIdUser());
                lblValPesanan.Text = totalPesanan.ToString();
            }
            catch { lblValPesanan.Text = "0"; }

            try
            {
                // PO tersedia (aktif di sistem)
                int poAktif = _poCtrl.GetJumlahPoAktif();
                lblValSaldo.Text = poAktif.ToString();
            }
            catch { lblValSaldo.Text = "0"; }

            // Item keranjang selalu 0 karena keranjang di RAM (per-sesi)
            lblValKeranjang.Text = "0";
        }

        private void MuatKatalogTerbaru()
        {
            try
            {
                DataTable dt = _prodCtrl.GetKatalogAktifDashboard(10);
                DataTable dtUI = BangunTabelUI(dt);
                dgvKatalog.DataSource = dtUI;
                dgvKatalog.ClearSelection();
            }
            catch { /* biarkan grid kosong */ }
        }

        /// <summary>
        /// Membangun DataTable UI dengan kolom tambahan "slot_tersedia" dan "harga_display"
        /// sehingga label teks muat dengan sempurna di dalam kolom grid.
        /// </summary>
        private DataTable BangunTabelUI(DataTable dt)
        {
            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("id_produk", typeof(int));
            dtUI.Columns.Add("nama_produk", typeof(string));
            dtUI.Columns.Add("nama_penjual", typeof(string));
            dtUI.Columns.Add("harga_display", typeof(string));
            dtUI.Columns.Add("slot_tersedia", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int idProduk = Convert.ToInt32(row["id_produk"]);
                string namaProduk = row["nama_produk"]?.ToString() ?? "-";
                string namaPenjual = row["nama_penjual"]?.ToString() ?? "-";

                // Format harga
                long harga = 0;
                if (dt.Columns.Contains("harga_dasar"))
                    long.TryParse(row["harga_dasar"]?.ToString(), out harga);
                string hargaDisplay = "Rp " + harga.ToString("N0");

                // Format slot tersedia
                string slotText = "Bebas";
                if (dt.Columns.Contains("target_kuota") && dt.Columns.Contains("terpesan"))
                {
                    object kuotaObj = row["target_kuota"];
                    if (kuotaObj != DBNull.Value && kuotaObj != null)
                    {
                        int targetKuota = Convert.ToInt32(kuotaObj);
                        int terpesan = 0;
                        if (row["terpesan"] != DBNull.Value)
                            terpesan = Convert.ToInt32(row["terpesan"]);
                        int sisa = targetKuota - terpesan;
                        slotText = sisa > 0 ? $"{sisa} slot" : "Penuh";
                    }
                }

                dtUI.Rows.Add(idProduk, namaProduk, namaPenjual, hargaDisplay, slotText);
            }

            return dtUI;
        }

        private void dgvKatalog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Kolom terakhir = tombol "Detail"
            if (e.ColumnIndex == dgvKatalog.Columns["colDetail"].Index)
            {

                // Ambil id_produk dari data source
                DataTable dt = dgvKatalog.DataSource as DataTable;
                if (dt != null && e.RowIndex < dt.Rows.Count)
                {
                    int id = Convert.ToInt32(dt.Rows[e.RowIndex]["id_produk"]);
                    OnNavigateDetailProduk?.Invoke(id);
                }
            }
        }

        private void btnLihatSemua_Click(object sender, EventArgs e)
        {
            OnNavigateKatalog?.Invoke();
        }

        private void AturLayout()
        {
            int margin = 30;
            int w = this.Width > 0 ? this.Width : 980;

            // Atur lebar kartu secara proporsional (maks 3 kartu, min 160)
            int cardW = Math.Max(160, (w - margin * 2 - 40) / 3);
            int cardH = 110;
            int cardTop = 110;

            pnlPesanan.SetBounds(margin, cardTop, cardW, cardH);
            pnlKeranjang.SetBounds(margin + cardW + 20, cardTop, cardW, cardH);
            pnlSaldo.SetBounds(margin + (cardW + 20) * 2, cardTop, cardW, cardH);

            // Tombol "Lihat Semua"
            btnLihatSemua.Location = new System.Drawing.Point(w - margin - btnLihatSemua.Width, 244);

            // Panel katalog — lebar penuh minus margin kanan
            int katalogTop = 290;
            int katalogH = Math.Max(200, this.Height - katalogTop - margin);
            pnlKatalog.SetBounds(margin, katalogTop, w - margin * 2, katalogH);
            dgvKatalog.SetBounds(2, 2, pnlKatalog.Width - 4, pnlKatalog.Height - 4);
        }
    }
}
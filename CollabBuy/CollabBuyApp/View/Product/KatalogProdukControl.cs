using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.User
{
    public partial class KatalogProdukControl : UserControl
    {
        private readonly Models.User _user;
        private readonly ProductController _prodCtrl;
        private readonly TransactionController _trxCtrl;

        private DataTable _dtSemua; // Cache data mentah dari DB
        private System.Windows.Forms.Timer _timerInfo;

        // Event navigasi
        public event Action<int> OnNavigateDetailProduk;
        public event Action OnNavigateKeranjang;

        public KatalogProdukControl(Models.User user)
        {
            InitializeComponent();
            _user = user;
            _prodCtrl = new ProductController();
            _trxCtrl = new TransactionController(_user.GetIdUser());

            // Timer otomatis sembunyikan lblInfo setelah 3 detik
            _timerInfo = new System.Windows.Forms.Timer();
            _timerInfo.Interval = 3000;
            _timerInfo.Tick += (s, e) => { lblInfo.Visible = false; _timerInfo.Stop(); };
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
            MuatKatalog();
            AturLayout();
        }

        private void KatalogProdukControl_Resize(object sender, EventArgs e)
        {
            AturLayout();
        }

        private void MuatKatalog()
        {
            try
            {
                DataTable dt = _prodCtrl.GetKatalogUtama();
                _dtSemua = dt;
                TampilkanKatalog(dt);
            }
            catch (Exception ex)
            {
                TampilkanInfo($"Gagal memuat katalog: {ex.Message}", false);
            }
        }

        private void TampilkanKatalog(DataTable dt)
        {
            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("id_produk", typeof(int));
            dtUI.Columns.Add("nama_produk", typeof(string));
            dtUI.Columns.Add("nama_penjual", typeof(string));
            dtUI.Columns.Add("harga_display", typeof(string));
            dtUI.Columns.Add("slot_tersedia", typeof(string));
            dtUI.Columns.Add("tipe_po", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int idProduk = 0;
                if (dt.Columns.Contains("id_produk"))
                    int.TryParse(row["id_produk"]?.ToString(), out idProduk);

                string namaProduk = dt.Columns.Contains("nama_produk") ? row["nama_produk"]?.ToString() ?? "-" : "-";
                string namaPenjual = dt.Columns.Contains("nama_penjual") ? row["nama_penjual"]?.ToString() ?? "-" : "-";

                // Harga
                long harga = 0;
                if (dt.Columns.Contains("harga_dasar"))
                    long.TryParse(row["harga_dasar"]?.ToString(), out harga);
                string hargaDisplay = "Rp " + harga.ToString("N0");

                // Slot tersedia
                string slotText = "Bebas";
                if (dt.Columns.Contains("target_kuota") && row["target_kuota"] != DBNull.Value && row["target_kuota"] != null)
                {
                    int targetKuota = Convert.ToInt32(row["target_kuota"]);
                    int terpesan = 0;
                    if (dt.Columns.Contains("terpesan") && row["terpesan"] != DBNull.Value)
                        terpesan = Convert.ToInt32(row["terpesan"]);
                    int sisa = targetKuota - terpesan;
                    slotText = sisa > 0 ? $"{sisa} slot" : "⛔ Penuh";
                }

                // Tipe PO
                string tipePo = "Reguler";
                if (dt.Columns.Contains("jenis_po") && row["jenis_po"] != DBNull.Value)
                    tipePo = row["jenis_po"]?.ToString() ?? "Reguler";

                dtUI.Rows.Add(idProduk, namaProduk, namaPenjual, hargaDisplay, slotText, tipePo);
            }

            dgvKatalog.DataSource = dtUI;
            dgvKatalog.ClearSelection();
        }

        private void dgvKatalog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataTable dt = dgvKatalog.DataSource as DataTable;
            if (dt == null || e.RowIndex >= dt.Rows.Count) return;

            int idProduk = Convert.ToInt32(dt.Rows[e.RowIndex]["id_produk"]);

            // Tombol "Lihat Detail" — arahkan ke halaman detail
            if (e.ColumnIndex == dgvKatalog.Columns["colDetail"].Index)
            {
                OnNavigateDetailProduk?.Invoke(idProduk);
                return;
            }

            // Tombol "+ Keranjang" — masukkan ke keranjang langsung dengan qty=1
            if (e.ColumnIndex == dgvKatalog.Columns["colKeranjang"].Index)
            {
                string namaProduk = dt.Rows[e.RowIndex]["nama_produk"]?.ToString() ?? "Produk";
                var (sukses, pesan) = _trxCtrl.TambahItemKeKeranjang(idProduk, _user.GetNama(), 1, "");
                if (sukses)
                {
                    TampilkanInfo($"✅ '{namaProduk}' berhasil masuk keranjang!", true);
                }
                else
                {
                    TampilkanInfo($"❌ {pesan}", false);
                }
            }
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            lblInfo.Text = pesan;
            lblInfo.BackColor = sukses
                ? System.Drawing.Color.FromArgb(210, 255, 230)
                : System.Drawing.Color.FromArgb(255, 220, 220);
            lblInfo.ForeColor = sukses
                ? System.Drawing.Color.FromArgb(0, 100, 50)
                : System.Drawing.Color.FromArgb(150, 0, 0);
            lblInfo.Visible = true;
            _timerInfo.Stop();
            _timerInfo.Start();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            FilterKatalog(txtCari.Text.Trim());
        }

        private void txtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                FilterKatalog(txtCari.Text.Trim());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCari.Text = "";
            if (_dtSemua != null)
                TampilkanKatalog(_dtSemua);
        }

        private void FilterKatalog(string kata)
        {
            if (_dtSemua == null) return;
            if (string.IsNullOrEmpty(kata))
            {
                TampilkanKatalog(_dtSemua);
                return;
            }

            DataTable dtFilter = _dtSemua.Clone();
            foreach (DataRow row in _dtSemua.Rows)
            {
                bool cocok = false;
                foreach (DataColumn col in _dtSemua.Columns)
                {
                    if (row[col]?.ToString().ToLower().Contains(kata.ToLower()) == true)
                    {
                        cocok = true;
                        break;
                    }
                }
                if (cocok) dtFilter.ImportRow(row);
            }
            TampilkanKatalog(dtFilter);
        }

        private void AturLayout()
        {
            int margin = 30;
            int w = this.Width > 0 ? this.Width : 980;

            // Filter panel
            pnlFilter.SetBounds(0, 90, w, 68);

            // Label info
            lblInfo.SetBounds(margin, 133, w - margin * 2, 26);

            // Panel katalog — isi sisa tinggi
            int katalogTop = 165;
            int katalogH = Math.Max(200, this.Height - katalogTop - margin);
            pnlKatalog.SetBounds(margin, katalogTop, w - margin * 2, katalogH);
            dgvKatalog.SetBounds(2, 2, pnlKatalog.Width - 4, pnlKatalog.Height - 4);
        }
    }
}
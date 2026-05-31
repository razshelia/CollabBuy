using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.User
{
    public partial class KeranjangBelanjaControl : UserControl
    {
        private readonly Models.User _user;
        private readonly TransactionController _trxCtrl;
        private System.Windows.Forms.Timer _timerInfo;

        // Event: checkout sekarang harus mengarah ke halaman pembayaran
        public event Action<long> OnNavigatePembayaran;

        public KeranjangBelanjaControl(Models.User user, TransactionController trxCtrl)
        {
            InitializeComponent();
            _user = user;
            _trxCtrl = trxCtrl;

            _timerInfo = new System.Windows.Forms.Timer();
            _timerInfo.Interval = 3500;
            _timerInfo.Tick += (s, e) => { lblInfo.Visible = false; _timerInfo.Stop(); };
        }

        private void KeranjangBelanjaControl_Load(object sender, EventArgs e)
        {
            MuatKeranjang();
            AturLayout();
        }

        private void KeranjangBelanjaControl_Resize(object sender, EventArgs e)
        {
            AturLayout();
        }

        public void MuatKeranjang()
        {
            try
            {
                DataTable dt = _trxCtrl.GetKeranjangDataTable();
                DataTable dtUI = BangunTabelUI(dt);
                dgvKeranjang.DataSource = dtUI;
                dgvKeranjang.ClearSelection();

                // Update total
                long total = _trxCtrl.HitungTotalKeranjangSaatIni();
                lblTotal.Text = "Rp " + total.ToString("N0");
            }
            catch (Exception ex)
            {
                TampilkanInfo($"Gagal memuat keranjang: {ex.Message}", false);
            }
        }

        private DataTable BangunTabelUI(DataTable dt)
        {
            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("IdProduk", typeof(int));
            dtUI.Columns.Add("NamaItem", typeof(string));
            dtUI.Columns.Add("NamaPenitip", typeof(string));
            dtUI.Columns.Add("Catatan", typeof(string));
            dtUI.Columns.Add("HargaDisplay", typeof(string));
            dtUI.Columns.Add("Kuantitas", typeof(int));
            dtUI.Columns.Add("SubtotalDisplay", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int idProduk = dt.Columns.Contains("IdProduk") ? Convert.ToInt32(row["IdProduk"]) : 0;
                string nama = dt.Columns.Contains("NamaItem") ? row["NamaItem"]?.ToString() ?? "-" : "-";
                string penitip = dt.Columns.Contains("NamaPenitip") ? row["NamaPenitip"]?.ToString() ?? "-" : "-";
                string catatan = dt.Columns.Contains("Catatan") ? row["Catatan"]?.ToString() ?? "-" : "-";

                long harga = 0;
                int qty = 1;
                if (dt.Columns.Contains("Harga")) long.TryParse(row["Harga"]?.ToString(), out harga);
                if (dt.Columns.Contains("Kuantitas")) int.TryParse(row["Kuantitas"]?.ToString(), out qty);

                long subtotal = harga * qty;

                dtUI.Rows.Add(
                    idProduk,
                    nama,
                    penitip,
                    catatan,
                    "Rp " + harga.ToString("N0"),
                    qty,
                    "Rp " + subtotal.ToString("N0")
                );
            }

            return dtUI;
        }

        // Checkout mengarah ke halaman Pembayaran — BUKAN langsung proses
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            long total = _trxCtrl.HitungTotalKeranjangSaatIni();

            if (total <= 0)
            {
                TampilkanInfo("⚠️ Keranjang masih kosong! Tambahkan produk dulu ya.", false);
                return;
            }

            // Navigasi ke halaman pembayaran dengan total tagihan
            OnNavigatePembayaran?.Invoke(total);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvKeranjang.SelectedRows.Count == 0)
            {
                TampilkanInfo("⚠️ Pilih item yang ingin dihapus dulu!", false);
                return;
            }

            DataTable dt = dgvKeranjang.DataSource as DataTable;
            if (dt == null) return;

            int rowIdx = dgvKeranjang.SelectedRows[0].Index;
            int idProduk = Convert.ToInt32(dt.Rows[rowIdx]["IdProduk"]);
            string namaPenitip = dt.Rows[rowIdx]["NamaPenitip"]?.ToString() ?? "";

            _trxCtrl.HapusItemKeranjang(idProduk, namaPenitip);
            TampilkanInfo("✅ Item berhasil dihapus dari keranjang.", true);
            MuatKeranjang();
        }

        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            DialogResult hasil = MessageBox.Show(
                "Yakin mau kosongkan semua item di keranjang?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (hasil == DialogResult.Yes)
            {
                _trxCtrl.KosongkanKeranjang();
                TampilkanInfo("✅ Keranjang berhasil dikosongkan.", true);
                MuatKeranjang();
            }
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            lblInfo.Text = pesan;
            lblInfo.BackColor = sukses
                ? Color.FromArgb(210, 255, 230)
                : Color.FromArgb(255, 220, 220);
            lblInfo.ForeColor = sukses
                ? Color.FromArgb(0, 100, 50)
                : Color.FromArgb(150, 0, 0);
            lblInfo.Size = new System.Drawing.Size(lblInfo.Width, 26);
            lblInfo.Visible = true;
            _timerInfo.Stop();
            _timerInfo.Start();
        }

        private void AturLayout()
        {
            int margin = 30;
            int w = this.Width > 0 ? this.Width : 980;
            int bottomH = 80;
            int infoH = lblInfo.Visible ? 28 : 0;

            // Panel info
            lblInfo.SetBounds(margin, 97, w - margin * 2, 26);

            // Panel grid — isi tengah
            int gridTop = 106;
            int gridBottom = this.Height - bottomH - margin;
            int gridH = Math.Max(100, gridBottom - gridTop);
            pnlGrid.SetBounds(margin, gridTop, w - margin * 2, gridH);
            dgvKeranjang.SetBounds(2, 2, pnlGrid.Width - 4, pnlGrid.Height - 4);

            // Panel bottom — selalu di bawah
            pnlBottom.SetBounds(0, this.Height - bottomH, w, bottomH);

            // Tombol checkout selalu di kanan
            btnCheckout.Location = new Point(pnlBottom.Width - btnCheckout.Width - margin, 15);

            // Tombol hapus & kosongkan di tengah
            int tengah = (pnlBottom.Width - btnKosongkan.Width - btnHapus.Width - 10) / 2;
            btnKosongkan.Location = new Point(tengah, 22);
            btnHapus.Location = new Point(tengah + btnKosongkan.Width + 10, 22);

            // Total label
            lblTotalLabel.Location = new Point(20, 28);
            lblTotal.Location = new Point(148, 24);
        }
    }
}
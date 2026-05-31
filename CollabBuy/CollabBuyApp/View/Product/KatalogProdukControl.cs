using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class KatalogProdukControl : UserControl
    {
        private readonly Models.User _user;
        private readonly ProductController _prodCtrl;
        private readonly TransactionController _trxCtrl;

        private DataTable _dtSemua;
        private System.Windows.Forms.Timer _timerInfo;

        // Events
        public event Action<int> OnNavigateDetailProduk;
        public event Action OnNavigateKeranjang;

        // Card sizing
        private const int CARD_W = 220;
        private const int CARD_H = 210;

        public KatalogProdukControl(Models.User user)
        {
            InitializeComponent();
            _user = user;
            _prodCtrl = new ProductController();
            _trxCtrl = new TransactionController(_user.GetIdUser());

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
                _dtSemua = _prodCtrl.GetKatalogUtama();
                TampilkanKartu(_dtSemua);
            }
            catch (Exception ex)
            {
                TampilkanInfo($"Gagal memuat katalog: {ex.Message}", false);
            }
        }

        private void TampilkanKartu(DataTable dt)
        {
            flpKartu.SuspendLayout();
            flpKartu.Controls.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                var lblKosong = new Label
                {
                    Text = "😔  Belum ada produk tersedia saat ini.",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.FromArgb(130, 80, 180),
                    AutoSize = true,
                    Margin = new Padding(20)
                };
                flpKartu.Controls.Add(lblKosong);
                flpKartu.ResumeLayout();
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                int idProduk = 0;
                if (dt.Columns.Contains("id_produk")) int.TryParse(row["id_produk"]?.ToString(), out idProduk);

                string nama = dt.Columns.Contains("nama_produk") ? row["nama_produk"]?.ToString() ?? "-" : "-";
                string penjual = dt.Columns.Contains("nama_penjual") ? row["nama_penjual"]?.ToString() ?? "-" : "-";

                long harga = 0;
                if (dt.Columns.Contains("harga_dasar")) long.TryParse(row["harga_dasar"]?.ToString(), out harga);
                string hargaStr = "Rp " + harga.ToString("N0");

                string slot = "Bebas";
                if (dt.Columns.Contains("target_kuota") && row["target_kuota"] != DBNull.Value)
                {
                    int kuota = Convert.ToInt32(row["target_kuota"]);
                    int terpesan = 0;
                    if (dt.Columns.Contains("terpesan") && row["terpesan"] != DBNull.Value)
                        terpesan = Convert.ToInt32(row["terpesan"]);
                    int sisa = kuota - terpesan;
                    slot = sisa > 0 ? $"{sisa} slot" : "⛔ Penuh";
                }

                string tipePo = "Reguler";
                if (dt.Columns.Contains("jenis_po") && row["jenis_po"] != DBNull.Value)
                    tipePo = row["jenis_po"]?.ToString() ?? "Reguler";

                flpKartu.Controls.Add(BuatKartu(idProduk, nama, penjual, hargaStr, slot, tipePo));
            }

            flpKartu.ResumeLayout();
        }

        private Panel BuatKartu(int idProduk, string nama, string penjual, string harga, string slot, string tipePo)
        {
            var pnl = new Panel
            {
                Size = new Size(CARD_W, CARD_H),
                BackColor = Color.White,
                Margin = new Padding(8),
                Cursor = Cursors.Default
            };

            // Shadow-like border
            pnl.Paint += (s, e) =>
            {
                using var pen = new System.Drawing.Pen(Color.FromArgb(220, 200, 255), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            // === Colour bar top ===
            var bar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 6,
                BackColor = Color.FromArgb(130, 80, 200)
            };
            pnl.Controls.Add(bar);

            // === Nama produk ===
            var lblNama = new Label
            {
                Text = nama,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 0, 90),
                Location = new Point(10, 16),
                Size = new Size(CARD_W - 20, 42),
                AutoSize = false
            };
            pnl.Controls.Add(lblNama);

            // === Penjual ===
            var lblPenjual = new Label
            {
                Text = "🏪 " + penjual,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(110, 80, 140),
                Location = new Point(10, 60),
                Size = new Size(CARD_W - 20, 18),
                AutoSize = false
            };
            pnl.Controls.Add(lblPenjual);

            // === Harga ===
            var lblHarga = new Label
            {
                Text = harga,
                Font = new Font("Segoe UI Black", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 0, 160),
                Location = new Point(10, 82),
                Size = new Size(CARD_W - 20, 28),
                AutoSize = false
            };
            pnl.Controls.Add(lblHarga);

            // === Slot badge ===
            bool penuh = slot.Contains("Penuh");
            var lblSlot = new Label
            {
                Text = slot,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = penuh ? Color.FromArgb(180, 0, 0) : Color.FromArgb(0, 110, 50),
                BackColor = penuh ? Color.FromArgb(255, 230, 230) : Color.FromArgb(210, 255, 230),
                Location = new Point(10, 114),
                Size = new Size(90, 20),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnl.Controls.Add(lblSlot);

            // === Tipe PO badge ===
            var lblTipe = new Label
            {
                Text = tipePo,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(60, 0, 100),
                BackColor = Color.FromArgb(230, 215, 255),
                Location = new Point(106, 114),
                Size = new Size(CARD_W - 120, 20),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnl.Controls.Add(lblTipe);

            // === Tombol Lihat Detail ===
            var btnDetail = new Button
            {
                Text = "Lihat Detail",
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(72, 0, 120),
                ForeColor = Color.FromArgb(254, 252, 200),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(10, 144),
                Size = new Size(96, 30)
            };
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += (s, e) => OnNavigateDetailProduk?.Invoke(idProduk);
            pnl.Controls.Add(btnDetail);

            // === Tombol + Keranjang ===
            var btnKeranjang = new Button
            {
                Text = "+ Keranjang",
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(254, 245, 100),
                ForeColor = Color.FromArgb(70, 50, 0),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(112, 144),
                Size = new Size(98, 30)
            };
            btnKeranjang.FlatAppearance.BorderSize = 0;
            btnKeranjang.Click += (s, e) =>
            {
                var (sukses, pesan) = _trxCtrl.TambahItemKeKeranjang(idProduk, _user.GetNama(), 1, "");
                TampilkanInfo(sukses ? $"✅ '{nama}' berhasil masuk keranjang!" : $"❌ {pesan}", sukses);
            };
            pnl.Controls.Add(btnKeranjang);

            return pnl;
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            lblInfo.Text = pesan;
            lblInfo.BackColor = sukses ? Color.FromArgb(210, 255, 230) : Color.FromArgb(255, 220, 220);
            lblInfo.ForeColor = sukses ? Color.FromArgb(0, 100, 50) : Color.FromArgb(150, 0, 0);
            lblInfo.Visible = true;
            _timerInfo.Stop();
            _timerInfo.Start();
        }

        private void btnCari_Click(object sender, EventArgs e) => FilterKatalog(txtCari.Text.Trim());
        private void txtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) FilterKatalog(txtCari.Text.Trim());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCari.Text = "";
            if (_dtSemua != null) TampilkanKartu(_dtSemua);
        }

        private void FilterKatalog(string kata)
        {
            if (_dtSemua == null) return;
            if (string.IsNullOrEmpty(kata)) { TampilkanKartu(_dtSemua); return; }

            DataTable dtF = _dtSemua.Clone();
            foreach (DataRow row in _dtSemua.Rows)
            {
                foreach (DataColumn col in _dtSemua.Columns)
                {
                    if (row[col]?.ToString().ToLower().Contains(kata.ToLower()) == true)
                    { dtF.ImportRow(row); break; }
                }
            }
            TampilkanKartu(dtF);
        }

        private void AturLayout()
        {
            int w = Math.Max(this.Width, 600);
            pnlFilter.Width = w;
            lblInfo.Width = w - 60;
            flpKartu.SetBounds(0, 190, w, Math.Max(300, this.Height - 190));
        }
    }
}

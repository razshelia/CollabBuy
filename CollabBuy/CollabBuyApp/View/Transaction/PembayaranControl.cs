using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using Npgsql;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class PembayaranControl : UserControl
    {
        private readonly Models.User _user;
        private readonly TransactionController _trxCtrl;
        private readonly long _totalTagihan;

        private byte[] _buktiBayar = null;
        private int _idTransaksiBaru = 0;
        private bool _checkoutSudahDilakukan = false;

        // Event navigasi
        public event Action OnNavigateKembali;
        public event Action<int> OnCheckoutBerhasil;

        public PembayaranControl(Models.User user, TransactionController trxCtrl, long totalTagihan)
        {
            InitializeComponent();
            _user = user;
            _trxCtrl = trxCtrl;
            _totalTagihan = totalTagihan;
            this.Dock = DockStyle.Fill;
        }

        private void PembayaranControl_Load(object sender, EventArgs e)
        {
            lblTotal.Text = "Rp " + _totalTagihan.ToString("N0");

            LoadInfoRekening();

            AturLayout();
        }

        private void LoadInfoRekening()
        {
            try
            {
                DataTable dtKeranjang = _trxCtrl.GetKeranjangDataTable();
                if (dtKeranjang.Rows.Count > 0)
                {
                    int idProdukPertama = Convert.ToInt32(dtKeranjang.Rows[0]["IdProduk"]);

                    ProductController pc = new ProductController();
                    Models.Product p = pc.GetProdukById(idProdukPertama);

                    if (p != null && p.GetIdPo().HasValue)
                    {
                        string infoRek = "";
                        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
                        using (var conn = new NpgsqlConnection(connStr))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand("SELECT info_rekening FROM preorders WHERE id_po = @id", conn))
                            {
                                cmd.Parameters.AddWithValue("@id", p.GetIdPo().Value);
                                var res = cmd.ExecuteScalar();
                                if (res != null && res != DBNull.Value) infoRek = res.ToString();
                            }
                        }
                        lblRekeningInfo.Text = string.IsNullOrEmpty(infoRek) ? "Rekening tidak ditemukan di sistem." : infoRek;
                    }
                    else
                    {
                        lblRekeningInfo.Text = "Barang jualan reguler (Tanpa PO). Silakan chat penjual untuk info transfer.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblRekeningInfo.Text = "Gagal memuat info rekening: " + ex.Message;
            }
        }

        private void PembayaranControl_Resize(object sender, EventArgs e)
        {
            AturLayout();
        }

        private void btnPilihFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Bukti Transfer";
                ofd.Filter = "Gambar|*.jpg;*.jpeg;*.png;*.bmp|Semua File|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _buktiBayar = File.ReadAllBytes(ofd.FileName);
                        lblNamaFile.Text = Path.GetFileName(ofd.FileName);

                        using (var ms = new MemoryStream(_buktiBayar))
                        {
                            picPreview.Image = Image.FromStream(ms);
                        }
                        picPreview.Visible = true;

                        if (_checkoutSudahDilakukan && _idTransaksiBaru > 0)
                        {
                            UploadBuktiBayar();
                        }
                    }
                    catch (Exception ex)
                    {
                        TampilkanStatus($"❌ Gagal membaca file: {ex.Message}", false);
                    }
                }
            }
        }

        private void btnKonfirmasiCheckout_Click(object sender, EventArgs e)
        {
            if (_checkoutSudahDilakukan)
            {
                TampilkanStatus("⚠️ Checkout udah diproses bestie, santai aja.", false);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                $"Udah yakin mau checkout?\n\nTotal Tagihan: Rp {_totalTagihan:N0}\n\nJangan lupa upload bukti transfer ya biar pesanan kamu divalidasi!",
                "Konfirmasi Checkout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (konfirmasi != DialogResult.Yes) return;

            var (sukses, pesan) = _trxCtrl.ProsesCheckout();

            if (!sukses)
            {
                TampilkanStatus($"❌ Checkout gagal: {pesan}", false);
                return;
            }

            _checkoutSudahDilakukan = true;
            try
            {
                string[] parts = pesan.Split(':');
                if (parts.Length >= 2)
                    int.TryParse(parts[parts.Length - 1].Trim(), out _idTransaksiBaru);
            }
            catch { }

            if (_idTransaksiBaru > 0)
            {
                txtIdTransaksi.Text = _idTransaksiBaru.ToString();
                txtIdTransaksi.Visible = true;
                lblIdTrxLabel.Visible = true;
                lblIdTrxHint.Visible = true;
            }

            if (_buktiBayar != null && _buktiBayar.Length > 0)
            {
                UploadBuktiBayar();
            }
            else
            {
                TampilkanStatus($"✅ Checkout berhasil! ID: {_idTransaksiBaru}. Buruan upload buktinya ya!", true);
            }

            btnKonfirmasiCheckout.Enabled = false;
            btnKonfirmasiCheckout.BackColor = Color.FromArgb(150, 150, 150);
            btnKonfirmasiCheckout.Text = "✅ Transaksi Diproses";

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 2500;
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                OnCheckoutBerhasil?.Invoke(_idTransaksiBaru);
            };
            timer.Start();
        }

        private void UploadBuktiBayar()
        {
            if (_idTransaksiBaru <= 0 || _buktiBayar == null) return;

            var (sukses, pesanUpload) = _trxCtrl.UploadBuktiBayar(_idTransaksiBaru, _buktiBayar, _user.GetIdUser());
            if (sukses)
                TampilkanStatus($"✅ Mantap! Checkout & resi berhasil di-upload!", true);
            else
                TampilkanStatus($"⚠️ Checkout sukses, tapi resi gagal di-upload: {pesanUpload}", false);
        }

        private void TampilkanStatus(string pesan, bool sukses)
        {
            lblStatus.Text = pesan;
            lblStatus.BackColor = sukses ? Color.FromArgb(210, 255, 230) : Color.FromArgb(255, 220, 220);
            lblStatus.ForeColor = sukses ? Color.FromArgb(0, 100, 50) : Color.FromArgb(150, 0, 0);
            lblStatus.Visible = true;
        }

        private void btnBatalKembali_Click(object sender, EventArgs e)
        {
            if (_checkoutSudahDilakukan) OnCheckoutBerhasil?.Invoke(_idTransaksiBaru);
            else OnNavigateKembali?.Invoke();
        }

        private void AturLayout()
        {
            int margin = 30;
            int w = this.Width > 0 ? this.Width : 980;
            int contentW = w - margin * 2;

            pnlRingkasan.SetBounds(margin, 20, Math.Min(560, contentW), 90);
            pnlRekening.SetBounds(margin, 126, Math.Min(560, contentW), 85);
            lblUploadTitle.Location = new Point(margin, 228);

            btnPilihFile.Location = new Point(margin, 258);

            // PERBAIKAN: Posisi dan ukuran label nama file biar lega & gak kepotong!
            lblNamaFile.AutoSize = false;
            lblNamaFile.AutoEllipsis = true; // Kalau kepanjangan diganti "..."
            lblNamaFile.Location = new Point(margin + btnPilihFile.Width + 20, 260);
            lblNamaFile.Size = new Size(contentW - btnPilihFile.Width - 40, 30);
            lblNamaFile.TextAlign = ContentAlignment.MiddleLeft;

            picPreview.Location = new Point(margin, 305);

            lblIdTrxLabel.Location = new Point(margin, 480);
            txtIdTransaksi.Location = new Point(margin + 210, 477);
            lblIdTrxHint.Location = new Point(margin, 510);

            btnBatalKembali.Location = new Point(margin, 540);
            btnKonfirmasiCheckout.Location = new Point(margin + btnBatalKembali.Width + 20, 540);

            lblStatus.SetBounds(margin, 605, Math.Min(700, contentW), 28);
        }
    }
}
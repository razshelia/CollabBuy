using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.User
{
    /// <summary>
    /// Halaman Pembayaran — muncul setelah user klik "Checkout Sekarang" dari Keranjang.
    /// Alur:
    ///   1. Tampilkan total tagihan
    ///   2. User upload bukti transfer (opsional, bisa belakangan)
    ///   3. User klik "Konfirmasi & Checkout" → proses transaksi ke DB
    ///   4. Tampilkan ID Transaksi hasil checkout
    ///   5. Event OnCheckoutBerhasil dipancarkan ke parent form
    /// </summary>
    public partial class PembayaranControl : UserControl
    {
        private readonly Models.User _user;
        private readonly TransactionController _trxCtrl;
        private readonly long _totalTagihan;

        private byte[] _buktiBayar = null;
        private int _idTransaksiBaru = 0;
        private bool _checkoutSudahDilakukan = false;

        // Event navigasi
        public event Action OnNavigateKembali;         // Kembali ke keranjang
        public event Action<int> OnCheckoutBerhasil;  // Checkout sukses, bawa id transaksi

        public PembayaranControl(Models.User user, TransactionController trxCtrl, long totalTagihan)
        {
            InitializeComponent();
            _user = user;
            _trxCtrl = trxCtrl;
            _totalTagihan = totalTagihan;
        }

        private void PembayaranControl_Load(object sender, EventArgs e)
        {
            lblTotal.Text = "Rp " + _totalTagihan.ToString("N0");
            lblRekeningInfo.Text =
                "💡 Info rekening tersedia di detail setiap PO / Produk penjual.\n" +
                "Pastikan kamu transfer ke rekening yang sesuai dengan produk yang dipesan.";
            AturLayout();
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

                        // Preview
                        using (var ms = new MemoryStream(_buktiBayar))
                        {
                            picPreview.Image = Image.FromStream(ms);
                        }
                        picPreview.Visible = true;

                        // Jika checkout sudah dilakukan, langsung upload bukti
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
                TampilkanStatus("⚠️ Checkout sudah dilakukan sebelumnya.", false);
                return;
            }

            // Konfirmasi ke user
            DialogResult konfirmasi = MessageBox.Show(
                $"Konfirmasi checkout?\n\nTotal: Rp {_totalTagihan:N0}\n\nPesanan akan diproses setelah pembayaran diverifikasi.",
                "Konfirmasi Checkout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (konfirmasi != DialogResult.Yes) return;

            // Proses checkout ke database
            var (sukses, pesan) = _trxCtrl.ProsesCheckout();

            if (!sukses)
            {
                TampilkanStatus($"❌ Checkout gagal: {pesan}", false);
                return;
            }

            // Ambil ID transaksi dari pesan (format: "Checkout berhasil! ID Transaksi Anda: 123")
            _checkoutSudahDilakukan = true;
            try
            {
                string[] parts = pesan.Split(':');
                if (parts.Length >= 2)
                    int.TryParse(parts[parts.Length - 1].Trim(), out _idTransaksiBaru);
            }
            catch { /* id tetap 0 */ }

            // Tampilkan ID transaksi
            if (_idTransaksiBaru > 0)
            {
                txtIdTransaksi.Text = _idTransaksiBaru.ToString();
                txtIdTransaksi.Visible = true;
                lblIdTrxLabel.Visible = true;
                lblIdTrxHint.Visible = true;
            }

            // Upload bukti jika sudah dipilih
            if (_buktiBayar != null && _buktiBayar.Length > 0)
            {
                UploadBuktiBayar();
            }
            else
            {
                TampilkanStatus(
                    $"✅ Checkout berhasil! ID Transaksi: {_idTransaksiBaru}. Jangan lupa upload bukti transfer ya!",
                    true
                );
            }

            // Ubah tombol
            btnKonfirmasiCheckout.Enabled = false;
            btnKonfirmasiCheckout.BackColor = Color.FromArgb(150, 150, 150);
            btnKonfirmasiCheckout.Text = "✅ Sudah Diproses";

            // Navigasi ke riwayat setelah 2,5 detik
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
                TampilkanStatus($"✅ Checkout & bukti bayar berhasil! ID Transaksi: {_idTransaksiBaru}", true);
            else
                TampilkanStatus($"⚠️ Checkout berhasil tapi gagal upload bukti: {pesanUpload}", false);
        }

        private void TampilkanStatus(string pesan, bool sukses)
        {
            lblStatus.Text = pesan;
            lblStatus.BackColor = sukses
                ? Color.FromArgb(210, 255, 230)
                : Color.FromArgb(255, 220, 220);
            lblStatus.ForeColor = sukses
                ? Color.FromArgb(0, 100, 50)
                : Color.FromArgb(150, 0, 0);
            lblStatus.Visible = true;
        }

        private void btnBatalKembali_Click(object sender, EventArgs e)
        {
            if (_checkoutSudahDilakukan)
            {
                // Kalau sudah checkout, arahkan ke riwayat
                OnCheckoutBerhasil?.Invoke(_idTransaksiBaru);
            }
            else
            {
                OnNavigateKembali?.Invoke();
            }
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
            lblNamaFile.Location = new Point(margin + 170, 268);
            picPreview.Location = new Point(margin, 305);

            // Posisi ID transaksi & hint
            lblIdTrxLabel.Location = new Point(margin, 480);
            txtIdTransaksi.Location = new Point(margin + 210, 477);
            lblIdTrxHint.Location = new Point(margin, 510);

            // Tombol
            btnBatalKembali.Location = new Point(margin, 540);
            btnKonfirmasiCheckout.Location = new Point(margin + btnBatalKembali.Width + 20, 540);

            // Status
            lblStatus.SetBounds(margin, 605, Math.Min(700, contentW), 28);
        }
    }
}
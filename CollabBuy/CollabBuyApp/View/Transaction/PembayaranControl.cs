using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    /// <summary>
    /// Halaman pembayaran: ditampilkan setelah checkout berhasil.
    /// User bisa lihat ringkasan pesanan dan upload bukti transfer.
    /// </summary>
    public partial class PembayaranControl : UserControl
    {
        private readonly User _currentUser;
        private readonly int _idTransaksi;
        private readonly long _totalTagihan;
        private readonly TransactionController _transactionController;
        private byte[] _buktiBayarBytes = null;

        // Event untuk kembali ke riwayat setelah upload berhasil
        public event Action OnPembayaranSelesai;

        public PembayaranControl(User currentUser, int idTransaksi, long totalTagihan)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _idTransaksi = idTransaksi;
            _totalTagihan = totalTagihan;
            _transactionController = new TransactionController(_currentUser.GetIdUser());
        }

        private void PembayaranControl_Load(object sender, EventArgs e)
        {
            // Isi info transaksi
            lblIdTransaksi.Text = $"ID Transaksi  :  #{_idTransaksi}";
            lblTotalBayar.Text = $"Rp {_totalTagihan:N0}";
            lblStatusBayar.Text = "⏳ Menunggu Bukti Pembayaran";
            lblStatusBayar.ForeColor = Color.FromArgb(200, 120, 0);
        }

        // ── Pilih gambar bukti bayar ──
        private void btnPilihBukti_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Bukti Pembayaran";
                ofd.Filter = "Gambar (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Semua File (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _buktiBayarBytes = File.ReadAllBytes(ofd.FileName);
                    lblNamaFile.Text = Path.GetFileName(ofd.FileName);

                    // Preview gambar
                    try
                    {
                        pbPreview.Image = Image.FromFile(ofd.FileName);
                    }
                    catch
                    {
                        pbPreview.Image = null;
                    }

                    btnUpload.Enabled = true;
                    btnUpload.BackColor = Color.FromArgb(36, 0, 70);
                }
            }
        }

        // ── Upload bukti pembayaran ──
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (_buktiBayarBytes == null || _buktiBayarBytes.Length == 0)
            {
                MessageBox.Show("Pilih file bukti pembayaran dulu ya!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = _transactionController.UploadBuktiBayar(_idTransaksi, _buktiBayarBytes, _currentUser.GetIdUser());

            if (result.sukses)
            {
                lblStatusBayar.Text = "✅ Bukti berhasil diupload, menunggu verifikasi admin";
                lblStatusBayar.ForeColor = Color.ForestGreen;

                MessageBox.Show(
                    "Bukti pembayaran berhasil diupload! 🎉\nAdmin akan memverifikasi dalam 1×24 jam.",
                    "Berhasil!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnUpload.Enabled = false;
                btnPilihBukti.Enabled = false;

                OnPembayaranSelesai?.Invoke();
            }
            else
            {
                MessageBox.Show(result.pesan, "Upload Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Kembali tanpa upload (bayar nanti) ──
        private void btnNanti_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                "Kamu bisa upload bukti pembayaran nanti di halaman Riwayat Pesanan.\nYakin mau kembali dulu?",
                "Bayar Nanti?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
                OnPembayaranSelesai?.Invoke();
        }
    }
}
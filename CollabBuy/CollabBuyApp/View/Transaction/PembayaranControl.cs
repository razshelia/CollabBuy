using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class PembayaranControl : UserControl
    {
        private readonly Models.User _user;
        private readonly TransactionController _trxCtrl;
        private readonly PreOrderController _poCtrl; // Controller khusus untuk PreOrder
        private readonly long _totalTagihan;

        private byte[] _buktiBayar;
        private int _idTransaksiBaru;
        private bool _checkoutSudahDilakukan;

        // Event navigasi
        public event Action OnNavigateKembali;
        public event Action<int> OnCheckoutBerhasil;

        public PembayaranControl(Models.User user, TransactionController trxCtrl, long totalTagihan)
        {
            this.InitializeComponent();

            this._user = user;
            this._trxCtrl = trxCtrl;
            this._poCtrl = new PreOrderController(); // Inisialisasi controller tambahan
            this._totalTagihan = totalTagihan;

            this._buktiBayar = null;
            this._idTransaksiBaru = 0;
            this._checkoutSudahDilakukan = false;

            this.Dock = DockStyle.Fill;
        }

        private void PembayaranControl_Load(object sender, EventArgs e)
        {
            this.lblTotal.Text = "Rp " + this._totalTagihan.ToString("N0");
            this.LoadInfoRekening();
            this.AturLayout();
        }

        private void LoadInfoRekening()
        {
            try
            {
                DataTable dtKeranjang = this._trxCtrl.GetKeranjangDataTable();

                if (dtKeranjang.Rows.Count > 0)
                {
                    int idProdukPertama = Convert.ToInt32(dtKeranjang.Rows[0]["IdProduk"]);

                    ProductController pc = new ProductController();
                    Models.Product produkTeratas = pc.GetProdukById(idProdukPertama);

                    if (produkTeratas != null && produkTeratas.GetIdPo().HasValue)
                    {
                        // =======================================================
                        // OOP BEST PRACTICE: PANGGIL LEWAT CONTROLLER & MODEL
                        // BUKAN NULIS QUERY SQL DI VIEW!
                        // =======================================================
                        Models.PreOrder sesiPo = this._poCtrl.GetPreOrder(produkTeratas.GetIdPo().Value);

                        if (sesiPo != null)
                        {
                            this.lblRekeningInfo.Text = sesiPo.GetInfoRekening();
                        }
                        else
                        {
                            this.lblRekeningInfo.Text = "Data PO tidak valid atau telah dihapus.";
                        }
                    }
                    else
                    {
                        this.lblRekeningInfo.Text = "Barang jualan reguler (Tanpa PO). Silakan chat penjual untuk info transfer.";
                    }
                }
                else
                {
                    this.lblRekeningInfo.Text = "Keranjang kamu kosong!";
                }
            }
            catch (Exception ex)
            {
                this.lblRekeningInfo.Text = "Gagal memuat info rekening: " + ex.Message;
            }
        }

        private void PembayaranControl_Resize(object sender, EventArgs e)
        {
            this.AturLayout();
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
                        this._buktiBayar = File.ReadAllBytes(ofd.FileName);
                        this.lblNamaFile.Text = Path.GetFileName(ofd.FileName);

                        using (MemoryStream ms = new MemoryStream(this._buktiBayar))
                        {
                            this.picPreview.Image = Image.FromStream(ms);
                        }
                        this.picPreview.Visible = true;

                        if (this._checkoutSudahDilakukan && this._idTransaksiBaru > 0)
                        {
                            this.UploadBuktiBayar();
                        }
                        else
                        {
                            // Belum checkout, cuma prepare gambar doang
                            bool skipUpload = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.TampilkanStatus($"❌ Gagal membaca file: {ex.Message}", false);
                    }
                }
                else
                {
                    // User cancel file dialog
                    bool batalPilih = true;
                }
            }
        }

        private void btnKonfirmasiCheckout_Click(object sender, EventArgs e)
        {
            if (this._checkoutSudahDilakukan)
            {
                this.TampilkanStatus("⚠️ Checkout udah diproses bestie, santai aja.", false);
            }
            else
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Udah yakin mau checkout?\n\nTotal Tagihan: Rp {this._totalTagihan:N0}\n\nJangan lupa upload bukti transfer ya biar pesanan kamu divalidasi!",
                    "Konfirmasi Checkout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (konfirmasi == DialogResult.Yes)
                {
                    var (sukses, pesan) = this._trxCtrl.ProsesCheckout();

                    if (!sukses)
                    {
                        this.TampilkanStatus($"❌ Checkout gagal: {pesan}", false);
                    }
                    else
                    {
                        this._checkoutSudahDilakukan = true;

                        try
                        {
                            string[] parts = pesan.Split(':');
                            if (parts.Length >= 2)
                            {
                                int.TryParse(parts[parts.Length - 1].Trim(), out this._idTransaksiBaru);
                            }
                            else
                            {
                                bool skipParse = true;
                            }
                        }
                        catch
                        {
                            this._idTransaksiBaru = 0;
                        }

                        if (this._idTransaksiBaru > 0)
                        {
                            this.txtIdTransaksi.Text = this._idTransaksiBaru.ToString();
                            this.txtIdTransaksi.Visible = true;
                            this.lblIdTrxLabel.Visible = true;
                            this.lblIdTrxHint.Visible = true;
                        }
                        else
                        {
                            bool idGagalMulai = true;
                        }

                        if (this._buktiBayar != null && this._buktiBayar.Length > 0)
                        {
                            this.UploadBuktiBayar();
                        }
                        else
                        {
                            this.TampilkanStatus($"✅ Checkout berhasil! ID: {this._idTransaksiBaru}. Buruan upload buktinya ya!", true);
                        }

                        this.btnKonfirmasiCheckout.Enabled = false;
                        this.btnKonfirmasiCheckout.BackColor = Color.FromArgb(150, 150, 150);
                        this.btnKonfirmasiCheckout.Text = "✅ Transaksi Diproses";

                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 2500;
                        timer.Tick += (s, ev) =>
                        {
                            timer.Stop();
                            this.OnCheckoutBerhasil?.Invoke(this._idTransaksiBaru);
                        };
                        timer.Start();
                    }
                }
                else
                {
                    // User membatalkan checkout via MessageBox
                    bool userBatal = true;
                }
            }
        }

        private void UploadBuktiBayar()
        {
            if (this._idTransaksiBaru <= 0 || this._buktiBayar == null)
            {
                // Lewati upload jika data tidak valid
                bool dataUploadTidakValid = true;
            }
            else
            {
                var (sukses, pesanUpload) = this._trxCtrl.UploadBuktiBayar(this._idTransaksiBaru, this._buktiBayar, this._user.GetIdUser());

                if (sukses)
                {
                    this.TampilkanStatus($"✅ Mantap! Checkout & resi berhasil di-upload!", true);
                }
                else
                {
                    this.TampilkanStatus($"⚠️ Checkout sukses, tapi resi gagal di-upload: {pesanUpload}", false);
                }
            }
        }

        private void TampilkanStatus(string pesan, bool sukses)
        {
            this.lblStatus.Text = pesan;
            this.lblStatus.Visible = true;

            if (sukses)
            {
                this.lblStatus.BackColor = Color.FromArgb(210, 255, 230);
                this.lblStatus.ForeColor = Color.FromArgb(0, 100, 50);
            }
            else
            {
                this.lblStatus.BackColor = Color.FromArgb(255, 220, 220);
                this.lblStatus.ForeColor = Color.FromArgb(150, 0, 0);
            }
        }

        private void btnBatalKembali_Click(object sender, EventArgs e)
        {
            if (this._checkoutSudahDilakukan)
            {
                this.OnCheckoutBerhasil?.Invoke(this._idTransaksiBaru);
            }
            else
            {
                this.OnNavigateKembali?.Invoke();
            }
        }

        private void AturLayout()
        {
            int margin = 30;
            int w;

            if (this.Width > 0)
            {
                w = this.Width;
            }
            else
            {
                w = 980;
            }

            int contentW = w - (margin * 2);

            this.pnlRingkasan.SetBounds(margin, 20, Math.Min(560, contentW), 90);
            this.pnlRekening.SetBounds(margin, 126, Math.Min(560, contentW), 85);
            this.lblUploadTitle.Location = new Point(margin, 228);

            this.btnPilihFile.Location = new Point(margin, 258);

            this.lblNamaFile.AutoSize = false;
            this.lblNamaFile.AutoEllipsis = true;
            this.lblNamaFile.Location = new Point(margin + this.btnPilihFile.Width + 20, 260);
            this.lblNamaFile.Size = new Size(contentW - this.btnPilihFile.Width - 40, 30);
            this.lblNamaFile.TextAlign = ContentAlignment.MiddleLeft;

            this.picPreview.Location = new Point(margin, 305);

            this.lblIdTrxLabel.Location = new Point(margin, 480);
            this.txtIdTransaksi.Location = new Point(margin + 210, 477);
            this.lblIdTrxHint.Location = new Point(margin, 510);

            this.btnBatalKembali.Location = new Point(margin, 540);
            this.btnKonfirmasiCheckout.Location = new Point(margin + this.btnBatalKembali.Width + 20, 540);

            this.lblStatus.SetBounds(margin, 605, Math.Min(700, contentW), 28);
        }
    }
}
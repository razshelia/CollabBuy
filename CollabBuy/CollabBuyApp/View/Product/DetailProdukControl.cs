using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.User
{
    public partial class DetailProdukControl : UserControl
    {
        private readonly Models.User _user;
        private readonly ProductController _prodCtrl;
        private readonly TransactionController _trxCtrl;
        private readonly int _idProduk;

        private Product _produk;
        private System.Windows.Forms.Timer _timerStatus;

        // Event navigasi
        public event Action OnNavigateKembali; // Kembali ke katalog
        public event Action OnNavigateKeranjang;

        public DetailProdukControl(Models.User user, int idProduk)
        {
            InitializeComponent();
            _user = user;
            _idProduk = idProduk;
            _prodCtrl = new ProductController();
            _trxCtrl = new TransactionController(_user.GetIdUser());

            _timerStatus = new System.Windows.Forms.Timer();
            _timerStatus.Interval = 3000;
            _timerStatus.Tick += (s, e) => { lblStatus.Visible = false; _timerStatus.Stop(); };
        }

        private void DetailProdukControl_Load(object sender, EventArgs e)
        {
            MuatDetailProduk();
            AturLayout();
        }

        private void DetailProdukControl_Resize(object sender, EventArgs e)
        {
            AturLayout();
        }

        private void MuatDetailProduk()
        {
            try
            {
                _produk = _prodCtrl.GetProdukById(_idProduk);
                if (_produk == null)
                {
                    lblNamaProduk.Text = "Produk tidak ditemukan";
                    return;
                }

                // Nama
                lblNamaProduk.Text = _produk.GetNamaProduk();
                lblHeaderTitle.Text = _produk.GetNamaProduk();

                // Harga
                long hargaSaatIni = _produk.HitungTotal();
                lblHarga.Text = "Rp " + hargaSaatIni.ToString("N0");

                // Deskripsi
                string deskripsi = _produk.GetDeskripsi();
                lblDeskripsi.Text = string.IsNullOrEmpty(deskripsi) ? "(Tidak ada deskripsi)" : deskripsi;

                // Info PO & Slot
                string tipePo = "Produk Reguler";
                string slotInfo = "Tidak terbatas";
                string batasWaktu = "-";
                string minOrder = _produk.GetMinOrder().ToString();

                if (_produk.GetIdPo().HasValue)
                {
                    // Ada PO — ambil info dari controller kuota
                    var (statusKuota, sisaKuota) = _prodCtrl.CekStatusKuota(_idProduk);

                    if (_produk.GetTargetKuota() > 0)
                    {
                        int sisa = _produk.GetSisaKuota();
                        slotInfo = sisa > 0 ? $"{sisa} slot tersisa" : "⛔ Penuh";
                        lblSlotNilai.ForeColor = sisa > 0
                            ? Color.FromArgb(0, 100, 50)
                            : Color.FromArgb(180, 0, 0);
                    }
                }

                lblTipePo.Text = "Tipe: " + tipePo;
                lblSlotLabel.Text = "Slot Tersedia:";
                lblSlotNilai.Text = slotInfo;
                lblMinOrderLabel.Text = "Min Order:";
                lblMinOrder.Text = minOrder + " pcs";
                lblBatasLabel.Text = "Status:";
                lblBatas.Text = batasWaktu;

                // Foto produk
                byte[] foto = _produk.GetFotoProduk();
                if (foto != null && foto.Length > 0)
                {
                    try
                    {
                        using (var ms = new System.IO.MemoryStream(foto))
                        {
                            picFoto.Image = Image.FromStream(ms);
                        }
                    }
                    catch { TampilkanIkonDefault(); }
                }
                else
                {
                    TampilkanIkonDefault();
                }

                // Set nilai minimal nudQty
                nudQty.Minimum = _produk.GetMinOrder();
                nudQty.Value = _produk.GetMinOrder();
            }
            catch (Exception ex)
            {
                lblNamaProduk.Text = "Error memuat produk";
                TampilkanStatus($"Error: {ex.Message}", false);
            }
        }

        private void TampilkanIkonDefault()
        {
            picFoto.Image = null;
            // Tampilkan teks placeholder
            Label lblPlaceholder = new Label();
            lblPlaceholder.Text = "🖼️\nTidak ada foto";
            lblPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            lblPlaceholder.Font = new Font("Segoe UI", 10F);
            lblPlaceholder.ForeColor = Color.FromArgb(130, 80, 180);
            lblPlaceholder.Dock = DockStyle.Fill;
            picFoto.Controls.Clear();
            picFoto.Controls.Add(lblPlaceholder);
        }

        private void btnMasukKeranjang_Click(object sender, EventArgs e)
        {
            if (_produk == null) return;

            int jumlah = (int)nudQty.Value;
            string catatan = txtCatatan.Text.Trim();

            var (sukses, pesan) = _trxCtrl.TambahItemKeKeranjang(_idProduk, _user.GetNama(), jumlah, catatan);

            if (sukses)
            {
                TampilkanStatus($"✅ Berhasil! '{_produk.GetNamaProduk()}' masuk keranjang.", true);
            }
            else
            {
                TampilkanStatus($"❌ {pesan}", false);
            }
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
            _timerStatus.Stop();
            _timerStatus.Start();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            OnNavigateKembali?.Invoke();
        }

        private void AturLayout()
        {
            int margin = 30;
            int w = this.Width > 0 ? this.Width : 980;
            int contentW = w - margin * 2;

            // Foto tetap di kiri
            picFoto.SetBounds(margin, 20, 220, 220);

            // Info di kanan foto
            int infoLeft = margin + 240;
            int infoW = contentW - 240;

            lblNamaProduk.SetBounds(infoLeft, 20, infoW, 50);
            pnlHarga.SetBounds(infoLeft, 78, Math.Min(320, infoW), 65);
            pnlInfoPo.SetBounds(infoLeft, 155, infoW, 85);

            // Deskripsi
            lblDeskripsiTitle.Location = new Point(margin, 260);
            pnlDeskripsi.SetBounds(margin, 288, contentW, 100);

            // Form beli
            pnlBeli.SetBounds(margin, 406, contentW, 110);

            // Tombol "Masukkan Keranjang" selalu di kanan panel beli
            btnMasukKeranjang.Location = new Point(pnlBeli.Width - btnMasukKeranjang.Width - 20, 28);

            // Status
            lblStatus.SetBounds(margin, 530, Math.Min(600, contentW), 28);
        }
    }
}
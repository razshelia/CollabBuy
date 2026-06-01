using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.View.Helper;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class DetailProdukControl : UserControl
    {
        private readonly Models.User _user;
        private readonly ProductController _prodCtrl;
        private readonly TransactionController _trxCtrl;
        private readonly int _idProduk;

        private Models.Product _produk;
        private System.Windows.Forms.Timer _timerStatus;

        public event Action OnNavigateKembali;
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

            this.Dock = DockStyle.Fill;
        }

        private void DetailProdukControl_Load(object sender, EventArgs e)
        {
            MuatDetailProduk();
        }

        private void MuatDetailProduk()
        {
            try
            {
                _produk = _prodCtrl.GetProdukById(_idProduk);
                if (_produk == null)
                {
                    lblNamaProduk.Text = "Waduh, barangnya ngilang bestie 😭";
                    btnMasukKeranjang.Enabled = false;
                    return;
                }

                // 1. Info Teks Dasar
                lblNamaProduk.Text = _produk.GetNamaProduk();
                lblHeaderTitle.Text = "✨ Detail: " + _produk.GetNamaProduk();

                long hargaSaatIni = _produk.HitungTotal();
                lblHarga.Text = "Rp " + hargaSaatIni.ToString("N0");

                string deskripsi = _produk.GetDeskripsi();
                txtDeskripsi.Text = string.IsNullOrEmpty(deskripsi) ? "Penjualnya misterius, nggak ngasih deskripsi nih." : deskripsi;

                // 2. Info PO & Slot
                string tipePo = "Ready Stock (Langsung Gass)";
                string slotInfo = "Aman banget (Unlimited)";
                string minOrder = _produk.GetMinOrder().ToString();

                if (_produk.GetIdPo().HasValue)
                {
                    tipePo = "Pre-Order (PO)";
                    if (_produk.GetTargetKuota() > 0)
                    {
                        int sisa = _produk.GetSisaKuota();
                        slotInfo = sisa > 0 ? $"🔥 Sisa {sisa} slot lagi!" : "⛔ Penuh Bestie!";
                        lblSlotNilai.ForeColor = sisa > 0 ? Color.FromArgb(200, 50, 50) : Color.FromArgb(180, 0, 0);
                    }
                }

                lblTipePoNilai.Text = tipePo;
                lblSlotNilai.Text = slotInfo;
                lblMinOrderNilai.Text = minOrder + " pcs";

                // 3. Multi-Foto (Byte Packing System)
                RenderFotoProduk();

                // 4. Form Order (Hanya Quantity)
                nudQty.Minimum = _produk.GetMinOrder() > 0 ? _produk.GetMinOrder() : 1;
                nudQty.Value = nudQty.Minimum;

                if (slotInfo == "⛔ Penuh Bestie!") btnMasukKeranjang.Enabled = false;
            }
            catch (Exception ex)
            {
                lblNamaProduk.Text = "Error memuat produk";
                TampilkanStatus($"Error: {ex.Message}", false);
            }
        }

        private void RenderFotoProduk()
        {
            flpThumbnails.Controls.Clear();
            byte[] fotoData = _produk.GetFotoProduk();

            if (fotoData != null && fotoData.Length > 0)
            {
                try
                {
                    List<byte[]> images = ImageHelper.UnpackImages(fotoData);

                    if (images.Count > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(images[0])) { picFoto.Image = new Bitmap(Image.FromStream(ms)); }

                        foreach (var imgByte in images)
                        {
                            PictureBox thumb = new PictureBox
                            {
                                Width = 60,
                                Height = 60,
                                SizeMode = PictureBoxSizeMode.Zoom,
                                BorderStyle = BorderStyle.FixedSingle,
                                Cursor = Cursors.Hand,
                                Margin = new Padding(0, 0, 10, 0),
                                BackColor = Color.White
                            };

                            using (MemoryStream msThumb = new MemoryStream(imgByte)) { thumb.Image = new Bitmap(Image.FromStream(msThumb)); }

                            thumb.Click += (s, e) => { picFoto.Image = thumb.Image; };
                            flpThumbnails.Controls.Add(thumb);
                        }
                    }
                    else { TampilkanIkonDefault(); }
                }
                catch { TampilkanIkonDefault(); }
            }
            else
            {
                TampilkanIkonDefault();
            }
        }

        private void TampilkanIkonDefault()
        {
            picFoto.Image = null;
            picFoto.Controls.Clear();
            Label lblPlaceholder = new Label
            {
                Text = "🖼️\nNo Image",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.Gray,
                Dock = DockStyle.Fill
            };
            picFoto.Controls.Add(lblPlaceholder);
        }

        private void btnMasukKeranjang_Click(object sender, EventArgs e)
        {
            if (_produk == null) return;

            int jumlah = (int)nudQty.Value;
            string catatan = "";
            string namaPenitip = _user.GetNama();

            var (sukses, pesan) = _trxCtrl.TambahItemKeKeranjang(_idProduk, namaPenitip, jumlah, catatan);

            if (sukses)
            {
                // GANTI PAKE MESSAGE BOX BIAR POP-UP NYA JELAS DAN GAK KETIMPA!
                MessageBox.Show($"✅ Yeay! '{_produk.GetNamaProduk()}' udah masuk keranjang jajan lo bestie. Gas cek keranjang!",
                    "Masuk Keranjang!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"❌ Waduh gagal: {pesan}", "Error Bestie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TampilkanStatus(string pesan, bool sukses)
        {
            lblStatus.Text = pesan;
            lblStatus.BackColor = sukses ? Color.FromArgb(210, 255, 230) : Color.FromArgb(255, 220, 220);
            lblStatus.ForeColor = sukses ? Color.FromArgb(0, 100, 50) : Color.FromArgb(150, 0, 0);
            lblStatus.Visible = true;
            _timerStatus.Stop();
            _timerStatus.Start();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            if (OnNavigateKembali != null)
            {
                OnNavigateKembali.Invoke();
            }
            else
            {
                var parentPanel = this.Parent;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();
                    KatalogProdukControl katalog = new KatalogProdukControl(_user);
                    katalog.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(katalog);
                }
            }
        }
    }
}
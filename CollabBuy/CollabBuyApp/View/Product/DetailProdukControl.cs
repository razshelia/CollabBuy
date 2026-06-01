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
            this.InitializeComponent();

            this._user = user;
            this._idProduk = idProduk;
            this._prodCtrl = new ProductController();
            this._trxCtrl = new TransactionController(this._user.GetIdUser());

            this._timerStatus = new System.Windows.Forms.Timer();
            this._timerStatus.Interval = 3000;
            this._timerStatus.Tick += (s, e) =>
            {
                this.lblStatus.Visible = false;
                this._timerStatus.Stop();
            };

            this.Dock = DockStyle.Fill;
        }

        private void DetailProdukControl_Load(object sender, EventArgs e)
        {
            this.MuatDetailProduk();
        }

        private void MuatDetailProduk()
        {
            try
            {
                this._produk = this._prodCtrl.GetProdukById(this._idProduk);

                if (this._produk == null)
                {
                    this.lblNamaProduk.Text = "Waduh, barangnya ngilang bestie 😭";
                    this.btnMasukKeranjang.Enabled = false;
                }
                else
                {
                    // 1. Info Teks Dasar
                    this.lblNamaProduk.Text = this._produk.GetNamaProduk();
                    this.lblHeaderTitle.Text = "✨ Detail: " + this._produk.GetNamaProduk();

                    // =======================================================
                    // OOP BEST PRACTICE: Panggil Method Behavior dari Model!
                    // =======================================================
                    this.lblHarga.Text = this._produk.DapatkanFormatHargaUI();

                    string deskripsi = this._produk.GetDeskripsi();

                    if (string.IsNullOrWhiteSpace(deskripsi) || deskripsi == "Tidak ada deskripsi.")
                    {
                        this.txtDeskripsi.Text = "Penjualnya misterius, nggak ngasih deskripsi nih.";
                    }
                    else
                    {
                        this.txtDeskripsi.Text = deskripsi;
                    }

                    // 2. Info PO & Slot
                    string tipePo;
                    if (this._produk.GetIdPo().HasValue)
                    {
                        tipePo = "Pre-Order (PO)";
                    }
                    else
                    {
                        tipePo = "Ready Stock (Langsung Gass)";
                    }

                    this.lblTipePoNilai.Text = tipePo;

                    // =======================================================
                    // Mengambil langsung info slot dari Behavior Model
                    // =======================================================
                    string slotInfo = this._produk.DapatkanInfoSlot();
                    this.lblSlotNilai.Text = slotInfo;
                    this.lblMinOrderNilai.Text = this._produk.GetMinOrder().ToString() + " pcs";

                    if (this._produk.GetSisaKuota() > 0 || !this._produk.GetIdPo().HasValue)
                    {
                        this.lblSlotNilai.ForeColor = Color.FromArgb(200, 50, 50);
                        this.btnMasukKeranjang.Enabled = true;
                    }
                    else
                    {
                        this.lblSlotNilai.ForeColor = Color.FromArgb(180, 0, 0);
                        this.btnMasukKeranjang.Enabled = false;
                    }

                    // 3. Multi-Foto (Byte Packing System)
                    this.RenderFotoProduk();

                    // 4. Form Order (Hanya Quantity)
                    if (this._produk.GetMinOrder() > 0)
                    {
                        this.nudQty.Minimum = this._produk.GetMinOrder();
                    }
                    else
                    {
                        this.nudQty.Minimum = 1;
                    }

                    this.nudQty.Value = this.nudQty.Minimum;
                }
            }
            catch (Exception ex)
            {
                this.lblNamaProduk.Text = "Error memuat produk";
                this.TampilkanStatus($"Error: {ex.Message}", false);
            }
        }

        private void RenderFotoProduk()
        {
            this.flpThumbnails.Controls.Clear();
            byte[] fotoData;

            if (this._produk != null)
            {
                fotoData = this._produk.GetFotoProduk();
            }
            else
            {
                fotoData = null;
            }

            if (fotoData != null && fotoData.Length > 0)
            {
                try
                {
                    List<byte[]> images = ImageHelper.UnpackImages(fotoData);

                    if (images.Count > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(images[0]))
                        {
                            this.picFoto.Image = new Bitmap(Image.FromStream(ms));
                        }

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

                            using (MemoryStream msThumb = new MemoryStream(imgByte))
                            {
                                thumb.Image = new Bitmap(Image.FromStream(msThumb));
                            }

                            thumb.Click += (s, e) => { this.picFoto.Image = thumb.Image; };
                            this.flpThumbnails.Controls.Add(thumb);
                        }
                    }
                    else
                    {
                        this.TampilkanIkonDefault();
                    }
                }
                catch
                {
                    this.TampilkanIkonDefault();
                }
            }
            else
            {
                this.TampilkanIkonDefault();
            }
        }

        private void TampilkanIkonDefault()
        {
            this.picFoto.Image = null;
            this.picFoto.Controls.Clear();

            Label lblPlaceholder = new Label
            {
                Text = "🖼️\nNo Image",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.Gray,
                Dock = DockStyle.Fill
            };

            this.picFoto.Controls.Add(lblPlaceholder);
        }

        private void btnMasukKeranjang_Click(object sender, EventArgs e)
        {
            if (this._produk == null)
            {
                // Keadaan di mana _produk gagal dimuat tapi tombol sempat diklik
                bool proteksiNull = true;
            }
            else
            {
                int jumlah = (int)this.nudQty.Value;
                string catatan = "";
                string namaPenitip = this._user.GetNama();

                var (sukses, pesan) = this._trxCtrl.TambahItemKeKeranjang(this._idProduk, namaPenitip, jumlah, catatan);

                if (sukses)
                {
                    MessageBox.Show($"✅ Yeay! '{this._produk.GetNamaProduk()}' udah masuk keranjang jajan lo bestie. Gas cek keranjang!",
                        "Masuk Keranjang!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ Waduh gagal: {pesan}", "Error Bestie", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            this._timerStatus.Stop();
            this._timerStatus.Start();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            if (this.OnNavigateKembali != null)
            {
                this.OnNavigateKembali.Invoke();
            }
            else
            {
                var parentPanel = this.Parent;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();
                    KatalogProdukControl katalog = new KatalogProdukControl(this._user);
                    katalog.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(katalog);
                }
                else
                {
                    // Tidak ada parent, tidak bisa load Katalog
                    bool parentKosong = true;
                }
            }
        }
    }
}
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
        private Label _lblNamaToko;
        private System.Windows.Forms.Timer _timerStatus;

        public event Action OnNavigateKembali;
        public event Action OnNavigateKeranjang;

        public DetailProdukControl(Models.User user, int idProduk)
        {
            this.InitializeComponent();

            this._user = user;
            this._idProduk = idProduk;
            this._prodCtrl = new ProductController();
            this._trxCtrl = new TransactionController(this._user.IdUser);

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
                    this.btnMasukKeranjang.BackColor = Color.FromArgb(210, 210, 210);
                    this.btnMasukKeranjang.ForeColor = Color.FromArgb(140, 140, 140);
                }
                else
                {
                    // 1. Info Teks Dasar
                    this.lblNamaProduk.Text = this._produk.NamaProduk;
                    this.lblHeaderTitle.Text = "✨ Detail: " + this._produk.NamaProduk;
                    string namaToko = this._prodCtrl.GetNamaTokoByIdProduk(this._idProduk);
                    if (_lblNamaToko == null)
                    {
                        _lblNamaToko = new Label
                        {
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                            ForeColor = Color.FromArgb(120, 80, 160),
                            Location = new Point(424, 130),
                            Size = new Size(590, 15),
                            AutoSize = false
                        };
                        this.Controls.Add(_lblNamaToko);
                        _lblNamaToko.BringToFront();
                    }
                    _lblNamaToko.Text = "🏪 " + namaToko;

                    // =======================================================
                    // OOP BEST PRACTICE: Panggil Method Behavior dari Model!
                    // =======================================================
                    this.lblHarga.Text = this._produk.DapatkanFormatHargaUI(); // Tetap method

                    string deskripsi = this._produk.Deskripsi;

                    if (string.IsNullOrWhiteSpace(deskripsi) || deskripsi == "Tidak ada deskripsi.")
                    {
                        this.txtDeskripsi.Text = "Penjualnya misterius, nggak ngasih deskripsi nih.";
                    }
                    else
                    {
                        this.txtDeskripsi.Text = deskripsi;
                    }

                    // 2. Info PO & Slot
                    bool dalamSesiPO = this._produk.IdPo.HasValue;

                    this.lblTipePoNilai.Text = dalamSesiPO
                        ? "Pre-Order (PO)"
                        : "Tidak dalam Sesi PO";

                    // Ketersediaan: produk tanpa PO tidak bisa dipesan
                    if (!dalamSesiPO)
                    {
                        this.lblSlotNilai.Text = "⛔ Tidak tersedia untuk dipesan";
                        this.lblSlotNilai.ForeColor = Color.FromArgb(180, 0, 0);
                        this.btnMasukKeranjang.Enabled = false;
                        this.btnMasukKeranjang.Text = "Tidak Dalam Sesi PO";
                        this.btnMasukKeranjang.BackColor = Color.FromArgb(210, 210, 210);
                        this.btnMasukKeranjang.ForeColor = Color.FromArgb(140, 140, 140);
                    }
                    else if (this._produk.GetSisaKuota() <= 0 && this._produk.GetTargetKuota() > 0)
                    {
                        // Kuota penuh
                        this.lblSlotNilai.Text = this._produk.DapatkanInfoSlot();
                        this.lblSlotNilai.ForeColor = Color.FromArgb(180, 0, 0);
                        this.btnMasukKeranjang.Enabled = false;
                        this.btnMasukKeranjang.BackColor = Color.FromArgb(210, 210, 210);
                        this.btnMasukKeranjang.ForeColor = Color.FromArgb(140, 140, 140);
                    }
                    else
                    {
                        // Bisa dipesan
                        this.lblSlotNilai.Text = this._produk.DapatkanInfoSlot();
                        this.lblSlotNilai.ForeColor = Color.FromArgb(0, 130, 50);
                        this.btnMasukKeranjang.Enabled = true;
                        this.btnMasukKeranjang.BackColor = Color.FromArgb(36, 0, 70);
                        this.btnMasukKeranjang.ForeColor = Color.FromArgb(253, 255, 182);
                    }

                    this.lblMinOrderNilai.Text = this._produk.MinOrder.ToString() + " pcs";

                    // 3. Multi-Foto (Byte Packing System)
                    this.RenderFotoProduk();

                    // 4. Form Order (Hanya Quantity)
                    if (this._produk.MinOrder > 0)
                    {
                        this.nudQty.Minimum = this._produk.MinOrder;
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
                fotoData = this._produk.FotoProduk;
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
                MessageBox.Show("Data produk belum termuat, coba refresh halaman ini.",
                    "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                int jumlah = (int)this.nudQty.Value;
                string catatan = "";
                string namaPenitip = this._user.Nama;

                var (sukses, pesan) = this._trxCtrl.TambahItemKeKeranjang(this._idProduk, namaPenitip, jumlah, catatan);

                if (sukses)
                {
                    MessageBox.Show($"✅ Yeay! '{this._produk.NamaProduk}' udah masuk keranjang jajan lo bestie. Gas cek keranjang!",
                        "Masuk Keranjang!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ Waduh gagal: {pesan}", "Error Bestie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLihatKeranjang_Click(object sender, EventArgs e)
        {
            this.OnNavigateKeranjang?.Invoke();
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
            }
        }
    }
}
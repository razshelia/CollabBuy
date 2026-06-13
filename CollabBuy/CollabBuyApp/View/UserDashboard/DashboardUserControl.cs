using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.View.Helper;
using CollabBuy.CollabBuyApp.View.Product;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DashboardUserControl : UserControl
    {
        private Models.User _currentUser;
        private readonly ProductController _productController;
        private readonly TransactionController _transController;
        private readonly PreOrderController _poController;
        private readonly LaporanController _laporanController;

        public DashboardUserControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._currentUser = currentUser;
            this._productController = new ProductController();
            this._transController = new TransactionController(this._currentUser.IdUser);
            this._poController = new PreOrderController();
            this._laporanController = new Controllers.LaporanController();

            this.Dock = DockStyle.Fill;
        }

        private void DashboardUserControl_Load(object sender, EventArgs e)
        {
            this.lblSapaan.Text = $"Halo, {this._currentUser.Nama}! 👋";
            this.LoadStatistikAtas();
            this.LoadPOMauHabis();
        }

        private void DashboardUserControl_Resize(object sender, EventArgs e)
        {
            // Dummy logic untuk mempertahankan struktur Strict OOP jika event ini dipanggil
            bool layarDisesuaikan = true;
        }

        private void LoadStatistikAtas()
        {
            try
            {
                bool isPenjual = this._currentUser.Peran == "Penjual";

                if (isPenjual)
                {
                    this.lblTitleKeranjang.Text = "PO Aktif";

                    DataTable dtStat = this._laporanController.GetStatistikDashboardPenjual(this._currentUser.IdUser);

                    if (dtStat != null && dtStat.Rows.Count > 0)
                    {
                        DataRow r = dtStat.Rows[0];
                        this.lblValPesanan.Text = r["total_produk_master"].ToString();

                        int jumlahPoAktif = this._poController.GetJumlahPoAktif();
                        this.lblValKeranjang.Text = jumlahPoAktif.ToString();

                        long omzet = r["total_omzet_kotor"] != DBNull.Value
                            ? Convert.ToInt64(r["total_omzet_kotor"]) : 0L;
                        this.lblValSaldo.Text = "Rp " + omzet.ToString("N0");
                    }
                }
                else
                {
                    this.lblTitlePesanan.Text = "Pesanan Aktif";
                    this.lblTitleKeranjang.Text = "Item di Keranjang";
                    this.lblTitleSaldo.Text = "PO Tersedia";

                    this.lblValPesanan.Text = this._transController
                        .GetTotalPesananAktif(this._currentUser.IdUser).ToString();
                    this.lblValKeranjang.Text = this._transController
                        .GetIsiKeranjang().Count.ToString();
                    this.lblValSaldo.Text = this._poController
                        .GetActiveSesiPO("").Rows.Count.ToString();
                }
            }
            catch
            {
                this.lblValPesanan.Text = "0";
                this.lblValKeranjang.Text = "0";
                this.lblValSaldo.Text = "0";
            }
        }

        private void LoadPOMauHabis()
        {
            this.flpDashboard.Controls.Clear();
            DataTable dt = this._productController.GetPOHampirPenuh();

            if (dt == null || dt.Rows.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "Aman bestie, belum ada PO yang mau tutup nih.",
                    Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Margin = new Padding(10)
                };

                this.flpDashboard.Controls.Add(lblKosong);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    Panel card = this.BuatKartuPO(row);
                    this.flpDashboard.Controls.Add(card);
                }
            }
        }

        private Panel BuatKartuPO(DataRow row)
        {
            int idProduk = Convert.ToInt32(row["id_produk"]);
            int sisaSlot = Convert.ToInt32(row["target_kuota"]) - Convert.ToInt32(row["terisi"]);

            Panel card = new Panel
            {
                Width = 210,
                Height = 350,
                BackColor = Color.FromArgb(255, 235, 235),
                Margin = new Padding(10, 10, 15, 15),
                BorderStyle = BorderStyle.None
            };

            // Garis merah peringatan FOMO
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.LightCoral, ButtonBorderStyle.Solid);
            };

            PictureBox pbFoto = new PictureBox
            {
                Width = 190,
                Height = 140,
                Top = 10,
                Left = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Render Foto (Unpack format byte array yang diserialisasi)
            if (row["foto_produk"] != DBNull.Value)
            {
                try
                {
                    var images = ImageHelper.UnpackImages((byte[])row["foto_produk"]);

                    if (images.Count > 0 && images[0].Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(images[0]))
                        {
                            pbFoto.Image = new Bitmap(Image.FromStream(ms));
                        }
                    }
                    else
                    {
                        bool formatGambarSalah = true;
                    }
                }
                catch
                {
                    pbFoto.Image = null;
                }
            }
            else
            {
                bool tanpaGambar = true;
            }

            if (pbFoto.Image == null)
            {
                Label lblNoImage = new Label
                {
                    Text = "No Image",
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pbFoto.Controls.Add(lblNoImage);
            }
            else
            {
                bool gambarAda = true;
            }

            Label lblBadge = new Label
            {
                Text = $"🔥 SISA {sisaSlot} SLOT LAGI!",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                AutoSize = true,
                Top = 160,
                Left = 10,
                Padding = new Padding(3)
            };

            Label lblNama = new Label
            {
                Text = row["nama_produk"].ToString(),
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Top = 185,
                Left = 10,
                Width = 190,
                Height = 40
            };

            Label lblHarga = new Label
            {
                Text = $"Rp {Convert.ToInt32(row["harga_dasar"]):N0}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Top = 230,
                Left = 10,
                AutoSize = true
            };

            Label lblPo = new Label
            {
                Text = $"PO: {row["judul_po"]}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.DimGray,
                Top = 255,
                Left = 10,
                AutoSize = true,
                MaximumSize = new Size(190, 0)
            };

            Button btnDetail = new Button
            {
                Text = "🔍 Sikat Sekarang!",
                Width = 190,
                Height = 35,
                Top = 295,
                Left = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(36, 0, 70),
                ForeColor = Color.FromArgb(253, 255, 182),
                Font = new Font("Segoe UI Black", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += (s, e) => this.BukaHalamanDetail(idProduk);

            card.Controls.Add(pbFoto);
            card.Controls.Add(lblBadge);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblHarga);
            card.Controls.Add(lblPo);
            card.Controls.Add(btnDetail);

            return card;
        }

        private void BukaHalamanDetail(int idProduk)
        {
            var parentPanel = this.Parent;

            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                DetailProdukControl detailPage = new DetailProdukControl(this._currentUser, idProduk);
                detailPage.Dock = DockStyle.Fill;
                parentPanel.Controls.Add(detailPage);
            }
            else
            {
                bool tidakBisaGantiHalaman = true;
            }
        }

        private void btnLihatSemua_Click(object sender, EventArgs e)
        {
            var parentPanel = this.Parent;

            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                KatalogProdukControl katalogPage = new KatalogProdukControl(this._currentUser);
                katalogPage.Dock = DockStyle.Fill;
                parentPanel.Controls.Add(katalogPage);
            }
            else
            {
                bool tidakBisaGantiHalaman = true;
            }
        }
    }
}
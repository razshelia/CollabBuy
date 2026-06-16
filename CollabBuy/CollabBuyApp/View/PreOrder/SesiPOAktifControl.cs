using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class SesiPOAktifControl : UserControl
    {
        private readonly PreOrderController _poController;
        private Models.User _currentUser;
        public event Action<int> OnNavigateKeProdukPO;
        public event Action OnNavigateKeKeranjang;

        public SesiPOAktifControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._poController = new PreOrderController();
            this._currentUser = currentUser;

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void SesiPOAktifControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadDataSesiPO("");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.txtCari.Text = "Kepoin sesi PO...";
            this.txtCari.ForeColor = Color.Gray;
            this.LoadDataSesiPO("");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            string keyword = this.txtCari.Text;

            if (keyword == "Kepoin sesi PO...")
            {
                keyword = "";
            }

            this.LoadDataSesiPO(keyword);
        }

        private void LoadDataSesiPO(string keyword)
        {
            this.flpSesiPO.Controls.Clear();

            try
            {
                DataTable dtPO = this._poController.GetActiveSesiPO(keyword);

                if (dtPO != null && dtPO.Rows.Count > 0)
                {
                    foreach (DataRow row in dtPO.Rows)
                    {
                        try
                        {
                            int jumlahProduk = row["jumlah_produk"] == DBNull.Value ? 0 : Convert.ToInt32(row["jumlah_produk"]);
                            decimal hargaMin = row["harga_min"] == DBNull.Value ? 0 : Convert.ToDecimal(row["harga_min"]);
                            decimal hargaMax = row["harga_max"] == DBNull.Value ? 0 : Convert.ToDecimal(row["harga_max"]);
                            string jenisPO = row["jenis_po"].ToString();

                            Panel pnlCard = this.BuatKartuPO(
                                Convert.ToInt32(row["id_po"]),
                                row["nama_sesi"].ToString(),
                                row["nama_toko"].ToString(),
                                jenisPO,
                                jumlahProduk,
                                hargaMin,
                                hargaMax,
                                Convert.ToDateTime(row["deadline"])
                            );
                            this.flpSesiPO.Controls.Add(pnlCard);
                        }
                        catch (Exception exRow)
                        {
                            MessageBox.Show("Error di baris: " + exRow.Message, "Debug Row");
                        }
                    }
                }
                else
                {
                    Label lblKosong = new Label
                    {
                        Text = "Yah, lagi sepi nih... Gak ada PO yang open. 🥲",
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Margin = new Padding(10)
                    };
                    this.flpSesiPO.Controls.Add(lblKosong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data ngab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel BuatKartuPO(int idPO, string namaSesi, string namaToko,
            string jenisPO, int jumlahProduk, decimal hargaMin, decimal hargaMax, DateTime deadline)
        {
            bool isGotongRoyong = jenisPO == "Gotong Royong";
            bool isTutup = !this._poController.CekPoBerjalan(idPO);
            if (idPO <= 0) isTutup = deadline < DateTime.Now;

            string badgeText = isGotongRoyong ? "GOTONG ROYONG" : "GASKEUN";
            Color badgeColor = isGotongRoyong
                ? Color.FromArgb(255, 236, 153)
                : Color.FromArgb(155, 246, 255);

            if (isTutup) { badgeText = "SESI BERAKHIR"; badgeColor = Color.FromArgb(255, 173, 173); }

            Color bgCard = Color.FromArgb(235, 204, 255);
            Color purple = Color.FromArgb(36, 0, 70);
            Color yellow = Color.FromArgb(253, 255, 182);

            Panel card = new Panel
            {
                Width = 270,
                Height = 210,
                BackColor = bgCard,
                Margin = new Padding(10, 10, 15, 15)
            };
            card.Paint += (s, e) =>
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, purple, ButtonBorderStyle.Solid);

            card.Controls.Add(new Label
            {
                Text = badgeText,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = badgeColor,
                ForeColor = purple,
                AutoSize = true,
                Top = 10,
                Left = 10,
                Padding = new Padding(3)
            });

            card.Controls.Add(new Label
            {
                Text = namaSesi.ToUpper(),
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = purple,
                Top = 35,
                Left = 10,
                Width = 250,
                Height = 40,
                AutoSize = false
            });

            card.Controls.Add(new Label
            {
                Text = $"🏪 {namaToko}",
                Font = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = Color.FromArgb(90, 24, 154),
                Top = 78,
                Left = 10,
                AutoSize = true
            });

            // Info produk — jumlah dan rentang harga
            string infoHarga = jumlahProduk == 0
                ? "Belum ada produk"
                : hargaMin == hargaMax
                    ? $"Rp {hargaMin:N0}"
                    : $"Rp {hargaMin:N0} – {hargaMax:N0}";

            card.Controls.Add(new Label
            {
                Text = $"📦 {jumlahProduk} produk  •  {infoHarga}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = purple,
                Top = 103,
                Left = 10,
                Width = 250,
                Height = 35,
                AutoSize = false
            });

            card.Controls.Add(new Label
            {
                Text = $"⏰ Tutup: {deadline:dd MMM yyyy HH:mm}",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = purple,
                Top = 138,
                Left = 10,
                AutoSize = true
            });

            Button btnIkut = new Button
            {
                Text = isTutup ? "Sesi Sudah Berakhir" : "🛒 Lihat Produk PO Ini",
                Width = 250,
                Height = 35,
                Top = 165,
                Left = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = isTutup ? Color.Gray : purple,
                ForeColor = isTutup ? Color.White : yellow,
                Cursor = isTutup ? Cursors.No : Cursors.Hand,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Tag = idPO,
                Enabled = !isTutup && jumlahProduk > 0
            };
            btnIkut.FlatAppearance.BorderSize = 0;

            if (!isTutup && jumlahProduk > 0)
                btnIkut.Click += this.BtnIkut_Click;

            card.Controls.Add(btnIkut);
            // === TAMBAHKAN DI BAGIAN PALING BAWAH, SEBELUM return card ===
            Models.PreOrder poTemp = this._poController.GetPreOrder(idPO);
            string infoCard = poTemp != null
                ? poTemp.DapatkanInfoCardPO()
                : $"{namaSesi} | {(isTutup ? "⏳ Waktu Habis!" : "Aktif")}";

            ToolTip ttCard = new ToolTip();
            ttCard.SetToolTip(card, infoCard);
            return card;
        }

        private void BtnIkut_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPO = Convert.ToInt32(btn.Tag);

            // Navigasi ke katalog produk dengan filter PO ini
            if (this.OnNavigateKeProdukPO != null)
            {
                this.OnNavigateKeProdukPO.Invoke(idPO);
            }
            else
            {
                MessageBox.Show(
                    "Buka halaman Katalog Produk dan cari produk dari sesi PO ini untuk menambahkannya ke keranjang.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCari_Enter(object sender, EventArgs e)
        {
            if (this.txtCari.Text == "Kepoin sesi PO...")
            {
                this.txtCari.Text = "";
                this.txtCari.ForeColor = Color.FromArgb(36, 0, 70);
            }
        }

        private void txtCari_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtCari.Text))
            {
                this.txtCari.Text = "Kepoin sesi PO...";
                this.txtCari.ForeColor = Color.Gray;
            }
        }

        private void AdjustLayout()
        {
            int margin = 36;
            this.flpSesiPO.Width = this.Width - (margin * 2);
            this.flpSesiPO.Height = this.Height - this.flpSesiPO.Top - margin;
        }
    }
}
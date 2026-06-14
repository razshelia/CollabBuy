using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.View.Helper;
using CollabBuy.CollabBuyApp.View.Transaction;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class KatalogProdukControl : UserControl
    {
        private readonly Models.User _user;
        private readonly ProductController _prodCtrl;
        private readonly TransactionController _trxCtrl;

        private DataTable _dtSemua;
        private readonly int? _filterIdPO;
        private System.Windows.Forms.Timer _timerInfo;

        private ComboBox _cmbKategori;

        public event Action<int> OnNavigateDetailProduk;
        public event Action OnNavigateKeranjang;
        public event Action OnNavigateKembali;

        private const int CARD_W = 220;
        private const int CARD_H = 340;

        public KatalogProdukControl(Models.User user)
        {
            this.InitializeComponent();

            this._user = user;
            this._prodCtrl = new ProductController();
            this._trxCtrl = new TransactionController(this._user.IdUser);

            this._timerInfo = new System.Windows.Forms.Timer();
            this._timerInfo.Interval = 3000;
            this._timerInfo.Tick += (s, e) =>
            {
                this.lblInfo.Visible = false;
                this._timerInfo.Stop();
            };

            this.Dock = DockStyle.Fill;
        }
        public KatalogProdukControl(Models.User user, int idPO)
            : this(user)
        {
            this._filterIdPO = idPO;
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
            this.btnKembaliSesiPO.Visible = this._filterIdPO.HasValue;
            this.InisialisasiDropdownKategori();
            this.MuatKatalog();
            this.AturLayout();
        }

        private void InisialisasiDropdownKategori()
        {
            if (this.pnlFilter == null)
            {
                bool abaikanInisialisasi = true;
            }
            else
            {
                this._cmbKategori = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Width = 180,
                    Cursor = Cursors.Hand
                };

                this._cmbKategori.SelectedIndexChanged += (s, e) => this.TerapkanFilterGabungan();

                this.pnlFilter.Controls.Add(this._cmbKategori);
                this._cmbKategori.BringToFront();
            }
        }

        private void MuatKatalog()
        {
            try
            {
                if (this._filterIdPO.HasValue)
                    this._dtSemua = this._prodCtrl.GetProdukDalamPO(this._filterIdPO.Value);
                else
                    this._dtSemua = this._prodCtrl.GetKatalogUtama();

                this.PopulasiDropdownKategori();
                this.TampilkanKartu(this._dtSemua);
            }
            catch (Exception ex)
            {
                this.TampilkanInfo($"Waduh gagal load katalog: {ex.Message}", false);
            }
        }

        private void PopulasiDropdownKategori()
        {
            if (this._cmbKategori == null)
            {
                bool batalkanPopulasi = true;
            }
            else
            {
                this._cmbKategori.Items.Clear();
                this._cmbKategori.Items.Add("Semua Kategori");

                if (this._dtSemua != null && this._dtSemua.Columns.Contains("nama_kategori"))
                {
                    DataView view = new DataView(this._dtSemua);
                    DataTable distinctKategori = view.ToTable(true, "nama_kategori");

                    foreach (DataRow row in distinctKategori.Rows)
                    {
                        string namaKatMentah;
                        if (row["nama_kategori"] != DBNull.Value)
                        {
                            namaKatMentah = row["nama_kategori"].ToString();
                        }
                        else
                        {
                            namaKatMentah = "";
                        }

                        if (!string.IsNullOrWhiteSpace(namaKatMentah))
                        {
                            Models.Category katObj = new Models.Category(namaKatMentah);
                            this._cmbKategori.Items.Add(katObj.NamaKategori);
                        }
                        else
                        {
                            bool dataKosongDilewati = true;
                        }
                    }
                }
                else
                {
                    bool tidakAdaDataUntukDropdown = true;
                }

                this._cmbKategori.SelectedIndex = 0;
            }
        }

        private void TampilkanKartu(DataTable dt)
        {
            this.flpKartu.SuspendLayout();
            this.flpKartu.Controls.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                Label lblKosong = new Label
                {
                    Text = "😔 Yahh, lapaknya lagi sepi nih... Belum ada barang.",
                    Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(20)
                };
                this.flpKartu.Controls.Add(lblKosong);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    int idProduk = 0;
                    if (dt.Columns.Contains("id_produk") && row["id_produk"] != DBNull.Value)
                        int.TryParse(row["id_produk"].ToString(), out idProduk);

                    string nama = (dt.Columns.Contains("nama_produk") && row["nama_produk"] != DBNull.Value)
                        ? row["nama_produk"].ToString() : "-";

                    string penjual = (dt.Columns.Contains("nama_toko") && row["nama_toko"] != DBNull.Value)
                        ? row["nama_toko"].ToString() : "Penjual Anonim";

                    long harga = 0;
                    if (dt.Columns.Contains("harga_dasar") && row["harga_dasar"] != DBNull.Value)
                        long.TryParse(row["harga_dasar"].ToString(), out harga);
                    string hargaStr = "Rp " + harga.ToString("N0");

                    string slot = "Ready (Bebas)";
                    if (dt.Columns.Contains("target_kuota") && row["target_kuota"] != DBNull.Value)
                    {
                        int kuota = Convert.ToInt32(row["target_kuota"]);
                        int terpesan = (dt.Columns.Contains("terpesan") && row["terpesan"] != DBNull.Value)
                            ? Convert.ToInt32(row["terpesan"]) : 0;
                        int sisa = kuota - terpesan;
                        slot = sisa > 0 ? $"Sisa {sisa} Slot!" : "⛔ Ludes/Penuh!";
                    }

                    string tipePo = (dt.Columns.Contains("judul_po") && row["judul_po"] != DBNull.Value)
                    ? row["judul_po"].ToString() : "Reguler";

                    // Buat objek Product sementara untuk akses DapatkanLabelPromo()
                    int hargaDasarTemp = 0;
                    if (dt.Columns.Contains("harga_dasar") && row["harga_dasar"] != DBNull.Value)
                        int.TryParse(row["harga_dasar"].ToString(), out hargaDasarTemp);
                    if (hargaDasarTemp <= 0) hargaDasarTemp = 1; // fallback agar konstruktor tidak throw

                    int idPenjualTemp = dt.Columns.Contains("id_penjual") && row["id_penjual"] != DBNull.Value
                        ? Convert.ToInt32(row["id_penjual"]) : 1;

                    Models.Product produkTemp = new Models.Product(idPenjualTemp, 1, nama, hargaDasarTemp);

                    // Set JenisPo jika ada
                    if (dt.Columns.Contains("jenis_po") && row["jenis_po"] != DBNull.Value)
                        produkTemp.JenisPo = row["jenis_po"].ToString();

                    // Set TargetKuota jika ada
                    if (dt.Columns.Contains("target_kuota") && row["target_kuota"] != DBNull.Value)
                        produkTemp.TargetKuota = Convert.ToInt32(row["target_kuota"]);

                    // Set HargaDiskon jika ada
                    if (dt.Columns.Contains("harga_diskon") && row["harga_diskon"] != DBNull.Value && row["harga_diskon"] != DBNull.Value)
                    {
                        int diskon = Convert.ToInt32(row["harga_diskon"]);
                        if (diskon > 0 && diskon < hargaDasarTemp)
                            produkTemp.HargaDiskon = diskon;
                    }

                    string labelPromo = produkTemp.DapatkanLabelPromo(); // ← sambungkan dead code

                    // Tambahkan label badge ke kartu produk (setelah label nama produk):
                    Label lblBadge = new Label
                    {
                        Text = labelPromo,
                        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                        BackColor = labelPromo.Contains("🔥") ? Color.FromArgb(255, 220, 100)
                                  : labelPromo.Contains("📦") ? Color.FromArgb(200, 230, 255)
                                  : Color.FromArgb(220, 255, 220),
                        ForeColor = Color.FromArgb(36, 0, 70),
                        AutoSize = true,
                        Padding = new Padding(4, 2, 4, 2),
                        // Sesuaikan posisi dengan layout kartu yang ada
                    };
                    // tambahkan lblBadge ke panel kartu produk

                    byte[] fotoData = (dt.Columns.Contains("foto_produk") && row["foto_produk"] != DBNull.Value)
                        ? (byte[])row["foto_produk"] : null;

                    // Kolom in_sesi_po dari query yang sudah diupdate di ProductRepository
                    bool inSesiPo = true;
                    if (dt.Columns.Contains("in_sesi_po") && row["in_sesi_po"] != DBNull.Value)
                        inSesiPo = Convert.ToBoolean(row["in_sesi_po"]);

                    this.flpKartu.Controls.Add(
                        this.BuatKartu(idProduk, nama, penjual, hargaStr, slot, tipePo, fotoData, inSesiPo));
                }
            }

            this.flpKartu.ResumeLayout();
        }

        private Panel BuatKartu(int idProduk, string nama, string penjual, string harga,
            string slot, string tipePo, byte[] fotoData, bool inSesiPo = true)
        {
            Panel pnl = new Panel
            {
                Size = new Size(CARD_W, CARD_H),
                BackColor = Color.FromArgb(235, 204, 255),
                Margin = new Padding(10, 10, 15, 15),
                Cursor = Cursors.Default
            };

            pnl.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnl.ClientRectangle,
                    Color.FromArgb(36, 0, 70), ButtonBorderStyle.Solid);
            };

            // ── Foto ──
            PictureBox pbFoto = new PictureBox
            {
                Width = 200,
                Height = 150,
                Top = 10,
                Left = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            if (fotoData != null && fotoData.Length > 0)
            {
                try
                {
                    var images = ImageHelper.UnpackImages(fotoData);
                    if (images.Count > 0 && images[0].Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(images[0]))
                            pbFoto.Image = new Bitmap(Image.FromStream(ms));
                    }
                }
                catch
                {
                    pbFoto.Image = null;
                }
            }

            if (pbFoto.Image == null)
            {
                pbFoto.Controls.Add(new Label
                {
                    Text = "No Image",
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                });
            }

            pnl.Controls.Add(pbFoto);

            // ── Badge tipe PO ──
            Color bgColorTipe = tipePo == "Reguler"
                ? Color.FromArgb(155, 246, 255)
                : Color.FromArgb(253, 255, 182);

            pnl.Controls.Add(new Label
            {
                Text = tipePo,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                BackColor = bgColorTipe,
                Location = new Point(10, 170),
                AutoSize = true,
                Padding = new Padding(3)
            });

            // ── Nama produk ──
            pnl.Controls.Add(new Label
            {
                Text = nama,
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Location = new Point(10, 195),
                Size = new Size(CARD_W - 20, 45),
                AutoSize = false
            });

            // ── Harga ──
            pnl.Controls.Add(new Label
            {
                Text = harga,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Location = new Point(10, 245),
                AutoSize = true
            });

            // ── Penjual & slot ──
            Color lblSlotWarna = slot.Contains("Penuh") ? Color.Red : Color.FromArgb(110, 80, 140);
            pnl.Controls.Add(new Label
            {
                Text = $"🏪 {penjual}\n🔥 {slot}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = lblSlotWarna,
                Location = new Point(10, 270),
                Size = new Size(CARD_W - 20, 30),
                AutoSize = false
            });

            // ── Logika tombol ──
            bool isPenuh = slot.Contains("Penuh");
            // bisaDipesan: harus dalam sesi PO aktif DAN belum penuh
            bool bisaDipesan = inSesiPo && !isPenuh;

            // ── Tombol Detail ──
            Button btnDetail = new Button
            {
                Text = "🔍 Cek Detail",
                Font = new Font("Segoe UI Black", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(72, 0, 120),
                ForeColor = Color.FromArgb(254, 252, 200),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(10, 305),
                Size = new Size(96, 30)
            };
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += (s, e) =>
            {
                if (this.OnNavigateDetailProduk != null)
                {
                    this.OnNavigateDetailProduk.Invoke(idProduk);
                }
                else
                {
                    var parentPanel = this.Parent;
                    if (parentPanel != null)
                    {
                        parentPanel.Controls.Clear();
                        DetailProdukControl detailPage = new DetailProdukControl(this._user, idProduk);
                        detailPage.Dock = DockStyle.Fill;
                        detailPage.OnNavigateKembali += () =>
                        {
                            parentPanel.Controls.Clear();
                            KatalogProdukControl katalogBaru = new KatalogProdukControl(this._user);
                            katalogBaru.Dock = DockStyle.Fill;
                            parentPanel.Controls.Add(katalogBaru);
                        };
                        detailPage.OnNavigateKeranjang += () =>
                        {
                            parentPanel.Controls.Clear();
                            var trxCtrl = new Controllers.TransactionController(this._user.IdUser);
                            var keranjang = new KeranjangBelanjaControl(this._user, trxCtrl);
                            keranjang.Dock = DockStyle.Fill;
                            parentPanel.Controls.Add(keranjang);
                        };
                        parentPanel.Controls.Add(detailPage);
                    }
                }
            };
            pnl.Controls.Add(btnDetail);

            // ── Tombol Keranjang / Sikat ──
            string teksBtn;
            Color btnSikatBg;
            Color btnSikatTeks;

            if (isPenuh)
            {
                teksBtn = "Habis😭";
                btnSikatBg = Color.Gray;
                btnSikatTeks = Color.White;
            }
            else if (!inSesiPo)
            {
                // Produk tidak dalam sesi PO aktif — hanya bisa lihat detail
                teksBtn = "👁️ Lihat Saja";
                btnSikatBg = Color.FromArgb(180, 180, 180);
                btnSikatTeks = Color.FromArgb(80, 80, 80);
            }
            else
            {
                teksBtn = "🛒 Sikat!";
                btnSikatBg = Color.FromArgb(254, 245, 100);
                btnSikatTeks = Color.FromArgb(70, 50, 0);
            }

            Button btnKeranjang = new Button
            {
                Text = teksBtn,
                Font = new Font("Segoe UI Black", 8F, FontStyle.Bold),
                BackColor = btnSikatBg,
                ForeColor = btnSikatTeks,
                FlatStyle = FlatStyle.Flat,
                Cursor = bisaDipesan ? Cursors.Hand : Cursors.No,
                Location = new Point(112, 305),
                Size = new Size(98, 30),
                Enabled = bisaDipesan
            };
            btnKeranjang.FlatAppearance.BorderSize = 0;

            if (bisaDipesan)
            {
                btnKeranjang.Click += (s, e) =>
                {
                    Models.Product pUtuh = this._prodCtrl.GetProdukById(idProduk);
                    if (pUtuh != null)
                    {
                        int minOrder = pUtuh.MinOrder > 0 ? pUtuh.MinOrder : 1;
                        var (sukses, pesan) = this._trxCtrl.TambahItemKeKeranjang(
                            idProduk, this._user.Nama, minOrder, "");

                        if (sukses)
                            this.TampilkanInfo($"✅ '{nama}' berhasil masuk keranjang!", true);
                        else
                            this.TampilkanInfo($"❌ {pesan}", false);
                    }
                    else
                    {
                        this.TampilkanInfo("Gagal mengambil data produk secara penuh.", false);
                    }
                };
            }

            pnl.Controls.Add(btnKeranjang);
            return pnl;
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            this.lblInfo.Text = pesan;
            this.lblInfo.Visible = true;
            this.lblInfo.BackColor = sukses
                ? Color.FromArgb(210, 255, 230)
                : Color.FromArgb(255, 220, 220);
            this.lblInfo.ForeColor = sukses
                ? Color.FromArgb(0, 100, 50)
                : Color.FromArgb(150, 0, 0);

            this._timerInfo.Stop();
            this._timerInfo.Start();
        }

        private void KatalogProdukControl_Resize(object sender, EventArgs e)
        {
            this.AturLayout();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            this.TerapkanFilterGabungan();
        }

        private void txtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                this.TerapkanFilterGabungan();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (this.txtCari != null) this.txtCari.Text = "";
            if (this._cmbKategori != null) this._cmbKategori.SelectedIndex = 0;
            if (this._dtSemua != null) this.TampilkanKartu(this._dtSemua);
        }

        private void TerapkanFilterGabungan()
        {
            if (this._dtSemua == null) return;

            string kataKunci = this.txtCari?.Text?.Trim() ?? "";
            string kategoriPilihan = (this._cmbKategori != null
                && this._cmbKategori.SelectedIndex > 0
                && this._cmbKategori.SelectedItem != null)
                ? this._cmbKategori.SelectedItem.ToString() : "";

            DataTable dtFilter = this._dtSemua.Clone();

            foreach (DataRow row in this._dtSemua.Rows)
            {
                // Filter teks
                bool lolosTeks = string.IsNullOrEmpty(kataKunci);
                if (!lolosTeks)
                {
                    foreach (DataColumn col in this._dtSemua.Columns)
                    {
                        if (row[col] != DBNull.Value &&
                            row[col].ToString().ToLower().Contains(kataKunci.ToLower()))
                        {
                            lolosTeks = true;
                            break;
                        }
                    }
                }

                // Filter kategori
                bool lolosKategori = string.IsNullOrEmpty(kategoriPilihan);
                if (!lolosKategori && this._dtSemua.Columns.Contains("nama_kategori"))
                {
                    string namaKatDb = row["nama_kategori"] != DBNull.Value
                        ? row["nama_kategori"].ToString() : "";
                    Models.Category katRow = new Models.Category(namaKatDb);
                    lolosKategori = katRow.PencarianCocok(kategoriPilihan);
                }

                if (lolosTeks && lolosKategori)
                    dtFilter.ImportRow(row);
            }

            this.TampilkanKartu(dtFilter);
        }

        private void AturLayout()
        {
            int w = Math.Max(this.Width, 600);

            if (this.pnlFilter != null) this.pnlFilter.Width = w;
            if (this.lblInfo != null) this.lblInfo.Width = w - 60;
            if (this.flpKartu != null)
                this.flpKartu.SetBounds(0, 205, w, Math.Max(300, this.Height - 205)); 

            if (this._cmbKategori != null && this.txtCari != null)
            {
                this._cmbKategori.Top = this.txtCari.Top;
                this._cmbKategori.Left = this.txtCari.Left + this.txtCari.Width + 15;

                if (this.btnCari != null)
                {
                    this.btnCari.Top = this.txtCari.Top - 1;
                    this.btnCari.Left = this._cmbKategori.Left + this._cmbKategori.Width + 15;
                }

                if (this.btnReset != null)
                {
                    this.btnReset.Top = this.txtCari.Top - 1;
                    this.btnReset.Left = this.btnCari != null
                        ? this.btnCari.Left + this.btnCari.Width + 10
                        : this._cmbKategori.Left + this._cmbKategori.Width + 10;
                }
            }
        }

        private void btnKembaliSesiPO_Click(object sender, EventArgs e)
        {
            this.OnNavigateKembali?.Invoke();
        }
    }
}
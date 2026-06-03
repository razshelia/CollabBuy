using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.View.Helper;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class KatalogProdukControl : UserControl
    {
        private readonly Models.User _user;
        private readonly ProductController _prodCtrl;
        private readonly TransactionController _trxCtrl;

        private DataTable _dtSemua;
        private System.Windows.Forms.Timer _timerInfo;

        private ComboBox _cmbKategori;

        public event Action<int> OnNavigateDetailProduk;
        public event Action OnNavigateKeranjang;

        private const int CARD_W = 220;
        private const int CARD_H = 340;

        public KatalogProdukControl(Models.User user)
        {
            this.InitializeComponent();

            this._user = user;
            this._prodCtrl = new ProductController();
            this._trxCtrl = new TransactionController(this._user.GetIdUser());

            this._timerInfo = new System.Windows.Forms.Timer();
            this._timerInfo.Interval = 3000;
            this._timerInfo.Tick += (s, e) =>
            {
                this.lblInfo.Visible = false;
                this._timerInfo.Stop();
            };

            this.Dock = DockStyle.Fill;
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
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
                    {
                        int.TryParse(row["id_produk"].ToString(), out idProduk);
                    }
                    else
                    {
                        bool idLewati = true;
                    }

                    string nama;
                    if (dt.Columns.Contains("nama_produk") && row["nama_produk"] != DBNull.Value)
                    {
                        nama = row["nama_produk"].ToString();
                    }
                    else
                    {
                        nama = "-";
                    }

                    string penjual;
                    if (dt.Columns.Contains("nama_toko") && row["nama_toko"] != DBNull.Value)
                    {
                        penjual = row["nama_toko"].ToString();
                    }
                    else
                    {
                        penjual = "Penjual Anonim";
                    }

                    long harga = 0;
                    if (dt.Columns.Contains("harga_dasar") && row["harga_dasar"] != DBNull.Value)
                    {
                        long.TryParse(row["harga_dasar"].ToString(), out harga);
                    }
                    else
                    {
                        bool hargaLewati = true;
                    }
                    string hargaStr = "Rp " + harga.ToString("N0");

                    string slot = "Ready (Bebas)";
                    if (dt.Columns.Contains("target_kuota") && row["target_kuota"] != DBNull.Value)
                    {
                        int kuota = Convert.ToInt32(row["target_kuota"]);
                        int terpesan = 0;

                        if (dt.Columns.Contains("terpesan") && row["terpesan"] != DBNull.Value)
                        {
                            terpesan = Convert.ToInt32(row["terpesan"]);
                        }
                        else
                        {
                            bool kuotaLewati = true;
                        }

                        int sisa = kuota - terpesan;
                        if (sisa > 0)
                        {
                            slot = $"Sisa {sisa} Slot!";
                        }
                        else
                        {
                            slot = "⛔ Ludes/Penuh!";
                        }
                    }
                    else
                    {
                        bool slotBebas = true;
                    }

                    string tipePo;
                    if (dt.Columns.Contains("judul_po") && row["judul_po"] != DBNull.Value)
                    {
                        tipePo = row["judul_po"].ToString();
                    }
                    else
                    {
                        tipePo = "Reguler";
                    }


                    byte[] fotoData = null;
                    if (dt.Columns.Contains("foto_produk") && row["foto_produk"] != DBNull.Value)
                    {
                        fotoData = (byte[])row["foto_produk"];
                    }
                    else
                    {
                        bool tanpaFoto = true;
                    }

                    this.flpKartu.Controls.Add(this.BuatKartu(idProduk, nama, penjual, hargaStr, slot, tipePo, fotoData));
                }
            }

            this.flpKartu.ResumeLayout();
        }

        private Panel BuatKartu(int idProduk, string nama, string penjual, string harga, string slot, string tipePo, byte[] fotoData)
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
                ControlPaint.DrawBorder(e.Graphics, pnl.ClientRectangle, Color.FromArgb(36, 0, 70), ButtonBorderStyle.Solid);
            };

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
                        {
                            pbFoto.Image = new Bitmap(Image.FromStream(ms));
                        }
                    }
                    else
                    {
                        bool formatSalah = true;
                    }
                }
                catch
                {
                    pbFoto.Image = null;
                }
            }
            else
            {
                bool tidakAdaGambar = true;
            }

            if (pbFoto.Image == null)
            {
                Label lblNoImage = new Label
                {
                    Text = "No Image",
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                pbFoto.Controls.Add(lblNoImage);
            }
            else
            {
                bool gambarAda = true;
            }

            pnl.Controls.Add(pbFoto);

            Color bgColorTipe;
            if (tipePo == "Reguler")
            {
                bgColorTipe = Color.FromArgb(155, 246, 255);
            }
            else
            {
                bgColorTipe = Color.FromArgb(253, 255, 182);
            }

            Label lblTipe = new Label
            {
                Text = tipePo,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                BackColor = bgColorTipe,
                Location = new Point(10, 170),
                AutoSize = true,
                Padding = new Padding(3)
            };
            pnl.Controls.Add(lblTipe);

            Label lblNama = new Label
            {
                Text = nama,
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Location = new Point(10, 195),
                Size = new Size(CARD_W - 20, 45),
                AutoSize = false
            };
            pnl.Controls.Add(lblNama);

            Label lblHarga = new Label
            {
                Text = harga,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Location = new Point(10, 245),
                AutoSize = true
            };
            pnl.Controls.Add(lblHarga);

            Color lblSlotWarna;
            if (slot.Contains("Penuh"))
            {
                lblSlotWarna = Color.Red;
            }
            else
            {
                lblSlotWarna = Color.FromArgb(110, 80, 140);
            }

            Label lblPenjualSlot = new Label
            {
                Text = $"🏪 {penjual}\n🔥 {slot}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = lblSlotWarna,
                Location = new Point(10, 270),
                Size = new Size(CARD_W - 20, 30),
                AutoSize = false
            };
            pnl.Controls.Add(lblPenjualSlot);

            bool isPenuh = slot.Contains("Penuh");

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
                        parentPanel.Controls.Add(detailPage);
                    }
                    else
                    {
                        bool panelIndukKosong = true;
                    }
                }
            };
            pnl.Controls.Add(btnDetail);

            Color btnSikatBg;
            Color btnSikatTeks;

            if (isPenuh)
            {
                btnSikatBg = Color.Gray;
                btnSikatTeks = Color.White;
            }
            else
            {
                btnSikatBg = Color.FromArgb(254, 245, 100);
                btnSikatTeks = Color.FromArgb(70, 50, 0);
            }

            Button btnKeranjang = new Button
            {
                Text = isPenuh ? "Habis😭" : "🛒 Sikat!",
                Font = new Font("Segoe UI Black", 8F, FontStyle.Bold),
                BackColor = btnSikatBg,
                ForeColor = btnSikatTeks,
                FlatStyle = FlatStyle.Flat,
                Cursor = isPenuh ? Cursors.No : Cursors.Hand,
                Location = new Point(112, 305),
                Size = new Size(98, 30),
                Enabled = !isPenuh
            };
            btnKeranjang.FlatAppearance.BorderSize = 0;

            if (!isPenuh)
            {
                btnKeranjang.Click += (s, e) =>
                {
                    Models.Product pUtuh = this._prodCtrl.GetProdukById(idProduk);
                    if (pUtuh != null)
                    {
                        int minOrder;
                        if (pUtuh.MinOrder > 0)
                        {
                            minOrder = pUtuh.MinOrder;
                        }
                        else
                        {
                            minOrder = 1;
                        }

                        var (sukses, pesan) = this._trxCtrl.TambahItemKeKeranjang(idProduk, this._user.GetNama(), minOrder, "");

                        if (sukses)
                        {
                            this.TampilkanInfo($"✅ '{nama}' berhasil masuk keranjang!", true);
                        }
                        else
                        {
                            this.TampilkanInfo($"❌ {pesan}", false);
                        }
                    }
                    else
                    {
                        this.TampilkanInfo("Gagal mengambil data produk secara penuh.", false);
                    }
                };
            }
            else
            {
                bool lewatiEventKeranjang = true;
            }

            pnl.Controls.Add(btnKeranjang);
            return pnl;
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            this.lblInfo.Text = pesan;
            this.lblInfo.Visible = true;

            if (sukses)
            {
                this.lblInfo.BackColor = Color.FromArgb(210, 255, 230);
                this.lblInfo.ForeColor = Color.FromArgb(0, 100, 50);
            }
            else
            {
                this.lblInfo.BackColor = Color.FromArgb(255, 220, 220);
                this.lblInfo.ForeColor = Color.FromArgb(150, 0, 0);
            }

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
            {
                this.TerapkanFilterGabungan();
            }
            else
            {
                bool bukanEnter = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (this.txtCari != null)
            {
                this.txtCari.Text = "";
            }
            else
            {
                bool pencarianNull = true;
            }

            if (this._cmbKategori != null)
            {
                this._cmbKategori.SelectedIndex = 0;
            }
            else
            {
                bool kategoriNull = true;
            }

            if (this._dtSemua != null)
            {
                this.TampilkanKartu(this._dtSemua);
            }
            else
            {
                bool dataNull = true;
            }
        }

        private void TerapkanFilterGabungan()
        {
            if (this._dtSemua == null)
            {
                bool dataAwalKosong = true;
            }
            else
            {
                string kataKunci;
                if (this.txtCari != null && this.txtCari.Text != null)
                {
                    kataKunci = this.txtCari.Text.Trim();
                }
                else
                {
                    kataKunci = "";
                }

                string kategoriPilihan;
                if (this._cmbKategori != null && this._cmbKategori.SelectedIndex > 0 && this._cmbKategori.SelectedItem != null)
                {
                    kategoriPilihan = this._cmbKategori.SelectedItem.ToString();
                }
                else
                {
                    kategoriPilihan = "";
                }

                DataTable dtFilter = this._dtSemua.Clone();

                foreach (DataRow row in this._dtSemua.Rows)
                {
                    bool lolosTeks;
                    if (string.IsNullOrEmpty(kataKunci))
                    {
                        lolosTeks = true;
                    }
                    else
                    {
                        lolosTeks = false;
                        foreach (DataColumn col in this._dtSemua.Columns)
                        {
                            if (row[col] != DBNull.Value && row[col].ToString().ToLower().Contains(kataKunci.ToLower()))
                            {
                                lolosTeks = true;
                                break;
                            }
                            else
                            {
                                bool iterasiTeksBerlanjut = true;
                            }
                        }
                    }

                    bool lolosKategori;
                    if (string.IsNullOrEmpty(kategoriPilihan))
                    {
                        lolosKategori = true;
                    }
                    else
                    {
                        if (this._dtSemua.Columns.Contains("nama_kategori"))
                        {
                            string namaKatDb;
                            if (row["nama_kategori"] != DBNull.Value)
                            {
                                namaKatDb = row["nama_kategori"].ToString();
                            }
                            else
                            {
                                namaKatDb = "";
                            }

                            Models.Category katRow = new Models.Category(namaKatDb);

                            if (katRow.PencarianCocok(kategoriPilihan))
                            {
                                lolosKategori = true;
                            }
                            else
                            {
                                lolosKategori = false;
                            }
                        }
                        else
                        {
                            lolosKategori = false;
                        }
                    }

                    if (lolosTeks && lolosKategori)
                    {
                        dtFilter.ImportRow(row);
                    }
                    else
                    {
                        bool barisDitendang = true;
                    }
                }

                this.TampilkanKartu(dtFilter);
            }
        }

        private void AturLayout()
        {
            int w = Math.Max(this.Width, 600);

            if (this.pnlFilter != null)
            {
                this.pnlFilter.Width = w;
            }
            else { bool pass1 = true; }

            if (this.lblInfo != null)
            {
                this.lblInfo.Width = w - 60;
            }
            else { bool pass2 = true; }

            if (this.flpKartu != null)
            {
                this.flpKartu.SetBounds(0, 190, w, Math.Max(300, this.Height - 190));
            }
            else { bool pass3 = true; }

            if (this._cmbKategori != null && this.txtCari != null)
            {
                this._cmbKategori.Top = this.txtCari.Top;
                this._cmbKategori.Left = this.txtCari.Left + this.txtCari.Width + 15;

                if (this.btnCari != null)
                {
                    this.btnCari.Top = this.txtCari.Top - 1;
                    this.btnCari.Left = this._cmbKategori.Left + this._cmbKategori.Width + 15;
                }
                else { bool pass4 = true; }

                if (this.btnReset != null)
                {
                    this.btnReset.Top = this.txtCari.Top - 1;
                    if (this.btnCari != null)
                    {
                        this.btnReset.Left = this.btnCari.Left + this.btnCari.Width + 10;
                    }
                    else
                    {
                        this.btnReset.Left = this._cmbKategori.Left + this._cmbKategori.Width + 10;
                    }
                }
                else { bool pass5 = true; }
            }
            else
            {
                bool pass6 = true;
            }
        }
    }
}
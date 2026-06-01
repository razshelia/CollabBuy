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

        // KOMPONEN UI BARU UNTUK FILTER KATEGORI (Dibuat lewat kode biar aman)
        private ComboBox _cmbKategori;

        // Events Navigasi
        public event Action<int> OnNavigateDetailProduk;
        public event Action OnNavigateKeranjang;

        private const int CARD_W = 220;
        private const int CARD_H = 340;

        public KatalogProdukControl(Models.User user)
        {
            InitializeComponent();
            _user = user;
            _prodCtrl = new ProductController();
            _trxCtrl = new TransactionController(_user.GetIdUser());

            _timerInfo = new System.Windows.Forms.Timer();
            _timerInfo.Interval = 3000;
            _timerInfo.Tick += (s, e) => { lblInfo.Visible = false; _timerInfo.Stop(); };

            this.Dock = DockStyle.Fill;
        }

        private void KatalogProdukControl_Load(object sender, EventArgs e)
        {
            InisialisasiDropdownKategori();
            MuatKatalog();
            AturLayout();
        }

        // =========================================================
        // FITUR BARU: Membuat Dropdown Filter Kategori
        // =========================================================
        private void InisialisasiDropdownKategori()
        {
            if (pnlFilter == null) return;

            _cmbKategori = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Width = 180,
                Cursor = Cursors.Hand
            };

            _cmbKategori.SelectedIndexChanged += (s, e) => TerapkanFilterGabungan();

            pnlFilter.Controls.Add(_cmbKategori);
            _cmbKategori.BringToFront();
        }

        private void MuatKatalog()
        {
            try
            {
                _dtSemua = _prodCtrl.GetKatalogUtama();
                PopulasiDropdownKategori(); // Isi dropdown setelah data katalog ditarik
                TampilkanKartu(_dtSemua);
            }
            catch (Exception ex)
            {
                TampilkanInfo($"Waduh gagal load katalog: {ex.Message}", false);
            }
        }

        // =========================================================
        // OOP IN ACTION: Menggunakan Model Category untuk Dropdown
        // =========================================================
        private void PopulasiDropdownKategori()
        {
            if (_cmbKategori == null) return;

            _cmbKategori.Items.Clear();
            _cmbKategori.Items.Add("Semua Kategori"); // Opsi default

            if (_dtSemua != null && _dtSemua.Columns.Contains("nama_kategori"))
            {
                // Mengambil nilai unik dari kolom nama_kategori di database
                DataView view = new DataView(_dtSemua);
                DataTable distinctKategori = view.ToTable(true, "nama_kategori");

                foreach (DataRow row in distinctKategori.Rows)
                {
                    string namaKatMentah = row["nama_kategori"]?.ToString() ?? "";
                    if (!string.IsNullOrWhiteSpace(namaKatMentah))
                    {
                        // MENGGUNAKAN CLASS MODEL: 
                        // Saat objek dibuat, nama kategori otomatis dirapikan (Title Case)
                        Models.Category katObj = new Models.Category(namaKatMentah);

                        // Masukkan nama yang sudah rapi ke dalam ComboBox
                        _cmbKategori.Items.Add(katObj.GetNamaKategori());
                    }
                }
            }

            _cmbKategori.SelectedIndex = 0; // Pilih "Semua Kategori" sebagai awal
        }

        private void TampilkanKartu(DataTable dt)
        {
            flpKartu.SuspendLayout();
            flpKartu.Controls.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                var lblKosong = new Label
                {
                    Text = "😔 Yahh, lapaknya lagi sepi nih... Belum ada barang.",
                    Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(20)
                };
                flpKartu.Controls.Add(lblKosong);
                flpKartu.ResumeLayout();
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                int idProduk = 0;
                if (dt.Columns.Contains("id_produk")) int.TryParse(row["id_produk"]?.ToString(), out idProduk);

                string nama = dt.Columns.Contains("nama_produk") ? row["nama_produk"]?.ToString() ?? "-" : "-";
                string penjual = dt.Columns.Contains("nama_toko") ? row["nama_toko"]?.ToString() ?? "Penjual Anonim" : "Lapak Kampus";

                long harga = 0;
                if (dt.Columns.Contains("harga_dasar")) long.TryParse(row["harga_dasar"]?.ToString(), out harga);
                string hargaStr = "Rp " + harga.ToString("N0");

                string slot = "Ready (Bebas)";
                if (dt.Columns.Contains("target_kuota") && row["target_kuota"] != DBNull.Value)
                {
                    int kuota = Convert.ToInt32(row["target_kuota"]);
                    int terpesan = 0;

                    if (dt.Columns.Contains("terpesan") && row["terpesan"] != DBNull.Value)
                        terpesan = Convert.ToInt32(row["terpesan"]);

                    int sisa = kuota - terpesan;
                    slot = sisa > 0 ? $"Sisa {sisa} Slot!" : "⛔ Ludes/Penuh!";
                }

                string tipePo = "Reguler";
                if (dt.Columns.Contains("judul_po") && row["judul_po"] != DBNull.Value)
                    tipePo = row["judul_po"]?.ToString() ?? "Reguler";

                byte[] fotoData = null;
                if (dt.Columns.Contains("foto_produk") && row["foto_produk"] != DBNull.Value)
                {
                    fotoData = (byte[])row["foto_produk"];
                }

                flpKartu.Controls.Add(BuatKartu(idProduk, nama, penjual, hargaStr, slot, tipePo, fotoData));
            }

            flpKartu.ResumeLayout();
        }

        private Panel BuatKartu(int idProduk, string nama, string penjual, string harga, string slot, string tipePo, byte[] fotoData)
        {
            var pnl = new Panel
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
                        using (MemoryStream ms = new MemoryStream(images[0])) { pbFoto.Image = new Bitmap(Image.FromStream(ms)); }
                    }
                }
                catch { pbFoto.Image = null; }
            }

            if (pbFoto.Image == null)
            {
                Label lblNoImage = new Label { Text = "No Image", ForeColor = Color.Gray, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
                pbFoto.Controls.Add(lblNoImage);
            }
            pnl.Controls.Add(pbFoto);

            var lblTipe = new Label
            {
                Text = tipePo,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                BackColor = tipePo == "Reguler" ? Color.FromArgb(155, 246, 255) : Color.FromArgb(253, 255, 182),
                Location = new Point(10, 170),
                AutoSize = true,
                Padding = new Padding(3)
            };
            pnl.Controls.Add(lblTipe);

            var lblNama = new Label
            {
                Text = nama,
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Location = new Point(10, 195),
                Size = new Size(CARD_W - 20, 45),
                AutoSize = false
            };
            pnl.Controls.Add(lblNama);

            var lblHarga = new Label
            {
                Text = harga,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 24, 154),
                Location = new Point(10, 245),
                AutoSize = true
            };
            pnl.Controls.Add(lblHarga);

            var lblPenjualSlot = new Label
            {
                Text = $"🏪 {penjual}\n🔥 {slot}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = slot.Contains("Penuh") ? Color.Red : Color.FromArgb(110, 80, 140),
                Location = new Point(10, 270),
                Size = new Size(CARD_W - 20, 30),
                AutoSize = false
            };
            pnl.Controls.Add(lblPenjualSlot);

            bool isPenuh = slot.Contains("Penuh");

            var btnDetail = new Button
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
                if (OnNavigateDetailProduk != null) OnNavigateDetailProduk.Invoke(idProduk);
                else
                {
                    var parentPanel = this.Parent;
                    if (parentPanel != null)
                    {
                        parentPanel.Controls.Clear();
                        DetailProdukControl detailPage = new DetailProdukControl(_user, idProduk);
                        detailPage.Dock = DockStyle.Fill;
                        parentPanel.Controls.Add(detailPage);
                    }
                }
            };
            pnl.Controls.Add(btnDetail);

            var btnKeranjang = new Button
            {
                Text = isPenuh ? "Habis😭" : "🛒 Sikat!",
                Font = new Font("Segoe UI Black", 8F, FontStyle.Bold),
                BackColor = isPenuh ? Color.Gray : Color.FromArgb(254, 245, 100),
                ForeColor = isPenuh ? Color.White : Color.FromArgb(70, 50, 0),
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
                    Models.Product pUtuh = _prodCtrl.GetProdukById(idProduk);
                    if (pUtuh != null)
                    {
                        int minOrder = pUtuh.GetMinOrder() > 0 ? pUtuh.GetMinOrder() : 1;
                        var (sukses, pesan) = _trxCtrl.TambahItemKeKeranjang(idProduk, _user.GetNama(), minOrder, "");
                        TampilkanInfo(sukses ? $"✅ '{nama}' berhasil masuk keranjang!" : $"❌ {pesan}", sukses);
                    }
                };
            }
            pnl.Controls.Add(btnKeranjang);
            return pnl;
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            lblInfo.Text = pesan;
            lblInfo.BackColor = sukses ? Color.FromArgb(210, 255, 230) : Color.FromArgb(255, 220, 220);
            lblInfo.ForeColor = sukses ? Color.FromArgb(0, 100, 50) : Color.FromArgb(150, 0, 0);
            lblInfo.Visible = true;
            _timerInfo.Stop();
            _timerInfo.Start();
        }

        private void KatalogProdukControl_Resize(object sender, EventArgs e) => AturLayout();

        // Mengarahkan semua event pencarian ke metode TerapkanFilterGabungan
        private void btnCari_Click(object sender, EventArgs e) => TerapkanFilterGabungan();
        private void txtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) TerapkanFilterGabungan();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (txtCari != null) txtCari.Text = "";
            if (_cmbKategori != null) _cmbKategori.SelectedIndex = 0;
            if (_dtSemua != null) TampilkanKartu(_dtSemua);
        }

        // =========================================================
        // OOP IN ACTION: Filter menggunakan Method dari Class Category
        // =========================================================
        private void TerapkanFilterGabungan()
        {
            if (_dtSemua == null) return;

            string kataKunci = txtCari?.Text.Trim() ?? "";
            string kategoriPilihan = _cmbKategori != null && _cmbKategori.SelectedIndex > 0 ? _cmbKategori.SelectedItem.ToString() : "";

            DataTable dtFilter = _dtSemua.Clone();

            foreach (DataRow row in _dtSemua.Rows)
            {
                // 1. Cek Pencarian Teks
                bool lolosTeks = string.IsNullOrEmpty(kataKunci);
                if (!lolosTeks)
                {
                    foreach (DataColumn col in _dtSemua.Columns)
                    {
                        if (row[col]?.ToString().ToLower().Contains(kataKunci.ToLower()) == true)
                        {
                            lolosTeks = true; break;
                        }
                    }
                }

                // 2. Cek Kategori (Menggunakan Objek Model)
                bool lolosKategori = string.IsNullOrEmpty(kategoriPilihan);
                if (!lolosKategori && _dtSemua.Columns.Contains("nama_kategori"))
                {
                    string namaKatDb = row["nama_kategori"]?.ToString() ?? "";

                    // Buat objek kategori dari data baris ini
                    Models.Category katRow = new Models.Category(namaKatDb);

                    // Panggil method PencarianCocok dari Model OOP untuk mengevaluasi!
                    if (katRow.PencarianCocok(kategoriPilihan))
                    {
                        lolosKategori = true;
                    }
                }

                // Jika lolos kedua filter, masukkan ke hasil
                if (lolosTeks && lolosKategori)
                {
                    dtFilter.ImportRow(row);
                }
            }

            TampilkanKartu(dtFilter);
        }

        private void AturLayout()
        {
            int w = Math.Max(this.Width, 600);
            if (pnlFilter != null) pnlFilter.Width = w;
            if (lblInfo != null) lblInfo.Width = w - 60;
            if (flpKartu != null) flpKartu.SetBounds(0, 190, w, Math.Max(300, this.Height - 190));

            if (_cmbKategori != null && txtCari != null)
            {
                // 1. Posisi Dropdown Kategori di sebelah kanan TextBox Cari
                _cmbKategori.Top = txtCari.Top;
                _cmbKategori.Left = txtCari.Left + txtCari.Width + 15;

                // 2. Geser Tombol Cari ke sebelah kanan Dropdown Kategori
                if (btnCari != null)
                {
                    btnCari.Top = txtCari.Top - 1; // Penyesuaian presisi margin atas
                    btnCari.Left = _cmbKategori.Left + _cmbKategori.Width + 15;
                }

                // 3. Geser Tombol Reset ke sebelah kanan Tombol Cari
                if (btnReset != null)
                {
                    btnReset.Top = txtCari.Top - 1; // Penyesuaian presisi margin atas
                    btnReset.Left = btnCari.Left + btnCari.Width + 10;
                }
            }
        }
    }
}
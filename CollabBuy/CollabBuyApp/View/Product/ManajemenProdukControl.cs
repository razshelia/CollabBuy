using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.View.Product
{
    public partial class ManajemenProdukControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly ProductController _productController;
        private readonly CategoryRepository _categoryRepo;
        private byte[] _fotoProdukBytes = null;
        private int? _targetKuotaLama = null;
        private int _editIdProduk = -1;
        private bool _modeEdit = false;
        private readonly PreOrderController _poController;
        private ToolTip _gridTooltip = new ToolTip();
        private DataTable _dtProdukCache;

        public ManajemenProdukControl(Models.User currentUser)
        {
            this.InitializeComponent();
            this._currentUser = currentUser;
            this._productController = new ProductController();
            this._categoryRepo = new CategoryRepository();
            this.Resize += (s, e) => this.AdjustLayout();
            this._poController = new PreOrderController();
        }

        private void ManajemenProdukControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataProduk();
            this.LoadKategori();
            this.LoadSesiPO(hanyaAktif: true);
            this.pnlTambahProduk.Visible = false;
        }
        private void LoadSesiPO(bool hanyaAktif = true)
        {
            try
            {
                DataTable dt = hanyaAktif
                    ? this._poController.GetPOAktifByPenjual(this._currentUser.IdUser)
                    : this._poController.GetPOByPenjual(this._currentUser.IdUser);

                DataTable dtCombo = new DataTable();
                dtCombo.Columns.Add("id_po", typeof(object));
                dtCombo.Columns.Add("judul_po", typeof(string));
                dtCombo.Columns.Add("jenis_po", typeof(string)); // ← TAMBAH INI

                DataRow rowKosong = dtCombo.NewRow();
                rowKosong["id_po"] = DBNull.Value;
                rowKosong["judul_po"] = "— Tidak terikat PO (Reguler) —";
                rowKosong["jenis_po"] = "Biasa"; // ← TAMBAH INI
                dtCombo.Rows.Add(rowKosong);

                foreach (DataRow row in dt.Rows)
                {
                    DataRow r = dtCombo.NewRow();
                    r["id_po"] = row["id_po"];
                    string label = row["judul_po"].ToString();
                    if (!hanyaAktif)
                    {
                        bool aktif = row["is_aktif"] != DBNull.Value && Convert.ToBoolean(row["is_aktif"]);
                        if (!aktif) label += " (Tutup)";
                    }
                    r["judul_po"] = label;
                    r["jenis_po"] = row["jenis_po"] != DBNull.Value ? row["jenis_po"].ToString() : "Biasa"; // ← TAMBAH INI
                    dtCombo.Rows.Add(r);
                }

                this.cbSesiPO.DataSource = dtCombo;
                this.cbSesiPO.DisplayMember = "judul_po";
                this.cbSesiPO.ValueMember = "id_po";
                this.cbSesiPO.SelectedIndexChanged += this.CbSesiPO_SelectedIndexChanged; // ← TAMBAH INI
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load sesi PO: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbSesiPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AturFieldGotongRoyong();
        }

        private void AturFieldGotongRoyong()
        {
            bool isGotongRoyong = false;

            if (this.cbSesiPO.SelectedItem is DataRowView drv)
            {
                string jenis = drv["jenis_po"]?.ToString() ?? "Biasa";
                isGotongRoyong = jenis == "Gotong Royong";
            }

            // Enable/disable field diskon dan kuota
            this.txtHargaDiskon.Enabled = isGotongRoyong;
            this.txtTargetKuota.Enabled = isGotongRoyong;

            // Kosongkan dan beri placeholder kalau bukan GR
            if (!isGotongRoyong)
            {
                this.txtHargaDiskon.Text = "";
                this.txtTargetKuota.Text = "";
                this.txtHargaDiskon.PlaceholderText = "Hanya untuk PO Gotong Royong";
                this.txtTargetKuota.PlaceholderText = "Hanya untuk PO Gotong Royong";
            }
            else
            {
                this.txtHargaDiskon.PlaceholderText = "Kosongkan jika tidak ada";
                this.txtTargetKuota.PlaceholderText = "cth: 50";
            }

            // Visual hint: warna abu kalau disabled
            System.Drawing.Color bgDisabled = System.Drawing.Color.FromArgb(220, 220, 220);
            System.Drawing.Color bgEnabled = System.Drawing.Color.White;
            this.txtHargaDiskon.BackColor = isGotongRoyong ? bgEnabled : bgDisabled;
            this.txtTargetKuota.BackColor = isGotongRoyong ? bgEnabled : bgDisabled;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataProduk();
        }

        // === TOMBOL TAMBAH PRODUK ===
        private void btnTambahProduk_Click(object sender, EventArgs e)
        {
            _modeEdit = false;
            _editIdProduk = -1;
            lblFormTitle.Text = "➕ Input Produk Baru";
            btnSimpanProduk.Text = "✅ Simpan Produk";
            this.ResetFormTambah();
            this.pnlTambahProduk.Visible = true;
            this.LoadSesiPO(hanyaAktif: true);
            this.AturFieldGotongRoyong();
        }

        private void btnBatalTambah_Click(object sender, EventArgs e)
        {
            this.pnlTambahProduk.Visible = false;
            this.ResetFormTambah();
            _modeEdit = false;
            _editIdProduk = -1;
        }

        private void btnPilihFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Pilih Foto Produk";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length > 2097152)
                    {
                        MessageBox.Show("Ukuran foto maksimal 2MB ya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _fotoProdukBytes = File.ReadAllBytes(ofd.FileName);
                    picFotoPreview.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnSimpanProduk_Click(object sender, EventArgs e)
        {
            if (this._currentUser is Penjual penjualAktif && !penjualAktif.ApakahBisaBukaLapak())
            {
                MessageBox.Show(
                    "Lapak kamu belum terverifikasi atau sedang diblokir. Hubungi Admin.",
                    "Akses Ditolak",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNamaProduk.Text))
            {
                MessageBox.Show("Nama produk wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaProduk.Focus();
                return;
            }

            if (cbKategoriProduk.SelectedValue == null)
            {
                MessageBox.Show("Pilih kategori dulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtHargaProduk.Text, out int harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHargaProduk.Focus();
                return;
            }

            if (!int.TryParse(txtMinOrder.Text, out int minOrder) || minOrder <= 0)
            {
                MessageBox.Show("Min. order harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMinOrder.Focus();
                return;
            }

            int idKategori = Convert.ToInt32(cbKategoriProduk.SelectedValue);

            if (_modeEdit)
            {
                // === MODE EDIT / UPDATE ===
                int? hargaDiskonEdit = null;
                if (!string.IsNullOrWhiteSpace(txtHargaDiskon.Text) &&
                    int.TryParse(txtHargaDiskon.Text.Trim(), out int hdEditVal) && hdEditVal > 0)
                {
                    int hargaAkhir = harga - hdEditVal;  // harga dasar - potongan = harga akhir
                    if (hargaAkhir <= 0)
                    {
                        MessageBox.Show("Potongan diskon tidak boleh melebihi harga dasar!", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtHargaDiskon.Focus();
                        return;
                    }
                    hargaDiskonEdit = hargaAkhir;
                }

                int? kuotaEdit = null;
                if (!string.IsNullOrWhiteSpace(txtTargetKuota.Text) &&
                    int.TryParse(txtTargetKuota.Text.Trim(), out int kuotaEditVal) && kuotaEditVal > 0)
                    kuotaEdit = kuotaEditVal;
                else if (string.IsNullOrWhiteSpace(txtTargetKuota.Text))
                    kuotaEdit = _targetKuotaLama;

                var result = this._productController.UpdateProduk(
                    idProduk: _editIdProduk,
                    idPenjual: this._currentUser.IdUser,
                    idKategori: idKategori,
                    namaProduk: txtNamaProduk.Text.Trim(),
                    hargaDasar: harga,
                    minOrder: minOrder,
                    deskripsi: txtDeskripsiProduk.Text.Trim(),
                    fotoProduk: _fotoProdukBytes,
                    targetKuota: kuotaEdit,
                    hargaDiskon: hargaDiskonEdit,
                    idPo: this.cbSesiPO.SelectedValue == DBNull.Value || this.cbSesiPO.SelectedValue == null
                          ? (int?)null
                          : (int?)Convert.ToInt32(this.cbSesiPO.SelectedValue)
                );

                if (result.sukses)
                {
                    MessageBox.Show(result.pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.pnlTambahProduk.Visible = false;
                    this.ResetFormTambah();
                    _modeEdit = false;
                    _editIdProduk = -1;
                    this.LoadDataProduk();
                }
                else
                {
                    MessageBox.Show(result.pesan, "Gagal Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // === MODE TAMBAH BARU ===

                // ✅ kuotaBaru — dideklarasikan di sini
                int? kuotaBaru = null;
                if (!string.IsNullOrWhiteSpace(txtTargetKuota.Text) &&
                    int.TryParse(txtTargetKuota.Text.Trim(), out int kuotaBaruVal) && kuotaBaruVal > 0)
                    kuotaBaru = kuotaBaruVal;

                // ✅ hargaDiskonBaru — hanya satu deklarasi, sudah pakai konversi potongan → harga akhir
                int? hargaDiskonBaru = null;
                if (!string.IsNullOrWhiteSpace(txtHargaDiskon.Text) &&
                    int.TryParse(txtHargaDiskon.Text.Trim(), out int hdBaruVal) && hdBaruVal > 0)
                {
                    int hargaAkhirBaru = harga - hdBaruVal;
                    if (hargaAkhirBaru <= 0)
                    {
                        MessageBox.Show("Potongan diskon tidak boleh melebihi harga dasar!", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtHargaDiskon.Focus();
                        return;
                    }
                    hargaDiskonBaru = hargaAkhirBaru;
                }

                var result = this._productController.TambahProdukBaru(
                    idPenjual: this._currentUser.IdUser,
                    idKategori: idKategori,
                    namaProduk: txtNamaProduk.Text.Trim(),
                    hargaDasar: harga,
                    idPo: this.cbSesiPO.SelectedValue == DBNull.Value || this.cbSesiPO.SelectedValue == null
                        ? (int?)null
                        : (int?)Convert.ToInt32(this.cbSesiPO.SelectedValue),
                    targetKuota: kuotaBaru,
                    hargaDiskon: hargaDiskonBaru,
                    minOrder: minOrder,
                    fotoProduk: _fotoProdukBytes
                );

                if (result.sukses)
                {
                    MessageBox.Show(result.pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.pnlTambahProduk.Visible = false;
                    this.ResetFormTambah();
                    this.LoadDataProduk();
                }
                else
                {
                    MessageBox.Show(result.pesan, "Gagal Simpan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // === TOMBOL EDIT (dari kolom action di grid) ===
        private void btnEdit_GridClick(int idProduk, DataRow rowData)
        {
            _modeEdit = true;
            _editIdProduk = idProduk;
            lblFormTitle.Text = "✏️ Edit Produk";
            btnSimpanProduk.Text = "💾 Update Produk";

            // Load PO DULU (semua, termasuk yang tutup) sebelum form diisi
            this.LoadSesiPO(hanyaAktif: false);

            // Isi form dengan data yang dipilih
            txtNamaProduk.Text = rowData["nama_produk"].ToString();
            txtHargaProduk.Text = rowData["harga_dasar"].ToString();
            txtMinOrder.Text = rowData["min_order"] != DBNull.Value ? rowData["min_order"].ToString() : "1";
            txtDeskripsiProduk.Text = rowData["deskripsi"] != DBNull.Value ? rowData["deskripsi"].ToString() : "";
            if (rowData.Table.Columns.Contains("harga_diskon") && rowData["harga_diskon"] != DBNull.Value)
            {
                int hargaAkhirDB = Convert.ToInt32(rowData["harga_diskon"]);
                int hargaDasarDB = Convert.ToInt32(rowData["harga_dasar"]);
                txtHargaDiskon.Text = (hargaDasarDB - hargaAkhirDB).ToString(); // tampilkan sebagai potongan
            }
            else
            {
                txtHargaDiskon.Text = "";
            }


            // Set kategori
            if (rowData["id_kategori"] != DBNull.Value)
            {
                int idKat = Convert.ToInt32(rowData["id_kategori"]);
                cbKategoriProduk.SelectedValue = idKat;
            }

            // Set PO — sekarang aman karena cbSesiPO sudah terisi lengkap
            if (rowData["id_po"] != DBNull.Value)
            {
                int idPo = Convert.ToInt32(rowData["id_po"]);
                for (int i = 0; i < this.cbSesiPO.Items.Count; i++)
                {
                    DataRowView drv = this.cbSesiPO.Items[i] as DataRowView;
                    if (drv != null && drv["id_po"] != DBNull.Value &&
                        Convert.ToInt32(drv["id_po"]) == idPo)
                    {
                        this.cbSesiPO.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                this.cbSesiPO.SelectedIndex = 0;
            }
            this.AturFieldGotongRoyong();

            // Foto
            _fotoProdukBytes = null;
            picFotoPreview.Image = null;
            if (rowData["foto_produk"] != DBNull.Value)
            {
                try
                {
                    byte[] imgBytes = (byte[])rowData["foto_produk"];
                    if (imgBytes.Length > 1)
                    {
                        _fotoProdukBytes = imgBytes;  //simpan foto lama ke field
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                            picFotoPreview.Image = new Bitmap(Image.FromStream(ms));
                    }
                }
                catch { picFotoPreview.Image = null; }
            }

            this.pnlTambahProduk.Visible = true;
            this.pnlTambahProduk.BringToFront();
        }

        // === TOMBOL HAPUS (dari kolom action di grid) ===
        private void btnHapus_GridClick(int idProduk, string namaProduk)
        {
            var konfirmasi = MessageBox.Show(
                $"Yakin mau hapus produk \"{namaProduk}\"?\n\nProduk tidak akan muncul di katalog tapi data tetap tersimpan di database.",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (konfirmasi == DialogResult.Yes)
            {
                var result = this._productController.HapusProduk(idProduk, this._currentUser.IdUser, namaProduk);

                if (result.sukses)
                {
                    MessageBox.Show(result.pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.LoadDataProduk();
                }
                else
                {
                    MessageBox.Show(result.pesan, "Gagal Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadKategori()
        {
            try
            {
                DataTable dt = this._categoryRepo.GetAll();
                if (dt != null && dt.Columns.Contains("id_kategori") && dt.Columns.Contains("nama_kategori"))
                {
                    if (!dt.Columns.Contains("format_dropdown"))
                        dt.Columns.Add("format_dropdown", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string namaKat = row["nama_kategori"].ToString();
                        if (!string.IsNullOrWhiteSpace(namaKat))
                        {
                            Models.Category katObj = new Models.Category(namaKat);
                            katObj.IdKategori = Convert.ToInt32(row["id_kategori"]);
                            row["format_dropdown"] = katObj.DapatkanFormatDropdown();
                        }
                    }
                }

                cbKategoriProduk.DataSource = dt;
                cbKategoriProduk.DisplayMember = "format_dropdown";
                cbKategoriProduk.ValueMember = "id_kategori";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load kategori: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetFormTambah()
        {
            txtNamaProduk.Clear();
            txtHargaProduk.Clear();
            txtMinOrder.Text = "1";
            txtHargaDiskon.Text = "";
            txtTargetKuota.Text = "";
            _targetKuotaLama = null;
            txtDeskripsiProduk.Clear();
            _fotoProdukBytes = null;
            picFotoPreview.Image = null;
            if (cbKategoriProduk.Items.Count > 0) cbKategoriProduk.SelectedIndex = 0;
            if (this.cbSesiPO.Items.Count > 0) this.cbSesiPO.SelectedIndex = 0;
        }

        private void TerapkanFilterProduk()
        {
            if (this._dtProdukCache == null) return;
            string kata = this.txtCariProduk.Text.Trim().ToLower();
            DataView dv = this._dtProdukCache.DefaultView;
            dv.RowFilter = string.IsNullOrEmpty(kata) ? ""
                : $"nama_produk LIKE '%{kata}%' OR nama_penitip LIKE '%{kata}%'";
            this.dgvLapak.DataSource = dv;
            this.dgvLapak.ClearSelection();
        }

        private void txtCariProduk_TextChanged(object sender, EventArgs e)
        {
            this.TerapkanFilterProduk();
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);

            this.pnlGrid.Width = w;
            this.dgvLapak.Width = this.pnlGrid.Width - 68;
            this.btnRefresh.Left = this.pnlGrid.Width - this.btnRefresh.Width - 34;
            this.btnTambahProduk.Left = this.btnRefresh.Left - this.btnTambahProduk.Width - 12;

            this.pnlTambahProduk.Width = w;
            this.lblSesiPO.Location = new System.Drawing.Point(
                this.cbKategoriProduk.Left,
                this.cbKategoriProduk.Top + this.cbKategoriProduk.Height + 18
            );
            this.cbSesiPO.Location = new System.Drawing.Point(
                this.cbKategoriProduk.Left,
                this.lblSesiPO.Top + this.lblSesiPO.Height + 4
            );
            this.cbSesiPO.Width = this.cbKategoriProduk.Width;
        }

        private void SetupDataGridView()
        {
            this.dgvLapak.AutoGenerateColumns = false;
            this.dgvLapak.Columns.Clear();
            this.dgvLapak.RowTemplate.Height = 80;

            DataGridViewImageColumn colFoto = new DataGridViewImageColumn();
            colFoto.Name = "Foto";
            colFoto.HeaderText = "Foto";
            colFoto.DataPropertyName = "foto_image";
            colFoto.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colFoto.Width = 80;
            this.dgvLapak.Columns.Add(colFoto);

            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProduk", HeaderText = "ID", DataPropertyName = "id_produk", Visible = false });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Barang", DataPropertyName = "nama_produk", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kategori", HeaderText = "Kategori", DataPropertyName = "nama_kategori", Width = 140 });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "PO", HeaderText = "Sesi PO", DataPropertyName = "judul_po", Width = 130 });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga Jual", DataPropertyName = "harga_format", Width = 120 });
            this.dgvLapak.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuota", HeaderText = "Target Kuota", DataPropertyName = "target_kuota", Width = 90 });

            // Kolom tombol Edit
            DataGridViewButtonColumn colEdit = new DataGridViewButtonColumn();
            colEdit.Name = "BtnEdit";
            colEdit.HeaderText = "";
            colEdit.Text = "✏️ Edit";
            colEdit.UseColumnTextForButtonValue = true;
            colEdit.Width = 80;
            colEdit.FlatStyle = FlatStyle.Flat;
            colEdit.DefaultCellStyle.BackColor = Color.FromArgb(90, 24, 154);
            colEdit.DefaultCellStyle.ForeColor = Color.White;
            colEdit.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dgvLapak.Columns.Add(colEdit);

            // Kolom tombol Hapus
            DataGridViewButtonColumn colHapus = new DataGridViewButtonColumn();
            colHapus.Name = "BtnHapus";
            colHapus.HeaderText = "";
            colHapus.Text = "🗑️ Hapus";
            colHapus.UseColumnTextForButtonValue = true;
            colHapus.Width = 85;
            colHapus.FlatStyle = FlatStyle.Flat;
            colHapus.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            colHapus.DefaultCellStyle.ForeColor = Color.White;
            colHapus.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dgvLapak.Columns.Add(colHapus);

            // Handle klik tombol di grid
            this.dgvLapak.CellClick += DgvLapak_CellClick;
            _gridTooltip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ShowAlways = true };
            this.dgvLapak.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                string colName = this.dgvLapak.Columns[e.ColumnIndex].Name;
                if (colName != "PO" && colName != "Nama") return;
                string teks = this.dgvLapak.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                if (teks.Length > 25)
                    _gridTooltip.Show(teks, this.dgvLapak,
                        this.dgvLapak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location, 5000);
            };
            this.dgvLapak.CellMouseLeave += (s, e) => _gridTooltip.Hide(this.dgvLapak);
        }

        private void DgvLapak_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = this.dgvLapak.Rows[e.RowIndex];
            if (row.DataBoundItem == null) return;

            // Ambil data raw dari _dtRaw berdasarkan index
            if (_dtRaw == null || e.RowIndex >= _dtRaw.Rows.Count) return;
            DataRow rawRow = _dtRaw.Rows[e.RowIndex];

            int idProduk = Convert.ToInt32(rawRow["id_produk"]);
            string namaProduk = rawRow["nama_produk"].ToString();

            if (e.ColumnIndex == this.dgvLapak.Columns["BtnEdit"].Index)
            {
                this.btnEdit_GridClick(idProduk, rawRow);
            }
            else if (e.ColumnIndex == this.dgvLapak.Columns["BtnHapus"].Index)
            {
                this.btnHapus_GridClick(idProduk, namaProduk);
            }
        }

        // Simpan dtRaw agar bisa diakses saat klik grid
        private DataTable _dtRaw = null;

        private void LoadDataProduk()
        {
            try
            {
                _dtRaw = this._productController.GetProdukLapak(this._currentUser.IdUser);
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("foto_image", typeof(Image));
                dtUI.Columns.Add("id_produk", typeof(int));
                dtUI.Columns.Add("id_po", typeof(object)); // ← tambah ini
                dtUI.Columns.Add("nama_produk", typeof(string));
                dtUI.Columns.Add("nama_kategori", typeof(string));
                dtUI.Columns.Add("judul_po", typeof(string));
                dtUI.Columns.Add("harga_format", typeof(string));
                dtUI.Columns.Add("target_kuota", typeof(string));

                if (_dtRaw != null)
                {
                    foreach (DataRow row in _dtRaw.Rows)
                    {
                        string judulPo = row.IsNull("judul_po") ? "Reguler" : row["judul_po"].ToString();
                        string namaKatMentah = row.IsNull("nama_kategori") ? "Umum" : row["nama_kategori"].ToString();
                        Models.Category katObj = new Models.Category(namaKatMentah);
                        string kategoriRapi = katObj.NamaKategori;
                        string harga = row["harga_dasar"] != DBNull.Value ? "Rp " + Convert.ToInt32(row["harga_dasar"]).ToString("N0") : "Rp 0";
                        string kuota = row.IsNull("target_kuota") ? "-" : row["target_kuota"].ToString();

                        Image foto = null;
                        if (row["foto_produk"] != DBNull.Value)
                        {
                            try
                            {
                                byte[] imgBytes = (byte[])row["foto_produk"];
                                if (imgBytes.Length > 1)
                                {
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                        foto = Image.FromStream(ms);
                                }
                            }
                            catch { foto = null; }
                        }

                        int idProduk = Convert.ToInt32(row["id_produk"]);
                        object idPoVal = row["id_po"] != DBNull.Value ? row["id_po"] : DBNull.Value;
                        dtUI.Rows.Add(foto, idProduk, idPoVal, row["nama_produk"], kategoriRapi, judulPo, harga, kuota);
                    }
                }

                this._dtProdukCache = dtUI;
                this.dgvLapak.DataSource = this._dtProdukCache;
                this.dgvLapak.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data lapak: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class CheckoutControl : UserControl
    {
        private int _idUser;
        private int _idProduk;
        private Product _produk;
        private Preorder _po;
        private List<dynamic> _daftarPenitip; // simpan sebagai list objek anonymous
        private string _pathBukti;

        private ProductService _productService;
        private PreorderService _preorderService;
        private TransactionService _transactionService;

        public CheckoutControl(int idUser, int idProduk)
        {
            InitializeComponent();
            _idUser = idUser;
            _idProduk = idProduk;
            _daftarPenitip = new List<dynamic>();
            _productService = new ProductService();
            _preorderService = new PreorderService();
            _transactionService = new TransactionService();

            LoadData();
        }

        private void LoadData()
        {
            _produk = _productService.AmbilProdukById(_idProduk);
            if (_produk == null)
            {
                UXHelper.TampilkanError("Produk tidak ditemukan.");
                Kembali();
                return;
            }

            // 1. Cek dulu apakah produk ini punya ID PO atau tidak
            if (!_produk.IdPo.HasValue)
            {
                UXHelper.TampilkanError("Produk ini belum dimasukkan ke dalam sesi Pre-Order.");
                Kembali(); // Mengasumsikan method ini untuk menutup form/kembali ke list
                return;
            }

            // 2. Jika ada (HasValue), panggil service dengan menggunakan .Value
            _po = _preorderService.AmbilPOById(_produk.IdPo.Value);

            // 3. Cek apakah datanya benar-benar ada di database
            if (_po == null)
            {
                UXHelper.TampilkanError("Data Pre-Order tidak ditemukan di sistem.");
                Kembali();
                return;
            }

            // Tampilkan info produk di header
            lblNamaProduk.Text = _produk.NamaProduk;
            lblHargaSatuan.Text = $"Rp {_produk.HargaDasar:N0}";
            lblMinOrder.Text = $"Minimal order: {_produk.MinOrder} pcs";
            lblInfoPO.Text = $"PO: {_po.JudulPo} • Jenis: {_po.JenisPo}";
            lblInfoRekening.Text = $"Rekening: {_po.InfoRekening}";
        }

        // ── STEP 1: Tambah penitip ──
        private void btnTambahPenitip_Click(object sender, EventArgs e)
        {
            string nama = txtNamaPenitip.Text.Trim();
            if (string.IsNullOrWhiteSpace(nama))
            {
                UXHelper.TampilkanError("Nama penitip wajib diisi, bestie!");
                return;
            }

            if (!int.TryParse(txtJumlah.Text.Trim(), out int jumlah) || jumlah < 1)
            {
                UXHelper.TampilkanError("Jumlah pesanan harus angka ≥ 1.");
                return;
            }

            string catatan = txtCatatan.Text.Trim();

            // Simpan ke list
            _daftarPenitip.Add(new
            {
                Nama = nama,
                Jumlah = jumlah,
                Catatan = string.IsNullOrEmpty(catatan) ? null : catatan
            });

            // Refresh listbox
            RefreshListPenitip();

            // Bersihkan input
            txtNamaPenitip.Clear();
            txtJumlah.Clear();
            txtCatatan.Clear();
            txtNamaPenitip.Focus();
        }

        private void RefreshListPenitip()
        {
            listBoxPenitip.Items.Clear();
            foreach (var item in _daftarPenitip)
            {
                string teks = $"{item.Nama} — {item.Jumlah} pcs";
                if (!string.IsNullOrEmpty(item.Catatan))
                    teks += $" ({item.Catatan})";
                listBoxPenitip.Items.Add(teks);
            }

            lblTotalPenitip.Text = $"{_daftarPenitip.Count} penitip";
        }

        private void btnHapusPenitip_Click(object sender, EventArgs e)
        {
            if (listBoxPenitip.SelectedIndex >= 0)
            {
                _daftarPenitip.RemoveAt(listBoxPenitip.SelectedIndex);
                RefreshListPenitip();
            }
            else
            {
                UXHelper.TampilkanError("Pilih dulu penitip yang mau dihapus.");
            }
        }

        private void btnLanjutkan_Click(object sender, EventArgs e)
        {
            // Validasi minimal order
            int totalJumlah = 0;
            foreach (var item in _daftarPenitip)
                totalJumlah += item.Jumlah;

            if (totalJumlah < _produk.MinOrder)
            {
                UXHelper.TampilkanError($"Total pesanan minimal {_produk.MinOrder} pcs ya bestie! Sekarang baru {totalJumlah} pcs.");
                return;
            }

            if (_daftarPenitip.Count == 0)
            {
                UXHelper.TampilkanError("Minimal tambah satu penitip dulu, dong!");
                return;
            }

            // Hitung total bayar (harga aktual dari function)
            int hargaSatuan = _productService.HitungHargaAktual(_idProduk);
            int totalBayar = totalJumlah * hargaSatuan;

            // Tampilkan di step 2
            lblRingkasanProduk.Text = $"{_produk.NamaProduk}";
            lblRingkasanJumlah.Text = $"{_daftarPenitip.Count} penitip • {totalJumlah} pcs";
            lblRingkasanHargaSatuan.Text = $"Rp {hargaSatuan:N0}";
            lblRingkasanTotal.Text = $"Rp {totalBayar:N0}";

            // Tampilkan info rekening
            lblStep2Rekening.Text = $"Transfer ke rekening penjual:\n{_po.InfoRekening}";

            // Simpan total bayar untuk digunakan nanti
            lblRingkasanTotal.Tag = totalBayar;

            // Tukar panel
            pnlStep1.Visible = false;
            pnlStep2.Visible = true;
        }

        // ── STEP 2: Upload & Konfirmasi ──
        private void btnUploadBukti_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Pilih Bukti Transfer";
                dlg.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _pathBukti = FileHelper.SimpanFile(dlg.FileName, "Bukti");
                        // Tampilkan preview
                        pictureBoxBukti.Image = Image.FromFile(FileHelper.DapatkanFullPath(_pathBukti));
                        lblStatusUpload.Text = "Bukti berhasil diunggah ✨";
                        lblStatusUpload.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        UXHelper.TampilkanError("Gagal menyimpan bukti: " + ex.Message);
                    }
                }
            }
        }

        private void btnKonfirmasi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathBukti))
            {
                UXHelper.TampilkanError("Upload bukti transfer dulu ya, bestie! 📸");
                return;
            }

            // Buat list TransactionDetail
            List<TransactionDetail> details = new List<TransactionDetail>();
            foreach (var item in _daftarPenitip)
            {
                TransactionDetail detail = new TransactionDetail();
                detail.IdProduk = _idProduk;
                detail.NamaPenitip = item.Nama;
                detail.JumlahPesanan = item.Jumlah;
                detail.Catatan = item.Catatan;
                details.Add(detail);
            }

            int totalBayar = (int)lblRingkasanTotal.Tag;

            int idTransaksi = _transactionService.BuatTransaksi(_idUser, totalBayar, details);
            if (idTransaksi > 0)
            {
                // Update bukti bayar
                _transactionService.ValidasiPembayaran(idTransaksi, _pathBukti);

                UXHelper.TampilkanSukses("Pesanan berhasil dibuat! Tunggu konfirmasi penjual ya. 🎉");
                Kembali();
            }
        }

        private void btnKembaliStep1_Click(object sender, EventArgs e)
        {
            pnlStep2.Visible = false;
            pnlStep1.Visible = true;
        }

        private void Kembali()
        {
            if (ParentForm is MainForm main)
            {
                var user = main.AmbilUserAktif();
                if (user != null)
                    main.GantiHalaman(new UserDashboardControl(user));
            }
        }
    }
}
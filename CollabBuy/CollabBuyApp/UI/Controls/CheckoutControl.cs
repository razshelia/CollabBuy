using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class CheckoutControl : UserControl
    {
        private int _idUser;
        private int _idProduk;
        private Product _produk;
        private Preorder _po;
        private List<dynamic> _daftarPenitip;
        private string _pathBukti;

        private readonly ProductService _productService;
        private readonly PreorderService _preorderService;
        private readonly TransactionService _transactionService;

        public CheckoutControl(int idUser, int idProduk)
        {
            InitializeComponent();
            _idUser = idUser;
            _idProduk = idProduk;
            _daftarPenitip = new List<dynamic>();

            // TAHAP 4: INJEKSI MANUAL DI UI
            _productService = new ProductService(new ProductRepository());
            _preorderService = new PreorderService(new PreorderRepository());
            _transactionService = new TransactionService(new TransactionRepository());

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

            if (!_produk.IdPo.HasValue)
            {
                UXHelper.TampilkanError("Produk ini belum dimasukkan ke dalam sesi Pre-Order.");
                Kembali();
                return;
            }

            _po = _preorderService.AmbilPOById(_produk.IdPo.Value);

            if (_po == null)
            {
                UXHelper.TampilkanError("Data Pre-Order tidak ditemukan di sistem.");
                Kembali();
                return;
            }

            // Tampilkan info produk di header
            lblNamaProduk.Text = _produk.NamaProduk.ToUpper();
            lblHargaSatuan.Text = $"Rp {_produk.HargaDasar:N0}";
            lblMinOrder.Text = $"Minimal order: {_produk.MinOrder} pcs";
            lblInfoPO.Text = $"PO: {_po.JudulPo}   •   Jenis: {_po.JenisPo}";
            lblInfoRekening.Text = $"Rekening: {_po.InfoRekening}";
        }

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

            _daftarPenitip.Add(new
            {
                Nama = nama,
                Jumlah = jumlah,
                Catatan = string.IsNullOrEmpty(catatan) ? null : catatan
            });

            RefreshListPenitip();

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
                string teks = $"👤 {item.Nama}  —  📦 {item.Jumlah} pcs";
                if (!string.IsNullOrEmpty(item.Catatan))
                    teks += $"   (📝 {item.Catatan})";
                listBoxPenitip.Items.Add(teks);
            }

            lblTotalPenitip.Text = $"Total: {_daftarPenitip.Count} penitip";
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
                UXHelper.TampilkanError("Pilih dulu penitip yang mau dihapus dari list.");
            }
        }

        private void btnLanjutkan_Click(object sender, EventArgs e)
        {
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

            int hargaSatuan = _productService.HitungHargaAktual(_idProduk);
            int totalBayar = totalJumlah * hargaSatuan;

            lblRingkasanProduk.Text = $"📦 {_produk.NamaProduk.ToUpper()}";
            lblRingkasanJumlah.Text = $"👥 {_daftarPenitip.Count} penitip   •   📝 {totalJumlah} pcs";
            lblRingkasanHargaSatuan.Text = $"Satuan: Rp {hargaSatuan:N0}";
            lblRingkasanTotal.Text = $"Rp {totalBayar:N0}";

            lblStep2Rekening.Text = $"💳 Transfer ke rekening penjual:\n{_po.InfoRekening}";
            lblRingkasanTotal.Tag = totalBayar;

            pnlStep1.Visible = false;
            pnlStep2.Visible = true;
        }

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
                        pictureBoxBukti.Image = Image.FromFile(FileHelper.DapatkanFullPath(_pathBukti));
                        lblStatusUpload.Text = "Bukti berhasil diunggah ✨";
                        lblStatusUpload.ForeColor = Color.FromArgb(0, 150, 0); // Green
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
                    main.GantiHalaman(new UserDashboardControl(user)); // Asumsikan kembali ke User Dashboard
            }
        }
    }
}
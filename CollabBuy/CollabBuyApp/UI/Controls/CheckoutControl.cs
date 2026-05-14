using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class CheckoutControl : UserControl
    {
        private int idUser;
        private int idPo;
        private string pathBukti = null;

        // Konstruktor menerima ID user dan ID produk (Preorder/Product)
        public CheckoutControl(int idUser, int idPo)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.idPo = idPo;
            this.MuatDetailProduk();
        }

        private void MuatDetailProduk()
        {
            // Panggil repository langsung (boleh juga lewat service)
            var productRepo = new ProductRepository();
            var produk = productRepo.AmbilProdukById(this.idPo);
            if (produk != null)
            {
                lblNamaProduk.Text = produk.NamaProduk;
                lblHargaSatuan.Text = $"Rp {produk.HargaSatuan:N0}";
            }
            else
            {
                UXHelper.TampilkanError("Produk tidak ditemukan.");
            }
        }

        private void btnUploadBukti_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Pilih Bukti Transfer";
                openFileDialog.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp|Semua File|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string folderTujuan = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", "Bukti");
                        if (!Directory.Exists(folderTujuan))
                            Directory.CreateDirectory(folderTujuan);

                        string namaFile = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(openFileDialog.FileName)}";
                        string fullPath = Path.Combine(folderTujuan, namaFile);
                        File.Copy(openFileDialog.FileName, fullPath);

                        pathBukti = Path.Combine("Uploads", "Bukti", namaFile);

                        pictureBoxBukti.Image = Image.FromFile(fullPath);
                        lblStatusUpload.Text = "Bukti terupload: " + namaFile;
                        lblStatusUpload.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        UXHelper.TampilkanError("Gagal menyimpan bukti: " + ex.Message);
                    }
                }
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                UXHelper.TampilkanError("Jumlah pesanan harus angka lebih dari 0.");
                return;
            }
            if (string.IsNullOrEmpty(pathBukti))
            {
                UXHelper.TampilkanError("Silakan upload bukti transfer dulu.");
                return;
            }

            TransactionService checkoutService = new TransactionService();
            bool sukses = checkoutService.BuatTransaksi(this.idUser, this.idPo, jumlah, pathBukti);
            if (sukses)
            {
                // Kembali ke katalog atau halaman lain
                if (this.ParentForm is MainForm main)
                {
                    main.GantiHalaman(new CatalogControl());
                }
            }
        }
    }
}
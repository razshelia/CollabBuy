using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerProductControl : UserControl
    {
        private ProductService productService;
        private CategoryService categoryService;
        private int sellerId;

        public SellerProductControl(int p_sellerId)
        {
            this.InitializeComponent();
            this.productService = new ProductService();
            this.categoryService = new CategoryService();
            this.sellerId = p_sellerId;
            this.MuatDaftarKategori();
        }

        private void MuatDaftarKategori()
        {
            var data = this.categoryService.MuatSemuaKategori();
            if (data != null)
            {
                this.cmbKategori.DataSource = data;
                this.cmbKategori.DisplayMember = "NamaKategori";
            }
            else
            {
                // Tidak ada kategori
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtNamaProduk.Text))
            {
                UXHelper.TampilkanError("Namanya jangan kosong dong bestie!");
            }
            else
            {
                if (this.numHarga.Value <= 0)
                {
                    UXHelper.TampilkanError("Harga nggak boleh 0 ya!");
                }
                else
                {
                    if (UXHelper.TampilkanKonfirmasi("Udah bener datanya?"))
                    {
                        Product p = new Product();
                        p.NamaProduk = this.txtNamaProduk.Text;
                        p.StokProduk = (int)this.numStok.Value;

                        bool res = this.productService.TambahProdukBaru(p, this.sellerId);
                        if (res)
                        {
                            this.BersihkanForm();
                        }
                        else
                        {
                            // Gagal simpan
                        }
                    }
                    else
                    {
                        // Batal simpan
                    }
                }
            }
        }

        private void BersihkanForm()
        {
            this.txtNamaProduk.Text = "";
            this.numStok.Value = 0;
            this.numHarga.Value = 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class CatalogControl : UserControl
    {
        private ProductService productService;
        private CategoryService categoryService;

        public CatalogControl()
        {
            this.InitializeComponent();
            this.productService = new ProductService();
            this.categoryService = new CategoryService();
            this.MuatKategori();
        }

        private void MuatKategori()
        {
            var data = this.categoryService.MuatSemuaKategori();
            if (data != null && data.Count > 0)
            {
                this.cmbCariKategori.DataSource = data;
                this.cmbCariKategori.DisplayMember = "NamaKategori";
                this.cmbCariKategori.ValueMember = "IdKategori"; // Asumsi ada IdKategori di model
            }
        }

        private void btnCariNama_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtCariNama.Text))
            {
                UXHelper.TampilkanError("Ketik dulu dong nama barangnya, Bestie!");
                return; // Langsung keluar, pengganti else
            }

            // POLYMORPHISM OVERLOADING 1: Memanggil dengan parameter String
            // List<Product> hasil = this.productService.CariKatalog(this.txtCariNama.Text);

            UXHelper.TampilkanSukses($"Lagi nyari '{this.txtCariNama.Text}' ya? Wait, mimin cariin! 🏃‍♂️");
            // RenderCards(hasil); // Fungsi merender panel card
        }

        private void btnCariKategori_Click(object sender, EventArgs e)
        {
            if (this.cmbCariKategori.SelectedItem != null)
            {
                int idKategori = 1; // Contoh: asumsikan ambil dari ValueMember

                // POLYMORPHISM OVERLOADING 2: Memanggil dengan parameter Integer
                // List<Product> hasil = this.productService.CariKatalog(idKategori);

                UXHelper.TampilkanSukses("Filter kategori udah kepasang! ✨");
                // RenderCards(hasil);
            }
        }
        // Di dalam CatalogControl, misalnya saat tombol "Beli" diklik:
        private void BeliProduk(int idPo)
        {
            if (this.ParentForm is MainForm main)
            {
                Akun user = main.AmbilUserAktif();
                if (user != null)
                {
                    main.GantiHalaman(new CheckoutControl(user.IdUser, idPo));
                }
                else
                {
                    UXHelper.TampilkanError("Silakan login terlebih dahulu.");
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class PreorderControl : UserControl
    {
        private int _idPenjual;
        private ProductService _productService;

        public PreorderControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _productService = new ProductService();

            dtpBatasWaktu.MinDate = DateTime.Now.AddDays(1);
            dtpBatasWaktu.Value = DateTime.Now.AddDays(14);

            LoadProdukCombo();
        }

        private void LoadProdukCombo()
        {
            try
            {
                var listProduk = _productService.AmbilProdukByPenjual(_idPenjual) ?? new List<Product>();

                // Tambahkan opsi default di indeks 0
                listProduk.Insert(0, new Product { IdProduk = 0, NamaProduk = "-- Pilih Produk --" });

                cmbProduk.DataSource = listProduk;
                cmbProduk.DisplayMember = "NamaProduk"; // Teks yang tampil di antarmuka
                cmbProduk.ValueMember = "IdProduk";     // ID yang digunakan di database
                cmbProduk.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat daftar produk: " + ex.Message);
            }
        }

        private void PreorderControl_Resize(object sender, EventArgs e)
        {
            pnlCard.Location = new Point(
                (this.ClientSize.Width - pnlCard.Width) / 2,
                (this.ClientSize.Height - pnlCard.Height) / 2
            );
        }

        private void cmbJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isGotong = cmbJenis.Text == TipePO.GotongRoyong;
            lblTargetKuota.Visible = isGotong;
            txtTargetKuota.Visible = isGotong;
        }

        private void btnBuat_Click(object sender, EventArgs e)
        {
            string judul = txtJudulPO.Text.Trim();
            string jenis = cmbJenis.Text;
            string rekening = txtInfoRekening.Text.Trim();
            DateTime batas = dtpBatasWaktu.Value;
            int targetKuota = 0;

            // Validasi ComboBox Produk dengan TryParse agar aman dari NullReferenceException
            int idProdukTerpilih = 0;
            if (cmbProduk.SelectedValue != null)
            {
                int.TryParse(cmbProduk.SelectedValue.ToString(), out idProdukTerpilih);
            }

            if (jenis == TipePO.GotongRoyong)
            {
                int.TryParse(txtTargetKuota.Text, out targetKuota);
            }

            var poService = new PreorderService();
            // Lempar idProdukTerpilih ke Service
            bool sukses = poService.BuatPO(_idPenjual, idProdukTerpilih, judul, jenis, rekening, batas, targetKuota);

            if (sukses)
            {
                if (ParentForm is MainForm main)
                {
                    main.GantiHalaman(new SellerPOListControl(_idPenjual));
                }
            }
        }
    }
}
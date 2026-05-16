using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class PreorderControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly ProductService _productService;
        private readonly PreorderService _poService;

        public PreorderControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _productService = new ProductService(new ProductRepository());
            _poService = new PreorderService(new PreorderRepository());

            dtpBatasWaktu.MinDate = DateTime.Now.AddDays(1);
            dtpBatasWaktu.Value = DateTime.Now.AddDays(14);

            // Center card saat ukuran berubah atau pertama kali dimuat
            this.Resize += (s, e) => CenterCard();
            this.Load += (s, e) => CenterCard();

            LoadProdukCombo();
        }

        private void CenterCard()
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
            }
        }

        private void LoadProdukCombo()
        {
            try
            {
                var listProduk = _productService.AmbilProdukByPenjual(_idPenjual) ?? new List<Product>();
                var itemsCombo = new List<dynamic>();
                itemsCombo.Add(new { Id = 0, Nama = "Pilih Produk Master" });
                foreach (var p in listProduk)
                {
                    itemsCombo.Add(new { Id = p.IdProduk, Nama = p.NamaProduk });
                }
                cmbProduk.DataSource = itemsCombo;
                cmbProduk.DisplayMember = "Nama";
                cmbProduk.ValueMember = "Id";
                cmbProduk.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat daftar produk: " + ex.Message);
            }
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

            int idProdukTerpilih = 0;
            if (cmbProduk.SelectedValue != null)
            {
                int.TryParse(cmbProduk.SelectedValue.ToString(), out idProdukTerpilih);
            }

            if (idProdukTerpilih <= 0)
            {
                UXHelper.TampilkanError("Pilih produk master yang valid dulu ya, bestie! 📦");
                return;
            }

            if (jenis == TipePO.GotongRoyong)
            {
                int.TryParse(txtTargetKuota.Text, out targetKuota);
            }

            bool sukses = _poService.BuatPO(_idPenjual, idProdukTerpilih, judul, jenis, rekening, batas, targetKuota);

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
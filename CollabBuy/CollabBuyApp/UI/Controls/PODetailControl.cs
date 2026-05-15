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
    public partial class PODetailControl : UserControl
    {
        private int _idPo;
        private User _user;
        private PreorderService _poService;
        private ProductService _productService;
        private Preorder _po;

        public PODetailControl(User user, int idPo)
        {
            InitializeComponent();
            _user = user;
            _idPo = idPo;
            _poService = new PreorderService();
            _productService = new ProductService();
            LoadPODetail();
        }

        private void LoadPODetail()
        {
            _po = _poService.AmbilPOById(_idPo);
            if (_po == null)
            {
                UXHelper.TampilkanError("PO tidak ditemukan.");
                KembaliKeKatalog();
                return;
            }

            // Tampilkan info PO
            lblJudulPO.Text = _po.JudulPo;
            lblJenisPO.Text = $"Jenis: {_po.JenisPo}";
            lblRekening.Text = $"Rekening: {_po.InfoRekening}";
            lblBatasWaktu.Text = $"Batas Waktu: {_po.BatasWaktu:dd MMMM yyyy HH:mm}";

            // Tampilkan produk dalam PO
            List<Product> listProdukDiPO = _productService.AmbilProdukByPo(_idPo);

            // 2. Kirim list tersebut ke method tampilan
            if (listProdukDiPO != null)
            {
                TampilkanProdukCard(listProdukDiPO);
            }
        }

        private void TampilkanProdukCard(List<Product> produkList)
        {
            flowPanelProduk.Controls.Clear();

            if (produkList.Count == 0)
            {
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada produk di PO ini, bestie! 😴";
                lblKosong.Font = new Font("Segoe UI", 12F);
                lblKosong.ForeColor = Color.Gray;
                lblKosong.Size = new Size(400, 40);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                flowPanelProduk.Controls.Add(lblKosong);
                return;
            }

            foreach (var produk in produkList)
            {
                Panel card = BuatCardProduk(produk);
                flowPanelProduk.Controls.Add(card);
            }
        }

        private Panel BuatCardProduk(Product produk)
        {
            Panel card = new Panel();
            card.Size = new Size(260, 300);
            card.BackColor = Color.White;
            card.Margin = new Padding(8);

            PictureBox pic = new PictureBox();
            pic.Size = new Size(236, 120);
            pic.Location = new Point(12, 12);
            pic.BackColor = Color.FromArgb(167, 139, 250);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            if (!string.IsNullOrEmpty(produk.FotoProduk))
            {
                string fullPath = FileHelper.DapatkanFullPath(produk.FotoProduk);
                if (File.Exists(fullPath))
                    pic.Image = Image.FromFile(fullPath);
            }

            Label lblNama = new Label();
            lblNama.Text = produk.NamaProduk;
            lblNama.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNama.ForeColor = Color.FromArgb(45, 27, 79);
            lblNama.Size = new Size(236, 35);
            lblNama.Location = new Point(12, 140);

            Label lblHarga = new Label();
            lblHarga.Text = $"Rp {produk.HargaDasar:N0}";
            if (produk.HargaDiskon.HasValue)
                lblHarga.Text += $" → Rp {produk.HargaDiskon:N0}";
            lblHarga.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHarga.ForeColor = Color.FromArgb(253, 224, 71);
            lblHarga.Size = new Size(236, 25);
            lblHarga.Location = new Point(12, 175);

            Label lblMinOrder = new Label();
            lblMinOrder.Text = $"Min order: {produk.MinOrder} pcs";
            lblMinOrder.Font = new Font("Segoe UI", 8F);
            lblMinOrder.ForeColor = Color.Gray;
            lblMinOrder.Size = new Size(236, 20);
            lblMinOrder.Location = new Point(12, 200);

            Button btnTitip = new Button();
            btnTitip.Text = "Titip Sekarang ✨";
            btnTitip.BackColor = Color.FromArgb(167, 139, 250);
            btnTitip.ForeColor = Color.White;
            btnTitip.FlatStyle = FlatStyle.Flat;
            btnTitip.FlatAppearance.BorderSize = 0;
            btnTitip.Size = new Size(236, 32);
            btnTitip.Location = new Point(12, 230);
            btnTitip.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTitip.Click += (s, e) =>
            {
                if (ParentForm is MainForm main)
                    main.GantiHalaman(new CheckoutControl(_user.IdUser, produk.IdProduk));
            };

            card.Controls.Add(pic);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblHarga);
            card.Controls.Add(lblMinOrder);
            card.Controls.Add(btnTitip);
            return card;
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            KembaliKeKatalog();
        }

        private void KembaliKeKatalog()
        {
            if (ParentForm is MainForm main)
                main.GantiHalaman(new UserDashboardControl(_user));
        }
    }
}
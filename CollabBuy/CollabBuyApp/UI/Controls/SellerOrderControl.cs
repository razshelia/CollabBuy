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
    public partial class SellerOrderControl : UserControl
    {
        private int _idPenjual;
        private TransactionService _transactionService;
        private List<Transaction> _daftarPesanan;

        public SellerOrderControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            _transactionService = new TransactionService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _daftarPesanan = _transactionService.AmbilPesananMasukPenjual(_idPenjual);
                TampilkanPesanan();
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat pesanan: " + ex.Message);
            }
        }

        private void TampilkanPesanan()
        {
            flowPanelPesanan.Controls.Clear();
            if (_daftarPesanan.Count == 0)
            {
                Label lblKosong = new Label();
                lblKosong.Text = "Belum ada pesanan masuk, bestie! 🥺";
                lblKosong.Font = new Font("Segoe UI", 14F);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
                flowPanelPesanan.Controls.Add(lblKosong);
                return;
            }

            foreach (var trans in _daftarPesanan)
            {
                Panel card = BuatCardPesanan(trans);
                flowPanelPesanan.Controls.Add(card);
            }
        }

        private Panel BuatCardPesanan(Transaction trans)
        {
            Panel card = new Panel();
            card.Size = new Size(680, 150);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            // ID dan status
            Label lblID = new Label()
            {
                Text = $"Transaksi #{trans.IdTransaksi}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(167, 139, 250),
                Size = new Size(200, 20),
                Location = new Point(15, 10)
            };

            Label lblStatus = new Label()
            {
                Text = $"Status: {trans.StatusPesanan}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = trans.StatusPesanan == StatusTransaksi.Menunggu ? Color.Orange :
                           trans.StatusPesanan == StatusTransaksi.Diproses ? Color.Blue : Color.Green,
                Size = new Size(150, 20),
                Location = new Point(250, 10)
            };

            // Total
            Label lblTotal = new Label()
            {
                Text = $"💰 Rp {trans.TotalBayarGrup:N0}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(253, 224, 71),
                Size = new Size(200, 25),
                Location = new Point(15, 40)
            };

            // Bukti bayar
            PictureBox pic = new PictureBox()
            {
                Size = new Size(100, 75),
                Location = new Point(15, 70),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            if (!string.IsNullOrEmpty(trans.BuktiBayar))
            {
                string full = FileHelper.DapatkanFullPath(trans.BuktiBayar);
                if (File.Exists(full)) pic.Image = Image.FromFile(full);
            }

            // Detail penitip
            var detailList = _transactionService.AmbilDetailTransaksi(trans.IdTransaksi);
            string detailText = "";
            foreach (var d in detailList)
            {
                detailText += $"{d.NamaPenitip} x{d.JumlahPesanan}";
                if (!string.IsNullOrEmpty(d.Catatan)) detailText += $" ({d.Catatan})";
                detailText += "\n";
            }

            Label lblDetail = new Label()
            {
                Text = detailText,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Size = new Size(300, 70),
                Location = new Point(130, 70)
            };

            // Tombol validasi
            Button btnValidasi = new Button()
            {
                Text = trans.IsValid ? "✅ Tervalidasi" : "🔍 Validasi",
                BackColor = trans.IsValid ? Color.Green : Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Size = new Size(100, 30),
                Location = new Point(550, 30)
            };
            if (!trans.IsValid)
            {
                btnValidasi.Click += (s, e) =>
                {
                    if (_transactionService.ValidasiPembayaran(trans.IdTransaksi, trans.BuktiBayar))
                        LoadData();
                };
            }

            // Tombol ubah status
            ComboBox cmbStatus = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { StatusTransaksi.Menunggu, StatusTransaksi.Diproses, StatusTransaksi.Selesai },
                Location = new Point(550, 70),
                Size = new Size(100, 23)
            };
            cmbStatus.SelectedItem = trans.StatusPesanan;
            cmbStatus.SelectedIndexChanged += (s, e) =>
            {
                if (_transactionService.UbahStatusPesanan(trans.IdTransaksi, cmbStatus.Text))
                    LoadData();
            };

            card.Controls.Add(lblID);
            card.Controls.Add(lblStatus);
            card.Controls.Add(lblTotal);
            card.Controls.Add(pic);
            card.Controls.Add(lblDetail);
            card.Controls.Add(btnValidasi);
            card.Controls.Add(cmbStatus);

            return card;
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
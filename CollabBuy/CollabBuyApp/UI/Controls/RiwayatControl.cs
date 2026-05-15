using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class RiwayatControl : UserControl
    {
        private int _idUser;
        private TransactionService _transactionService;
        private ProductService _productService;

        public RiwayatControl(int idUser)
        {
            InitializeComponent();
            _idUser = idUser;
            _transactionService = new TransactionService();
            _productService = new ProductService();
            LoadRiwayat();
        }

        private void LoadRiwayat()
        {
            try
            {
                var riwayat = _transactionService.AmbilRiwayatKoordinator(_idUser);
                TampilkanRiwayat(riwayat);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat riwayat: " + ex.Message);
            }
        }

        private void TampilkanRiwayat(List<Transaction> daftar)
        {
            flowPanelRiwayat.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label();
                lblKosong.Text = "Kamu belum pernah transaksi, bestie! 🥺\nYuk titip sesuatu di katalog~";
                lblKosong.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
                lblKosong.ForeColor = Color.FromArgb(45, 27, 79);
                lblKosong.TextAlign = ContentAlignment.MiddleCenter;
                lblKosong.Dock = DockStyle.Fill;
                flowPanelRiwayat.Controls.Add(lblKosong);
                return;
            }

            foreach (var trans in daftar)
            {
                Panel card = BuatCardTransaksi(trans);
                flowPanelRiwayat.Controls.Add(card);
            }
        }

        private Panel BuatCardTransaksi(Transaction trans)
        {
            Panel card = new Panel();
            card.Size = new Size(680, 130);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(15);

            // ID Transaksi
            Label lblID = new Label();
            lblID.Text = $"Transaksi #{trans.IdTransaksi}";
            lblID.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblID.ForeColor = Color.FromArgb(167, 139, 250);
            lblID.Size = new Size(200, 20);
            lblID.Location = new Point(15, 10);

            // Tanggal
            Label lblTanggal = new Label();
            lblTanggal.Text = $"📅 {trans.TanggalTransaksi:dd MMM yyyy HH:mm}";
            lblTanggal.Font = new Font("Segoe UI", 9F);
            lblTanggal.ForeColor = Color.Gray;
            lblTanggal.Size = new Size(250, 20);
            lblTanggal.Location = new Point(15, 35);

            // Total bayar
            Label lblTotal = new Label();
            lblTotal.Text = $"💰 Rp {trans.TotalBayarGrup:N0}";
            lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(253, 224, 71);
            lblTotal.Size = new Size(200, 25);
            lblTotal.Location = new Point(15, 60);

            // Status
            Label lblStatus = new Label();
            lblStatus.Text = $"Status: {trans.StatusPesanan}";
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Color statusColor = trans.StatusPesanan switch
            {
                "Menunggu" => Color.Orange,
                "Diproses" => Color.Blue,
                "Selesai" => Color.Green,
                _ => Color.Gray
            };
            lblStatus.ForeColor = statusColor;
            lblStatus.Size = new Size(150, 20);
            lblStatus.Location = new Point(230, 60);

            // Tombol detail
            Button btnDetail = new Button();
            btnDetail.Text = "Detail 👀";
            btnDetail.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnDetail.BackColor = Color.FromArgb(167, 139, 250);
            btnDetail.ForeColor = Color.White;
            btnDetail.FlatStyle = FlatStyle.Flat;
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Size = new Size(80, 28);
            btnDetail.Location = new Point(550, 55);
            btnDetail.Click += (s, e) =>
            {
                var detailList = _transactionService.AmbilDetailTransaksi(trans.IdTransaksi);
                string info = "📋 Detail Penitip:\n\n";
                foreach (var d in detailList)
                {
                    var produk = _productService.AmbilProdukById(d.IdProduk);
                    string namaProduk = produk != null ? produk.NamaProduk : "(tidak diketahui)";
                    info += $"• {d.NamaPenitip} — {namaProduk} x{d.JumlahPesanan}";
                    if (!string.IsNullOrEmpty(d.Catatan))
                        info += $" (catatan: {d.Catatan})";
                    info += "\n";
                }
                MessageBox.Show(info, $"Detail Transaksi #{trans.IdTransaksi}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            card.Controls.Add(lblID);
            card.Controls.Add(lblTanggal);
            card.Controls.Add(lblTotal);
            card.Controls.Add(lblStatus);
            card.Controls.Add(btnDetail);
            return card;
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadRiwayat();
    }
}
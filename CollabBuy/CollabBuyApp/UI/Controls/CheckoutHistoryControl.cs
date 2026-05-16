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
    public partial class CheckoutHistoryControl : UserControl
    {
        private readonly int _idUser;
        private readonly TransactionService _transactionService;
        private readonly ProductService _productService;

        public CheckoutHistoryControl(int idUser)
        {
            InitializeComponent();
            _idUser = idUser;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _transactionService = new TransactionService(new TransactionRepository());
            _productService = new ProductService(new ProductRepository());

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var riwayat = _transactionService.AmbilRiwayatKoordinator(_idUser);
                TampilkanData(riwayat);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal memuat riwayat checkout: " + ex.Message);
            }
        }

        private void TampilkanData(List<Transaction> daftar)
        {
            flowPanelCheckout.Controls.Clear();

            if (daftar.Count == 0)
            {
                Label lblKosong = new Label()
                {
                    Text = "Belum ada riwayat checkout nih, bestie! 🛒\nYuk checkout sesuatu dulu~",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
                flowPanelCheckout.Controls.Add(lblKosong);
                return;
            }

            foreach (var trans in daftar)
            {
                Panel card = BuatCardTransaksi(trans);
                flowPanelCheckout.Controls.Add(card);
            }
        }

        private Panel BuatCardTransaksi(Transaction trans)
        {
            // Desain Card Riwayat bergaya Flat Neo-Retro
            Panel card = new Panel()
            {
                Size = new Size(850, 130), // Lebar untuk full screen
                BackColor = Color.FromArgb(200, 182, 255), // Pastel Purple Muda
                Margin = new Padding(10),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Kotak retro
            };

            Label lblID = new Label()
            {
                Text = $"#TRX-{trans.IdTransaksi}",
                Font = new Font("Segoe UI Black", 10F),
                ForeColor = Color.FromArgb(36, 0, 70),
                Size = new Size(200, 20),
                Location = new Point(15, 15)
            };

            Label lblTanggal = new Label()
            {
                Text = $"📅 {trans.TanggalTransaksi:dd MMM yyyy HH:mm}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                Size = new Size(300, 20),
                Location = new Point(15, 40)
            };

            Label lblTotal = new Label()
            {
                Text = $"💰 Rp {trans.TotalBayarGrup:N0}",
                Font = new Font("Segoe UI Black", 14F),
                ForeColor = Color.FromArgb(36, 0, 70),
                Size = new Size(250, 30),
                Location = new Point(15, 65)
            };

            // Warna Status yang lebih jelas dengan font tebal
            Color statusColor = trans.StatusPesanan switch
            {
                StatusTransaksi.Menunggu => Color.FromArgb(220, 120, 0), // Dark Orange
                StatusTransaksi.Diproses => Color.FromArgb(0, 0, 180),   // Dark Blue
                StatusTransaksi.Selesai => Color.FromArgb(0, 150, 0),    // Dark Green
                _ => Color.Gray
            };

            Label lblStatus = new Label()
            {
                Text = $"STATUS: {trans.StatusPesanan.ToUpper()}",
                Font = new Font("Segoe UI Black", 10F),
                ForeColor = statusColor,
                Size = new Size(200, 20),
                Location = new Point(350, 40)
            };

            Label lblValidasi = new Label()
            {
                Text = trans.IsValid ? "✅ Pembayaran Tervalidasi" : "⏳ Menunggu Validasi",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = trans.IsValid ? Color.FromArgb(0, 150, 0) : Color.FromArgb(220, 120, 0),
                Size = new Size(250, 20),
                Location = new Point(350, 70)
            };

            Button btnDetail = new Button()
            {
                Text = "Detail 👀",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(253, 255, 182), // Kuning Pastel
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 40),
                Location = new Point(680, 45),
                Cursor = Cursors.Hand
            };
            btnDetail.FlatAppearance.BorderSize = 1;
            btnDetail.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);

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
                MessageBox.Show(info, $"Detail Checkout #{trans.IdTransaksi}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            card.Controls.Add(lblID);
            card.Controls.Add(lblTanggal);
            card.Controls.Add(lblTotal);
            card.Controls.Add(lblStatus);
            card.Controls.Add(lblValidasi);
            card.Controls.Add(btnDetail);
            return card;
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
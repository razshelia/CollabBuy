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
    public partial class SellerOrderControl : UserControl
    {
        private readonly int _idPenjual;
        private readonly TransactionService _transactionService;
        private List<Transaction> _daftarPesanan;

        public SellerOrderControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;

            // TAHAP 4: INJEKSI MANUAL DI UI
            _transactionService = new TransactionService(new TransactionRepository());

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
                Label lblKosong = new Label()
                {
                    Text = "Belum ada pesanan masuk, bestie! 🥺",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
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
            // Desain Card Pesanan bergaya Flat Neo-Retro
            Panel card = new Panel()
            {
                Size = new Size(850, 160), // Diperlebar
                BackColor = Color.FromArgb(253, 255, 182), // Kuning pastel
                Margin = new Padding(10),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle // Garis pinggir tegas
            };

            // ID
            Label lblID = new Label()
            {
                Text = $"#TRX-{trans.IdTransaksi}",
                Font = new Font("Segoe UI Black", 12F),
                ForeColor = Color.FromArgb(36, 0, 70),
                Size = new Size(200, 25),
                Location = new Point(15, 15)
            };

            // Warna status
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
                Size = new Size(250, 20),
                Location = new Point(220, 18)
            };

            // Total
            Label lblTotal = new Label()
            {
                Text = $"💰 Rp {trans.TotalBayarGrup:N0}",
                Font = new Font("Segoe UI Black", 14F),
                ForeColor = Color.FromArgb(255, 138, 138), // Soft red
                Size = new Size(250, 30),
                Location = new Point(15, 45)
            };

            // Bukti bayar
            PictureBox pic = new PictureBox()
            {
                Size = new Size(110, 70),
                Location = new Point(15, 80),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };
            if (!string.IsNullOrEmpty(trans.BuktiBayar))
            {
                string full = FileHelper.DapatkanFullPath(trans.BuktiBayar);
                if (File.Exists(full)) pic.Image = Image.FromFile(full);
            }
            // Tambahkan fitur klik gambar untuk memperbesar (opsional, sebagai UX tambahan)
            pic.Click += (s, e) => {
                if (pic.Image != null) MessageBox.Show("Bukti pembayaran telah dilampirkan.", "Bukti Bayar");
            };

            // Detail penitip
            var detailList = _transactionService.AmbilDetailTransaksi(trans.IdTransaksi);
            string detailText = "Daftar Penitip:\n";
            foreach (var d in detailList)
            {
                detailText += $"• {d.NamaPenitip} x{d.JumlahPesanan}";
                if (!string.IsNullOrEmpty(d.Catatan)) detailText += $" ({d.Catatan})";
                detailText += "\n";
            }

            Label lblDetail = new Label()
            {
                Text = detailText,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                Size = new Size(400, 70),
                Location = new Point(140, 80)
            };

            // Tombol validasi
            Button btnValidasi = new Button()
            {
                Text = trans.IsValid ? "✅ TERVALIDASI" : "🔍 VALIDASI",
                BackColor = trans.IsValid ? Color.FromArgb(182, 255, 200) : Color.FromArgb(255, 218, 185), // Soft Green / Soft Orange
                ForeColor = Color.FromArgb(36, 0, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(150, 40),
                Location = new Point(680, 15),
                Cursor = Cursors.Hand
            };
            btnValidasi.FlatAppearance.BorderSize = 1;
            btnValidasi.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);

            if (!trans.IsValid)
            {
                btnValidasi.Click += (s, e) =>
                {
                    if (_transactionService.ValidasiPembayaran(trans.IdTransaksi, trans.BuktiBayar))
                        LoadData();
                };
            }

            // Label & ComboBox ubah status
            Label lblUbahStatus = new Label()
            {
                Text = "Update Status:",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Location = new Point(680, 75),
                Size = new Size(150, 15)
            };

            ComboBox cmbStatus = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(680, 95),
                Size = new Size(150, 28)
            };
            cmbStatus.Items.AddRange(new object[] { StatusTransaksi.Menunggu, StatusTransaksi.Diproses, StatusTransaksi.Selesai });
            cmbStatus.SelectedItem = trans.StatusPesanan;

            cmbStatus.SelectedIndexChanged += (s, e) =>
            {
                // Mencegah trigger saat load data
                if (cmbStatus.Text != trans.StatusPesanan)
                {
                    if (_transactionService.UbahStatusPesanan(trans.IdTransaksi, cmbStatus.Text))
                        LoadData();
                }
            };

            card.Controls.Add(lblID);
            card.Controls.Add(lblStatus);
            card.Controls.Add(lblTotal);
            card.Controls.Add(pic);
            card.Controls.Add(lblDetail);
            card.Controls.Add(btnValidasi);
            card.Controls.Add(lblUbahStatus);
            card.Controls.Add(cmbStatus);

            return card;
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
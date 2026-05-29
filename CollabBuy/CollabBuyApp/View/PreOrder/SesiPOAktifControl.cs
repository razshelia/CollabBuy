using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class SesiPOAktifControl : UserControl
    {
        private readonly PreOrderController _preOrderController;
        private User _currentUser;

        public SesiPOAktifControl(User currentUser)
        {
            InitializeComponent();
            _preOrderController = new PreOrderController();
            _currentUser = currentUser;
        }

        private void SesiPOAktifControl_Load(object sender, EventArgs e)
        {
            LoadDataSesiPO("");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Text = "Cari sesi PO...";
            txtCari.ForeColor = Color.Gray;
            LoadDataSesiPO("");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            string keyword = txtCari.Text;
            if (keyword == "Cari sesi PO...") keyword = "";
            LoadDataSesiPO(keyword);
        }

        private void LoadDataSesiPO(string keyword)
        {
            flpSesiPO.Controls.Clear();

            try
            {
                // TODO: Panggil method di PreOrderController untuk mengambil sesi aktif
                // List<Models.Preorder> listPO = _preOrderController.GetActivePreOrders(keyword);

                // --- MOCK DATA ---
                List<dynamic> listPO = new List<dynamic>
                {
                    new { Id = 1, Nama = "Danus Makaroni HMTI", Toko = "HMTI Mandiri", Kuota = 50, Terisi = 45, Harga = 5000, Deadline = DateTime.Now.AddDays(1) },
                    new { Id = 2, Nama = "PO Kemeja Angkatan 24", Toko = "BEM Fasilkom", Kuota = 100, Terisi = 20, Harga = 120000, Deadline = DateTime.Now.AddDays(7) },
                    new { Id = 3, Nama = "Risoles Lumer Pagi", Toko = "Siti Jajanan", Kuota = 30, Terisi = 30, Harga = 3000, Deadline = DateTime.Now.AddHours(2) } // Contoh Penuh
                };
                // -----------------

                foreach (var po in listPO)
                {
                    if (!string.IsNullOrWhiteSpace(keyword) &&
                        !po.Nama.ToString().ToLower().Contains(keyword.ToLower()))
                    {
                        continue;
                    }

                    Panel pnlCard = BuatKartuPO(po.Id, po.Nama, po.Toko, po.Kuota, po.Terisi, po.Harga, po.Deadline);
                    flpSesiPO.Controls.Add(pnlCard);
                }

                if (flpSesiPO.Controls.Count == 0)
                {
                    Label lblKosong = new Label
                    {
                        Text = "Tidak ada sesi PO yang aktif saat ini.",
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Margin = new Padding(10)
                    };
                    flpSesiPO.Controls.Add(lblKosong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat sesi PO: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- UI BUILDER KARTU PO ---
        private Panel BuatKartuPO(int idPO, string namaSesi, string namaToko, int kuota, int terisi, decimal harga, DateTime deadline)
        {
            bool isPenuh = (terisi >= kuota);
            bool isExpired = (DateTime.Now > deadline);
            bool isTutup = isPenuh || isExpired;

            Panel card = new Panel
            {
                Width = 260,
                Height = 180,
                BackColor = Color.White,
                Margin = new Padding(10, 10, 15, 15),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Status Badge (Pojok Kanan Atas)
            Label lblBadge = new Label
            {
                Text = isTutup ? "TUTUP" : "AKTIF",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = isTutup ? Color.LightCoral : Color.LightGreen,
                ForeColor = isTutup ? Color.White : Color.DarkGreen,
                AutoSize = true,
                Top = 10,
                Left = 205,
                Padding = new Padding(3)
            };

            // Nama Sesi
            Label lblNama = new Label
            {
                Text = namaSesi,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Top = 10,
                Left = 10,
                Width = 190,
                AutoSize = false,
                Height = 45
            };

            // Nama Penyelenggara/Toko
            Label lblToko = new Label
            {
                Text = $"🏪 {namaToko}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.Gray,
                Top = 55,
                Left = 10,
                AutoSize = true
            };

            // Harga Estimasi
            Label lblHarga = new Label
            {
                Text = $"Rp {harga:N0}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Top = 80,
                Left = 10,
                AutoSize = true
            };

            // Info Kuota & Deadline
            Label lblInfo = new Label
            {
                Text = $"Kuota: {terisi}/{kuota}  •  Berakhir: {deadline.ToString("dd MMM")}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = isTutup ? Color.Red : Color.FromArgb(36, 0, 70),
                Top = 110,
                Left = 10,
                AutoSize = true
            };

            // Tombol Ikut Sesi
            Button btnIkut = new Button
            {
                Text = isTutup ? "Sesi Berakhir" : "➕ Ikut Sesi PO",
                Width = 240,
                Height = 30,
                Top = 140,
                Left = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = isTutup ? Color.LightGray : Color.FromArgb(36, 0, 70),
                ForeColor = isTutup ? Color.Gray : Color.FromArgb(253, 255, 182),
                Cursor = isTutup ? Cursors.No : Cursors.Hand,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Tag = idPO,
                Enabled = !isTutup // Disable tombol jika penuh/waktu habis
            };
            btnIkut.FlatAppearance.BorderSize = 0;

            if (!isTutup)
            {
                btnIkut.Click += BtnIkut_Click;
            }

            card.Controls.Add(lblBadge);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblToko);
            card.Controls.Add(lblHarga);
            card.Controls.Add(lblInfo);
            card.Controls.Add(btnIkut);

            return card;
        }

        // --- EVENT HANDLER ---
        private void BtnIkut_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPO = Convert.ToInt32(btn.Tag);

            // TODO: Integrasikan ke CartManager / Sesi Checkout Anda
            // Logika: Memasukkan pesanan ke dalam TransactionDetail dengan referensi ke Id PreOrder

            MessageBox.Show($"Berhasil mendaftar ke Sesi PO ini!\nSilakan cek keranjang/sesi checkout Anda untuk menyelesaikan pembayaran.",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // UX Helper Search Bar
        private void txtCari_Enter(object sender, EventArgs e)
        {
            if (txtCari.Text == "Cari sesi PO...")
            {
                txtCari.Text = "";
                txtCari.ForeColor = Color.Black;
            }
        }

        private void txtCari_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCari.Text))
            {
                txtCari.Text = "Cari sesi PO...";
                txtCari.ForeColor = Color.Gray;
            }
        }
    }
}

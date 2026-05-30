using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class SesiPOAktifControl : UserControl
    {
        private readonly PreOrderController _poController;
        private User _currentUser;

        public SesiPOAktifControl(User currentUser)
        {
            InitializeComponent();
            _poController = new PreOrderController();
            _currentUser = currentUser;

            this.Resize += (s, e) => AdjustLayout();
        }

        private void SesiPOAktifControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            LoadDataSesiPO("");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Text = "Kepoin sesi PO...";
            txtCari.ForeColor = Color.Gray;
            LoadDataSesiPO("");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            string keyword = txtCari.Text;
            if (keyword == "Kepoin sesi PO...") keyword = "";
            LoadDataSesiPO(keyword);
        }

        private void LoadDataSesiPO(string keyword)
        {
            flpSesiPO.Controls.Clear();
            try
            {
                DataTable dtPO = _poController.GetActiveSesiPO(keyword);

                foreach (DataRow row in dtPO.Rows)
                {
                    Panel pnlCard = BuatKartuPO(
                        Convert.ToInt32(row["id_po"]),
                        row["nama_sesi"].ToString(),
                        row["nama_toko"].ToString(),
                        Convert.ToInt32(row["kuota"]),
                        Convert.ToInt32(row["terisi"]),
                        Convert.ToDecimal(row["harga"]),
                        Convert.ToDateTime(row["deadline"])
                    );
                    flpSesiPO.Controls.Add(pnlCard);
                }

                if (dtPO.Rows.Count == 0)
                {
                    Label lblKosong = new Label
                    {
                        Text = "Yah, lagi sepi nih... Gak ada PO yang open. 🥲",
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
                MessageBox.Show("Gagal muat data ngab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel BuatKartuPO(int idPO, string namaSesi, string namaToko, int kuota, int terisi, decimal harga, DateTime deadline)
        {
            bool isPenuh = (terisi >= kuota);
            bool isExpired = (DateTime.Now > deadline);
            bool isTutup = isPenuh || isExpired;

            // NEO-RETRO COLORS
            Color bgCard = Color.FromArgb(235, 204, 255); // Sangat Soft Purple
            Color accentPurple = Color.FromArgb(36, 0, 70);
            Color highlightYellow = Color.FromArgb(253, 255, 182);

            Panel card = new Panel
            {
                Width = 270,
                Height = 190,
                BackColor = bgCard,
                Margin = new Padding(10, 10, 15, 15),
                BorderStyle = BorderStyle.None
            };

            // Tambahkan border melengkung imajiner dengan warna tegas
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, accentPurple, ButtonBorderStyle.Solid);
            };

            Label lblBadge = new Label
            {
                Text = isTutup ? "YAH, LATE" : "GASKEUN",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = isTutup ? Color.FromArgb(255, 173, 173) : Color.FromArgb(155, 246, 255),
                ForeColor = accentPurple,
                AutoSize = true,
                Top = 10,
                Left = 200,
                Padding = new Padding(3)
            };

            Label lblNama = new Label
            {
                Text = namaSesi.ToUpper(),
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = accentPurple,
                Top = 10,
                Left = 10,
                Width = 180,
                AutoSize = false,
                Height = 45
            };

            Label lblToko = new Label
            {
                Text = $"🏪 {namaToko}",
                Font = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = Color.FromArgb(90, 24, 154),
                Top = 55,
                Left = 10,
                AutoSize = true
            };

            Label lblHarga = new Label
            {
                Text = $"Rp {harga:N0}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = accentPurple,
                Top = 80,
                Left = 10,
                AutoSize = true
            };

            Label lblInfo = new Label
            {
                Text = $"Slot: {terisi}/{kuota}  •  Tutup: {deadline.ToString("dd MMM HH:mm")}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = isTutup ? Color.Red : accentPurple,
                Top = 115,
                Left = 10,
                AutoSize = true
            };

            Button btnIkut = new Button
            {
                Text = isTutup ? "Udah Habis Bestie 😭" : "🛒 Checkout Yuk!",
                Width = 250,
                Height = 35,
                Top = 145,
                Left = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = isTutup ? Color.Gray : accentPurple,
                ForeColor = isTutup ? Color.White : highlightYellow,
                Cursor = isTutup ? Cursors.No : Cursors.Hand,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Tag = idPO,
                Enabled = !isTutup
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

        private void BtnIkut_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPO = Convert.ToInt32(btn.Tag);
            MessageBox.Show($"Sip! Produk udah masuk wishlist keranjang kamu. Jangan lupa dibayar ya bestie!",
                            "Masuk Keranjang", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtCari_Enter(object sender, EventArgs e)
        {
            if (txtCari.Text == "Kepoin sesi PO...")
            {
                txtCari.Text = "";
                txtCari.ForeColor = Color.FromArgb(36, 0, 70);
            }
        }

        private void txtCari_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCari.Text))
            {
                txtCari.Text = "Kepoin sesi PO...";
                txtCari.ForeColor = Color.Gray;
            }
        }

        private void AdjustLayout()
        {
            int margin = 36;
            flpSesiPO.Width = this.Width - (margin * 2);
            flpSesiPO.Height = this.Height - flpSesiPO.Top - margin;
        }
    }
}
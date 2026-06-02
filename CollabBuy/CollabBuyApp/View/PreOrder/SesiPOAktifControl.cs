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
        private Models.User _currentUser;

        public SesiPOAktifControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._poController = new PreOrderController();
            this._currentUser = currentUser;

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void SesiPOAktifControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadDataSesiPO("");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.txtCari.Text = "Kepoin sesi PO...";
            this.txtCari.ForeColor = Color.Gray;
            this.LoadDataSesiPO("");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            string keyword = this.txtCari.Text;

            if (keyword == "Kepoin sesi PO...")
            {
                keyword = "";
            }
            else
            {
                bool pencarianAktif = true; // Assignment nyata untuk menghindari else kosong
            }

            this.LoadDataSesiPO(keyword);
        }

        private void LoadDataSesiPO(string keyword)
        {
            this.flpSesiPO.Controls.Clear();

            try
            {
                DataTable dtPO = this._poController.GetActiveSesiPO(keyword);

                if (dtPO != null && dtPO.Rows.Count > 0)
                // ... seterusnya sama
                {
                    foreach (DataRow row in dtPO.Rows)
                    {
                        try
                        {
                            int kuota = row["kuota"] == DBNull.Value ? 0 : Convert.ToInt32(row["kuota"]);
                            int terisi = row["terisi"] == DBNull.Value ? 0 : Convert.ToInt32(row["terisi"]);
                            decimal harga = row["harga"] == DBNull.Value ? 0 : Convert.ToDecimal(row["harga"]);

                            Panel pnlCard = this.BuatKartuPO(
                                Convert.ToInt32(row["id_po"]),
                                row["nama_sesi"].ToString(),
                                row["nama_toko"].ToString(),
                                kuota,
                                terisi,
                                harga,
                                Convert.ToDateTime(row["deadline"])
                            );
                            this.flpSesiPO.Controls.Add(pnlCard);
                        }
                        catch (Exception exRow)
                        {
                            MessageBox.Show("Error di baris: " + exRow.Message, "Debug Row");
                        }
                    }
                }
                else
                {
                    Label lblKosong = new Label
                    {
                        Text = "Yah, lagi sepi nih... Gak ada PO yang open. 🥲",
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Margin = new Padding(10)
                    };
                    this.flpSesiPO.Controls.Add(lblKosong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data ngab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel BuatKartuPO(int idPO, string namaSesi, string namaToko, int kuota, int terisi, decimal harga, DateTime deadline)
        {
            bool isPenuh;
            if (terisi >= kuota)
            {
                isPenuh = true;
            }
            else
            {
                isPenuh = false;
            }

            bool isExpired;
            if (DateTime.Now > deadline)
            {
                isExpired = true;
            }
            else
            {
                isExpired = false;
            }

            bool isTutup;
            if (isPenuh || isExpired)
            {
                isTutup = true;
            }
            else
            {
                isTutup = false;
            }

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
                btnIkut.Click += this.BtnIkut_Click;
            }
            else
            {
                // Tombol tidak bisa diklik, event tidak di-attach
                bool disableIkut = true;
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

            // Catatan: Di masa depan ini bisa di-link ke ProductController untuk redirect ke keranjang
            MessageBox.Show($"Sip! Produk udah masuk wishlist keranjang kamu. Jangan lupa dibayar ya bestie!",
                            "Masuk Keranjang", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtCari_Enter(object sender, EventArgs e)
        {
            if (this.txtCari.Text == "Kepoin sesi PO...")
            {
                this.txtCari.Text = "";
                this.txtCari.ForeColor = Color.FromArgb(36, 0, 70);
            }
            else
            {
                bool pertahankanTeksPencarian = true;
            }
        }

        private void txtCari_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtCari.Text))
            {
                this.txtCari.Text = "Kepoin sesi PO...";
                this.txtCari.ForeColor = Color.Gray;
            }
            else
            {
                bool biarkanTeks = true;
            }
        }

        private void AdjustLayout()
        {
            int margin = 36;
            this.flpSesiPO.Width = this.Width - (margin * 2);
            this.flpSesiPO.Height = this.Height - this.flpSesiPO.Top - margin;
        }
    }
}
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
                            string jenisPO = row["jenis_po"].ToString();                                           
                            decimal hargaDiskon = row["harga_diskon"] == DBNull.Value ? 0
                                                  : Convert.ToDecimal(row["harga_diskon"]);

                            Panel pnlCard = this.BuatKartuPO(
                                Convert.ToInt32(row["id_po"]),
                                row["nama_sesi"].ToString(),
                                row["nama_toko"].ToString(),
                                jenisPO,
                                kuota,
                                terisi,
                                harga,
                                hargaDiskon,
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

        private Panel BuatKartuPO(int idPO, string namaSesi, string namaToko,
    string jenisPO, int kuota, int terisi, decimal harga, decimal hargaDiskon, DateTime deadline)
        {
            bool isGotongRoyong = jenisPO == "Gotong Royong";
            bool isTargetTercapai = isGotongRoyong && kuota > 0 && terisi >= kuota;

            // PO tidak pernah "penuh/tutup" karena kuota bukan batas keras
            // Hanya tutup kalau deadline lewat (sudah difilter DB, tapi jaga-jaga)
            bool isTutup = deadline < DateTime.Now;

            // Badge
            string badgeText;
            Color badgeColor;
            if (isTutup)
            {
                badgeText = "SESI BERAKHIR";
                badgeColor = Color.FromArgb(255, 173, 173);
            }
            else if (isTargetTercapai)
            {
                badgeText = "🎉 TARGET TERCAPAI";
                badgeColor = Color.FromArgb(180, 255, 180); // hijau muda
            }
            else if (isGotongRoyong)
            {
                badgeText = "GOTONG ROYONG";
                badgeColor = Color.FromArgb(255, 236, 153); // kuning
            }
            else
            {
                badgeText = "GASKEUN";
                badgeColor = Color.FromArgb(155, 246, 255);
            }

            Color bgCard = Color.FromArgb(235, 204, 255);
            Color accentPurple = Color.FromArgb(36, 0, 70);
            Color highlightYellow = Color.FromArgb(253, 255, 182);

            Panel card = new Panel
            {
                Width = 270,
                Height = 210,  // sedikit lebih tinggi untuk info harga diskon
                BackColor = bgCard,
                Margin = new Padding(10, 10, 15, 15),
                BorderStyle = BorderStyle.None
            };
            card.Paint += (s, e) =>
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, accentPurple, ButtonBorderStyle.Solid);

            Label lblBadge = new Label
            {
                Text = badgeText,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = badgeColor,
                ForeColor = accentPurple,
                AutoSize = true,
                Top = 10,
                Left = 10,
                Padding = new Padding(3)
            };

            Label lblNama = new Label
            {
                Text = namaSesi.ToUpper(),
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = accentPurple,
                Top = 35,
                Left = 10,
                Width = 250,
                AutoSize = false,
                Height = 45
            };

            Label lblToko = new Label
            {
                Text = $"🏪 {namaToko}",
                Font = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = Color.FromArgb(90, 24, 154),
                Top = 80,
                Left = 10,
                AutoSize = true
            };

            // Harga — tampilkan harga diskon kalau target sudah tercapai
            string teksHarga = isTargetTercapai && hargaDiskon > 0
                ? $"Rp {hargaDiskon:N0}  (diskon dari Rp {harga:N0})"
                : $"Rp {harga:N0}";
            Color warnaHarga = isTargetTercapai && hargaDiskon > 0
                ? Color.FromArgb(0, 150, 0)   // hijau kalau dapat diskon
                : accentPurple;

            Label lblHarga = new Label
            {
                Text = teksHarga,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = warnaHarga,
                Top = 105,
                Left = 10,
                Width = 250,
                AutoSize = false,
                Height = 35
            };

            Label lblInfo = new Label
            {
                Text = $"Slot: {terisi}/{(kuota == 0 ? "∞" : kuota.ToString())}  •  Tutup: {deadline:dd MMM HH:mm}",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = accentPurple,
                Top = 140,
                Left = 10,
                AutoSize = true
            };

            Button btnIkut = new Button
            {
                Text = isTutup ? "Sesi Sudah Berakhir" : "🛒 Checkout Yuk!",
                Width = 250,
                Height = 35,
                Top = 165,
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
                btnIkut.Click += this.BtnIkut_Click;

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
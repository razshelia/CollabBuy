using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DashboardUserControl : UserControl
    {
        private readonly User _currentUser;

        private readonly TransactionController _transactionController;
        private readonly ProductController _productController;

        // ── Warna Palet Neo-Retro Ungu Soft + Kuning Soft ──
        private static readonly Color ClrUnguSoft = Color.FromArgb(210, 195, 255);  // ungu muda
        private static readonly Color ClrUnguMedium = Color.FromArgb(167, 139, 250);  // ungu sedang
        private static readonly Color ClrUnguDark = Color.FromArgb(88, 56, 163);   // ungu tua
        private static readonly Color ClrKuningLemon = Color.FromArgb(255, 240, 120);  // kuning lemon
        private static readonly Color ClrKuningHangat = Color.FromArgb(255, 214, 51);  // kuning emas soft
        private static readonly Color ClrCardBg = Color.FromArgb(252, 249, 255);  // putih ungu sangat muda
        private static readonly Color ClrCardBorder = Color.FromArgb(220, 210, 255);  // border ungu lembut
        private static readonly Color ClrTextDark = Color.FromArgb(36, 0, 70);   // teks gelap

        private Panel pnlKatalogContainer;

        public DashboardUserControl(User user)
        {
            InitializeComponent();
            _currentUser = user;

            _transactionController = new TransactionController();
            _productController = new ProductController();

            if (_currentUser != null)
                lblWelcome.Text = $"Halo, {_currentUser.GetNama()}! 👋";

            pnlStatsCard1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlStatsCard2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            BuildKatalogContainer();

            this.Resize += (s, e) => AdjustLayout();
        }

        // ─────────────────────────────────────────────
        // BUILD: kontainer scrollable untuk card-card
        // ─────────────────────────────────────────────
        private void BuildKatalogContainer()
        {
            pnlKatalogContainer = new Panel
            {
                AutoScroll = true,
                BackColor = Color.Transparent,
                Location = new Point(37, 335),
                Size = new Size(940, 360),
                Anchor = AnchorStyles.Top | AnchorStyles.Left |
                                  AnchorStyles.Right | AnchorStyles.Bottom,
                Name = "pnlKatalogContainer"
            };
            this.Controls.Add(pnlKatalogContainer);
            pnlKatalogContainer.BringToFront();
        }

        // ─────────────────────────────────────────────
        // LOAD
        // ─────────────────────────────────────────────
        private void DashboardUserControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            LoadUserDataSummary();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserDataSummary();
        }

        // ─────────────────────────────────────────────
        // LAYOUT
        // ─────────────────────────────────────────────
        private void AdjustLayout()
        {
            int margin = 37;
            int availableW = this.Width - (margin * 2);
            int cardHalfW = (availableW / 2) - 10;

            pnlStatsCard1.Width = cardHalfW;
            pnlStatsCard2.Width = cardHalfW;
            pnlStatsCard2.Left = margin + cardHalfW + 20;

            pnlKatalogContainer.Width = availableW;
            btnRefresh.Left = this.Width - margin - btnRefresh.Width;
        }

        // ─────────────────────────────────────────────
        // LOAD DATA → bangun card-card
        // ─────────────────────────────────────────────
        private void LoadUserDataSummary()
        {
            try
            {
                if (_currentUser == null) return;

                // Stat card 1 – status lapak
                if (_currentUser.GetPeran() == "Penjual")
                {
                    lblValueShopStatus.Text = "🏪 Lapak Aktif!";
                    lblValueShopStatus.ForeColor = Color.ForestGreen;
                }
                else
                {
                    lblValueShopStatus.Text = "🔒 Terkunci (Buyer)";
                    lblValueShopStatus.ForeColor = ClrTextDark;
                }

                // Stat card 2 – pesanan aktif
                int totalAktif = _transactionController.GetTotalPesananAktif(_currentUser.GetIdUser());
                lblValueActiveOrders.Text = totalAktif.ToString();

                // Ambil katalog
                DataTable dtRaw = _productController.GetKatalogAktifDashboard(15);

                // Bersihkan card lama
                pnlKatalogContainer.Controls.Clear();

                int yOffset = 0;
                int idx = 0;

                foreach (DataRow row in dtRaw.Rows)
                {
                    Panel card = BuildProductCard(row, idx);
                    card.Top = yOffset;
                    card.Left = 0;
                    card.Width = pnlKatalogContainer.ClientSize.Width - 4;
                    pnlKatalogContainer.Controls.Add(card);
                    yOffset += card.Height + 12;
                    idx++;
                }

                if (dtRaw.Rows.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "😴 Belum ada produk PO aktif nih, nantikan rilisan berikutnya~",
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = ClrUnguDark,
                        AutoSize = false,
                        Width = pnlKatalogContainer.Width,
                        Height = 50,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = 20,
                        Left = 0
                    };
                    pnlKatalogContainer.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duh, gagal narik data dari server nih bestie: " + ex.Message,
                                "Waduh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // BUILDER: satu card horizontal
        // ─────────────────────────────────────────────
        private Panel BuildProductCard(DataRow row, int index)
        {
            // ── Ambil data ──
            string namaProduk = row["nama_produk"]?.ToString() ?? "Produk";
            string judulPo = row.IsNull("judul_po") ? "Reguler / Non-PO" : row["judul_po"].ToString();
            int hargaDasar = row.IsNull("harga_dasar") ? 0 : Convert.ToInt32(row["harga_dasar"]);
            int hargaDiskon = row.IsNull("harga_diskon") ? 0 : Convert.ToInt32(row["harga_diskon"]);

            DateTime? batasWaktu = null;
            if (!row.IsNull("batas_waktu"))
                batasWaktu = Convert.ToDateTime(row["batas_waktu"]);

            byte[] fotoBytes = row.IsNull("foto_produk") ? null : row["foto_produk"] as byte[];

            // Countdown & badge
            string badgeText = "✅ Aktif";
            Color badgeColor = ClrUnguDark;
            Color badgeFg = Color.White;
            string countdown = "—";

            if (batasWaktu.HasValue)
            {
                TimeSpan sisa = batasWaktu.Value - DateTime.Now;
                if (sisa.TotalSeconds <= 0)
                {
                    badgeText = "🔒 Tutup";
                    badgeColor = Color.FromArgb(200, 80, 80);
                    countdown = "Sudah berakhir";
                }
                else if (sisa.TotalHours <= 2)
                {
                    badgeText = $"⏰ Tutup {(int)sisa.TotalHours}j {sisa.Minutes}m lagi";
                    badgeColor = ClrKuningHangat;
                    badgeFg = ClrTextDark;
                    countdown = $"{(int)sisa.TotalHours:D2}:{sisa.Minutes:D2}:{sisa.Seconds:D2}";
                }
                else if (sisa.TotalDays <= 1)
                {
                    badgeText = $"⏳ {(int)sisa.TotalHours}j lagi";
                    badgeColor = ClrKuningLemon;
                    badgeFg = ClrTextDark;
                    countdown = $"{(int)sisa.TotalHours}j {sisa.Minutes}m";
                }
                else
                {
                    badgeText = $"📅 {batasWaktu.Value:dd MMM}";
                    badgeColor = ClrUnguMedium;
                    countdown = batasWaktu.Value.ToString("dd MMM yyyy, HH:mm");
                }
            }

            // Progress bar simulasi (kuota tidak tersedia di view, pakai placeholder)
            int progressPct = 0; // default 0 jika tidak ada data kuota

            // ── Warna alternasi card ──
            Color cardBg = (index % 2 == 0) ? ClrCardBg : Color.FromArgb(248, 244, 255);

            // ── Panel utama card ──
            Panel card = new Panel
            {
                Height = 100,
                BackColor = cardBg,
                Cursor = Cursors.Hand,
                Padding = new Padding(0),
                Tag = row.IsNull("id_produk") ? 0 : Convert.ToInt32(row["id_produk"])
            };

            // Border halus via Paint
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (var pen = new Pen(ClrCardBorder, 1.5f))
                    g.DrawRectangle(pen, rect);

                // Garis aksen kiri ungu
                using (var brush = new SolidBrush(ClrUnguMedium))
                    g.FillRectangle(brush, 0, 0, 5, card.Height);
            };

            // ══════════════════════════════
            // AREA KIRI – Foto + Identitas
            // ══════════════════════════════
            Panel pnlLeft = new Panel
            {
                Width = 260,
                Height = card.Height,
                Left = 8,
                Top = 0,
                BackColor = Color.Transparent
            };
            card.Controls.Add(pnlLeft);

            // Foto produk
            PictureBox pbFoto = new PictureBox
            {
                Width = 72,
                Height = 72,
                Left = 10,
                Top = 14,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = ClrUnguSoft
            };
            if (fotoBytes != null && fotoBytes.Length > 0)
            {
                try
                {
                    using (var ms = new System.IO.MemoryStream(fotoBytes))
                        pbFoto.Image = Image.FromStream(ms);
                }
                catch { /* pakai background warna saja */ }
            }

            // Border bulat pada foto via Paint
            pbFoto.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(ClrUnguMedium, 2))
                {
                    var rect = new Rectangle(1, 1, pbFoto.Width - 3, pbFoto.Height - 3);
                    e.Graphics.DrawEllipse(pen, rect);
                }
            };
            pnlLeft.Controls.Add(pbFoto);

            // Label nama produk
            Label lblNamaProduk = new Label
            {
                Text = namaProduk,
                Font = new Font("Segoe UI Black", 10.5F, FontStyle.Bold),
                ForeColor = ClrTextDark,
                AutoSize = false,
                Width = 160,
                Height = 22,
                Left = 90,
                Top = 14,
                AutoEllipsis = true
            };
            pnlLeft.Controls.Add(lblNamaProduk);

            // Label nama sesi PO
            Label lblJudulPO = new Label
            {
                Text = judulPo,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.SlateGray,
                AutoSize = false,
                Width = 160,
                Height = 18,
                Left = 90,
                Top = 36,
                AutoEllipsis = true
            };
            pnlLeft.Controls.Add(lblJudulPO);

            // Badge status / waktu
            Label lblBadge = new Label
            {
                Text = badgeText,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = badgeFg,
                BackColor = badgeColor,
                AutoSize = false,
                Width = 160,
                Height = 22,
                Left = 90,
                Top = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(4, 0, 4, 0)
            };
            lblBadge.Paint += (s, e) =>
            {
                // Rounded corners badge
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = RoundedRect(new Rectangle(0, 0, lblBadge.Width - 1, lblBadge.Height - 1), 8))
                using (var brush = new SolidBrush(badgeColor))
                {
                    e.Graphics.FillPath(brush, path);
                    TextRenderer.DrawText(e.Graphics, lblBadge.Text, lblBadge.Font,
                        new Rectangle(0, 0, lblBadge.Width, lblBadge.Height),
                        badgeFg,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            };
            pnlLeft.Controls.Add(lblBadge);

            // ══════════════════════════════
            // AREA TENGAH – Progress + Waktu
            // ══════════════════════════════
            Panel pnlMid = new Panel
            {
                Width = 0,    // diatur saat Resize
                Height = card.Height,
                Left = 275,
                Top = 0,
                BackColor = Color.Transparent,
                Name = "pnlMid"
            };
            card.Controls.Add(pnlMid);

            // Label "Progress Slot"
            Label lblProgressTitle = new Label
            {
                Text = "PROGRESS SLOT",
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.Gray,
                AutoSize = true,
                Left = 8,
                Top = 18
            };
            pnlMid.Controls.Add(lblProgressTitle);

            // Progress Bar custom
            Panel pnlProgressBg = new Panel
            {
                Height = 14,
                Left = 8,
                Top = 38,
                BackColor = ClrUnguSoft,
                Name = "pnlProgressBg"
            };
            pnlMid.Controls.Add(pnlProgressBg);

            Panel pnlProgressFill = new Panel
            {
                Height = 14,
                Left = 0,
                Top = 0,
                BackColor = ClrUnguMedium,
                Name = "pnlProgressFill"
            };
            pnlProgressBg.Controls.Add(pnlProgressFill);

            // Rounded corners progress
            pnlProgressBg.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = RoundedRect(new Rectangle(0, 0, pnlProgressBg.Width - 1, pnlProgressBg.Height - 1), 6))
                using (var brush = new SolidBrush(ClrUnguSoft))
                {
                    e.Graphics.FillPath(brush, path);
                    int fillW = (int)(pnlProgressBg.Width * (progressPct / 100.0));
                    if (fillW > 0)
                    {
                        var fillRect = new Rectangle(0, 0, fillW, pnlProgressBg.Height - 1);
                        using (var fillPath = RoundedRect(fillRect, 6))
                        using (var fillBrush = new SolidBrush(ClrUnguDark))
                            e.Graphics.FillPath(fillBrush, fillPath);
                    }
                }
            };

            Label lblSlotInfo = new Label
            {
                Text = progressPct > 0 ? $"{progressPct}% Slot Terisi" : "Info slot tidak tersedia",
                Font = new Font("Segoe UI", 8F),
                ForeColor = ClrTextDark,
                AutoSize = true,
                Left = 8,
                Top = 56
            };
            pnlMid.Controls.Add(lblSlotInfo);

            // Countdown timer
            Label lblCountdown = new Label
            {
                Text = $"⏱ {countdown}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = (batasWaktu.HasValue && (batasWaktu.Value - DateTime.Now).TotalHours <= 2)
                            ? Color.Crimson : ClrUnguDark,
                AutoSize = true,
                Left = 8,
                Top = 72
            };
            pnlMid.Controls.Add(lblCountdown);

            // ══════════════════════════════
            // AREA KANAN – Harga + Tombol
            // ══════════════════════════════
            Panel pnlRight = new Panel
            {
                Width = 210,
                Height = card.Height,
                BackColor = Color.Transparent,
                Name = "pnlRight"
            };
            card.Controls.Add(pnlRight);

            // Harga
            bool adaDiskon = hargaDiskon > 0 && hargaDiskon < hargaDasar;
            int hargaShow = adaDiskon ? hargaDiskon : hargaDasar;

            Label lblHarga = new Label
            {
                Text = "Rp " + hargaShow.ToString("N0"),
                Font = new Font("Segoe UI Black", 13F, FontStyle.Bold),
                ForeColor = ClrUnguDark,
                AutoSize = true,
                Left = 8,
                Top = 14
            };
            pnlRight.Controls.Add(lblHarga);

            if (adaDiskon)
            {
                Label lblHargaCoret = new Label
                {
                    Text = "Rp " + hargaDasar.ToString("N0"),
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Left = 8,
                    Top = 38
                };
                // Coretan via Paint
                lblHargaCoret.Paint += (s, e) =>
                {
                    TextRenderer.DrawText(e.Graphics, lblHargaCoret.Text, lblHargaCoret.Font,
                        new Point(0, 0), Color.Gray);
                    int midY = lblHargaCoret.Height / 2;
                    e.Graphics.DrawLine(Pens.Gray, 0, midY, lblHargaCoret.Width, midY);
                };
                pnlRight.Controls.Add(lblHargaCoret);
            }

            // Tombol "Lihat Detail" (filled)
            Button btnDetail = new Button
            {
                Text = "📋 Lihat Detail",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = ClrUnguDark,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 30),
                Left = 8,
                Top = 55,
                Cursor = Cursors.Hand,
                Tag = card.Tag
            };
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += (s, e) =>
            {
                // Hook ini bisa disambungkan ke detail view sesuai arsitektur proyek
                MessageBox.Show($"Membuka detail: {namaProduk}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            pnlRight.Controls.Add(btnDetail);

            // Tombol "Pesan Sekarang" (outline)
            Button btnPesan = new Button
            {
                Text = "🛒 Pesan Sekarang",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = ClrKuningLemon,
                ForeColor = ClrTextDark,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 28),
                Left = 8,
                Top = 60,
                Cursor = Cursors.Hand,
                Tag = card.Tag,
                Visible = false   // tampilkan sesuai logika bisnis
            };
            btnPesan.FlatAppearance.BorderSize = 1;
            btnPesan.FlatAppearance.BorderColor = ClrKuningHangat;
            pnlRight.Controls.Add(btnPesan);

            // ── Atur posisi pnlRight menempel kanan card ──
            card.Resize += (s, e) =>
            {
                pnlRight.Left = card.Width - pnlRight.Width - 12;
                int midLeft = pnlLeft.Right + 8;
                int midWidth = pnlRight.Left - midLeft - 8;
                pnlMid.Left = midLeft;
                pnlMid.Width = midWidth > 0 ? midWidth : 1;
                pnlProgressBg.Width = pnlMid.Width - 16;
                pnlProgressFill.Width = (int)((pnlProgressBg.Width) * (progressPct / 100.0));
            };

            // Trigger awal
            int initMidLeft = pnlLeft.Right + 8;
            int initMidWidth = 940 - initMidLeft - pnlRight.Width - 20;
            pnlMid.Left = initMidLeft;
            pnlMid.Width = initMidWidth > 80 ? initMidWidth : 200;
            pnlProgressBg.Width = pnlMid.Width - 16;
            pnlProgressFill.Width = (int)(pnlProgressBg.Width * (progressPct / 100.0));
            pnlRight.Left = 940 - pnlRight.Width - 12;

            return card;
        }

        // ─────────────────────────────────────────────
        // HELPER: Path rounded rectangle
        // ─────────────────────────────────────────────
        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
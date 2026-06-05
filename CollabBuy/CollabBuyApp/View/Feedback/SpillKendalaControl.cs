using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class SpillKendalaControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly ComplaintController _controller;

        public SpillKendalaControl(Models.User user)
        {
            this.InitializeComponent();

            this._currentUser = user;
            this._controller = new ComplaintController();

            this.Resize += (s, e) => this.AdjustLayout();

            // Mendaftarkan event click untuk tombol detail di tabel
            this.dgvRiwayat.CellClick += new DataGridViewCellEventHandler(this.dgvRiwayat_CellClick);
        }

        private void SpillKendalaControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadRiwayat();
        }

        private void btnAduan_Click(object sender, EventArgs e)
        {
            var (sukses, pesan) = this._controller.GasSpillKendala(this._currentUser.GetIdUser(), this.txtSubjek.Text, this.txtDeskripsi.Text);

            if (sukses)
            {
                MessageBox.Show("Laporan udah masuk ke sistem! Beberapa saat lagi akan dikabari sama Mimin ya bestie! 💌",
                                "Aduan Terkirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSubjek.Clear();
                this.txtDeskripsi.Clear();
                this.LoadRiwayat();
            }
            else
            {
                MessageBox.Show(pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadRiwayat()
        {
            DataTable dtRaw = this._controller.GetRiwayatSpill(this._currentUser.GetIdUser());

            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("subjek", typeof(string));
            dtUI.Columns.Add("deskripsi", typeof(string)); // Preview
            dtUI.Columns.Add("tanggal", typeof(string));
            dtUI.Columns.Add("status", typeof(string));
            dtUI.Columns.Add("balasan", typeof(string)); // Preview

            // Kolom tersembunyi untuk menyimpan data full
            dtUI.Columns.Add("full_deskripsi", typeof(string));
            dtUI.Columns.Add("full_balasan", typeof(string));

            if (dtRaw != null)
            {
                foreach (DataRow row in dtRaw.Rows)
                {
                    string subjek = row["subjek"].ToString();
                    string deskripsiRaw = row["deskripsi"].ToString();

                    bool isSelesai;
                    if (!row.IsNull("is_selesai"))
                    {
                        isSelesai = Convert.ToBoolean(row["is_selesai"]);
                    }
                    else
                    {
                        isSelesai = false;
                    }

                    string balasanRaw;
                    if (row["balasan"] != DBNull.Value)
                    {
                        balasanRaw = row["balasan"].ToString();
                    }
                    else
                    {
                        balasanRaw = "";
                    }

                    Complaint aduanObj = new Complaint(this._currentUser.IdUser, subjek, deskripsiRaw);

                    aduanObj.Status = isSelesai ? "Selesai" : "Menunggu";
                    if (!string.IsNullOrWhiteSpace(balasanRaw))
                    {
                        aduanObj.TanggapanAdmin = balasanRaw;
                    }

                    string statusKece = aduanObj.DapatkanStatusUI();
                    string previewTeks = aduanObj.DapatkanPreviewDeskripsi(30);

                    string previewBalasan;
                    string fullBalasanAsli;

                    if (string.IsNullOrWhiteSpace(aduanObj.TanggapanAdmin))
                    {
                        previewBalasan = "Belum direspon";
                        fullBalasanAsli = "Mimin belum membalas aduan ini. Harap bersabar ya bestie~";
                    }
                    else
                    {
                        fullBalasanAsli = aduanObj.TanggapanAdmin;
                        if (aduanObj.TanggapanAdmin.Length > 35)
                        {
                            previewBalasan = aduanObj.TanggapanAdmin.Substring(0, 35) + "...";
                        }
                        else
                        {
                            previewBalasan = aduanObj.TanggapanAdmin;
                        }
                    }

                    string tanggalFormat;
                    if (row["tanggal"] != DBNull.Value)
                    {
                        tanggalFormat = Convert.ToDateTime(row["tanggal"]).ToString("dd MMM yyyy, HH:mm");
                    }
                    else
                    {
                        tanggalFormat = "-";
                    }

                    dtUI.Rows.Add(
                        subjek,
                        previewTeks,
                        tanggalFormat,
                        statusKece,
                        previewBalasan,
                        deskripsiRaw,    // Simpan full teks
                        fullBalasanAsli  // Simpan full teks
                    );
                }
            }
            else
            {
                bool tableKosong = true;
            }

            // Bersihkan kolom lama agar tidak duplikat saat refresh
            this.dgvRiwayat.DataSource = null;
            this.dgvRiwayat.Columns.Clear();

            this.dgvRiwayat.DataSource = dtUI;

            if (this.dgvRiwayat.Columns.Count > 0)
            {
                this.dgvRiwayat.Columns["subjek"].HeaderText = "Subjek Masalah";
                this.dgvRiwayat.Columns["deskripsi"].HeaderText = "Detail Curhatan";
                this.dgvRiwayat.Columns["tanggal"].HeaderText = "Waktu Spill";
                this.dgvRiwayat.Columns["status"].HeaderText = "Status";
                this.dgvRiwayat.Columns["balasan"].HeaderText = "Balasan Mimin";

                // Sembunyikan kolom full
                this.dgvRiwayat.Columns["full_deskripsi"].Visible = false;
                this.dgvRiwayat.Columns["full_balasan"].Visible = false;

                // Styling Tabel biar Ungu Estetik
                this.dgvRiwayat.EnableHeadersVisualStyles = false;
                this.dgvRiwayat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255);
                this.dgvRiwayat.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
                this.dgvRiwayat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                this.dgvRiwayat.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 230, 255);
                this.dgvRiwayat.DefaultCellStyle.SelectionForeColor = Color.Black;
                this.dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.dgvRiwayat.RowTemplate.Height = 35;

                // Tambahkan Tombol Detail di akhir tabel
                DataGridViewButtonColumn btnDetailCol = new DataGridViewButtonColumn();
                btnDetailCol.Name = "BtnDetail";
                btnDetailCol.HeaderText = "Aksi";
                btnDetailCol.Text = "🔍 Baca Detail";
                btnDetailCol.UseColumnTextForButtonValue = true;
                btnDetailCol.FlatStyle = FlatStyle.Flat;
                btnDetailCol.DefaultCellStyle.BackColor = Color.FromArgb(36, 0, 70);
                btnDetailCol.DefaultCellStyle.ForeColor = Color.FromArgb(253, 255, 182);
                btnDetailCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnDetailCol.Width = 120;

                this.dgvRiwayat.Columns.Add(btnDetailCol);
            }
            else
            {
                bool gridAwal = true;
            }
        }

        private void dgvRiwayat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.dgvRiwayat.Columns[e.ColumnIndex].Name == "BtnDetail")
                {
                    DataTable dataSource = this.dgvRiwayat.DataSource as DataTable;
                    if (dataSource != null)
                    {
                        DataRow row = dataSource.Rows[e.RowIndex];
                        string subjek = row["subjek"].ToString();
                        string status = row["status"].ToString();
                        string tanggal = row["tanggal"].ToString();
                        string fullDeskripsi = row["full_deskripsi"].ToString();
                        string fullBalasan = row["full_balasan"].ToString();

                        this.TampilkanDetailLayarPenuh(subjek, status, tanggal, fullDeskripsi, fullBalasan);
                    }
                    else
                    {
                        bool dataKosong = true;
                    }
                }
                else
                {
                    bool bukanTombol = true;
                }
            }
            else
            {
                bool headerDiklik = true;
            }
        }

        // =========================================================
        // PERBAIKAN: Menggunakan TableLayoutPanel Anti Tumpang Tindih
        // =========================================================
        private void TampilkanDetailLayarPenuh(string subjek, string status, string tanggal, string deskripsi, string balasan)
        {
            // Sembunyikan elemen utama halaman
            this.pnlForm.Visible = false;
            this.lblRiwayat.Visible = false;
            this.dgvRiwayat.Visible = false;

            // Panel Utama SPA
            Panel pnlFull = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            TableLayoutPanel tblGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            tblGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));   // 0 Header (Tombol & Judul)
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));   // 1 Label Kronologi
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));    // 2 Txt Kronologi (Proporsional)
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));   // 3 Label Balasan
            tblGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));    // 4 Txt Balasan (Proporsional)

            // [Row 0] Header Panel
            Panel pnlHdr = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };

            Button btnKembali = new Button
            {
                Text = "⬅ Kembali",
                Font = new Font("Segoe UI Black", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                BackColor = Color.FromArgb(235, 204, 255),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 40),
                Location = new Point(0, 10),
                Cursor = Cursors.Hand
            };
            btnKembali.FlatAppearance.BorderSize = 0;
            btnKembali.Click += (s, ev) =>
            {
                this.Controls.Remove(pnlFull);
                pnlFull.Dispose();

                // Munculkan kembali halaman utama
                this.pnlForm.Visible = true;
                this.lblRiwayat.Visible = true;
                this.dgvRiwayat.Visible = true;
            };
            pnlHdr.Controls.Add(btnKembali);

            Label lblDetailTitle = new Label
            {
                Text = $"🗣️ Subjek: {subjek}",
                Font = new Font("Segoe UI Black", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                AutoSize = true,
                Location = new Point(130, 10)
            };
            pnlHdr.Controls.Add(lblDetailTitle);

            Label lblStatus = new Label
            {
                Text = $"Status: {status}  |  Waktu Spill: {tanggal}",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(90, 24, 154),
                AutoSize = true,
                Location = new Point(132, 40)
            };
            pnlHdr.Controls.Add(lblStatus);

            tblGrid.Controls.Add(pnlHdr, 0, 0);

            // [Row 1] Label Kronologi
            Label lblKronologi = new Label
            {
                Text = "📝 Kronologi Curhatan Kamu:",
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomLeft,
                Height = 30
            };
            tblGrid.Controls.Add(lblKronologi, 0, 1);

            // [Row 2] Textbox Kronologi
            TextBox txtKronologiFull = new TextBox
            {
                Text = deskripsi,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10.5F),
                BackColor = Color.White,
                Margin = new Padding(0, 5, 0, 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            tblGrid.Controls.Add(txtKronologiFull, 0, 2);

            // [Row 3] Label Balasan
            Label lblBalasan = new Label
            {
                Text = "💬 Balasan dari Mimin:",
                Font = new Font("Segoe UI Black", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomLeft,
                Height = 30
            };
            tblGrid.Controls.Add(lblBalasan, 0, 3);

            // [Row 4] Textbox Balasan
            TextBox txtBalasanFull = new TextBox
            {
                Text = balasan,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10.5F),
                BackColor = Color.FromArgb(245, 232, 255), // Ungu muda agar beda dengan curhatan
                Margin = new Padding(0, 5, 0, 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            tblGrid.Controls.Add(txtBalasanFull, 0, 4);

            pnlFull.Controls.Add(tblGrid);
            this.Controls.Add(pnlFull);
            pnlFull.BringToFront();
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int formW = (int)(w * 0.42);

            if (formW < 300)
            {
                formW = 300;
            }
            else
            {
                bool ukuranAman = true;
            }

            if (this.pnlForm != null)
            {
                this.pnlForm.Width = formW;
                this.pnlForm.Height = this.Height - this.pnlForm.Top - margin;

                int innerW = formW - 48;

                if (this.txtSubjek != null && this.txtDeskripsi != null && this.btnAduan != null)
                {
                    this.txtSubjek.Width = innerW;
                    this.txtDeskripsi.Width = innerW;
                    this.txtDeskripsi.Height = this.pnlForm.Height - this.btnAduan.Height - 200;

                    this.btnAduan.Top = this.txtDeskripsi.Top + this.txtDeskripsi.Height + 15;
                    this.btnAduan.Width = innerW;
                }
                else
                {
                    bool formKosong = true;
                }
            }
            else
            {
                bool pnlFormKosong = true;
            }

            if (this.lblRiwayat != null && this.dgvRiwayat != null)
            {
                int riwayatLeft = margin + formW + 24;
                this.lblRiwayat.Left = riwayatLeft;
                this.dgvRiwayat.Left = riwayatLeft;
                this.dgvRiwayat.Width = this.Width - riwayatLeft - margin;
                this.dgvRiwayat.Height = this.Height - this.dgvRiwayat.Top - margin;
            }
            else
            {
                bool dgvKosong = true;
            }
        }
    }
}
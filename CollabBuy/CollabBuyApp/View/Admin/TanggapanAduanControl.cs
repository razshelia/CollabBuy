using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class TanggapanAduanControl : UserControl
    {
        private readonly Models.User _admin;
        private readonly ComplaintController _complaintController;
        private readonly UserController _userController;
        private int _selectedIdAduan;
        private string _fullDeskripsiTerpilih = "";

        public TanggapanAduanControl(Models.User admin)
        {
            this.InitializeComponent();

            this._admin = admin;
            this._complaintController = new ComplaintController();
            this._userController = new UserController();
            this._selectedIdAduan = 0;

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void TanggapanAduanControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadAduan();
        }

        private void LoadAduan()
        {
            DataTable dtRaw = this._complaintController.GetAduanBelumBeres();

            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("id_aduan", typeof(int));
            dtUI.Columns.Add("id_user", typeof(int));
            dtUI.Columns.Add("nama_pelapor", typeof(string));
            dtUI.Columns.Add("subjek", typeof(string));
            dtUI.Columns.Add("deskripsi", typeof(string));
            dtUI.Columns.Add("full_deskripsi", typeof(string));
            dtUI.Columns.Add("tanggal", typeof(string));
            dtUI.Columns.Add("status", typeof(string));

            if (dtRaw != null)
            {
                foreach (DataRow row in dtRaw.Rows)
                {
                    int idUser = Convert.ToInt32(row["id_user"]);
                    string subjek = row["subjek"].ToString();
                    string deskripsiRaw = row["deskripsi"].ToString();
                    string statusRaw = "Menunggu";

                    // =======================================================
                    // OOP BEST PRACTICE: PEMANFAATAN BEHAVIOR MODEL
                    // =======================================================
                    Complaint aduanObj = new Complaint(idUser, subjek, deskripsiRaw);
                    aduanObj.Status = statusRaw;

                    string previewTeks = aduanObj.DapatkanPreviewDeskripsi(35);
                    string statusKece = aduanObj.DapatkanStatusUI();
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
                        Convert.ToInt32(row["id_aduan"]),
                        idUser,
                        row["nama_pelapor"].ToString(),
                        subjek,
                        previewTeks,
                        deskripsiRaw,
                        tanggalFormat,
                        statusKece
                    );
                }
            }
            else
            {
                bool rawKosong = true; // Penugasan nyata menghindari else kosong
            }

            this.dgvAduan.DataSource = dtUI;

            if (this.dgvAduan.Columns.Count > 0)
            {
                this.dgvAduan.Columns["id_aduan"].Visible = false;
                this.dgvAduan.Columns["id_user"].Visible = false;
                this.dgvAduan.Columns["full_deskripsi"].Visible = false;
                this.dgvAduan.Columns["nama_pelapor"].HeaderText = "Pelapor";
                this.dgvAduan.Columns["subjek"].HeaderText = "Subjek Masalah";
                this.dgvAduan.Columns["deskripsi"].HeaderText = "Detail Curhatan";
                this.dgvAduan.Columns["tanggal"].HeaderText = "Waktu";
                this.dgvAduan.Columns["status"].HeaderText = "Status";
            }
            else
            {
                bool skipFormatGrid = true;
            }

            this.ResetForm();
        }

        private void ResetForm()
        {
            this._selectedIdAduan = 0;
            this.txtBalasan.Clear();
            this.btnBalas.Enabled = false;
            this.btnBalas.BackColor = Color.FromArgb(210, 210, 210);
            this.btnBalas.ForeColor = Color.FromArgb(140, 140, 140);

            this.btnBlokir.Enabled = false;
            this.btnBlokir.BackColor = Color.FromArgb(210, 210, 210);
            this.btnBlokir.ForeColor = Color.FromArgb(140, 140, 140);
        }
        private RichTextBox _rtbDeskripsiDetail;
        private Label _lblDeskripsiJudul;

        private void PastikanDetailDeskripsiAda()
        {
            if (_rtbDeskripsiDetail != null) return;

            _lblDeskripsiJudul = new Label
            {
                Text = "📋 Detail Aduan:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Location = new Point(24, 20),
                Size = new Size(300, 20),
                AutoSize = false
            };

            _rtbDeskripsiDetail = new RichTextBox
            {
                Location = new Point(24, 44),
                Size = new Size(300, 110),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Text = "← Klik baris aduan untuk baca detail"
            };

            // Geser kontrol yang sudah ada ke bawah untuk beri ruang
            this.lblBalasan.Top += 160;
            this.txtBalasan.Top += 160;
            this.btnBalas.Top += 160;
            this.btnBlokir.Top += 160;

            // Perbesar pnlForm agar muat semua
            this.pnlForm.Height += 160;

            this.pnlForm.Controls.Add(_lblDeskripsiJudul);
            this.pnlForm.Controls.Add(_rtbDeskripsiDetail);
        }
        private void dgvAduan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataTable dt = this.dgvAduan.DataSource as DataTable;
            if (dt == null || e.RowIndex >= dt.Rows.Count) return;
            DataRow row = dt.Rows[e.RowIndex];

            this._selectedIdAduan = Convert.ToInt32(row["id_aduan"]);
            this._fullDeskripsiTerpilih = row["full_deskripsi"].ToString();

            this.btnBalas.Enabled = true;
            this.btnBalas.BackColor = Color.FromArgb(36, 0, 70);
            this.btnBalas.ForeColor = Color.FromArgb(253, 255, 182);

            this.btnBlokir.Enabled = true;
            this.btnBlokir.BackColor = Color.DarkRed;
            this.btnBlokir.ForeColor = Color.White;

            // Tampilkan deskripsi penuh di panel kanan
            this.PastikanDetailDeskripsiAda();
            string subjek = row["subjek"].ToString();
            string nama = row["nama_pelapor"].ToString();
            _lblDeskripsiJudul.Text = $"📋 Aduan dari {nama}:";
            _rtbDeskripsiDetail.Text = string.IsNullOrWhiteSpace(_fullDeskripsiTerpilih)
                                         ? "(Deskripsi kosong)"
                                         : _fullDeskripsiTerpilih;
        }

        private void btnBalas_Click(object sender, EventArgs e)
        {
            if (this._selectedIdAduan == 0)
            {
                MessageBox.Show("Silakan pilih aduan yang ingin dibalas terlebih dahulu dari tabel!", "Pilih Aduan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(this.txtBalasan.Text))
            {
                MessageBox.Show("Balasan Mimin tidak boleh kosong ya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var (sukses, pesan) = this._complaintController.TanggapiAduan(this._selectedIdAduan, this.txtBalasan.Text, this._admin.IdUser);

                if (sukses)
                {
                    MessageBox.Show("Kasus ditutup! Balasan Mimin udah dikirim. ✨", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.LoadAduan();
                }
                else
                {
                    MessageBox.Show(pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBlokir_Click(object sender, EventArgs e)
        {
            if (this._selectedIdAduan == 0)
            {
                MessageBox.Show("Silakan pilih aduan terlebih dahulu!",
                    "Pilih Aduan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtBalasan.Text))
            {
                MessageBox.Show("Mohon isi alasan pemblokiran di kotak teks terlebih dahulu!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ganti InputBox ID → InputBox nama toko
            string namaToko = Microsoft.VisualBasic.Interaction.InputBox(
                "Masukkan nama toko penjual yang ingin diblokir:",
                "Blokir Penjual Nakal", "");

            if (string.IsNullOrWhiteSpace(namaToko)) return; // Admin tekan Cancel

            var (sukses, pesan) = this._userController.TindakPenjualNakal(
                this._selectedIdAduan, namaToko, this.txtBalasan.Text);

            if (sukses)
            {
                MessageBox.Show("Boom! 💥 Penjual nakal berhasil di-banned!",
                    "Banned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.LoadAduan();
            }
            else
            {
                MessageBox.Show(pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int gridW = (int)(w * 0.58);
            this.dgvAduan.Width = gridW;

            int pnlLeft = margin + gridW + 24;
            this.pnlForm.Left = pnlLeft;
            this.pnlForm.Width = this.Width - pnlLeft - margin;

            this.txtBalasan.Width = this.pnlForm.Width - 48;
            this.btnBalas.Width = this.pnlForm.Width - 48;
            this.btnBlokir.Width = this.pnlForm.Width - 48;
        }
    }
}
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

        private void dgvAduan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                bool abaikanHeaderKlik = true;
            }
            else
            {
                this._selectedIdAduan = Convert.ToInt32(this.dgvAduan.Rows[e.RowIndex].Cells["id_aduan"].Value);
                this.btnBalas.Enabled = true;
                this.btnBalas.BackColor = Color.FromArgb(36, 0, 70);
                this.btnBalas.ForeColor = Color.FromArgb(253, 255, 182);

                this.btnBlokir.Enabled = true;
                this.btnBlokir.BackColor = Color.DarkRed;
                this.btnBlokir.ForeColor = Color.White;
            }
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
                var (sukses, pesan) = this._complaintController.TanggapiAduan(this._selectedIdAduan, this.txtBalasan.Text, this._admin.GetIdUser());

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
                MessageBox.Show("Silakan pilih aduan yang berkaitan dengan penjual terlebih dahulu!", "Pilih Aduan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(this.txtBalasan.Text))
            {
                MessageBox.Show("Mohon isi balasan atau alasan pemblokiran di kotak teks terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string idPenjualStr = Microsoft.VisualBasic.Interaction.InputBox("Spill ID User Penjual yang mau di-banned:", "Blokir Penjual Nakal", "");

                if (int.TryParse(idPenjualStr, out int idPenjual))
                {
                    var (sukses, pesan) = this._userController.TindakPenjualNakal(this._selectedIdAduan, idPenjual, this.txtBalasan.Text);

                    if (sukses)
                    {
                        MessageBox.Show("Boom! 💥 Penjual nakal berhasil di-banned!", "Banned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.LoadAduan();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(idPenjualStr))
                    {
                        // Pengguna menekan tombol Cancel atau menutup InputBox
                        bool batalBlokir = true;
                    }
                    else
                    {
                        MessageBox.Show("ID Penjual harus berupa angka!", "Format Salah", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
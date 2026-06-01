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
        private readonly User _admin;
        private readonly ComplaintController _complaintController;
        private readonly UserController _userController;
        private int _selectedIdAduan = 0;

        public TanggapanAduanControl(User admin)
        {
            InitializeComponent();
            _admin = admin;
            _complaintController = new ComplaintController();
            _userController = new UserController();

            this.Resize += (s, e) => AdjustLayout();
        }

        private void TanggapanAduanControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            LoadAduan();
        }

        private void LoadAduan()
        {
            // Ambil data mentah
            DataTable dtRaw = _complaintController.GetAduanBelumBeres();

            // Buat DataTable baru yang sudah dipoles untuk UI
            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("id_aduan", typeof(int));
            dtUI.Columns.Add("id_user", typeof(int));
            dtUI.Columns.Add("nama_pelapor", typeof(string));
            dtUI.Columns.Add("subjek", typeof(string));
            dtUI.Columns.Add("deskripsi", typeof(string)); // Akan kita isi preview
            dtUI.Columns.Add("tanggal", typeof(string));
            dtUI.Columns.Add("status", typeof(string)); // Akan kita isi status emoji

            foreach (DataRow row in dtRaw.Rows)
            {
                int idUser = Convert.ToInt32(row["id_user"]);
                string subjek = row["subjek"].ToString();
                string deskripsiRaw = row["deskripsi"].ToString();
                string statusRaw = "Menunggu"; // Default query ini memanggil aduan pending

                // --- OOP BEST PRACTICE CALL ---
                // Kita buat objek untuk memformat teks yang akan tampil di grid
                Complaint aduanObj = new Complaint(idUser, subjek, deskripsiRaw);
                aduanObj.SetStatus(statusRaw);

                // Gunakan Method Behavior Model!
                string previewTeks = aduanObj.DapatkanPreviewDeskripsi(35); // Batasi 35 huruf
                string statusKece = aduanObj.DapatkanStatusUI(); // Tambah Emoji
                // ------------------------------

                dtUI.Rows.Add(
                    Convert.ToInt32(row["id_aduan"]),
                    idUser,
                    row["nama_pelapor"].ToString(),
                    subjek,
                    previewTeks, // Yang tampil di grid jadi rapi
                    Convert.ToDateTime(row["tanggal"]).ToString("dd MMM yyyy, HH:mm"),
                    statusKece
                );
            }

            dgvAduan.DataSource = dtUI;

            if (dgvAduan.Columns.Count > 0)
            {
                dgvAduan.Columns["id_aduan"].Visible = false;
                dgvAduan.Columns["id_user"].Visible = false;
                dgvAduan.Columns["nama_pelapor"].HeaderText = "Pelapor";
                dgvAduan.Columns["subjek"].HeaderText = "Subjek Masalah";
                dgvAduan.Columns["deskripsi"].HeaderText = "Detail Curhatan";
                dgvAduan.Columns["tanggal"].HeaderText = "Waktu";
                dgvAduan.Columns["status"].HeaderText = "Status";
            }
            ResetForm();
        }

        private void ResetForm()
        {
            _selectedIdAduan = 0;
            txtBalasan.Clear();
            btnBalas.Enabled = false;
            btnBlokir.Enabled = false;
        }

        private void dgvAduan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedIdAduan = Convert.ToInt32(dgvAduan.Rows[e.RowIndex].Cells["id_aduan"].Value);
                btnBalas.Enabled = true;
                btnBlokir.Enabled = true;
            }
        }

        private void btnBalas_Click(object sender, EventArgs e)
        {
            if (_selectedIdAduan == 0) return;

            var res = _complaintController.TanggapiAduan(_selectedIdAduan, txtBalasan.Text, _admin.GetIdUser());
            if (res.sukses)
            {
                MessageBox.Show("Kasus ditutup! Balasan Mimin udah dikirim. ✨", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAduan();
            }
            else MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnBlokir_Click(object sender, EventArgs e)
        {
            if (_selectedIdAduan == 0) return;

            string idPenjualStr = Microsoft.VisualBasic.Interaction.InputBox("Spill ID User Penjual yang mau di-banned:", "Blokir Penjual Nakal", "");
            if (int.TryParse(idPenjualStr, out int idPenjual))
            {
                var res = _userController.TindakPenjualNakal(_selectedIdAduan, idPenjual, txtBalasan.Text);
                if (res.sukses)
                {
                    MessageBox.Show("Boom! 💥 Penjual nakal berhasil di-banned!", "Banned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAduan();
                }
                else MessageBox.Show(res.pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int gridW = (int)(w * 0.58);
            dgvAduan.Width = gridW;

            int pnlLeft = margin + gridW + 24;
            pnlForm.Left = pnlLeft;
            pnlForm.Width = this.Width - pnlLeft - margin;
            txtBalasan.Width = pnlForm.Width - 48;
            btnBalas.Width = pnlForm.Width - 48;
            btnBlokir.Width = pnlForm.Width - 48;
        }
    }
}
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
        }

        private void TanggapanAduanControl_Load(object sender, EventArgs e)
        {
            LoadAduan();
        }

        private void LoadAduan()
        {
            dgvAduan.DataSource = _complaintController.GetAduanBelumBeres();
            if (dgvAduan.Columns.Count > 0)
            {
                dgvAduan.Columns["id_aduan"].Visible = false;
                dgvAduan.Columns["id_user"].Visible = false;
                dgvAduan.Columns["nama_pelapor"].HeaderText = "Pelapor";
                dgvAduan.Columns["subjek"].HeaderText = "Subjek Masalah";
                dgvAduan.Columns["deskripsi"].HeaderText = "Detail Curhatan";
                dgvAduan.Columns["tanggal"].HeaderText = "Waktu";
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
    }
}
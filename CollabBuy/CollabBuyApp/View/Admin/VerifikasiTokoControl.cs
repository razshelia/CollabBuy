using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class VerifikasiTokoControl : UserControl
    {
        private readonly UserController _userController;
        private int _selectedIdUser = 0;

        public VerifikasiTokoControl()
        {
            InitializeComponent();
            _userController = new UserController();

            this.Resize += (s, e) => AdjustLayout();
        }

        private void VerifikasiTokoControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            LoadVerifikasi();
        }

        private void LoadVerifikasi()
        {
            dgvVerifikasi.DataSource = _userController.GetAntreanLapak();
            if (dgvVerifikasi.Columns.Count > 0)
            {
                dgvVerifikasi.Columns["id_user"].Visible = false;
                dgvVerifikasi.Columns["bukti_ktm"].Visible = false;
                dgvVerifikasi.Columns["nama_owner"].HeaderText = "Nama Pemilik";
                dgvVerifikasi.Columns["nim"].HeaderText = "NIM";
                dgvVerifikasi.Columns["nama_toko"].HeaderText = "Nama Lapak";
                dgvVerifikasi.Columns["tahun_masuk"].HeaderText = "Angkatan";
            }
            pbKTM.Image = null;
            btnApprove.Enabled = false;
            _selectedIdUser = 0;
        }

        private void dgvVerifikasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVerifikasi.Rows[e.RowIndex];
                _selectedIdUser = Convert.ToInt32(row.Cells["id_user"].Value);
                btnApprove.Enabled = true;

                // Coba render foto KTM dari Byte Array ke PictureBox
                if (row.Cells["bukti_ktm"].Value != DBNull.Value)
                {
                    try
                    {
                        byte[] imgBytes = (byte[])row.Cells["bukti_ktm"].Value;
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            pbKTM.Image = Image.FromStream(ms);
                        }
                    }
                    catch { pbKTM.Image = null; }
                }
                else { pbKTM.Image = null; }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (_selectedIdUser == 0) return;

            DialogResult dr = MessageBox.Show("Udah yakin datanya bener dan mau di-ACC lapaknya?", "Konfirmasi ACC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                var res = _userController.ValidasiPenjual(_selectedIdUser);
                if (res.sukses)
                {
                    MessageBox.Show("Mantap! Lapak bestie ini udah resmi dibuka. 🎉", "ACC Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVerifikasi();
                }
                else MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int gridW = (int)(w * 0.58);
            dgvVerifikasi.Width = gridW;

            int pnlLeft = margin + gridW + 24;
            pnlKTM.Left = pnlLeft;
            pnlKTM.Width = this.Width - pnlLeft - margin;
            pbKTM.Width = pnlKTM.Width - 48;
            btnApprove.Width = pnlKTM.Width - 48;
        }
    }
}
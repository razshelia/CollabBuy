using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class KelolaUserControl : UserControl
    {
        private readonly AdminController _adminController;
        private int _selectedIdUser = 0;

        public KelolaUserControl()
        {
            InitializeComponent();
            _adminController = new AdminController();
        }

        private void KelolaUserControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataUser();
            this.Resize += (s, ev) => AdjustLayout();
            AdjustLayout();
        }

        private void SetupDataGridView()
        {
            dgvUser.AutoGenerateColumns = false;
            dgvUser.Columns.Clear();

            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdUser", DataPropertyName = "id_user", Visible = false });
            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Lengkap", DataPropertyName = "nama", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", DataPropertyName = "username", Width = 130 });
            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "email", Width = 180 });
            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Telepon", HeaderText = "No. WA", DataPropertyName = "nomor_telepon", Width = 120 });
            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Peran", HeaderText = "Peran", DataPropertyName = "peran", Width = 80 });
            dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", DataPropertyName = "status_akun", Width = 100 });
        }

        private void LoadDataUser()
        {
            try
            {
                DataTable dt = _adminController.GetSemuaUser();
                dgvUser.DataSource = dt;
                dgvUser.ClearSelection();
                ResetDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUser.Rows[e.RowIndex];
            _selectedIdUser = Convert.ToInt32(row.Cells["IdUser"].Value);

            lblDetailNama.Text = row.Cells["Nama"].Value.ToString();
            lblDetailUsername.Text = "@" + row.Cells["Username"].Value.ToString();
            lblDetailEmail.Text = row.Cells["Email"].Value.ToString();
            lblDetailTelepon.Text = row.Cells["Telepon"].Value.ToString();
            lblDetailPeran.Text = row.Cells["Peran"].Value.ToString();

            string status = row.Cells["Status"].Value.ToString();
            lblDetailStatus.Text = status;
            lblDetailStatus.ForeColor = status == "Aktif" ? Color.ForestGreen : Color.Red;

            bool isDiblokir = status == "Diblokir";
            btnBlokir.Text = isDiblokir ? "✅ Aktifkan Akun" : "🚫 Blokir Akun";
            btnBlokir.BackColor = isDiblokir ? Color.ForestGreen : Color.FromArgb(200, 0, 0);
            btnBlokir.Enabled = true;
        }

        private void btnBlokir_Click(object sender, EventArgs e)
        {
            if (_selectedIdUser == 0) return;

            bool isDiblokir = lblDetailStatus.Text == "Diblokir";
            string aksi = isDiblokir ? "mengaktifkan kembali" : "memblokir";

            DialogResult dr = MessageBox.Show(
                $"Yakin mau {aksi} akun '{lblDetailNama.Text}'?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                var (sukses, pesan) = _adminController.ToggleBlokirUser(_selectedIdUser, !isDiblokir);
                if (sukses)
                {
                    MessageBox.Show(pesan, "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataUser();
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataUser();
        }

        private void ResetDetail()
        {
            _selectedIdUser = 0;
            lblDetailNama.Text = "Klik baris untuk lihat detail";
            lblDetailUsername.Text = "-";
            lblDetailEmail.Text = "-";
            lblDetailTelepon.Text = "-";
            lblDetailPeran.Text = "-";
            lblDetailStatus.Text = "-";
            lblDetailStatus.ForeColor = Color.Gray;
            btnBlokir.Enabled = false;
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            int gridW = (int)(w * 0.62);

            pnlCard.Width = gridW;
            pnlCard.Height = this.Height - pnlCard.Top - margin;
            dgvUser.Width = pnlCard.Width - 48;
            dgvUser.Height = pnlCard.Height - btnRefresh.Height - 70;
            btnRefresh.Top = pnlCard.Height - btnRefresh.Height - 20;
            btnRefresh.Left = pnlCard.Width - btnRefresh.Width - 14;

            int detailLeft = margin + gridW + 24;
            pnlDetail.Left = detailLeft;
            pnlDetail.Width = this.Width - detailLeft - margin;
            pnlDetail.Height = this.Height - pnlDetail.Top - margin;
            btnBlokir.Width = pnlDetail.Width - 40;
        }
    }
}
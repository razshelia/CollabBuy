using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class KelolaUserControl : UserControl
    {
        private readonly AdminController _adminController;
        private readonly Models.User _currentUser;
        private int _selectedIdUser;
        private string _selectedRawStatus;
        private ToolTip _gridTooltip = new ToolTip();
        private DataTable _dtUserCache;

        public KelolaUserControl(Models.User currentUser)
        {
            InitializeComponent();
            this._adminController = new AdminController();
            this._currentUser = currentUser;
            this._selectedIdUser = 0;
            this._selectedRawStatus = "";
        }

        private void KelolaUserControl_Load(object sender, EventArgs e)
        {
            this.SetupDataGridView();
            this.LoadDataUser();
            this.Resize += (s, ev) => this.AdjustLayout();
            this.BeginInvoke(new Action(() => this.AdjustLayout()));
        }

        private void SetupDataGridView()
        {
            this.dgvUser.AutoGenerateColumns = false;
            this.dgvUser.Columns.Clear();

            // Kolom tersembunyi untuk logika internal
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdUser", DataPropertyName = "id_user", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "RawStatus", DataPropertyName = "raw_status", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "EmailRaw", DataPropertyName = "email", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "TeleponRaw", DataPropertyName = "nomor_telepon", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaRaw", DataPropertyName = "nama_raw", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "UsernameRaw", DataPropertyName = "username_raw", Visible = false });

            // Kolom tampilan UI
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Lengkap", DataPropertyName = "nama", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", DataPropertyName = "username", Width = 110 });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "InfoKontak", HeaderText = "Info Kontak", DataPropertyName = "info_kontak", Width = 230 });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Peran", HeaderText = "Peran", DataPropertyName = "peran", Width = 140 });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Akun", DataPropertyName = "status_akun", Width = 140 });
            _gridTooltip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ShowAlways = true };
            this.dgvUser.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (this.dgvUser.Columns[e.ColumnIndex].Name != "InfoKontak") return;
                string teks = this.dgvUser.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                if (teks.Length > 30)
                    _gridTooltip.Show(teks, this.dgvUser,
                        this.dgvUser.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location, 5000);
            };
            this.dgvUser.CellMouseLeave += (s, e) => _gridTooltip.Hide(this.dgvUser);
        }

        private void LoadDataUser()
        {
            try
            {
                DataTable dtRaw = this._adminController.GetSemuaUser();
                DataTable dtUI = new DataTable();

                // Definisi kolom DataTable UI
                dtUI.Columns.Add("id_user", typeof(int));
                dtUI.Columns.Add("raw_status", typeof(string));
                dtUI.Columns.Add("email", typeof(string));
                dtUI.Columns.Add("nomor_telepon", typeof(string));
                dtUI.Columns.Add("nama_raw", typeof(string)); // nama asli tanpa format
                dtUI.Columns.Add("username_raw", typeof(string)); // username asli tanpa @
                dtUI.Columns.Add("nama", typeof(string));
                dtUI.Columns.Add("username", typeof(string));
                dtUI.Columns.Add("info_kontak", typeof(string));
                dtUI.Columns.Add("peran", typeof(string));
                dtUI.Columns.Add("status_akun", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string nama = row["nama"].ToString();
                    string username = row["username"].ToString();
                    string email = row["email"].ToString();
                    string telepon = row["nomor_telepon"].ToString();
                    string peran = row["peran"].ToString();
                    string statusRaw = row["status_akun"].ToString();

                    Pembeli userTemp = new Pembeli(nama, username, "placeholder");
                    if (!string.IsNullOrWhiteSpace(email) && email != "-") userTemp.Email = email;
                    if (!string.IsNullOrWhiteSpace(telepon) && telepon != "-") userTemp.NomorTelepon = telepon;
                    string infoKontak = userTemp.DapatkanInfoKontak();

                    string tipeUser = peran == "Admin" ? "Administrator Sistem"
                        : peran == "Penjual" ? "Penjual Terverifikasi"
                        : "Pembeli";

                    string statusKece = statusRaw == "Diblokir" ? "🚫 Diblokir" : "✅ Aktif";

                    dtUI.Rows.Add(
                        Convert.ToInt32(row["id_user"]),
                        statusRaw,           // raw: "Aktif" / "Diblokir"
                        email,               // email asli untuk detail panel
                        telepon,             // telepon asli untuk detail panel
                        nama,                // nama asli (tanpa format) untuk detail panel
                        username,            // username asli (tanpa @) untuk detail panel
                        nama,                // kolom tampilan Nama
                        "@" + username,      // kolom tampilan Username
                        infoKontak,
                        tipeUser,
                        statusKece
                    );
                }

                this._dtUserCache = dtUI;
                this.dgvUser.DataSource = this._dtUserCache;
                this.dgvUser.DataSource = dtUI;
                this.dgvUser.ClearSelection();
                this.ResetDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TerapkanFilterUser()
        {
            if (this._dtUserCache == null) return;
            string kata = this.txtCariUser.Text.Trim().ToLower();
            DataView dv = this._dtUserCache.DefaultView;
            if (string.IsNullOrEmpty(kata))
                dv.RowFilter = "";
            else
                dv.RowFilter = $"nama LIKE '%{kata}%' OR username LIKE '%{kata}%' OR info_kontak LIKE '%{kata}%'";
            this.dgvUser.DataSource = dv;
            this.dgvUser.ClearSelection();
            this.ResetDetail();
        }
        private void txtCariUser_TextChanged(object sender, EventArgs e)
        {
            this.TerapkanFilterUser();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = this.dgvUser.Rows[e.RowIndex];

            this._selectedIdUser = Convert.ToInt32(row.Cells["IdUser"].Value);
            this._selectedRawStatus = row.Cells["RawStatus"].Value?.ToString() ?? "";

            // Gunakan kolom raw (tanpa prefix @ atau format panjang) untuk panel detail
            this.lblDetailNama.Text = row.Cells["NamaRaw"].Value?.ToString() ?? "-";
            this.lblDetailUsername.Text = "@" + (row.Cells["UsernameRaw"].Value?.ToString() ?? "-");
            this.lblDetailEmail.Text = row.Cells["EmailRaw"].Value?.ToString() ?? "-";
            this.lblDetailTelepon.Text = row.Cells["TeleponRaw"].Value?.ToString() ?? "-";
            this.lblDetailPeran.Text = row.Cells["Peran"].Value?.ToString() ?? "-";

            // Status tampil dengan warna
            if (this._selectedRawStatus == "Aktif")
            {
                this.lblDetailStatus.Text = "✅ Aktif";
                this.lblDetailStatus.ForeColor = Color.ForestGreen;
                this.btnBlokir.Text = "🚫 Blokir Akun";
                this.btnBlokir.BackColor = Color.FromArgb(200, 0, 0);
            }
            else
            {
                this.lblDetailStatus.Text = "🚫 Diblokir";
                this.lblDetailStatus.ForeColor = Color.Red;
                this.btnBlokir.Text = "✅ Aktifkan Akun";
                this.btnBlokir.BackColor = Color.ForestGreen;
            }

            this.btnBlokir.Enabled = true;
        }

        private void btnBlokir_Click(object sender, EventArgs e)
        {
            if (this._selectedIdUser == 0) return;
            if (this._selectedIdUser == this._currentUser.IdUser)
            {
                MessageBox.Show("Kamu tidak bisa memblokir akun kamu sendiri!",
                    "Tidak Diizinkan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool sedangDiblokir = (this._selectedRawStatus == "Diblokir");
            string aksi = sedangDiblokir ? "mengaktifkan kembali" : "memblokir";

            DialogResult dr = MessageBox.Show(
                $"Yakin mau {aksi} akun '{this.lblDetailNama.Text}'?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            // Jika sedang diblokir → kita AKTIFKAN → blokir = false
            // Jika sedang aktif    → kita BLOKIR   → blokir = true
            bool blokirBaru = !sedangDiblokir;

            var (sukses, pesan) = this._adminController.ToggleBlokirUser(
                this._selectedIdUser, blokirBaru, this._currentUser.IdUser);
            if (sukses)
            {
                MessageBox.Show(pesan, "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.LoadDataUser();
            }
            else
            {
                MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataUser();
        }

        private void ResetDetail()
        {
            this._selectedIdUser = 0;
            this._selectedRawStatus = "";

            this.lblDetailNama.Text = "Klik baris untuk lihat detail";
            this.lblDetailUsername.Text = "-";
            this.lblDetailEmail.Text = "-";
            this.lblDetailTelepon.Text = "-";
            this.lblDetailPeran.Text = "-";
            this.lblDetailStatus.Text = "-";
            this.lblDetailStatus.ForeColor = Color.Gray;
            this.btnBlokir.Enabled = false;
            this.btnBlokir.BackColor = Color.FromArgb(210, 210, 210);
            this.btnBlokir.ForeColor = Color.FromArgb(140, 140, 140);
            this.btnBlokir.Text = "— Pilih User Dulu —";
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            int gridW = (int)(w * 0.60);

            pnlCard.Width = gridW;
            pnlCard.Height = this.Height - pnlCard.Top - margin;

            // Posisi txtCariUser sudah fixed di Designer (Y=14, Height=28)
            // dgvUser dimulai di bawah txtCari
            dgvUser.Top = 50;                                       
            dgvUser.Width = pnlCard.Width - 48;
            dgvUser.Height = pnlCard.Height - btnRefresh.Height - 80; 

            btnRefresh.Top = pnlCard.Height - btnRefresh.Height - 15;
            btnRefresh.Left = pnlCard.Width - btnRefresh.Width - 14;

            int detailLeft = margin + gridW + 20;
            pnlDetail.Left = detailLeft;
            pnlDetail.Width = this.Width - detailLeft - margin;
            pnlDetail.Height = this.Height - pnlDetail.Top - margin;
            pnlDetail.AutoScroll = true;
            pnlDetail.AutoScrollMinSize = new System.Drawing.Size(0, 625);

            btnBlokir.Width = 160;
            btnBlokir.Left = (pnlDetail.Width - btnBlokir.Width) / 2;
        }
    }
}
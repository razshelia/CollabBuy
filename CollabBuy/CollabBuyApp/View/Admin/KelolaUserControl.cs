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
        private int _selectedIdUser;
        private string _selectedRawStatus;

        public KelolaUserControl()
        {
            InitializeComponent();
            this._adminController = new AdminController();
            this._selectedIdUser = 0;
            this._selectedRawStatus = "";
        }

        private void KelolaUserControl_Load(object sender, EventArgs e)
        {
            this.SetupDataGridView();
            this.LoadDataUser();
            this.Resize += (s, ev) => this.AdjustLayout();
            this.AdjustLayout();
        }

        private void SetupDataGridView()
        {
            this.dgvUser.AutoGenerateColumns = false;
            this.dgvUser.Columns.Clear();

            // Kolom Tersembunyi untuk kebutuhan logika
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdUser", DataPropertyName = "id_user", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "RawStatus", DataPropertyName = "raw_status", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "EmailRaw", DataPropertyName = "email", Visible = false });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "TeleponRaw", DataPropertyName = "nomor_telepon", Visible = false });

            // Kolom UI Kece
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nama", HeaderText = "Nama Lengkap", DataPropertyName = "nama", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", DataPropertyName = "username", Width = 110 });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "InfoKontak", HeaderText = "Info Kontak (Auto)", DataPropertyName = "info_kontak", Width = 230 });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Peran", HeaderText = "Peran (Tipe)", DataPropertyName = "peran", Width = 140 });
            this.dgvUser.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status Akun", DataPropertyName = "status_akun", Width = 140 });
        }

        private void LoadDataUser()
        {
            try
            {
                DataTable dtRaw = this._adminController.GetSemuaUser();
                DataTable dtUI = new DataTable();

                // Pembuatan kerangka DataTable untuk UI
                dtUI.Columns.Add("id_user", typeof(int));
                dtUI.Columns.Add("raw_status", typeof(string));
                dtUI.Columns.Add("email", typeof(string));
                dtUI.Columns.Add("nomor_telepon", typeof(string));
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

                    // =======================================================
                    // OOP BEST PRACTICE: POLIMORFISME & BEHAVIOR
                    // =======================================================
                    User userObj;
                    if (peran == "Penjual")
                    {
                        userObj = new Penjual(nama, username, "dummy");
                    }
                    else if (peran == "Admin")
                    {
                        userObj = new Models.Admin(nama, username, "dummy", "dummy");
                    }
                    else
                    {
                        userObj = new Pembeli(nama, username, "dummy");
                    }

                    userObj.SetEmail(email);
                    userObj.SetNomorTelepon(telepon);

                    // Hydration Status Pemblokiran
                    if (statusRaw == "Diblokir")
                    {
                        userObj.Blokir("Terdeteksi pelanggaran sistem");
                    }
                    else
                    {
                        userObj.BukaBlokir();
                    }

                    // Pemanfaatan Method Cerdas Model
                    string kontakKece = userObj.DapatkanInfoKontak();
                    string statusKece = userObj.DapatkanStatusAkun();
                    string tipeUser = userObj.GetTipeUser(); // Pembuktian Overriding/Polimorfisme

                    dtUI.Rows.Add(
                        Convert.ToInt32(row["id_user"]),
                        statusRaw,
                        email,
                        telepon,
                        nama,
                        "@" + username,
                        kontakKece,
                        tipeUser,
                        statusKece
                    );
                }

                this.dgvUser.DataSource = dtUI;
                this.dgvUser.ClearSelection();
                this.ResetDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            else
            {
                var row = this.dgvUser.Rows[e.RowIndex];

                this._selectedIdUser = Convert.ToInt32(row.Cells["IdUser"].Value);
                this._selectedRawStatus = row.Cells["RawStatus"].Value.ToString();

                this.lblDetailNama.Text = row.Cells["Nama"].Value.ToString();
                this.lblDetailUsername.Text = row.Cells["Username"].Value.ToString();

                // Ambil dari kolom hidden yang kita siapkan khusus detail panel
                this.lblDetailEmail.Text = row.Cells["EmailRaw"].Value.ToString();
                this.lblDetailTelepon.Text = row.Cells["TeleponRaw"].Value.ToString();
                this.lblDetailPeran.Text = row.Cells["Peran"].Value.ToString();

                string statusKece = row.Cells["Status"].Value.ToString();
                this.lblDetailStatus.Text = statusKece;

                if (this._selectedRawStatus == "Aktif")
                {
                    this.lblDetailStatus.ForeColor = Color.ForestGreen;
                    this.btnBlokir.Text = "🚫 Blokir Akun";
                    this.btnBlokir.BackColor = Color.FromArgb(200, 0, 0);
                }
                else
                {
                    this.lblDetailStatus.ForeColor = Color.Red;
                    this.btnBlokir.Text = "✅ Aktifkan Akun";
                    this.btnBlokir.BackColor = Color.ForestGreen;
                }

                this.btnBlokir.Enabled = true;
            }
        }

        private void btnBlokir_Click(object sender, EventArgs e)
        {
            if (this._selectedIdUser == 0)
            {
                return;
            }
            else
            {
                bool isDiblokir;
                string aksi;

                if (this._selectedRawStatus == "Diblokir")
                {
                    isDiblokir = true;
                    aksi = "mengaktifkan kembali";
                }
                else
                {
                    isDiblokir = false;
                    aksi = "memblokir";
                }

                DialogResult dr = MessageBox.Show(
                    $"Yakin mau {aksi} akun '{this.lblDetailNama.Text}'?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    var (sukses, pesan) = this._adminController.ToggleBlokirUser(this._selectedIdUser, !isDiblokir);
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
                else
                {
                    // Eksekusi dibatalkan oleh pengguna
                    bool cancel = true;
                }
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
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            int gridW = (int)(w * 0.62);

            this.pnlCard.Width = gridW;
            this.pnlCard.Height = this.Height - this.pnlCard.Top - margin;
            this.dgvUser.Width = this.pnlCard.Width - 48;
            this.dgvUser.Height = this.pnlCard.Height - this.btnRefresh.Height - 70;
            this.btnRefresh.Top = this.pnlCard.Height - this.btnRefresh.Height - 20;
            this.btnRefresh.Left = this.pnlCard.Width - this.btnRefresh.Width - 14;

            int detailLeft = margin + gridW + 24;
            this.pnlDetail.Left = detailLeft;
            this.pnlDetail.Width = this.Width - detailLeft - margin;
            this.pnlDetail.Height = this.Height - this.pnlDetail.Top - margin;
            this.btnBlokir.Width = this.pnlDetail.Width - 40;
        }
    }
}
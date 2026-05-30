using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class LogAktivitasControl : UserControl
    {
        private readonly AdminController _adminController;

        public LogAktivitasControl()
        {
            InitializeComponent();
            _adminController = new AdminController();
        }

        private void LogAktivitasControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadLog("Semua");
            this.Resize += (s, ev) => AdjustLayout();
            AdjustLayout();
        }

        private void SetupDataGridView()
        {
            dgvLog.AutoGenerateColumns = false;
            dgvLog.Columns.Clear();

            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pelaku", HeaderText = "Nama User", DataPropertyName = "pelaku", Width = 160 });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Peran", HeaderText = "Peran", DataPropertyName = "peran", Width = 80 });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Aktivitas", HeaderText = "Aktivitas", DataPropertyName = "aktivitas", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Waktu", HeaderText = "Waktu Akses", DataPropertyName = "waktu_format", Width = 160 });
        }

        private void LoadLog(string filter)
        {
            try
            {
                // Panggil AdminController → ActivityLogRepository.GetAllAsDataTable()
                DataTable dtRaw = _adminController.GetLogAktivitasDataTable();
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("pelaku", typeof(string));
                dtUI.Columns.Add("peran", typeof(string));
                dtUI.Columns.Add("aktivitas", typeof(string));
                dtUI.Columns.Add("waktu_format", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string aktivitas = row["aktivitas"].ToString().ToLower();
                    bool isLogin = aktivitas.Contains("login") || aktivitas.Contains("logout");
                    bool isPerubahan = aktivitas.Contains("ubah") || aktivitas.Contains("update") ||
                                       aktivitas.Contains("edit") || aktivitas.Contains("ganti") ||
                                       aktivitas.Contains("tambah") || aktivitas.Contains("hapus") ||
                                       aktivitas.Contains("blokir") || aktivitas.Contains("acc") ||
                                       aktivitas.Contains("verif");

                    bool tampilkan = filter == "Semua"
                        || (filter == "Login/Logout" && isLogin)
                        || (filter == "Perubahan Data" && isPerubahan);

                    if (tampilkan)
                    {
                        string waktu = Convert.ToDateTime(row["waktu_akses"]).ToString("dd MMM yyyy, HH:mm");
                        dtUI.Rows.Add(row["pelaku"], row["peran"], row["aktivitas"], waktu);
                    }
                }

                dgvLog.DataSource = dtUI;
                lblJumlah.Text = $"Menampilkan {dtUI.Rows.Count} aktivitas";
                dgvLog.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSemua_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnSemua);
            LoadLog("Semua");
        }

        private void btnLoginLogout_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnLoginLogout);
            LoadLog("Login/Logout");
        }

        private void btnPerubahan_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnPerubahan);
            LoadLog("Perubahan Data");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnSemua);
            LoadLog("Semua");
        }

        private void SetActiveFilter(Button activeBtn)
        {
            foreach (Button btn in new[] { btnSemua, btnLoginLogout, btnPerubahan })
            {
                btn.BackColor = Color.FromArgb(200, 182, 255);
                btn.ForeColor = Color.FromArgb(36, 0, 70);
            }
            activeBtn.BackColor = Color.FromArgb(36, 0, 70);
            activeBtn.ForeColor = Color.FromArgb(253, 255, 182);
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            pnlCard.Width = w;
            pnlCard.Height = this.Height - pnlCard.Top - margin;
            dgvLog.Width = pnlCard.Width - 48;
            dgvLog.Height = pnlCard.Height - dgvLog.Top - 20;
            btnRefresh.Left = pnlCard.Width - btnRefresh.Width - 24;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

// PERHATIKAN: Tidak ada lagi 'using CollabBuy.CollabBuyApp.Repositories;' di sini!
// Ini membuktikan View kita sangat taat aturan MVC!

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class LogAktivitasControl : UserControl
    {
        private readonly AdminController _adminController;
        private Button _btnExport; // Tombol export rahasia buatan kode

        public LogAktivitasControl()
        {
            InitializeComponent();
            _adminController = new AdminController();
        }

        private void LogAktivitasControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadLog("Semua");
            BuatTombolExport();

            this.Resize += (s, ev) => AdjustLayout();
            AdjustLayout();
        }

        private void SetupDataGridView()
        {
            dgvLog.AutoGenerateColumns = false;
            dgvLog.Columns.Clear();

            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pelaku", HeaderText = "Nama User", DataPropertyName = "pelaku", Width = 140 });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Peran", HeaderText = "Peran", DataPropertyName = "peran", Width = 80 });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kategori", HeaderText = "Kategori", DataPropertyName = "kategori", Width = 130 });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Aktivitas", HeaderText = "Aktivitas", DataPropertyName = "aktivitas", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvLog.Columns.Add(new DataGridViewTextBoxColumn { Name = "Waktu", HeaderText = "Waktu Akses", DataPropertyName = "waktu_format", Width = 150 });
        }

        private void LoadLog(string filter)
        {
            try
            {
                DataTable dtRaw = _adminController.GetLogAktivitasDataTable();
                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("pelaku", typeof(string));
                dtUI.Columns.Add("peran", typeof(string));
                dtUI.Columns.Add("kategori", typeof(string));
                dtUI.Columns.Add("aktivitas", typeof(string));
                dtUI.Columns.Add("waktu_format", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string teksAktivitas = row["aktivitas"].ToString();
                    DateTime waktuAkses = Convert.ToDateTime(row["waktu_akses"]);

                    // Buat Objek Model untuk memanfaatkan fungsi cerdasnya
                    ActivityLog logObj = new ActivityLog(1, teksAktivitas);
                    logObj.SetWaktuAkses(waktuAkses);

                    // Panggil Method / Behavior dari Model
                    string kategoriObjek = logObj.DapatkanKategori();
                    bool isHariIni = logObj.ApakahHariIni();

                    // Filter menggunakan kategori dari objek, kodenya jadi sangat bersih!
                    bool tampilkan = filter == "Semua"
                        || (filter == "Login/Logout" && kategoriObjek == "Autentikasi")
                        || (filter == "Perubahan Data" && (kategoriObjek == "Perubahan Data" || kategoriObjek == "Tindakan Kritis"));

                    if (tampilkan)
                    {
                        string waktuFormat = waktuAkses.ToString("dd MMM yyyy, HH:mm");
                        if (isHariIni)
                        {
                            waktuFormat = "🔥 HARI INI, " + waktuAkses.ToString("HH:mm");
                        }

                        dtUI.Rows.Add(row["pelaku"], row["peran"], kategoriObjek, teksAktivitas, waktuFormat);
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

        // ==========================================================
        // FITUR EXPORT 100% BEST PRACTICE MVC (Lewat Controller)
        // ==========================================================
        private void BuatTombolExport()
        {
            _btnExport = new Button();
            _btnExport.Text = "💾 Export (.txt)";
            _btnExport.BackColor = Color.FromArgb(0, 150, 80); // Warna hijau khas Excel/Teks
            _btnExport.ForeColor = Color.White;
            _btnExport.FlatStyle = FlatStyle.Flat;
            _btnExport.FlatAppearance.BorderSize = 0;
            _btnExport.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            _btnExport.Cursor = Cursors.Hand;
            _btnExport.Size = new Size(130, 35);
            _btnExport.Click += BtnExport_Click;

            pnlCard.Controls.Add(_btnExport);
            _btnExport.BringToFront();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text File|*.txt";
                sfd.FileName = "Audit_Trail_CollabBuy_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                sfd.Title = "Simpan Export Log Aktivitas";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // View meminta data dari Controller, BUKAN dari Repository
                        List<ActivityLog> logs = _adminController.GetAllActivityLogs();

                        List<string> barisTeks = new List<string>();
                        barisTeks.Add("=== AUDIT TRAIL COLLABBUY ===");
                        barisTeks.Add("Diexport pada: " + DateTime.Now.ToString("dd MMM yyyy HH:mm:ss"));
                        barisTeks.Add("=============================\n");

                        foreach (ActivityLog log in logs)
                        {
                            // View memanfaatkan method formatting dari Model (Information Expert)
                            barisTeks.Add(log.DapatkanFormatLog());
                        }

                        File.WriteAllLines(sfd.FileName, barisTeks);
                        MessageBox.Show("Yeay! Log aktivitas berhasil diexport ke TXT.", "Sukses Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Waduh gagal export nih: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

            if (_btnExport != null)
            {
                _btnExport.Top = btnRefresh.Top;
                _btnExport.Left = btnRefresh.Left - _btnExport.Width - 10;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class DashboardAdminControl : UserControl
    {
        private AdminController _adminController;

        public DashboardAdminControl()
        {
            InitializeComponent();
            _adminController = new AdminController();
        }

        private void DashboardAdminControl_Load(object sender, EventArgs e)
        {
            // PERBAIKAN: Setup kolom grid SEBELUM memuat data dan mengakses nama kolom.
            // Sebelumnya, kode mengakses dgvRecentActivity.Columns["Waktu"] setelah data diload
            // tanpa menjamin kolom bernama "Waktu" benar-benar ada, menyebabkan NullReferenceException.
            SetupRecentActivityGrid();
            LoadDashboardData();
        }

        /// <summary>
        /// Mendefinisikan kolom DataGridView secara eksplisit agar nama kolom dapat diandalkan.
        /// PERBAIKAN OOP: Setup kolom dipisahkan ke method sendiri (Single Responsibility).
        /// </summary>
        private void SetupRecentActivityGrid()
        {
            dgvRecentActivity.AutoGenerateColumns = false;
            dgvRecentActivity.Columns.Clear();

            dgvRecentActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Waktu",
                HeaderText = "Waktu",
                DataPropertyName = "Waktu",
                Width = 150
            });

            DataGridViewTextBoxColumn colAktivitas = new DataGridViewTextBoxColumn
            {
                Name = "Aktivitas",
                HeaderText = "Aktivitas",
                DataPropertyName = "Aktivitas",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgvRecentActivity.Columns.Add(colAktivitas);

            dgvRecentActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "User",
                HeaderText = "User",
                DataPropertyName = "User",
                Width = 200
            });
        }

        private void LoadDashboardData()
        {
            try
            {
                // Ambil statistik dari controller
                int totalUsers = _adminController.GetTotalUsersCount();
                int pendingShops = _adminController.GetPendingShopVerificationsCount();
                int openComplaints = _adminController.GetOpenComplaintsCount();

                // Update label statistik jika ada di form
                // lblValueUsers.Text = totalUsers.ToString();
                // lblValueShops.Text = pendingShops.ToString();
                // lblValueComplaints.Text = openComplaints.ToString();

                // Muat log aktivitas terbaru
                List<ActivityLog> logs = _adminController.GetLogAktivitas();
                DataTable dtLog = new DataTable();
                dtLog.Columns.Add("Waktu", typeof(string));
                dtLog.Columns.Add("Aktivitas", typeof(string));
                dtLog.Columns.Add("User", typeof(string));

                foreach (ActivityLog log in logs)
                {
                    dtLog.Rows.Add(
                        log.GetWaktuAkses().ToString("dd MMM yyyy HH:mm"),
                        log.GetAktivitas(),
                        "User #" + log.GetIdUser()
                    );
                }

                dgvRecentActivity.DataSource = dtLog;
                // Kolom sudah disetup di SetupRecentActivityGrid(), tidak perlu re-set lagi.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
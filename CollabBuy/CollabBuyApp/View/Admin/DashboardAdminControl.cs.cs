using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class DashboardAdminControl : UserControl
    {
        // Mendeklarasikan controller (gunakan Dependency Injection jika sistem Anda mendukungnya)
        private AdminController _adminController;

        public DashboardAdminControl()
        {
            InitializeComponent();

            // Inisialisasi controller (sesuaikan dengan implementasi Anda saat ini)
            _adminController = new AdminController();
        }

        private void DashboardAdminControl_Load(object sender, EventArgs e)
        {
            // Panggil fungsi load saat User Control dirender di MainForm
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                /* * CATATAN: Kode di bawah ini adalah representasi dari Controller Anda.
                 * Sesuaikan pemanggilan method dengan method yang sudah ada 
                 * di dalam class AdminController Anda.
                 */

                //1.Mengambil Jumlah Pengguna
                // int totalUsers = _adminController.GetTotalUsers();
                //lblValueUsers.Text = totalUsers.ToString();

                ////2.Mengambil Jumlah Toko yang Menunggu Verifikasi
                // int pendingShops = _adminController.GetPendingShopVerificationsCount();
                //lblValueShops.Text = pendingShops.ToString();

                ////3.Mengambil Jumlah Aduan yang Belum Diselesaikan
                // int openComplaints = _adminController.GetOpenComplaintsCount();
                //lblValueComplaints.Text = openComplaints.ToString();

                ////4.Load DataGridView untuk Aktivitas / Log Terbaru
                //DataTable dtActivities = _adminController.GetRecentActivities(10); // Ambil 10 data terakhir
                //dgvRecentActivity.DataSource = dtActivities;

                // Merapikan kolom DataGridView
                dgvRecentActivity.Columns["Waktu"].Width = 150;
                dgvRecentActivity.Columns["Aktivitas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvRecentActivity.Columns["User"].Width = 200;
                // ---------------------------------------------
            }
            catch (Exception ex)
            {
                // Gunakan NotifikasiUX yang dibuat sebelumnya untuk standardisasi error
                // NotifikasiUX.Error("Gagal memuat data dashboard: " + ex.Message);
                MessageBox.Show("Gagal memuat data dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

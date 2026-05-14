using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminUserManagementControl : UserControl
    {
        private UserService userService;

        public AdminUserManagementControl()
        {
            this.InitializeComponent();
            this.userService = new UserService();
            this.MuatDataUser();
        }

        private void MuatDataUser()
        {
            // Ambil data dari repository via service
            this.dgvUsers.DataSource = this.userService.MuatDaftarPengajuanVerifikasi();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (this.dgvUsers.SelectedRows.Count > 0)
            {
                int idVerifikasi = Convert.ToInt32(this.dgvUsers.SelectedRows[0].Cells[0].Value);

                if (this.userService.SetujuiPenjual(idVerifikasi))
                {
                    this.MuatDataUser(); // Refresh grid
                }
            }
            else
            {
                UXHelper.TampilkanError("Pilih dulu user yang mau di-approve, Mimin sayang! 😘");
            }
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            if (this.dgvUsers.SelectedRows.Count > 0)
            {
                if (UXHelper.TampilkanKonfirmasi("Yakin mau block user ini? Gabisa balik lagi lho!"))
                {
                    // Logika panggil userService.BlokirAkun()
                    UXHelper.TampilkanSukses("User berhasil dikandangkan! 🚫");
                    this.MuatDataUser();
                }
            }
        }
    }
}
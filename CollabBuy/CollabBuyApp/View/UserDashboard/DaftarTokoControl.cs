using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DaftarTokoControl : UserControl
    {
        private User _currentUser;
        private readonly UserController _userController;

        public DaftarTokoControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userController = new UserController();
        }

        private void DaftarTokoControl_Load(object sender, EventArgs e)
        {
            CekStatusVerifikasi();
        }

        private void CekStatusVerifikasi()
        {
            // Pengecekan status saat ini.
            // Sesuaikan logika di bawah dengan status verifikasi di database Anda.
            // Misalnya, cek tabel Verification atau properti User.Role / User.StatusVerifikasi

            bool isVerifiedSeller = false; // Ganti dengan logika pengecekan dari model/database
            bool isPendingVerification = false; // Ganti dengan logika pengecekan dari model/database

            // *Contoh Dummy Logic*
            // isVerifiedSeller = _currentUser.Role == "Penjual"; 
            // isPendingVerification = _userController.CheckPendingVerification(_currentUser.IdUser);

            if (isVerifiedSeller)
            {
                // Jika sudah menjadi penjual, form dinonaktifkan dan pesan diubah
                pnlForm.Enabled = false;
                pnlStatus.Visible = true;
                lblStatusVerifikasi.Text = "✅ Selamat! Anda sudah terverifikasi sebagai Penjual.";
                pnlStatus.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (isPendingVerification)
            {
                // Jika pengajuan sudah masuk tapi belum di-acc admin
                pnlForm.Enabled = false;
                pnlStatus.Visible = true;
                lblStatusVerifikasi.Text = "⏳ Pengajuan lapak Anda sedang menunggu review Admin.";
                pnlStatus.BackColor = System.Drawing.Color.FromArgb(253, 255, 182); // Kuning pastel
            }
            else
            {
                // Belum mengajukan sama sekali, form siap diisi
                pnlForm.Enabled = true;
                pnlStatus.Visible = false;
            }
        }

        private void btnAjukan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input
            if (string.IsNullOrWhiteSpace(txtNamaToko.Text) || string.IsNullOrWhiteSpace(txtDeskripsi.Text))
            {
                MessageBox.Show("Nama Toko dan Deskripsi harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validasi Persetujuan Syarat
            if (!chkSyarat.Checked)
            {
                MessageBox.Show("Anda harus menyetujui syarat dan ketentuan sebelum mengajukan verifikasi.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Konfirmasi
            DialogResult dialog = MessageBox.Show($"Ajukan pembuatan lapak dengan nama '{txtNamaToko.Text}'?",
                                                  "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                ProsesPengajuan();
            }
        }

        private void ProsesPengajuan()
        {
            try
            {
                string namaToko = txtNamaToko.Text.Trim();
                string deskripsi = txtDeskripsi.Text.Trim();

                // PANGGIL CONTROLLER UNTUK MENYIMPAN PENGAJUAN
                // TODO: Pastikan ada metode di Controller untuk menyimpan pengajuan ini.
                // bool success = _userController.AjukanVerifikasiToko(_currentUser.IdUser, namaToko, deskripsi);

                // MOCK SUCCESS
                bool success = true;

                if (success)
                {
                    MessageBox.Show("Pengajuan berhasil dikirim! Silakan tunggu konfirmasi dari Admin.",
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Segarkan status UI
                    CekStatusVerifikasi();

                    // Opsional: Paksa set status mock untuk melihat perubahan langsung
                    pnlForm.Enabled = false;
                    pnlStatus.Visible = true;
                    lblStatusVerifikasi.Text = "⏳ Pengajuan lapak Anda sedang menunggu review Admin.";
                }
                else
                {
                    MessageBox.Show("Gagal mengirim pengajuan. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

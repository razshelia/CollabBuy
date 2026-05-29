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
    public partial class KelolaProfilControl : UserControl
    {
        private User _currentUser;
        private readonly UserController _userController;

        public KelolaProfilControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userController = new UserController();
        }

        private void KelolaProfilControl_Load(object sender, EventArgs e)
        {
            LoadDataProfil();
        }

        private void LoadDataProfil()
        {
            if (_currentUser != null)
            {
                // Asumsi property model User: Nama, NIM, Email
                // Sesuaikan dengan nama property yang benar di class User.cs Anda
                txtNama.Text = _currentUser.Nama;
                txtNIM.Text = _currentUser.NIM; // NIM dibuat ReadOnly di Designer
                txtEmail.Text = _currentUser.Email;

                // Kosongkan password agar tidak terekspos, user hanya mengisi jika ingin mengubah
                txtPassword.Clear();
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input Dasar
            if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Nama dan Email tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi format email sederhana
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Format email tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Konfirmasi
            DialogResult dialog = MessageBox.Show("Apakah Anda yakin ingin menyimpan perubahan profil ini?",
                                                  "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                ProsesSimpanProfil();
            }
        }

        private void ProsesSimpanProfil()
        {
            try
            {
                // Update object currentUser dengan data baru
                _currentUser.Nama = txtNama.Text.Trim();
                _currentUser.Email = txtEmail.Text.Trim();

                // Jika user mengetikkan password baru, kita ubah passwordnya
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    _currentUser.Password = txtPassword.Text; // Pastikan controller/repository Anda menghandle hashing jika ada
                }

                // PANGGIL CONTROLLER UNTUK UPDATE KE DATABASE
                // TODO: Pastikan UserController memiliki method UpdateProfile atau EditUser
                // bool success = _userController.UpdateProfile(_currentUser);

                // MOCK SUCCESS
                bool success = true;

                if (success)
                {
                    MessageBox.Show("Profil berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Bersihkan kolom password lagi setelah sukses menyimpan
                    txtPassword.Clear();

                    // Opsional: Jika Nama berubah, Anda mungkin perlu memicu event untuk memperbarui 
                    // label nama di Sidebar MainForm. 
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan perubahan profil. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

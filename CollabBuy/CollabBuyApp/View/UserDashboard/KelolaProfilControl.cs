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
                // PERBAIKAN: Model User menggunakan metode getter (GetNama(), GetEmail()),
                // bukan properti auto (.Nama, .Email). Sesuai pola enkapsulasi OOP proyek ini.
                txtNama.Text = _currentUser.GetNama();
                txtEmail.Text = _currentUser.GetEmail() ?? "";

                // NIM hanya ada pada Penjual — cek dengan casting
                Penjual penjual = _currentUser as Penjual;
                if (penjual != null)
                {
                    txtNIM.Text = penjual.GetNim() ?? "";
                }
                else
                {
                    txtNIM.Text = "";
                    txtNIM.Enabled = false; // Pembeli tidak punya NIM
                }

                // Kosongkan password agar tidak terekspos
                txtPassword.Clear();
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Nama dan Email tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Format email tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                // PERBAIKAN: Gunakan setter method (SetNama, SetEmail) bukan assignment property langsung.
                // Ini melewati validasi yang didefinisikan di dalam kelas User (enkapsulasi).
                _currentUser.SetNama(txtNama.Text.Trim());
                _currentUser.SetEmail(txtEmail.Text.Trim());

                // Jika user mengisi password baru
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    _currentUser.SetPassword(txtPassword.Text);
                }

                // TODO: Hubungkan ke UserController untuk menyimpan ke database
                // var (sukses, pesan) = _userController.UpdateProfil(_currentUser);

                bool success = true; // Mock success sampai method controller dibuat

                if (success)
                {
                    MessageBox.Show("Profil berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan perubahan profil.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOrderException ex)
            {
                // Tangkap validasi dari setter model (misal nama terlalu pendek)
                MessageBox.Show(ex.GetPesanLengkap(), "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
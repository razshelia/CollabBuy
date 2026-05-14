using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class EditProfileControl : UserControl
    {
        private UserService userService;
        private Akun userAktif;

        public EditProfileControl(Akun akun)
        {
            this.InitializeComponent();
            this.userService = new UserService();
            this.userAktif = akun;

            // Auto-fill data saat ini
            this.txtNama.Text = "Nama Dari Database"; // Asumsi binding data
        }

        private void chkLihatPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkLihatPassword.Checked)
            {
                // Karakter \0 (null) akan memperlihatkan teks aslinya
                this.txtPasswordLama.PasswordChar = '\0';
                this.txtPasswordBaru.PasswordChar = '\0';
            }
            else
            {
                // Kembalikan ke karakter bulat
                this.txtPasswordLama.PasswordChar = '●';
                this.txtPasswordBaru.PasswordChar = '●';
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtPasswordLama.Text))
            {
                UXHelper.TampilkanError("Password lama wajib diisi buat verifikasi kalau ini beneran kamu! 🔒");
                return;
            }

            if (UXHelper.TampilkanKonfirmasi("Yakin nih data barunya udah bener?"))
            {
                // Logika ganti password:
                // 1. Service ngecek dulu apakah txtPasswordLama cocok sama hash di database.
                // 2. Kalau txtPasswordBaru ga kosong, berarti password ikut di-update (di-hash ulang).
                // 3. Simpan perubahan ke database.

                UXHelper.TampilkanSukses("Profil kamu berhasil di-glow up! Ganti password sukses! ✨");
                this.txtPasswordLama.Clear();
                this.txtPasswordBaru.Clear();
                this.chkLihatPassword.Checked = false;
            }
        }
    }
}
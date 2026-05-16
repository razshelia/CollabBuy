using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Repositories; // Wajib untuk DI

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class RegisterControl : UserControl
    {
        public event EventHandler PindahKeLogin;

        private readonly AuthService _authService;

        public RegisterControl()
        {
            InitializeComponent();

            // TAHAP 4: INJEKSI MANUAL DI UI
            _authService = new AuthService(new UserRepository());

            this.Resize += (s, e) => CenterCard();
            this.Load += (s, e) => CenterCard();
        }

        private void CenterCard()
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
            }
        }

        private void chkLihatPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkLihatPassword.Checked;
            txtKonfirmasiPassword.UseSystemPasswordChar = !chkLihatPassword.Checked;
        }

        private void txtNomorTelepon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            string telp = txtNomorTelepon.Text.Trim();
            string email = txtEmail.Text.Trim();
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string pass2 = txtKonfirmasiPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                UXHelper.TampilkanError("Semua field wajib diisi dulu ya, bestie! 📝");
                return;
            }

            if (pass != pass2)
            {
                UXHelper.TampilkanError("Password dan konfirmasinya beda nih 😖, coba samain dulu.");
                return;
            }

            if (!chkSetuju.Checked)
            {
                UXHelper.TampilkanError("Harus setuju sama syarat & ketentuannya ya, bestie! 📜");
                return;
            }

            // Panggil Service yang sudah disuntik Repository
            bool sukses = _authService.Register(nama, telp, email, user, pass);
            if (sukses)
            {
                MessageBox.Show("Akun kamu berhasil dibuat, bestie! Login sekarang ya. 🎉",
                    "CollabBuy – Yeay!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PindahKeLogin?.Invoke(this, EventArgs.Empty);
            }
        }

        private void lblLoginLink_Click(object sender, EventArgs e)
        {
            PindahKeLogin?.Invoke(this, EventArgs.Empty);
        }

        // EVENT: Menampilkan Syarat & Ketentuan
        private void lblSyaratKetentuan_Click(object sender, EventArgs e)
        {
            string teksSyarat =
                "📜 Syarat & Ketentuan CollabBuy\n\n" +
                "1. Kamu mahasiswa aktif Universitas Jember (UNEJ) yang terdaftar di database kemahasiswaan.\n" +
                "2. Akun ini hanya untuk transaksi resmi danus, bukan buat nitip doi.\n" +
                "3. Dilarang keras melakukan spam, penipuan, atau jualan jasa skripsi.\n" +
                "4. Semua transaksi yang terjadi adalah tanggung jawab masing‑masing pengguna.\n" +
                "5. Admin berhak memblokir akun yang melanggar aturan tanpa peringatan dulu.\n" +
                "6. Password jangan kasih tahu siapa‑siapa, termasuk gebetan!\n" +
                "7. Dengan mendaftar, kamu setuju buat ikut gotong royong demi solidaritas kampus. ✨";

            MessageBox.Show(teksSyarat, "CollabBuy – Syarat & Ketentuan",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static class UXHelper
        {
            public static void TampilkanError(string pesan) =>
                MessageBox.Show(pesan, "CollabBuy – Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
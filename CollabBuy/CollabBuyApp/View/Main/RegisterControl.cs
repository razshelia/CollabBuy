using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Main
{
    /// <summary>
    /// RegisterControl: UserControl untuk halaman pendaftaran akun baru.
    ///
    /// Tanggung Jawab:
    /// - Menerima input data registrasi dari user.
    /// - Mendelegasikan proses registrasi ke UserController.
    /// - Mengirim notifikasi registrasi selesai ke MainForm melalui event OnRegistrationComplete.
    ///
    /// Pola yang diterapkan:
    /// - Event-driven communication: RegisterControl tidak tahu siapa subscriber-nya.
    /// - Separation of Concerns: Validasi bisnis di Controller/Model, bukan di View.
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        // === EVENT ===

        /// <summary>
        /// Event yang di-raise saat registrasi berhasil.
        /// MainForm akan merespons dengan menampilkan kembali LoginControl.
        /// </summary>
        public event EventHandler OnRegistrationComplete;

        // === DEPENDENCY ===
        private readonly UserController _userController;

        // === KONSTRUKTOR ===
        public RegisterControl()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        // === EVENT HANDLERS ===

        /// <summary>
        /// Proses registrasi saat tombol Daftar diklik.
        ///
        /// Fix CS1061: UserController tidak punya method Register().
        /// Method yang benar adalah RegistrasiPembeli(nama, username, password)
        /// yang mengembalikan tuple (bool sukses, string pesan).
        /// </summary>
        private void btnDaftar_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string konfirmasiPassword = txtKonfirmasiPassword.Text;

            // Validasi input dasar di View (sebelum menyentuh controller)
            if (string.IsNullOrWhiteSpace(nama) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Nama, username, dan password tidak boleh kosong.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != konfirmasiPassword)
            {
                MessageBox.Show("Password dan konfirmasi password tidak sama.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKonfirmasiPassword.Clear();
                txtKonfirmasiPassword.Focus();
                return;
            }

            try
            {
                // CS1061 FIX: Gunakan RegistrasiPembeli() — method yang benar di UserController.
                // Mengembalikan tuple (bool sukses, string pesan).
                var (sukses, pesan) = _userController.RegistrasiPembeli(nama, username, password);

                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Raise event ke MainForm agar kembali ke halaman Login
                    OnRegistrationComplete?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    // Tampilkan pesan error spesifik dari controller (misal: username duplikat)
                    MessageBox.Show(pesan, "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kembali ke halaman Login tanpa mendaftar.
        /// </summary>
        private void btnBatal_Click(object sender, EventArgs e)
        {
            OnRegistrationComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
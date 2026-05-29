using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Main
{
    /// <summary>
    /// LoginControl: UserControl untuk halaman login.
    ///
    /// Tanggung Jawab:
    /// - Menerima input username dan password dari user.
    /// - Mendelegasikan autentikasi ke UserController.
    /// - Mengirim notifikasi login sukses ke MainForm melalui event OnLoginSuccess.
    ///
    /// Pola yang diterapkan:
    /// - Event-driven communication: LoginControl tidak tahu siapa yang mendengarkan,
    ///   cukup raise event. MainForm mendaftar sebagai subscriber.
    /// - Separation of Concerns: LoginControl tidak menyimpan logika bisnis,
    ///   hanya mengatur tampilan dan event.
    /// </summary>
    public partial class LoginControl : UserControl
    {
        // === EVENT ===

        /// <summary>
        /// Event yang di-raise saat login berhasil.
        /// Subscriber (MainForm) menerima objek User yang terautentikasi.
        /// </summary>
        public event Action<User> OnLoginSuccess;

        // === DEPENDENCY ===
        private readonly UserController _userController;

        // === KONSTRUKTOR ===
        public LoginControl()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        // === EVENT HANDLERS ===

        /// <summary>
        /// Proses login saat tombol Login diklik.
        /// Validasi input, panggil controller, lalu raise event jika sukses.
        ///
        /// Fix CS0029: UserController.Login() mengembalikan tuple (User user, string pesan),
        /// bukan User langsung. Destructure tuple dan tampilkan pesan dari controller.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan password tidak boleh kosong.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // CS0029 FIX: Login() mengembalikan (User user, string pesan) — destructure tuple
                var (user, pesan) = _userController.Login(username, password);

                if (user != null)
                {
                    // Raise event ke MainForm dengan objek User yang sudah terautentikasi
                    OnLoginSuccess?.Invoke(user);
                }
                else
                {
                    // Tampilkan pesan error langsung dari controller (termasuk "akun diblokir", dll.)
                    MessageBox.Show(pesan, "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Navigasi ke halaman registrasi saat tombol Daftar diklik.
        /// </summary>
        private void btnDaftar_Click(object sender, EventArgs e)
        {
            // Navigasi ditangani oleh MainForm.
            // Jika diperlukan, bisa tambahkan event OnNavigateToRegister di sini.
        }
    }
}
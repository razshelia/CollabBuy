using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class AuthService
    {
        private UserRepository userRepo;

        public AuthService()
        {
            this.userRepo = new UserRepository();
        }

        public Akun ProsesLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                UXHelper.TampilkanError("Username dan Password tidak boleh kosong!");
                return null;
            }

            Akun akunDitemukan = this.userRepo.Login(username, password);
            if (akunDitemukan == null)
            {
                // UserRepository sudah menampilkan error, tidak perlu lagi di sini
                return null;
            }

            // Login berhasil
            UXHelper.TampilkanSukses(akunDitemukan.TampilkanDashboard());
            return akunDitemukan;
        }
    }
}
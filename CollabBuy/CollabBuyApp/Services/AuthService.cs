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
            else
            {
                Akun akunDitemukan = this.userRepo.Login(username, password);

                if (akunDitemukan == null)
                {
                    UXHelper.TampilkanError("Username tidak terdaftar, Password salah, atau Akun Anda sedang diblokir.");
                    return null;
                }
                else
                {
                    // Menampilkan dashboard dengan Polymorphism
                    UXHelper.TampilkanSukses(akunDitemukan.TampilkanDashboard());
                    return akunDitemukan;
                }
            }
        }
    }
}
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService()
        {
            _userRepo = new UserRepository();
        }

        public User AmbilUserById(int idUser)
        {
            return _userRepo.AmbilUserById(idUser);
        }

        public bool UpdateProfil(User user, string passwordBaru = null)
        {
            if (!string.IsNullOrEmpty(passwordBaru))
                user.Password = PasswordHelper.HashPassword(passwordBaru);

            bool sukses = _userRepo.UpdateProfil(user);
            if (sukses)
                UXHelper.TampilkanSukses("Profil berhasil diperbarui.");
            return sukses;
        }

        public bool BlokirUser(int idUser, bool diblokir)
        {
            bool sukses = _userRepo.BlokirUser(idUser, diblokir);
            if (sukses)
                UXHelper.TampilkanSukses(diblokir ? "User diblokir." : "User diaktifkan kembali.");
            return sukses;
        }

        public List<User> AmbilSemuaUser()
        {
            return _userRepo.AmbilSemuaUser();
        }
    }
}
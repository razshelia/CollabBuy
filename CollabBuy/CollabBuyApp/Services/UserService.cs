using System;
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
            try
            {
                return _userRepo.AmbilUserById(idUser);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return null;
            }
        }

        public bool UpdateProfil(User user, string passwordBaru = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(passwordBaru))
                    user.Password = PasswordHelper.HashPassword(passwordBaru);

                bool sukses = _userRepo.UpdateProfil(user);
                if (sukses)
                    UXHelper.TampilkanSukses("Profil berhasil diperbarui.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public bool BlokirUser(int idUser, bool diblokir)
        {
            try
            {
                bool sukses = _userRepo.BlokirUser(idUser, diblokir);
                if (sukses)
                    UXHelper.TampilkanSukses(diblokir ? "User berhasil diblokir." : "User diaktifkan kembali.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<User> AmbilSemuaUser()
        {
            try
            {
                return _userRepo.AmbilSemuaUser();
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return new List<User>();
            }
        }
    }
}
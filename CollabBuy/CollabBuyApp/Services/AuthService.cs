using System;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                UXHelper.TampilkanError("Username dan password wajib diisi.");
                return null;
            }

            try
            {
                User user = _userRepo.Login(username, password);

                if (user == null)
                {
                    UXHelper.TampilkanError("Username atau password salah.");
                    return null;
                }

                if (user.IsDiblokir)
                {
                    UXHelper.TampilkanError("Akun Anda telah diblokir. Hubungi admin.");
                    return null;
                }

                UXHelper.TampilkanSukses($"Selamat datang, {user.Nama}!");
                return user;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return null;
            }
        }

        public bool Register(string nama, string nomorTelepon, string email, string username, string password)
        {
            // Validasi Input
            if (string.IsNullOrWhiteSpace(nama)) { UXHelper.TampilkanError("Nama wajib diisi."); return false; }
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) { UXHelper.TampilkanError("Email tidak valid."); return false; }
            if (string.IsNullOrWhiteSpace(username) || username.Length < 5) { UXHelper.TampilkanError("Username minimal 5 karakter."); return false; }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8) { UXHelper.TampilkanError("Password minimal 8 karakter."); return false; }

            RegularUser userBaru = new RegularUser();
            userBaru.Nama = nama;
            userBaru.NomorTelepon = nomorTelepon;
            userBaru.Email = email;
            userBaru.Username = username;
            userBaru.Password = PasswordHelper.HashPassword(password);

            try
            {
                bool sukses = _userRepo.Register(userBaru);
                if (sukses) UXHelper.TampilkanSukses("Registrasi berhasil! Silakan login.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }
    }
}
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class UserService
    {
        private IUserRepository repository;

        public UserService()
        {
            this.repository = new UserRepository();
        }

        // Method login
        public Akun Login(string username, string password)
        {
            var akun = repository.Login(username, password);
            if (akun == null)
                UXHelper.TampilkanError("Username atau password salah.");
            return akun;
        }

        // Method yang dipanggil RegisterControl
        public bool DaftarPenggunaBaru(Akun akun)
        {
            // Validasi bisa ditambahkan
            bool sukses = repository.Register(akun);
            if (sukses)
                UXHelper.TampilkanSukses("Registrasi berhasil! Silakan login.");
            else
                UXHelper.TampilkanError("Gagal mendaftar, coba lagi.");
            return sukses;
        }

        // Ajukan verifikasi seller (dipanggil dari SellerVerificationControl)
        public bool AjukanVerifikasiSeller(int idUser, string namaToko, string nim, int tahunMasuk, string pathKTM)
        {
            return repository.AjukanVerifikasiSeller(idUser, namaToko, nim, tahunMasuk, pathKTM);
        }

        // Method yang dipanggil AdminUserManagementControl untuk memuat data
        public List<dynamic> MuatDaftarPengajuanVerifikasi()
        {
            return repository.AmbilDaftarPengajuanVerifikasi();
        }

        // Method yang dipanggil AdminUserManagementControl untuk menyetujui
        public bool SetujuiPenjual(int idVerifikasi)
        {
            if (!UXHelper.TampilkanKonfirmasi("Setujui pengajuan ini?"))
                return false;
            bool sukses = repository.SetujuiVerifikasi(idVerifikasi);
            if (sukses)
                UXHelper.TampilkanSukses("Penjual telah disetujui.");
            else
                UXHelper.TampilkanError("Gagal menyetujui.");
            return sukses;
        }

        // Method tolak verifikasi (opsional)
        public bool TolakPenjual(int idVerifikasi)
        {
            if (!UXHelper.TampilkanKonfirmasi("Tolak pengajuan ini?"))
                return false;
            bool sukses = repository.TolakVerifikasi(idVerifikasi);
            if (sukses)
                UXHelper.TampilkanSukses("Pengajuan ditolak.");
            return sukses;
        }

        // Update profil
        public bool UpdateProfil(Akun akun)
        {
            return repository.UpdateProfil(akun);
        }
    }
}
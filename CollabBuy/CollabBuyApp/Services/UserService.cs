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

        public Akun Login(string username, string password)
        {
            var akun = repository.Login(username, password);
            if (akun == null)
                UXHelper.TampilkanError("Username atau password salah.");
            return akun;
        }

        public bool DaftarPenggunaBaru(Akun akun)
        {
            // Repository.Register sudah melakukan hashing, validasi, dan menampilkan pesan error
            bool sukses = repository.Register(akun);
            if (sukses)
                UXHelper.TampilkanSukses("Registrasi berhasil! Silakan login.");
            // Error sudah ditampilkan oleh repository
            return sukses;
        }

        public bool AjukanVerifikasiSeller(int idUser, string namaToko, string nim, int tahunMasuk, string pathKTM)
        {
            return repository.AjukanVerifikasiSeller(idUser, namaToko, nim, tahunMasuk, pathKTM);
        }

        public List<dynamic> MuatDaftarPengajuanVerifikasi()
        {
            return repository.AmbilDaftarPengajuanVerifikasi();
        }

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

        public bool TolakPenjual(int idVerifikasi)
        {
            if (!UXHelper.TampilkanKonfirmasi("Tolak pengajuan ini?"))
                return false;
            bool sukses = repository.TolakVerifikasi(idVerifikasi);
            if (sukses)
                UXHelper.TampilkanSukses("Pengajuan ditolak.");
            return sukses;
        }

        public bool UpdateProfil(Akun akun)
        {
            return repository.UpdateProfil(akun);
        }
    }
}
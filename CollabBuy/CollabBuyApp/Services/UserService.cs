using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class UserService
    {
        private UserRepository userRepo;

        public UserService()
        {
            this.userRepo = new UserRepository();
        }

        public bool DaftarPenggunaBaru(User penggunaBaru)
        {
            if (penggunaBaru == null)
            {
                UXHelper.TampilkanError("Data pendaftaran tidak valid.");
                return false;
            }
            else
            {
                // Panggil repository untuk INSERT
                bool sukses = this.userRepo.Register(penggunaBaru);

                if (sukses)
                {
                    UXHelper.TampilkanSukses("Akun berhasil didaftarkan! Silakan Login.");
                    return true;
                }
                else
                {
                    UXHelper.TampilkanError("Gagal mendaftar. Username atau Email mungkin sudah dipakai.");
                    return false;
                }
            }
        }

        public bool SetujuiPenjual(int idVerifikasi)
        {
            if (idVerifikasi <= 0)
            {
                UXHelper.TampilkanError("ID Verifikasi tidak ditemukan.");
                return false;
            }
            else
            {
                bool yakin = UXHelper.TampilkanKonfirmasi("Setujui mahasiswa ini sebagai Penjual?");
                if (yakin)
                {
                    bool sukses = this.userRepo.SetujuiVerifikasi(idVerifikasi);
                    if (sukses)
                    {
                        UXHelper.TampilkanSukses("Status Penjual berhasil diaktifkan!");
                        return true;
                    }
                    else
                    {
                        UXHelper.TampilkanError("Gagal menyetujui. Terjadi kesalahan pada database.");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        // Tambahkan method ini untuk menjembatani UI dan Repository
        public List<Seller> MuatDaftarPengajuanVerifikasi()
        {
            // Memanggil method dari UserRepository
            List<Seller> daftarPengajuan = this.userRepo.AmbilDaftarPengajuanVerifikasi();

            if (daftarPengajuan == null || daftarPengajuan.Count == 0)
            {
                // Jika kosong, kembalikan list kosong agar DataGridView tidak error/crash
                return new List<Seller>();
            }
            else
            {
                // Jika ada isinya, langsung lemparkan ke UI
                return daftarPengajuan;
            }
        }
    }
}
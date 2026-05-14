using CollabBuy.CollabBuyApp.Models;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IUserRepository
    {
        // Fungsi Autentikasi
        Akun Login(string username, string password);
        bool Register(User newUser);

        // Fungsi Pengelolaan Akun
        bool AjukanVerifikasiPenjual(Seller pengajuan);
        List<User> AmbilSemuaUser();
        bool BlokirAkun(int idUser);

        // Fungsi Verifikasi (Admin)
        List<Seller> AmbilDaftarPengajuanVerifikasi();
        bool SetujuiVerifikasi(int idVerifikasi);
    }
}
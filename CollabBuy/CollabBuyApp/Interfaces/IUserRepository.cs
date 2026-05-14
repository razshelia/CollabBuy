using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IUserRepository
    {
        Akun Login(string username, string password);
        bool Register(Akun akun);
        bool AjukanVerifikasiSeller(int idUser, string namaToko, string nim, int tahunMasuk, string pathKTM);
        List<dynamic> AmbilDaftarPengajuanVerifikasi();
        bool SetujuiVerifikasi(int idVerifikasi);
        bool TolakVerifikasi(int idVerifikasi);
        bool UpdateProfil(Akun akun);
    }
}
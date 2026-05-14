using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IVerificationRepository
    {
        bool AjukanVerifikasi(Verification verif);
        List<Verification> AmbilPengajuanPending();
        bool SetujuiVerifikasi(int idVerifikasi);
        bool TolakVerifikasi(int idVerifikasi);
        Verification AmbilVerifikasiByUser(int idUser);
    }
}
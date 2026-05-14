using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IComplaintRepository
    {
        bool KirimAduan(Complaint aduan);
        List<Complaint> AmbilSemuaAduan();
        List<Complaint> AmbilAduanByUser(int idUser);
        bool TandaiSelesai(int idAduan);
        bool BalasAduan(int idAduan, string balasan);
    }
}
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IPreorderRepository
    {
        bool TambahPreorder(Preorder preorder, int idProduk, int targetKuota);
        List<Preorder> AmbilPreorderAktif();
        List<Preorder> AmbilPreorderByPenjual(int idPenjual);
        Preorder AmbilPreorderById(int idPo);
        bool TutupPreorder(int idPo); // ← Pastikan ada ini untuk fitur TutupPO
    }
}
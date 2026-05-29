using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.Models.Interfaces
{
    public interface IResolvable
    {
        void BeriTanggapan(string tanggapan);
        bool IsSelesai();
        string GetTanggapan();
    }
}

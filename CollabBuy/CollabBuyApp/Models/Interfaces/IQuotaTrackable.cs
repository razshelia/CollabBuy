using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.Models.Interfaces
{
    public interface IQuotaTrackable
    {
        int GetTargetKuota();
        int GetTerpesan();
        int GetSisaKuota();
        bool IsKuotaTerpenuhi();
    }
}

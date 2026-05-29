using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.Models.Interfaces
{
    public interface IStatusTrackable
    {
        string GetStatus();
        void UbahStatus(string statusBaru);
        bool BisaDiubahKe(string statusBaru);
    }
}

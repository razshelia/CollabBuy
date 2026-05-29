using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.Models.Interfaces
{
    public interface IApprovable
    {
        void Approve();
        void Reject(string alasan);
        bool GetStatusPersetujuan();
    }
}

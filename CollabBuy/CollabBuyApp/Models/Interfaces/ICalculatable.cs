using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.Models.Interfaces
{
    public interface ICalculatable
    {
        long HitungTotal();
        long HitungDiskon();
    }
}

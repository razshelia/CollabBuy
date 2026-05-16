using CollabBuy.CollabBuyApp.Helpers;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public static class PreorderFactory
    {
        public static Preorder BuatPreorder(string jenisPo)
        {
            if (jenisPo == TipePO.GotongRoyong)
            {
                return new POGotongRoyong();
            }
            return new POBiasa();
        }
    }
}
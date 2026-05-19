namespace CollabBuy.CollabBuyApp.Helpers
{
    // Kumpulan teks baku 
    public static class TipePO
    {
        public const string Biasa = "Biasa";
        public const string GotongRoyong = "Gotong Royong";
    }
    public static class StatusTransaksi
    {
        public const string Menunggu = "Menunggu";
        public const string Diproses = "Diproses";
        public const string Selesai = "Selesai";
    }
    public enum JenisPO
    {
        Biasa,
        GotongRoyong
    }
}
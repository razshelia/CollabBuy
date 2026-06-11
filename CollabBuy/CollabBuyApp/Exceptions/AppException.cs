using System;

namespace CollabBuy.CollabBuyApp.Exceptions
{
    /// <summary>
    /// Abstract class induk untuk semua exception custom di CollabBuy.
    /// Dibuat abstract agar tidak bisa diinstansiasi langsung —
    /// setiap exception harus spesifik (InvalidOrderException, dst).
    /// </summary>
    public abstract class AppException : Exception
    {
        protected AppException(string pesan) : base(pesan) { }

        protected AppException(string pesan, Exception innerException)
            : base(pesan, innerException) { }

        public abstract string GetKategoriError();
    }
}

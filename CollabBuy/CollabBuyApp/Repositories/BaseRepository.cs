using System;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Abstract base class untuk semua Repository.
    /// Menyediakan _connectionString dari App.config di satu tempat terpusat,
    /// sehingga semua repository tidak perlu menduplikasi kode inisialisasi yang sama.
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;

        protected BaseRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString
                ?? throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
        }
    }
}
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Repositories.Interfaces
{
    /// <summary>
    /// Kontrak untuk repository yang mendukung fetch semua data sekaligus.
    /// Hanya implement di entitas yang memang wajar di-GetAll:
    /// ActivityLog, Transaction, User.
    /// JANGAN implement di Product, Review, Complaint — gunakan method spesifik.
    /// </summary>
    public interface IQueryAllRepository<T> where T : class
    {
        List<T> GetAll();
    }
}

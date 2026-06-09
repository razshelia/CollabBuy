using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Repositories.Interfaces
{
    /// <summary>
    /// Kontrak dasar untuk query satu entitas by primary key.
    /// Semua repository wajib bisa ambil satu data by ID.
    /// </summary>
    public interface IQueryRepository<T> where T : class
    {
        T GetById(int id);
    }
}
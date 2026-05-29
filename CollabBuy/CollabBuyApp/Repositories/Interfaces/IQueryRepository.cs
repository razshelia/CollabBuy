using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Repositories.Interfaces
{
    public interface IQueryRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
    }
}
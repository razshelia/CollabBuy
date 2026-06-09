using System.Threading.Tasks;

namespace CollabBuy.CollabBuyApp.Repositories.Interfaces
{
    /// <summary>
    /// Kontrak untuk operasi tulis: Insert dan Update.
    /// </summary>
    public interface ICommandRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
    }
}
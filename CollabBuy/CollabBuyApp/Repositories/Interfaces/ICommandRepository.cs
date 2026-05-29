using System.Threading.Tasks;

namespace CollabBuy.CollabBuyApp.Repositories.Interfaces
{
    public interface ICommandRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
    }
}
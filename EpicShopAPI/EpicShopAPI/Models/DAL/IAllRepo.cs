using Microsoft.Azure.Documents;
using System.Linq.Expressions;

namespace EpicShopAPI.Models.DAL
{
    public interface IAllRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task Create(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}

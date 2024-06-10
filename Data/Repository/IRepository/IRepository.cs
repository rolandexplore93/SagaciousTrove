using System.Linq.Expressions;

namespace Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // Assume T is Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

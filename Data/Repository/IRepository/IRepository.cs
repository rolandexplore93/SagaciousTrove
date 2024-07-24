using System.Linq.Expressions;

namespace Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // E.g T is Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

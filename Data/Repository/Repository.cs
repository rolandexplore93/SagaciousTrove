using Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Implement the ApplicationDbContext
        private readonly ApplicationDbContext _db;
        // Get the DbSet instance from ApplicationDbContext to perform the db operations directly
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db; // database implementation
            // From the _db above, assign it to dbSet on the generic class T. This will set the DbSet to the particular instance of the class that is calling the repository
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            // This should return IEnumerable<T> but we may want to query the data first before returnimg IEnumerable<T>. So, use IQuerable
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}

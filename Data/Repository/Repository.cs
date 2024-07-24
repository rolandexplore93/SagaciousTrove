﻿using Data.Repository.IRepository;
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
            //_db.Products.Include(u => u.Category).Include(u => u.CoverType);
            // From the _db above, assign it to dbSet on the generic class T. This will set the DbSet to the particular instance of the class that is calling the repository
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        // includeProperties - "Category,CoverType" i.e they are separated by a comma
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            // This should return IEnumerable<T> but we may want to query the data first before returnimg IEnumerable<T>. So, use IQuerable
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
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

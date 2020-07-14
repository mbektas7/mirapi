using Microsoft.EntityFrameworkCore;
using Mirapi.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mirapi.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _contextTable;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _contextTable = _context.Set<T>();
        }

        public T Get(int id)
        {
            return _contextTable.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _contextTable;
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> includeMembers)
        {
            DbSet<T> set = _contextTable;
            IQueryable<T> result = includeMembers(set);

            return result;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _contextTable.Where(predicate);
        }

        public IQueryable<T> Find(Func<IQueryable<T>, IQueryable<T>> includeMembers)
        {
            DbSet<T> set = _contextTable;
            IQueryable<T> result = includeMembers(set);

            return result;
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _contextTable.FirstOrDefault(predicate);
        }

        public T SingleOrDefault(Func<IQueryable<T>, IQueryable<T>> includeMembers)
        {
            DbSet<T> set = _contextTable;
            IQueryable<T> result = includeMembers(set);

            return result.AsEnumerable().FirstOrDefault();
        }

        public void Add(T entity)
        {
            _contextTable.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _contextTable.AddRange(entities);
        }

        public void Remove(T entity)
        {
            _contextTable.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _contextTable.RemoveRange(entities);
        }

    }
}

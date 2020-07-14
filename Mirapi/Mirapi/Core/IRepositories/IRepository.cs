using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mirapi.Core.IRepositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> includeMembers);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> Find(Func<IQueryable<T>, IQueryable<T>> includeMembers);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Func<IQueryable<T>, IQueryable<T>> includeMembers);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}

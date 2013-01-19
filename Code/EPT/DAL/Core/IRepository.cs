using System;
using System.Linq;
using System.Linq.Expressions;

namespace ETP.DAL.Core
{

    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> AllIncluding(params string[] includeProperties);
        T Find(int id);
    } 
}
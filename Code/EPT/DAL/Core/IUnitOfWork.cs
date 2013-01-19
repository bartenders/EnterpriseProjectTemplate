using System;
using System.Data.Entity;

namespace ETP.DAL.Core
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        int Commit();
        TContext Context { get; }
    }

}
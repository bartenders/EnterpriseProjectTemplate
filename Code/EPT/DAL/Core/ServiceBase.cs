using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace ETP.DAL.Core
{
    public class ServiceBase<C> : IDisposable
        where C : DbContext, new()
    {
        private C _dbContext;

        public virtual C DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new C();
                    this.AllowSerialization = true;
                    //Disable ProxyCreationDisabled to prevent the "In order to serialize the parameter, add the type to the known types collection for the operation using ServiceKnownTypeAttribute" error
                    _dbContext.Configuration.LazyLoadingEnabled = false;
                    _dbContext.Configuration.ProxyCreationEnabled = true;
                }
                return _dbContext;
            }
        }

        public virtual bool AllowSerialization
        {
            get
            {
                //return ((IObjectContextAdapter) _DataContext)
                //.ObjectContext.ContextOptions.ProxyCreationEnabled = false;
                return _dbContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _dbContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        public virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                using (DbContext)
                {
                    return DbContext.Set<T>().Where(predicate).SingleOrDefault();
                }
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
        }

        public virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return DbContext.Set<T>().Where(predicate);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList<T>().OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T>() where T : class
        {
            try
            {
                return DbContext.Set<T>();
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }


        public virtual IQueryable<T> All<T>() where T : class
        {
            return DbContext.Set<T>();
        }


        public virtual IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class 
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);

            }
            return query;
        }

        public IQueryable<T> AllIncluding<T>(params string[] includeProperties) where T : class 
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);

            }
            return query;
        }

        public T Find<T>(int id) where T : class 
        {
            return DbContext.Set<T>().Find(id);
        }


        public OperationResult Commit<T>(List<T> disconnectedEntities = null) where T : class , IObjectWithState 
        {
            OperationResult result;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted}))
            {

                if (disconnectedEntities != null)
                    foreach (var customer in disconnectedEntities)
                    {
                        if (customer.State != State.Unchanged)
                            DbContext.Set<T>().Add(customer);
                    }

                DbContext.ApplyStateChanges();
                try
                {
                    DbContext.SaveChanges();
                    result = new OperationResult { Status = true };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Optimistic Concurrency reloads data from Database.
                    if (disconnectedEntities != null)
                    {
                        foreach (var entity in disconnectedEntities)
                        {
                            if (entity.State != State.Unchanged)
                            {
                                DbContext.Entry(entity).CurrentValues.SetValues(DbContext.Entry(entity).GetDatabaseValues());
                            }
                        }
                    }
                    else
                    {
                        foreach (var entity in DbContext.Set<T>())
                        {
                            if (entity.State != State.Unchanged)
                            {
                                DbContext.Entry(entity).CurrentValues.SetValues(DbContext.Entry(entity).GetDatabaseValues());
                            }
                        }
                    }

                    result = OperationResult.CreateFromException("Concurrency Exception occured!", ex);
                    result.ResultType = OperationResultType.ConcurencyExeption;
                }
                finally
                {
                    DbContext.ResetEntityStates();
                    scope.Complete();
                }

            }

            return result;
        }
        
        public void Dispose()
        {
            if (DbContext != null) DbContext.Dispose();
        }
    }
}

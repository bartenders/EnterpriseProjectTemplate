using System.Data.Entity;

namespace ETP.DAL.Core
{
    public class BaseContext<TContext>
      : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }
        protected BaseContext()
            : base("name=AdventureWorks2012")
        { }
    }
}

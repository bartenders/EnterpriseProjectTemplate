using System.Data.Entity;

namespace ETP.DAL.Core
{
    public static class DbContextExtension
    {
        //Only use with short lived contexts 
        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = StateHelpers.ConvertState(stateInfo.State);
            }
        }

        public static void ResetEntityStates(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                entry.Entity.State = State.Unchanged;
            }
        }

    }
}

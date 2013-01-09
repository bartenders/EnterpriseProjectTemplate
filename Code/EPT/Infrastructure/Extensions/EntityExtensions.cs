using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;

namespace EPT.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Registers a user to a given ObjectContext
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="user">The username.</param>
        public static void RegisterContextUser(this ObjectContext context, string user)
        {
            ((EntityConnection)context.Connection).RegisterContextUser(user);
        }

        /// <summary>
        /// Registers a user to a given EntityConnection
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="user">The user.</param>
        public static void RegisterContextUser(this EntityConnection connection, string user)
        {
            connection.StateChange += (sender, args) =>
            {
                if (!args.CurrentState.Equals(ConnectionState.Open)) return;
                var command = connection.StoreConnection.CreateCommand();
                command.CommandText = "[audit].[SetCurrentUserData]";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "username";
                parameter.Value = user;
                parameter.DbType = DbType.AnsiString;

                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            };
        } 
    }
}
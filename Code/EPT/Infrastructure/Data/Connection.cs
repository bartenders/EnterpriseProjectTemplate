using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Reflection;
using EPT.Infrastructure.Extensions;

namespace EPT.Infrastructure.Data
{
    public static class Connection
    {

        /// <summary>
        /// Creates the connection string and Registers a Context User
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="model">The model.</param>
        /// <param name="sqlConnectionString">The SQL connection string.</param>
        /// <param name="user">The user to be registered by the Context.</param>
        /// <returns></returns>
        public static EntityConnection CreateConnectionString(Assembly assembly, string model,
                                                              string sqlConnectionString, string user)
        {
            var connection = CreateConnectionString(assembly, model, sqlConnectionString);
            connection.RegisterContextUser(user);
            return connection;
        }

        /// <summary>
        /// Creates the connection string.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="model">The model.</param>
        /// <param name="sqlConnectionString">The SQL connection string.</param>
        /// <returns></returns>
        public static EntityConnection CreateConnectionString(Assembly assembly, string model, string sqlConnectionString)
        {
            var entityconnectionBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlConnectionString,
                Metadata = string.Format("res://{0}/{1}.csdl|res://{0}/{1}.ssdl|res://{0}/{1}.msl", assembly.GetName().Name, model)
            };

            return new EntityConnection(entityconnectionBuilder.ToString());
        }
    }
}
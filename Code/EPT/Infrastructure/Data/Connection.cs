using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Reflection;

namespace EPT.Infrastructure.Data
{
    public static class Connection
    {
        public static EntityConnection CreateConnection(Assembly assembly, string model, string sqlConnectionString)
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
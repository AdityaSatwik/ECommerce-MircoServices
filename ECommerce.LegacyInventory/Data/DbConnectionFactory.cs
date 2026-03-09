using System.Data;
using System.Data.SqlClient;
using ECommerce.LegacyInventory.Config;

namespace ECommerce.LegacyInventory.Data
{
    /// <summary>
    /// interface for the database connection factory.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>Creates a new database connection.</summary>
        IDbConnection CreateConnection();
    }

    /// <summary>
    /// Factory for creating SQL Server connections.
    /// </summary>
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Creates a SQL connection using the singleton configuration.
        /// </summary>
        public IDbConnection CreateConnection()
        {
            // Uses Singleton config
            var connectionString = InventorySingletonConfig.Instance.ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}

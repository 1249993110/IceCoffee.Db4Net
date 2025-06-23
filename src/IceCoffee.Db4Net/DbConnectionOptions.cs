using IceCoffee.Db4Net.Core;

namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents the configuration settings for a database connection.
    /// </summary>
    public class DbConnectionOptions
    {
        /// <summary>
        /// Specifies the database provider type.
        /// </summary>
        public required DatabaseProvider DatabaseProvider { get; set; }

        /// <summary>
        /// Defines the connection string used to connect to the database.
        /// </summary>
        public required string ConnectionString { get; set; }
    }
}
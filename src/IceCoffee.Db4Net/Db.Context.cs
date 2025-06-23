using IceCoffee.Db4Net.Core;
using IceCoffee.Db4Net.Core.SqlAdapters;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Data;
using System.Data.Common;

namespace IceCoffee.Db4Net
{
    public static partial class Db
    {
        /// <summary>
        /// Represents the context for a single database connection, storing associated options and provider factory.
        /// </summary>
        class DbContext
        {
            /// <summary>
            /// Gets or sets the options that define the connection string and provider information.
            /// </summary>
            public required DbConnectionOptions DbConnectionOptions { get; set; }

            /// <summary>
            /// Gets or sets the provider factory used to create connections for this context.
            /// </summary>
            public required DbProviderFactory DbProviderFactory { get; set; }
        }

        /// <summary>
        /// Holds the default context used for connections when no specific database name is provided.
        /// </summary>
        private static DbContext? _defaultDbContext;

        /// <summary>
        /// Provides a mapping between database names and their respective DbContext objects.
        /// </summary>
        private static Dictionary<string, DbContext>? _dbContextMap;

        /// <summary>
        /// Gets the configuration settings used across the Db4Net library.
        /// </summary>
        internal static Db4NetSettings Settings { get; private set; } = new Db4NetSettings();

        /// <summary>
        /// Creates a new repository instance associated with the specified database name.
        /// </summary>
        /// <param name="databaseName">Optional. The name of the target database.</param>
        /// <returns>A new <see cref="Repository"/> instance.</returns>
        public static Repository CreateRepository(string databaseName = "")
        {
            return new Repository(databaseName);
        }

        /// <summary>
        /// Configures the Db4Net library with custom settings.
        /// </summary>
        /// <param name="db4NetSettings">Custom settings for Db4Net.</param>
        public static void Configure(Db4NetSettings db4NetSettings)
        {
            Settings = db4NetSettings;
            ParameterBuilder.ReuseParameters = Settings.ReuseParameters;
            ParameterBuilder.ParameterNamePrefix = Settings.ParameterNamePrefix;
        }

        /// <summary>
        /// Registers a new database context for the specified database name internally.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <param name="dbConnectionOptions">The connection options containing the connection string and provider information.</param>
        /// <returns>The newly registered <see cref="DbContext"/> instance.</returns>
        private static DbContext InternalRegister(string databaseName, DbConnectionOptions dbConnectionOptions)
        {
            var dbContext = new DbContext()
            {
                DbConnectionOptions = dbConnectionOptions,
                DbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(dbConnectionOptions.DatabaseProvider)
            };

            _dbContextMap ??= new Dictionary<string, DbContext>();
            _dbContextMap[databaseName] = dbContext;

            return dbContext;
        }

        /// <summary>
        /// Registers a database connection context for the specified database name.
        /// </summary>
        /// <param name="databaseName">The name of the database to register.</param>
        /// <param name="dbConnectionOptions">The connection options containing the connection string and provider information.</param>
        /// <exception cref="ArgumentException">Thrown when the provided database name is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when dbConnectionOptions is null.</exception>
        public static void Register(string databaseName, DbConnectionOptions dbConnectionOptions)
        {
            if (databaseName == null)
            {
                throw new ArgumentNullException(nameof(databaseName), "Database name cannot be null.");
            }
            if (dbConnectionOptions == null)
            {
                throw new ArgumentNullException(nameof(dbConnectionOptions), "Database connection options cannot be null.");
            }

            if (databaseName == string.Empty)
            {
                Register(dbConnectionOptions);
            }
            else
            {
                InternalRegister(databaseName, dbConnectionOptions);
            }
        }

        /// <summary>
        /// Registers the default database connection context using the provided connection options.
        /// This default context will be used when no specific database name is specified.
        /// </summary>
        /// <param name="dbConnectionOptions">The connection options containing the connection string and provider information.</param>
        public static void Register(DbConnectionOptions dbConnectionOptions)
        {
            _defaultDbContext ??= InternalRegister(string.Empty, dbConnectionOptions);
        }

        /// <summary>
        /// Registers a default database connection context with the specified provider and connection string.
        /// </summary>
        /// <param name="databaseProvider">The database provider type.</param>
        /// <param name="connectionString">The connection string for the database.</param>
        public static void Register(DatabaseProvider databaseProvider, string connectionString)
        {
            Register(new DbConnectionOptions()
            {
                DatabaseProvider = databaseProvider,
                ConnectionString = connectionString
            });
        }

        /// <summary>
        /// Registers multiple database connection contexts.
        /// </summary>
        /// <param name="dbConnectionOptionsMap">A collection mapping database names to their connection options.</param>
        public static void RegisterMany(IEnumerable<KeyValuePair<string, DbConnectionOptions>> dbConnectionOptionsMap)
        {
            foreach (var item in dbConnectionOptionsMap)
            {
                Register(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Gets the default database connection context.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the default database connection context has not been registered.
        /// </exception>
        private static DbContext DefaultDbContext
        {
            get
            {
                if (_defaultDbContext == null)
                {
                    throw new InvalidOperationException("The default database connection context has not been registered. Please register a default database connection context before creating a connection.");
                }
                return _defaultDbContext;
            }
        }

        /// <summary>
        /// Gets the mapping of database connection contexts by database name.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no database connection contexts have been registered.
        /// </exception>
        private static Dictionary<string, DbContext> DbContextMap
        {
            get
            {
                if (_dbContextMap == null)
                {
                    throw new InvalidOperationException("The database connection contexts have not been initialized. Please register at least one database connection context before creating a connection.");
                }
                return _dbContextMap;
            }
        }

        /// <summary>
        /// Creates a new database connection using the named context or the default context.
        /// </summary>
        /// <param name="databaseName">Optional. The name of the target database. If empty, the default context is used.</param>
        /// <returns>A new <see cref="DbConnection"/> instance.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the connection context is not initialized or if the specified context is not found.
        /// </exception>
        public static DbConnection CreateDbConnection(string databaseName = "")
        {
            if (databaseName == string.Empty)
            {
                return InternalCreateDbConnection(DefaultDbContext);
            }

            if (DbContextMap.TryGetValue(databaseName, out var dbConnectionContext) == false)
            {
                throw new InvalidOperationException($"The database connection context for database name: '{databaseName}' was not found. Please ensure that the database is registered correctly.");
            }

            return InternalCreateDbConnection(dbConnectionContext);
        }

        /// <summary>
        /// Creates an SQL adapter implementation based on the provided or default database context.
        /// </summary>
        /// <param name="databaseName">Optional. The name of the target database. If empty, the default context is used.</param>
        /// <returns>A new <see cref="ISqlAdapter"/> instance.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the connection context is not initialized or if the specified context is not found.
        /// </exception>
        public static ISqlAdapter CreateSqlAdapter(string databaseName = "")
        {
            if (databaseName == string.Empty)
            {
                return SqlAdapterFactory.CreateAdapter(DefaultDbContext.DbConnectionOptions.DatabaseProvider);
            }

            if (DbContextMap.TryGetValue(databaseName, out var dbConnectionContext) == false)
            {
                throw new InvalidOperationException($"The database connection context for database name: '{databaseName}' was not found. Please ensure that the database is registered correctly.");
            }

            return SqlAdapterFactory.CreateAdapter(dbConnectionContext.DbConnectionOptions.DatabaseProvider);
        }

        /// <summary>
        /// Internally creates a new <see cref="DbConnection"/> using the given context.
        /// </summary>
        /// <param name="dbConnectionContext">The context containing connection details and provider factory.</param>
        /// <returns>The newly created <see cref="DbConnection"/>.</returns>
        /// <exception cref="Exception">Thrown if creating the connection fails.</exception>
        private static DbConnection InternalCreateDbConnection(DbContext dbConnectionContext)
        {
            var dbConnection = dbConnectionContext.DbProviderFactory.CreateConnection();
            if (dbConnection == null)
            {
                throw new Exception("Create connection by db provider factory failed.");
            }
            dbConnection.ConnectionString = dbConnectionContext.DbConnectionOptions.ConnectionString;

            return dbConnection;
        }
    }
}
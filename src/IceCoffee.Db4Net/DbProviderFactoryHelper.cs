using IceCoffee.Db4Net.Core;
using System.Data.Common;
using System.Reflection;

namespace IceCoffee.Db4Net
{
    internal static class DbProviderFactoryHelper
    {
        /// <summary>
        /// Try to load an assembly into the application's app domain.
        /// Loads by name first then checks for filename
        /// </summary>
        /// <param name="assemblyName">Assembly name. (without filename extension)</param>
        /// <returns>null on failure</returns>
        private static Assembly LoadAssembly(string assemblyName)
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load(new AssemblyName(assemblyName));
                return assembly;
            }
            catch { }

            string path = Path.Combine(AppContext.BaseDirectory, assemblyName + ".dll");
            assembly = Assembly.LoadFrom(path);
            return assembly;
        }

        private static DbProviderFactory GetDbProviderFactory(string assemblyName, string @namespace)
        {
            try
            {
#pragma warning disable CS8600,CS8602,CS8603
                return (DbProviderFactory)LoadAssembly(assemblyName)
                        .GetType(@namespace)
                        .GetField("Instance", BindingFlags.Static | BindingFlags.Public)
                        .GetValue(null);
#pragma warning restore
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to retrieve DbProviderFactory form assembly name: {assemblyName}, namespace: {@namespace}.", ex);
            }
        }

        public static DbProviderFactory GetDbProviderFactory(DatabaseProvider databaseProvider)
        {
            switch (databaseProvider)
            {
                case DatabaseProvider.SQLServer:
                    return GetDbProviderFactory("Microsoft.Data.SqlClient", "Microsoft.Data.SqlClient.SqlClientFactory");

                case DatabaseProvider.SQLite:
                    return GetDbProviderFactory("Microsoft.Data.Sqlite", "Microsoft.Data.Sqlite.SqliteFactory");

                case DatabaseProvider.PostgreSQL:
                    return GetDbProviderFactory("Npgsql", "Npgsql.NpgsqlFactory");

                case DatabaseProvider.MySQL:
                    return GetDbProviderFactory("MySql.Data", "MySql.Data.MySqlClient.MySqlClientFactory");

                case DatabaseProvider.DaMeng:
                    return GetDbProviderFactory("DM.DmProvider", "Dm.DmClientFactory");

                case DatabaseProvider.Undefined:
                default:
                    throw new NotSupportedException($"The database provider '{databaseProvider}' is not supported. Please specify a valid database provider.");
            }
        }
    }
}
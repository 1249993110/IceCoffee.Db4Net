using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IceCoffee.Db4Net.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering database connections and repositories.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures and registers a database connection using the specified configuration action.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository that derives from <see cref="Repository"/>.</typeparam>
        /// <param name="services">The service collection to add the database connection to.</param>
        /// <param name="configure">An action to configure the <see cref="DbConnectionOptions"/>.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbConnection<TRepository>(this IServiceCollection services, Action<DbConnectionOptions> configure) where TRepository : Repository
        {
            return services.AddDbConnection<TRepository>(string.Empty, configure);
        }

        /// <summary>
        /// Configures and registers a database connection with a specified database name using the provided configuration action.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository that derives from <see cref="Repository"/>.</typeparam>
        /// <param name="services">The service collection to add the database connection to.</param>
        /// <param name="databaseName">The name of the database.</param>
        /// <param name="configure">An action to configure the <see cref="DbConnectionOptions"/>.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbConnection<TRepository>(this IServiceCollection services, string databaseName, Action<DbConnectionOptions> configure) where TRepository : Repository
        {
            services.AddSingleton((sp) => ActivatorUtilities.CreateInstance<TRepository>(sp, new object[] { databaseName }));
            services.AddOptions<DbConnectionOptions>(databaseName)
                .Configure(configure)
                .PostConfigure<TRepository>((options, resp) =>
                {
                    Db.Register(resp.DatabaseName, options);
                });
            return services;
        }

        /// <summary>
        /// Configures and registers a database connection using an <see cref="IConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository that derives from <see cref="Repository"/>.</typeparam>
        /// <param name="services">The service collection to add the database connection to.</param>
        /// <param name="configuration">The configuration instance used to bind to <see cref="DbConnectionOptions"/>.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbConnection<TRepository>(this IServiceCollection services, IConfiguration configuration) where TRepository : Repository
        {
            return services.AddDbConnection<TRepository>(string.Empty, configuration);
        }

        /// <summary>
        /// Configures and registers a database connection with a specified database name using an <see cref="IConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository that derives from <see cref="Repository"/>.</typeparam>
        /// <param name="services">The service collection to add the database connection to.</param>
        /// <param name="databaseName">The name of the database.</param>
        /// <param name="configuration">The configuration instance used to bind to <see cref="DbConnectionOptions"/>.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbConnection<TRepository>(this IServiceCollection services, string databaseName, IConfiguration configuration) where TRepository : Repository
        {
            services.AddSingleton((sp) => ActivatorUtilities.CreateInstance<TRepository>(sp, new object[] { databaseName }));
            services.AddOptions<DbConnectionOptions>(databaseName)
                .Bind(configuration)
                .PostConfigure<TRepository>((options, resp) =>
                {
                    Db.Register(resp.DatabaseName, options);
                });
            return services;
        }

        /// <summary>
        /// Configures and registers a database connection using a configuration section path.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository that derives from <see cref="Repository"/>.</typeparam>
        /// <param name="services">The service collection to add the database connection to.</param>
        /// <param name="configurationSectionPath">The configuration section path used to bind to <see cref="DbConnectionOptions"/>.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbConnection<TRepository>(this IServiceCollection services, string configurationSectionPath) where TRepository : Repository
        {
            return services.AddDbConnection<TRepository>(string.Empty, configurationSectionPath);
        }

        /// <summary>
        /// Configures and registers a database connection with a specified database name using a configuration section path.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository that derives from <see cref="Repository"/>.</typeparam>
        /// <param name="services">The service collection to add the database connection to.</param>
        /// <param name="databaseName">The name of the database.</param>
        /// <param name="configurationSectionPath">The configuration section path used to bind to <see cref="DbConnectionOptions"/>.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbConnection<TRepository>(this IServiceCollection services, string databaseName, string configurationSectionPath) where TRepository : Repository
        {
            services.AddSingleton((sp) => ActivatorUtilities.CreateInstance<TRepository>(sp, new object[] { databaseName }));
            services.AddOptions<DbConnectionOptions>(databaseName)
                .BindConfiguration(configurationSectionPath)
                .PostConfigure<TRepository>((options, resp) =>
                {
                    Db.Register(resp.DatabaseName, options);
                });
            return services;
        }
    }
}

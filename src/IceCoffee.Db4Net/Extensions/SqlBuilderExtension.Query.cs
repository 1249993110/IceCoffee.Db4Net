using Dapper;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Data;
using static Dapper.SqlMapper;

namespace IceCoffee.Db4Net.Extensions
{
    /// <summary>
    /// Provides extension methods for SQL builders.
    /// </summary>
    public static partial class SqlBuilderExtension
    {
        #region SqlQueryBuilder

        #region GetFirst
        /// <summary>
        /// Executes the SQL query and returns the first record of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The first record of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static T GetFirst<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QueryFirst<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns the first record of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first record of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<T> GetFirstAsync<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryFirstAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query and returns the first record of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The first record of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static TEntity GetFirst<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QueryFirst<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns the first record of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first record of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<TEntity> GetFirstAsync<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryFirstAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region GetFirstOrDefault
        /// <summary>
        /// Executes the SQL query and returns the first record of type T or default if not found.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The first record of type T or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static T? GetFirstOrDefault<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QueryFirstOrDefault<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns the first record of type T or default if not found.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first record of type T or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<T?> GetFirstOrDefaultAsync<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryFirstOrDefaultAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query and returns the first record of type TEntity or default if not found.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The first record of type TEntity or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static TEntity? GetFirstOrDefault<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QueryFirstOrDefault<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns the first record of type TEntity or default if not found.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first record of type TEntity or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryFirstOrDefaultAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region GetSingle
        /// <summary>
        /// Executes the SQL query and returns a single record of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single record of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static T GetSingle<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QuerySingle<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns a single record of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a single record of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<T> GetSingleAsync<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QuerySingleAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query and returns a single record of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single record of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static TEntity GetSingle<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QuerySingle<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns a single record of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a single record of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<TEntity> GetSingleAsync<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QuerySingleAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region GetSingleOrDefault
        /// <summary>
        /// Executes the SQL query and returns a single record of type T or default if not found.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single record of type T or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static T? GetSingleOrDefault<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QuerySingleOrDefault<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns a single record of type T or default if not found.
        /// </summary>
        /// <typeparam name="T">The type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a single record of type T or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<T?> GetSingleOrDefaultAsync<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QuerySingleOrDefaultAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query and returns a single record of type TEntity or default if not found.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single record of type TEntity or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static TEntity? GetSingleOrDefault<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.QuerySingleOrDefault<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns a single record of type TEntity or default if not found.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned record.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a single record of type TEntity or default if not found.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QuerySingleOrDefaultAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region GetList
        /// <summary>
        /// Executes the SQL query and returns a list of records of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static IEnumerable<T> GetList<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Query<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns a list of records of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this SqlQueryBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query and returns a list of records of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static IEnumerable<TEntity> GetList<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Query<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the SQL query and returns a list of records of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<IEnumerable<TEntity>> GetListAsync<TEntity>(this SqlQueryBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #endregion

        #region SqlQueryPagedBuilder
        /// <summary>
        /// Executes the paged SQL query and returns a list of records of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static IEnumerable<T> GetList<T>(this SqlQueryPagedBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Query<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the paged SQL query and returns a list of records of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this SqlQueryPagedBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the paged SQL query and returns a list of records of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static IEnumerable<TEntity> GetList<TEntity>(this SqlQueryPagedBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Query<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the paged SQL query and returns a list of records of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<IEnumerable<TEntity>> GetListAsync<TEntity>(this SqlQueryPagedBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the paged SQL query and returns the paged result including records and total count.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A paged result containing the total count and a collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static PagedResult<T> GetPagedResult<T>(this SqlQueryPagedBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql + sqlResult.AttachedSql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                using var multi = dbConnection.QueryMultiple(commandDefinition);
                var items = multi.Read<T>();
                long total = multi.ReadFirstOrDefault<long>();
                return new PagedResult<T>() { Total = total, Items = items };
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the paged SQL query and returns the paged result including records and total count.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a paged result with total count and a collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<PagedResult<T>> GetPagedResultAsync<T>(this SqlQueryPagedBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql + sqlResult.AttachedSql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                using var multi = dbConnection.QueryMultiple(commandDefinition);
                var items = await multi.ReadAsync<T>();
                long total = await multi.ReadFirstOrDefaultAsync<long>();
                return new PagedResult<T>() { Total = total, Items = items };
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the paged SQL query and returns the paged result including records and total count.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A paged result containing the total count and a collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static PagedResult<TEntity> GetPagedResult<TEntity>(this SqlQueryPagedBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql + sqlResult.AttachedSql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                using var multi = dbConnection.QueryMultiple(commandDefinition);
                var items = multi.Read<TEntity>();
                long total = multi.ReadFirstOrDefault<long>();
                return new PagedResult<TEntity>() { Total = total, Items = items };
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the paged SQL query and returns the paged result including records and total count.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The paged SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a paged result with total count and a collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<PagedResult<TEntity>> GetPagedResultAsync<TEntity>(this SqlQueryPagedBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql + sqlResult.AttachedSql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                using var multi = dbConnection.QueryMultiple(commandDefinition);
                var items = await multi.ReadAsync<TEntity>();
                long total = await multi.ReadFirstOrDefaultAsync<long>();
                return new PagedResult<TEntity>() { Total = total, Items = items };
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region SqlQueryExistsBuilder
        /// <summary>
        /// Executes the exists SQL query and returns a boolean indicating if any record exists.
        /// </summary>
        /// <param name="sqlBuilder">The exists SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query result is buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if a record exists; otherwise, false.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static bool Get(this SqlQueryExistsBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteScalar<bool>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the exists SQL query and returns a boolean indicating if any record exists.
        /// </summary>
        /// <param name="sqlBuilder">The exists SQL query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query result is buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if a record exists; otherwise, false.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<bool> GetAsync(this SqlQueryExistsBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.ExecuteScalarAsync<bool>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region SqlQueryStoredProcedureBuilder
        /// <summary>
        /// Executes the stored procedure query and returns a list of records of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The stored procedure query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static IEnumerable<T> GetList<T>(this SqlQueryStoredProcedureBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.StoredProcedure, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Query<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the stored procedure query and returns a list of records of type T.
        /// </summary>
        /// <typeparam name="T">The type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The stored procedure query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of records of type T.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this SqlQueryStoredProcedureBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.StoredProcedure, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryAsync<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the stored procedure query and returns a list of records of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The stored procedure query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static IEnumerable<TEntity> GetList<TEntity>(this SqlQueryStoredProcedureBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.StoredProcedure, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Query<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the stored procedure query and returns a list of records of type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the returned records.</typeparam>
        /// <param name="sqlBuilder">The stored procedure query builder.</param>
        /// <param name="transaction">The optional database transaction.</param>
        /// <param name="buffered">Determines whether the query results are buffered.</param>
        /// <param name="commandTimeout">The optional command timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of records of type TEntity.</returns>
        /// <exception cref="SqlExecuteException">Thrown when SQL execution fails.</exception>
        public static async Task<IEnumerable<TEntity>> GetListAsync<TEntity>(this SqlQueryStoredProcedureBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.StoredProcedure, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.QueryAsync<TEntity>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion
    }
}                                                                                                                                                           

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

        /// <summary>
        /// Executes the SQL query built by the <see cref="SqlQueryExistsBuilder{TEntity}"/> and returns a boolean
        /// indicating whether the query result satisfies the condition.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlQueryExistsBuilder{TEntity}"/> instance containing the SQL query to execute.</param>
        /// <param name="transaction">An optional <see cref="IDbTransaction"/> to associate with the query execution. If null, a new database
        /// connection is created.</param>
        /// <param name="buffered">A value indicating whether the query results should be buffered. If <see langword="true"/>, results are
        /// fully loaded into memory; otherwise, results are streamed.</param>
        /// <param name="commandTimeout">An optional timeout value, in seconds, for the command execution. If null, the default timeout is used.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns><see langword="true"/> if the query result satisfies the condition; otherwise, <see langword="false"/>.</returns>
        public static bool Get<TEntity>(this SqlQueryExistsBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
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
        /// Executes the SQL query built by the <see cref="SqlQueryExistsBuilder{TEntity}"/> and determines whether the
        /// query returns a result.
        /// </summary>
        /// <remarks>This method uses the SQL query defined by the <paramref name="sqlBuilder"/> to check
        /// for the existence of a result. If a transaction is provided, the query will execute within the context of
        /// that transaction.</remarks>
        /// <typeparam name="TEntity">The type of the entity associated with the SQL query.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlQueryExistsBuilder{TEntity}"/> instance used to construct the SQL query.</param>
        /// <param name="transaction">An optional <see cref="IDbTransaction"/> to associate with the query execution. If null, the query will
        /// execute without a transaction.</param>
        /// <param name="buffered">Indicates whether the query results should be buffered. Set to <see langword="true"/> to enable buffering;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="commandTimeout">An optional timeout value, in seconds, for the command execution. If null, the default timeout is used.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns><see langword="true"/> if the query returns a result; otherwise, <see langword="false"/>.</returns>
        public static async Task<bool> GetAsync<TEntity>(this SqlQueryExistsBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
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

        #region SqlQueryCountBuilder
        /// <summary>
        /// Executes the SQL query represented by the <see cref="SqlQueryCountBuilder{TEntity}"/> instance and returns
        /// the result as a <see langword="long"/>.
        /// </summary>
        /// <remarks>This method executes the SQL query defined in the <paramref name="sqlBuilder"/> and
        /// retrieves the result using the provided database connection. If a transaction is specified, the query is
        /// executed within the context of that transaction.</remarks>
        /// <typeparam name="TEntity">The type of the entity associated with the SQL query.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlQueryCountBuilder{TEntity}"/> instance containing the SQL query to execute.</param>
        /// <param name="transaction">An optional <see cref="IDbTransaction"/> to associate with the query execution. If <see langword="null"/>,
        /// no transaction is used.</param>
        /// <param name="commandTimeout">An optional timeout value, in seconds, for the command execution. If <see langword="null"/>, the default
        /// timeout is used.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete. Defaults to <see
        /// cref="CancellationToken.None"/>.</param>
        /// <returns>The result of the SQL query execution as a <see langword="long"/>.</returns>
        public static long Get<TEntity>(this SqlQueryCountBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteScalar<long>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query represented by the <see cref="SqlQueryCountBuilder{TEntity}"/> instance and retrieves
        /// the resulting count as a <see cref="long"/> value.
        /// </summary>
        /// <remarks>This method uses the SQL query and parameters defined in the <paramref
        /// name="sqlBuilder"/> to execute a scalar query that returns a count. If a transaction is provided, the query
        /// is executed within the context of that transaction; otherwise, a new database connection is
        /// created.</remarks>
        /// <typeparam name="TEntity">The type of the entity associated with the SQL query.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlQueryCountBuilder{TEntity}"/> instance containing the SQL query and its parameters.</param>
        /// <param name="transaction">An optional <see cref="IDbTransaction"/> to associate with the query execution. If null, a new database
        /// connection is created.</param>
        /// <param name="commandTimeout">An optional timeout value, in seconds, for the command execution. If null, the default timeout is used.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>The count resulting from the execution of the SQL query as a <see cref="long"/> value.</returns>
        public static async Task<long> GetAsync<TEntity>(this SqlQueryCountBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.ExecuteScalarAsync<long>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query defined by the <paramref name="sqlBuilder"/> and retrieves a single scalar value of
        /// type <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>This method uses the SQL query defined by the <paramref name="sqlBuilder"/> to
        /// execute a scalar query. If a <paramref name="transaction"/> is provided, the query will execute within the
        /// scope of that transaction. Otherwise, a new database connection is created based on the <paramref
        /// name="sqlBuilder"/> configuration.</remarks>
        /// <typeparam name="T">The type of the scalar value to retrieve.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder that defines the query to execute.</param>
        /// <param name="transaction">An optional database transaction to associate with the query execution. If <see langword="null"/>, the query
        /// will execute without a transaction.</param>
        /// <param name="commandTimeout">An optional timeout value, in seconds, for the command execution. If <see langword="null"/>, the default
        /// timeout is used.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The scalar value of type <typeparamref name="T"/> retrieved from the query execution, or <see
        /// langword="null"/> if the query result is empty.</returns>
        public static T? Get<T>(this ISqlQueryCountBuilder sqlBuilder, IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteScalar<T>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the SQL query defined by the <paramref name="sqlBuilder"/> and retrieves a single value of type
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>This method uses the SQL query defined by the <paramref name="sqlBuilder"/> to
        /// execute a scalar query. If <paramref name="transaction"/> is provided, the query is executed within the
        /// scope of the transaction. Otherwise, a new database connection is created for the query execution.</remarks>
        /// <typeparam name="T">The type of the value to retrieve from the query result.</typeparam>
        /// <param name="sqlBuilder">The SQL query builder that defines the query to execute.</param>
        /// <param name="transaction">An optional database transaction to associate with the query execution. If null, a new connection is
        /// created.</param>
        /// <param name="commandTimeout">An optional timeout value, in seconds, for the query execution. If null, the default timeout is used.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the value of type <typeparamref
        /// name="T"/> retrieved from the query. If no value is found, the result is <see langword="null"/>.</returns>
        public static async Task<T?> GetAsync<T>(this ISqlQueryCountBuilder sqlBuilder, IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return await dbConnection.ExecuteScalarAsync<T>(commandDefinition);
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

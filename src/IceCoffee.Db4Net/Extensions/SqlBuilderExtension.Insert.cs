using Dapper;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Data;
using static Dapper.SqlMapper;

namespace IceCoffee.Db4Net.Extensions
{
    public static partial class SqlBuilderExtension
    {
        #region SqlInsertBuilder

        /// <summary>
        /// Executes the INSERT command built by the specified <see cref="SqlInsertBuilder{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute<TEntity>(this SqlInsertBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Execute(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the INSERT command built by the specified <see cref="SqlInsertBuilder{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with the number of rows affected.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlInsertBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteAsync(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Executes the INSERT command built by the specified <see cref="SqlInsertEntityBuilder{TEntity}"/>, typically using entity objects.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertEntityBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute<TEntity>(this SqlInsertEntityBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Execute(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the INSERT command built by the specified <see cref="SqlInsertEntityBuilder{TEntity}"/>, typically using entity objects.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertEntityBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with the number of rows affected.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlInsertEntityBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteAsync(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region SqlInsertAndGetIdBuilder

        /// <summary>
        /// Executes the INSERT command and returns the newly inserted identity value of type <typeparamref name="TId"/>.
        /// </summary>
        /// <typeparam name="TId">The type of the identity value to return.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="ISqlInsertAndGetIdBuilder"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The identity value of the inserted record.</returns>
        public static TId? Execute<TId>(this ISqlInsertAndGetIdBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteScalar<TId>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the INSERT command and returns the newly inserted identity value of type <typeparamref name="TId"/>.
        /// </summary>
        /// <typeparam name="TId">The type of the identity value to return.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="ISqlInsertAndGetIdBuilder"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with the new identity value.</returns>
        public static Task<TId?> ExecuteAsync<TId>(this ISqlInsertAndGetIdBuilder sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteScalarAsync<TId>(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region SqlInsertOrIgnoreBuilder

        /// <summary>
        /// Executes the INSERT OR IGNORE command built by the specified <see cref="SqlInsertOrIgnoreBuilder{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertOrIgnoreBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute<TEntity>(this SqlInsertOrIgnoreBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Execute(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the INSERT OR IGNORE command built by the specified <see cref="SqlInsertOrIgnoreBuilder{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertOrIgnoreBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with the number of rows affected.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlInsertOrIgnoreBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteAsync(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion

        #region SqlInsertOrReplaceBuilder

        /// <summary>
        /// Executes the INSERT OR REPLACE command built by the specified <see cref="SqlInsertOrReplaceBuilder{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertOrReplaceBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute<TEntity>(this SqlInsertOrReplaceBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.Execute(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }

        /// <summary>
        /// Asynchronously executes the INSERT OR REPLACE command built by the specified <see cref="SqlInsertOrReplaceBuilder{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the insert operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlInsertOrReplaceBuilder{TEntity}"/>.</param>
        /// <param name="transaction">An optional database transaction.</param>
        /// <param name="buffered">Specifies whether results are buffered.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with the number of rows affected.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlInsertOrReplaceBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var sqlResult = sqlBuilder.SqlResult;
            try
            {
                var commandDefinition = new CommandDefinition(sqlResult.Sql, sqlResult.GetParameters(), transaction, commandTimeout, CommandType.Text, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
                var dbConnection = transaction?.Connection ?? Db.CreateDbConnection(sqlBuilder.DatabaseName);
                return dbConnection.ExecuteAsync(commandDefinition);
            }
            catch (Exception ex)
            {
                throw SqlExecuteExceptionFactory.Create(ex, sqlResult.Sql, Db.Settings.SqlCaptureMode);
            }
        }
        #endregion
    }
}

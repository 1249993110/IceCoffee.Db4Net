using Dapper;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Data;

namespace IceCoffee.Db4Net.Extensions
{
    public static partial class SqlBuilderExtension
    {
        /// <summary>
        /// Executes a DELETE statement based on the specified <see cref="SqlDeleteBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the DELETE operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlDeleteBuilder{TEntity}"/> containing the delete command.</param>
        /// <param name="transaction">The optional database transaction to enlist in.</param>
        /// <param name="buffered">Indicates whether the query results should be buffered.</param>
        /// <param name="commandTimeout">An optional timeout in seconds for the command.</param>
        /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
        /// <returns>The number of rows affected by the DELETE operation.</returns>
        public static int Execute<TEntity>(this SqlDeleteBuilder<TEntity> sqlBuilder,
            IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null,
            CancellationToken cancellationToken = default)
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
        /// Asynchronously executes a DELETE statement based on the specified <see cref="SqlDeleteBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the DELETE operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlDeleteBuilder{TEntity}"/> containing the delete command.</param>
        /// <param name="transaction">The optional database transaction to enlist in.</param>
        /// <param name="buffered">Indicates whether the query results should be buffered.</param>
        /// <param name="commandTimeout">An optional timeout in seconds for the command.</param>
        /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation and returns the number of rows affected by the DELETE operation.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlDeleteBuilder<TEntity> sqlBuilder,
            IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null,
            CancellationToken cancellationToken = default)
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
        /// Executes a DELETE statement using the specified <see cref="SqlDeleteEntityBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the DELETE operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlDeleteEntityBuilder{TEntity}"/> containing the delete command.</param>
        /// <param name="transaction">The optional database transaction to enlist in.</param>
        /// <param name="buffered">Indicates whether the query results should be buffered.</param>
        /// <param name="commandTimeout">An optional timeout in seconds for the command.</param>
        /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
        /// <returns>The number of rows affected by the DELETE operation.</returns>
        public static int Execute<TEntity>(this SqlDeleteEntityBuilder<TEntity> sqlBuilder,
            IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null,
            CancellationToken cancellationToken = default)
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
        /// Asynchronously executes a DELETE statement using the specified <see cref="SqlDeleteEntityBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The entity type for the DELETE operation.</typeparam>
        /// <param name="sqlBuilder">An instance of <see cref="SqlDeleteEntityBuilder{TEntity}"/> containing the delete command.</param>
        /// <param name="transaction">The optional database transaction to enlist in.</param>
        /// <param name="buffered">Indicates whether the query results should be buffered.</param>
        /// <param name="commandTimeout">An optional timeout in seconds for the command.</param>
        /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation and returns the number of rows affected by the DELETE operation.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlDeleteEntityBuilder<TEntity> sqlBuilder,
            IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null,
            CancellationToken cancellationToken = default)
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
    }
}

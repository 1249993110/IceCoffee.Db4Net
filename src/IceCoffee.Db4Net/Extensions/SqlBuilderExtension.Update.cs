using Dapper;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Data;

namespace IceCoffee.Db4Net.Extensions
{
    public static partial class SqlBuilderExtension
    {
        /// <summary>
        /// Executes an update command based on the provided <see cref="SqlUpdateBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be updated.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlUpdateBuilder{TEntity}"/> instance that defines the update operation.</param>
        /// <param name="transaction">The database transaction to use for this command. (Optional)</param>
        /// <param name="buffered">A value indicating whether the command should be buffered. (Optional, defaults to true)</param>
        /// <param name="commandTimeout">The time in seconds to wait for the command to execute. (Optional)</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. (Optional)</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute<TEntity>(this SqlUpdateBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
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
        /// Asynchronously executes an update command based on the provided <see cref="SqlUpdateBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be updated.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlUpdateBuilder{TEntity}"/> instance that defines the update operation.</param>
        /// <param name="transaction">The database transaction to use for this command. (Optional)</param>
        /// <param name="buffered">A value indicating whether the command should be buffered. (Optional, defaults to true)</param>
        /// <param name="commandTimeout">The time in seconds to wait for the command to execute. (Optional)</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. (Optional)</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains the number of rows affected.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlUpdateBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
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
        /// Executes an update command based on the provided <see cref="SqlUpdateEntityBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be updated.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlUpdateEntityBuilder{TEntity}"/> instance that defines the update operation.</param>
        /// <param name="transaction">The database transaction to use for this command. (Optional)</param>
        /// <param name="buffered">A value indicating whether the command should be buffered. (Optional, defaults to true)</param>
        /// <param name="commandTimeout">The time in seconds to wait for the command to execute. (Optional)</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. (Optional)</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute<TEntity>(this SqlUpdateEntityBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
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
        /// Asynchronously executes an update command based on the provided <see cref="SqlUpdateEntityBuilder{TEntity}"/> and returns the number of affected rows.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be updated.</typeparam>
        /// <param name="sqlBuilder">The <see cref="SqlUpdateEntityBuilder{TEntity}"/> instance that defines the update operation.</param>
        /// <param name="transaction">The database transaction to use for this command. (Optional)</param>
        /// <param name="buffered">A value indicating whether the command should be buffered. (Optional, defaults to true)</param>
        /// <param name="commandTimeout">The time in seconds to wait for the command to execute. (Optional)</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. (Optional)</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains the number of rows affected.</returns>
        public static Task<int> ExecuteAsync<TEntity>(this SqlUpdateEntityBuilder<TEntity> sqlBuilder, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CancellationToken cancellationToken = default)
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

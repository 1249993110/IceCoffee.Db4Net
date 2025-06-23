using Dapper;
using IceCoffee.Db4Net.Core.SqlAdapters;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Data;
using System.Data.Common;

namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents a repository for executing SQL commands and building SQL queries.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets the database name associated with this repository.
        /// </summary>
        public virtual string DatabaseName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class with the specified database name.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        public Repository(string databaseName)
        {
            DatabaseName = databaseName;
        }

        /// <summary>
        /// Creates a SQL adapter for the current database.
        /// </summary>
        /// <returns>An instance of <see cref="ISqlAdapter"/> configured for the current database.</returns>
        public ISqlAdapter CreateSqlAdapter()
        {
            return Db.CreateSqlAdapter(DatabaseName);
        }

        /// <summary>
        /// Creates a new database connection using the named context or the default context.
        /// </summary>
        /// <returns></returns>
        public DbConnection CreateDbConnection()
        {
            return Db.CreateDbConnection(DatabaseName);
        }

        /// <summary>
        /// Creates a stored procedure builder with optional parameters.
        /// </summary>
        /// <param name="procName">The name of the stored procedure.</param>
        /// <param name="param">Optional parameters for the procedure.</param>
        /// <returns>An instance of <see cref="SqlQueryStoredProcedureBuilder"/>.</returns>
        public SqlQueryStoredProcedureBuilder QueryStoredProcedure(string procName, object? param = null)
        {
            return new SqlQueryStoredProcedureBuilder(CreateSqlAdapter(), procName, param) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a stored procedure builder using <see cref="DynamicParameters"/>.
        /// </summary>
        /// <param name="procName">The name of the stored procedure.</param>
        /// <param name="dynamicParameters">Dynamic parameters for the procedure.</param>
        /// <returns>An instance of <see cref="SqlQueryStoredProcedureBuilder"/>.</returns>
        public SqlQueryStoredProcedureBuilder QueryStoredProcedure(string procName, Dapper.DynamicParameters dynamicParameters)
        {
            return new SqlQueryStoredProcedureBuilder(CreateSqlAdapter(), procName, dynamicParameters) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a stored procedure builder with optional parameters for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="procName">The name of the stored procedure.</param>
        /// <param name="param">Optional parameters for the procedure.</param>
        /// <returns>An instance of <see cref="SqlQueryStoredProcedureBuilder{TEntity}"/>.</returns>
        public SqlQueryStoredProcedureBuilder<TEntity> QueryStoredProcedure<TEntity>(string procName, object? param = null)
        {
            return new SqlQueryStoredProcedureBuilder<TEntity>(CreateSqlAdapter(), procName, param) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a stored procedure builder using <see cref="DynamicParameters"/> for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="procName">The name of the stored procedure.</param>
        /// <param name="dynamicParameters">Dynamic parameters for the procedure.</param>
        /// <returns>An instance of <see cref="SqlQueryStoredProcedureBuilder{TEntity}"/>.</returns>
        public SqlQueryStoredProcedureBuilder<TEntity> QueryStoredProcedure<TEntity>(string procName, Dapper.DynamicParameters dynamicParameters)
        {
            return new SqlQueryStoredProcedureBuilder<TEntity>(CreateSqlAdapter(), procName, dynamicParameters) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryBuilder"/> instance.
        /// </summary>
        /// <returns>A <see cref="SqlQueryBuilder"/> object.</returns>
        public SqlQueryBuilder Query()
        {
            return new SqlQueryBuilder(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryPagedBuilder"/> instance for paginated queries.
        /// </summary>
        /// <returns>A <see cref="SqlQueryPagedBuilder"/> object.</returns>
        public SqlQueryPagedBuilder QueryPaged()
        {
            return new SqlQueryPagedBuilder(CreateSqlAdapter()) { DatabaseName = DatabaseName };    ;
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryPagedBuilder"/> with specified page number and size.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of items in each page.</param>
        /// <returns>A <see cref="SqlQueryPagedBuilder"/> object.</returns>
        public SqlQueryPagedBuilder QueryPaged(int pageNumber, int pageSize)
        {
            return new SqlQueryPagedBuilder(CreateSqlAdapter()) { DatabaseName = DatabaseName }.PageNumber(pageNumber).PageSize(pageSize);
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryExistsBuilder"/> to check for existence of records.
        /// </summary>
        /// <returns>A <see cref="SqlQueryExistsBuilder"/> object.</returns>
        public SqlQueryExistsBuilder QueryExists()
        {
            return new SqlQueryExistsBuilder(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryExistsBuilder{TEntity}"/> to check for existence of records for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlQueryExistsBuilder{TEntity}"/> object.</returns>
        public SqlQueryExistsBuilder QueryExists<TEntity>()
        {
            return new SqlQueryExistsBuilder<TEntity>(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryExistsBuilder{TEntity}"/> to check for existence of a record by its id.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The record identifier.</param>
        /// <returns>A <see cref="SqlQueryExistsBuilder{TEntity}"/> object.</returns>
        public SqlQueryExistsBuilder QueryExists<TEntity>(object id)
        {
            return new SqlQueryExistsBuilder<TEntity>(CreateSqlAdapter(), id) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates an SQL query builder for counting records in the default table of an entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlQueryBuilder"/> preconfigured to count.</returns>
        public SqlQueryBuilder QueryCount<TEntity>()
        {
            return new SqlQueryBuilder(CreateSqlAdapter()) { DatabaseName = DatabaseName }
                .From(SqlBuilder<TEntity>.DefaultTableName)
                .SelectCount();
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryBuilder{TEntity}"/> for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlQueryBuilder{TEntity}"/> object.</returns>
        public SqlQueryBuilder<TEntity> Query<TEntity>()
        {
            return new SqlQueryBuilder<TEntity>(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a new <see cref="SqlQueryBuilder{TEntity}"/> querying by a specific record id.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The record identifier.</param>
        /// <returns>A <see cref="SqlQueryBuilder{TEntity}"/> object.</returns>
        public SqlQueryBuilder<TEntity> Query<TEntity>(object id)
        {
            return new SqlQueryBuilder<TEntity>(CreateSqlAdapter(), id) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a paginated query builder for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlQueryPagedBuilder{TEntity}"/> object.</returns>
        public SqlQueryPagedBuilder<TEntity> QueryPaged<TEntity>()
        {
            return new SqlQueryPagedBuilder<TEntity>(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a paginated query builder for a specific entity type with explicit page number and size.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A <see cref="SqlQueryPagedBuilder{TEntity}"/> object.</returns>
        public SqlQueryPagedBuilder<TEntity> QueryPaged<TEntity>(int pageNumber, int pageSize)
        {
            return new SqlQueryPagedBuilder<TEntity>(CreateSqlAdapter(), pageNumber, pageSize) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert new records for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlInsertBuilder{TEntity}"/> instance.</returns>
        public SqlInsertBuilder<TEntity> Insert<TEntity>()
        {
            return new SqlInsertBuilder<TEntity>(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert a single entity record.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A <see cref="SqlInsertEntityBuilder{TEntity}"/> instance.</returns>
        public SqlInsertEntityBuilder<TEntity> Insert<TEntity>(TEntity entity)
        {
            return new SqlInsertEntityBuilder<TEntity>(CreateSqlAdapter(), entity) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert multiple entity records.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="SqlInsertEntityBuilder{TEntity}"/> instance.</returns>
        public SqlInsertEntityBuilder<TEntity> InsertMany<TEntity>(IEnumerable<TEntity> entities)
        {
            return new SqlInsertEntityBuilder<TEntity>(CreateSqlAdapter(), entities) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert an entity and retrieve its generated identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A <see cref="SqlInsertAndGetIdBuilder{TEntity}"/> instance.</returns>
        public SqlInsertAndGetIdBuilder<TEntity> InsertAndGetId<TEntity>(TEntity entity)
        {
            return new SqlInsertAndGetIdBuilder<TEntity>(CreateSqlAdapter(), entity) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert an entity if it does not exist, ignoring conflicts otherwise.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A <see cref="SqlInsertOrIgnoreBuilder{TEntity}"/> instance.</returns>
        public SqlInsertOrIgnoreBuilder<TEntity> InsertOrIgnore<TEntity>(TEntity entity)
        {
            return new SqlInsertOrIgnoreBuilder<TEntity>(CreateSqlAdapter(), entity) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert multiple entities, ignoring conflicts for existing records.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="SqlInsertOrIgnoreBuilder{TEntity}"/> instance.</returns>
        public SqlInsertOrIgnoreBuilder<TEntity> InsertOrIgnoreMany<TEntity>(IEnumerable<TEntity> entities)
        {
            return new SqlInsertOrIgnoreBuilder<TEntity>(CreateSqlAdapter(), entities) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert or replace an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to insert or replace.</param>
        /// <returns>A <see cref="SqlInsertOrReplaceBuilder{TEntity}"/> instance.</returns>
        public SqlInsertOrReplaceBuilder<TEntity> InsertOrReplace<TEntity>(TEntity entity)
        {
            return new SqlInsertOrReplaceBuilder<TEntity>(CreateSqlAdapter(), entity) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to insert or replace multiple entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to insert or replace.</param>
        /// <returns>A <see cref="SqlInsertOrReplaceBuilder{TEntity}"/> instance.</returns>
        public SqlInsertOrReplaceBuilder<TEntity> InsertOrReplaceMany<TEntity>(IEnumerable<TEntity> entities)
        {
            return new SqlInsertOrReplaceBuilder<TEntity>(CreateSqlAdapter(), entities) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to update records for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlUpdateBuilder{TEntity}"/> instance.</returns>
        public SqlUpdateBuilder<TEntity> Update<TEntity>()
        {
            return new SqlUpdateBuilder<TEntity>(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to update a single entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A <see cref="SqlUpdateEntityBuilder{TEntity}"/> instance.</returns>
        public SqlUpdateEntityBuilder<TEntity> Update<TEntity>(TEntity entity)
        {
            return new SqlUpdateEntityBuilder<TEntity>(CreateSqlAdapter(), entity) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to update multiple entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to update.</param>
        /// <returns>A <see cref="SqlUpdateEntityBuilder{TEntity}"/> instance.</returns>
        public SqlUpdateEntityBuilder<TEntity> UpdateMany<TEntity>(IEnumerable<TEntity> entities)
        {
            return new SqlUpdateEntityBuilder<TEntity>(CreateSqlAdapter(), entities) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to delete records for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>A <see cref="SqlDeleteBuilder{TEntity}"/> instance.</returns>
        public SqlDeleteBuilder<TEntity> Delete<TEntity>()
        {
            return new SqlDeleteBuilder<TEntity>(CreateSqlAdapter()) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to delete a record by its identifier for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The record identifier.</param>
        /// <returns>A <see cref="SqlDeleteBuilder{TEntity}"/> instance.</returns>
        public SqlDeleteBuilder<TEntity> Delete<TEntity>(object id)
        {
            return new SqlDeleteBuilder<TEntity>(CreateSqlAdapter(), id) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to delete multiple records by their identifiers for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="ids">A collection of record identifiers.</param>
        /// <returns>A <see cref="SqlDeleteBuilder{TEntity}"/> instance.</returns>
        public SqlDeleteBuilder<TEntity> DeleteMany<TEntity>(System.Collections.IEnumerable ids)
        {
            return new SqlDeleteBuilder<TEntity>(CreateSqlAdapter(), ids) { DatabaseName = DatabaseName };
        }

        /// <summary>
        /// Creates a builder to delete a single entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A <see cref="SqlDeleteEntityBuilder{TEntity}"/> instance.</returns>
        public SqlDeleteEntityBuilder<TEntity> Delete<TEntity>(TEntity entity)
        {
            return new SqlDeleteEntityBuilder<TEntity>(CreateSqlAdapter(), entity) { DatabaseName = DatabaseName }  ;
        }

        /// <summary>
        /// Creates a builder to delete multiple entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">A collection of entities to delete.</param>
        /// <returns>A <see cref="SqlDeleteEntityBuilder{TEntity}"/> instance.</returns>
        public SqlDeleteEntityBuilder<TEntity> DeleteMany<TEntity>(IEnumerable<TEntity> entities)
        {
            return new SqlDeleteEntityBuilder<TEntity>(CreateSqlAdapter(), entities) { DatabaseName = DatabaseName };
        }
    }
}

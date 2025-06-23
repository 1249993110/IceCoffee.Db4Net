using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public interface ISqlInsertAndGetIdBuilder : ISqlBuilder
    {
    }

    public class SqlInsertAndGetIdBuilder<TEntity> : SqlBuilder<TEntity>, ISqlInsertAndGetIdBuilder
    {
        public SqlInsertAndGetIdBuilder(ISqlAdapter sqlAdapter, TEntity entity) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entity;
        }
        protected override SqlResult GetSqlResult()
        {
            (string columns, string parameters) = DefaultInsertClause;
            return new SqlResult()
            {
                Sql = SqlAdapter.InsertCommand(_tableName ?? DefaultTableName, columns, parameters) + SqlAdapter.InsertReturningIdCommand(),
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities,
            };
        }

        #region To
        private string? _tableName;
        public SqlInsertAndGetIdBuilder<TEntity> To(string table)
        {
            if (Utils.IsValidSqlIdentifier(table))
            {
                table = SqlAdapter.Quote(table);
            }
            _tableName = table;
            return this;
        }
        #endregion
    }
}

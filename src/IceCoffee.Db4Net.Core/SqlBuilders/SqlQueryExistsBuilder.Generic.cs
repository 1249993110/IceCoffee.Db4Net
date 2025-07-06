using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryExistsBuilder<TEntity> : FilterableSqlBuilder<SqlQueryExistsBuilder<TEntity>, TEntity>
    {
        public SqlQueryExistsBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
            From(DefaultTableName);
        }

        public SqlQueryExistsBuilder(ISqlAdapter sqlAdapter, object id) : this(sqlAdapter)
        {
            string idColumn = TryQuote(GetFieldNameByProperty(GetSingleUniqueKey()));
            string name = ParameterBuilder.AddNamedParam(id);
            WhereRaw($"{idColumn} = {name}");
        }

        protected override SqlResult GetSqlResult()
        {
            string sql = SqlAdapter.ExistsCommand(GetFromTarget(), GetWhereConditions());
            return new SqlResult()
            {
                Sql = sql,
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities,
            };
        }

        #region From
        private string? _tableName;
        private string GetFromTarget()
        {
            return _tableName ?? DefaultTableName;
        }
        public SqlQueryExistsBuilder<TEntity> From(string table, string? alias = null)
        {
            _tableName = alias == null ? table : $"{TryQuote(table)} AS {TryQuote(alias)}";
            return this;
        }
        #endregion
    }
}

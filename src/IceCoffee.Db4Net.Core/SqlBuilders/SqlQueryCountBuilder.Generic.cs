using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public interface ISqlQueryCountBuilder : ISqlBuilder
    {
    }
    public class SqlQueryCountBuilder<TEntity> : FilterableSqlBuilder<SqlQueryCountBuilder<TEntity>, TEntity>, ISqlQueryCountBuilder
    {
        public SqlQueryCountBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        protected override SqlResult GetSqlResult()
        {
            string sql = SqlAdapter.CountCommand(GetFromTarget(), GetWhereConditions());
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
        public SqlQueryCountBuilder<TEntity> From(string table, string? alias = null)
        {
            _tableName = alias == null ? table : $"{TryQuote(table)} AS {TryQuote(alias)}";
            return this;
        }
        #endregion
    }
}

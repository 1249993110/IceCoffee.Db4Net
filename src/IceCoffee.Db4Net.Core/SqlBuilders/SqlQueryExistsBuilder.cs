using IceCoffee.Db4Net.Core.SqlAdapters;
using IceCoffee.Db4Net.Core.SqlFragmentDefinitions;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryExistsBuilder : FilterableSqlBuilder<SqlQueryExistsBuilder>
    {
        public SqlQueryExistsBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
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
        private SqlFragmentDefinition? _from;
        private string GetFromTarget()
        {
            if (_from == null)
            {
                throw new InvalidOperationException("From target is not defined.");
            }

            return _from.Render(ParameterBuilder);
        }

        public SqlQueryExistsBuilder From(string table, string? alias = null)
        {
            if (Utils.IsValidSqlIdentifier(table))
            {
                table = SqlAdapter.Quote(table);
            }
            _from = new TableSqlFragmentDefinition(table, alias);
            return this;
        }
        public SqlQueryExistsBuilder From(FormattableString subQuery, string alias)
        {
            _from = new SubQuerySqlFragmentDefinition(subQuery, alias);
            return this;
        }
        public SqlQueryExistsBuilder From(SqlQueryBuilder subQuery, string alias)
        {
            _from = new SubQuerySqlFragmentDefinition(subQuery, alias);
            return this;
        }
        public SqlQueryExistsBuilder FromRaw(string rawSql, object? param = null)
        {
            _from = new RawSqlFragmentDefinition(rawSql, param);
            return this;
        }
        #endregion
    }
}

using IceCoffee.Db4Net.Core.SqlBuilders;

namespace IceCoffee.Db4Net.Core.SqlFragmentDefinitions
{
    internal class SubQuerySqlFragmentDefinition : SqlFragmentDefinition
    {
        private readonly object _subQuery;
        private readonly string? _alias;
        public SubQuerySqlFragmentDefinition(SqlQueryBuilder subQuery, string? alias = null)
        {
            _subQuery = subQuery;
            _alias = alias;
        }

        public SubQuerySqlFragmentDefinition(FormattableString formattableString, string? alias = null)
        {
            _subQuery = formattableString;
            _alias = alias;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            string? alias = _alias == null ? null : (Utils.IsValidSqlIdentifier(_alias) ? parameterBuilder.SqlAdapter.Quote(_alias) : _alias);

            if (_subQuery is FormattableString formattableString)
            {
                string sql = Utils.ParseFormattableString(formattableString, parameterBuilder.AddNamedParam);
                return alias == null ? sql : $"{sql} AS {alias}";
            }
            else if (_subQuery is SqlQueryBuilder sqlBuilder)
            {
                var sqlResult = sqlBuilder.SqlResult;
                string sql = sqlResult.Sql;
                if (sqlResult.NamedParameters != null)
                {
                    foreach (var item in sqlResult.NamedParameters)
                    {
                        sql = sql.Replace(parameterBuilder.SqlAdapter.Parameter(item.Key), parameterBuilder.AddNamedParam(item.Value));
                    }
                }
                if (sqlResult.DynamicParameters != null)
                {
                    parameterBuilder.AddDynamicParams(sqlResult.DynamicParameters);
                }

                return alias == null ? $"({sql})" : $"({sql}) AS {alias}";
            }
            else
            {
                throw new InvalidOperationException("Invalid subquery type.");
            }
        }
    }
}

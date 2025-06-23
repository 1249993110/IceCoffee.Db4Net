using IceCoffee.Db4Net.Core.SqlBuilders;

namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal class ExistsFilterDefinition : FilterDefinition
    {
        private readonly object _subQuery;
        public ExistsFilterDefinition(FormattableString subQuery)
        {
            _subQuery = subQuery;
        }
        public ExistsFilterDefinition(SqlQueryBuilder subQuery)
        {
            _subQuery = subQuery;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            string subQuerySql;

            if(_subQuery is FormattableString formattableString)
            {
                subQuerySql = Utils.ParseFormattableString(formattableString, parameterBuilder.AddNamedParam);
            }
            else if(_subQuery is SqlQueryBuilder sqlBuilder)
            {
                var sqlResult = sqlBuilder.SqlResult;
                subQuerySql = sqlResult.Sql;
                if (sqlResult.NamedParameters != null)
                {
                    foreach (var item in sqlResult.NamedParameters)
                    {
                        subQuerySql = subQuerySql.Replace(parameterBuilder.SqlAdapter.Parameter(item.Key), parameterBuilder.AddNamedParam(item.Value));
                    }
                }
                if (sqlResult.DynamicParameters != null)
                {
                    parameterBuilder.AddDynamicParams(sqlResult.DynamicParameters);
                }
            }
            else
            {
                throw new InvalidOperationException("SubQuery must be either FormattableString or SqlBuilder.");
            }

            return $"EXISTS ({subQuerySql})";
        }
    }
}

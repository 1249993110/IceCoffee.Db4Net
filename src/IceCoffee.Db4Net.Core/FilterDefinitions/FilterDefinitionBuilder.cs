using IceCoffee.Db4Net.Core.Constants;
using IceCoffee.Db4Net.Core.PropertyDefinitions;
using IceCoffee.Db4Net.Core.SqlBuilders;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    public class FilterDefinitionBuilder
    {
        public static FilterDefinitionBuilder Default { get; } = new FilterDefinitionBuilder();

        #region Logical Operators
        public FilterDefinition And(IEnumerable<FilterDefinition> filters)
        {
            return new AndFilterDefinition(filters);
        }
        public FilterDefinition And(params FilterDefinition[] filters)
        {
            return And((IEnumerable<FilterDefinition>)filters);
        }

        public FilterDefinition Or(IEnumerable<FilterDefinition> filters)
        {
            return new OrFilterDefinition(filters);
        }
        public FilterDefinition Or(params FilterDefinition[] filters)
        {
            return Or((IEnumerable<FilterDefinition>)filters);
        }

        public FilterDefinition Not(FilterDefinition filter)
        {
            return new NotFilterDefinition(filter);
        }
        public FilterDefinition Raw(string rawSql, object? param = null)
        {
            return new RawSqlFilterDefinition(rawSql, param);
        }
        #endregion

        #region Existence Checks with Subqueries
        public FilterDefinition Exists(FormattableString subQuery)
        {
            return new ExistsFilterDefinition(subQuery);
        }
        public FilterDefinition Exists(SqlQueryBuilder subQuery)
        {
            return new ExistsFilterDefinition(subQuery);
        }
        public FilterDefinition NotExists(FormattableString subQuery)
        {
            return !Exists(subQuery);
        }
        public FilterDefinition NotExists(SqlQueryBuilder subQuery)
        {
            return !Exists(subQuery);
        }
        #endregion
    }
}

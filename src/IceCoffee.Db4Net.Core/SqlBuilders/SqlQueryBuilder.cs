using IceCoffee.Db4Net.Core.FilterDefinitions;
using IceCoffee.Db4Net.Core.SqlAdapters;
using IceCoffee.Db4Net.Core.SqlFragmentDefinitions;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryBuilder : SqlQueryBuilderBase<SqlQueryBuilder>
    {
        public SqlQueryBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }
    }

    public abstract class SqlQueryBuilderBase<TBuilder> : FilterableSqlBuilder<TBuilder> where TBuilder : SqlQueryBuilderBase<TBuilder>
    {
        public SqlQueryBuilderBase(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        protected override SqlResult GetSqlResult()
        {
            string fromTarget = GetFromTarget() + GetJoinClause();
            string selection = GetSelection();
            string whereConditions = GetWhereConditions();
            string groupBy = GetGroupBy();
            string havingConditions = GetHavingConditions();
            string orderBy = GetOrderBy();
            string sql = SqlAdapter.QueryCommand(selection, fromTarget, whereConditions, groupBy, havingConditions, orderBy);
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
        protected string GetFromTarget()
        {
            if (_from == null)
            {
                throw new InvalidOperationException("From target is not defined.");
            }

            return _from.Render(ParameterBuilder);
        }
        public TBuilder From(string table, string? alias = null)
        {
            if (Utils.IsValidSqlIdentifier(table))
            {
                table = SqlAdapter.Quote(table);
            }
            _from = new TableSqlFragmentDefinition(table, alias);
            return (TBuilder)this;
        }
        public TBuilder From(FormattableString subQuery, string alias)
        {
            _from = new SubQuerySqlFragmentDefinition(subQuery, alias);
            return (TBuilder)this;
        }
        public TBuilder From(SqlQueryBuilder subQuery, string alias)
        {
            _from = new SubQuerySqlFragmentDefinition(subQuery, alias);
            return (TBuilder)this;
        }
        public TBuilder FromRaw(string rawSql, object? param = null)
        {
            _from = new RawSqlFragmentDefinition(rawSql, param);
            return (TBuilder)this;
        }
        #endregion

        #region Join
        private List<string>? _joinClause;
        private string GetJoinClause()
        {
            return _joinClause == null ? string.Empty : " " + string.Join(" ", _joinClause);
        }
        public TBuilder InnerJoin(string rawSql)
        {
            _joinClause ??= new List<string>();
            _joinClause.Add($"INNER JOIN {rawSql}");
            return (TBuilder)this;
        }
        public TBuilder LeftJoin(string rawSql)
        {
            _joinClause ??= new List<string>();
            _joinClause.Add($"LEFT JOIN {rawSql}");
            return (TBuilder)this;
        }
        public TBuilder RightJoin(string rawSql)
        {
            _joinClause ??= new List<string>();
            _joinClause.Add($"RIGHT JOIN {rawSql}");
            return (TBuilder)this;
        }
        public TBuilder FullJoin(string rawSql)
        {
            _joinClause ??= new List<string>();
            _joinClause.Add($"FULL JOIN {rawSql}");
            return (TBuilder)this;
        }
        #endregion

        #region Select
        private bool _isDistinct;
        private readonly List<SqlFragmentDefinition> _columns = new List<SqlFragmentDefinition>();
        private string GetSelection()
        {
            string selection = _columns.Count == 0 ? "*" : string.Join(", ", _columns.Select(i => i.Render(ParameterBuilder)));
            return _isDistinct ? "DISTINCT " + selection : selection;
        }
        public TBuilder Select(string column)
        {
            if (BraceExpansionHelper.TryExpand(column, out var expandedColumns))
            {
                foreach (var expandedColumn in expandedColumns)
                {
                    Select(expandedColumn);
                }
                return (TBuilder)this;
            }

            _columns.Add(new RawSqlFragmentDefinition(Utils.IsValidSqlIdentifier(column) ? SqlAdapter.Quote(column) : column));
            return (TBuilder)this;
        }
        public TBuilder Select(IEnumerable<string> columns)
        {
            foreach (var column in columns)
            {
                Select(column);
            }
            return (TBuilder)this;
        }
        public TBuilder Select(params string[] columns)
        {
            return Select(columns.AsEnumerable());
        }
        public TBuilder Select(FormattableString subQuery, string alias)
        {
            _columns.Add(new SubQuerySqlFragmentDefinition(subQuery, alias));
            return (TBuilder)this;
        }
        public TBuilder Select(SqlQueryBuilder subQuery, string alias)
        {
            _columns.Add(new SubQuerySqlFragmentDefinition(subQuery, alias));
            return (TBuilder)this;
        }
        public TBuilder SelectRaw(string rawSql, object? param = null)
        {
            _columns.Add(new RawSqlFragmentDefinition(rawSql, param));
            return (TBuilder)this;
        }
        public TBuilder SelectCount(string column = "*", string? alias = null)
        {
            return SelectRaw(alias == null ? $"COUNT({TryQuote(column)})" : $"COUNT({TryQuote(column)}) AS {TryQuote(alias)}");
        }
        public TBuilder SelectSum(string column, string? alias = null)
        {
            return SelectRaw(alias == null ? $"SUM({TryQuote(column)})" : $"SUM({TryQuote(column)}) AS {TryQuote(alias)}");
        }
        public TBuilder SelectAvg(string column, string? alias = null)
        {
            return SelectRaw(alias == null ? $"AVG({TryQuote(column)})" : $"AVG({TryQuote(column)}) AS {TryQuote(alias)}");
        }
        public TBuilder SelectMin(string column, string? alias = null)
        {
            return SelectRaw(alias == null ? $"MIN({TryQuote(column)})" : $"MIN({TryQuote(column)}) AS {TryQuote(alias)}");
        }
        public TBuilder SelectMax(string column, string? alias = null)
        {
            return SelectRaw(alias == null ? $"MAX({TryQuote(column)})" : $"MAX({TryQuote(column)}) AS {TryQuote(alias)}");
        }
        public TBuilder Distinct()
        {
            _isDistinct = true;
            return (TBuilder)this;
        }
        #endregion

        #region GroupBy
        private List<string>? _groupByList;
        private string GetGroupBy()
        {
            return _groupByList == null ? string.Empty : "GROUP BY " + string.Join(", ", _groupByList);
        }
        public TBuilder GroupBy(string column)
        {
            _groupByList ??= new List<string>();
            _groupByList.Add(TryQuote(column));
            return (TBuilder)this;
        }
        public TBuilder GroupBy(bool condition, string column)
        {
            if (condition) return GroupBy(column);
            return (TBuilder)this;
        }
        public TBuilder GroupBy(IEnumerable<string> columns)
        {
            foreach (var column in columns)
            {
                GroupBy(column);
            }
            return (TBuilder)this;
        }
        public TBuilder GroupBy(bool condition, IEnumerable<string> columns)
        {
            if (condition) return GroupBy(columns);
            return (TBuilder)this;
        }
        public TBuilder GroupBy(params string[] columns)
        {
            return GroupBy(columns.AsEnumerable());
        }
        public TBuilder GroupBy(bool condition, params string[] columns)
        {
            if (condition) return GroupBy(columns);
            return (TBuilder)this;
        }
        #endregion

        #region Having
        private List<FilterDefinition>? _havingConditions;
        private string GetHavingConditions()
        {
            if (_havingConditions == null)
            {
                return string.Empty;
            }

            return "HAVING " + new AndFilterDefinition(_havingConditions).Render(ParameterBuilder);
        }
        public TBuilder Having(FilterDefinition filterDefinition)
        {
            _havingConditions ??= new List<FilterDefinition>();
            _havingConditions.Add(filterDefinition);
            return (TBuilder)this;
        }
        public TBuilder Having(bool condition, FilterDefinition filterDefinition)
        {
            if (condition) Having(filterDefinition);
            return (TBuilder)this;
        }
        public TBuilder Having(FormattableString formattableString)
        {
            return Having(new FormattableStringFilterDefinition(formattableString));
        }
        public TBuilder Having(bool condition, FormattableString formattableString)
        {
            if (condition) Having(formattableString);
            return (TBuilder)this;
        }
        public TBuilder HavingOr(params FilterDefinition[] filterDefinitions)
        {
            return Having(new OrFilterDefinition(filterDefinitions));
        }
        public TBuilder HavingOr(bool condition, params FilterDefinition[] filterDefinitions)
        {
            if (condition) HavingOr(filterDefinitions);
            return (TBuilder)this;
        }
        public TBuilder HavingRaw(string rawSql, object? param = null)
        {
            return Having(new RawSqlFilterDefinition(rawSql, param));
        }
        public TBuilder HavingRaw(bool condition, string rawSql, object? param = null)
        {
            if (condition) HavingRaw(rawSql, param);
            return (TBuilder)this;
        }
        #endregion

        #region OrderBy
        private List<string>? _orderByList;
        private string GetOrderBy()
        {
            return _orderByList == null ? string.Empty : "ORDER BY " + string.Join(", ", _orderByList);
        }
        public TBuilder OrderBy(string column, bool desc = false)
        {
            if (desc) return OrderByDescending(column);

            _orderByList ??= new List<string>();
            var orderBy = Utils.IsValidSqlIdentifier(column) ? SqlAdapter.Quote(column) : column;
            _orderByList.Add(orderBy);
            return (TBuilder)this;
        }
        public TBuilder OrderBy(bool condition, string column, bool desc = false)
        {
            if (condition) return OrderBy(column, desc);

            return (TBuilder)this;
        }
        public TBuilder OrderByDescending(string column)
        {
            _orderByList ??= new List<string>();
            var orderBy = Utils.IsValidSqlIdentifier(column) ? SqlAdapter.Quote(column) : column;
            _orderByList.Add(orderBy + " DESC");
            return (TBuilder)this;
        }
        public TBuilder OrderByDescending(bool condition, string column)
        {
            if (condition) return OrderByDescending(column);
            return (TBuilder)this;
        }
        #endregion
    }
}

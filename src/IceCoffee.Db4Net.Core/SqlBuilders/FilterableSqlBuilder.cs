using IceCoffee.Db4Net.Core.FilterDefinitions;
using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public abstract class FilterableSqlBuilder<TBuilder> : SqlBuilder where TBuilder : FilterableSqlBuilder<TBuilder>
    {
        public FilterableSqlBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }


        #region Where
        private readonly List<FilterDefinition> _whereConditions = new List<FilterDefinition>();
        protected string GetWhereConditions()
        {
            if (_whereConditions.Count == 0)
            {
                return string.Empty;
            }

            return "WHERE " + new AndFilterDefinition(_whereConditions).Render(ParameterBuilder);
        }
        public TBuilder Where(FilterDefinition filterDefinition)
        {
            _whereConditions.Add(filterDefinition);
            return (TBuilder)this;
        }
        public TBuilder Where(bool condition, FilterDefinition filterDefinition)
        {
            if (condition) Where(filterDefinition);
            return (TBuilder)this;
        }
        public TBuilder Where(FormattableString formattableString)
        {
            return Where(new FormattableStringFilterDefinition(formattableString));
        }
        public TBuilder Where(bool condition, FormattableString formattableString)
        {
            if (condition) Where(formattableString);
            return (TBuilder)this;
        }
        public TBuilder WhereOr(params FilterDefinition[] filterDefinitions)
        {
            return Where(new OrFilterDefinition(filterDefinitions));
        }
        public TBuilder WhereOr(bool condition, params FilterDefinition[] filterDefinitions)
        {
            if (condition) WhereOr(filterDefinitions);
            return (TBuilder)this;
        }
        public TBuilder WhereRaw(string rawSql, object? param = null)
        {
            return Where(new RawSqlFilterDefinition(rawSql, param));
        }
        public TBuilder WhereRaw(bool condition, string rawSql, object? param = null)
        {
            if (condition) WhereRaw(rawSql, param);
            return (TBuilder)this;
        }
        public TBuilder WhereExists(FormattableString subQuery)
        {
            return Where(new ExistsFilterDefinition(subQuery));
        }
        public TBuilder WhereExists(bool condition, FormattableString subQuery)
        {
            if (condition) WhereExists(subQuery);
            return (TBuilder)this;
        }
        public TBuilder WhereExists(SqlQueryBuilder subQuery)
        {
            return Where(new ExistsFilterDefinition(subQuery));
        }
        public TBuilder WhereExists(bool condition, SqlQueryBuilder subQuery)
        {
            if (condition) WhereExists(subQuery);
            return (TBuilder)this;
        }
        public TBuilder WhereNotExists(FormattableString subQuery)
        {
            return Where(!new ExistsFilterDefinition(subQuery));
        }
        public TBuilder WhereNotExists(bool condition, FormattableString subQuery)
        {
            if (condition) WhereNotExists(subQuery);
            return (TBuilder)this;
        }
        public TBuilder WhereNotExists(SqlQueryBuilder subQuery)
        {
            return Where(!new ExistsFilterDefinition(subQuery));
        }
        public TBuilder WhereNotExists(bool condition, SqlQueryBuilder subQuery)
        {
            if (condition) WhereNotExists(subQuery);
            return (TBuilder)this;
        }
        #endregion
    }
}

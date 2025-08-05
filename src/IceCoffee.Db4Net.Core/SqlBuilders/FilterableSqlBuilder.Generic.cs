using IceCoffee.Db4Net.Core.FilterDefinitions;
using IceCoffee.Db4Net.Core.PropertyDefinitions;
using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Collections;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public abstract class FilterableSqlBuilder<TBuilder, TEntity> : SqlBuilder<TEntity> where TBuilder : FilterableSqlBuilder<TBuilder, TEntity>
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

        public TBuilder WhereEq(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Eq(prop, value));
        }
        public TBuilder WhereEq(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereEq(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereEq<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Eq(prop, value));
        }
        public TBuilder WhereEq<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereEq(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereNe(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Ne(prop, value));
        }
        public TBuilder WhereNe(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereNe(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereNe<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Ne(prop, value));
        }
        public TBuilder WhereNe<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereNe(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereGt(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Gt(prop, value));
        }
        public TBuilder WhereGt(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereGt(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereGt<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Gt(prop, value));
        }
        public TBuilder WhereGt<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereGt(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereGte(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Gte(prop, value));
        }
        public TBuilder WhereGte(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereGte(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereGte<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Gte(prop, value));
        }
        public TBuilder WhereGte<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereGte(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereLt(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Lt(prop, value));
        }
        public TBuilder WhereLt(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereLt(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereLt<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Lt(prop, value));
        }
        public TBuilder WhereLt<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereLt(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereLte(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Lte(prop, value));
        }
        public TBuilder WhereLte(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereLte(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereLte<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Lte(prop, value));
        }
        public TBuilder WhereLte<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereLte(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereLike(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.Like(prop, value));
        }
        public TBuilder WhereLike(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereLike(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereLike<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.Like(prop, value));
        }
        public TBuilder WhereLike<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereLike(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereNotLike(PropertyDefinition<TEntity> prop, object value)
        {
            return Where(Filter.NotLike(prop, value));
        }
        public TBuilder WhereNotLike(bool condition, PropertyDefinition<TEntity> prop, object value)
        {
            if (condition) WhereNotLike(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereNotLike<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return Where(Filter.NotLike(prop, value));
        }
        public TBuilder WhereNotLike<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            if (condition) WhereNotLike(prop, value);
            return (TBuilder)this;
        }
        public TBuilder WhereIn(PropertyDefinition<TEntity> prop, IEnumerable values)
        {
            return Where(Filter.In(prop, values));
        }
        public TBuilder WhereIn(bool condition, PropertyDefinition<TEntity> prop, IEnumerable values)
        {
            if (condition) WhereIn(prop, values);
            return (TBuilder)this;
        }
        public TBuilder WhereIn<TProp>(Expression<Func<TEntity, TProp>> prop, IEnumerable<TProp> values)
        {
            return Where(Filter.In(prop, values));
        }
        public TBuilder WhereIn<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, IEnumerable<TProp> values)
        {
            if (condition) WhereIn(prop, values);
            return (TBuilder)this;
        }
        public TBuilder WhereNotIn(PropertyDefinition<TEntity> prop, IEnumerable values)
        {
            return Where(Filter.NotIn(prop, values));
        }
        public TBuilder WhereNotIn(bool condition, PropertyDefinition<TEntity> prop, IEnumerable values)
        {
            if (condition) WhereNotIn(prop, values);
            return (TBuilder)this;
        }
        public TBuilder WhereNotIn<TProp>(Expression<Func<TEntity, TProp>> prop, IEnumerable<TProp> values)
        {
            return Where(Filter.NotIn(prop, values));
        }
        public TBuilder WhereNotIn<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, IEnumerable<TProp> values)
        {
            if (condition) WhereNotIn(prop, values);
            return (TBuilder)this;
        }
        public TBuilder WhereIsNull(PropertyDefinition<TEntity> prop)
        {
            return Where(Filter.IsNull(prop));
        }
        public TBuilder WhereIsNull(bool condition, PropertyDefinition<TEntity> prop)
        {
            if (condition) WhereIsNull(prop);
            return (TBuilder)this;
        }
        public TBuilder WhereIsNull<TProp>(Expression<Func<TEntity, TProp>> prop)
        {
            return Where(Filter.IsNull(prop));
        }
        public TBuilder WhereIsNull<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop)
        {
            if (condition) WhereIsNull(prop);
            return (TBuilder)this;
        }
        public TBuilder WhereIsNotNull(PropertyDefinition<TEntity> prop)
        {
            return Where(Filter.IsNotNull(prop));
        }
        public TBuilder WhereIsNotNull(bool condition, PropertyDefinition<TEntity> prop)
        {
            if (condition) WhereIsNotNull(prop);
            return (TBuilder)this;
        }
        public TBuilder WhereIsNotNull<TProp>(Expression<Func<TEntity, TProp>> prop)
        {
            return Where(Filter.IsNotNull(prop));
        }
        public TBuilder WhereIsNotNull<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop)
        {
            if (condition) WhereIsNotNull(prop);
            return (TBuilder)this;
        }
        public TBuilder WhereBetween(PropertyDefinition<TEntity> prop, object minValue, object maxValue)
        {
            return Where(Filter.Between(prop, minValue, maxValue));
        }
        public TBuilder WhereBetween(bool condition, PropertyDefinition<TEntity> prop, object minValue, object maxValue)
        {
            if (condition) WhereBetween(prop, minValue, maxValue);
            return (TBuilder)this;
        }
        public TBuilder WhereBetween<TProp>(Expression<Func<TEntity, TProp>> prop, TProp minValue, TProp maxValue)
        {
            return Where(Filter.Between(prop, minValue, maxValue));
        }
        public TBuilder WhereBetween<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp minValue, TProp maxValue)
        {
            if (condition) WhereBetween(prop, minValue, maxValue);
            return (TBuilder)this;
        }
        public TBuilder WhereNotBetween(PropertyDefinition<TEntity> prop, object minValue, object maxValue)
        {
            return Where(Filter.NotBetween(prop, minValue, maxValue));
        }
        public TBuilder WhereNotBetween(bool condition, PropertyDefinition<TEntity> prop, object minValue, object maxValue)
        {
            if (condition) WhereNotBetween(prop, minValue, maxValue);
            return (TBuilder)this;
        }
        public TBuilder WhereNotBetween<TProp>(Expression<Func<TEntity, TProp>> prop, TProp minValue, TProp maxValue)
        {
            return Where(Filter.NotBetween(prop, minValue, maxValue));
        }
        public TBuilder WhereNotBetween<TProp>(bool condition, Expression<Func<TEntity, TProp>> prop, TProp minValue, TProp maxValue)
        {
            if (condition) WhereNotBetween(prop, minValue, maxValue);
            return (TBuilder)this;
        }
        #endregion

    }
}

using IceCoffee.Db4Net.Core.PropertyDefinitions;
using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Collections;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryBuilder<TEntity> : SqlQueryBuilderBase<SqlQueryBuilder<TEntity>, TEntity>
    {
        public SqlQueryBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        internal SqlQueryBuilder(ISqlAdapter sqlAdapter, object id) : base(sqlAdapter)
        {
            if(id is IEnumerable ids && id is not string)
            {
                WhereIn(GetSingleUniqueKey(), ids);
            }
            else
            {
                WhereEq(GetSingleUniqueKey(), id);
            }
        }
    }

    public abstract class SqlQueryBuilderBase<TBuilder, TEntity> : FilterableSqlBuilder<TBuilder, TEntity> where TBuilder : SqlQueryBuilderBase<TBuilder, TEntity>
    {
        public SqlQueryBuilderBase(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        protected override SqlResult GetSqlResult()
        {
            string fromTarget = GetFromTarget();
            string selection = GetSelection();
            string whereConditions = GetWhereConditions();
            string orderBy = GetOrderBy();
            string sql = SqlAdapter.QueryCommand(selection, fromTarget, whereConditions, orderBy);

            return new SqlResult()
            {
                Sql = sql,
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities
            };
        }

        #region From
        private string? _tableName;
        protected string GetFromTarget()
        {
            return _tableName ?? DefaultTableName;
        }
        public TBuilder From(string table, string? alias = null)
        {
            _tableName = alias == null ? table : $"{TryQuote(table)} AS {TryQuote(alias)}";
            return (TBuilder)this;
        }
        #endregion

        #region Select
        private List<PropertyDefinition<TEntity>>? _selectedProperties;
        private string GetSelection()
        {
            string selection;
            if (_selectedProperties == null)
            {
                selection = DefaultSelection;
            }
            else
            {
                selection = string.Join(", ", _selectedProperties.Select(i => 
                {
                    string propertyName = i.PropertyName;
                    string fieldName = GetFieldNameByProperty(propertyName);
                    if (propertyName == fieldName)
                    {
                        return SqlAdapter.Quote(fieldName);
                    }
                    
                    return SqlAdapter.Quote(fieldName) + " AS " + SqlAdapter.Quote(propertyName);
                }));
            }

            return _isDistinct ? "DISTINCT " + selection : selection;
        }

        public TBuilder Select(PropertyDefinition<TEntity> prop)
        {
            _selectedProperties ??= new List<PropertyDefinition<TEntity>>();
            _selectedProperties.Add(prop);
            return (TBuilder)this;
        }

        public TBuilder Select<TProp>(Expression<Func<TEntity, TProp>> prop)
        {
            return Select(new ExpressionPropertyDefinition<TEntity, TProp>(prop));
        }
        public TBuilder Select<TProp>(IEnumerable<Expression<Func<TEntity, TProp>>> props)
        {
            foreach (var column in props)
            {
                Select(column);
            }
            return (TBuilder)this;
        }
        public TBuilder Select<TProp>(params Expression<Func<TEntity, TProp>>[] props)
        {
            return Select(props.AsEnumerable());
        }
        private bool _isDistinct = false;
        public TBuilder Distinct()
        {
            _isDistinct = true;
            return (TBuilder)this;
        }
        #endregion

        #region OrderBy
        private List<string>? _orderByList;
        private string GetOrderBy()
        {
            return _orderByList == null ? string.Empty : "ORDER BY " + string.Join(", ", _orderByList);
        }
        public TBuilder OrderBy(PropertyDefinition<TEntity> prop)
        {
            _orderByList ??= new List<string>();
            _orderByList.Add(prop.Render(SqlAdapter));
            return (TBuilder)this;
        }
        public TBuilder OrderBy(bool condition, PropertyDefinition<TEntity> prop)
        {
            if (condition) return OrderBy(prop);
            return (TBuilder)this;
        }
        public TBuilder OrderBy<TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            return OrderBy(new ExpressionPropertyDefinition<TEntity, TProp>(expression));
        }
        public TBuilder OrderBy<TProp>(bool condition, Expression<Func<TEntity, TProp>> expression)
        {
            if (condition) return OrderBy(expression);
            return (TBuilder)this;
        }

        public TBuilder OrderByDescending(PropertyDefinition<TEntity> prop)
        {
            _orderByList ??= new List<string>();
            _orderByList.Add(prop.Render(SqlAdapter) + " DESC");
            return (TBuilder)this;
        }
        public TBuilder OrderByDescending<TProp>(bool condition, PropertyDefinition<TEntity> prop)
        {
            if (condition) return OrderByDescending(prop);
            return (TBuilder)this;
        }
        public TBuilder OrderByDescending<TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            return OrderByDescending(new ExpressionPropertyDefinition<TEntity, TProp>(expression));
        }
        public TBuilder OrderByDescending<TProp>(bool condition, Expression<Func<TEntity, TProp>> expression)
        {
            if (condition) return OrderByDescending(expression);
            return (TBuilder)this;
        }
        #endregion
    }
}

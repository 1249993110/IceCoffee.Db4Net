using IceCoffee.Db4Net.Core.PropertyDefinitions;
using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlUpdateBuilder<TEntity> : FilterableSqlBuilder<SqlUpdateBuilder<TEntity>, TEntity>
    {
        public SqlUpdateBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        protected override SqlResult GetSqlResult()
        {
            return new SqlResult()
            {
                Sql = SqlAdapter.UpdateCommand(_tableName ?? DefaultTableName, GetUpdateClause(), GetWhereConditions()),
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities
            };
        }

        #region To
        private string? _tableName;
        public SqlUpdateBuilder<TEntity> To(string table)
        {
            if (Utils.IsValidSqlIdentifier(table))
            {
                table = SqlAdapter.Quote(table);
            }
            _tableName = table;
            return this;
        }
        #endregion

        #region Set Definitions
        class SetDefinition
        {
            public required PropertyDefinition<TEntity> Prop { get; set; }
            public required object? Value { get; set; }
            public bool IsRaw { get; set; }
        }
        private List<SetDefinition>? _sets;
        private string GetUpdateClause()
        {
            if (_sets == null)
            {
                return DefaultUpdateClause;
            }

            return string.Join(", ", _sets.Select(i => $"{i.Prop.Render(SqlAdapter)} = {(i.IsRaw ? i.Value : ParameterBuilder.AddNamedParam(i.Value))}"));
        }
        public SqlUpdateBuilder<TEntity> Set(PropertyDefinition<TEntity> prop, object? value)
        {
            _sets ??= new List<SetDefinition>();
            _sets.Add(new SetDefinition() { Prop = prop, Value = value });
            return this;
        }
        public SqlUpdateBuilder<TEntity> Set<TProp>(bool condition, PropertyDefinition<TEntity> prop, object? value)
        {
            if (condition) Set(prop, value);
            return this;
        }
        public SqlUpdateBuilder<TEntity> Set<TProp>(Expression<Func<TEntity, TProp>> expression, TProp value)
        {
            return Set(new ExpressionPropertyDefinition<TEntity, TProp>(expression), value);
        }
        public SqlUpdateBuilder<TEntity> Set<TProp>(bool condition, Expression<Func<TEntity, TProp>> expression, TProp value)
        {
            if (condition) Set(expression, value);
            return this;
        }
        public SqlUpdateBuilder<TEntity> SetRaw(PropertyDefinition<TEntity> prop, string rawSql)
        {
            _sets ??= new List<SetDefinition>();
            _sets.Add(new SetDefinition() { Prop = prop, Value = rawSql, IsRaw = true });
            return this;
        }
        public SqlUpdateBuilder<TEntity> SetRaw<TProp>(bool condition, PropertyDefinition<TEntity> prop, string rawSql)
        {
            if (condition) SetRaw(prop, rawSql);
            return this;
        }
        public SqlUpdateBuilder<TEntity> SetRaw<TProp>(Expression<Func<TEntity, TProp>> expression, string rawSql)
        {
            return SetRaw(new ExpressionPropertyDefinition<TEntity, TProp>(expression), rawSql);
        }
        public SqlUpdateBuilder<TEntity> SetRaw<TProp>(bool condition, Expression<Func<TEntity, TProp>> expression, string rawSql)
        {
            if (condition) SetRaw(expression, rawSql);
            return this;
        }
        #endregion
    }
}

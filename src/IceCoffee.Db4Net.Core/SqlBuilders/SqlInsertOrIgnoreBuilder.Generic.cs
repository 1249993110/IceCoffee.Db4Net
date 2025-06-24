using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlInsertOrIgnoreBuilder<TEntity> : SqlBuilder<TEntity>
    {
        public SqlInsertOrIgnoreBuilder(ISqlAdapter sqlAdapter, TEntity entity) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entity;
        }

        public SqlInsertOrIgnoreBuilder(ISqlAdapter sqlAdapter, IEnumerable<TEntity> entities) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entities;
        }

        protected override SqlResult GetSqlResult()
        {
            (string columns, string parameters) = DefaultInsertClause;
            return new SqlResult()
            {
                Sql = SqlAdapter.InsertIgnoreCommand(_tableName ?? DefaultTableName, columns, parameters, GetUniqueConstraint(), GetUniqueKeys()),
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities
            };
        }

        private string? _tableName;
        public SqlInsertOrIgnoreBuilder<TEntity> To(string table)
        {
            if (Utils.IsValidSqlIdentifier(table))
            {
                table = SqlAdapter.Quote(table);
            }
            _tableName = table;
            return this;
        }
    }
}

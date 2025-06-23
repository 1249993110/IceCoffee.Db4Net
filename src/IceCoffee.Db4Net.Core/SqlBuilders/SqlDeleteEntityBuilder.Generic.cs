using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlDeleteEntityBuilder<TEntity> : SqlBuilder<TEntity>
    {
        public SqlDeleteEntityBuilder(ISqlAdapter sqlAdapter, TEntity entity) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entity;
        }
        public SqlDeleteEntityBuilder(ISqlAdapter sqlAdapter, IEnumerable<TEntity> entities) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entities;
        }

        protected override SqlResult GetSqlResult()
        {
            string whereConditions = "WHERE " + GetUniqueConstraint(SqlAdapter);
            return new SqlResult()
            {
                Sql = SqlAdapter.DeleteCommand(_tableName ?? DefaultTableName, whereConditions),
                NamedParameters = ParameterBuilder.NamedParameters, // null
                DynamicParameters = ParameterBuilder.DynamicParameters, // null
                Entities = ParameterBuilder.Entities
            };
        }

        private string? _tableName;
        public SqlDeleteEntityBuilder<TEntity> From(string table)
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

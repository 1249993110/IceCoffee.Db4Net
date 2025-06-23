using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlInsertEntityBuilder<TEntity> : SqlBuilder<TEntity>
    {
        public SqlInsertEntityBuilder(ISqlAdapter sqlAdapter, TEntity entity) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entity;
        }
        public SqlInsertEntityBuilder(ISqlAdapter sqlAdapter, IEnumerable<TEntity> entities) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entities;
        }

        protected override SqlResult GetSqlResult()
        {
            (string columns, string parameters) = DefaultInsertClause;
            return new SqlResult()
            {
                Sql = SqlAdapter.InsertCommand(_tableName ?? DefaultTableName, columns, parameters),
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities,
            };
        }

        #region To
        private string? _tableName;
        public SqlInsertEntityBuilder<TEntity> To(string table)
        {
            if (Utils.IsValidSqlIdentifier(table))
            {
                table = SqlAdapter.Quote(table);
            }
            _tableName = table;
            return this;
        }
        #endregion
    }
}

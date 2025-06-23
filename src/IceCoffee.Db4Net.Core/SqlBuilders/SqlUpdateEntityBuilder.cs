using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlUpdateEntityBuilder<TEntity> : SqlBuilder<TEntity>
    {
        public SqlUpdateEntityBuilder(ISqlAdapter sqlAdapter, TEntity entity) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entity;
        }
        public SqlUpdateEntityBuilder(ISqlAdapter sqlAdapter, IEnumerable<TEntity> entities) : base(sqlAdapter)
        {
            ParameterBuilder.Entities = entities;
        }

        protected override SqlResult GetSqlResult()
        {
            string whereConditions = "WHERE " + GetUniqueConstraint(SqlAdapter);
            return new SqlResult()
            {
                Sql = SqlAdapter.UpdateCommand(_tableName ?? DefaultTableName, DefaultUpdateClause, whereConditions),
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities
            };
        }

        #region To
        private string? _tableName;
        public SqlUpdateEntityBuilder<TEntity> To(string table)
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

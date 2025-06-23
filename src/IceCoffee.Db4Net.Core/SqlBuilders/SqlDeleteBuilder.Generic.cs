using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Collections;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlDeleteBuilder<TEntity> : FilterableSqlBuilder<SqlDeleteBuilder<TEntity>, TEntity>
    {
        public SqlDeleteBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        internal SqlDeleteBuilder(ISqlAdapter sqlAdapter, string id) : base(sqlAdapter)
        {
            WhereEq(GetSingleUniqueKey(), id);
        }
        internal SqlDeleteBuilder(ISqlAdapter sqlAdapter, object id) : base(sqlAdapter)
        {
            WhereEq(GetSingleUniqueKey(), id);
        }
        internal SqlDeleteBuilder(ISqlAdapter sqlAdapter, IEnumerable ids) : base(sqlAdapter)
        {
            WhereIn(GetSingleUniqueKey(), ids);
        }

        protected override SqlResult GetSqlResult()
        {
            return new SqlResult()
            {
                Sql = SqlAdapter.DeleteCommand(_tableName ?? DefaultTableName, GetWhereConditions()),
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.Entities
            };
        }

        private string? _tableName;
        public SqlDeleteBuilder<TEntity> From(string table)
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

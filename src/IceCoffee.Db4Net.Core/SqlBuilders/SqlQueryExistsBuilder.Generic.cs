using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryExistsBuilder<TEntity> : SqlQueryExistsBuilder
    {
        public SqlQueryExistsBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
            From(SqlBuilder<TEntity>.DefaultTableName);
        }

        public SqlQueryExistsBuilder(ISqlAdapter sqlAdapter, object id) : this(sqlAdapter)
        {
            string idColumn = TryQuote(SqlBuilder<TEntity>.GetFieldNameByProperty(SqlBuilder<TEntity>.GetSingleUniqueKey()));
            string name = ParameterBuilder.AddNamedParam(id);
            WhereRaw($"{idColumn} = {name}");
        }
    }
}

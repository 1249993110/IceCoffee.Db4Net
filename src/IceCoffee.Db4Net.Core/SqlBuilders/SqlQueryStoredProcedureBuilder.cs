using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryStoredProcedureBuilder : SqlBuilder
    {
        public SqlQueryStoredProcedureBuilder(ISqlAdapter sqlAdapter, string procedureName, object? param = null) : base(sqlAdapter)
        {
            _procedureName = procedureName;
            _param = param;
        }

        private readonly string _procedureName;
        private readonly object? _param;
        protected override SqlResult GetSqlResult()
        {
            ParameterBuilder.AddDynamicParams(_param);
            return new SqlResult()
            {
                Sql = _procedureName,
                NamedParameters = ParameterBuilder.NamedParameters,
                DynamicParameters = ParameterBuilder.DynamicParameters,
                Entities = ParameterBuilder.DynamicParameters,
            };
        }
    }
}

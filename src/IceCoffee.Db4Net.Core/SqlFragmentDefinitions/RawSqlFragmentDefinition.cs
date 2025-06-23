namespace IceCoffee.Db4Net.Core.SqlFragmentDefinitions
{
    internal class RawSqlFragmentDefinition : SqlFragmentDefinition
    {
        private readonly string _rawSql;
        private readonly object? _param;

        public RawSqlFragmentDefinition(string rawSql, object? param = null)
        {
            _rawSql = rawSql;
            _param = param;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            parameterBuilder.AddDynamicParams(_param);
            return _rawSql;
        }
    }
}

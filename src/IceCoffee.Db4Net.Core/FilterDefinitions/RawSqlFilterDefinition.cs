namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal class RawSqlFilterDefinition : FilterDefinition
    {
        private readonly string _rawSql;
        private readonly object? _param;

        public RawSqlFilterDefinition(string rawSql, object? param = null)
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

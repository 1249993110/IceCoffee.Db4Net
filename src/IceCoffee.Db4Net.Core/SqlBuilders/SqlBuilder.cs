using IceCoffee.Db4Net.Core.FilterDefinitions;
using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public abstract class SqlBuilder : ISqlBuilder
    {
        internal string DatabaseName { get; set; } = string.Empty;
        string ISqlBuilder.DatabaseName => DatabaseName;

        private readonly ISqlAdapter _sqlAdapter;
        protected ISqlAdapter SqlAdapter => _sqlAdapter;

        public static FilterDefinitionBuilder Filter => FilterDefinitionBuilder.Default;

        public SqlBuilder(ISqlAdapter sqlAdapter)
        {
            _sqlAdapter = sqlAdapter;
            ParameterBuilder = new ParameterBuilder(_sqlAdapter);
        }

        protected ParameterBuilder ParameterBuilder { get; }

        public SqlResult SqlResult => GetSqlResult();
        protected abstract SqlResult GetSqlResult();

        protected string TryQuote(string value)
        {
            if (Utils.IsValidSqlIdentifier(value))
            {
                value = SqlAdapter.Quote(value);
            }

            return value;
        }
    }
}

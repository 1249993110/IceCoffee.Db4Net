namespace IceCoffee.Db4Net.Core.SqlFragmentDefinitions
{
    internal class TableSqlFragmentDefinition : SqlFragmentDefinition
    {
        private readonly string _table;
        private readonly string? _alias;
        public TableSqlFragmentDefinition(string table, string? alias = null)
        {
            _table = table;
            _alias = alias;
        }
        internal override string Render(ParameterBuilder parameterBuilder)
        {
            string? alias = _alias == null ? null : (Utils.IsValidSqlIdentifier(_alias) ? parameterBuilder.SqlAdapter.Quote(_alias) : _alias);

            return alias == null ? _table : $"{_table} AS {alias}";
        }
    }
}

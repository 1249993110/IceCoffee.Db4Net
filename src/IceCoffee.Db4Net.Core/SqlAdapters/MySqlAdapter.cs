namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    internal class MySqlAdapter : SqlAdapterBase
    {
        public override string PagingCommand(int pageIndex, int pageSize)
        {
            return $" LIMIT {pageSize} OFFSET {pageIndex * pageSize}";
        }

        public override string Quote(string name)
        {
            int index = name.IndexOf('.');
            if (index > 0 && index == name.LastIndexOf('.'))
            {
                return Quote(name.Substring(0, index)) + "." + Quote(name.Substring(index));
            }
            return $"`{name}`";
        }

        public override string Parameter(string parameterName)
        {
            return "@" + parameterName;
        }

        public override string InsertReturningIdCommand()
        {
            return "; SELECT LAST_INSERT_ID()";
        }

        public override string InsertIgnoreCommand(string tableName, string columns, string parameters, string uniqueConstraint, string uniqueKeys)
        {
            return $"INSERT OR IGNORE INTO {tableName} ({columns}) VALUES ({parameters})";
        }

        public override string InsertReplaceCommand(string tableName, string columns, string parameters, string uniqueConstraint, string updateClause, string uniqueKeys)
        {
            return $"REPLACE INTO {tableName} ({columns}) VALUES ({parameters})";
        }
    }
}

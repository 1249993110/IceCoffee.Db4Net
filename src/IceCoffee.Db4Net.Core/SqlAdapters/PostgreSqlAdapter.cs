namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    internal class PostgreSqlAdapter : SqlAdapterBase
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
            return $"\"{name}\"";
        }

        public override string Parameter(string parameterName)
        {
            return "@" + parameterName;
        }

        public override string InsertReturningIdCommand()
        {
            return " RETURNING id";
        }

        public override string InsertIgnoreCommand(string tableName, string columns, string parameters, string uniqueConstraint, string uniqueKeys)
        {
            return $"INSERT INTO {tableName} ({columns}) VALUES ({parameters}) ON CONFLICT ({uniqueKeys}) DO NOTHING";
        }

        public override string InsertReplaceCommand(string tableName, string columns, string parameters, string uniqueConstraint, string updateClause, string uniqueKeys)
        {
            return $"INSERT INTO {tableName} ({columns}) VALUES ({parameters}) ON CONFLICT ({uniqueKeys}) DO UPDATE SET {updateClause}";
        }

        public override string Like(string parameterPlaceholder)
        {
            return "I" + base.Like(parameterPlaceholder);
        }

        public override string In(string parameterPlaceholder)
        {
            return $"= ANY({parameterPlaceholder})";
        }
    }
}

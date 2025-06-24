namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    internal class SqlServerAdapter : SqlAdapterBase
    {
        public override string PagingCommand(int pageIndex, int pageSize)
        {
            return $" OFFSET {pageIndex * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        public override string ExistsCommand(string fromTarget, string whereConditions)
        {
            return $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {fromTarget} {whereConditions}) THEN 1 ELSE 0 END";
        }

        public override string Quote(string name)
        {
            int index = name.IndexOf('.');
            if (index > 0 && index == name.LastIndexOf('.'))
            {
                return Quote(name.Substring(0, index)) + "." + Quote(name.Substring(index));
            }
            return $"[{name}]";
        }

        public override string Parameter(string parameterName)
        {
            return "@" + parameterName;
        }

        public override string InsertReturningIdCommand()
        {
            return "; SELECT SCOPE_IDENTITY()";
        }

        public override string InsertIgnoreCommand(string tableName, string columns, string parameters, string uniqueConstraint, string uniqueKeys)
        {
            return $"INSERT INTO {tableName} ({columns}) SELECT {parameters} WHERE NOT EXISTS (SELECT 1 FROM {tableName} WHERE {uniqueConstraint})";
        }

        public override string InsertReplaceCommand(string tableName, string columns, string parameters, string uniqueConstraint, string updateClause, string uniqueKeys)
        {
            return $"UPDATE {tableName} SET {updateClause} WHERE {uniqueConstraint}; IF @@ROWCOUNT=0 BEGIN {InsertCommand(tableName, columns, parameters)} END";
        }
    }
}

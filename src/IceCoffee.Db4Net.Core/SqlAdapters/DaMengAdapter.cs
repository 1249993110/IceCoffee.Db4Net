namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    internal class DaMengAdapter : SqlAdapterBase
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
            return ":" + parameterName;
        }

        public override string InsertReturningIdCommand()
        {
            return "; SELECT LAST_INSERT_ID()";
        }

        public override string InsertIgnoreCommand(string tableName, string columns, string parameters, string uniqueConstraint)
        {
            return $"INSERT INTO {tableName} ({columns}) SELECT {parameters} WHERE NOT EXISTS (SELECT 1 FROM {tableName} WHERE {uniqueConstraint})";
        }

        public override string InsertReplaceCommand(string tableName, string columns, string parameters, string uniqueConstraint, string updateClause)
        {
            return @$"
BEGIN
    UPDATE {tableName} SET {updateClause} WHERE {uniqueConstraint};
    IF NOT EXISTS (SELECT 1 FROM {tableName} WHERE {uniqueConstraint}) THEN
        {InsertCommand(tableName, columns, parameters)};
    END IF;
END";
        }

        public override string Like(string parameterPlaceholder)
        {
            return $"LIKE '%'||{parameterPlaceholder}||'%'";
        }
    }
}

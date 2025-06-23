namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    /// <summary>
    /// Generates the SQL queries that are compatible to all supported databases
    /// </summary>
    internal abstract class SqlAdapterBase : ISqlAdapter
    {
        public virtual string QueryCommand(string selection, string fromTarget, string whereConditions, string orderBy)
        {
            return $"SELECT {selection} FROM {fromTarget} {whereConditions} {orderBy}".TrimEnd();
        }

        public virtual string QueryCommand(string selection, string fromTarget, string whereConditions, string groupBy, string havingConditions, string orderBy)
        {
            return $"SELECT {selection} FROM {fromTarget} {havingConditions} {groupBy} {havingConditions} {orderBy}".TrimEnd();
        }

        public virtual string ExistsCommand(string fromTarget, string whereConditions)
        {
            return $"SELECT EXISTS(SELECT 1 FROM {fromTarget} {whereConditions})";
        }

        public virtual string CountCommand(string fromTarget, string whereConditions)
        {
            return $"; SELECT COUNT(*) FROM {fromTarget} {whereConditions}".TrimEnd();
        }

        public abstract string PagingCommand(int pageIndex, int pageSize);

        public virtual string InsertCommand(string tableName, string columns, string parameters)
        {
            return $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";
        }

        public abstract string InsertReturningIdCommand();

        public abstract string InsertIgnoreCommand(string tableName, string columns, string parameters, string uniqueConstraint);

        public abstract string InsertReplaceCommand(string tableName, string columns, string parameters, string uniqueConstraint, string updateClause);

        public virtual string UpdateCommand(string tableName, string updateClause, string whereConditions)
        {
            if(string.IsNullOrEmpty(whereConditions))
                throw new ArgumentNullException(nameof(whereConditions));

            return $"UPDATE {tableName} SET {updateClause} {whereConditions}".TrimEnd();
        }

        public virtual string DeleteCommand(string tableName, string whereConditions)
        {
            if (string.IsNullOrEmpty(whereConditions))
                throw new ArgumentNullException(nameof(whereConditions));

            return $"DELETE FROM {tableName} {whereConditions}".TrimEnd();
        }

        public abstract string Quote(string name);
        public abstract string Parameter(string parameterName);

        public virtual string Like(string parameterPlaceholder)
        {
            return $"LIKE CONCAT('%', {parameterPlaceholder}, '%')";
        }

        public virtual string In(string parameterPlaceholder)
        {
            return $"IN {parameterPlaceholder}";
        }

    }
}

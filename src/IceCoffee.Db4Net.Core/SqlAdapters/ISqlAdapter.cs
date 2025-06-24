namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    public interface ISqlAdapter
    {
        string QueryCommand(string selection, string fromTarget, string whereConditions, string orderBy);
        string QueryCommand(string selection, string fromTarget, string whereConditions, string groupBy, string havingConditions, string orderBy);
        string ExistsCommand(string fromTarget, string whereConditions);
        string CountCommand(string fromTarget, string whereConditions);
        string PagingCommand(int pageIndex, int pageSize);

        string InsertCommand(string tableName, string columns, string parameters);

        string InsertReturningIdCommand();
        string InsertIgnoreCommand(string tableName, string columns, string parameters, string uniqueConstraint, string uniqueKeys);
        string InsertReplaceCommand(string tableName, string columns, string parameters, string uniqueConstraint, string updateClause, string uniqueKeys);

        string UpdateCommand(string tableName, string updateClause, string whereConditions);

        string DeleteCommand(string tableName, string whereConditions);

        string Quote(string name);

        string Parameter(string parameterName);

        string Like(string parameterPlaceholder);

        string In(string parameterPlaceholder);
    }
}

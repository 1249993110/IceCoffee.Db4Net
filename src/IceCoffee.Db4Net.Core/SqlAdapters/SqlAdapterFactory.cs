namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    public static class SqlAdapterFactory
    {
        public static ISqlAdapter CreateAdapter(DatabaseProvider databaseProvider)
        {
            switch (databaseProvider)
            {
                case DatabaseProvider.SQLServer:
                    return new SqlServerAdapter();
                case DatabaseProvider.SQLite:
                    return new SqliteAdapter();
                case DatabaseProvider.DaMeng:
                    return new DaMengAdapter();
                case DatabaseProvider.MySQL:
                    return new MySqlAdapter();
                case DatabaseProvider.PostgreSQL:
                    return new PostgreSqlAdapter();
                case DatabaseProvider.Undefined:
                default:
                    throw new NotSupportedException($"Database provider type '{databaseProvider}' is not supported.");
            }
        }
    }
}

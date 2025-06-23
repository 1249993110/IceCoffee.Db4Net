namespace IceCoffee.Db4Net.Core.SqlAdapters
{
    public static class SqlAdapterFactory
    {
        public static ISqlAdapter CreateAdapter(DatabaseProvider databaseType)
        {
            switch (databaseType)
            {
                case DatabaseProvider.SQLServer:
                    return new SqlServerAdapter();
                case DatabaseProvider.SQLite:
                    return new SqliteAdapter();
                case DatabaseProvider.DaMeng:
                    return new DaMengAdapter();
                case DatabaseProvider.PostgreSQL:
                case DatabaseProvider.MySQL:
                case DatabaseProvider.Undefined:
                default:
                    throw new NotSupportedException($"Database type '{databaseType}' is not supported.");
            }
        }
    }
}

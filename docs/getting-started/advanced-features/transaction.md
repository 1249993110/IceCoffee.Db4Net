# Transaction

## Synchronous
``` csharp
using (var dbConnection = Db.CreateDbConnection())
{
    dbConnection.Open();
    using (var transaction = dbConnection.BeginTransaction())
    {
        try
        {
            Db.Insert(entity).Execute(transaction);
            Db.Insert(entity).Execute(transaction);

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}
```

## Asynchronous
``` csharp
using (var dbConnection = Db.CreateDbConnection())
{
    dbConnection.Open();
    using (var transaction = await dbConnection.BeginTransactionAsync())
    {
        try
        {
            await Db.Insert(entity).ExecuteAsync(transaction);
            await Db.Insert(entity).ExecuteAsync(transaction);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}
```

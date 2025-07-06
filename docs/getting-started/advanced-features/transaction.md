# Transaction

## Use unit of work

### *Synchronous*
``` csharp
using (var uow = Db.CreateUnitOfWork())
{
    Db.Insert(entity).Execute(uow.DbTransaction);
    Db.Insert(entity).Execute(uow.DbTransaction);
    uow.Commit();
}
```

### *Asynchronous*
``` csharp
using (var uow = Db.CreateUnitOfWork())
{
    await Db.Insert(entity).ExecuteAsync(uow.DbTransaction);
    await Db.Insert(entity).ExecuteAsync(uow.DbTransaction);
    await uow.CommitAsync();
}
```

## Use vanilla

### *Synchronous*
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
            throw;
        }
    }
}
```

### *Asynchronous*
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
            throw;
        }
    }
}
```
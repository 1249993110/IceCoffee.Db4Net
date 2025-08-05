# Insert

### Insert single
```csharp
int affectedRows = Db.Insert(entity)
    .Execute();
```

### Insert multiple
```csharp
int affectedRows = Db.InsertMany(entities)
    .Execute();
```

### Insert and get database generated id
```csharp
int id = Db.InsertAndGetId(entity)
    .Execute<int>();
```

### Insert or ignore single
```csharp
int affectedRows = Db.InsertOrIgnore(entity)
    .Execute();
```

### Insert or ignore multiple
```csharp
int affectedRows = Db.InsertOrIgnoreMany(entities)
    .Execute();
```

### Insert or replace single
```csharp
int affectedRows = Db.InsertOrReplace(entity)
    .Execute();
```

### Insert or replace multiple
```csharp
int affectedRows = Db.InsertOrReplaceMany(entities)
    .Execute();
```

### Insert specific columns
```csharp
int affectedRows = Db.Insert<EntityClass>()
    .Set(i => i.Id, entity.Id)
    .Set(i => i.Name, entity.Name)
    .Execute();
```

### Insert single to specific table
```csharp
int affectedRows = Db.Insert(entity)
    .To(table)
    .Execute();
```
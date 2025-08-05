# Update

### Update single
```csharp
var affectedRows = Db.Update(entity).Execute();
```

### Update multiple
```csharp
var affectedRows = Db.UpdateMany(entities).Execute();
```

### Update with where condition
```csharp
var affectedRows = Db.Update(entity)
    .WhereIn(i => i.Id, idsToUpdate)
    .WhereEq(i => i.Name, name)
    .WhereLt(i => i.Age, 18)
    .Execute();
```

### Update specific columns
```csharp
int affectedRows = Db.Update<EntityClass>()
    .Set(i => i.Id, entity.Id)
    .Set(i => i.Name, entity.Name)
    .Execute();
```

### Update single to specific table
```csharp
int affectedRows = Db.Update(entity)
    .To(table)
    .Execute();
```
# Delete

### Delete single
```csharp
var affectedRows = Db.Delete<EntityClass>(entityId).Execute();
// Or
var affectedRows = Db.Delete(entity).Execute();
```

### Delete multiple
```csharp
var affectedRows = Db.DeleteMany<EntityClass>(entityIds).Execute();
// Or
var affectedRows = Db.DeleteMany(entities).Execute();
```

### Update with where condition
```csharp
var affectedRows = Db.Delete<EntityClass>()
    .WhereIn(i => i.Id, idsToUpdate)
    .WhereEq(i => i.Name, name)
    .WhereLt(i => i.Age, 18)
    .Execute();
```
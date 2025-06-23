# Update

### 1. Update single
``` csharp
var affectedRows = Db.Update(entity).Execute();
```

### 2. Update multiple
``` csharp
var affectedRows = Db.UpdateMany(entities).Execute();
```
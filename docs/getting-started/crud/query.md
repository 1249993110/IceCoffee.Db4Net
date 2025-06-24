# Query

### Query by ID
``` csharp
var entity = Db.Query<EntityClass>(entityId)
    .GetSingleOrDefault();
```

### Query all
``` csharp
var entities = Db.Query<EntityClass>()
    .GetList();
```

### Query paged
``` csharp
var pagedResult = Db.QueryPaged<EntityClass>(1, 1)
    .OrderBy(i => i.Id)
    .GetPagedResult();
```

### Query count
``` csharp
int count = Db.QueryCount<EntityClass>()
    .GetSingle<int>();
```

### Query exists
``` csharp
bool exists = Db.QueryExists<EntityClass>(entityId)
    .Get();
```

### Query with where condition
``` csharp
var entities = Db.Query<EntityClass>()
    .WhereEq(i => i.Name, name)
    .WhereLt(i => i.Age, 18)
    .GetList();
```

### Query select properties
``` csharp
var entities = Db.Query<EntityClass>()
    .Select(i => i.Id)
    .Select(i => i.Name)
    .GetList();
```

### 9. Query from specific table
``` csharp
var entities = Db.Query<EntityClass>()
    .From(table)
    .GetList();
```
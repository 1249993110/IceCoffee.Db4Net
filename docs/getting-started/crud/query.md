# Query

### 1. Query by ID
``` csharp
var entity = Db.Query<EntityClass>(entityId).GetSingleOrDefault();
```

### 2. Query all
``` csharp
var entities = Db.Query<EntityClass>().GetList();
```

### 3. Query paged
``` csharp
var pagedResult = Db.QueryPaged<EntityClass>(1, 1).OrderBy(i => i.Id).GetPagedResult();
```

### 4. Query count
``` csharp
int count = Db.QueryCount<EntityClass>().GetSingle<int>();
```

### 5. Query exists
``` csharp
bool exists = Db.QueryExists<EntityClass>(entityId).Get();
```

### 7. Query with where condition
``` csharp
var entities = Db.Query<EntityClass>()
    .WhereEq(i => i.Name, name)
    .GetList();
```

### 8. Query select properties
``` csharp
var entities = Db.Query<EntityClass>()
    .Select(i => i.Id)
    .Select(i => i.Name)
    .GetList();
```
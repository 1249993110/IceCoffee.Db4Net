# Insert

### 1. Insert single
``` csharp
int affectedRows = Db.Insert(entity).Execute();
```

### 2. Insert multiple
``` csharp
int affectedRows = Db.InsertMany(entities).Execute();
```

### 3. Insert and get database generated id
``` csharp
int id = Db.InsertAndGetId(entity).Execute<int>();
```

### 4. Insert or ignore single
``` csharp
int affectedRows = Db.InsertOrIgnore(entity).Execute();
```

### 5. Insert or ignore multiple
``` csharp
int affectedRows = Db.InsertOrIgnoreMany(entities).Execute();
```

### 6. Insert or replace single
``` csharp
int affectedRows = Db.InsertOrReplace(entity).Execute();
```

### 7. Insert or replace multiple
``` csharp
int affectedRows = Db.InsertOrReplaceMany(entities).Execute();
```
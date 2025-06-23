# Insert

### 1. Insert single
``` csharp
var foo = new Foo { Name = "Tom" };
int affectedRows = Db.Insert(foo).Execute();
```

### 2. Insert multiple
``` csharp
var foos = new Foo[] { new Foo { Name = "Tom" }, new Foo { Name = "James" } };
int affectedRows = Db.InsertMany(foos).Execute();
```

### 3. Insert and get database generated id
``` csharp
int id = Db.InsertAndGetId(foo).Execute<int>();
```

### 4. Insert or ignore single
``` csharp
int affectedRows = Db.InsertOrIgnore(foo).Execute();
```

### 5. Insert or ignore multiple
``` csharp
int affectedRows = Db.InsertOrIgnoreMany(foos).Execute();
```

### 6. Insert or replace single
``` csharp
int affectedRows = Db.InsertOrReplace(foo).Execute();
```

### 7. Insert or replace multiple
``` csharp
int affectedRows = Db.InsertOrReplaceMany(foos).Execute();
```
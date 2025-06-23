# Quick Examples

## Installation
The example below shows how to install the [IceCoffee.Db4Net](https://www.nuget.org/packages/IceCoffee.Db4Net) package. To install other packages, see the [Packages](../index.md#packages) section.

Install via the NuGet Package Manager Console

```powershell
Install-Package IceCoffee.Db4Net
```

Or via the .NET Core command line interface

```bash
dotnet add package IceCoffee.Db4Net
```

### Installing Database Providers

* **SQL Server**
```powershell
Install-Package Microsoft.Data.SqlClient
```

* **SQLite**
```powershell
Install-Package  Microsoft.Data.SQLite
```

* **PostgreSQL**
```powershell
Install-Package Npgsql
```

* **MySQL**
```powershell
Install-Package  MySql.Data
```

* **DaMeng**
```powershell
Install-Package SqlSugarCore.Dm
```

## Usage

### 1. Introducing namespaces
``` csharp
using IceCoffee.Db4Net;
using IceCoffee.Db4Net.OptionalAttributes;
using IceCoffee.Db4Net.Extensions;
using IceCoffee.Db4Net.Core;
```

### 2. Register database connection
``` csharp
string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
Db.Register(DatabaseProvider.SQLite, connectionString);
```

### 3. Define entity
``` csharp
[Table("foo")]
public class Foo
{
    [UniqueKey, DatabaseGenerated]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
```

### 4. Insert an entity
``` csharp
var foo = new Foo { Name = "Tom" };
Db.Insert(foo).Execute();
```

The generated SQL will be:
``` sql
INSERT INTO [foo] ([Name], [Age], [created_at]) VALUES (@Name, @Age, @CreatedAt)
```

### 5. Get an entity by ID
``` csharp
Db.Query<Foo>(1).GetSingle();
```

The generated SQL will be:
``` sql
SELECT [Id], [Name], [Age], [created_at] AS [CreatedAt] FROM [foo] WHERE [Id] = @p1
```

### 6. Update an entity
``` csharp
var foo = new Foo { Id = 0, Name = "James" };
Db.Update(foo).Execute();
```

The generated SQL will be:
``` sql
INSERT INTO [foo] ([Name], [Age], [created_at]) VALUES (@Name, @Age, @CreatedAt)
```

### 7. Delete an entity by ID
``` csharp
Db.Delete<Foo>(0).Execute();
```

The generated SQL will be:
``` sql
DELETE FROM [foo] WHERE [Id] = @p1
```
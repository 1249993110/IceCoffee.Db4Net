# Dependency Injection

## Example with ASP.NET Core and D.I

### Installing packages
```powershell
Install-Package IceCoffee.Db4Net.DependencyInjection
# Installing Database Providers
```

### Implements custom repository
```csharp
// Follow one repository for one database
public class ProdRepository : Repository
{
    public ProdRepository() : base("ProdDatabaseName")
    {
    }

    // Or
    public ProdRepository(string databaseName) : base(databaseName)
    {
    }

    // Your code
}
```

### Configure Services
```csharp
// Register repositories
services.AddDbConnection<ProdRepository>("ProdDatabaseName", options =>
{
    options.ConnectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
    options.DatabaseProvider = DatabaseProvider.SQLite;
});

// services.AddDbConnection<OtherRepository>("OtherDatabaseName", options => {...});
```

### Use in API Controller
```csharp
[Route("[controller]")]
public class FooController : ControllerBase
{
    private readonly ProdRepository _prodRepository
    public FooController(ProdRepository prodRepository)
    {
        _prodRepository = prodRepository;
    }

    [HttpPost]
    public ActionResult<Foo> Create([FromBody] FooDto dto)
    {
        var entity = dto; // Convert dto to entity
        int id = await _prodRepository.InsertAndGetId(entity).ExecuteAsync<int>();
        return CreatedAtAction(nameof(Get), new { id = id }, entity);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Foo>> Get([FromRoute] int id)
    {
        var entity =  await _prodRepository.Query(id).GetSingleOrDefaultAsync();
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] FooDto dto)
    {
        var entity =  await _prodRepository.Query(id).GetSingleOrDefaultAsync();
        if (entity == null)
        {
            return NotFound();
        }

        entity.Name = dto.Name;
        // ...

        await _prodRepository.Update(entity).ExecuteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity =  await _prodRepository.Query(id).GetSingleOrDefaultAsync();
        if (entity == null)
        {
            return NotFound();
        }

        await _prodRepository.Delete(entity).ExecuteAsync();

        return NoContent();
    }
}
```
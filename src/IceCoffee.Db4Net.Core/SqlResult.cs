
namespace IceCoffee.Db4Net.Core
{
    public class SqlResult
    {
        public required string Sql { get; set; }
        public string? AttachedSql { get; set; }
        public required IReadOnlyList<KeyValuePair<string, object?>>? NamedParameters { get; set; }
        public required IReadOnlyList<object?>? DynamicParameters { get; set; }
        public required object? Entities { get; set; }
    }
}

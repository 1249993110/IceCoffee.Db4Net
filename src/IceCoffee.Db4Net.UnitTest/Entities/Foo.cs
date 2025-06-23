using IceCoffee.Db4Net.Core.OptionalAttributes;

namespace IceCoffee.Db4Net.UnitTest.Entities
{
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
}

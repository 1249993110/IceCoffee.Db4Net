using IceCoffee.Db4Net.Core.OptionalAttributes;

namespace IceCoffee.Db4Net.UnitTest.Entities
{
    [Table("country")]
    public class Country
    {
        [Column("Code"), UniqueKey]
        public int Id { get; set; }

        public required string Name { get; set; }

        public int Sort { get; set; }
    }
}

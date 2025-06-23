using IceCoffee.Db4Net.Core.OptionalAttributes;

namespace IceCoffee.Db4Net.UnitTest.Entities
{
    [Table("role")]
    public class Role
    {
        [Column("id"), UniqueKey, DatabaseGenerated]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("created_at"), DatabaseGenerated]
        public DateTime CreatedAt { get; set; }
    }
}

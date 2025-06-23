using IceCoffee.Db4Net.Core.OptionalAttributes;

namespace IceCoffee.Db4Net.UnitTest.Entities
{
    [Table("role")]
    public class UserRole
    {
        [Column("user_id"), UniqueKey]
        public int UserId { get; set; }

        [Column("role_id"), UniqueKey]
        public int RoleId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

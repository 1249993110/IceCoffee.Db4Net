namespace IceCoffee.Db4Net.Core.OptionalAttributes
{
    /// <summary>
    /// Identify unmapped field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotMappedAttribute : Attribute
    {
    }
}
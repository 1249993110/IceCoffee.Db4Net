namespace IceCoffee.Db4Net.Core.OptionalAttributes
{
    /// <summary>
    /// Identify unique key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueKeyAttribute : Attribute
    {
    }
}
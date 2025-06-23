namespace IceCoffee.Db4Net.Core.OptionalAttributes
{
    /// <summary>
    /// Identify ignore insertion.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnoreInsertAttribute : Attribute
    {
    }
}
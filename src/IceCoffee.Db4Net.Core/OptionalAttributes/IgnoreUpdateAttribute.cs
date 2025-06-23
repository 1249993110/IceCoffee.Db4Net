namespace IceCoffee.Db4Net.Core.OptionalAttributes
{
    /// <summary>
    /// Identify ignore update field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
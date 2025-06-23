namespace IceCoffee.Db4Net.Core.OptionalAttributes
{
    /// <summary>
    /// Attribute to specify the column for a property in a database table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public ColumnAttribute(string fieldName)
        {
            Name = fieldName;
        }

        public string Name { get; set; }
    }
}
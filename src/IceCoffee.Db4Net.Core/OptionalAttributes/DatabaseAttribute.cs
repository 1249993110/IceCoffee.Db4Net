namespace IceCoffee.Db4Net.Core.OptionalAttributes
{
    /// <summary>
    /// Specifies metadata about a database associated with an assembly or class.
    /// </summary>
    /// <remarks>This attribute can be applied to assemblies or classes to provide information about the
    /// database they are associated with. It is primarily used for configuration or identification purposes.</remarks>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DatabaseAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public DatabaseAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
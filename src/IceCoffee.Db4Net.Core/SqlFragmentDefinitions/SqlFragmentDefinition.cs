namespace IceCoffee.Db4Net.Core.SqlFragmentDefinitions
{
    internal abstract class SqlFragmentDefinition
    {
        internal abstract string Render(ParameterBuilder parameterBuilder);
    }
}

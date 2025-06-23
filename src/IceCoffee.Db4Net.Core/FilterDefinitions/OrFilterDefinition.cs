namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal sealed class OrFilterDefinition : FilterDefinition
    {
        private readonly IEnumerable<FilterDefinition> _filters;

        public OrFilterDefinition(IEnumerable<FilterDefinition> filters)
        {
            _filters = filters;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            return string.Join(" OR ", _filters.Select(i => i is AndFilterDefinition ? "(" + i.Render(parameterBuilder) + ")" : i.Render(parameterBuilder)));
        }
    }
}
